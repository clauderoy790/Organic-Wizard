using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Shared;
using Organic_Wizard.Logic;
using Vector = System.Windows.Vector;

namespace Organic_Wizard
{
    public class AttackState : CharState
    {
        const int NB_R = 2;
        const int NB_ATTACKS = 3;
        const float KEY_DELAY = 0.3f;
        const float ATTACK_ANIMATION_DELAY = 0.4f;
        const float MAX_IDLE_TIME = 5f;

        List<SkillInfo> _skills;
        SkillInfo _currentskill = null;
        int _keyCounter = 0;
        int _previousMpPercent = 0;
        bool _attackStarted = false;
        bool _manaRegeneratedWhileSendingSkill = false;
        bool _sendingSkillKey = false;
        bool _usedAtLeastOneCooldown = false;
        Vector _startPos;

        CTimer _timerIdle;
        CTimer _timerZ;

        public bool MonsterDead { get; private set; }

        public AttackState(StateMachine sm) : base(sm)
        {
            CreateTimers();
            _skills = new List<SkillInfo>();
        }

        void CreateTimers()
        {
            _timerIdle = new CTimer();
            _timerIdle.Interval = MAX_IDLE_TIME * 1000;
            _timerIdle.Elapsed += OnIdleTimerElapsed;

            _timerZ = new CTimer();
            _timerZ.Interval = KEY_DELAY * 1000;
            _timerZ.Elapsed += OnZTimer;

            LogicEngine.Stopped -= OnStopped;
            LogicEngine.Stopped += OnStopped;
        }


        public override void OnEnter()
        {
            base.OnEnter();
            MonsterDead = false;
            _usedAtLeastOneCooldown = false;
            _keyCounter = 0;
            CreateTimers();
            _attackStarted = false;
            _sendingSkillKey = false;
            _manaRegeneratedWhileSendingSkill = false;
            if (_skills.Count == 0)
                InitSkills();

            if (!SavedData.AttackMode)
            {
                FinishState();
                return;
            }
            else
            {
                _startPos = Character.Position;
                _previousMpPercent = Character.MpPercent;
                Character.MpChanged -= OnMpChanged;
                Character.MpChanged += OnMpChanged;
                Character.SelectedMonsterHpChanged -= OnMonsterHpChanged;
                Character.SelectedMonsterHpChanged += OnMonsterHpChanged;

                _timerIdle.Restart();

                SendInstantKey("zr", () =>
                {
                    _timerZ.Restart(); //Start pressing z
                });
            }
        }

        private void OnZTimer(object sender, ElapsedEventArgs e)
        {
            _timerZ.Stop();

            if (!Character.IsMonsterSelected || Character.SelectedMonsterHpPercent == 0 ||
                _skills.Count == 0 || (SavedData.MaxTravelRange > 0 && _startPos == Character.Position))
            {
                SendInstantKey("zr", () =>
                {
                    _timerZ.Start();
                });
            }
            else
            {
                SendKeyUp('r', Attack);
            }

        }

        private void InitSkills()
        {
            _skills = new List<SkillInfo>();
            int count = SavedData.GetAttackSkillsCount();
            for (int i = 0; i < count; ++i)
            {
                int skillNumber = SavedData.GetAttackSkillAtPos(i);
                if (skillNumber != Constants.NONE)
                {
                    SkillInfo skill = SkillBar.GetSkillForNumber(skillNumber);
                    _skills.Add(skill);
                }
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (IsActive && SavedData.MaxTravelRange != Constants.NONE)
            {
                double distance = (Character.InitialPosition - Character.Position).Length;
                if (distance >= SavedData.MaxTravelRange)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data["destination"] = Character.InitialPosition;
                    SetState(EState.Move, data);
                }
            }
        }

        private void Attack()
        {
            if (!IsActive)
                return;

            _sendingSkillKey = false;
            _attackStarted = true;
            _currentskill = GetSkillToUse();
            if (_currentskill == null)
                return;

            int modulo = _keyCounter++ % (NB_R + NB_ATTACKS);
            if (modulo < NB_R)
            {
                SendKey("zr", KEY_DELAY, Attack);
            }
            else
            {
                _sendingSkillKey = true;
                _manaRegeneratedWhileSendingSkill = false;
                SendKey(_currentskill.Number, ATTACK_ANIMATION_DELAY, Attack);
            }
        }

        private void OnMonsterHpChanged(int hp)
        {
            Debug.Log("Monster HP changed:" + hp);
            if (hp == 0)
            {
                MonsterDead = true;
                ClickAt(ScreenPosition.Center, () =>
                {
                    Debug.Log($"loot: {SavedData.Loot}, is full: {Inventory.IsFull}, Used one cd: {_usedAtLeastOneCooldown}");
                    if (SavedData.Loot && !Inventory.IsFull && _usedAtLeastOneCooldown)
                        SetState(EState.CheckInventory);
                    else
                    {
                        System.Console.WriteLine("finishsing it");
                        FinishState();
                    }
                });
            }
        }

        private void OnIdleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timerIdle.Stop();
            if (!_attackStarted)
            {
                System.Console.WriteLine("idle too long");
                FinishState();
            }
        }


        private SkillInfo GetSkillToUse()
        {
            if (_skills.Count == 0)
                return null;

            SkillInfo skillToUse = null;
            int currentSkillMp = -1;

            //Take the highest possible mp skill
            for (int i = 0; i < _skills.Count; i++)
            {
                SkillInfo skill = _skills[i];
                if (skill.PercentageMpUsed <= Character.MpPercent && skill.PercentageMpUsed > currentSkillMp)
                {
                    bool isOnCoooldown = skill.IsOnCooldown();
                    _usedAtLeastOneCooldown = _usedAtLeastOneCooldown | isOnCoooldown;
                    if (!isOnCoooldown)
                    {
                        skillToUse = skill;
                        currentSkillMp = skill.PercentageMpUsed;
                    }
                }
            }

            return skillToUse;
        }

        public override void OnLeave()
        {
            base.OnLeave();
            SendKeyUp('w');
            SendKeyUp('r');
            _attackStarted = false;
            Character.MpChanged -= OnMpChanged;
            Character.SelectedMonsterHpChanged -= OnMonsterHpChanged;
            _timerZ.Stop();
            _timerIdle.Stop();
        }

        private void OnMpChanged(int currentPercent)
        {
            if (_sendingSkillKey)
            {
                if (currentPercent > _previousMpPercent)
                {
                    _manaRegeneratedWhileSendingSkill = true;
                }
                else if (!_manaRegeneratedWhileSendingSkill && _currentskill != null && currentPercent < _previousMpPercent)
                {
                    int mpUsed = _previousMpPercent - currentPercent;
                    if (mpUsed > _currentskill.PercentageMpUsed)
                    {
                        Debug.Log($"Setting mp used for skill {_currentskill.Number} to {mpUsed}");
                        _currentskill.PercentageMpUsed = mpUsed;
                        _skills.Sort(SkillsCompare);
                    }
                }
            }

            _previousMpPercent = currentPercent;
        }

        private int SkillsCompare(SkillInfo skill1, SkillInfo skill2)
        {
            if (skill1.PercentageMpUsed > skill2.PercentageMpUsed)
            {
                return -1;
            }
            else if (skill1.PercentageMpUsed < skill2.PercentageMpUsed)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void OnStopped()
        {
            _skills.Clear();
        }

        public override EState GetName()
        {
            return EState.Attacking;
        }
    }
}
