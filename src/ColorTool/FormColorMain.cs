using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorTool
{
    public partial class FormColorMain : Form
    {
        Timer _healTimer = null;
        Timer _updateTimer = null;
        bool _recordCursorPos = false;
        KListener _keyListener = null;
        private Point _savedPosition;
        Color _savedColor;

        public FormColorMain()
        {
            InitializeComponent();
            ImgUtils.Init();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _keyListener = new KListener();

            _updateTimer = new Timer();
            _updateTimer.Tick += OnUpateTick;
            _updateTimer.Interval = 20;

            _healTimer = new Timer();
            _healTimer.Tick += OnHeal;
            _healTimer.Interval = 1000;

            _keyListener.KeyDown += OnKKDown;
            _updateTimer.Start();
            _healTimer.Start();
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                ResearchColor();
            }
            else
            {
                Go();
            }

        }

        private void Go()
        {
            var watch = Stopwatch.StartNew();
            var result = ImgUtils.ProcessImage(104, 75, 175, 90, 2);
            txtProcess.Text = result.Text;
            var ms = watch.ElapsedMilliseconds;
            WinUtils.ActivateWindow("Notepad");
        }

        private void ResearchColor()
        {
            Color c = Color.FromName("wfe4rewerw");
            Color col = Color.Empty;
            int nb;
            if (int.TryParse(txtSearch.Text, out nb))
            {
                col = CUtils.GetColorFromInt(nb);
            }
            else
            {
                col = Color.FromName(txtSearch.Text);
                if (col == Color.Empty)
                    col = CUtils.GetHexColorFromString(txtSearch.Text);
            }

            if (col != Color.Empty)
                UpdateColorUI(col);
        }

        private void OnHeal(object sender, EventArgs e)
        {
            //string text = ProcessImage(104, 75, 175, 88);
            //string x = ProcessImage(104,75,136,87);
            //string y = ProcessImage(138,75,169,87);
            //this.Invoke((MethodInvoker)delegate
            //{
            //    Console.WriteLine(text);
            //});
            //SendKeys.SendWait("{TAB}");
            //int colorDiff = Math.Abs(GetIntColor(GetColorAt(579, 47)) - 0x121212);
            //if (colorDiff < 100000)
            //    SendKeys.SendWait("1");
        }

        private void OnKKDown(object sender, RawKeyEventArgs args)
        {
            switch (args.Key)
            {
                case Keys.Q:
                    _recordCursorPos = !_recordCursorPos;
                    break;
                case Keys.R:
                    _savedPosition = Cursor.Position;
                    _savedColor = CUtils.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
                    panSavedColor.BackColor = _savedColor;
                    panSavedColor.ForeColor = _savedColor;
                    txtSavedHex.Text = CUtils.GetHexColor(_savedColor);
                    txtSavedX.Text = _savedPosition.X + "";
                    txtSavedY.Text = _savedPosition.Y + "";
                    break;
                case Keys.Left:
                    if (_recordCursorPos)
                    {
                        Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                    }
                    break;
                case Keys.Right:
                    if (_recordCursorPos)
                    {
                        Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                    }
                    break;
                case Keys.Down:
                    if (_recordCursorPos)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                    }
                    break;
                case Keys.Up:
                    if (_recordCursorPos)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                    }
                    break;
            }
        }

        private void OnUpateTick(object sender, EventArgs e)
        {
            if (_recordCursorPos)
            {
                this.Invoke((MethodInvoker)delegate {
                    int x = Cursor.Position.X;
                    int y = Cursor.Position.Y;
                    txtX.Text = x.ToString();
                    txtY.Text = y.ToString();
                    UpdateColorUI(x, y);
                });
            }
        }



        private void UpdateColorUI(int x, int y)
        {
            Color col = CUtils.GetColorAt(x, y);
            UpdateColorUI(col);
        }

        private void UpdateColorUI(Color col)
        {

            panColor.BackColor = col;
            panColor.ForeColor = col;
            txtHexColor.Text = CUtils.GetHexColor(col).ToString();
            txtIntColor.Text = CUtils.GetColorAsInt(col).ToString();

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            int colDiff = CUtils.ColorDiff(_savedColor, GetCursorColor());
            if (colDiff == 0 && Cursor.Position != _savedPosition)
                Console.WriteLine("SAME COLOR AS SAVED: " + _savedPosition);
            txtDiff.Text = colDiff + "";
        }

        public static Color GetResearchFieldColor(TextBox txt)
        {
            Color val = Color.Empty;

            if (!string.IsNullOrEmpty(txt.Text))
            {
                Color[] cols = new Color[]
                {
                        Color.FromName(txt.Text),
                        CUtils.GetHexColorFromString(txt.Text),
                };

                foreach (var col in cols)
                {
                    if (col.A > 0 && col != Color.Empty)
                    {
                        val = col;
                        break;
                    }
                }
            }
            return val;
        }



        private Color GetCursorColor()
        {
            return CUtils.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
        }

        private void btnHexColor_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHexColor.Text);
        }

        private void btnIntColor_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIntColor.Text);
        }

        private void btnHex_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHexColor.Text);
        }

        private void btnInt_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIntColor.Text);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            //CharacterInfoDebugForm debug = new CharacterInfoDebugForm();
            //debug.Show();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            FormColorCompare frmCompare = new FormColorCompare();
            frmCompare.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Color col = GetResearchFieldColor(txtSearch);
            if (col != Color.Empty)
                UpdateColorUI(col);
        }
    }
}
