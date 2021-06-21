using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnLeave();

    }
}
