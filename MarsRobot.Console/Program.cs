using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRobot
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("Welcome to MarsRobot console application");
            Console.WriteLine("Coding test applying for job at Codec! - Wish me good luck :)");
            Console.WriteLine("By Leonardo Granata - lgranata@gmail.com");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("First Input: The limits of the plateau (Example: 5x5, 3x4, etc)");
            
            string plateuLimits = Console.ReadLine();
            
            Console.WriteLine("Second Input: Navigation command string using the following characters in a unique string ");
            Console.WriteLine("");
            Console.WriteLine(String.Format("{0}: Turn the robot left", MarsRobot.NavigationConstants.TurnLeft));
            Console.WriteLine(String.Format("{0}: Turn the robot right", MarsRobot.NavigationConstants.TurnRight));
            Console.WriteLine(String.Format("{0}: Move forward on it's facing direction", MarsRobot.NavigationConstants.MoveForward));
            Console.WriteLine("");
            Console.WriteLine("Example: LFLRFLFF");
            
            string navigationCommand = Console.ReadLine();

            MarsRobot robot = new MarsRobot(plateuLimits);
            robot.Navigate(navigationCommand);

            Console.WriteLine(String.Format("The current position is (X, Y, Orientation) {0}", robot.CurrentPosition.ToString()));
            Console.WriteLine("");
            Console.WriteLine("Thanks for using MarsRobot, press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
