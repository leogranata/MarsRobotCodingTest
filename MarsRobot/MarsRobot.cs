using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRobot
{
    /// <summary>
    /// Class that handles the logic for the robot to move and follows commands as per the requirements
    /// </summary>
    public class MarsRobot
    {
        // The space limits, it's populated on the constructor
        private Coordinates plateuLimits;

        // Stores the current position of the robot and initializes the initial position
        public Position CurrentPosition { get; set; } = new Position(1, 1, FacingDirection.North);

        /// <summary>
        /// Initializes an instance of the MarsRobot class and assigns the size of the limits of the plateau
        /// </summary>
        /// <param name="plateuLimits">string containing the limits of the plateau (e.g. "5x5")</param>
        public MarsRobot(string plateuLimits)
        {
            this.plateuLimits = new Coordinates(plateuLimits);
        }

        /// <summary>
        /// Handles robot navigation by accepting the commands and updating its position
        /// We assume command's input format is always valid
        /// </summary>
        /// <param name="command">The navigation command for the robot to execute (e.g. "FFLFRF")</param>
        public void Navigate(string command)
        {
            // Save Current Position
            // This is useful as it simplifies the logic of checking whether or not the resulting position is within the limits
            // Allow the robot to restore the previous positiong ignoring commands that makes it go off the limits
            Position previousPosition = new Position(CurrentPosition.Coordinates.X, CurrentPosition.Coordinates.Y, CurrentPosition.Direction);

            // Execute navigation
            // There is one letter representing each action
            // Below we call the corresponding method for each action to be executed
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

        /// <summary>
        /// Turn the robot right
        /// Depending on the current position this means swithing between facing directions
        /// </summary>
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
        /// <summary>
        /// Turn the robot left
        /// Depending on the current position this means swithing between facing directions
        /// </summary>
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

        /// <summary>
        /// Move forward on it's facing direction
        /// Depending on the current facing direction it will increase or descrease X or Y
        /// </summary>
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

        /// <summary>
        /// Facing Directions the robot is able to use
        /// </summary>
        public enum FacingDirection
        {
            North,
            South,
            East,
            West
        }

        /// <summary>
        /// Stores the position/direction of the robot
        /// </summary>
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

            /// <summary>
            /// Overrides the ToString() method to return the position/direction in the correct format
            /// </summary>
            /// <returns>the position/direction in the correct format</returns>
            public override string ToString()
            {
                return String.Format("{0},{1},{2}", this.Coordinates.X, this.Coordinates.Y, this.Direction.ToString());
            }
        }

        /// <summary>
        /// Stores the coordinates X/Y
        /// Parses a initialization coordinate string (e.g. "5x5") that is used to initialize the limits of the plateau
        /// We assume input format is always valid
        /// </summary>
        public struct Coordinates
        {
            public int X;
            public int Y;
            
            public Coordinates(string xy)
            {
                string[] valueArray = xy.Split('x');
                this.X = int.Parse(valueArray[0].ToString());
                this.Y = int.Parse(valueArray[1].ToString());
            }
        }

        /// <summary>
        /// Definition of possible commands to be received by the robot
        /// It simplifies the potential situation where the commands have to be modified to something else
        /// </summary>
        public static class NavigationConstants
        {
            public const char TurnLeft = 'L';
            public const char TurnRight = 'R';
            public const char MoveForward = 'F';
        }
    }
}
