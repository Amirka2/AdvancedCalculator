using System;
using System.IO;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        static double ReturnYValue(string expression, double x)
        {
            expression = expression.Replace("x", x.ToString());
            double y = RPN.Calculate(expression);
            return y;
        }
        static int GetMaxNumLength(double a, double b)
        {
            if (a >= b)
                return a.ToString().Length;
            else
                return b.ToString().Length;
        }
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
            
            string expression = String.Empty;
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
                    }
                    else if (text[m].Contains("Начало"))
                    {
                        start = Convert.ToDouble(ReturnNumber(text[m]));
                    }
                    else if (text[m].Contains("Конец"))
                    {
                        end = Convert.ToDouble(ReturnNumber(text[m]));
                    }
                }
                else if (text[m].Contains("f"))
                {
                    string expr = ReturnNumber(text[m]);  //Получаем выражение
                    expr = expr.Trim();
                    while (expr.Contains("  "))
                    {
                        expr = expr.Replace("  ", " ");
                    }
                    var els = expr.Split(' ');
                    funcArr.AddRange(els);
                    expression = expr;
                }
            }
            
            using (var sw = new StreamWriter("output.txt", true))
            {
                sw.WriteLine();
                for (double i = start, j = 0; i <= end; i++, j++)
                {
                    double x = i, y = ReturnYValue(expression, i);
                    sw.WriteLine($"x = {x}, y = {y}");
                }
            }
        }
    }
}
