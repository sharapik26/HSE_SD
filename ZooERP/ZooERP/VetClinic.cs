using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooERP.Classes;

namespace ZooERP
{
    /// <summary>
    /// Класс, представляющий ветеринарную клинику, отвечающую за проверку здоровья животных.
    /// </summary>
    public class VetClinic
    {
        /// <summary>
        /// Проверяет состояние здоровья животного.
        /// </summary>
        /// <param name="animal">Животное, состояние здоровья которого нужно проверить.</param>
        /// <returns>Возвращает true, если животное здорово; иначе false.</returns>
        public bool CheckHealth(Animal animal)
        {
            return animal.IsHealthy;
        }
    }
}