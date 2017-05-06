using Quget_Engine_One.Sound;
using System;
using System.Threading;
namespace Quget_Engine_One
{
    class Program
    {
       // [STAThread]
        static void Main(string[] args)
        {
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