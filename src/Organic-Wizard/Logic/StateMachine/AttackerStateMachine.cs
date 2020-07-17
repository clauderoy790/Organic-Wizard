using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public class AttackerStateMachine : StateMachine
    {
        static int WOLF_DURATION = 120 * 1000;
        
        WolfState _wolfState = null;
        AttackState _attackState = null;
        CheckBox _chkUseWolf = null;
        Timers.Timer _wolfTimer = null;
        bool _reWolf = false;

        public  AttackerStateMachine()
        {
            _wolfTimer = new Timers.Timer();
            _wolfTimer.Elapsed += OnWolfTimerTick;
            _wolfTimer.Interval = WOLF_DURATION + WolfState.WOLF_ANIMATION_DELAY + 1000;
            _wolfTimer.AutoReset = false;

            //TODO
            //_chkUseWolf = chkUseWolf;
            //_wolfState = new WolfState(txtWolfKey, OnWolfAnimationDone);
            //_attackState = new AttackState(txtAtkKey, txtAtkDelay, OnAttackDone);
        }

        public override void Start()
        {
            base.Start();
            if (_chkUseWolf.Checked)
                Wolf();
            else
                SetState(_attackState);
        }

        public override void Stop()
        {
            base.Stop();
            _wolfTimer.Stop();
        }

        private void OnWolfTimerTick(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("wolf timer tick!");
            _wolfTimer.Stop();
            if (_chkUseWolf.Checked)
                _reWolf = true;
        }

        void Wolf()
        {
            Console.WriteLine("Wolf, rewolf:"+_reWolf);
            _reWolf = false;
            SetState(_wolfState);
        }

        void OnWolfAnimationDone()
        {
            Console.WriteLine("wolfAnimationDone");
            _wolfTimer.Stop();
            if (_chkUseWolf.Checked)
            {
                _wolfTimer.Start();
            }
            SetState(_attackState);
            _reWolf = false;
        }

        void OnAttackDone()
        {
            if (_reWolf && _currentState != _wolfState)
                Wolf();
            else
                SetState(_attackState);
        }
    }
}
