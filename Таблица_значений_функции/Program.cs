using System;

namespace Таблица_значений_функции
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите значение, на которое увеличивается х:");
            double addition = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите диапазон значений х(начало и конец на разных строках):");
            int startRange = Convert.ToInt16(Console.ReadLine());
            int endRange = Convert.ToInt16(Console.ReadLine());

            int i = startRange;
            double result;
            while (i <= endRange)
            {
                result = i + addition;
                Console.WriteLine("{0} {1}", i, result);
                i++;
            }
        }
    }
}
