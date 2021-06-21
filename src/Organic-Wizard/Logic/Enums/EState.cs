using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard
{
    public enum EState
    {
        Idle,
        Attacking,
        Sitting,
        CheckForParty,
        Buffing,
        CheckForHeal,
        CheckForBuff,
        Healing,
        Move,
        Stuck,
        TryOpenChest,
        Loot,
        InitUpPosition,
        CheckInventory
    }
}
