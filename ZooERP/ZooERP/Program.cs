using ZooERP.Animals;
using ZooERP.Inventory;
using Microsoft.Extensions.DependencyInjection;
using ZooERP.Classes;
using ZooERP.Interfaces;

namespace ZooERP
{
    class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddSingleton<VetClinic>();
            services.AddSingleton<Zoo>();
            var provider = services.BuildServiceProvider();

            var zoo = provider.GetRequiredService<Zoo>();

            while (true)
            {
                MenuDrawer.DrawMainMenu();
                MenuDrawer.PromptChoice();
                string choice = Console.ReadLine();
                Console.ResetColor();

                switch (choice)
                {
                    case "1":
                        AddAnimal(zoo);
                        break;
                    case "2":
                        AddItem(zoo);
                        break;
                    case "3":
                        zoo.PrintFoodConsumption();
                        break;
                    case "4":
                        zoo.PrintContactZooAnimals();
                        break;
                    case "5":
                        zoo.PrintInventory();
                        break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nЗавершение программы...");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некорректный ввод, попробуйте снова.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        static void AddAnimal(Zoo zoo)
        {
            Console.Clear();
            MenuDrawer.DrawHeader();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║         Добавление нового животного        ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            int id = InputValidator.GetIntInput("Введите ID животного: ");
            string type = InputValidator.GetAnimalType();
            int food = InputValidator.GetIntInput("Введите количество еды в день (кг): ");

            Animal animal = null;

            if (type.Equals("Monkey", StringComparison.OrdinalIgnoreCase) || type.Equals("Rabbit", StringComparison.OrdinalIgnoreCase))
            {
                int kindness = InputValidator.GetIntInput("Введите уровень доброты (1-10): ", 1, 10);
                if (type.Equals("Monkey", StringComparison.OrdinalIgnoreCase)) animal = new Monkey(id, food, kindness);
                if (type.Equals("Rabbit", StringComparison.OrdinalIgnoreCase)) animal = new Rabbit(id, food, kindness);
            }
            else if (type.Equals("Tiger", StringComparison.OrdinalIgnoreCase) || type.Equals("Wolf", StringComparison.OrdinalIgnoreCase))
            {
                if (type.Equals("Tiger", StringComparison.OrdinalIgnoreCase)) animal = new Tiger(id, food);
                if (type.Equals("Wolf", StringComparison.OrdinalIgnoreCase)) animal = new Wolf(id, food);
            }
            else
            {
                bool isPredator = InputValidator.GetBoolInput("Животное является хищником? (yes/no): ");
                if (!isPredator)
                {
                    int kindness = InputValidator.GetIntInput("Введите уровень доброты (1-10): ", 1, 10);
                    animal = new CustomHerbo(id, food, type, kindness);
                }
                else
                {
                    animal = new CustomPredator(id, food, type);
                }
            }

            animal.IsHealthy = InputValidator.GetBoolInput("Введите состояние здоровья (yes - здоров, no - не здоров): ");

            zoo.AddAnimal(animal);
        }

        static void AddItem(Zoo zoo)
        {
            Console.Clear();
            MenuDrawer.DrawHeader();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║            Добавление предмета             ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            int id = InputValidator.GetIntInput("Введите ID предмета: ");
            string type = InputValidator.GetItemType();

            IInventory item = null;

            if (type == "Table") item = new Table(id);
            else if (type == "Computer") item = new Computer(id);
            else
            {
                item = new CustomThing(id);
            }

            zoo.AddItem(item);
        }
    }
}