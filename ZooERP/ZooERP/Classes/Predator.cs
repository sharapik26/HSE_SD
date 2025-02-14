using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Classes
{
    /// <summary>
    /// Абстрактный класс, представляющий хищное животное.
    /// Наследуется от класса Animal.
    /// </summary>
    public abstract class Predator : Animal
    {
        /// <summary>
        /// Конструктор для создания экземпляра хищного животного.
        /// </summary>
        /// <param name="number">Уникальный идентификатор животного.</param>
        /// <param name="food">Количество еды (в кг), которое животное потребляет в день.</param>
        public Predator(int number, int food) : base(number, food) { }
    }
}