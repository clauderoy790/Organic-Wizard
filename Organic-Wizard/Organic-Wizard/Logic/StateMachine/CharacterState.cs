using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    public class CharacterState : State
    {
        public override void OnEnter()
        {
            
        }

        public override void OnUpdate()
        {
            Recover();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        private void Recover()
        {
            
        }
    }
}
