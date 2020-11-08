using System;
using System.Collections.Generic;

namespace Таблица_значений_функции
{
    public class RPN
    {
        static public double Calculate(string input)
        {
            var expression = ToPostfixNotation(input);
            double result = Calculations(expression);
            return result;
        }

        static string ToPostfixNotation(string input)
        {
            string output = string.Empty; 
            Stack<char> StackOfOperations = new Stack<char>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                if (IsSpace(input[i]))
                    continue; 

                if (Char.IsDigit(input[i])) 
                {
                    while (!IsSpace(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; 
                        i++; 

                        if (i == input.Length) break; 
                    }

                    output += " ";
                    i--; 
                }

                if (IsOperator(input[i])) 
                {
                    if (input[i] == '(')
                    {
                        StackOfOperations.Push(input[i]);
                    }                    
                    else if (input[i] == ')') 
                    {
                        char s = StackOfOperations.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = StackOfOperations.Pop();
                        }
                    }
                    else 
                    {
                        if (StackOfOperations.Count > 0) 
                            if (Priority(StackOfOperations.Peek()) >= Priority(input[i])) 
                                output += StackOfOperations.Pop().ToString() + " ";
                        StackOfOperations.Push(
                            char.Parse(input[i]
                                .ToString()));
                    }
                }
            }

            while (StackOfOperations.Count > 0)
            {
                output += StackOfOperations.Pop() + " ";
            }

            return output;
        }

        static double Calculations(string input)
        {
            double result = 0; 
            Stack<double> temp = new Stack<double>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                if (Char.IsDigit(input[i])) 
                {
                    string num = string.Empty;

                    while (!IsSpace(input[i]) && !IsOperator(input[i]))
                    {
                        num += input[i]; 
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(num));
                    i--;
                }
                else if (IsOperator(input[i])) 
                {
                    double a = temp.Pop(); 
                    double b = temp.Pop();
                    

                    switch (input[i]) 
                    { 
                        case '+': 
                            result = b + a; 
                            break;
                        case '-': 
                            result = b - a;
                            break;
                        case '*': 
                            result = b * a;
                            break;
                        case '/': 
                            result = b / a;
                            break;
                        case '^':
                            result = Math.Pow(b, a);
                            break;
                    }
                    temp.Push(result); 
                }
            }
            return temp.Peek();
        }

        static bool IsSpace(char ch)
        {
            if ((" ".IndexOf(ch) != -1))
                return true;
            return false;
        }

        static bool IsOperator(char ch)
        {
            if (("+-/*^()".IndexOf(ch) != -1))
                return true;
            return false;
        }

        static int Priority(char ch)
        {
            switch (ch)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                    return 2;
                case '-':
                    return 3;
                case '*':
                    return 4;
                case '/':
                    return 4;
                case '^':
                    return 5;
                default:
                    return 6;
                    
            }
        }

        static bool Check(string input)
        {
            var elements = input.Split(" ");
            double c;
            foreach (var el in elements)
            {
                if (el.Length == 1)
                {
                    if (IsOperator(Convert.ToChar(el)))
                        continue;
                    else if (!double.TryParse(el, out c))
                        return false;
                }
                else
                {
                    if (!double.TryParse(el, out c))
                        return false;
                }
            }

            return true;
        }
        
    }
}