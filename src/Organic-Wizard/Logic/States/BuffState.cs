using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Organic_Wizard.Logic;
using Organic_Wizard.Logic.States;
using Shared;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public class BuffState : CharState
    {
        CheckForBuffState _checkState = null;
        List<BuffInfo> _buffsToApply = null;
        BuffInfo _currentBuff = null;
        bool _buffingInProgress = false;
        int _startingIndex = -1;

        public BuffState(StateMachine sm) : base(sm)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _buffingInProgress = false;
            _currentBuff = null;
            if (_checkState == null)
            {
                _checkState = (CheckForBuffState)_sm.GetState(EState.CheckForBuff);
                _partyState = (CheckForPartyState)_sm.GetState(EState.CheckForParty);
            }

            if (!SavedData.UseBuffSkills)
            {
                FinishState();
                return;
            }
            else
            {
                SkillBar.InitSkillsInfo();
                _buffsToApply = _checkState.Buffs.Where((b) => { return b.NeedsRefresh; }).ToList();
                _buffsToApply.Sort(SortBuffs);
                ApplyFirstOrFinish();
            }
        }

        private int SortBuffs(BuffInfo b1, BuffInfo b2)
        {
            if (!b1.UseOnParty && b2.UseOnParty)
                return -1;
            else if (!b2.UseOnParty && b1.UseOnParty)
                return 1;
            else return 0;
        }

        private void ApplyFirstOrFinish()
        {
            _buffingInProgress = false;
            if (_buffsToApply.Count > 0)
            {
                _partyState.SetPartyIndex();
                _currentBuff = _buffsToApply[0];
                _buffsToApply.RemoveAt(0);
                if (Character.IsInParty)
                {
                    SetValidIndexAndStart();
                }
                else
                    ExecuteBuff();
            }
            else
                FinishState();
        }

        private void SetValidIndexAndStart()
        {
            _partyState.SetPartyIndex();
            _startingIndex = Character.SelectedPartyMemberIndex;
            if (Character.IsInParty && _startingIndex == -1)
            {
                SendTab(SetValidIndexAndStart);
                return;
            }
            if (_startingIndex != -1)
                _partyState.ClickPartyMember(_startingIndex);
            ExecuteBuff();
        }

        private void ExecuteBuff()
        {
            _partyState.SetPartyIndex();
            if (AtEndOfCurrentBuff())
            {
                CurrentBuffRefreshed();
                ApplyFirstOrFinish();

                if (_buffsToApply.Count == 0) //Done buffing
                {
                    return;
                }
            }
            else
            {
                SendKey(_currentBuff.Skill, Constants.BUFF_ANIMATION_DELAY, () =>
                {
                    if (!_currentBuff.UseOnParty || !Character.IsInParty)
                    {
                        CurrentBuffRefreshed();
                        ApplyFirstOrFinish();
                    }
                    else
                    {
                        BuffParty();
                    }
                });
            }
        }

        private void BuffParty()
        {
            _partyState.SetPartyIndex();
            Debug.Log($"buff in progress: {_buffingInProgress},  starting index: {_startingIndex}, selected member  inedx: {Character.SelectedPartyMemberIndex}");
            //if we're not  in party or we back at the start of the list.
            if (!Character.IsInParty || AtEndOfCurrentBuff())
            {
                CurrentBuffRefreshed();
                ApplyFirstOrFinish();
            }
            else
            {
                _buffingInProgress = true;
                SendTab(() =>
                {
                    ExecuteBuff();
                });
            }
        }

        private bool AtEndOfCurrentBuff()
        {
            return _buffingInProgress && Character.SelectedPartyMemberIndex == _startingIndex;
        }

        private void CurrentBuffRefreshed()
        {
            _currentBuff.NeedsRefresh = false;
            _currentBuff.Timer.Restart();
            _currentBuff = null;
        }

        public override EState GetName()
        {
            return EState.Buffing;
        }
    }
}
