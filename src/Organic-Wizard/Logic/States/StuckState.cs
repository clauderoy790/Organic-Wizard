using Organic_Wizard.Data;
using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic.States
{
    public class StuckState : CharState
    {
        const double SECONDS_BEFORE_STUCK = 1;
        const double BASE_MOVE_LENGTH = 2;
        const double MOVE_INCREMENT = 1.5;
        private Vector initialDirection;
        private Vector startPos;
        private EDirection direction;
        private CTimer timerStuck;
        private double movementLength = 0;
        private Action onMoveCompleted = null;

        public StuckState(StateMachine sm) : base(sm)
        {
            timerStuck = new CTimer();
            timerStuck.IntervalSeconds = SECONDS_BEFORE_STUCK;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            initialDirection = ScreenPosition.Direction;
            movementLength = BASE_MOVE_LENGTH;
            onMoveCompleted = null;
            Move(EDirection.Back);
        }

        private void OnMove(Vector newPos)
        {
            timerStuck.Restart();
        }

        private void Move(EDirection direction,Action onCompleted = null)
        {
            this.onMoveCompleted = onCompleted;
            this.direction = direction;
            KeyUtils.SendCharUp('w');
            startPos = Character.Position;
            double angle = GetAngleForDirection();
            Point newPoint = ScreenPosition.RotateDirection(initialDirection,angle);
            ActionManager.SendDelayedAction(0.25f, () =>
            {
                ClickAt(newPoint, () =>
                {
                    if (!IsActive)
                        return;
                    timerStuck.Start();
                    ScreenPosition.SetClickPoint(newPoint);
                    KeyUtils.SendCharDown('w');
                    Character.PositionChanged -= OnMove;
                    Character.PositionChanged += OnMove;
                });
            });
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector diff = Character.Position - startPos;
            if (diff.Length >= movementLength || !timerStuck.Enabled)
            {
                if (diff.Length >= movementLength)
                {
                    MovementCompleted();
                }
                else
                {
                    StuckInMovement();
                }
            }
        }

        private void IncrementMoveLength()
        {
            movementLength *= MOVE_INCREMENT;
        }

        private void StuckInMovement()
        {
            Debug.Log($"StuckInMovement {direction}");
            switch (direction)
            {
                case EDirection.Left:
                    Debug.Log("Unstuck");
                    Move(EDirection.Right,IncrementMoveLength);
                    break;
                case EDirection.Right:
                case EDirection.Back:
                    FinishState(); //Technically should not happen.
                    break;
            }
        }

        private void MovementCompleted()
        {
            Debug.Log($"movement completed {direction}");
            onMoveCompleted?.Invoke();
            onMoveCompleted = null;

            switch(direction)
            {
                case EDirection.Left:
                    Debug.Log("UNSTUCK");
                    SetState(EState.Move); //Unstuck
                    break;
                case EDirection.Right:
                    Move(EDirection.Back);
                    break;
                case EDirection.Back:
                    Move(EDirection.Left);
                    break;
            }
        }

        private double GetAngleForDirection()
        {
            double angle = 0;
            switch (direction)
            {
                case EDirection.Back:
                    angle = 180;
                    break;
                case EDirection.Left:
                    angle = -90;
                    break;
                case EDirection.Right:
                    angle = 90;
                    break;
            }

            return angle;
        }

        public override void OnLeave()
        {
            base.OnLeave();
            Character.PositionChanged -= OnMove;
        }

        public override EState GetName()
        {
            return EState.Stuck;
        }
    }
}
