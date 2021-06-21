using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic
{
    public class Inventory
    {
        const int WIDTH = 7;
        const int HEIGHT = 4;
        const int SLOT_SIZE = 49;
        const int PIXEL_INCREMENT = 5;
        static readonly Int32Rect screenshotRect = new Int32Rect(657, 419, 343, 200);

        static bool[,] inventorySlots;

        public static bool IsFull { get; private set; }

        static Inventory()
        {
            Clear();
        }

        public static void Clear()
        {
            inventorySlots = new bool[WIDTH, HEIGHT];
            IsFull = false;
        }

        public static void Process()
        {
            PointGroup pg = new PointGroup(
                new Point(6, 10),
                new Point(35, 33),
                new Point(12, 37)
            );

            Color emptyInvColor = ColorUtils.GetColorAt(6, 745);
            int minDiffBeforeNotEmpty = 40;
            LockBitmap lockb = ImgUtils.GetLockBitmap(screenshotRect);
            lockb.LockBits();
            IsFull = true;
            for (int x = 0; x < inventorySlots.GetLength(0); x++)
            {
                int y = 0;
                for (y = 0; y < inventorySlots.GetLength(1); y++)
                {
                    if (inventorySlots[x, y])
                        continue;

                    bool empty = IsSlotEmpty(x, y, lockb, emptyInvColor, minDiffBeforeNotEmpty);
                    IsFull &= !empty;
                    if (!empty)
                        Debug.Log($"{x},{y} not empty");
                    inventorySlots[x, y] = !empty;
                    pg.OffsetY(SLOT_SIZE);
                }
                pg.Offset(SLOT_SIZE, -y * SLOT_SIZE);
            }
            lockb.UnlockBits();
            Debug.Log("inv full:" + IsFull);
        }

        private static bool IsSlotEmpty(int x, int y, LockBitmap lockb, Color compare, int minColorDiff)
        {
            x *= SLOT_SIZE;
            y *= SLOT_SIZE;

            for (int offsetX = 0; offsetX < SLOT_SIZE; offsetX += PIXEL_INCREMENT)
            {
                for (int offsetY = 0; offsetY < SLOT_SIZE; offsetY += PIXEL_INCREMENT)
                {
                    Color col = lockb.GetPixel(x + offsetX, y + offsetY);
                    int diff = ColorUtils.ColorDiff(col, compare);
                    if (diff > minColorDiff)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
