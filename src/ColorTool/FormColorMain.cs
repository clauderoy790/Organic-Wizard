using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ColorTool.ColorHelper;

namespace ColorTool
{
    public partial class FormColorMain : Form
    {
        CTimer _updateTimer = null;
        bool _recordCursorPos = false;
        KListener _keyListener = null;
        private Point _savedPosition;
        private Point _currentPosition;
        private Point _previousPosition;

        Color _currentColor;
        Color _savedColor;
        ToolTip btnFindToolTip;

        public FormColorMain()
        {
            InitializeComponent();
            btnFindToolTip = new ToolTip();
            ImgUtils.Init();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _keyListener = new KListener();
            _keyListener.KeyDown += OnKKDown;

            _updateTimer = new CTimer();
            _updateTimer.Elapsed += OnUpateTick;
            _updateTimer.Interval = 20;

            _recordCursorPos = true;

            _updateTimer.Start();
            ColorHelper.RegisterColorField(txtSearch, UpdateColorUI);
        }

        private void OnKKDown(object sender, RawKeyEventArgs args)
        {
            switch (args.Key)
            {
                case Keys.Q:
                    _recordCursorPos = !_recordCursorPos;
                    break;
                case Keys.R:
                    SetSavedColor(Cursor.Position);
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

        private void SetSavedColor(Point position)
        {
            _savedPosition = position;
            _savedColor = ColorUtils.GetColorAt(position);
            panSavedColor.BackColor = _savedColor;
            panSavedColor.ForeColor = _savedColor;
            txtSavedRgb.Text = $"{_savedColor.R}, {_savedColor.G}, {_savedColor.B}";
            txtSavedX.Text = _savedPosition.X + "";
            txtSavedY.Text = _savedPosition.Y + "";
        }

        private void OnUpateTick(object sender, EventArgs e)
        {

            if (_recordCursorPos)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    _currentPosition = Cursor.Position;
                    UpdateColorUIPixels(_currentPosition);
                    //if (!_previousPosition.IsZero() &&
                    //_previousPosition != _currentPosition && _previousPosition != _savedPosition)
                    //{
                    //    SetSavedColor(_previousPosition);
                    //}

                    _previousPosition = Cursor.Position;
                });
            }
            _updateTimer.Start();
        }

        private void UpdateColorUIPixels(Point position)
        {
            int x = position.X;
            int y = position.Y;
            txtX.Text = x.ToString();
            txtY.Text = y.ToString();
            _currentColor = ColorUtils.GetColorAt(x, y);
            UpdateColorUI(new TextColorInfo() { Color = _currentColor });
        }

        private void UpdateColorUI(TextColorInfo info)
        {
            Color col = info.Color;
            panColor.BackColor = col;
            panColor.ForeColor = col;
            txtRgb.Text = $"{col.R},{col.G},{col.B}";

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            int colDiff = ColorUtils.ColorDiff(_savedColor, GetCursorColor());
            if (colDiff == 0 && Cursor.Position != _savedPosition)
                Debug.Log("SAME COLOR AS SAVED: " + _savedPosition);
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
                        ColorUtils.GetHexColorFromString(txt.Text),
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
            return ColorUtils.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
        }

        private void btnRGB_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtRgb.Text);
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
                UpdateColorUI(new TextColorInfo() { Color = col });
        }

        private void btnPointGroup_Click(object sender, EventArgs e)
        {
            FormGroup frmGroup = new FormGroup();
            frmGroup.Show();
        }

        private void btnTesseract_Click(object sender, EventArgs e)
        {
            FormOCR frmOcr = new FormOCR();
            frmOcr.Show();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FormFindColor frm = new FormFindColor();
            frm.Show();
        }
    }
}
