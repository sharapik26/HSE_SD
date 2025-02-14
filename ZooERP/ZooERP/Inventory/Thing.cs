using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Interfaces;

namespace ZooERP.Inventory
{
    /// <summary>
    /// Абстрактный класс, представляющий предмет в инвентаре.
    /// Наследуется от интерфейса IInventory.
    /// </summary>
    public abstract class Thing : IInventory
    {
        /// <summary>
        /// Уникальный идентификатор предмета.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Конструктор для создания экземпляра предмета.
        /// </summary>
        /// <param name="number">Уникальный идентификатор предмета.</param>
        public Thing(int number) { Number = number; }
    }
}