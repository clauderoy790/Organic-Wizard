using NuGet;
using Organic_Wizard.Properties;
using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Organic_Wizard
{
    public partial class MainForm : Form
    {
        private SupportSkillUI[] _suppSkillUi;
        private TextBox[] _txtAttackSkills;
        private TextBox[] _txtRecoverSkills;
        private TrackBarUI _trkRecSkill;
        private TrackBarUI _trkHp;
        private TrackBarUI _trkMp;

        private LogicEngine _logicEngine;
        private bool _loadDone = false;

        public MainForm()
        {
            InitializeComponent();
            SavedData.Init();
            _logicEngine = new LogicEngine();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddVersionNumber();
            Task.Run(async () =>
            {
                await Updater.CheckForUpdates(OnUpdateChecked);
            });
#if DEBUG
            btnDebug.Visible = true;
            btnDebug.Enabled = true;
#endif
            _suppSkillUi = new SupportSkillUI[]
            {
                new SupportSkillUI(txtSuppSkill1,chkParty1),
                new SupportSkillUI(txtSuppSkill2,chkParty2),
                new SupportSkillUI(txtSuppSkill3,chkParty3),
                new SupportSkillUI(txtSuppSkill4,chkParty4),
                new SupportSkillUI(txtSuppSkill5,chkParty5),
                new SupportSkillUI(txtSuppSkill6,chkParty6),
                new SupportSkillUI(txtSuppSkill7,chkParty7),
                new SupportSkillUI(txtSuppSkill8,chkParty8)
            };

            _txtAttackSkills = new TextBox[]
            {
                txtAtkSkill1,
                txtAtkSkill2,
                txtAtkSkill3,
                txtAtkSkill4
            };

            _txtRecoverSkills = new TextBox[]
            {
                txtRecSkill1,
                txtRecSkill2,
                txtRecSkill3,
                txtRecSkill4
            };

            _trkRecSkill = new TrackBarUI(trkRecoverySkillPercent, lblUseRecSkillPercent, btnRecSkillBarPlus, btnRecSkillBarMinus, SaveInfo);
            _trkHp = new TrackBarUI(trkHpRecovery, lblHpRecovery, btnHpRecoveryPlus, btnHpRecoveryMinus, SaveInfo);
            _trkMp = new TrackBarUI(trkMpRecovery, lblMpRecoveryPercent, btnHpRecoveryPlus, btnHpRecoveryMinus, SaveInfo);

            LoadInfo();
            StopOnMinimize.OnMinimize += UpdateBtnStartText;
            _loadDone = true;
        }

        private void OnUpdateChecked(Updater.UpdateStatus updateStatus)
        {
            if (updateStatus.InstalledNewVersion)
            {
                InvokeUI(() =>
                {
                    MessageBox.Show($"Successfully updated to version: { updateStatus.Release.Version}!{Environment.NewLine}You may restart the app to get the newest features.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            else if (updateStatus.Error != null)
            {
                InvokeUI(() =>
                {
                    MessageBox.Show($"An error occurred while trying to update, try again later.{Environment.NewLine}Error: " + updateStatus.Error.Message, "Uh oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });
            }
        }

        private void AddVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            this.Text += $" - {assembly.GetName().Version.ToString(3)}";
        }

        private void KeyPress_IntOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }


            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void SaveInfo()
        {
            SavedData.AttackMode = chkAtkMode.Checked;
            SavedData.RecoveryMode = chkRecMode.Checked;
            SavedData.HpRecovery = chkHpRecovery.Checked;
            SavedData.MpRecovery = chkMpRecovery.Checked;
            SavedData.SupportSkills = chkUseSupportSkills.Checked;

            SavedData.HpRecoverySkill = GetTextboxSkillNumber(txtHpRecoverySkill);
            SavedData.MpRecoverySkill = GetTextboxSkillNumber(txtMpRecoverySkill);

            for (int i = 0; i < _txtAttackSkills.Length; i++)
            {
                SavedData.SetAttackSkillAtPos(i, GetTextboxSkillNumber(_txtAttackSkills[i]));
            }

            for (int i = 0; i < _txtRecoverSkills.Length; i++)
            {
                SavedData.SetRecoverySkillAtPos(i, GetTextboxSkillNumber(_txtRecoverSkills[i]));
            }

            SavedData.RecoverySkillPecent = _trkRecSkill.Percent;
            SavedData.HpRecoveryPercent = _trkHp.Percent;
            SavedData.MpRecoveryPercent = _trkMp.Percent;
            SavedData.Save();
        }

        private void LoadInfo()
        {
            chkAtkMode.Checked = SavedData.AttackMode;
            chkRecMode.Checked = SavedData.RecoveryMode;
            chkHpRecovery.Checked = SavedData.HpRecovery;
            chkMpRecovery.Checked = SavedData.MpRecovery;
            chkUseSupportSkills.Checked = SavedData.SupportSkills;

            SetTextboxValue(txtHpRecoverySkill, SavedData.HpRecoverySkill);
            SetTextboxValue(txtMpRecoverySkill, SavedData.MpRecoverySkill);

            for (int i = 0; i < _txtAttackSkills.Length; i++)
            {
                SetTextboxValue(_txtAttackSkills[i], SavedData.GetAttackSkillAtPos(i));
            }

            for (int i = 0; i < _txtRecoverSkills.Length; i++)
            {
                SetTextboxValue(_txtRecoverSkills[i], SavedData.GetRecoverySkillAtPos(i));
            }

            _trkRecSkill.SetPercent(SavedData.RecoverySkillPecent);
            _trkHp.SetPercent(SavedData.HpRecoveryPercent);
            _trkMp.SetPercent(SavedData.MpRecoveryPercent);
        }

        private void SetTextboxValue(TextBox txt, int val)
        {
            if (val != SavedData.NONE)
            {
                txt.Text = val.ToString();
            }
            else
            {
                txt.Text = string.Empty;
            }
        }

        private int GetTextboxSkillNumber(TextBox txt)
        {
            int nb = SavedData.NONE;
            if (!string.IsNullOrEmpty(txt.Text) && int.TryParse(txt.Text, out nb))
            {
                if (nb < 0 && nb >= 10)
                {
                    nb = SavedData.NONE;
                }
            }

            return nb;
        }

        public class SupportSkillUI
        {
            public SupportSkillUI(TextBox txt, CheckBox chk)
            {
                TxtBox = txt;
                ChkBox = chk;
            }
            public TextBox TxtBox { get; set; }
            public CheckBox ChkBox { get; set; }
        }

        private void InfoChanged(object sender, EventArgs e)
        {
            if (_loadDone)
                SaveInfo();
        }

        private void UpdateBtnStartText()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                btnStartStop.Text = btnStartStop.Text == "Start" ? "Stop" : "Start";

            });
        }

        private void StartStopClick(object sender, EventArgs e)
        {
            UpdateBtnStartText();
            _logicEngine.ToggleStartStop();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            CharacterInfoDebugForm debug = new CharacterInfoDebugForm();
            debug.Show();
        }

        private void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }
    }
}
