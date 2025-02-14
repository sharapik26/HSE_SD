using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Classes
{
    /// <summary>
    /// Класс, представляющий пользовательское хищное животное.
    /// Наследуется от класса Predator.
    /// </summary>
    public class CustomPredator : Predator
    {
        /// <summary>
        /// Вид животного (например, "Lion", "Crocodile" и т.д.).
        /// </summary>
        public string Species { get; }

        /// <summary>
        /// Конструктор для создания экземпляра пользовательского хищного животного.
        /// </summary>
        /// <param name="number">Уникальный идентификатор животного.</param>
        /// <param name="food">Количество еды (в кг), которое животное потребляет в день.</param>
        /// <param name="species">Вид животного (например, "Lion", "Crocodile" и т.д.).</param>
        public CustomPredator(int number, int food, string species) : base(number, food)
        {
            Species = species;
        }
    }
}