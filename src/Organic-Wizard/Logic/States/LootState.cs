using Shared;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic.States
{
    public class LootState : CharState
    {
        const int PIXEL_INCREMENT = 5;
        const int NB_SLOTS = 4;
        const int SLOT_HEIGHT = 52;
        const int SLOT_WIDTH = 48;
        PointGroupColorDiff group;
        bool[] slots = new bool[NB_SLOTS];
        int currentSlot = 0;

        public LootState(StateMachine sm) : base(sm)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            ResetSlots();
            InitGroup();
            using (group)
            {
                Point topLeft = group.FindExactPoint();

                if (!topLeft.IsZero())
                {
                    topLeft.Offset(1, 0);
                    Debug.Log($"bar is at: {topLeft}");
                    Point relativeTopLeft = group.GetRelativePoint(topLeft);
                    for (int slot = 0; slot < NB_SLOTS; slot++)
                        slots[slot] = IsSlotEmpty(relativeTopLeft, slot);

                    for (int i = 0; i < slots.Length; i++)
                        Debug.Log($"slot {i} is empty: {slots[i]}");

                    currentSlot = 0;
                    Loot(topLeft);
                }
                else
                {
                    Debug.Log("Failed to find top bar");
                    FinishState();
                }
            }
        }

        private void Loot(Point topLeft)
        {
            Debug.Log("LOOT");
            if (currentSlot == NB_SLOTS)
            {
                Debug.Log("DONZO");
                FinishState();
            }
            else if (!slots[currentSlot])
            {
                int x = topLeft.X + SLOT_WIDTH / 2;
                int y = topLeft.Y + SLOT_HEIGHT * currentSlot;
                y += SLOT_HEIGHT / 2;
                Point center = new Point(x, y);
                ++currentSlot;
                ClickAt(center, () => { Loot(topLeft); });
            }
            else
            {
                ++currentSlot;
                Loot(topLeft);
            }
        }

        private bool IsSlotEmpty(Point topLeft, int index)
        {
            int slotHeightOffset = -8;
            int minDiff = 40;
            int startX = topLeft.X;
            int startY = topLeft.Y + index * SLOT_HEIGHT;

            for (int x = 0; x < SLOT_WIDTH; x += PIXEL_INCREMENT)
            {
                for (int y = 0; y < SLOT_HEIGHT + slotHeightOffset; y += PIXEL_INCREMENT)
                {
                    Color col = group.LockB.GetPixel(startX + x, startY + y);
                    int diff = ColorUtils.ColorDiff(col, Color.Black);
                    if (diff > minDiff)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void InitGroup()
        {
            Int32Rect rect = new Int32Rect(100, 100, 800, 600);
            group = new PointGroupColorDiff(rect,
                new Point(0, 0),
                new Point(1, -1),
                new Point(48, 0),
                new Point(0, 199),
                new Point(51, 199),
                new Point(1, 0)
            );
            group.Tolerance = 3;

            Color bottomUI = ColorUtils.GetColorAt(464, 753);
            group.AddDiff(0, 5, 42);
            group.AddDiff(0, bottomUI, 2);
            group.AddDiff(0, 1, 0);
            group.AddDiff(0, 2, 0);
            group.AddDiff(0, 3, 0);
            group.AddDiff(0, 4, 0);
        }

        private void ResetSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = false;
            }
        }

        public override EState GetName()
        {
            return EState.Loot;
        }
    }
}
