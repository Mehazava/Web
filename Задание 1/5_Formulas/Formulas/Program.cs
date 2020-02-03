using System;

namespace Formulas
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] i_a = { 0, 0, 0, 0 };
            double d_result = 0;
            bool succ = false;
            
            //First formula
            Console.WriteLine("Write x, y, z for formula:\n" +
                "f = (x-y)^sin(z):\n");
            do
            {
                for (int i = 0; i < 3; ++i)
                {
                    do
                    {
                        try
                        {
                            i_a[i] = Convert.ToDouble(Console.ReadLine());
                            succ = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message} Enter argument {i + 1} again.");
                            succ = false;
                        }
                    } while (!succ);
                }
                try
                {
                    d_result = Math.Pow((i_a[0] - i_a[1]), (Math.Sin(i_a[2])));
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try other arguments.");
                    succ = false;
                }
                if (Double.IsNaN(d_result))
                {
                    Console.WriteLine("An error has occured. Try other arguments.");
                    succ = false;
                }
            } while (!succ);
            Console.WriteLine($"The result is {d_result}.\n");

            //Second formula
            Console.WriteLine("Write x, y, z for formula:\n" +
                "f = log(5y/z, x):\n");
            do
            {
                for (int i = 0; i < 3; ++i)
                {
                    do
                    {
                        try
                        {
                            i_a[i] = Convert.ToDouble(Console.ReadLine());
                            succ = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message} Enter argument {i + 1} again.");
                            succ = false;
                        }
                    } while (!succ);
                }
                try
                {
                    d_result = Math.Log(i_a[1] * 5 / i_a[2], i_a[0]);
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try other arguments.");
                    succ = false;
                }
                if (Double.IsNaN(d_result))
                {
                    Console.WriteLine("An error has occured. Try other arguments.");
                    succ = false;
                }
            } while (!succ);
            Console.WriteLine($"The result is {d_result}.\n");

            //Third formula
            Console.WriteLine("Write x, y, z for formula:\n" +
                "f = log(x*e^y, 2)*z:\n");
            do
            {
                for (int i = 0; i < 3; ++i)
                {
                    do
                    {
                        try
                        {
                            i_a[i] = Convert.ToDouble(Console.ReadLine());
                            succ = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message} Enter argument {i + 1} again.");
                            succ = false;
                        }
                    } while (!succ);
                }
                try
                {
                    d_result = Math.Log(i_a[0] * Math.Exp(i_a[1]), 2) * i_a[2];
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try other arguments.");
                    succ = false;
                }
                if (Double.IsNaN(d_result))
                {
                    Console.WriteLine("An error has occured. Try other arguments.");
                    succ = false;
                }
            } while (!succ);
            Console.WriteLine($"The result is {d_result}.\n");

            //Fourth formula
            Console.WriteLine("Write x, y, z, p for formula:\n" +
                "f = (x-1)^log(y,e)*(e*z)^(p+1):\n");
            do
            {
                for (int i = 0; i < 4; ++i)
                {
                    do
                    {
                        try
                        {
                            i_a[i] = Convert.ToDouble(Console.ReadLine());
                            succ = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message} Enter argument {i + 1} again.");
                            succ = false;
                        }
                    } while (!succ);
                }
                try
                {
                    d_result = Math.Pow(i_a[0] - 1, Math.Log(i_a[1], Math.E)) * Math.Pow(i_a[2] * Math.E, i_a[3] + 1);
                    succ = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Try other arguments.");
                    succ = false;
                }
                if (Double.IsNaN(d_result))
                {
                    Console.WriteLine("An error has occured. Try other arguments.");
                    succ = false;
                }
            } while (!succ);
            Console.WriteLine($"The result is {d_result}.\n");
            Console.ReadKey();
        }
    }
}
