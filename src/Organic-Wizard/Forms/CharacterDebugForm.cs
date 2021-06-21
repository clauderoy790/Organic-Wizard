using Organic_Wizard.Forms;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using SysTimers = System.Timers;

namespace Organic_Wizard
{
    public partial class CharacterDebugForm : Form
    {
        List<Label> _labels = new List<Label>();
        bool _closing = false;
        System.Timers.Timer _timer = new SysTimers.Timer();
        MainForm _parent;

        public bool FromMain { get; set; }

        public CharacterDebugForm(MainForm parent)
        {
            _parent = parent;
            InitializeComponent();
            Debug.SetDebugAction(OnDebugLog);
        }

        public void ClearConsole()
        {
            txtConsole.Clear();
        }

        private void OnDebugLog(string str)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtConsole.Multiline = true;
                txtConsole.AppendText($"{str}{Environment.NewLine}");
                txtConsole.SelectionStart = txtConsole.Text.Length;
                txtConsole.ScrollToCaret();
            });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetPosition();
            UpdateTheme();
            _timer.Interval = 40;
            _timer.SynchronizingObject = this;
            _timer.Elapsed += OnUpdate;
            _timer.Start();
            foreach (var c in this.Controls)
            {
                Label lbl = c as Label;
                if (lbl != null)
                    _labels.Add(lbl);
            }
            PreventResize();
            RegisterEvents();
            UpdaCharacterInfo();
            Theme.Changed += UpdateTheme;
        }

        private void UpdateTheme()
        {
            this.BackColor = Theme.Current.BackColor;

            foreach (Control control in Controls)
            {
                if (control is Label || control is Button || control is RichTextBox)
                {
                    control.BackColor = Theme.Current.BackColor;
                    control.ForeColor = Theme.Current.ForeColor;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void PreventResize()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            if (this.IsDisposed)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                SetPosition();
                lblCurrentWindow.Text = $"Current Window: {WinUtils.GetActiveWindow()}";
            });
        }

        private void SetPosition()
        {
            if (_parent != null)
                this.Location = new Point(_parent.Location.X + _parent.Width, _parent.Location.Y);
        }

        private void RegisterEvents()
        {
            LogicEngine.Started += UpdaCharacterInfo;
            Character.PartyMemberSelectedChanged += (s) => { UpdaCharacterInfo(); };
            Character.StateChanged += (s) => { UpdaCharacterInfo(); };
            Character.HpChanged += (s) => { UpdaCharacterInfo(); };
            Character.IsInPartyChanged += (s) => { UpdaCharacterInfo(); };
            Character.MpChanged += (s) => { UpdaCharacterInfo(); };
            Character.PartyMemberHpChanged += (s) => { UpdaCharacterInfo(); };
            Character.PartySizeChanged += (s) => { UpdaCharacterInfo(); };
            Character.PositionChanged += (s) => { UpdaCharacterInfo(); };
            Character.MonsterSelectedChanged += (s) => { UpdaCharacterInfo(); };
            Character.SelectedMonsterHpChanged += (s) => { UpdaCharacterInfo(); };
            Character.SelectedPartyMemberIndexChanged += (s) => { UpdaCharacterInfo(); };
        }

        private void UpdaCharacterInfo()
        {
            if (_closing)
                return;
            this.Invoke((MethodInvoker)delegate
            {
                ResetLabelText();
                lblHpPercent.Text = string.Format("Health: {0}%", Character.HpPercent != Constants.NONE ? Character.HpPercent.ToString() : "INVALID");
                lblMpPercent.Text = string.Format("Mana: {0}%", Character.MpPercent != Constants.NONE ? Character.MpPercent.ToString() : "INVALID");
                lblIsInParty.Text = string.Format("Is in party: {0}", Character.IsInParty);
                lblIsPartyMemberSelected.Text = string.Format("Is pt member selected: {0}", Character.IsPartyMemberSelected);
                lblPartySize.Text = string.Format("Party Size: {0}", Character.PartySize);
                lblSelectedPartyMemberHp.Text = string.Format("Selected Pt Member HP: {0}%", Character.PtMemberHpPercent != Constants.NONE ? Character.PtMemberHpPercent.ToString() : "INVALID");
                lblPosition.Text = string.Format("Position: {0}, {1}", Character.Position.X, Character.Position.Y);
                lblDirection.Text = $"Direction: {Character.Direction.X.ToString("0.##")}, {Character.Direction.Y.ToString("0.##")}";
                lblInitialPosition.Text = string.Format("Init Position: {0}, {1}", Character.InitialPosition.X, Character.InitialPosition.Y);
                lblState.Text = $"State: {Character.State.ToString()}";
                lblKingDisplay.Text = $"King Displayed: {Character.IsKingDisplayed}";
                lblIsMonterSelected.Text = $"Monster Selected: {Character.IsMonsterSelected}";
                lblSelectedMonsterHp.Text = $"Selected Monster Hp: {Character.SelectedMonsterHpPercent}%";
                lblSelectedPartyMemberIndex.Text = $"Selected Party Member  Index: {Character.SelectedPartyMemberIndex}";
            });

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _closing = true;
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

        private void CharacterInfoDebugForm_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }

        private void btnSkillBar_Click(object sender, EventArgs e)
        {
            var frm = new FormDebugSkillBar();
            frm.Show();
        }
    }
}
