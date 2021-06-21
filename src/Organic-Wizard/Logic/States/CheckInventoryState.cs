using Shared;
using SharpCompress.Compressors.PPMd;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic.States
{
    public class CheckInventoryState : CharState
    {
        const float OPEN_INV_ANIM_TIME = 0.7f;
        const int MAX_ATTEMPTS = 5;
        int nbAttempts = 0;
        bool opened = false;

        public CheckInventoryState(StateMachine sm) : base(sm)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            nbAttempts = 0;
            opened = false;
            TryOpenInv();
        }

        private void TryOpenInv()
        {
            if (nbAttempts++ > MAX_ATTEMPTS)
            {
                FinishState();
                return;
            }

            opened = IsOpenend();
            if (!opened)
            {
                if (nbAttempts > 1)
                {
                    SendKey(Keyboard.ScanCodeShort.RETURN, OPEN_INV_ANIM_TIME, SendITryOpen);
                }
                else
                {
                    SendITryOpen();
                }
            }
            else
            {
                Inventory.Process();
                SendKey("i", OPEN_INV_ANIM_TIME, FinishState);
            }
        }

        void SendITryOpen()
        {
            SendKey("i", OPEN_INV_ANIM_TIME, TryOpenInv);
        }

        private bool IsOpenend()
        {
            Int32Rect invRec = new Int32Rect(624, 70, 400, 50);
            Color bottomLeftColor = ColorUtils.GetColorAt(6, 745);
            PointGroupColorDiff invPg = new PointGroupColorDiff(invRec,
                new Point(0, 0),
                new Point(0, 1),
                new Point(88, 6),
                new Point(292, 6),
                new Point(352, 13)
                );

            bool opened = false;
            using (invPg)
            {
                invPg.Tolerance = 3;
                invPg.AddDiff(0, 1, 33);
                invPg.AddDiff(0, 2, 12);
                invPg.AddDiff(2, 3, 0);
                invPg.AddDiff(2, 4, 32, 15);
                invPg.AddDiff(2, bottomLeftColor, 159);
                opened = invPg.FindExact();
            }
            return opened;
        }

        public override EState GetName()
        {
            return EState.CheckInventory;
        }
    }
}
