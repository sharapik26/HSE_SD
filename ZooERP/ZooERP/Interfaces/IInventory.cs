using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Interfaces
{
    /// <summary>
    /// Интерфейс для определения принадлежности наших типов к категории «инвентаризационная вещь».
    /// </summary>
    internal interface IInventory
    {
        /// <summary>
        /// Свойство для учета инвентаризационных номеров.
        /// </summary>
        int Number { get; }
    }
}