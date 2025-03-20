using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace HSE_Bank.Export
{
    /// <summary>
    /// Класс для экспорта данных о операциях в формат YAML.
    /// </summary>
    public class YamlDataExportVisitor : IDataExportVisitor
    {
        /// <summary>
        /// Экспортирует список операций в строку в формате YAML.
        /// </summary>
        /// <param name="operations">Список операций для экспорта.</param>
        /// <returns>Строка, представляющая данные операций в формате YAML.</returns>
        public string Export(List<Operation> operations)
        {
            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(operations);
        }
    }
}
