using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    class RecoveryCheckState : State
    {
        private Action<bool> _onRecoveryNeeded;
        private ResourceBar _hpBar;
        private ResourceBar _mpBar;
        private int _hpRecoveryPercent;
        private int _mpRecoveryPercent;
        private Action _hpPercentUpdate;
        private Action _mpPercentUpdate;

        public RecoveryCheckState( Action<bool> onRecoveryNeeded)
        {
            _hpBar = new ResourceBar(28, 216, 36, ResourceBar.EResourceType.Hp);
            _mpBar = new ResourceBar(28, 216, 53, ResourceBar.EResourceType.Mana);

            _onRecoveryNeeded = onRecoveryNeeded;
            _hpRecoveryPercent = 0;
            _mpRecoveryPercent = 0;
        }


        public override void OnEnter()
        {
           
        }

        public override void OnUpdate()
        {
            if (SavedData.HpRecovery && _hpBar.GetCurrentPercent() < _hpRecoveryPercent)
            {
                _onRecoveryNeeded?.Invoke(true);
            }
            else if (SavedData.MpRecovery && _mpBar.GetCurrentPercent() < _mpRecoveryPercent)
            {
                _onRecoveryNeeded?.Invoke(false);
            }
            Console.WriteLine("HP %: "+_hpBar.GetCurrentPercent()+", MP %: "+_mpBar.GetCurrentPercent());
        }
    }
}
