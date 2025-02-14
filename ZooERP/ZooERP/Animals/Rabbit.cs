using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;

namespace ZooERP.Animals
{
    /// <summary>
    /// Класс, представляющий животное "Кролик". Наследуется от класса Herbo.
    /// </summary>
    public class Rabbit : Herbo
    {
        /// <summary>
        /// Конструктор для создания экземпляра кролика.
        /// </summary>
        /// <param name="number">Уникальный идентификатор кролика.</param>
        /// <param name="food">Количество еды (в кг), которое кролик потребляет в день.</param>
        /// <param name="kindness">Уровень доброты кролика (от 1 до 10).</param>
        public Rabbit(int number, int food, int kindness) : base(number, food, kindness) { }
    }
}