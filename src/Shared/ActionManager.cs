using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Shared
{
    public class ActionManager
    {
        const int INSTANT_ACTIONS_DELAY = 30;
        static ActionManager _intance = null;
        ConcurrentQueue<ActionInfo> _instantActions = null;
        ConcurrentQueue<ActionInfo> _consecutiveActions = null;
        ActionInfo _currentAction = null;
        Timer _timerInstant;
        Timer _timerConsecutive;
        bool _updatingConsecutiveActions = false;
        bool _disposed = false;
        bool _running = false;

        private static ActionManager Instance
        {
            get
            {
                if (_intance == null)
                {
                    _intance = new ActionManager();
                }
                return _intance;
            }
        }

        private ActionManager()
        {
            _instantActions = new ConcurrentQueue<ActionInfo>();
            _consecutiveActions = new ConcurrentQueue<ActionInfo>();

            _timerInstant = new Timer();
            _timerInstant.Interval = INSTANT_ACTIONS_DELAY;
            _timerInstant.AutoReset = false;
            _timerInstant.Elapsed += OnTimer_UpdateInstantActions;
            _timerInstant.Disposed += OnTimer_UpdateInstantActions_Disposed;

            _timerConsecutive = new Timer();
            _timerConsecutive.AutoReset = false;
            _timerConsecutive.Elapsed += OnTimer_UpdateConsecutiveActions;
            _timerConsecutive.Disposed += OnTimer_UpdateConsecutiveActions_Disposed;
        }

        public static void Start()
        {
            Stop();
            Instance._running = true;
        }

        public static void Stop()
        {
            Instance._running = false;
            Instance._timerConsecutive.Stop();
            Instance._timerInstant.Stop();
            Instance._instantActions = new ConcurrentQueue<ActionInfo>();
            Instance._consecutiveActions = new ConcurrentQueue<ActionInfo>();
            Instance._currentAction = null;
            Instance._updatingConsecutiveActions = false;
        }

        private void OnTimer_UpdateConsecutiveActions_Disposed(object sender, EventArgs e)
        {
            _disposed = true;
            _timerConsecutive.Elapsed -= OnTimer_UpdateConsecutiveActions;
            _timerConsecutive.Disposed -= OnTimer_UpdateConsecutiveActions_Disposed;
        }

        private void OnTimer_UpdateInstantActions_Disposed(object sender, EventArgs e)
        {
            _disposed = true;
            _timerInstant.Elapsed -= OnTimer_UpdateInstantActions;
            _timerInstant.Disposed -= OnTimer_UpdateInstantActions_Disposed;
        }

        public static void SendInstantAction(Action action, Action callback)
        {
            if (action != null)
            {
                Instance._instantActions.Enqueue(new ActionInfo(action, callback));
                Instance._timerInstant.Start();
            }
        }

        public static void SendAction(Action action, float duration, Action callback)
        {
            Send(action, duration, callback, false);
        }

        public static void SendDelayedAction(float delay, Action action)
        {
            Send(null, delay, action, true);
        }

        private static void Send(Action action, float duration, Action callback, bool acceptNull)
        {
            if (action == null && !acceptNull)
                throw new InvalidOperationException("Cannot send a null action");

            Instance._consecutiveActions.Enqueue(new ActionInfo(action, duration, callback));
            if (Instance._updatingConsecutiveActions)
                return;

            if (Instance._consecutiveActions.Count == 1)
            {
                action?.Invoke();
                Instance._timerConsecutive.Interval = Math.Max(0, duration) * 1000;
                Instance._timerConsecutive.Stop();
                Instance._timerConsecutive.Start();
            }

        }

        private void OnTimer_UpdateConsecutiveActions(object sender, ElapsedEventArgs e)
        {
            if (_disposed || !_running)
                return;

            Instance._updatingConsecutiveActions = true;
            Instance._timerConsecutive.Stop();
            ActionInfo actionInfo = null;
            Instance._consecutiveActions.TryDequeue(out actionInfo);
            if (Instance._consecutiveActions.Count > 0 && actionInfo == null)
            {
                Instance._timerConsecutive.Start();
                return;
            }

            actionInfo.Callback?.Invoke();

            if (Instance._consecutiveActions.Count > 0)
            {
                actionInfo = null;
                Instance._consecutiveActions.TryPeek(out actionInfo);
                if (actionInfo != null)
                {
                    Instance._currentAction = actionInfo;
                    Instance._currentAction.Action?.Invoke();
                    Instance._timerConsecutive.Interval = Math.Max(_currentAction.Duration * 1000, 1);
                }
                Instance._timerConsecutive.Start();
            }
            else
            {
                Instance._currentAction = null;
            }
            Instance._updatingConsecutiveActions = false;
        }

        private void OnTimer_UpdateInstantActions(object sender, ElapsedEventArgs e)
        {
            if (_disposed || !_running)
                return;

            Instance._timerInstant.Stop();

            if (Instance._instantActions.Count > 0)
            {
                ActionInfo action = null;
                Instance._instantActions.TryDequeue(out action);
                if (action != null)
                {
                    action.Action?.Invoke();
                    action.Callback?.Invoke();
                }
                Instance._timerInstant.Start();
            }
        }


        public class ActionInfo
        {
            public ActionInfo(Action action)
            {
                Action = action;
            }

            public ActionInfo(Action action, Action callback) : this(action)
            {
                Callback = callback;
            }

            public ActionInfo(Action action, float duration, Action callback) : this(action, callback)
            {
                Duration = duration;
            }

            public Action Callback { get; set; }
            public Action Action { get; set; }
            public float Duration { get; set; }
        }
    }
}
