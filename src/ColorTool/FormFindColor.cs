using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ColorTool
{
    public partial class FormFindColor : Form
    {
        string saveFilePath = "";
        public FormFindColor()
        {
            saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "formFind.sav");
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Save();
            Point point = GetPointForTextBox(txtStartPos);
            Color color = GetColorForTextBox(txtRgb);
            int width, height, diff = 0;
            int.TryParse(txtHeight.Text, out height);
            int.TryParse(txtWidth.Text, out width);
            int.TryParse(txtDiff.Text, out diff);
            if (point.IsZero() || color == Color.Empty || width == 0 || height == 0)
                return;

            LockBitmap lockB = ImgUtils.GetLockBitmap(point.X, point.Y, width, height);
            lockB.LockBits();
            var points = FindPoints(point, lockB, color, diff);
            txtOutput.Clear();
            if (points.Count == 0)
                txtOutput.AppendText("No Result!");
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    var tuple = points[i];
                    txtOutput.AppendText($"{tuple.Item1.X}, {tuple.Item1.Y} | diff: {tuple.Item2}{Environment.NewLine}");
                }
            }
            lockB.UnlockBits();

        }

        void Save()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    File.Delete(saveFilePath);
                }

                string[] lines = new string[]
                {
                    txtStartPos.Text,
                    txtWidth.Text,
                    txtHeight.Text,
                    txtRgb.Text,
                    txtDiff.Text
                };
                File.WriteAllLines(saveFilePath, lines);
            }
            catch { }
        }

        void LoadData()
        {
            try
            {
                if (!File.Exists(saveFilePath))
                    return;

                string[] lines = File.ReadAllLines(saveFilePath);
                if (lines == null && lines.Length == 5)
                {
                    txtStartPos.Text = lines[0];
                    txtWidth.Text = lines[1];
                    txtHeight.Text = lines[2];
                    txtRgb.Text = lines[3];
                    txtDiff.Text = lines[4];
                }
                File.WriteAllLines(saveFilePath, lines);
            }
            catch { }
        }

        private Point GetPointForTextBox(TextBox txt)
        {
            Point point = new Point();
            try
            {
                var split = txt.Text.Split(',');
                int x = int.Parse(split[0].Trim());
                int y = int.Parse(split[1].Trim());
                point = new Point(x, y);
            }
            catch
            { }

            return point;
        }

        private Color GetColorForTextBox(TextBox txt)
        {
            Color color = new Color();
            try
            {
                var split = txt.Text.Split(',');
                int r = int.Parse(split[0].Trim());
                int g = int.Parse(split[1].Trim());
                int b = int.Parse(split[2].Trim());
                color = Color.FromArgb(r, g, b);
            }
            catch
            { }

            return color;
        }

        private static List<Tuple<Point,int>> FindPoints(Point from, LockBitmap bmp, Color color, int diff)
        {
            List<Tuple<Point, int>> points = new List<Tuple<Point, int>>();

            for (int x = 0; x < bmp.Width; ++x)
            {
                for (int y = 0; y < bmp.Height; ++y)
                {
                    Color col = bmp.GetPixel(x, y);
                    int d = ColorUtils.ColorDiff(color, col);
                    if (d <= diff)
                    {
                        points.Add(new Tuple<Point, int>(new Point(from.X + x, from.Y + y), d));
                    }
                }
            }
            return points;
        }

        private static bool PointAreInBounds(PointGroup group, Point initialPoint, int width, int height)
        {
            bool inBounds = true;

            foreach (var point in group.Points)
            {
                if (point.X >= initialPoint.X + width || point.Y >= initialPoint.Y + height)
                {
                    inBounds = false;
                    break;
                }
            }

            return inBounds;
        }
    }
}
