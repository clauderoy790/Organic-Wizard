using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard
{
    public class Theme
    {
        private static Dictionary<ETheme, Theme> themes;
        static Theme()
        {
            themes = new Dictionary<ETheme, Theme>()
            {
                { ETheme.Normal, new Theme(Color.FromArgb(240,240,240),Color.Black) },
                { ETheme.Dark, new Theme(Color.Black,Color.White) }
            };
        }

        public static Theme Current;

        public static event Action Changed;

        public static void SetTheme(ETheme theme)
        {
            if (themes[theme] != Current)
            {
                Current = themes[theme];
                Changed?.Invoke();
            }
        }

        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }

        public Theme(Color backColor, Color foreColor)
        {
            this.BackColor = backColor;
            this.ForeColor = foreColor;
        }

        public enum ETheme
        {
            Normal,
            Dark
        }
    }
}
