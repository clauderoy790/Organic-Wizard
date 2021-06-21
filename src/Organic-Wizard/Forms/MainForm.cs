
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
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

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
        private List<Control> _controls;
        private SolidBrush backBrush;
        private SolidBrush foreBrush;
        private Pen backPen;

        private Timer _timerRuntime;
        private LogicEngine _logicEngine;
        private bool _loadDone = false;
        private bool _closed = false;
        private int _runSeconds = 0;
        CharacterDebugForm _frmDebug = null;

        public bool FromChild = false;


        public MainForm()
        {
            InitializeComponent();
            SavedData.Init();
            _logicEngine = new LogicEngine();
            tabControl1.Refresh();
            this.Visible = false;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (FromChild)
            {
                return;
            }
#if DEBUG

            if (_frmDebug.Disposing || _frmDebug.IsDisposed)
                return;

            _frmDebug.FromMain = true;
            _frmDebug.Show();
            if (_frmDebug.WindowState == FormWindowState.Minimized)
            {
                _frmDebug.WindowState = FormWindowState.Normal;
            }
            _frmDebug.FromMain = false;
#endif
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _closed = true;
            _logicEngine.Stop();
            LogicEngine.Stopped -= StopClicked;
            LogicEngine.Started -= StartClicked;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Theme.Changed += UpdateTheme;
            backPen = new Pen(Theme.Current.BackColor);
            backBrush = new SolidBrush(Theme.Current.BackColor);
            foreBrush = new SolidBrush(Theme.Current.ForeColor);
            _frmDebug = new CharacterDebugForm(this);
            PreventResize();
            LogicEngine.Stopped += StopClicked;
            LogicEngine.Started += StartClicked;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += DrawTabItem;

            _runSeconds = 0;
            _timerRuntime = new Timer(1000);
            _timerRuntime.AutoReset = false;
            _timerRuntime.Elapsed += OnRuntimeUpdate;

            AddVersionNumber();
#if !DEBUG
            Task.Run(async () =>
            {
                await Updater.CheckForUpdates(OnUpdateChecked);
            });
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

            _controls = new List<Control>()
            {
                txtSuppSkill1,
                txtSuppSkill2,
                txtSuppSkill3,
                txtSuppSkill4,
                txtSuppSkill5,
                txtSuppSkill6,
                txtSuppSkill7,
                txtSuppSkill8,
                chkParty1,
                chkParty2,
                chkParty3,
                chkParty4,
                chkParty5,
                chkParty6,
                chkParty7,
                chkParty8,
                txtAtkSkill1,
                txtAtkSkill2,
                txtAtkSkill3,
                txtAtkSkill4,
                txtRecSkill1,
                txtRecSkill2,
                txtRecSkill3,
                txtRecSkill4
            };

            _trkRecSkill = new TrackBarUI(trkRecoverySkillPercent, lblUseRecSkillPercent, btnRecSkillBarPlus, btnRecSkillBarMinus, SaveInfo);
            _trkHp = new TrackBarUI(trkHpRecovery, lblHpRecovery, btnHpRecoveryPlus, btnHpRecoveryMinus, SaveInfo);
            _trkMp = new TrackBarUI(trkMpRecovery, lblMpRecoveryPercent, btnMpRecoveryPlus, btnMpRecoveryMinus, SaveInfo);

            LoadInfo();
            _loadDone = true;
            UpdateTheme();
            this.Visible = true;


#if DEBUG
            _frmDebug.Show();
#endif
        }

        private void UpdateTheme()
        {
            this.BackColor = Theme.Current.BackColor;
            SetControlsThemeColor(this.Controls);

            var tps = new TabPage[] { tpAttack, tpOther, tpRecover, tpSettings, tpSupport };
            foreach (var tp in tps)
            {
                tp.ForeColor = Theme.Current.ForeColor;
                tp.BackColor = Theme.Current.BackColor;
                SetControlsThemeColor(tp.Controls);
                tp.Refresh();
            }

            this.Refresh();
        }

        void SetControlsThemeColor(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label || control is Button || control is TextBox || control is CheckBox)
                {
                    if (control is TextBox)
                    {
                        control.BackColor = Color.White;
                        control.ForeColor = Color.Black;
                    }
                    else
                    {
                        control.BackColor = Theme.Current.BackColor;
                        control.ForeColor = Theme.Current.ForeColor;
                    }
                }
            }
        }


        private void DrawTabItem(object sender, DrawItemEventArgs e)
        {
            if (backBrush.Color != Theme.Current.BackColor)
            {
                backPen = new Pen(Theme.Current.BackColor);
                backBrush = new SolidBrush(Theme.Current.BackColor);
                foreBrush = new SolidBrush(Theme.Current.ForeColor);
            }

            using (Brush br = new SolidBrush(Theme.Current.BackColor))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, foreBrush, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(backPen, rect);
                e.DrawFocusRectangle();

                if (tabControl1.TabPages.Count == 0)
                    return;

                int offset = 4;
                Rectangle lasttabrect = tabControl1.GetTabRect(tabControl1.TabPages.Count - 1);
                RectangleF emptyspacerect = new RectangleF(
                        lasttabrect.X + lasttabrect.Width,
                         lasttabrect.Y - 3,
                        tabControl1.Width - (lasttabrect.X + lasttabrect.Width) - offset,
                        lasttabrect.Height * 2);

                e.Graphics.FillRectangle(backBrush, emptyspacerect);
            }
        }

        private void PaintTab(object sender, PaintEventArgs e)
        {
            SolidBrush fillBrush = new SolidBrush(BackColor);

            e.Graphics.FillRectangle(fillBrush, e.ClipRectangle);
        }

        private void OnRuntimeUpdate(object sender, ElapsedEventArgs e)
        {
            if (_closed)
                return;

            _timerRuntime.Stop();
            _runSeconds++;
            int MINUTE = 60;
            int HOUR = 60 * MINUTE;
            int hours = _runSeconds / HOUR;

            string runtimeText = "Runtime: ";
            if (hours > 0)
            {
                int minutes = _runSeconds % HOUR;
                runtimeText += $"{hours}h";
                if (minutes > 0)
                {
                    runtimeText += $" {minutes}m";
                }
            }
            else
            {
                int minutes = _runSeconds / MINUTE;
                if (minutes > 0)
                {
                    runtimeText += $" {minutes}m ";
                }

                int seconds = _runSeconds % MINUTE;
                if (seconds > 0)
                {
                    runtimeText += $" {seconds}s";
                }
            }

            InvokeUI(() =>
            {
                lblRunTime.Text = runtimeText;
                _timerRuntime.Start();
            });
        }

        private void PreventResize()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void StopClicked()
        {
            if (_closed)
                return;

            InvokeUI(() =>
            {
                _timerRuntime.Stop();
                this.lblRunTime.Visible = false;
                this.btnStartStop.Enabled = true;
                this.btnStartStop.Text = "Start";
            });
        }

        private void StartClicked()
        {
            if (_closed)
                return;

            InvokeUI(() =>
            {
                _runSeconds = 0;
                _timerRuntime.Start();
                _frmDebug.ClearConsole();
                this.lblRunTime.Visible = true;
                this.btnStartStop.Enabled = true;
                this.btnStartStop.Text = "Stop";
            });
        }

        private void OnUpdateChecked(Updater.UpdateStatus updateStatus)
        {
            if (_closed)
                return;

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
            if (!_loadDone)
                return;

            int maxTravelDistance = 0;
            SavedData.AttackMode = chkAtkMode.Checked;
            SavedData.RecoveryMode = chkRecMode.Checked;
            SavedData.HpRecovery = chkHpRecovery.Checked;
            SavedData.MpRecovery = chkMpRecovery.Checked;
            SavedData.UseBuffSkills = chkUseSupportSkills.Checked;
            SavedData.Loot = chkLoot.Checked;
            SavedData.DarkMode = chkDarkMode.Checked;

            if (int.TryParse(txtMaxTravelDistance.Text, out maxTravelDistance))
            {
                SavedData.MaxTravelRange = maxTravelDistance;
            }
            else
            {
                SavedData.MaxTravelRange = Constants.NONE;
            }

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

            for (int i = 0; i < _suppSkillUi.Length; i++)
            {
                SavedData.SetBuffSkillAtPos(i, GetTextboxSkillNumber(_suppSkillUi[i].TxtBox), _suppSkillUi[i].ChkBox.Checked);
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
            chkUseSupportSkills.Checked = SavedData.UseBuffSkills;
            chkLoot.Checked = SavedData.Loot;
            chkDarkMode.Checked = SavedData.DarkMode;

            txtMaxTravelDistance.Text = "";
            if (SavedData.MaxTravelRange != Constants.NONE)
                txtMaxTravelDistance.Text = SavedData.MaxTravelRange.ToString();

            SetTextboxValue(txtHpRecoverySkill, SavedData.HpRecoverySkill);
            SetTextboxValue(txtMpRecoverySkill, SavedData.MpRecoverySkill);

            for (int i = 0; i < _txtAttackSkills.Length; i++)
            {
                SetTextboxValue(_txtAttackSkills[i], SavedData.GetAttackSkillAtPos(i));
            }

            for (int i = 0; i < _txtRecoverSkills.Length; i++)
            {
                SetTextboxValue(_txtRecoverSkills[i], SavedData.GetHealSkillAtPos(i));
            }

            _trkRecSkill.SetPercent(SavedData.RecoverySkillPecent);
            _trkHp.SetPercent(SavedData.HpRecoveryPercent);
            _trkMp.SetPercent(SavedData.MpRecoveryPercent);
            UpdateTextWithSavedData();
        }

        private void SetTextboxValue(TextBox txt, int val)
        {
            if (val != Constants.NONE)
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
            int nb = Constants.NONE;
            if (!string.IsNullOrEmpty(txt.Text) && int.TryParse(txt.Text, out nb))
            {
                if (nb < 0 && nb >= 10)
                {
                    nb = Constants.NONE;
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
            {
                SaveInfo();
                RemoveInfoChangedEvent();
                UpdateTextWithSavedData();
                AddInfoChangedEvent();
            }
        }

        private void UpdateTextWithSavedData()
        {
            chkAtkMode.Checked = SavedData.AttackMode;
            chkRecMode.Checked = SavedData.RecoveryMode;
            chkHpRecovery.Checked = SavedData.HpRecovery;
            chkMpRecovery.Checked = SavedData.MpRecovery;
            chkUseSupportSkills.Checked = SavedData.UseBuffSkills;
            chkLoot.Checked = SavedData.Loot;
            chkDarkMode.Checked = SavedData.DarkMode;

            SetTextboxValue(txtHpRecoverySkill, SavedData.HpRecoverySkill);
            SetTextboxValue(txtMpRecoverySkill, SavedData.MpRecoverySkill);

            var buffSkills = SavedData.GetBuffSkills();
            for (int i = 0; i < buffSkills.Count; i++)
            {
                var buff = buffSkills[i];
                if (!buff.IsValid)
                    continue;
                var supp = _suppSkillUi[i];
                supp.ChkBox.Checked = buff.UseOnParty;
                supp.TxtBox.Text = buff.Skill.ToString();
            }

            for (int i = 0; i < _txtAttackSkills.Length; i++)
            {
                SetTextboxValue(_txtAttackSkills[i], SavedData.GetAttackSkillAtPos(i));
            }

            for (int i = 0; i < _txtRecoverSkills.Length; i++)
            {
                SetTextboxValue(_txtRecoverSkills[i], SavedData.GetHealSkillAtPos(i));
            }

            _trkRecSkill.SetPercent(SavedData.RecoverySkillPecent);
            _trkHp.SetPercent(SavedData.HpRecoveryPercent);
            _trkMp.SetPercent(SavedData.MpRecoveryPercent);
        }

        private void RemoveInfoChangedEvent()
        {
            foreach (var control in _controls)
            {
                TextBox txt = control as TextBox;
                if (txt != null)
                {
                    txt.TextChanged -= InfoChanged;
                }
                CheckBox chk = control as CheckBox;
                if (chk != null)
                {
                    chk.CheckedChanged -= InfoChanged;
                }
            }
        }

        private void AddInfoChangedEvent()
        {
            foreach (var control in _controls)
            {
                TextBox txt = control as TextBox;
                if (txt != null)
                {
                    txt.TextChanged -= InfoChanged;
                    txt.TextChanged += InfoChanged;
                }
                CheckBox chk = control as CheckBox;
                if (chk != null)
                {
                    chk.CheckedChanged -= InfoChanged;
                    chk.CheckedChanged += InfoChanged;
                }
            }
        }

        private void StartStopClick(object sender, EventArgs e)
        {
            this.btnStartStop.Enabled = false;
            _logicEngine.ToggleStartStop();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            if (_frmDebug.IsDisposed)
            {
                _frmDebug = new CharacterDebugForm(this);
            }
            _frmDebug.Show();
        }

        private void InvokeUI(Action a)
        {
            if (_closed)
                return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!_closed)
                {
                    a();
                }
            }));
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (!_frmDebug.IsDisposed)
            {
                _frmDebug.Close();
            }
        }

        private void DarkModeCheckChanged(object sender, EventArgs e)
        {
            if (chkDarkMode.Checked)
                Theme.SetTheme(Theme.ETheme.Dark);
            else
                Theme.SetTheme(Theme.ETheme.Normal);
            InfoChanged(sender, e);
        }
    }
}
