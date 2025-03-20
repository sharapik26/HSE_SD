using HSE_Bank.Domain;
using HSE_Bank.Facade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Import
{
    /// <summary>
    /// Абстрактный класс для импортеров данных.
    /// Содержит общую логику импорта данных и предоставляет метод для их обработки.
    /// </summary>
    public abstract class DataImporter
    {
        protected readonly FinancialFacade _facade;

        /// <summary>
        /// Конструктор класса DataImporter.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если facade равен null.</exception>
        public DataImporter(FinancialFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        /// <summary>
        /// Шаблонный метод, осуществляющий импорт данных.
        /// Считывает данные из файла, парсит их и добавляет в систему.
        /// </summary>
        /// <param name="filePath">Путь к файлу, содержащему данные для импорта.</param>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл не найден по указанному пути.</exception>
        public void ImportData(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден.", filePath);

            var content = File.ReadAllText(filePath);
            var parsedData = ParseData(content);
            ProcessData(parsedData);
        }

        /// <summary>
        /// Абстрактный метод для парсинга данных.
        /// Должен быть переопределен в классах-наследниках.
        /// </summary>
        /// <param name="content">Содержимое файла.</param>
        /// <returns>Список операций, разобранных из содержимого файла.</returns>
        protected abstract List<Operation> ParseData(string content);

        /// <summary>
        /// Добавляет данные о операциях в систему.
        /// </summary>
        /// <param name="operations">Список операций для добавления.</param>
        private void ProcessData(List<Operation> operations)
        {
            foreach (var operation in operations)
            {
                _facade.CreateOperation(
                    operation.BankAccountId,
                    operation.Amount,
                    operation.Date,
                    operation.Type,
                    operation.CategoryId,
                    operation.Description
                );
            }

            Console.WriteLine($"Импортировано {operations.Count} операций.");
        }
    }
}
