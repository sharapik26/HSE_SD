using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Export
{
    /// <summary>
    /// Интерфейс для классов, которые реализуют экспорт данных операций в различные форматы.
    /// </summary>
    public interface IDataExportVisitor
    {
        /// <summary>
        /// Экспортирует список операций в строку в заданном формате.
        /// </summary>
        /// <param name="operations">Список операций для экспорта.</param>
        /// <returns>Строка, представляющая данные операций в определенном формате.</returns>
        string Export(List<Operation> operations);
    }
}
