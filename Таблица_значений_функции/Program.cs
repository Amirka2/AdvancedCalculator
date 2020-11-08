using System;
using System.IO;
using System.Collections.Generic;

namespace Таблица_значений_функции
{
    class Program
    {
        static bool CheckText (string str)
        {
            bool check = false;
            if ((str.Contains("Шаг:")) || (str.Contains("Начало диапазона:")) || (str.Contains("Конец диапазона:")) || (str.Contains("f:")))
                check = true;
            else
                check = false;
            return check;
        }
        static string ReturnNumber (string str)
        {
            string[] arr = new string[2];
            if (str.Contains(":"))
            {
                arr = str.Split(':');
            }
            return arr[1];
        }
        static bool CheckNumber(string input)
        {
            double c;
            bool check = Double.TryParse(input, out c);
            return check;
        }
        private static double TrimAndConvertNumber(string input)
        {
            input = input.Trim();
            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }

            return Double.Parse(input);
        }

        /*static string[] ReturnExpresionArray(string input)
        {
            var elements = new List<string> { };
            input = input.Trim();

            return input;
        }*/
        static void Main(string[] args)
        {
            var text = new List<string> { };
            var funcArr = new List<string> { };
            double addition = 0, start = 0, end = 0;
            using (var sr = new StreamReader(@"input.txt"))
            {
                string str = String.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    text.Add(str);
                }
            }

            for (int m = 0; m < text.Count; m++)
            {
                if (string.IsNullOrWhiteSpace(text[m]))
                {
                    Console.WriteLine("строка пуста!");
                }
                else if ((CheckText(text[m])) && (CheckNumber(ReturnNumber(text[m]))))
                {
                    if (text[m].Contains("Шаг"))
                    {
                        addition = Convert.ToDouble(ReturnNumber(text[m]));
                        Console.WriteLine("add = " + addition);
                    }
                    else if (text[m].Contains("Начало"))
                    {
                        start = Convert.ToDouble(ReturnNumber(text[m]));
                        Console.WriteLine("start = " + start);
                    }
                    else if (text[m].Contains("Конец"))
                    {
                        end = Convert.ToDouble(ReturnNumber(text[m]));
                        Console.WriteLine("end = " + end);
                    }
                }
                else if (text[m].Contains("f"))
                {
                    string expression = ReturnNumber(text[m]);  //Получаем выражение
                    Console.WriteLine("expression = " + expression);
                    while (text[m].Contains("  "))
                    {
                        text[m] = text[m].Replace("  ", " ");
                    }
                    var els = text[m].Split(' ');
                    funcArr.AddRange(els);
                }
            }

            using (var sw = new StreamWriter("output.txt", true))
            {
                foreach (var el in funcArr)
                {
                    sw.Write(el);
                }
            }
            
            
        }
    }
}
