using System;

namespace TextAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string s_input, last_chars = String.Empty;
            char last_c = '\0';
            int w_length = 0, w_count = 0, symb_count = 0;
            Console.WriteLine("Write the text to analyze.");
            s_input = Console.ReadLine() + ' ';//' ' is to check last word
            for (int i = 0; i < s_input.Length; ++i)
            {
                if (s_input[i] != ' ')
                {
                    symb_count++;
                }
                if (Char.IsLetter(s_input[i]))
                {
                    w_length++;
                    last_c = s_input[i];
                }
                else
                {
                    //words must be at least 2 characters long
                    if (w_length > 1)
                    {
                        w_count++;
                        last_chars += last_c;
                    }
                    w_length = 0;
                }
            }
            Console.WriteLine($"Количество слов: {w_count};");
            Console.WriteLine($"Количество символов без пробелов: {symb_count};");
            Console.WriteLine($"Соотношение количества символов к количеству слов: {(Convert.ToDouble(symb_count) / w_count):F}");
            Console.WriteLine($"Слово из последних символов слов: \"{last_chars}\".");
            Console.ReadKey();
        }
    }
}
