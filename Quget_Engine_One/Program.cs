using Quget_Engine_One.Sound;
using System;
using System.Threading;
namespace Quget_Engine_One
{
    /// <summary>
    /// A 2D game Engine with 3d stuff... xD
    /// Tutorials followed
    /// https://dreamstatecoding.blogspot.com/p/opengl4-with-opentk-tutorials.html
    /// </summary>
    class Program
    {
       // [STAThread]
        static void Main(string[] args)
        {
            //Make it pretty!
            Console.Title = "Quget Engine One Console";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Starting Quget Engine One");

            new Game();

            Console.WriteLine();
            Console.WriteLine("Engine closed by user or error");
            //Console.ReadKey();
        }
    }
}