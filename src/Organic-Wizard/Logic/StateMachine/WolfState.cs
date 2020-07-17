using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Organic_Wizard
{
    public class WolfState : CharacterState
    {
        public static int WOLF_ANIMATION_DELAY = 3000;

        TextBox _txtWolfKey = null;
        int _wolfKey = 0;

        public WolfState(TextBox txtWolfKey,Action onEnterTimerDone) : base()
        {
            _txtWolfKey = txtWolfKey;
            _wolfKey = GetWolfKey();
            SetEnterTimerDoneAction(onEnterTimerDone);
        }


        public override void OnEnter()
        {

            base.OnEnter();
            StartEnterTimer(WOLF_ANIMATION_DELAY + 100);
            SendKeys.SendWait(_wolfKey+"");
            SendKeys.SendWait(_wolfKey + "");
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            _wolfKey = GetWolfKey();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        int GetWolfKey()
        {
            int wolfkey = 0;
            if (!int.TryParse(_txtWolfKey.Text, out wolfkey))
            {
                throw new Exception("Invalid Wolf Key");
            }

            return wolfkey;
        }
    }
}
