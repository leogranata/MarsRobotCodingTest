using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRobot
{
    public class MarsRobot
    {
        Coordinates plateuLimits;
        Position _currentPosition = new Position(1, 1, FacingDirection.North);

        public Position CurrentPosition { get => _currentPosition; set => _currentPosition = value; }

        public MarsRobot(string plateuLimits)
        {
            this.plateuLimits = new Coordinates(plateuLimits);
        }

        public void Navigate(string command)
        {
            // Save Current Position
            Position previousPosition = new Position(CurrentPosition.Coordinates.X, CurrentPosition.Coordinates.Y, CurrentPosition.Direction);

            // Execute navigation
            char[] navArray = command.ToCharArray();
            foreach(char navChar in navArray)
            {
                switch (navChar)
                {
                    case NavigationConstants.TurnRight:
                        TurnRight();
                        break;
                    case NavigationConstants.TurnLeft:
                        TurnLeft();
                        break;
                    case NavigationConstants.MoveForward:
                        MoveForward();
                        break;
                }
            }

            // Check that we are within the limits, if not, restore the previous position
            if (CurrentPosition.Coordinates.X > plateuLimits.X || CurrentPosition.Coordinates.Y > plateuLimits.Y ||
                CurrentPosition.Coordinates.X < 1 || CurrentPosition.Coordinates.Y < 1)
            {
                CurrentPosition = previousPosition;
            }
        }

        private void TurnRight()
        {
            switch (CurrentPosition.Direction)
            {
                case FacingDirection.North:
                    CurrentPosition.Direction = FacingDirection.East;
                    break;
                case FacingDirection.South:
                    CurrentPosition.Direction = FacingDirection.West;
                    break;
                case FacingDirection.East:
                    CurrentPosition.Direction = FacingDirection.South;
                    break;
                case FacingDirection.West:
                    CurrentPosition.Direction = FacingDirection.North;
                    break;
                default:
                    break;
            }
        }
        private void TurnLeft()
        {
            switch (CurrentPosition.Direction)
            {
                case FacingDirection.North:
                    CurrentPosition.Direction = FacingDirection.West;
                    break;
                case FacingDirection.South:
                    CurrentPosition.Direction = FacingDirection.East;
                    break;
                case FacingDirection.East:
                    CurrentPosition.Direction = FacingDirection.North;
                    break;
                case FacingDirection.West:
                    CurrentPosition.Direction = FacingDirection.South;
                    break;
                default:
                    break;
            }
        }
        private void MoveForward()
        {
            switch (CurrentPosition.Direction)
            {
                case FacingDirection.North:
                    CurrentPosition.Coordinates.Y++;
                    break;
                case FacingDirection.South:
                    CurrentPosition.Coordinates.Y--;
                    break;
                case FacingDirection.East:
                    CurrentPosition.Coordinates.X++;
                    break;
                case FacingDirection.West:
                    CurrentPosition.Coordinates.X--;
                    break;
            }
        }

        public enum FacingDirection
        {
            North,
            South,
            East,
            West
        }

        public class Position
        {
            public Coordinates Coordinates;
            public FacingDirection Direction;

            public Position(int x, int y, FacingDirection direction)
            {
                this.Coordinates.X = x;
                this.Coordinates.Y = y;
                this.Direction = direction;
            }

            public override string ToString()
            {
                return String.Format("{0},{1},{2}", this.Coordinates.X, this.Coordinates.Y, this.Direction.ToString());
            }
        }
        public struct Coordinates
        {
            public int X;
            public int Y;
            public Coordinates(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public Coordinates(string xy)
            {
                string[] valueArray = xy.Split('x');
                this.X = int.Parse(valueArray[0].ToString());
                this.Y = int.Parse(valueArray[1].ToString());
            }
        }

        public static class NavigationConstants
        {
            public const char TurnLeft = 'L';
            public const char TurnRight = 'R';
            public const char MoveForward = 'F';
        }
    }
}
