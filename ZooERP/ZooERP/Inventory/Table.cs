using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Inventory
{
    /// <summary>
    /// Класс, представляющий стол. Наследуется от класса Thing.
    /// </summary>
    public class Table : Thing
    {
        /// <summary>
        /// Конструктор для создания экземпляра стола.
        /// </summary>
        /// <param name="number">Уникальный идентификатор предмета (стола).</param>
        public Table(int number) : base(number) { }
    }
}