using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Interfaces;

namespace ZooERP.Classes
{
    /// <summary>
    /// Абстрактный класс, представляющий животное в зоопарке.
    /// Наследуется от интерфейсов IAlive и IInventory.
    /// </summary>
    public abstract class Animal : IAlive, IInventory
    {
        /// <summary>
        /// Количество еды (в кг), которое животное потребляет в день.
        /// </summary>
        public int Food { get; protected set; }

        /// <summary>
        /// Уникальный идентификатор животного.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Статус здоровья животного (true - здорово, false - не здорово).
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// Конструктор для создания экземпляра животного.
        /// </summary>
        /// <param name="number">Уникальный идентификатор животного.</param>
        /// <param name="food">Количество еды (в кг), которое животное потребляет в день.</param>
        public Animal(int number, int food)
        {
            Number = number;
            Food = food;
        }
    }
}