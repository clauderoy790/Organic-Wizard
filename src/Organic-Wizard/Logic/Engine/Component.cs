using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    public abstract class Component
    {
        private bool _active = true;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;

                if (_active)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnUpdate()
        {

        }
    }
}
