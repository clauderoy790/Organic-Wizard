using Organic_Wizard.Logic;
using Shared;
using System;
using System.Drawing;
using Vector = System.Windows.Vector;

namespace Organic_Wizard
{
    public class Character : Component
    {
        public static event Action<int> HpChanged;
        public static event Action<int> MpChanged;
        public static event Action<Vector> PositionChanged;
        public static event Action<int> PartySizeChanged;
        public static event Action<int> PartyMemberHpChanged;
        public static event Action<bool> PartyMemberSelectedChanged;
        public static event Action<bool> IsInPartyChanged;
        public static event Action<EState> StateChanged;
        public static event Action<bool> KingDisplayChanged;
        public static event Action<bool> MonsterSelectedChanged;
        public static event Action<int> SelectedMonsterHpChanged;
        public static event Action<int> SelectedPartyMemberIndexChanged;

        public static EState State { get { return _instance._currentInfo.State; } }
        public static Vector Position { get { return _instance._currentInfo.Position; } }
        public static Vector InitialPosition { get { return _instance._currentInfo.InitialPosition; } }
        public static Vector Direction { get { return _instance._dirComp.Direction; } }

        public static int HpPercent { get { return _instance._currentInfo.HpPercent; } }
        public static int MpPercent { get { return _instance._currentInfo.MpPercent; } }
        public static int PartySize { get { return _instance._currentInfo.PartySize; } set { _instance._currentInfo.PartySize = value; } }
        public static int SelectedPartyMemberIndex { get { return _instance._currentInfo.SelectedPartyMemberIndex; } set { _instance._currentInfo.SelectedPartyMemberIndex = value; } }
        public static int PtMemberHpPercent { get { return _instance._currentInfo.SelectedPartyMemberHpPercent; } }

        public static bool IsInParty {get { return _instance._currentInfo.IsInParty; } set {_instance._currentInfo.IsInParty = value;} }

        public static bool IsPartyMemberSelected { get { return _instance._currentInfo.IsPartyMemberSelected; } }

        public static bool IsKingDisplayed { get { return _instance._currentInfo.IsKingDisplayed; } }
        public static bool IsMonsterSelected { get { return _instance._currentInfo.IsMonsterSelected; } }
        public static int SelectedMonsterHpPercent { get { return _instance._currentInfo.SelectedMonsterHpPercent; } }

        private static Character _instance = null;

        CharacterInfo _currentInfo;
        CharacterInfo _previousFrameInfo;
        DirComponent _dirComp;
        ResourceBar _mpBar = null;
        ResourceBar _hpBar = null;
        ResourceBar _partyBar = null;
        ResourceBar _monsterBar = null;
        PointGroup _partyBarPoints = null;
        PointGroup _partyBarPointsOffseted = null;
        CharacterStateMachine _stateMachine = null;

        public Character(CharacterStateMachine sm) : base()
        {
            _instance = this;
            _stateMachine = sm;
            _previousFrameInfo = new CharacterInfo();
            _currentInfo = new CharacterInfo();
        }

        public override void onAttach()
        {
            base.onAttach();
            _dirComp = GetComponent<DirComponent>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _previousFrameInfo = new CharacterInfo();
            _currentInfo = new CharacterInfo();
            _partyBarPoints = new PointGroup(PointExt.New(393, 47), PointExt.New(599, 52), PointExt.New(605, 45));
            _partyBarPointsOffseted = _partyBarPoints.OffsetCopy(0, 40);
            _previousFrameInfo = new CharacterInfo();
            _hpBar = new ResourceBar(27, 217, 36, ResourceBar.EResourceType.Hp);
            _hpBar.ColorDiffTolerance = 5;
            _mpBar = new ResourceBar(27, 217, 53, ResourceBar.EResourceType.Mana);
            _mpBar.ColorDiffTolerance = 5;
            _partyBar = new ResourceBar(406, 598, 49, ResourceBar.EResourceType.PartyMemberHp);
            _monsterBar = new ResourceBar(406, 598, 49, ResourceBar.EResourceType.MonsterHp);
            _monsterBar.NbConsistencyFrame = 1;

            CalculateAttributes();
            _currentInfo.InitialPosition = _currentInfo.Position;
            _previousFrameInfo.CopyFrom(_currentInfo);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            var ms = Debug.CheckExecutionTime(() =>
            {
                CalculateAttributes();
                _currentInfo.State = _stateMachine.State;
                FireEventsIfNeeded();
                _previousFrameInfo.CopyFrom(_currentInfo);
            });
        }

        private void CalculateAttributes()
        {
            CalculateHpMp();
            CalculateKingDisplay();
            CalculatePartyBar();
            CalculatePosition();
            CalculateMonster();
        }

        private void CalculateMonster()
        {
            int diff = ColorUtils.ColorDiff(new Point(398, 45), new Point(606, 44));
            diff += ColorUtils.ColorDiff(new Point(394, 51), new Point(610, 50));
            _currentInfo.IsMonsterSelected = diff == 0;
            if (IsMonsterSelected)
            {
                _monsterBar.TrySetResColor(_hpBar.Color, 39);
                _currentInfo.SelectedMonsterHpPercent = _monsterBar.GetCurrentPercent();
            }
            else
                _currentInfo.SelectedMonsterHpPercent = 0;
        }

        private void CalculatePartyBar()
        {
            TryInitializePartyBarColor();
            if (IsPartyMemberSelected)
            {
                _currentInfo.SelectedPartyMemberHpPercent = _partyBar.GetCurrentPercent();
            }
            else
                _currentInfo.SelectedPartyMemberHpPercent = 0;

            Color col = ColorUtils.GetColorAt(_partyBar.Position);
            int diff = ColorUtils.ColorDiff(_hpBar.Color, col);
            _currentInfo.IsPartyMemberSelected = diff < 40 && ColorUtils.IsSimilarColor(_partyBarPoints);
        }

        private void TryInitializePartyBarColor()
        {
            if (!_partyBar.IsColorInitialized)
            {
                bool setted = _partyBar.TrySetResColor(_hpBar.Color, 39);
                if (setted)
                {
                    _currentInfo.IsKingDisplayed = false;
                }
                else
                {
                    bool isSameColor = ColorUtils.IsSimilarColor(_partyBarPointsOffseted);
                    _partyBar.Offset(0, 40);
                    setted = _partyBar.TrySetResColor(_hpBar.Color, 39);
                    if (setted)
                    {
                        _currentInfo.IsKingDisplayed = true;
                    }
                    else
                        _partyBar.ResetOffset();
                }
            }
        }

        private void CalculateHpMp()
        {
            _currentInfo.HpPercent = _hpBar.GetCurrentPercent();
            _currentInfo.MpPercent = _mpBar.GetCurrentPercent();
            if (_currentInfo.HpPercent < 80 || _currentInfo.MpPercent < 80)
            {
                Console.WriteLine("tests");
            }
        }

        private void CalculatePosition()
        {
            var result = ImgUtils.ProcessImage(104, 75, 175, 90, 2);
            if (result == null || string.IsNullOrEmpty(result.Text))
            {
                return;
            }
            Vector newPos;
            bool valid = GetPositionForText(result.Text,out newPos);
            if (valid)
            {
                _currentInfo.Position = newPos;
            }
        }

        private bool GetPositionForText(string text, out Vector output)
        {
            bool valid = false;
            output = new Vector(0, 0);
            if (!string.IsNullOrEmpty(text))
            {
                string[] splitStrings = text.Trim().Split(',');
                if (splitStrings.Length == 1)
                    splitStrings = text.Split(' ');

                if (splitStrings.Length == 2)
                {
                    int x, y = 0;
                    valid = StringFormatter.TryConvertToInt(splitStrings[0], out x);
                    valid = valid && StringFormatter.TryConvertToInt(splitStrings[1], out y);

                    if (valid)
                    {
                        output = new Vector(x, y);
                    }
                }
            }

            return valid;
        }

        private void CalculateKingDisplay()
        {
            if (!IsPartyMemberSelected && _previousFrameInfo.IsPartyMemberSelected)
            {
                bool offseted = _partyBarPoints.SameColor(_partyBarPointsOffseted);
                if (offseted)
                    _currentInfo.IsKingDisplayed = !_currentInfo.IsKingDisplayed;
                if (_currentInfo.IsKingDisplayed)
                {
                    _partyBar.Offset(0, 40);
                }
                else
                {
                    _partyBar.ResetOffset();
                }
            }
        }

        private void FireEventsIfNeeded()
        {
            if (_currentInfo.State != _previousFrameInfo.State)
                StateChanged?.Invoke(_currentInfo.State);
            if (_currentInfo.HpPercent != _previousFrameInfo.HpPercent)
                HpChanged?.Invoke(_currentInfo.HpPercent);
            if (_currentInfo.IsInParty != _previousFrameInfo.IsInParty)
                IsInPartyChanged?.Invoke(_currentInfo.IsInParty);
            if (_currentInfo.MpPercent != _previousFrameInfo.MpPercent)
                MpChanged?.Invoke(_currentInfo.MpPercent);
            if (_currentInfo.PartySize != _previousFrameInfo.PartySize)
                PartySizeChanged?.Invoke(_currentInfo.PartySize);
            if (_currentInfo.Position.X != _previousFrameInfo.Position.X || _currentInfo.Position.Y != _previousFrameInfo.Position.Y)
                PositionChanged?.Invoke(_currentInfo.Position);

            if (IsInParty && _currentInfo.SelectedPartyMemberHpPercent != _previousFrameInfo.SelectedPartyMemberHpPercent)
                PartyMemberHpChanged?.Invoke(_currentInfo.SelectedPartyMemberHpPercent);
            if (_currentInfo.IsPartyMemberSelected != _previousFrameInfo.IsPartyMemberSelected)
                PartyMemberSelectedChanged?.Invoke(_currentInfo.IsPartyMemberSelected);
            if (_currentInfo.IsKingDisplayed != _previousFrameInfo.IsKingDisplayed)
                KingDisplayChanged?.Invoke(_currentInfo.IsKingDisplayed);
            if (_currentInfo.IsMonsterSelected != _previousFrameInfo.IsMonsterSelected)
                MonsterSelectedChanged?.Invoke(_currentInfo.IsMonsterSelected);
            if (_currentInfo.SelectedMonsterHpPercent != _previousFrameInfo.SelectedMonsterHpPercent)
                SelectedMonsterHpChanged?.Invoke(_currentInfo.SelectedMonsterHpPercent);
            if (_currentInfo.SelectedPartyMemberIndex != _previousFrameInfo.SelectedPartyMemberIndex)
                SelectedPartyMemberIndexChanged?.Invoke(_currentInfo.SelectedPartyMemberIndex);

        }

        public class CharacterInfo
        {
            public EState State { get; set; }
            public Vector Position { get; set; }
            public Vector InitialPosition {get;set;}

            public int HpPercent { get; set; }
            public int MpPercent { get; set; }
            public int PartySize { get; set; }
            public int SelectedPartyMemberHpPercent { get; set; }
            public int SelectedPartyMemberIndex { get; set; }
            public int SelectedMonsterHpPercent { get; set; }

            public bool IsInParty { get; set; }

            public bool IsPartyMemberSelected { get; set; }

            public bool IsKingDisplayed { get; set; }
            public bool IsMonsterSelected { get; internal set; }

            public CharacterInfo()
            {
                Position = new Vector(0, 0);
                State = EState.Idle;
                SelectedPartyMemberIndex = -1;
            }

            public void CopyFrom(CharacterInfo info)
            {
                this.State = info.State;
                this.Position = new Vector(info.Position.X, info.Position.Y);
                this.InitialPosition = new Vector(info.InitialPosition.X, info.InitialPosition.Y);
                this.HpPercent = info.HpPercent;
                this.MpPercent = info.MpPercent;
                this.PartySize = info.PartySize;
                this.SelectedPartyMemberHpPercent = info.SelectedPartyMemberHpPercent;
                this.IsInParty = info.IsInParty;
                this.IsPartyMemberSelected = info.IsPartyMemberSelected;
                this.IsKingDisplayed = info.IsKingDisplayed;
                this.IsMonsterSelected = info.IsMonsterSelected;
                this.SelectedMonsterHpPercent = info.SelectedMonsterHpPercent;
                this.SelectedPartyMemberIndex = info.SelectedPartyMemberIndex;
                this.InitialPosition = info.InitialPosition;
            }
        }
    }
}
