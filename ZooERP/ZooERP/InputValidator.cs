using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP
{
    public static class InputValidator
    {
        public static int GetIntInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input))
                    break;
                else
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
            }
            return input;
        }

        public static int GetIntInput(string prompt, int min, int max)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max)
                    break;
                else
                    Console.WriteLine($"Некорректный ввод. Введите число от {min} до {max}.");
            }
            return input;
        }

        public static bool GetBoolInput(string prompt)
        {
            bool input;
            while (true)
            {
                Console.Write(prompt);
                string response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    input = true;
                    break;
                }
                else if (response == "no")
                {
                    input = false;
                    break;
                }
                else
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'yes' или 'no'.");
            }
            return input;
        }

        public static string GetAnimalType()
        {
            string type;
            while (true)
            {
                Console.Write("Введите тип животного (Monkey, Rabbit, Tiger, Wolf, или введите новый тип): ");
                type = Console.ReadLine();
                if (type.Equals("Monkey", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Rabbit", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Tiger", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Wolf", StringComparison.OrdinalIgnoreCase) ||
                    !string.IsNullOrEmpty(type))
                    break;
                else
                    Console.WriteLine("Некорректный тип животного, попробуйте снова.");
            }
            return type;
        }

        public static string GetItemType()
        {
            string type;
            while (true)
            {
                Console.Write("Введите тип предмета (Table, Computer или введите новый тип): ");
                type = Console.ReadLine();
                if (type.Equals("Table", StringComparison.OrdinalIgnoreCase) || type.Equals("Computer", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrEmpty(type))
                    break;
                else
                    Console.WriteLine("Некорректный тип предмета, попробуйте снова.");
            }
            return type;
        }
    }
}