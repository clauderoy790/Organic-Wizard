using Organic_Wizard.Data;
using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic.States
{
    public class MoveState : CharState
    {
        NavigatorSettings settings;
        Point previousClickPoint;
        Vector destination;
        bool isMoving = false;
        bool isStuck = false;
        double lastAngle = 0;
        double distanceToDestination = 0;
        CTimer timerStuck;
        EState previousState;

        public MoveState(StateMachine sm) : base(sm)
        {
            timerStuck = new CTimer(100);
            settings = new NavigatorSettings()
            {
                FinalDesinationLengthTreshold = 2,
                MinAngleBeforeAdjustRotation = 4,
                MinSamePathLengthBeforeAdjustRotation = 4,
                DistanceBeforeAlwaysAdjust = 2,
                LenghtBeforeUpdateDirection = 4,
                TimeBeforeStuck = 1,
            };
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Character.PositionChanged += OnPositionChanged;
            if (_sm.PreviousState == EState.Stuck)
            {
                StartFromStuck();
            }
            else
            {
                Start();
            }
        }

        private void StartFromStuck()
        {
            if (!IsCloseToDestination())
                PerformClickMove(previousClickPoint);
            else
                ArrivedAtDestination();
        }

        private void Start()
        {
            ResetVars();
            Vector? moveTo = GetParam<Vector?>("destination");

            if (!moveTo.HasValue)
                throw new ArgumentException("Need to have a 'destination' vector passed data.");

            previousState = _sm.PreviousState;
            destination = moveTo.Value;
            Debug.Log($"set destination to :{destination}");
            SetDistanceToDestination();
            if (IsCloseToDestination())
            {
                ArrivedAtDestination();
            }
            else
            {
                PerformClickMove(GetScreenPoint());
            }
        }

        private void ArrivedAtDestination()
        {
            FinishState(); //Done
            _sm.SetState(previousState);
            destination = new Vector(0, 0);
            Console.WriteLine("ARRIVED AT DESTINATION: "+previousState);
        }

        private void PerformClickMove(Point screenPoint)
        {
            Debug.Log("Perform click move: " + screenPoint);
            previousClickPoint = screenPoint;
            KeyUtils.SendCharUp('w');
            ScreenPosition.StopCalculateDirection();
            isMoving = false;
            ActionManager.SendDelayedAction(0.1f, () =>
            {
                ClickAt(screenPoint, () =>
                {
                    isMoving = true;
                    KeyUtils.SendCharDown('w');
                    ScreenPosition.SetClickPoint(screenPoint);
                    isStuck = false;
                    timerStuck.IntervalSeconds = settings.TimeBeforeStuck;
                    timerStuck.Elapsed += OnStuck;
                    timerStuck.Restart();
                });
            });
        }

        private void OnStuck(object sender, ElapsedEventArgs e)
        {
            isStuck = true;
        }

        private void ResetVars()
        {
            isStuck = false;
            destination = new Vector(0, 0);
            isMoving = false;
            lastAngle = 0;
            distanceToDestination = 0;
            timerStuck.Stop();
        }

        private void SetDistanceToDestination()
        {
            distanceToDestination = (destination - Character.Position).Length;
        }

        private bool IsCloseToDestination()
        {
            return distanceToDestination <= settings.FinalDesinationLengthTreshold;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            SetDistanceToDestination();

            if (IsCloseToDestination())
            {
                ArrivedAtDestination();
            }
            else if (isStuck)
            {
                _sm.SetState(EState.Stuck);
            }
            else if (isMoving && ShouldReajustPosition())
                PerformClickMove(GetScreenPoint());
        }

        public override void OnLeave()
        {
            base.OnLeave();
            Character.PositionChanged -= OnPositionChanged;
            KeyUtils.SendCharUp('w');
            ScreenPosition.StopCalculateDirection();
            timerStuck.Elapsed -= OnStuck;
            timerStuck.Stop();
        }

        private bool ShouldReajustPosition()
        {
            Vector fromDestination = destination - Character.Position;
            fromDestination.Normalize();
            Vector fromStartClick = Character.Position - ScreenPosition.StartPositionWorld;

            if (fromStartClick.Length >= settings.MinSamePathLengthBeforeAdjustRotation)
            {
                double angle = Maths.Angle(fromStartClick, fromDestination);
                double angleDiff = Math.Abs(lastAngle - angle);
                bool shouldReadjust = angleDiff >= settings.MinAngleBeforeAdjustRotation;
                lastAngle = angle;
                return shouldReadjust;
            }

            return false;
        }

        private void OnPositionChanged(Vector newPos)
        {
            timerStuck.Restart();
        }

        private Point GetScreenPoint()
        {
            Vector destinationDirection = destination - Character.Position;
            destinationDirection.Normalize();
            Point screenPoint = ScreenPosition.GetPointForDirection(destinationDirection);
            return screenPoint;
        }

        public override EState GetName()
        {
            return EState.Move;
        }
    }
}