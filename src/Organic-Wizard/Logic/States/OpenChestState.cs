using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting;
using Shared;
using Vector = System.Windows.Vector;

namespace Organic_Wizard.Logic.States
{
    public class OpenChestState : CharState
    {
        const double MAX_CLICK_LENGTH_PX = 100;
        const double CLICK_OFFSET_INCREMET = 5;
        const double INITIAL_CLICK_OFFSET = 20;
        const float TIME_OPEN_CHEST_AFTER_UNSELECTED = 2f;

        Vector initClickPos;
        Vector clickPos;
        Vector direction;
        CTimer timer;
        bool timerStarted = false;

        public OpenChestState(StateMachine sm) : base(sm)
        {
            timer = new CTimer();
            timer.IntervalSeconds = TIME_OPEN_CHEST_AFTER_UNSELECTED;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            timerStarted = false;
            SetInitialClickPos();
            clickPos = initClickPos;
            direction = clickPos - ScreenPosition.Center;
            direction.Normalize();
            TryOpenChest();
        }

        public override void OnLeave()
        {
            base.OnLeave();
            timer.Stop();
        }

        private void SetInitialClickPos()
        {
            Vector screenPoint = ScreenPosition.GetPointForDirection(Character.Direction).ToVector();
            initClickPos = Maths.Translate(screenPoint, screenPoint, INITIAL_CLICK_OFFSET).ToIntVector();
        }

        private void TryOpenChest()
        {
            
            if (!IsActive)
                return;

            if (OpenTimeOver())
            {
                FinishState();
                return;
            }

            ClickAt(clickPos, () =>
            {
                Debug.Log($"click at: {clickPos}");
                if (!IsActive)
                {
                    Debug.Log("not active");
                    return;
                }
                else if (OpenTimeOver())
                {
                    FinishState();
                    return;
                }
                else if (IsChestOpen())
                {
                    Debug.Log("chest is sopen");
                    timer.Stop();
                    SetState(EState.Loot);
                }
                else
                {
                    clickPos = Maths.Translate(clickPos, direction, CLICK_OFFSET_INCREMET).ToIntVector();
                    Debug.Log($"click pos: {clickPos}, direction:{direction}");
                    if ((clickPos - initClickPos).Length >= MAX_CLICK_LENGTH_PX)
                    {
                        Debug.Log("reset it to: " + initClickPos);
                        clickPos = initClickPos;
                    }

                    TryOpenChest();

                }
            }, EClickType.Right);
        }

        private bool OpenTimeOver()
        {
            if (!Character.IsMonsterSelected)
            {
                if (!timerStarted)
                {
                    timer.Restart();
                    timerStarted = true;
                }
                else if (!timer.Enabled)
                {
                    Debug.Log("OVERR");
                    return true;
                }
            }
            return false;
        }

        private bool IsChestOpen()
        {
            int tolerance = 5;
            int xOffset = 5;
            int width = 10;
            List<Color> colors = new List<Color>();

            Vector rightOfClick = new Vector(clickPos.X + xOffset, clickPos.Y);
            for (int x = 0; x < width; x++)
            {
                colors.Add(ColorUtils.GetColorAt(rightOfClick));
                rightOfClick.X += 1;
            }

            Color avg = ColorUtils.GetAverageColor(colors);
            int diffWithBlack = ColorUtils.ColorDiff(Color.Black, avg);
            if (diffWithBlack <= tolerance)
                Debug.Log("CHESt IS OPEN");
            else
                Debug.Log("chest not open");

            return diffWithBlack <= tolerance;
        }

        public override EState GetName()
        {
            return EState.TryOpenChest;
        }
    }
}
