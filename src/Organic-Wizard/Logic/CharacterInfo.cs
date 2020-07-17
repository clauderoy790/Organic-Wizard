using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    class CharacterInfo : Component
    {
        const int PARTY_FRAME_COLOR_TOLERANCE = 10;

        ResourceBar _mpBar = null;
        ResourceBar _hpBar = null;
        ResourceBar _partyBar = null;
        PointGroup _partyBarPoints = null;

        public CharacterInfo()
        {
            _partyBarPoints = new PointGroup(
                new Point(405, 52), new Point(405, 39), new Point(393, 47),
                new Point(599, 52), new Point(562, 52), new Point(522, 52),
                new Point(482, 52), new Point(605, 45), new Point(444, 52),
                new Point(444, 38), new Point(482, 38), new Point(522, 38),
                new Point(562, 38), new Point(598, 38));
        }

        public static Point Position { get; private set; }

        public static int HpPercent { get; private set; }
        public static int MpPercent { get; private set; }
        public static int PartySize { get; set; }
        public static int SelectedPartyMemberHpPercent { get; set; }

        public static bool IsInParty { get; set; }

        public static bool IsPartyMemberSelected { get; set; }

        public override void OnEnable()
        {
            base.OnEnable();
            _hpBar = new ResourceBar(27, 217, 47, ResourceBar.EResourceType.Hp);
            _mpBar = new ResourceBar(27, 217, 53, ResourceBar.EResourceType.Mana);
            _mpBar.ColorDiffTolerance = 5;
            _partyBar = new ResourceBar(406, 598, 49, ResourceBar.EResourceType.PartyMemberHp);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            HpPercent = _hpBar.GetCurrentPercent();
            MpPercent =  _mpBar.GetCurrentPercent();
            IsPartyMemberSelected = CUtils.IsSimilarColor(_partyBarPoints, PARTY_FRAME_COLOR_TOLERANCE);
            if (IsPartyMemberSelected)
                SelectedPartyMemberHpPercent = _partyBar.GetCurrentPercent();
            var ms = DebugUtils.CheckExecutionTime(() =>
            {
                CalculatePosition();
            });
            Console.WriteLine(string.Format("calculatd pos in {0} ms",ms));

        }

        private void CalculatePosition()
        {
            Position = new Point(0, 0);
            var result = ImgUtils.ProcessImage(104, 75, 175, 90, 2);
            if (result == null || string.IsNullOrEmpty(result.Text))
                return;

            var text = result.Text.Trim();
            var posStrings = text.Split(',');
            int x, y = 0;
            if (posStrings.Length == 2 && int.TryParse(posStrings[0],out x) &&
                int.TryParse(posStrings[1], out y))
            {
                Position = new Point(x,y);
            }
        }
    }
}
