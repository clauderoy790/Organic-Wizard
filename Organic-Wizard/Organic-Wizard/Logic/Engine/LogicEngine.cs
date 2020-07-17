using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Shared;

namespace Organic_Wizard
{
    public class LogicEngine
    {
        bool FOCUS_KO_WINDOW_ON_START = true;
        const int START_DELAY = 2000;
        const int UPDATE_DELAY = 20;

        bool _running = false;
        Timer _initTimer = null;
        Timer _updateTimer = null;
        AttackerStateMachine _archerStateMachine;
        RecoveryStateMachine _recoveryStateMachine;
        List<GameObject> _gos = null;

        public LogicEngine()
        {
            ImgUtils.Init();
            _gos = new List<GameObject>();
            //_archerStateMachine = new AttackerStateMachine();
            //_recoveryStateMachine = new RecoveryStateMachine();
            _initTimer = new Timer();
            _initTimer.Elapsed += OnInitTimer;
            _initTimer.Interval = START_DELAY;

            _updateTimer = new Timer();
            _updateTimer.Elapsed += OnUpdate;
            _updateTimer.Interval = UPDATE_DELAY;

            GameObject go = new GameObject();
            CharacterInfo charInfo = new CharacterInfo();
            StopOnMinimize stopOnMin = new StopOnMinimize(ToggleStartStop);
            go.AddComponent(charInfo);
            //go.AddComponent(stopOnMin);
            _gos.Add(go);
        }

        private void OnInitTimer(object sender, ElapsedEventArgs e)
        {
            if (WinUtils.GetActiveWindow() != Constants.KO_WINDOW && FOCUS_KO_WINDOW_ON_START)
            {
                WinUtils.ActivateWindow(Constants.KO_WINDOW);
            }
            
            _initTimer.Stop();
            _gos.ForEach(g => { g.Active = true; });
            _updateTimer.Stop();
            _updateTimer.Start();
        }

        void Start()
        {
            Stop();
            _initTimer.Stop();
            _initTimer.Start();
        }

        void Stop()
        {
            _initTimer.Stop();
            _gos.ForEach(g => { g.Active = false; });
            //_archerStateMachine.Stop();
            //_recoveryStateMachine.Stop();
        }

        private void OnUpdate(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();
            Time.StartFrame();
            try
            {
                foreach(var go in _gos)
                {
                    go.Update();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: "+ex.Message);
            }
            Time.EndFrame();
            _updateTimer.Start();
        }

        public void ToggleStartStop()
        {
            _running = !_running;
            if (_running)
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
