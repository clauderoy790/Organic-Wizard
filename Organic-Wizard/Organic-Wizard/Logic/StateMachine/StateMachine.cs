using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public abstract class StateMachine
    {
        protected int _smUpdateDelay = 5;
        protected IState _currentState;

        private Timers.Timer _updateTimer = null;
        private bool _isRunning = false;

        public StateMachine()
        {
            _updateTimer = new Timers.Timer();
            _updateTimer.Interval = _smUpdateDelay;
            _updateTimer.AutoReset = false;
            _updateTimer.Elapsed += OnUpdateTimerTick;
        }

        public virtual void Start()
        {
            if (_isRunning)
                return;

            _isRunning = true;
            _updateTimer.Stop();
            _updateTimer.Start();
        }

        public virtual void Stop()
        {
            _isRunning = false;
            if (_currentState != null)
                _currentState.OnLeave();
            _updateTimer.Stop();
        }

        private void OnUpdateTimerTick(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();
            _smUpdateDelay = Math.Max(0, _smUpdateDelay);
            _updateTimer.Interval = _smUpdateDelay;

            if (_currentState != null)
                _currentState.OnUpdate();

            _updateTimer.Start();
        }

        protected void SetState(IState newState)
        {
            if (newState == null)
                throw new Exception("Cannot set state to null");

            if (_currentState != null)
                _currentState.OnLeave();

            _currentState = newState;
            _currentState.OnEnter();
        }
    }
}
