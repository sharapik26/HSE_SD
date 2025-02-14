using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;

namespace ZooERP.Animals
{
    /// <summary>
    /// Класс, представляющий животное "Волк". Наследуется от класса Predator.
    /// </summary>
    public class Wolf : Predator
    {
        /// <summary>
        /// Конструктор для создания экземпляра волка.
        /// </summary>
        /// <param name="number">Уникальный идентификатор волка.</param>
        /// <param name="food">Количество еды (в кг), которое волк потребляет в день.</param>
        public Wolf(int number, int food) : base(number, food) { }
    }
}