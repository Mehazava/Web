using System;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of array:");
            int[] i_input = { 0, 0, 0 };
            bool succ = false;
            do
            {
                try
                {
                    i_input[0] = Convert.ToInt32(Console.ReadLine());
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}, try again");
                }
                finally
                {
                    if (succ && (i_input[0] < 1))
                    {
                        Console.WriteLine("Can't make an array smaller than 1 element.");
                        succ = false;
                    }
                }
            } while (!succ);
            Console.WriteLine("Enter the minimum value:");
            do
            {
                try
                {
                    i_input[1] = Convert.ToInt32(Console.ReadLine());
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}, try again");
                }
            } while (!succ);
            Console.WriteLine("Enter the maximum value:");
            do
            {
                try
                {
                    i_input[2] = Convert.ToInt32(Console.ReadLine());
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try again");
                }
                finally
                {
                    if (succ && (i_input[2] < i_input[1]))
                    {
                        Console.WriteLine("Can't have max value smaller than min.");
                        succ = false;
                    }
                }
            } while (!succ);
            Random rng = new Random();
            Console.WriteLine("Generated array:");
            for (int i = i_input[0]; i > 0; --i)
            {
                Console.Write($"{rng.Next(i_input[1], i_input[2])} ");
            }
            Console.ReadKey();
        }
    }
}
