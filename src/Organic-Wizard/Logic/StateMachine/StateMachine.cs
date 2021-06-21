
using Organic_Wizard.Logic.States;
using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public class StateMachine
    {
        protected int _smUpdateDelay = 5;
        private State _currentState;
        private State _previousState;
        Dictionary<EState, State> _states;
        Dictionary<EState, Type> _statesTypes;
        Dictionary<EState, Dictionary<string, object>> _passedData;
        ConcurrentQueue<EState> _pendingStates;

        public EState CurrentState { get { return _currentState == null ? EState.Idle : _currentState.GetName(); } }

        public EState PreviousState { get { return _previousState == null ? EState.Idle : _previousState.GetName(); } }

        public List<EState> States { get { return _states.Keys.ToList(); } }

        public Dictionary<EState, Type> StatesTypes { get { return _statesTypes; } }

        public bool HasPendingStates { get { return _pendingStates.Count > 0; } }

        public event Action<EState> StateFinished;

        private bool _isRunning = false;

        public StateMachine()
        {
            _statesTypes = new Dictionary<EState, Type>();
            _states = new Dictionary<EState, State>();
            _pendingStates = new ConcurrentQueue<EState>();
            _passedData = new Dictionary<EState, Dictionary<string, object>>();
            AddState(new IdleState(this));
        }

        public void AddState(State state)
        {
            EState stateName = state.GetName();
            if (_states.ContainsKey(stateName))
                throw new InvalidOperationException("Cannot have multiple states of the same type or a null state");

            _statesTypes[stateName] = state.GetType();
            _states[stateName] = state;
        }
        public State GetState(EState state)
        {
            if (_states.ContainsKey(state))
                return _states[state];
            throw new Exception($"Trying to get a state that does not exist: {state}");
        }

        public void AddPendingState(EState state, Dictionary<string, object> passedData = null)
        {
            if (!_pendingStates.Contains(state))
            {
                if (passedData != null)
                {
                    _passedData[state] = passedData ?? new Dictionary<string, object>();
                }
                else
                {
                    _passedData[state] = new Dictionary<string, object>();
                }
                _pendingStates.Enqueue(state);
            }
            else
                throw new InvalidOperationException("Pending state is already in queue, fix your logic.");
        }

        public bool TrySetToNextPendingStates()
        {
            if (_pendingStates.Count > 0)
            {
                EState newState;
                if (_pendingStates.TryDequeue(out newState))
                {
                    Debug.Log($"Send pending state: {newState}, {_pendingStates.Count}");
                    SetState(newState);
                    return true;
                }
            }
            return false;
        }

        public void Start()
        {
            if (_isRunning)
                return;

            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
            SetState(EState.Idle);
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnUpdate();
            }
        }

        private void OnStateFinished(EState finishedState)
        {
            if (CurrentState != finishedState)
                throw new InvalidOperationException("OnStateFinished called for something else than the current state, fix your logic.");

            if (!TrySetToNextPendingStates())
            {
                StateFinished?.Invoke(finishedState);
            }
        }

        public void SetState(EState newState, Dictionary<string, object> data)
        {
            if (_states.ContainsKey(newState))
            {
                _passedData[newState] = data;
            }

            SetState(newState);
        }

        public void SetState(EState newState)
        {
            if (!_states.ContainsKey(newState))
                throw new ArgumentException($"State machine does not contain the state {newState}");

            Debug.Log("Set new state: " + newState);

            if (_currentState != null)
            {
                _currentState.StateFinished -= OnStateFinished;
                _currentState.PendingState -= AddPendingState;
                _currentState.OnLeave();
                _previousState = _currentState;
            }

            _currentState = _states[newState];
            if (_passedData.ContainsKey(newState))
            {
                _currentState.SetPassedData(_passedData[newState]);
                _passedData.Remove(newState);
            }
            _currentState.StateFinished += OnStateFinished;
            _currentState.PendingState += AddPendingState;
            _currentState.OnEnter();
        }
    }
}
