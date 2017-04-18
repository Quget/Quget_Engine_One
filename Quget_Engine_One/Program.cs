using System;

namespace Quget_Engine_One
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Starting Quget Engine One");

            new Game();

            Console.WriteLine();
            Console.WriteLine("Engine closed by user or error");
            //Console.ReadKey();
        }
    }
}