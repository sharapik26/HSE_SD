using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Inventory
{
    /// <summary>
    /// Класс, представляющий компьютер. Наследуется от класса Thing.
    /// </summary>
    public class Computer : Thing
    {
        /// <summary>
        /// Конструктор для создания экземпляра компьютера.
        /// </summary>
        /// <param name="number">Уникальный идентификатор предмета (компьютера).</param>
        public Computer(int number) : base(number) { }
    }
}