using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Classes
{
    /// <summary>
    /// Абстрактный класс, представляющий травоядное животное.
    /// Наследуется от класса Animal.
    /// </summary>
    public abstract class Herbo : Animal
    {
        /// <summary>
        /// Уровень доброты животного (от 1 до 10).
        /// </summary>
        public int Kindness { get; }

        /// <summary>
        /// Конструктор для создания экземпляра травоядного животного.
        /// </summary>
        /// <param name="number">Уникальный идентификатор животного.</param>
        /// <param name="food">Количество еды (в кг), которое животное потребляет в день.</param>
        /// <param name="kindness">Уровень доброты животного (от 1 до 10).</param>
        public Herbo(int number, int food, int kindness) : base(number, food)
        {
            Kindness = kindness;
        }
    }
}