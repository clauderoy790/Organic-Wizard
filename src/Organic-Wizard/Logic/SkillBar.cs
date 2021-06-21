using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace Organic_Wizard.Logic
{
    public static class SkillBar
    {
        const int NB_SKILLS = 10;
        const int SKILL_HEIGHT = 36;
        const int SKILL_WIDTH = 30;
        const int SKILL_COOLDOWN_COLOR_CHECK_Y_PIXEL_OFFSET = 3;
        const int SKILL_COOLDOWN_COLOR_DIFF_TOLERANCE = 5;
        static Point initialOffset;
        static Dictionary<int, SkillInfo> skillInfos;
        static PointGroup cooldownSetPoints;
        static Dictionary<int, int> colorDiffMinutesCooldowns;
        static Point skillBarLocation;

        public static bool IsInitialized
        {
            get { return skillInfos != null && skillInfos.Count > 0; }
        }

        public static Point SkillBarLocation { get { return skillBarLocation; } }

        static SkillBar()
        {
            initialOffset = new Point(20, 26);
            cooldownSetPoints = new PointGroup(
                new Point(-17, 8),
                new Point(-23, 15),
                new Point(-7, 16)
            );

            colorDiffMinutesCooldowns = new Dictionary<int, int>()
            {
                {56,2 },//Wolf
                {12,2 },//Prayer of god's power
                {38,7 },//Wilderness
            };
        }

        public static void InitSkillsInfo()
        {
            skillInfos = new Dictionary<int, SkillInfo>();
            lock (skillInfos)
            {
                skillBarLocation = GetSkillBarLocation();
                if (skillBarLocation.IsZero())
                {
                    Debug.Log("Failed to find skillbar location");
                    return;
                }

                Point offsettedPoint = new Point(skillBarLocation.X + initialOffset.X, skillBarLocation.Y + initialOffset.Y);

                for (int i = 0; i < NB_SKILLS; i++)
                {
                    Point current = new Point(offsettedPoint.X, offsettedPoint.Y + (i * SKILL_HEIGHT));
                    Tuple<Point, Color> offCooldownInfo = GetCooldownPointAndColor(current);
                    int skillIndex = i + 1;
                    if (skillIndex == 10)
                        skillIndex = 0;
                    SkillInfo info = new SkillInfo(current, skillIndex, offCooldownInfo.Item1, offCooldownInfo.Item2);
                    SetCooldownMinutes(info);
                    skillInfos[skillIndex] = info;
                }
            }
        }

        public static void Reset()
        {
            if (skillInfos != null)
                skillInfos.Clear();
        }

        private static Tuple<Point, Color> GetCooldownPointAndColor(Point current)
        {
            Point point = new Point(0, 0);
            Color color = Color.Empty;
            if (!current.IsZero())
            {
                int x = (int)(current.X - (SKILL_WIDTH / 2f));
                int y = current.Y + SKILL_COOLDOWN_COLOR_CHECK_Y_PIXEL_OFFSET;
                point = new Point(x, y);
                color = ColorUtils.GetColorAt(point);
            }

            return new Tuple<Point, Color>(point, color);
        }

        public static bool IsOnCooldown(SkillInfo skill)
        {
            if (!IsInitialized)
                throw new Exception("Trying to use the SkillBarManager when it is not initialized.");

            Color color = ColorUtils.GetColorAt(skill.CooldownCheckPoint);
            if (color != Color.Empty)
            {
                int diff = ColorUtils.ColorDiff(skill.OffCooldownColor, color);
                return diff >= SKILL_COOLDOWN_COLOR_DIFF_TOLERANCE;
            }
            return false;
        }

        public static SkillInfo GetSkillForNumber(int skillNumber)
        {
            if (!IsInitialized)
                throw new Exception("Trying to use the SkillBarManager when it is not initialized.");

            if (skillInfos.ContainsKey(skillNumber))
                return skillInfos[skillNumber];
            else
                return null;
        }

        public static int GetCooldownMinutesForSkill(int skill)
        {
            if (!IsInitialized)
                throw new Exception("Trying to use the SkillBarManager when it is not initialized.");

            if (skill < 0 || skill >= NB_SKILLS)
                throw new Exception($"Range for skill is not valid, must be between 0 and {NB_SKILLS}");

            return skillInfos[skill].CooldownMinutes;
        }
        #region PRIVATE_METHODS
        private static void SetCooldownMinutes(SkillInfo info)
        {
            List<Color> colors = new List<Color>();
            foreach (var point in cooldownSetPoints.Points)
            {
                Point offsetted = new Point(info.Position.X + point.X, info.Position.Y + point.Y);
                colors.Add(ColorUtils.GetColorAt(offsetted));
            }

            int diff = ColorUtils.GetAverageDiff(colors);
            if (colorDiffMinutesCooldowns.ContainsKey(diff))
            {
                info.CooldownMinutes = colorDiffMinutesCooldowns[diff];
            }
        }
        private static Point GetSkillBarLocation()
        {
            Int32Rect rect = new Int32Rect(0, 90, 230, 650);
            Point comparePoint = new Point(464, 742);
            Color compareColor = ColorUtils.GetColorAt(comparePoint);

            PointGroupColorDiff points = new PointGroupColorDiff(rect,
                new Point(0, 0),
                new Point(-1, 0),
                new Point(13, 0),
                new Point(0, 1),
                new Point(0, 2)
            );

            Point exactPoint;
            using (points)
            {
                points.AddDiff(0, 1, 80);
                points.AddDiff(0, 2, 47);
                points.AddDiff(0, 3, 1);
                points.AddDiff(0, 4, 4);
                points.AddDiff(0, compareColor, 9);
                points.Tolerance = 3;
                exactPoint = points.FindExactPoint();
            }

            return exactPoint;
        }
        #endregion
    }
}
