using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mono.CompilerServices.SymbolWriter;
using Shared;
using Vector = System.Windows.Vector;

namespace Organic_Wizard.Logic
{
    public class ScreenPosition : Component
    {
        public const int UP_DIRECTION_LENGTH = 6;
        public static readonly Vector Up = new Vector(512, 400);
        public static readonly Vector Center = new Vector(512, 460);
        private static readonly Vector toRotate = Up - Center;
        private static ScreenPosition instance;
        private Vector upDirection;
        private Vector clickPoint;
        private Vector startPosWorld;
        private Vector direction;

        public static Vector StartPositionWorld { get { return instance.startPosWorld; } }

        public static Vector Direction { get { return instance.direction; } }

        public override void onAttach()
        {
            base.onAttach();
            clickPoint = new Vector(0, 0);
            direction = new Vector(0, 0);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            instance = this;
            Character.PositionChanged -= OnPositionChanged;
            Character.PositionChanged += OnPositionChanged;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Character.PositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector newPos)
        {
            direction = newPos - startPosWorld;
        }

        public static void SetClickPoint(Point screenPoint)
        {
            if (instance == null)
                throw new InvalidOperationException("Instance is not initialized, cannot set click point.");
            else if (screenPoint.IsZero())
                throw new ArgumentException("screenPoint cannot be zero");

            instance.Active = true;
            instance.clickPoint = new Vector(screenPoint.X, screenPoint.Y);
            instance.startPosWorld = Character.Position;
            instance.direction = instance.clickPoint - Center;
        }

        public static void SetUpDirection(Vector direction)
        {
            if (direction.Length < UP_DIRECTION_LENGTH)
                throw new Exception($"direction must be of length {UP_DIRECTION_LENGTH}.");

            direction.Normalize();
            instance.upDirection = direction;
        }

        public static Point RotateDirection(Vector screenDirection, double angle)
        {
            if (screenDirection.Length == 0)
                throw new ArgumentException("ScreenDirection cannot be of length 0.");
            Vector rotatedDir = screenDirection.Rotate(angle);
            Point screenPoint = GetPointForDirection(rotatedDir);
            return screenPoint;
        }

        public static void StopCalculateDirection()
        {
            if (instance == null)
                throw new InvalidOperationException("Instance is not initialized, cannot set click point.");

            instance.Active = false;
        }

        public static Point GetPointForDirection(Vector worldDirection)
        {
            if (worldDirection.Length == 0)
                throw new InvalidOperationException("Invalid direction.");
            else if (instance == null)
                throw new InvalidOperationException("Instance is not initialized, cannot set click point.");

            double angle = Maths.Angle(instance.upDirection, worldDirection);
            Vector rotatedScreenPoint = toRotate.Rotate(angle);
            Vector newScreenVect = Center + rotatedScreenPoint;

            return newScreenVect.ToPoint();
        }
    }
}
