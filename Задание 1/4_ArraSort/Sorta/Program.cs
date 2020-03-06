using System;

namespace Sorta
{
    class Program
    {
        static void Main(string[] args)
        {
            bool succ = false;
            int arr_size = 0;
            Console.WriteLine("Enter an array size:");
            do
            {
                try
                {
                    arr_size = Convert.ToInt32(Console.ReadLine());
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try again.");
                }
                if (succ && (arr_size < 1))
                {
                    Console.WriteLine("Array should be at least 1 element big. Try again.");
                    succ = false;
                }
            } while (!succ);
            int[] i_arr = new int[arr_size];
            Console.WriteLine($"Enter {arr_size} numbers:");
            for (int i = 0; i < arr_size; ++i)
            {
                do
                {
                    try
                    {
                        i_arr[i] = Convert.ToInt32(Console.ReadLine());
                        succ = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message} Try again.");
                        succ = false;
                    }
                } while (!succ);       
            }
            int temp;
            for (int i = 0; i < arr_size - 1; ++i)
            {
                for (int j = i + 1; j < arr_size; ++j)
                {
                    if (i_arr[i] > i_arr[j])
                    {
                        temp = i_arr[i];
                        i_arr[i] = i_arr[j];
                        i_arr[j] = temp;
                    }
                }
            }
            Console.WriteLine("\nSorted array:");
            for (int i = 0; i < arr_size; ++i)
            {
                Console.Write($"{i_arr[i]} ");
            }
            Console.ReadKey();
        }
    }
}
