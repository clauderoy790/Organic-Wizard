using Shared;
using Shared.Image_Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Organic_Wizard.Logic.States
{
    public class CheckForPartyState : CharState
    {
        const int PARTY_BAR_CLICK_POSITION_OFFSET = -60;
        DiffFinder partyBarFinder;
        Point partyBarLocation;
        List<Point> _partyBarPoints;
        Color partyBarColor = Color.Empty;

        public CheckForPartyState(StateMachine sm) : base(sm)
        {
            _partyBarPoints = new List<Point>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Task.Run(() =>
            {
                SetPartyIndex();
                FinishState();
            });
        }

        public void SetPartyIndex()
        {
            if (partyBarFinder == null)
            {
                InitPartyBarFinder();
            }
            if (partyBarLocation.X == 0 || partyBarLocation.Y == 0)
            {
                partyBarLocation = GetPartyBarLocation();
                if (partyBarLocation.X > 0 && partyBarLocation.Y > 0)
                    partyBarColor = ColorUtils.GetColorAt(partyBarLocation);
            }
            else if (partyBarColor != Color.Empty && 
                ColorUtils.GetColorAt(partyBarLocation) != partyBarColor) //If moved, reset it
            {
                Debug.Log("reset it");
                partyBarColor = Color.Empty;
                partyBarLocation = new Point(0,0);
                SetPartyIndex();
                return;
            }

            int ptSize = FindPartySize();
            Character.PartySize = ptSize;
            Character.IsInParty = ptSize > 0;
            if (Character.IsInParty)
                Character.SelectedPartyMemberIndex = FindSelectedPartyMemberIndex();
            else
                Character.SelectedPartyMemberIndex = -1;
        }

        public void ClickPartyMember(int index)
        {
            if (index < 0 || index >= _partyBarPoints.Count)
                return;

            Point pos = _partyBarPoints[index];
            MouseOperations.Click(pos);
        }

        private void InitPartyBarFinder()
        {
            partyBarFinder = new DiffFinder(650, 0, Constants.SCREEEN_WIDTH - 650, 550);

            var point1 = new Point(0, 0);
            var point5 = new Point(0, 1);
            var point2 = new Point(0, 25);
            var point3 = new Point(0, 26);
            var point4 = new Point(0, 40);
            partyBarFinder.AddDiff(point1, point3, 39);
            partyBarFinder.AddDiff(point2, point3, 9);
            partyBarFinder.AddDiff(point3, point4, 0);
            partyBarFinder.AddDiff(point1, point5, 15);
        }

        private int FindPartySize()
        {
            if (partyBarLocation.X == 0 || partyBarLocation.Y == 0)
                return 0;

            DiffFinder finder = new DiffFinder(partyBarLocation.X, partyBarLocation.Y, 1,
             Constants.SCREEEN_HEIGHT - partyBarLocation.Y);
            finder.AddDiff(new Point(0, 0), new Point(0, 1), 9);
            finder.AddDiff(new Point(0, 1), new Point(0, 15), 0);

            var points = finder.ProcessImage(Constants.MAX_PARTY_SIZE);
            _partyBarPoints.Clear();
            foreach (var point in points)
            {
                _partyBarPoints.Add(new Point(point.X + PARTY_BAR_CLICK_POSITION_OFFSET, point.Y));
            }
            return points.Count;
        }

        private int FindSelectedPartyMemberIndex()
        {
            int index = -1;
            if (partyBarLocation.X == 0 || partyBarLocation.Y == 0)
                return index;

            DiffFinder finder = new DiffFinder(partyBarLocation.X, partyBarLocation.Y, 1,
             Constants.SCREEEN_HEIGHT - partyBarLocation.Y);
            Point point0 = new Point(0, 0);
            Point point1 = new Point(0, 10);
            Point point2 = new Point(0, 24);
            Point point3 = new Point(0, 48);
            finder.AddDiff(point0, point3, 4);
            finder.AddDiff(point1, point2, 0);
            finder.AddDiff(point0, point1, 19);

            var points = finder.ProcessImage();

            if (points.Count == 1 && points[0].Y > 0)
            {
                var point = points[0];
                int diff = point.Y - partyBarLocation.Y;
                index = diff / 53;
            }

            return index;
        }

        private Point GetPartyBarLocation()
        {
            var points = partyBarFinder.ProcessImage();
            return points.Count > 0 ? points[0] : new Point(0, 0);
        }

        public override EState GetName()
        {
            return EState.CheckForParty;

        }
    }
}
