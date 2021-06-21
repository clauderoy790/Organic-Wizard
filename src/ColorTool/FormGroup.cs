using Shared;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorTool
{
    public partial class FormGroup : Form
    {
        List<PointInfo> _points;
        Color defaultColor = Color.FromArgb(255,240,240,240);
        public FormGroup()
        {
            InitializeComponent();
            _points = new List<PointInfo>();
            _points.Add(new PointInfo(textBox1, panel1));
            _points.Add(new PointInfo(textBox2, panel2));
            _points.Add(new PointInfo(textBox3, panel3));
            _points.Add(new PointInfo(textBox4, panel4));
            _points.Add(new PointInfo(textBox5, panel5));
            _points.Add(new PointInfo(textBox6, panel6));
            _points.Add(new PointInfo(textBox7, panel7));
            _points.Add(new PointInfo(textBox8, panel8));
            _points.Add(new PointInfo(textBox9, panel9));
            _points.Add(new PointInfo(textBox10, panel10));
            _points.Add(new PointInfo(textBox11, panel11));
            _points.Add(new PointInfo(textBox12, panel12));
            _points.Add(new PointInfo(textBox13, panel13));
            _points.Add(new PointInfo(textBox14, panel14));
            _points.Add(new PointInfo(textBox15, panel15));
            _points.Add(new PointInfo(textBox16, panel16));
            _points.Add(new PointInfo(textBox17, panel17));
            _points.Add(new PointInfo(textBox18, panel18));
            _points.Add(new PointInfo(textBox21, panel22));
            _points.Add(new PointInfo(textBox21, panel21));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupPointInfo();
        }

        private void SetupPointInfo()
        {
            foreach (var point in _points)
            {
                point.Panel.BackColor = Color.Empty;
                point.TextBox.TextChanged += OnTxtChanged;
                ToolTip tool = new ToolTip();
                point.ToolTip = tool;
            }
            lblDiff.Text = $"Color Diff: 0";
        }

        private void OnTxtChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string[] arr = txt.Text.Split(',');
            if (arr.Length != 2)
                return;
            int x = 0;
            int y = 0;

            int.TryParse(arr[0], out x);
            int.TryParse(arr[1], out y);

            if (x > 0 && y > 0)
            {
                var tooltip = GetToolTip(txt);
                Color col = ColorUtils.GetColorAt(x, y);
                var panel = GetPanel(txt);
                panel.BackColor = col;

                tooltip.SetToolTip(panel, "");
                if (col != Color.Empty)
                    tooltip.SetToolTip(panel, $"R:{col.R} G:{col.G} B:{col.B}");
                CalculateColorDiff();
            }
        }

        private void CalculateColorDiff()
        {
            double diff = 0;
            List<PointInfo> nonEmpty = new List<PointInfo>();
            foreach (var point in _points)
            {
                if (point.Panel.BackColor != Color.Empty && point.Panel.BackColor != defaultColor && point.Panel.BackColor.Name.ToLower() != "control")
                    nonEmpty.Add(point);
            }

            if (nonEmpty.Count >= 2)
            {
                for (int i = 1; i < nonEmpty.Count; ++i)
                {
                    Color c1 = nonEmpty[i].Panel.BackColor;
                    Color c2 = nonEmpty[i - 1].Panel.BackColor;
                    diff += ColorUtils.ColorDiff(c1, c2);
                }
                diff = diff / (nonEmpty.Count-1);
            }
               
            lblDiff.Text = $"Color Diff: {Math.Round(diff)}";
        }

        private Panel GetPanel(TextBox textbox)
        {
            Panel panel = null;

            foreach (var point in _points)
            {
                if (point.TextBox == textbox)
                {
                    panel = point.Panel;
                }
            }

            return panel;
        }

        private ToolTip GetToolTip(TextBox textbox)
        {
            ToolTip tooltip = null;

            foreach (var point in _points)
            {
                if (point.TextBox == textbox)
                {
                    tooltip = point.ToolTip;
                }
            }

            return tooltip;
        }

        public class PointInfo
        {
            public TextBox TextBox { get; set; }
            public Panel Panel { get; set; }
            public ToolTip ToolTip { get; set; }

            public PointInfo(TextBox txt, Panel pan)
            {
                TextBox = txt;
                Panel = pan;
            }
        }
    }
}
