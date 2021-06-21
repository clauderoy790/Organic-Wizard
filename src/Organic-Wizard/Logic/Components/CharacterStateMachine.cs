using Organic_Wizard.Logic.States;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard.Logic
{
    public class CharacterStateMachine : Component
    {
        StateMachine _sm = null;

        public EState State { get { return _sm.CurrentState; } }

        public CharacterStateMachine()
        {
            _sm = new StateMachine();
            _sm.AddState(new CheckForPartyState(_sm));
            _sm.AddState(new BuffState(_sm));
            _sm.AddState(new AttackState(_sm));
            _sm.AddState(new CheckForBuffState(_sm));
            _sm.AddState(new CheckForHealState(_sm));
            _sm.AddState(new HealingState(_sm));
            _sm.AddState(new MoveState(_sm));
            _sm.AddState(new StuckState(_sm));
            _sm.AddState(new OpenChestState(_sm));
            _sm.AddState(new LootState(_sm));
            _sm.AddState(new InitUpState(_sm));
            _sm.AddState(new CheckInventoryState(_sm));
            _sm.StateFinished += OnStateFinished;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _sm.Start();
            _sm.SetState(EState.InitUpPosition);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _sm.Update();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _sm.Stop();
        }

        public void AddPendingState(EState state)
        {
            _sm.AddPendingState(state);
        }

        public void AddPendingState(EState state, Dictionary<string, object> passedData)
        {
            _sm.AddPendingState(state, passedData);
        }

        private void OnStateFinished(EState currentState)
        {
            switch (currentState)
            {
                case EState.InitUpPosition:
                    _sm.SetState(EState.CheckForParty);
                    break;
                case EState.CheckForParty:
                    _sm.SetState(EState.CheckForBuff);
                    break;
                case EState.CheckForBuff:
                case EState.Buffing:
                    BuffingStatesFinished();
                    break;
                case EState.CheckForHeal:
                case EState.Healing:
                    HealingStatesFinished();
                    break;
                case EState.CheckInventory:
                    _sm.SetState(EState.TryOpenChest);
                    break;
                case EState.Attacking:
                case EState.TryOpenChest:
                case EState.Loot:
                    AttackFinished();
                    break;
            }
        }

        private void AttackFinished()
        {
            System.Console.WriteLine("Attack finished:" + _sm.CurrentState);
            if (SavedData.RecoveryMode)
                _sm.SetState(EState.CheckForHeal);
            else if (SavedData.AttackMode)
                _sm.SetState(EState.Attacking);
        }

        private void HealingStatesFinished()
        {
            if (SavedData.AttackMode)
                _sm.SetState(EState.Attacking);
        }

        private void BuffingStatesFinished()
        {
            if (SavedData.RecoveryMode)
                _sm.SetState(EState.CheckForHeal);
            else if (SavedData.AttackMode)
                _sm.SetState(EState.Attacking);
        }
    }
}
