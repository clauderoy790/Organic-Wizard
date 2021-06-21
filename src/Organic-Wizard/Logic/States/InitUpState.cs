using Shared;
using System.Drawing;
using Vector = System.Windows.Vector;

namespace Organic_Wizard.Logic.States
{
    public class InitUpState : CharState
    {
        double ARRIVED_AT_START_POS_TRESHOLD = 1.5;
        double prevDist = 0;
        Vector startPos;

        public InitUpState(StateMachine sm): base(sm)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            Character.PositionChanged -= OnPositionChanged;
            Character.PositionChanged += OnPositionChanged;
            prevDist = double.MaxValue;
            startPos = new Vector(0, 0);
            DoStateAction(EInitState.CalculateUp);
        }

        private void DoStateAction(EInitState state)
        {
            switch(state)
            {
                case EInitState.CalculateUp:
                    PerformClickMove(ScreenPosition.Up.ToPoint());
                    break;
                case EInitState.MoveBack:
                    MoveBack();
                    break;
            }
        }

        private void MoveBack()
        {
            Vector destinationDirection = startPos - Character.Position;
            destinationDirection.Normalize();
            Point screenPoint = ScreenPosition.GetPointForDirection(destinationDirection);
            PerformClickMove(screenPoint);
        }

        private void PerformClickMove(Point screenPoint)
        {
            KeyUtils.SendCharUp('w');
            ScreenPosition.StopCalculateDirection();
            ActionManager.SendDelayedAction(0.1f, () =>
            {
                ClickAt(screenPoint, () =>
                {
                    KeyUtils.SendCharDown('w');
                });
            });
        }

        public override void OnLeave()
        {
            base.OnLeave();
            Character.PositionChanged -= OnPositionChanged;
            Character.PositionChanged -= OnPositionChangedMoveBack;
            KeyUtils.SendCharUp('w');
        }

        private void OnPositionChanged(Vector newPos)
        {
            if (startPos.IsZero())
                startPos = newPos;

            Vector direction = newPos - startPos;
            
            if (direction.Length >= ScreenPosition.UP_DIRECTION_LENGTH)
            {
                ScreenPosition.SetUpDirection(direction);
                Character.PositionChanged -= OnPositionChanged;
                Character.PositionChanged -= OnPositionChangedMoveBack;
                Character.PositionChanged += OnPositionChangedMoveBack;
                DoStateAction(EInitState.MoveBack);
            }
        }

        private void OnPositionChangedMoveBack(Vector newPos)
        {
            double dist = (newPos - startPos).Length;
            if (dist <= ARRIVED_AT_START_POS_TRESHOLD || dist > prevDist)
            {
                FinishState(); //Arrived back at start pos
            }
            prevDist = dist;
        }

        public override EState GetName()
        {
            return EState.InitUpPosition;
        }

        private enum EInitState
        {
            CalculateUp,
            MoveBack
        }
    }

}
