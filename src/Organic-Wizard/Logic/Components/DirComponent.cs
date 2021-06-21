using System;
using System.Windows;
using Shared;

namespace Organic_Wizard
{
    public class DirComponent : Component
    {
        const double dirMinLength = 3;
        private Vector lastDirCalcPos;

        public Vector Direction { get; private set; }

        public override void OnEnable()
        {
            base.OnEnable();
            Direction = new Vector(0,0);
            lastDirCalcPos = new Vector(0,0);
            Character.PositionChanged -= OnPositionChanged;
            Character.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(Vector newPos)
        {
            if (lastDirCalcPos.IsZero())
            {
                lastDirCalcPos = newPos;
            }
            else
            {
                Vector dir = (newPos-lastDirCalcPos);
                if (dir.Length >= dirMinLength)
                {
                    lastDirCalcPos = newPos;
                    dir.Normalize();
                    Direction = dir;
                }
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Direction = new Vector(0,0);
            lastDirCalcPos = new Vector(0,0);
            Character.PositionChanged -= OnPositionChanged;
        }


    }
}