using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Remoting;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public abstract class State : IState
    {
        protected Dictionary<string, object> _passedData = null;

        public event Action<EState> StateFinished;
        public event Action<EState,Dictionary<string, object>> PendingState;

        public State()
        {
            _passedData = new Dictionary<string, object>();
        }
        public abstract EState GetName();

        protected T GetParam<T>(string paramName)
        {
            T obj = default(T);

            if (_passedData.ContainsKey(paramName))
            {
                try
                {
                    obj = (T)_passedData[paramName];
                }
                catch { }
            }

            return obj;
        }

        public void SetPassedData(Dictionary<string,object> passedData)
        {
            if (passedData == null)
                return;

            _passedData = passedData;
        }

        protected void FinishState()
        {
            StateFinished?.Invoke(GetName());
        }

        protected void AddPendingState(EState state)
        {
            PendingState?.Invoke(state,_passedData);
        }

        protected void AddPendingState(EState state,Dictionary<string,object> data)
        {
            if (data != null)
                _passedData = data;
            PendingState?.Invoke(state,_passedData);
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnLeave();
    }
}
