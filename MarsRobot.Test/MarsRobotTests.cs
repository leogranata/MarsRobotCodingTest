using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRobot;

namespace MarsRobot.Unit.Tests
{
    /// <summary>
    /// Test Class to test the functionality of the robot
    /// </summary>
    [TestClass]
    public class MarsRobotTests
    {
        /// <summary>
        /// Tests whether or not the robot starts at the correct position (1,1,North)
        /// </summary>
        [TestMethod]
        public void MarsRobot_Navigate_InitialPosition()
        {
            MarsRobot robot = new MarsRobot("5x5");

            // Check position is Okay at the beginning
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 1);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 1);
            Assert.AreEqual(robot.CurrentPosition.Direction, MarsRobot.FacingDirection.North);
        }

        /// <summary>
        /// Tests wether or not the robot can follow the commands to navigate between the limits of the defined plateau
        /// </summary>
        [TestMethod]
        public void MarsRobot_Navigate_WithinLimits()
        {
            MarsRobot robot = new MarsRobot("15x15");
            robot.Navigate("RFFLFF");

            // Check position is Okay so far
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 3);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 3);
        }

        /// <summary>
        /// Tests whether or not the robot handles correctly when the user input send it to a position 
        /// which is outside the boundaries
        /// </summary>
        [TestMethod]
        public void MarsRobot_Navigate_OffLimits()
        {
            MarsRobot robot = new MarsRobot("6x6");
            robot.Navigate("RFF");

            // Check position is Okay so far
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 3);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 1);

            // Move outside the limits (it's going to X=7)
            robot.Navigate("FFFF");

            // Check position is kept the same as the navigation command should be ignored
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 3);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 1);
        }

        /// <summary>
        /// Tests whether or not the robot is capable of moving to the limit of the plateau
        /// This is important to make sure it's not being confused by the fact the Zero position is not affected
        /// and it can navigate correctly to the limit without getting confused that it has maybe passed the limit
        /// </summary>
        [TestMethod]
        public void MarsRobot_Navigate_ToTheLimits()
        {
            MarsRobot robot = new MarsRobot("7x7");
            // move using right
            robot.Navigate("FFFFFFRFFFFFF");

            // Check position is Okay so far
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 7);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 7);

            robot = new MarsRobot("5x5");
            // move using left
            robot.Navigate("RFFFFLFFFF");

            // Check position is Okay so far
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 5);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 5);

        }



    }
}
