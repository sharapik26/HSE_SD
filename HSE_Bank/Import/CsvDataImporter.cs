using HSE_Bank.Domain;
using HSE_Bank.Facade;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Import
{
    /// <summary>
    /// Класс для импорта данных из CSV файла.
    /// Наследует от DataImporter и реализует метод для парсинга данных CSV.
    /// </summary>
    public class CsvDataImporter : DataImporter
    {
        /// <summary>
        /// Конструктор, принимающий фасад для работы с финансовыми сущностями.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public CsvDataImporter(FinancialFacade facade) : base(facade) { }

        /// <summary>
        /// Парсит данные из CSV строки и преобразует их в список операций.
        /// </summary>
        /// <param name="content">Содержимое CSV файла в виде строки.</param>
        /// <returns>Список операций, разобранных из CSV.</returns>
        protected override List<Operation> ParseData(string content)
        {
            var operations = new List<Operation>();
            var lines = content.Split('\n').Skip(1); // Пропускаем заголовок

            foreach (var line in lines)
            {
                var fields = line.Split(',');

                if (fields.Length < 6) continue;

                try
                {
                    var operation = new Operation(
                        Guid.Parse(fields[0]), // BankAccountId
                        decimal.Parse(fields[1], CultureInfo.InvariantCulture), // Amount
                        DateTime.Parse(fields[2]), // Date
                        (OperationType)Enum.Parse(typeof(OperationType), fields[3]), // Type
                        Guid.Parse(fields[4]), // CategoryId
                        fields[5] // Description
                    );

                    operations.Add(operation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при разборе строки CSV: {ex.Message}");
                }
            }

            return operations;
        }
    }
}
