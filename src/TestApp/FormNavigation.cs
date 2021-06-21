using Shared;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.IO;
using Organic_Wizard;
using System.Windows;
using Point = System.Drawing.Point;

namespace TestApp
{
    public partial class FormNavigation : Form
    {
        const int minInitialUpDirection = 3;
        const float lengthDiffForReachingDestination = 2f;
        const float minAngleBeforeAjdust = 4f;
        const float minSameDirectionLengthBeforeChangeOrientation = 4;
        const float distanceBeforeAlwaysAdjust = 2;
        Dictionary<TextBox, Point> directions;
        Action _initAction;
        CTimer timerUpdate;
        CTimer timerInit;
        CTimer timerCursorPosition;
        bool processing = false;
        bool isMoving = false;
        Vector currentPos;
        Vector initialPos;
        Vector upDirection;
        Vector destination;
        Vector currentDirectionStartPoint;
        Vector screenCenter;
        Vector screenUpPosition;
        Point screenPoint;
        double distanceToDestination = 0;
        double lastAngle = 0;

        public FormNavigation()
        {
            InitializeComponent();
            initialPos = new Vector(0, 0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            var res = new Vector(0, 1).Rotate(90);

            ImgUtils.Init();
            KeyUtils.WindowToSendKeysIn = Constants.KO_WINDOW;
            directions = new Dictionary<TextBox, Point>()
            {
                { txtUp,new Point(0, 0) },
                { txtDown,new Point(0, 0) },
                { txtRight,new Point(0, 0) },
                { txtLeft,new Point(0, 0) },
            };
            LoadData();

            timerCursorPosition = new CTimer();
            timerCursorPosition.Interval = 10;
            timerCursorPosition.Elapsed += OnCursorTimer;
            timerCursorPosition.Start();

            timerUpdate = new CTimer();
            timerUpdate.Interval = 300;
            timerUpdate.Elapsed += OnUpdate;

            timerInit = new CTimer();
            timerInit.Interval = 1000;
            timerInit.AutoReset = false;
            timerInit.Elapsed += OnInitDone;

            var keys = new List<TextBox>(directions.Keys);
            foreach (var key in keys)
            {
                var point = GetPointForText(key.Text);
                if (point.X != 0 && point.Y != 0)
                    directions[key] = point;
                key.TextChanged += OnDirTextChanged;
            }
        }

        private void OnCursorTimer(object sender, ElapsedEventArgs e)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                txtCursorPos.Text = $"{Cursor.Position.X}, {Cursor.Position.Y}";
            });
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            timerUpdate.Stop();
            string activeWindow = WinUtils.GetActiveWindow();
            if (activeWindow == null || (activeWindow != Constants.KO_WINDOW && !activeWindow.Contains("Microsoft Visual Studio")))
            {
                btnStop_Click(null, null);
                return;
            }

            if (upDirection.IsZero())
                TrySetUpDir();

            SetCurrentPosition();

            distanceToDestination = Math.Abs((destination - currentPos).Length);
            if (distanceToDestination <= lengthDiffForReachingDestination)
            {
                Console.WriteLine("DONE!!!!");
                btnStop_Click(null, null);
                return;
            }
            else if (isMoving && ShouldReajustPosition())
            {
                isMoving = false;
                KeyUtils.SendCharUp('w');
                currentDirectionStartPoint = currentPos;
                StartMove();
            }
            timerUpdate.Start();
        }

        private bool ShouldReajustPosition()
        {
            if (upDirection.IsZero())
                return false;

            Vector directionToDestination = destination - currentPos;
            directionToDestination.Normalize();

            double distanceToLastMove = (currentPos - currentDirectionStartPoint).Length;

            double angle = Maths.Angle(upDirection, directionToDestination);
            double angleDiff = Math.Abs(lastAngle - angle);
            Console.WriteLine($"angle  in  check: {angle}, angle diff: {angleDiff}, distance to dest: {distanceToDestination}");
            bool shouldReadjust = distanceToDestination <= distanceBeforeAlwaysAdjust 
                ||  (angleDiff >= minAngleBeforeAjdust && distanceToLastMove  >=  minSameDirectionLengthBeforeChangeOrientation);
            lastAngle = angle;
            return  shouldReadjust;
        }

        private void StartMove()
        {
            if (isMoving || upDirection.Length == 0 || destination.IsZero())
                return;

            screenPoint = GetScreenPointToMoveTo();
            
            ClickAt(screenPoint, () =>
            {
                currentDirectionStartPoint = currentPos;
                isMoving = true;
                KeyUtils.SendCharDown('w');
            });
        }

        private Point GetScreenPointToMoveTo()
        {
            Vector destinationDirection = destination - currentPos;
            destinationDirection.Normalize();

            double angle = Maths.Angle(upDirection, destinationDirection);

            Vector screenVecToRotate = screenUpPosition - screenCenter;

            Vector rotatedScreenPoint = screenVecToRotate.Rotate(angle);
            Vector newScreenVect = screenCenter + rotatedScreenPoint;

            Point newScreenPoint = newScreenVect.ToPoint();
            return newScreenPoint;
        }

        void SetCurrentPosition()
        {
            if (processing)
                return;

            processing = true;
            var result = ImgUtils.ProcessImage(104, 75, 175, 90, 2);
            processing = false;
            if (result == null || string.IsNullOrEmpty(result.Text))
            {
                return;
            }

            Point newPos = GetPointForText(result.Text);
            if (newPos.X > 0 && newPos.Y > 0)
            {
                currentPos = new Vector(newPos.X, newPos.Y);
            }
            if (initialPos.X == 0 && initialPos.Y == 0)
            {
                initialPos = currentPos;
            }
        }

        private void OnDirTextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            var keys = new List<TextBox>(directions.Keys);
            foreach (var key in keys)
            {
                if (key == txt)
                {
                    var point = GetPointForText(txt.Text);
                    if (point.X != 0 && point.Y != 0)
                        directions[key] = point;
                }
            }
            SaveData();
        }


        private void btnNavigate_Click(object sender, EventArgs e)
        {
            distanceToDestination = 0;
            lblUpDir.Text = "";
            timerUpdate.Restart();
            upDirection = new Vector(0, 0);
            initialPos = new Vector(0, 0);
            var point = GetPointForText(txtNavPos.Text);
            destination = point.ToVector();

            screenCenter = GetPointForText(txtScreenCenter.Text).ToVector();
            screenUpPosition = directions[txtUp].ToVector();

            WinUtils.ActivateWindow(Constants.KO_WINDOW);
            ActionManager.SendAction(() => { }, 0.01f, () =>
            {
                screenPoint = directions[txtUp];
                lastAngle = 0;
                ClickAt(screenPoint, () =>
                {
                    currentDirectionStartPoint = currentPos;
                    isMoving = true;
                    KeyUtils.SendCharDown('w');
                });
            });
        }

        void ClickAt(Point point, Action callback = null)
        {
            ActionManager.SendAction(() => { WinUtils.ActivateWindow(Constants.KO_WINDOW); }, 0.01f, () =>
            {
                MouseOperations.Click(point);
                if (callback != null)
                {
                    ActionManager.SendAction(() => { }, 0.1f, () => { callback.Invoke(); });
                }
            });
        }

        Point GetPointForText(string str)
        {
            Point point = new Point();
            string[] strs = str.Split(',');
            if (strs.Length == 1)
                strs = strs[0].Split(' ');
            if (strs.Length != 2)
                return point;

            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i].Trim().Replace(" ", "");
            }

            int x = 0;
            int y = 0;

            bool succes = int.TryParse(strs[0], out x);
            succes = succes && int.TryParse(strs[1], out y);
            if (succes)
                point = new Point(x, y);

            return point;
        }

        #region Data save

        void LoadData()
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "nav-save.dat");
            if (!File.Exists(file))
                return;

            var keys = new List<TextBox>(directions.Keys);
            List<string> strs = new List<string>();
            using (var sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                    strs.Add(sr.ReadLine());
            }
            int i = 0;
            foreach (var key in keys)
            {
                if (i < strs.Count)
                {
                    key.Text = strs[i++];
                }
                else
                    break;
            }
        }

        void SaveData()
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "nav-save.dat");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var sw = new StreamWriter(file))
            {
                foreach (var dir in directions)
                {
                    sw.WriteLine(dir.Key.Text ?? "");
                }
            }
        }

        #endregion

        #region button clicks


        private void btnGoUp_Click(object sender, EventArgs e)
        {
            ActionManager.SendAction(() => { WinUtils.ActivateWindow(Constants.KO_WINDOW); }, 2, () =>
            {
                MouseOperations.Click(directions[txtUp]);
            });
        }

        private void btnGoRight_Click(object sender, EventArgs e)
        {
            ActionManager.SendAction(() => { WinUtils.ActivateWindow(Constants.KO_WINDOW); }, 2, () =>
            {
                MouseOperations.Click(directions[txtRight]);
            });
        }

        private void btnGoDown_Click(object sender, EventArgs e)
        {
            ActionManager.SendAction(() => { WinUtils.ActivateWindow(Constants.KO_WINDOW); }, 2, () =>
            {
                MouseOperations.Click(directions[txtDown]);
            });
        }

        private void btnGoLeft_Click(object sender, EventArgs e)
        {
            ActionManager.SendAction(() => { WinUtils.ActivateWindow(Constants.KO_WINDOW); }, 2, () =>
            {
                MouseOperations.Click(directions[txtLeft]);
            });
        }

        private void ClickUp()
        {
            MouseOperations.Click(directions[txtUp]);
        }

        private void ClickDown()
        {
            MouseOperations.Click(directions[txtDown]);
        }

        private void ClickRight()
        {
            MouseOperations.Click(directions[txtRight]);
        }

        private void ClickLeft()
        {
            MouseOperations.Click(directions[txtLeft]);
        }

        #endregion

        private void TrySetUpDir()
        {
            if (upDirection.Length > 0 || initialPos.IsZero())
                return;

            var vec = currentPos - initialPos;
            if (vec.Length >= minInitialUpDirection)
            {
                upDirection = vec;
                upDirection.Normalize();
                this.Invoke((MethodInvoker)delegate
                {
                    lblUpDir.Text = $"Up dir: {Math.Round(upDirection.X, 2)},{Math.Round(upDirection.Y, 2)}";
                });
            }
        }


        private void OnInitDone(object sender, ElapsedEventArgs e)
        {
            timerInit.Stop();
            _initAction?.Invoke();
            _initAction = null;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            var point = GetPointForText(txtClickPoint.Text);
            if (point.X != 0 && point.Y != 0)
            {
                ClickAt(point);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lastAngle = 0;
            timerUpdate.Stop();
            isMoving = false;
            distanceToDestination = 0;
            KeyUtils.SendCharUp('w');
            upDirection = new Vector(0, 0);
            initialPos = new Vector(0, 0);
            currentDirectionStartPoint = new Vector(0, 0);
        }

        private void FormNavigation_Load(object sender, EventArgs e)
        {

        }
    }
}
