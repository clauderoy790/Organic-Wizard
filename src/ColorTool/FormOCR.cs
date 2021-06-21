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
using System.Windows;

namespace ColorTool
{
    public partial class FormOCR : Form
    {
        TextBox txtRecordingCursorPos = null;
        System.Timers.Timer timer = new System.Timers.Timer();
        KListener _keyListener;

        public FormOCR()
        {
            InitializeComponent();
            timer.Elapsed += OnUpdateTick;
            timer.Interval = 20;
            timer.Start();

            _keyListener = new KListener();
            _keyListener.KeyDown += OnKKDown;
        }

        private void OnKKDown(object sender, RawKeyEventArgs args)
        {
            switch (args.Key)
            {
                case Keys.Escape:
                    txtRecordingCursorPos = null;
                    break;
            }
        }

        private void OnUpdateTick(object sender, ElapsedEventArgs e)
        {
            if (txtRecordingCursorPos!= null)
            {
                this.Invoke((MethodInvoker)delegate {
                    int x = Cursor.Position.X;
                    int y = Cursor.Position.Y;
                    if (txtRecordingCursorPos  != null)
                        txtRecordingCursorPos.Text = $"{x}, {y}";
                });
            }
        }

        private void btnSetTopLeft_Click(object sender, EventArgs e)
        {
            txtRecordingCursorPos = txtTopLeft;
        }

        private void btnBottomRight_Click(object sender, EventArgs e)
        {
            txtRecordingCursorPos = txtBottomRight;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            float zoom = GetZoom();
            Vector topLeft = GetTopLeft();
            Vector botRight = GetBotRight();
            var info = ImgUtils.ProcessImage(topLeft.X, topLeft.Y, botRight.X, botRight.Y, zoom);
            if (info.Ex == null)
            {
                txtOutput.Text = info.Text;
            }
            else
            {
                txtOutput.Text = $"Ex: {info.Ex.Message}"+ Environment.NewLine;
                txtOutput.Text += $"{info.Ex.StackTrace}" + Environment.NewLine;
            }
        }

        private Vector GetBotRight()
        {
            return GetPointForTextBox(txtBottomRight);
        }



        private Vector GetTopLeft()
        {
            return GetPointForTextBox(txtTopLeft);
        }

        private float GetZoom()
        {
            float zoom = 1;

            try
            {
                zoom = float.Parse(txtZoom.Text);
            }
            catch 
            { }

            return zoom;
        }

        private Vector GetPointForTextBox(TextBox txt)
        {
            Vector point = new Vector();
            try
            {
                var split = txt.Text.Split(',');
                int x = int.Parse(split[0].Trim());
                int y = int.Parse(split[1].Trim());
                point = new Vector(x, y);
            }
            catch
            { }

            return point;
        }
    }
}
