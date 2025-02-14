using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;

namespace ZooERP.Animals
{
    /// <summary>
    /// Класс, представляющий животное "Обезьяна". Наследуется от класса Herbo.
    /// </summary>
    public class Monkey : Herbo
    {
        /// <summary>
        /// Конструктор для создания экземпляра обезьяны.
        /// </summary>
        /// <param name="number">Уникальный идентификатор обезьяны.</param>
        /// <param name="food">Количество еды (в кг), которое обезьяна потребляет в день.</param>
        /// <param name="kindness">Уровень доброты обезбяны (от 1 до 10).</param>
        public Monkey(int number, int food, int kindness) : base(number, food, kindness) { }
    }
}