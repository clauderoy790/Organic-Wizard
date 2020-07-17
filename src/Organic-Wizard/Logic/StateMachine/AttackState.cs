using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public class AttackState : CharacterState
    {
        TextBox _txtAtkKey = null;
        TextBox _txtAtkDelay = null;
        int _attackKey = 0;
        int _attackDelay = 0;

        public AttackState(TextBox txtAtkKey,TextBox txtAtkDelay,Action onEnterTimerDone) : base()
        {
            _txtAtkKey = txtAtkKey;
            _txtAtkDelay = txtAtkDelay;
            SetEnterTimerDoneAction(onEnterTimerDone);
            SetKeys();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            StartEnterTimer(_attackDelay + 100);
            SendKeys.SendWait("z");
            Thread.Sleep(_attackDelay);
            SendKeys.SendWait(_attackKey+"");
            Thread.Sleep(50);
            SendKeys.SendWait(_attackKey+"");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            SetKeys();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        void SetKeys()
        {
            if (!int.TryParse(_txtAtkKey.Text, out _attackKey) ||
                !int.TryParse(_txtAtkDelay.Text, out _attackDelay))
            {
                throw new Exception("Invalid Attack Key or Delay");
            }
        }
    }
}
