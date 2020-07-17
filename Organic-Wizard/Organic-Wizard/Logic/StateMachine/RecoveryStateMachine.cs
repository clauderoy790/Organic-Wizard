using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    enum ERecoverryState
    {
        CHECK_POTS,
        RECOVERING
    }

    class RecoveryStateMachine : StateMachine
    {
        RecoveryState _recoveringState = null;
        RecoveryCheckState _checkRecoveryState = null;

        public RecoveryStateMachine()
        {
            _recoveringState = new RecoveryState();
            _recoveringState.OnRecoveryDone = OnRecoveryDone;
            _checkRecoveryState = new RecoveryCheckState(OnRecoveryNeeded);
        }

        private void OnRecoveryDone()
        {
            SetState(ERecoverryState.CHECK_POTS);
        }

        public override void Start()
        {
            base.Start();
            SetState(ERecoverryState.CHECK_POTS);
        }

        private void SetState(ERecoverryState state)
        {
            switch (state)
            {
                case ERecoverryState.CHECK_POTS:
                    SetState(_checkRecoveryState);
                    break;
                case ERecoverryState.RECOVERING:
                    SetState(_recoveringState);
                    break;
            }
        }

        private void OnRecoveryNeeded(bool isHpPot)
        {
            _recoveringState.UseHpPot = isHpPot;
            SetState(ERecoverryState.RECOVERING);
        }
    }
}
