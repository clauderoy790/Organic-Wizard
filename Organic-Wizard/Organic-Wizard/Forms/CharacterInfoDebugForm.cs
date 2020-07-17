using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using SysTimers = System.Timers;

namespace Organic_Wizard
{
    public partial class CharacterInfoDebugForm : Form
    {
        SysTimers.Timer _updateTimer;
        List<Label> _labels = new List<Label>();
        bool _closing = false;

        public CharacterInfoDebugForm()
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            foreach (var c in this.Controls)
            {
                Label lbl = c as Label;
                if (lbl != null)
                    _labels.Add(lbl);
            }

            _updateTimer = new SysTimers.Timer();
            _updateTimer.SynchronizingObject = this;
            _updateTimer.AutoReset = true;
            _updateTimer.Elapsed += OnUpdateTimer;
            _updateTimer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _closing = true;
            _updateTimer.Dispose();
        }

        private void OnUpdateTimer(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (_closing)
                    return;

                ResetLabelText();
                lblHpPercent.Text = string.Format("Health: {0}%", CharacterInfo.HpPercent != Constants.NONE ? CharacterInfo.HpPercent.ToString() : "INVALID");
                lblMpPercent.Text = string.Format("Mana: {0}%", CharacterInfo.MpPercent != Constants.NONE ? CharacterInfo.MpPercent.ToString() : "INVALID");
                lblIsInParty.Text = string.Format("Is in party: {0}", CharacterInfo.IsInParty);
                lblIsPartyMemberSelected.Text = string.Format("Is pt member selected: {0}", CharacterInfo.IsPartyMemberSelected);
                lblPartySize.Text = string.Format("Party Size: {0}", CharacterInfo.PartySize);
                lblSelectedPartyMemberHp.Text = string.Format("Selected Pt Member HP: {0}%", CharacterInfo.SelectedPartyMemberHpPercent != Constants.NONE ? CharacterInfo.SelectedPartyMemberHpPercent.ToString() : "INVALID");
                lblPosition.Text = string.Format("Position: {0}, {1}", CharacterInfo.Position.X, CharacterInfo.Position.Y);
            });
        }

        void ResetLabelText()
        {
            _labels.ForEach(lbl =>
            {
                lbl.Text = "";
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestTesseract_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Shared/HelloFriends.png");
                if (!File.Exists(path))
                {
                    MessageBox.Show($"Cannot find image to test at path {path}");
                    return;
                }
                ImgUtils.Init();
                var processed = ImgUtils.ProcessImage(path);
                if (!string.IsNullOrEmpty(processed.Text))
                {
                    MessageBox.Show($"Tesserract result: {processed.Text}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tesserract failed: {ex.Message}, stack: {ex.StackTrace}");
            }

        }
    }
}
