using Organic_Wizard.Logic;
using System;
using System.Drawing;

namespace Organic_Wizard
{
    public class SkillInfo
    {
        public SkillInfo(Point position, int skillNumber, Point cooldownCheckPoint, Color offCooldownColor)
        {
            Position = position;
            Number = skillNumber;
            OffCooldownColor = offCooldownColor;
            CooldownCheckPoint = cooldownCheckPoint;
            PercentageMpUsed = 0;
        }

        public Point Position { get; set; }

        public int Number { get; set; }
        public Point CooldownCheckPoint { get; set; }
        public int PercentageMpUsed { get; set; }
        public Color OffCooldownColor { get; set; }

        public int CooldownMinutes { get; set; }

        public bool IsOnCooldown()
        {
            return SkillBar.IsOnCooldown(this);
        }
    }

}