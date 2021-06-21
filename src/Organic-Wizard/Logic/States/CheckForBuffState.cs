using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Organic_Wizard.Logic.States
{
    public class CheckForBuffState : CharState
    {
        List<BuffInfo> _buffs;

        public List<BuffInfo> Buffs { get { return _buffs; } }

        public CheckForBuffState(StateMachine sm) : base(sm)
        {
            LogicEngine.Stopped += OnStopped;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (_buffs == null)
                Init();

            if (!SavedData.UseBuffSkills)
            {
                FinishState();
                return;
            }

            bool needsRefresh = false;
            foreach (var buff in _buffs)
            {
                if (buff.NeedsRefresh)
                {
                    needsRefresh = true;
                    break;
                }
            }

            if (needsRefresh)
            {
                SetState(EState.Buffing);
            }
            else
            {
                FinishState();
            }
        }

        void Init()
        {
            _buffs = new List<BuffInfo>();
            var buffs = SavedData.GetBuffSkills();
            foreach (var buff in buffs)
            {
                if (buff.IsValid)
                {
                    CTimer timer = new CTimer();
                    timer.AutoReset = false;
                    timer.Interval = SkillBar.GetCooldownMinutesForSkill(buff.Skill) * 60 * 1000;
                    timer.Elapsed += OnBuffExpired;
                    BuffInfo info = new BuffInfo()
                    {
                        NeedsRefresh = true,
                        Skill = buff.Skill,
                        Timer = timer,
                        UseOnParty = buff.UseOnParty
                    };
                    _buffs.Add(info);
                }
            }
        }

        private void OnBuffExpired(object sender, ElapsedEventArgs e)
        {
            var timer = (Timer)sender;
            timer.Stop();
            foreach (var buff in _buffs)
            {
                if (buff.Timer.Source == timer)
                {
                    buff.NeedsRefresh = true;
                    _sm.AddPendingState(EState.CheckForBuff);
                }
            }
        }

        private void OnStopped()
        {
            if (_buffs == null)
                return;

            foreach (var buff in _buffs)
            {
                buff.Timer.Stop();
                buff.NeedsRefresh = true;
            }
        }

        public override EState GetName()
        {
            return EState.CheckForBuff;
        }
    }
}
