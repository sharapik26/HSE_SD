using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;

namespace ZooERP.Animals
{
    /// <summary>
    /// Класс, представляющий животное "Тигр". Наследуется от класса Predator.
    /// </summary>
    public class Tiger : Predator
    {
        /// <summary>
        /// Конструктор для создания экземпляра тигра.
        /// </summary>
        /// <param name="number">Уникальный идентификатор тигра.</param>
        /// <param name="food">Количество еды (в кг), которое тигр потребляет в день.</param>
        public Tiger(int number, int food) : base(number, food) { }
    }
}