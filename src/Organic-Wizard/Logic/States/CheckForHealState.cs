using Organic_Wizard.Logic;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    class CheckForHealState : CharState
    {
        public CheckForHealState(StateMachine sm) : base(sm)
        {
        }
        public override EState GetName()
        {
            return EState.CheckForHeal;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (!SavedData.RecoveryMode)
            {
                FinishState();
                return;
            }

            SelectSelf(OnSelfSelected);
        }

        private void OnSelfSelected()
        {
            if (Character.HpPercent <= SavedData.RecoverySkillPecent)
            {
                SetState(EState.Healing);
            }
            else if (Character.IsInParty)
            {
                CheckPartyHealing();
            }
            else
            {
                FinishState();
            }
        }

        private void CheckPartyHealing()
        {
            SendTab(() =>
            {
                if (!Character.IsInParty)
                {
                    FinishState();
                    return;
                }

                if (Character.IsPartyMemberSelected && Character.PtMemberHpPercent <= SavedData.RecoverySkillPecent)
                    SetState(EState.Healing);
                else if (++_currentMemberIndex < Character.PartySize)
                    CheckPartyHealing();
                else
                    FinishState();
            });
        }
    }
}
