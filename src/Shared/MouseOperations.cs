using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using Vector = System.Windows.Vector;

namespace Shared
{
    public static class MouseOperations
    {
        private const double DEFAULT_CLICK_TIME = 0.3;
        private const int DEFAULT_CLICK_RELEASE_TIME = 50;
        private const int DEFAULT_NB_CLICKS = 2;
        private static CTimer timerReleaseClick;
        private static CTimer timerClick;
        private static EClickType currentClickType;
        private static Action currentCallback;
        private static Point currentPoint;
        private static int nbClicks = 0;
        private static int currentClicks = 0;
        private static bool running = false;

        static MouseOperations()
        {
            timerReleaseClick = new CTimer();
            timerReleaseClick.Interval = DEFAULT_CLICK_RELEASE_TIME;
            timerReleaseClick.Elapsed += OnClickDone;

            timerClick = new CTimer();
            timerClick.IntervalSeconds = DEFAULT_CLICK_TIME;
            timerClick.Elapsed += PerformClick;
        }

        [Flags]
        private enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void Start()
        {
            running = true;
        }

        public static void Stop()
        {
            currentCallback = null;
            running = false;
            timerClick.Stop();
            timerReleaseClick.Stop();
            ReleaseCurrentClick();
        }

        private static void PerformClick(object sender, ElapsedEventArgs e)
        {
            if (!running)
                return;

            if (currentClicks < nbClicks)
            {
                currentClicks++;
                Click(currentPoint, currentClickType);
                timerClick.Restart();
            }
            else
            {
                currentClicks = 0;
                currentCallback?.Invoke();
                currentCallback = null;
            }
        }

        public static void Click(Vector position, EClickType clickType = EClickType.Left)
        {
            Click(position.ToPoint(), DEFAULT_CLICK_RELEASE_TIME, clickType);
        }

        public static void Click(Point position, EClickType clickType)
        {
            Click(position, DEFAULT_CLICK_RELEASE_TIME, clickType);
        }

        public static void Click(Vector position, int duration, EClickType clickType)
        {
            Click(position.ToPoint(), duration, clickType);
        }

        public static void Click(Point position)
        {
            Click(position, DEFAULT_CLICK_RELEASE_TIME, EClickType.Left);
        }

        public static void Click(Point position, int duration, EClickType clickType)
        {
            if (duration <= 0 || !running)
                return;

            timerReleaseClick.Stop();
            ReleaseCurrentClick();
            SetCursorPos(position.X, position.Y);
            currentClickType = clickType;
            switch (currentClickType)
            {
                case EClickType.Left:
                    MouseEvent(MouseEventFlags.LeftDown);
                    break;
                case EClickType.Right:
                    MouseEvent(MouseEventFlags.RightDown);
                    break;
            }
            timerReleaseClick.Interval = duration;
            timerReleaseClick.Start();
        }

        public static void Click(Point point, Action callback)
        {
            Click(point, callback, EClickType.Left, DEFAULT_NB_CLICKS);
        }

        public static void Click(Point position, Action callback, EClickType clickType)
        {
            Click(position, callback, clickType, DEFAULT_NB_CLICKS);
        }

        public static void Click(Point point, Action callback, EClickType clickType, int clicks)
        {

            if (!running)
                return;

            nbClicks = Math.Max(1, clicks);
            currentClicks = 0;
            currentClickType = clickType;
            currentCallback = callback;
            currentPoint = point;
            timerClick.Stop();
            PerformClick(null, null);
        }

        private static void OnClickDone(object sender, ElapsedEventArgs e)
        {
            ReleaseCurrentClick();
        }

        private static void ReleaseCurrentClick()
        {
            switch (currentClickType)
            {
                case EClickType.Left:
                    MouseEvent(MouseEventFlags.LeftUp);
                    break;
                case EClickType.Right:
                    MouseEvent(MouseEventFlags.RightUp);
                    break;
            }
        }

        private static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        private static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
