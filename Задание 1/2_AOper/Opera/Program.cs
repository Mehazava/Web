using System;

namespace Opera
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose operator (+, -, *, /)");
            string s_oper = "";
            char c_oper = '\0';
            do
            {
                s_oper = Console.ReadLine();
                try
                {
                    c_oper = Convert.ToChar(s_oper);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            } while (!((c_oper == '+') || (c_oper == '-') || (c_oper == '/') || (c_oper == '*')));
            double[] i_num = { 0, 0 };
            string[] s_num = { "", "" };
            bool succ = false;
            for (int i = 0; i <= 1; ++i)
            {
                Console.WriteLine($"Enter number {i + 1}:");
                do
                {
                    s_num[i] = Console.ReadLine();
                    succ = true;
                    try
                    {
                        i_num[i] = Convert.ToDouble(s_num[i]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}");
                        succ = false;
                    }
                } while (!succ);
            }
            double o_result = 0;
            switch (c_oper)
            {
                case '+':
                    o_result = i_num[0] + i_num[1];
                    break;
                case '-':
                    o_result = i_num[0] - i_num[1];
                    break;
                case '/':
                    o_result = i_num[0] / i_num[1];
                    break;
                case '*':
                    o_result = i_num[0] * i_num[1];
                    break;
            }
            Console.WriteLine($"The result is {o_result}");
            Console.ReadKey();
        }
    }
}
