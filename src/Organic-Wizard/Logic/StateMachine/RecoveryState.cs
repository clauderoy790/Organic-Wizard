using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Organic_Wizard
{
    public class RecoveryState: State
    {
        private static int RECOVERY_DELAY = 500;

        public Action OnRecoveryDone { get; set; }

        public bool UseHpPot
        {
            get;
            set;
        }

        public override void OnEnter()
        {
            StartEnterTimer(RECOVERY_DELAY, OnAnimDone);
            int curPotKey = UseHpPot ? SavedData.HpRecoverySkill : SavedData.MpRecoverySkill;
            if (curPotKey == -1)
                return;

            SendKeys.SendWait("{" + curPotKey + " down}");
            Thread.Sleep(50);
            SendKeys.SendWait("{" + curPotKey + " up}");
        }

        public override void OnUpdate()
        {
            
        }

        void OnAnimDone()
        {
            OnRecoveryDone?.Invoke();
        }
    }
}
