using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    public class ResourceBar
    {
        const int DEFAULT_DIFF_LIMIT = 10;

        private int _leftX = 0;
        private int _rightX = 0;
        private int _y = 0;
        private int _resBarLength;
        private Color _resBarColor = Color.Empty;
        private EResourceType _resType;


        public Color ResourceBarColor { get { return _resBarColor; } }

        public int ColorDiffTolerance { get; set; }

        public ResourceBar(int leftX, int rightX, int bottomY,EResourceType resType)
        {
            ColorDiffTolerance = DEFAULT_DIFF_LIMIT;
            _resType = resType;
            _y = bottomY;
            _leftX = leftX;
            _rightX = rightX;
            _resBarLength = rightX - leftX;
            _resBarColor = CUtils.GetColorAt(leftX, bottomY);
        }

        public int GetCurrentPercent()
        {
            bool found = false;
            int resLevel = 100;
            int currentX = _rightX;

            

            while (!found && resLevel > 0)
            {
                currentX = _leftX + (int)Math.Floor(_resBarLength * (resLevel / 100f));
                Color currentColor = CUtils.GetColorAt(currentX, _y);
                if (CUtils.ColorDiff(currentColor, _resBarColor) < ColorDiffTolerance)
                {
                    found = true;
                    break;
                }
                --resLevel;
            }

            if (found)
                return resLevel;
            else
                return 100;
        }
        public enum EResourceType
        {
            Hp,
            Mana,
            PartyMemberHp
        }
    }
}
