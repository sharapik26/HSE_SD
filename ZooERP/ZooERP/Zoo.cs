using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;
using ZooERP.Interfaces;

namespace ZooERP
{
    /// <summary>
    /// Класс, представляющий зоопарк.
    /// Управляет списком животных и инвентаря, а также осуществляет проверку здоровья животных через ветеринарную клинику.
    /// </summary>
    public class Zoo
    {
        private readonly List<Animal> _animals = new();
        private readonly List<IInventory> _inventory = new();
        private readonly VetClinic _vetClinic;

        /// <summary>
        /// Конструктор для создания экземпляра зоопарка.
        /// </summary>
        /// <param name="vetClinic">Ветеринарная клиника для проверки здоровья животных.</param>
        public Zoo(VetClinic vetClinic)
        {
            _vetClinic = vetClinic;
        }

        /// <summary>
        /// Добавляет животное в зоопарк, если оно прошло проверку здоровья.
        /// </summary>
        /// <param name="animal">Животное, которое необходимо добавить в зоопарк.</param>
        public void AddAnimal(Animal animal)
        {
            if (_vetClinic.CheckHealth(animal))
            {
                _animals.Add(animal);
                _inventory.Add(animal);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Животное с ID {animal.Number} добавлено в зоопарк.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Животное с ID {animal.Number} не прошло проверку здоровья.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Добавляет предмет в инвентарь зоопарка.
        /// </summary>
        /// <param name="item">Предмет для добавления в инвентарь.</param>
        internal void AddItem(IInventory item)
        {
            _inventory.Add(item);
        }

        /// <summary>
        /// Выводит общее потребление еды для всех животных в зоопарке.
        /// </summary>
        public void PrintFoodConsumption()
        {
            int totalFood = _animals.Sum(a => a.Food);
            Console.WriteLine($"Общее потребление еды в день: {totalFood} кг.");
        }

        /// <summary>
        /// Выводит список животных, которые могут быть в контактном зоопарке (с уровнем доброты выше 5).
        /// </summary>
        public void PrintContactZooAnimals()
        {
            var friendlyAnimals = _animals.OfType<Herbo>().Where(h => h.Kindness > 5);
            Console.WriteLine("Животные для контактного зоопарка:");
            foreach (var animal in friendlyAnimals)
            {
                string species = animal is CustomHerbo customHerbo ? customHerbo.Species : animal.GetType().Name;

                Console.WriteLine($"ID: {animal.Number}, Вид: {species}, Еда в день: {animal.Food} кг, Доброта: {animal.Kindness}/10");
            }
        }


        /// <summary>
        /// Выводит список всех предметов и животных в инвентаре зоопарка.
        /// </summary>
        public void PrintInventory()
        {
            Console.WriteLine("Инвентарные номера и объекты зоопарка:");
            foreach (var item in _inventory)
            {
                if (item is Animal animal)
                {
                    string species = animal is CustomHerbo customHerbo ? customHerbo.Species : animal.GetType().Name;

                    Console.WriteLine($"ID: {animal.Number}, Вид: {species}, Еда в день: {animal.Food} кг.");
                }
                else
                {
                    Console.WriteLine($"ID: {item.Number}, Тип: {item.GetType().Name}");
                }
            }
        }
    }
}