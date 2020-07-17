using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared;

namespace Organic_Wizard
{
    class StopOnMinimize : Component
    {
        public static event Action OnMinimize = null;

        Action _minimizeAction = null;
        string _currentWindow = null;

        public StopOnMinimize(Action minimizeAction)
        {
            _minimizeAction = minimizeAction;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _currentWindow = null;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            string activeWindow = WinUtils.GetActiveWindow();
            if (!string.IsNullOrEmpty(_currentWindow) && _currentWindow == Constants.KO_WINDOW
                && activeWindow != Constants.KO_WINDOW)
            {
                _minimizeAction?.Invoke();
                OnMinimize?.Invoke();
            }

            _currentWindow = activeWindow;
        }
    }
}
