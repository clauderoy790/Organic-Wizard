using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;
using System.Windows.Input;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Security.RightsManagement;

namespace Shared
{
    public class KeyUtils
    {
        private static Mutex mutex = new Mutex();
        private static float DELAY_BETWEEN_KEYS_COMBINATION = Keyboard.KEY_DOWN_TIME + 0.01f; //Add aditonnal delay to make sure we  don't overlap the keyboard
        static ConcurrentQueue<Keyboard.ScanCodeShort> _currentCombination;
        static Keyboard.ScanCodeShort? _currentKey;
        static Keyboard.ScanCodeShort? _repeatKey;
        static Keyboard _keyboard;
        static CTimer _timerCombination;
        static Action _callbackMultipleKey = null;
        static KeysConverter kc = new KeysConverter();
        static CTimer _timerRepeat;
        static string _window = "";
        static bool _active = false;

        public static string WindowToSendKeysIn
        {
            get
            {
                return _window;
            }

            set
            {
                if (_window != value)
                    _keyboard.SetWindow(value);
                _window = value;
            }
        }

        static KeyUtils()
        {
            _keyboard = new Keyboard();
            _timerCombination = new CTimer(DELAY_BETWEEN_KEYS_COMBINATION);
            _timerRepeat = new CTimer(DELAY_BETWEEN_KEYS_COMBINATION);
            _currentCombination = new ConcurrentQueue<Keyboard.ScanCodeShort>();
        }

        public static void Start()
        {
            _active = true;
            _timerCombination.Elapsed -= OnKeyTimer;
            _timerCombination.Elapsed += OnKeyTimer;

            _timerRepeat.Elapsed -= OnKeyRepeat;
            _timerRepeat.Elapsed += OnKeyRepeat;
        }

        public static void Stop()
        {
            _active = false;
            _keyboard.Stop();
            _timerRepeat.Stop();
            _timerCombination.Stop();
            _timerCombination.Elapsed -= OnKeyTimer;
            _timerRepeat.Elapsed -= OnKeyRepeat;
            if (_currentKey != null)
            {
                _keyboard.SendUp(_currentKey.Value);
                _currentKey = null;
            }
            if (_repeatKey != null)
            {
                _keyboard.SendUp(_repeatKey.Value);
                _repeatKey = null;
            }
        }

        public static void Send(Keyboard.ScanCodeShort key)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn || _currentKey != null)
                return;

            _currentKey = key;
            _keyboard.Send(_currentKey.Value);
            _timerCombination.Restart();
        }

        public static short KeyState(char c)
        {
            var code = GetScanCode(c);
            return _keyboard.KeyState(code);
        }

        public static void SendCombination(EKeyCombination keys, Action callback = null)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn)
                return;

            if (!_keyCombinations.ContainsKey(keys))
            {
                Debug.Log("Key map does not contain combination: " + keys.ToString());
                return;
            }

            if (_currentCombination != null)
            {
                Debug.Log("A combination is already running");
                return;
            }

            _callbackMultipleKey = callback;
            _currentCombination = GetKeyCombinations(keys);
            _timerCombination.Restart();
        }

        public static bool IsCharDown(char c)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn)
                return false;

            lock (mutex)
            {
                var key = GetScanCode(c);
                return _keyboard.IsDown(key);
            }
        }

        public static bool IsCharUp(char c)
        {
            return !IsCharDown(c);
        }

        public static void SendCharDown(char c)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn || !_active)
                return;

            lock (mutex)
            {
                var key = GetScanCode(c);
                _repeatKey = key;
                _keyboard.SendUp(key);
                _keyboard.SendDown(key);
                _timerRepeat.Start();
            }
        }

        public static void SendCharUp(char c)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn || !_active)
                return;

            lock (mutex)
            {
                var key = GetScanCode(c);
                _keyboard.SendUp(key);
                _timerRepeat.Stop();
                _repeatKey = null;
            }
        }

        public static void SendChar(char c, float duration = Keyboard.KEY_DOWN_TIME, Action callback = null)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn || !_active)
                return;

            lock (mutex)
            {
                if (_currentKey != null)
                {
                    return;
                }

                _currentKey = GetScanCode(c);
                _keyboard.SendUp(_currentKey.Value);
                _keyboard.Send(_currentKey.Value, duration, callback);
                _timerCombination.Restart();
            }
        }

        public static void SendMessage(string message, Action callback = null)
        {
            if (WinUtils.GetActiveWindow() != WindowToSendKeysIn || !_active)
                return;
            else if (_currentKey != null || string.IsNullOrEmpty(message))
                return;

            lock (mutex)
            {
                _currentCombination = GetCombination(message);
                _callbackMultipleKey = callback;
            }

            OnKeyTimer(null, null);
        }

        private static ConcurrentQueue<Keyboard.ScanCodeShort> GetCombination(string message)
        {
            ConcurrentQueue<Keyboard.ScanCodeShort> combination = new ConcurrentQueue<Keyboard.ScanCodeShort>();

            foreach (var ch in message)
            {
                Keyboard.ScanCodeShort keyCode = GetScanCode(ch);
                combination.Enqueue(keyCode);
            }

            return combination;
        }

        private static Keyboard.ScanCodeShort GetScanCode(char c)
        {
            string keyChar = kc.ConvertToString(c);
            byte vk = VkKeyScan(c);
            ushort scanCode = (ushort)MapVirtualKey(vk, 0);
            return (Keyboard.ScanCodeShort)scanCode;
        }

        private static void OnKeyRepeat(object sender, ElapsedEventArgs e)
        {
            lock (mutex)
            {
                if (_repeatKey.HasValue)
                {
                    _keyboard.SendUp(_repeatKey.Value);
                    if (_active)
                    {
                        _keyboard.SendDown(_repeatKey.Value);
                        _timerRepeat.Start();
                    }
                }
            }
        }

        private static void OnKeyTimer(object sender, ElapsedEventArgs e)
        {
            _currentKey = null;

            if (_currentCombination == null || !_active)
                return;

            if (_currentCombination.Count == 0)
            {
                _callbackMultipleKey?.Invoke();
                _callbackMultipleKey = null;
                _currentCombination = null;
            }
            else
            {
                Keyboard.ScanCodeShort key;
                if (_currentCombination.TryDequeue(out key))
                {
                    Send(key);
                }
                else
                {
                    _timerCombination.Start();
                }
            }
        }


        private static ConcurrentQueue<Keyboard.ScanCodeShort> GetKeyCombinations(EKeyCombination keys)
        {
            ConcurrentQueue<Keyboard.ScanCodeShort> lst = new ConcurrentQueue<Keyboard.ScanCodeShort>();

            foreach (var key in _keyCombinations[keys])
            {
                lst.Enqueue(key);
            }
            return lst;
        }

        public enum EKeyCombination
        {
            Town
        }

        static Dictionary<EKeyCombination, List<Keyboard.ScanCodeShort>> _keyCombinations = new Dictionary<EKeyCombination, List<Keyboard.ScanCodeShort>>()
        {
            { EKeyCombination.Town,new List<Keyboard.ScanCodeShort>() {
               Keyboard.ScanCodeShort.RETURN,
               Keyboard.ScanCodeShort.OEM_2,
               Keyboard.ScanCodeShort.KEY_T,
               Keyboard.ScanCodeShort.KEY_O,
               Keyboard.ScanCodeShort.KEY_W,
               Keyboard.ScanCodeShort.KEY_N,
               Keyboard.ScanCodeShort.RETURN,
            } }
        };

        public static void Send(int key)
        {
            key = Math.Max(0, key);
            key = Math.Min(key, 9);
            SendMessage(key.ToString());
        }

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        public static extern byte VkKeyScan(char ch);
    }
}
