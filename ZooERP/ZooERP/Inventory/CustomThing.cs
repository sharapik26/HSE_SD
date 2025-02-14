using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Inventory
{
    /// <summary>
    /// Класс, представляющий пользовательский предмет. Наследуется от класса Thing.
    /// </summary>
    public class CustomThing : Thing
    {
        /// <summary>
        /// Конструктор для создания экземпляра пользовательского предмета.
        /// </summary>
        /// <param name="number">Уникальный идентификатор предмета.</param>
        public CustomThing(int number) : base(number) { }
    }
}