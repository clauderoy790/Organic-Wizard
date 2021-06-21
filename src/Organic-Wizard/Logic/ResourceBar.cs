using NuGet.Runtime;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Organic_Wizard
{
    public class ResourceBar
    {
        const int DEFAULT_DIFF_LIMIT = 10;
        const int DEFAULT_NB_FRAME_CONSISTENCY = 3;
        private int _resBarLength;
        private bool _isResColorSet = false;
        private Color _resBarColor = Color.Empty;
        private EResourceType _resType;
        private Point _offset = new Point(0, 0);
        private Point _position = new Point(0, 0);
        private Point _initialPosition = new Point(0, 0);
        private List<int> _perviousFramesPercent = new List<int>();
        private int _lastValidPercent = 0;


        public Color Color { get { return _resBarColor; } }
        public int NbConsistencyFrame { get; set; }

        public bool IsColorInitialized { get { return _isResColorSet; } }

        public Point Position { get { return _position; } }

        public int ColorDiffTolerance { get; set; }

        public ResourceBar(int leftX, int rightX, int bottomY, EResourceType resType)
        {
            _lastValidPercent = Constants.NONE;
            NbConsistencyFrame = DEFAULT_NB_FRAME_CONSISTENCY;
            ColorDiffTolerance = DEFAULT_DIFF_LIMIT;
            _resType = resType;
            _resBarLength = rightX - leftX;
            _resBarColor = ColorUtils.GetColorAt(leftX, bottomY);
            _initialPosition = _position = new Point(leftX, bottomY);
        }

        public void Offset(int x,int y)
        {
            _offset = new Point(x, y);
            _position = new Point(_initialPosition.X + _offset.X, _initialPosition.Y + _offset.Y);
        }

        public void ResetOffset()
        {
            Offset(0, 0);
        }

        public bool TrySetResColor(Color similarColor,int diffTolerance)
        {
            Color col = ColorUtils.GetColorAt(_position.X, _position.Y);
            if (ColorUtils.ColorDiff(col,similarColor) <= diffTolerance)
            {
                _isResColorSet = true;
                _resBarColor = col;
            }
            return _isResColorSet;
        }

        public int GetCurrentPercent()
        {
            int percent = GetPercent();

            if (_lastValidPercent == Constants.NONE) //If never initialized
                _lastValidPercent = percent;

            if (_perviousFramesPercent.Count < NbConsistencyFrame)
            {
                _perviousFramesPercent.Add(percent);
            }
            else
            {
                bool equal = true;
                for (int i = 1; i < _perviousFramesPercent.Count; i++)
                {
                    if (_perviousFramesPercent[i-1] != _perviousFramesPercent[i])
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal)
                {
                    _lastValidPercent = _perviousFramesPercent[0];
                }
                _perviousFramesPercent.Clear();
            }

            return _lastValidPercent;
        }

        private int GetPercent()
        {
            int min = 0;
            int max = 100;

            //Binary search
            while (Math.Abs(max - min) > 1)
            {
                int percent = (int)Math.Floor((min + max) / 2d);
                SimilarInfo simInfo = GetrSimilarColorInfo(percent);
                if (simInfo.IsSimilarWithBar)
                {
                    min = percent;
                }
                else
                {
                    max = percent;
                }
            }

            var maxInfo = GetrSimilarColorInfo(max);
            return (int)(maxInfo.IsSimilarWithBar ? max : min);
        }

        private SimilarInfo GetrSimilarColorInfo(int percent)
        {
            SimilarInfo info = new SimilarInfo();
            if (percent < 0 || percent > 100)
                return info;

            int exactPixel = _position.X + (int)Math.Floor(_resBarLength *1d * percent / 100d);
            Color col = ColorUtils.GetColorAt(exactPixel +_offset.X, _position.Y +_offset.Y);
            int diff = ColorUtils.ColorDiff(col, _resBarColor);
            bool isSimilar  = diff <= ColorDiffTolerance;
            info.Difference = diff;
            info.Color = col;
            info.IsSimilarWithBar = isSimilar;
            return info;
        }

        public class SimilarInfo
        {
            public  SimilarInfo()
            {
                Color = Color.Empty;
            }
            public Color Color { get; set; }
            public bool IsSimilarWithBar { get; set; }
            public int Difference { get; set; }
        }

        public enum EResourceType
        {
            Hp,
            Mana,
            PartyMemberHp,
            MonsterHp
        }

    }
}
