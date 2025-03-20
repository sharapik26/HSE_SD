using HSE_Bank.Domain;
using HSE_Bank.Facade;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace HSE_Bank.Import
{
    /// <summary>
    /// Класс для импорта данных из формата YAML.
    /// Наследует логику импорта данных от абстрактного класса <see cref="DataImporter"/>.
    /// </summary>
    public class YamlDataImporter : DataImporter
    {
        /// <summary>
        /// Конструктор класса <see cref="YamlDataImporter"/>.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public YamlDataImporter(FinancialFacade facade) : base(facade) { }

        /// <summary>
        /// Реализация метода парсинга данных из YAML.
        /// </summary>
        /// <param name="content">Содержимое YAML-файла.</param>
        /// <returns>Список операций, разобранных из YAML.</returns>
        protected override List<Operation> ParseData(string content)
        {
            try
            {
                var deserializer = new DeserializerBuilder().Build();
                return deserializer.Deserialize<List<Operation>>(content) ?? new List<Operation>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при разборе YAML: {ex.Message}");
                return new List<Operation>();
            }
        }
    }
}
