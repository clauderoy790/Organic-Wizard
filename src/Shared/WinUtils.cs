using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Shared
{
    public static class WinUtils
    {
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const int SHOW_MAXIMIZED = 3;

        public static void ActivateWindow(string window)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process proc in processes)
            {
                if (ProcessIsWindow(proc, window))
                {
                    ActivateWindowFromHandle(proc.MainWindowHandle);
                    break;
                }
            }
        }

        private static void ActivateWindowFromHandle(IntPtr mainWindowHandle)
        {
            // Guard: check if window already has focus.
            if (mainWindowHandle == GetForegroundWindow()) return;

            // Show window maximized.
            ShowWindow(mainWindowHandle, SHOW_MAXIMIZED);

            // Simulate an "ALT" key press.
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | 0, 0);

            // Simulate an "ALT" key release.
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);

            // Show window in forground.
            SetForegroundWindow(mainWindowHandle);
        }

        private static bool ProcessIsWindow(Process proc, string window)
        {
            return proc.MainWindowTitle.EndsWith(window, StringComparison.InvariantCultureIgnoreCase);
        }

        private static void WinWaitActive(string window)
        {
            using (Process p = System.Diagnostics.Process.Start(window+".exe"))
            {
                p.WaitForInputIdle();
            }
        }

        

        public static string GetActiveWindow()
        {
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return null;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    }
}
