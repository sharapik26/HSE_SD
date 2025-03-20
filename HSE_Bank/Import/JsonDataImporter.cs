using System;
using System.Collections.Generic;
using HSE_Bank.Domain;
using HSE_Bank.Facade;
using Newtonsoft.Json;

namespace HSE_Bank.Import
{
    /// <summary>
    /// Класс для импорта данных из формата JSON.
    /// Наследует логику импорта данных от абстрактного класса <see cref="DataImporter"/>.
    /// </summary>
    public class JsonDataImporter : DataImporter
    {
        /// <summary>
        /// Конструктор класса <see cref="JsonDataImporter"/>.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public JsonDataImporter(FinancialFacade facade) : base(facade) { }

        /// <summary>
        /// Реализация метода парсинга данных из JSON.
        /// </summary>
        /// <param name="content">Содержимое JSON-файла.</param>
        /// <returns>Список операций, разобранных из JSON.</returns>
        protected override List<Operation> ParseData(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Operation>>(content) ?? new List<Operation>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при разборе JSON: {ex.Message}");
                return new List<Operation>();
            }
        }
    }
}
