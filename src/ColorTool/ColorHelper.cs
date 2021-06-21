using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorTool
{
    public class ColorHelper
    {
        static Dictionary<TextBox, Action<TextColorInfo>> _txts = new Dictionary<TextBox, Action<TextColorInfo>>();
       

        internal static void RegisterColorField(TextBox txtSearch,Action<TextColorInfo> onColorChanged)
        {
            _txts[txtSearch] = onColorChanged;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private static void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            Color c = TryParseColor(txt);
            TextColorInfo info = new TextColorInfo()
            {
                TextBox = txt,
                Color = c,
            };
            _txts[txt]?.Invoke(info);
        }

        private static Color TryParseColor(TextBox txt)
        {
            Color col = Color.Empty;
            Color nameColor = Color.FromName(txt.Text);
            Color hexColor = ColorUtils.GetHexColorFromString(txt.Text);
            int nb;
            if (int.TryParse(txt.Text, out nb))
            {
                col = ColorUtils.GetColorFromInt(nb);
            }
            else if (txt.Text.Split(',').Length == 3)
            {
                var rgbStrings = txt.Text.Split(',').ToList();
                int[] rgb = new int[3];
                for (int i = 0; i < rgbStrings.Count; i++)
                {
                    try
                    {
                        rgb[i] = int.Parse(rgbStrings[i].Trim());
                        col = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    }
                    catch
                    {

                    }
                }

            }
            else if (nameColor.R != 0 || nameColor.G != 0 || nameColor.B != 0)
            {
                col = nameColor;
            }
            else if (hexColor.R != 0 || hexColor.G != 0 || hexColor.B != 0)
            {
                col = ColorUtils.GetHexColorFromString(txt.Text);
            }
            return col;
        }

        public class TextColorInfo
        {
            public TextBox TextBox { get; set; }
            public Color Color { get; set; }
        }
    }
}
