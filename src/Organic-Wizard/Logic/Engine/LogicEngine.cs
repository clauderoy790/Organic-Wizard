using Organic_Wizard.Logic;
using Shared;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Organic_Wizard
{
    public class LogicEngine
    {
        public static event Action Stopped;
        public static event Action Started;

        const float START_DELAY = 1.2f;
        const float UPDATE_DELAY = 0.1f;
        const int MAX_ATTEMPT_INITIALIZE_SKILLBAR = 5;

        bool _sendStart = false;
        CTimer _initTimer = null;
        CTimer _updateTimer = null;
        List<GameObject> _gos = null;

        public LogicEngine()
        {
            ImgUtils.Init();
            KeyUtils.WindowToSendKeysIn = Constants.KO_WINDOW;
            _gos = new List<GameObject>();
            _initTimer = new CTimer();
            _initTimer.Interval = START_DELAY * 1000;
            _initTimer.AutoReset = false;

            _updateTimer = new CTimer();
            _updateTimer.Elapsed += OnUpdate;
            _updateTimer.AutoReset = false;
            _updateTimer.Interval = UPDATE_DELAY * 1000;

            GameObject go = new GameObject();
            CharacterStateMachine sm = new CharacterStateMachine();
            Character character = new Character(sm);
            DirComponent direction = new DirComponent();
            ScreenPosition screenPos = new ScreenPosition();
            go.AddComponent(screenPos);
            go.AddComponent(sm);
            go.AddComponent(direction);
            go.AddComponent(character);
            _gos.Add(go);
            _sendStart = true;
        }

        private void OnInitTimer(object sender, ElapsedEventArgs e)
        {
            ActionManager.Start();
            KeyUtils.Start();
            Inventory.Clear();
            MouseOperations.Start();
            _initTimer.Stop();
            _updateTimer.Elapsed -= OnUpdate;
            _updateTimer.Stop();
            ActionManager.SendAction(() =>
            {
                WinUtils.ActivateWindow(Constants.KO_WINDOW);
            }, 0.3f, () =>
            {
                int attempts = 0;
                SkillBar.Reset();
                while (!SkillBar.IsInitialized && attempts++ < MAX_ATTEMPT_INITIALIZE_SKILLBAR)
                {
                    SkillBar.InitSkillsInfo();
                }

                if (SkillBar.IsInitialized)
                {
                    _gos.ForEach(g => { g.Active = true; });

                    Started?.Invoke();
                    _sendStart = false;
                    _updateTimer.Restart();
                    _updateTimer.Elapsed += OnUpdate;
                }
                else
                {
                    Stop();
                    throw new Exception("Failed to initialize SkillBarManager, aborting.");
                }
            });
        }

        public void Start()
        {
            _initTimer.Elapsed += OnInitTimer;
            _initTimer.Restart();
            Debug.Log("Start");
        }

        public void Stop()
        {
            KeyUtils.Stop();
            MouseOperations.Stop();
            _initTimer.Elapsed -= OnInitTimer;
            _initTimer.Stop();
            _updateTimer.Elapsed -= OnUpdate;
            _updateTimer.Stop();
            _gos.ForEach(g => { g.Active = false; });
            Stopped?.Invoke();
            _sendStart = true;
            ActionManager.Stop();
            Debug.Log("Stop");
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();
            string activeWindow = WinUtils.GetActiveWindow();

            if (string.IsNullOrEmpty(activeWindow) || activeWindow != Constants.KO_WINDOW)
            {
#if DEBUG
                if (!activeWindow.ToLower().Contains("visual studio"))
#endif
                {
                    Stop();
                    return;
                }
            }

            Time.StartFrame();
            try
            {
                foreach (var go in _gos)
                {
                    go.Update();
                }
                Time.EndFrame();
                _updateTimer.Start();
            }
            catch (Exception ex)
            {
                Debug.Log("Exception: " + ex.Message);
                Stop();
            }
        }

        public void ToggleStartStop()
        {
            if (_sendStart)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }
    }
}
