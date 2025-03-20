using HSE_Bank.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Export
{
    /// <summary>
    /// Класс для экспорта данных о операциях в формат JSON.
    /// </summary>
    public class JsonDataExportVisitor : IDataExportVisitor
    {
        /// <summary>
        /// Экспортирует список операций в строку в формате JSON.
        /// </summary>
        /// <param name="operations">Список операций для экспорта.</param>
        /// <returns>Строка, представляющая данные операций в формате JSON.</returns>
        public string Export(List<Operation> operations)
        {
            return JsonConvert.SerializeObject(operations, Formatting.Indented);
        }
    }
}
