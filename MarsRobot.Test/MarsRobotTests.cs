using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRobot;

namespace MarsRobot.Unit.Tests
{
    [TestClass]
    public class MarsRobotTests
    {

        [TestMethod]
        public void MarsRobot_Navigate_InitialPosition()
        {
            MarsRobot robot = new MarsRobot("5x5");

            // Check position is Okay at the beginning
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 1);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 1);
            Assert.AreEqual(robot.CurrentPosition.Direction, MarsRobot.FacingDirection.North);
        }

        [TestMethod]
        public void MarsRobot_Navigate_WithinLimits()
        {
            MarsRobot robot = new MarsRobot("15x15");
            robot.Navigate("RFFLFF");

            // Check position is Okay so far
            Assert.AreEqual(robot.CurrentPosition.Coordinates.X, 3);
            Assert.AreEqual(robot.CurrentPosition.Coordinates.Y, 3);
        }

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
