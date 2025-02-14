using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Classes
{
    /// <summary>
    /// Класс, представляющий пользовательское травоядное животное.
    /// Наследуется от класса Herbo.
    /// </summary>
    public class CustomHerbo : Herbo
    {
        /// <summary>
        /// Вид животного (например, "Giraffe", "Zebra" и т.д.).
        /// </summary>
        public string Species { get; }

        /// <summary>
        /// Конструктор для создания экземпляра пользовательского травоядного животного.
        /// </summary>
        /// <param name="number">Уникальный идентификатор животного.</param>
        /// <param name="food">Количество еды (в кг), которое животное потребляет в день.</param>
        /// <param name="species">Вид животного (например, "Giraffe", "Zebra" и т.д.).</param>
        /// <param name="kindness">Уровень доброты животного (от 1 до 10).</param>
        public CustomHerbo(int number, int food, string species, int kindness) : base(number, food, kindness)
        {
            Species = species;
        }
    }
}