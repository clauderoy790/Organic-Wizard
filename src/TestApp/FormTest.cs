using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Organic_Wizard;
using Organic_Wizard.Logic;
using Shared;
using Shared.Image_Processing;
using Timer = System.Timers.Timer;
using WindowsInput;
using Vector = System.Windows.Vector;
using Debug = Shared.Debug;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;

namespace TestApp
{
    public partial class FormTest : Form
    {
        Timer _timer = null;
        Timer _timerTest = null;
        CTimer _timerUpdate = null;
        bool _closing = false;

        public FormTest()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnElapsed;
            _timer.AutoReset = false;

            _timerTest = new Timer();
            _timerTest.Interval = 3000;
            _timerTest.AutoReset = false;

            _timerUpdate = new CTimer();
            _timerUpdate.Elapsed += OnUpdate;
            _timerUpdate.Interval = 10;
            timer = new CTimer();
        }

        private bool GetPositionForText(string text, out Vector output)
        {
            bool valid = false;
            output = new Vector(0, 0);
            if (!string.IsNullOrEmpty(text))
            {
                string[] splitStrings = text.Trim().Split(',');
                if (splitStrings.Length == 1)
                    splitStrings = text.Split(' ');

                if (splitStrings.Length == 2)
                {
                    int x, y = 0;
                    valid = StringFormatter.TryConvertToInt(splitStrings[0], out x);
                    valid = valid && StringFormatter.TryConvertToInt(splitStrings[1], out y);

                    if (valid)
                    {
                        output = new Vector(x, y);
                    }
                }
            }

            return valid;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Debug.SetDebugAction((s) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtConsole.AppendText(s + Environment.NewLine);
                });
            });
            KeyUtils.WindowToSendKeysIn = Constants.KO_WINDOW;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _closing = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double sim = StringUtils.Similarity(txt2.Text, txt1.Text);
            //Console.WriteLine($"Similarity: {sim}");
            _timerTest.Start();
            _timer.Stop();
            _timer.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                //ReadDcMessage();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Errorr: " + ex.Message);
            }
            watch.Stop();
            Debug.Log($"took {watch.ElapsedMilliseconds}");
        }

        private void ReadDcMessage()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var info = ImgUtils.ProcessImage(360, 300, 530, 320, 2f);
            if (info != null && info.Ex == null && !string.IsNullOrEmpty(info.Text))
            {
                watch.Stop();
                Debug.Log($"Text: {info.Text} in {watch.ElapsedMilliseconds} ms");
            }
            else
            {
                Debug.Log("Error: " + info.Ex);
            }
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            FormNavigation nav = new FormNavigation();
            nav.Show();
        }

        Vector mousePos = new Vector(0, 0);
        bool valid = false;
        int max = 100;
        int cur = 0;
        CTimer timer;
        private void btnTest_Click(object sender, EventArgs e)
        {

            txtConsole.Clear();
            mousePos = ScreenPosition.Center;
            //_timerUpdate.Restart();
            //_timerUpdate.IntervalSeconds = 0.5;
            LogicEngine engine = new LogicEngine();
            engine.Stop();
            engine.Start();
            //PerformClick();
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            if (_closing)
                return;

            //DisplayCooldowns();
            KeyUtils.WindowToSendKeysIn = WinUtils.GetActiveWindow();


            this.Invoke((MethodInvoker)delegate
            {
                if (_closing)
                    return;

                lblIsDown.Text = $"elapsed s: {timer.ElapsedSeconds}, interval: {timer.Interval}";
                lblValue.Text = $"elapsed ms: {timer.ElapsedMs}";
                lblEnabled.Text = $"Enabled: {timer.Enabled}";
            });
            mousePos = Maths.Translate(mousePos, new Vector(1, 1),5).ToIntVector();
            _timerUpdate.Start();
        }
        
        void ClickAtMousePose()
        {
            cur = 0;
            PerformClick();
        }

        private void PerformClick()
        {
            if (cur++ < max && !valid)
            {
                mousePos = new Vector(mousePos.X, mousePos.Y - 1);
                ActionManager.SendAction(() => { MouseOperations.Click(mousePos); }, 0.05f, PerformClick);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            txt1.Focus();
            KeyUtils.SendCharUp('r');
            //sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_R);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            txt1.Focus();
            KeyUtils.SendCharDown('r');
            //KeyUtils.Send(Keyboard.ScanCodeShort.KEY_R);
            //sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_R);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }
    }
}
