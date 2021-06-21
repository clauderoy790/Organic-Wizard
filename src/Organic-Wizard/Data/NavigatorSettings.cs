using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard.Data
{
    public class NavigatorSettings
    {
        public double InitialUpDirectionLength { get; set; }
        public double FinalDesinationLengthTreshold { get; set; }
        public double MinAngleBeforeAdjustRotation { get; set; }
        public double MinSamePathLengthBeforeAdjustRotation { get; set; }
        public double DistanceBeforeAlwaysAdjust { get; set; }  
        public double LenghtBeforeUpdateDirection { get; set; }  
        public double TimeBeforeStuck { get; set; } 
    }
}
