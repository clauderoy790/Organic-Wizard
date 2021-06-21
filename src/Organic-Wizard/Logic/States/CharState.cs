using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Timers;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Organic_Wizard.Logic;
using Organic_Wizard.Logic.States;
using Shared;
using Vector = System.Windows.Vector;
using static Shared.KeyUtils;

namespace Organic_Wizard
{
    public abstract class CharState : State
    {
        const float DELAY_BETWEEN_KEYS = 0.25f;
        protected StateMachine _sm;
        protected int _currentMemberIndex = 0;
        protected bool _isActive = false;
        protected CheckForPartyState _partyState;
        private int _indexToSelect = 0;
        private static Dictionary<EState, State> _states;

        protected bool IsActive
        {
            get { return _isActive; }
        }

        static CharState()
        {
            _states = new Dictionary<EState, State>();
        }

        public CharState(StateMachine sm)
        {
            _sm = sm;
            _isActive = false;
        }

        #region Component Methods
        public override void OnEnter()
        {
            if (_partyState == null)
                _partyState = GetState<CheckForPartyState>();

            _isActive = true;
            _currentMemberIndex = 0;
            Character.MpChanged -= OnMpChanged;
            Character.MpChanged += OnMpChanged;

            Character.HpChanged -= OnHpChanged;
            Character.HpChanged += OnHpChanged;
        }

        public override void OnUpdate()
        {
        }

        public override void OnLeave()
        {
            _isActive = false;
            Character.HpChanged -= OnHpChanged;
            Character.MpChanged -= OnMpChanged;
        }
        #endregion

        private void OnHpChanged(int newHp)
        {
            if (SavedData.HpRecovery && SavedData.HpRecoverySkill != Constants.NONE &&
                            Character.HpPercent < SavedData.HpRecoveryPercent)
            {
                SendInstantKey(SavedData.HpRecoverySkill);
            }
        }

        private void OnMpChanged(int newMp)
        {
            if (SavedData.MpRecovery && SavedData.MpRecoverySkill != Constants.NONE &&
                Character.MpPercent < SavedData.MpRecoveryPercent)
            {
                SendInstantKey(SavedData.MpRecoverySkill);
            }
        }

        protected void SendCombination(EKeyCombination combination, int duration, Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.SendCombination(combination); }, duration, callback);
        }

        protected void SendKey(string str)
        {
            SendKey(str,DELAY_BETWEEN_KEYS,null);
        }

        protected void SendKey(string str, Action callback)
        {
            SendKey(str, DELAY_BETWEEN_KEYS, callback);
        }

        protected void SendKey(Keyboard.ScanCodeShort key, float duration, Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.Send(key); }, duration, callback);
        }

        protected void SendKey(string str, float duration, Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.SendMessage(str); }, duration, callback);
        }

        protected void SendKeyDown(char key, float duration, Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.SendCharDown(key); }, duration, callback);
        }

        protected void SendKeyUp(char key, Action callback = null)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.SendCharUp(key); }, DELAY_BETWEEN_KEYS, callback);
        }

        protected void SendKey(char key, float duration = DELAY_BETWEEN_KEYS)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.SendChar(key); }, duration, null);
        }

        protected void SendKey(int key, float duration, Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.Send(key); }, duration, callback);
        }

        protected void SendKey(int key, float duration = DELAY_BETWEEN_KEYS)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() => { KeyUtils.Send(key); }, duration, null);
        }

        protected void SendInstantKey(int key, Action callback = null)
        {
            if (!IsActive)
                return;
            ActionManager.SendInstantAction(() => { KeyUtils.Send(key); }, callback);
        }

        protected void SendInstantKey(string key, Action callback = null)
        {
            if (!IsActive)
                return;
            ActionManager.SendInstantAction(() => { KeyUtils.SendMessage(key); }, callback);
        }
        protected void SetState(EState newState,Dictionary<string,object> data)
        {
            _sm.SetState(newState, data);
        }

        protected void SetState(EState newState)
        {
            _sm.SetState(newState);
        }

        protected void SelectSelf(Action callback = null)
        {
            if (!IsActive)
                return;

            if (!Character.IsInParty)
            {
                callback?.Invoke();
                return;
            }
            else
            {
                MouseOperations.Click(ScreenPosition.Center);
                callback?.Invoke();
            }
        }
        protected void SelectPartyMember(int index, Action<bool> callback)
        {
            if (!IsActive || !Character.IsInParty || index < 0 || index >= Character.PartySize)
                return;

            _indexToSelect = index;
            CheckPtMemberAndSelectNext(callback);
        }

        private void CheckPtMemberAndSelectNext(Action<bool> callback)
        {
            _partyState.SetPartyIndex();
            if (_indexToSelect == Character.SelectedPartyMemberIndex)
            {
                callback?.Invoke(true);
            }
            else
            {
                SelectNextPartyMember(callback);
            }
        }

        protected void SelectNextPartyMember(Action<bool> callback)
        {
            if (!IsActive)
                return;

            if (Character.IsInParty)
            {
                SendTab(() =>
                {
                    CheckPtMemberAndSelectNext(callback);
                });
            }
            else
                callback?.Invoke(false);
        }


        protected void SendTab(Action callback)
        {
            if (!IsActive)
                return;
            ActionManager.SendAction(() =>
            {
                if (!IsActive)
                    return;
                KeyUtils.Send(Keyboard.ScanCodeShort.TAB);
            }, Constants.TAB_DELAY, callback);
        }

        protected void ClickAt(Point point)
        {
            MouseOperations.Click(point);
        }

        protected void ClickAt(Vector point, Action callback)
        {
            MouseOperations.Click(point.ToPoint(), callback);
        }

        protected void ClickAt(Vector point, Action callback,EClickType clickType)
        {
            MouseOperations.Click(point.ToPoint(), callback, clickType);
        }

        protected void ClickAt(Point point, Action callback)
        {
            MouseOperations.Click(point, callback);
        }

        protected void ClickAt(Point point, Action callback,EClickType clickType)
        {
            MouseOperations.Click(point, callback,clickType);
        }

        protected T GetState<T>() where T : State
        {
            Type t = typeof(T);

            if (!_sm.StatesTypes.ContainsValue(t))
                throw new Exception($"Failed to find state name for type {t.ToString()}");

            EState state = _sm.StatesTypes.First((p) => { return p.Value == t; }).Key;
            if (!_states.ContainsKey(state))
            {
                var stateObj = _sm.GetState(state);
                if (stateObj != null)
                    _states[state] = stateObj;
            }

            if (_states.ContainsKey(state))
            {
                if (_states[state] is T)
                {
                    return (T)_states[state];
                }
                else
                    throw new InvalidCastException($"{state} is not of type {typeof(T)}");
            }
            else
                throw new InvalidOperationException($"State {state} does not exist in state cache.");
        }
    }
}
