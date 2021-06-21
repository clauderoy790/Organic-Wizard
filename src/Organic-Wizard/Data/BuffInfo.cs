using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared;

namespace Organic_Wizard.Logic
{
    public class BuffInfo
    {
        public int Skill { get; set; }
        public bool NeedsRefresh { get; set; }
        public bool UseOnParty { get; set; }
        public CTimer Timer { get; set; }
    }
}