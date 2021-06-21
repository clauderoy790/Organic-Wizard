using Organic_Wizard.Logic;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Organic_Wizard
{
    public class HealingState : CharState
    {
        Dictionary<int,int> _skillsMpUsed;
        int _healSkill = 0;
        public HealingState(StateMachine sm) : base(sm)
        {
        }

        public override EState GetName()
        {
            return EState.Healing;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (_skillsMpUsed == null)
                InitSkills();

            SetHealingSkill();
            
            if (_healSkill != Constants.NONE)
                Heal();
            else
                FinishState();
        }

        private void Heal()
        {
            if (Character.HpPercent >= SavedData.RecoverySkillPecent)
            {
                FinishState();
                return;
            }
            
            SendKey(_healSkill, Constants.HEALING_ANIMATION_DELAY, Heal);
        }

        private void SetHealingSkill()
        {
            _healSkill = Constants.NONE;
            int count = SavedData.GetHealSkillsCount();
            for (int i = 0; i < count; i++)
            {
                _healSkill = SavedData.GetHealSkillAtPos(i);
                if (_healSkill != Constants.NONE)
                    break;
            }
        }

        private void InitSkills()
        {
            _skillsMpUsed = new Dictionary<int, int>();
            int count = SavedData.GetHealSkillsCount();
            for(int i =0; i< count;++i)
            {
                int skill = SavedData.GetHealSkillAtPos(i);
                if (skill != Constants.NONE)
                    _skillsMpUsed[skill] = 0;
            }
        }
    }
}
