using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard
{
    public class ActionManager
    {
        static ActionManager _intance = null;

        public static ActionManager Instance 
        { 
            get
            {
                if (_intance == null)
                    _intance = new ActionManager();
                return _intance;
            }
        }

        private Dictionary<Action, Action> _actionlist;



    }
}
