using System;

namespace TestConsole1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int jogger = 0;
                jogger = 10 / jogger;
                Console.WriteLine($"{jogger}");
            }
            catch
            {
                Console.WriteLine("RIP");
            }
            finally
            {
                Console.WriteLine("Ok, done");
            }
            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
