using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorTool
{
    public partial class FormColorCompare : Form
    {
        ColorFields _colFields1;
        ColorFields _colFields2;

        public FormColorCompare()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _colFields1 = new ColorFields()
            {
                TxtSearch = txtSearchCol1,
                TxtHex = txtHexColor,
                TxtInt = txtIntColor,
                TxtRgb = txtRGB,
                PanColor = panColor
            };

            _colFields2 = new ColorFields()
            {
                TxtSearch = txtSearchCol2,
                TxtHex = txtHex2,
                TxtInt = txtInt2,
                TxtRgb = txtRGB2,
                PanColor = panColor2
            };
        }

        private void btnPick1_Click(object sender, EventArgs e)
        {
            ProcessColorFields(_colFields1);
        }

        private void btnPick2_Click(object sender, EventArgs e)
        {
            ProcessColorFields(_colFields2);
        }

        private void ProcessColorFields(ColorFields fields)
        {
            var result = colDiag.ShowDialog();
            if (result == DialogResult.OK)
            {
                fields.SetColor(colDiag.Color);
            }
            UpdateColorDiff();
        }

        private void txtSearch1Changed(object sender, EventArgs e)
        {
            UpdateSearch(txtSearchCol1,_colFields1);
        }

        private void txtSearch2Changed(object sender, EventArgs e)
        {
            UpdateSearch(txtSearchCol2, _colFields2);
        }

        void UpdateSearch(TextBox txt,ColorFields fields)
        {
            Color col = FormColorMain.GetResearchFieldColor(txt);
            if (col != Color.Empty)
                fields.SetColor(col);
            UpdateColorDiff();
        }

        private void UpdateColorDiff()
        {
            int result = CUtils.ColorDiff(_colFields1.Color, _colFields2.Color);
            lblResult.Text = string.Format("Color Difference: {0}", result);
        }

        class ColorFields
        {
            Color _color = Color.Empty;

            public Color Color
            {
                get
                {
                    return _color;
                }

                set
                {
                    SetColor(value);
                }
            }
            public TextBox TxtSearch { get; set; }
            public TextBox TxtHex { get; set; }
            public TextBox TxtInt { get; set; }
            public TextBox TxtRgb { get; set; }
            public Panel PanColor { get; set; }

            public void SetColor(Color col)
            {
                _color = col;
                PanColor.ForeColor = col;
                PanColor.BackColor = col;
                TxtHex.Text = CUtils.GetHexColor(col);
                TxtInt.Text = CUtils.GetColorAsInt(col) + "";
                TxtRgb.Text = string.Format("R:{0}, G:{1}, B:{2}", col.R, col.G, col.B);
            }

            public void SetResearchFieldColor()
            {
                Color col = FormColorMain.GetResearchFieldColor(TxtSearch);
                if (col != Color.Empty)
                    SetColor(col);
            }
        } 
    }
}
