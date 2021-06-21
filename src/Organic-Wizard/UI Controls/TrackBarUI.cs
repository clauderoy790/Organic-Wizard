using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    public class TrackBarUI
    {
        private const int BTN_INCREMENT = 1;

        private int _percent = 0;
        private Button _btnMinus;
        private Button _btnPlus;
        private Action _saveAction;


        public TrackBarUI(TrackBar trk, Label lbl, Button btnPlus, Button btnMinus, Action saveAction)
        {
            TrkBar = trk;
            this.Lbl = lbl;
            _btnMinus = btnMinus;
            _btnPlus = btnPlus;

            TrkBar.ValueChanged += OnTrkValueChanged;
            _btnPlus.Click += OnPlusClick;
            _btnMinus.Click += OnMinusClick;
            _saveAction = saveAction;

        }

        private void OnTrkValueChanged(object sender, EventArgs e)
        {
            SetPercent(TrkBar.Value);
            if (_saveAction != null)
                _saveAction();
        }

        public TrackBar TrkBar { get; set; }
        public Label Lbl { get; set; }
        public int Percent { get { return _percent; } }

        public void SetPercent(int percent)
        {
            _percent = GetValidPercentage(percent);
            TrkBar.Value = _percent;
            Lbl.Text = TrkBar.Value + " %";
        }

        int GetValidPercentage(int val)
        {
            val = Math.Min(val, 100);
            val = Math.Max(val, 0);
            return val;
        }

        private void OnMinusClick(object sender, EventArgs e)
        {
            SetPercent(_percent - BTN_INCREMENT);
        }

        private void OnPlusClick(object sender, EventArgs e)
        {
            SetPercent(_percent + BTN_INCREMENT);
        }
    }
}
