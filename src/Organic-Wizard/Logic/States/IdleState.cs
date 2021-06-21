using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard.Logic.States
{
    public class IdleState : CharState
    {
        public IdleState(StateMachine sm) : base(sm)
        {
        }

        public override EState GetName()
        {
            return EState.Idle;
        }

        public override void OnEnter()
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _sm.TrySetToNextPendingStates();
        }
    }
}
