using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public abstract class State : IState
    {
        private Timers.Timer _timer = null;
        private int _onEnterTimerDelay = 0;
        private Action _onEnterTimerDone = null;

        public State()
        {
            _timer = new Timers.Timer();
            _timer.AutoReset = false;
            _timer.Elapsed += OnEnterTimerDone;
        }

        protected void SetEnterTimerDoneAction(Action onEnterTimerDone)
        {
            _onEnterTimerDone = onEnterTimerDone;
        }

        protected void StartEnterTimer(int enterTimerDelay,Action onEnterTimerDone = null)
        {
            if (onEnterTimerDone != null)
                _onEnterTimerDone = onEnterTimerDone;

            _onEnterTimerDelay = enterTimerDelay;
            _timer.Stop();
            _timer.Interval = enterTimerDelay;
            _timer.Start();
        }

        private void OnEnterTimerDone(object sender, ElapsedEventArgs e)
        {
            if (_onEnterTimerDone != null)
                _onEnterTimerDone();
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public virtual void OnLeave()
        {
            _timer.Stop();
        }
    }
}
