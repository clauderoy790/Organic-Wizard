using Organic_Wizard.Logic;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Organic_Wizard.Forms
{
    public partial class FormDebugSkillBar : Form
    {
        Timer timerUpdate;
        List<SkillDebugInfo> skills = new List<SkillDebugInfo>();
        List<int> skillsInt = new List<int>();
        bool initedCursorPos = false;
        bool closed = false;
        bool isInitializing = false;
        bool disposed = false;

        public FormDebugSkillBar()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            timerUpdate.Stop();
            closed = true;
        }

        private void FormDebugSkillBar_Load(object sender, EventArgs e)
        {
            Theme.Changed += UpdateTheme;
            UpdateTheme();
            isInitializing = true;
            skills = new List<SkillDebugInfo>();
            skillsInt = new List<int>();
            initedCursorPos = false;
            timerUpdate = new Timer();
            timerUpdate.Interval = 30;
            timerUpdate.AutoReset = false;
            timerUpdate.Elapsed += OnUpdate;
            timerUpdate.Disposed += OnDisposed;
            closed = false;

            skills = new List<SkillDebugInfo>()
            {
                new SkillDebugInfo()
                {
                    lblSkill = lblSkill1,
                    lblIsOnCd = lblIsOnCd1,
                    LblPercentage =  lblPercentage1,
                    lblCheckPoint = lblCheckPoint1,
                    LblCdColor = lblCdColor1,
                    LblCurColor = lblCurrentColor1,
                    PanCdColor = panCdColor1,
                    PanCurColor = panCurrentColor1
                },

                new SkillDebugInfo()
                {
                    lblSkill = lblSkill2,
                    lblIsOnCd = lblIsOnCd2,
                    LblPercentage =  lblPercentage2,
                    lblCheckPoint = lblCheckPoint2,
                    LblCdColor = lblCdColor2,
                    LblCurColor = lblCurColor2,
                    PanCdColor = panCdColor2,
                    PanCurColor = panCurColor2
                },


                new SkillDebugInfo()
                {
                    lblSkill = lblSkill3,
                    lblIsOnCd = lblIsOnCd3,
                    LblPercentage =  lblPercent3,
                    lblCheckPoint = lblCheckpoint3,
                    LblCdColor = lblCdColor3,
                    LblCurColor = lblCurColor3,
                    PanCdColor = panCdColor3,
                    PanCurColor = panCurColor3
                },

                new SkillDebugInfo()
                {
                    lblSkill = lblSkill4,
                    lblIsOnCd = lblIsOnCd4,
                    LblPercentage =  lblPercent4,
                    lblCheckPoint = lblCheckpoint4,
                    LblCdColor = lblCdColor4,
                    LblCurColor = lblCurColor4,
                    PanCdColor = panCdColor4,
                    PanCurColor = panCurColor5
                }
            };

            SavedData.Init();
            for (int i = 0; i < 4; i++)
            {
                skillsInt.Add(SavedData.GetAttackSkillAtPos(i));
            }

            SavedData.Init();
            bool initialized = false;
            int maxAttemps = 5;
            int attempts = 0;

            while (!initialized && attempts++ < maxAttemps)
            {
                SkillBar.InitSkillsInfo();
                initialized = SkillBar.IsInitialized;
            }
            if (SkillBar.IsInitialized)
            {
                timerUpdate.Start();
                isInitializing = false;
            }
            else
            {
                MessageBox.Show("Failed to init skillbar.");
            }
        }

        private void UpdateTheme()
        {
            this.BackColor = Theme.Current.BackColor;
            btnUpdateSkillBarPos.BackColor = Theme.Current.BackColor;
            btnUpdateSkillBarPos.ForeColor = Theme.Current.ForeColor;

            foreach (var skillUI in skills)
            {
                skillUI.LblCdColor.ForeColor = Theme.Current.ForeColor;
                skillUI.lblCheckPoint.ForeColor = Theme.Current.ForeColor;
                skillUI.LblCurColor.ForeColor = Theme.Current.ForeColor;
                skillUI.lblIsOnCd.ForeColor = Theme.Current.ForeColor;
                skillUI.LblPercentage.ForeColor = Theme.Current.ForeColor;
                skillUI.lblSkill.ForeColor = Theme.Current.ForeColor;
            }

            this.Refresh();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            disposed = true;
            timerUpdate.Stop();
            timerUpdate.Elapsed -= OnUpdate;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            timerUpdate.Elapsed -= OnUpdate;
            timerUpdate.Stop();
            timerUpdate.Dispose();
            FormDebugSkillBar_Load(null, null);
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            timerUpdate.Stop();
            if (closed || isInitializing || disposed)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                if (closed || isInitializing || disposed)
                    return;

                if (!initedCursorPos)
                {
                    initedCursorPos = true;
                    Cursor.Position = SkillBar.SkillBarLocation;
                }
                Color col = ColorUtils.GetColorAt(SkillBar.SkillBarLocation);
                lblSkillBarLocation.Text = $"Skill bar location: {SkillBar.SkillBarLocation}, Color: R:{col.R}, G:{col.G}, B:{col.B}";
                for (int i = 0; i < skillsInt.Count; i++)
                {
                    if (skillsInt[i] != Constants.NONE)
                        skills[i].Display(SkillBar.GetSkillForNumber(skillsInt[i]));
                }
                timerUpdate.Start();
            });
        }

        public class SkillDebugInfo
        {
            public Label lblSkill { get; set; }
            public Label lblIsOnCd { get; set; }
            public Label LblPercentage { get; set; }
            public Label lblCheckPoint { get; set; }
            public Label LblCdColor { get; set; }
            public Label LblCurColor { get; set; }
            public Panel PanCdColor { get; set; }
            public Panel PanCurColor { get; set; }

            public void Display(SkillInfo skill)
            {
                lblCheckPoint.Text = $"Check Point: {skill.CooldownCheckPoint.X}, {skill.CooldownCheckPoint.Y}";
                lblSkill.Text = $"Skill: {skill.Number}";
                lblIsOnCd.Text = $"Is on cooldown: {skill.IsOnCooldown()}";
                LblPercentage.Text = $"Percentage Mp Uesd: {skill.PercentageMpUsed}";
                LblCdColor.Text = $"Cooldown Color: {skill.OffCooldownColor.R}, {skill.OffCooldownColor.G}, {skill.OffCooldownColor.B}";
                Color col = ColorUtils.GetColorAt(skill.CooldownCheckPoint);
                LblCurColor.Text = $"CurrentColor: {col.R}, {col.G}, {col.B}";
                PanCdColor.BackColor = PanCdColor.ForeColor = skill.OffCooldownColor;
                PanCurColor.BackColor = PanCurColor.ForeColor = col;
            }

            public void Clear()
            {
                lblCheckPoint.Text = "";
                lblSkill.Text = "";
                lblIsOnCd.Text = "";
                LblPercentage.Text = "";
                lblCheckPoint.Text = "";
                LblCdColor.Text = "";
                LblCurColor.Text = "";
                PanCdColor.BackColor = PanCdColor.ForeColor = Color.Empty;
                PanCurColor.BackColor = PanCurColor.ForeColor = Color.Empty;
            }
        }
    }
}
