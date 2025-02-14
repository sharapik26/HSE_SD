using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP.Interfaces
{
    /// <summary>
    /// Интерфейс для определения принадлежности наших типов к категории «живых». 
    /// </summary>
    internal interface IAlive
    {
        /// <summary>
        /// Свойство для учета потребляемого количества еды в кг в сутки.
        /// </summary>
        int Food { get; }
    }

}