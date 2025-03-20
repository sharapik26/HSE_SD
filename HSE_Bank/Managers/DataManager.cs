using HSE_Bank.Facade;
using HSE_Bank.Import;
using HSE_Bank.Export;
using System;

namespace HSE_Bank.Managers
{
    /// <summary>
    /// Класс для управления импортом и экспортом данных.
    /// </summary>
    public class DataManager
    {
        private readonly FinancialFacade _facade;

        /// <summary>
        /// Конструктор класса <see cref="DataManager"/>.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public DataManager(FinancialFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Метод для импорта данных из файла.
        /// Пользователь выбирает формат (CSV, JSON, YAML) и указывает путь к файлу.
        /// </summary>
        public void ImportData()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();
            Console.Write("Выберите формат (csv, json, yaml): ");
            string format = Console.ReadLine()?.ToLower();

            DataImporter importer = format switch
            {
                "csv" => new CsvDataImporter(_facade),
                "json" => new JsonDataImporter(_facade),
                "yaml" => new YamlDataImporter(_facade),
                _ => null
            };

            if (importer != null)
            {
                importer.ImportData(filePath);
            }
            else
            {
                Console.WriteLine("Ошибка: неверный формат.");
            }
        }

        /// <summary>
        /// Метод для экспорта данных в файл.
        /// Пользователь выбирает формат (CSV, JSON, YAML) и указывает путь к файлу.
        /// </summary>
        public void ExportData()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();
            Console.Write("Выберите формат (csv, json, yaml): ");
            string format = Console.ReadLine()?.ToLower();

            IDataExportVisitor visitor = format switch
            {
                "csv" => new CsvDataExportVisitor(),
                "json" => new JsonDataExportVisitor(),
                "yaml" => new YamlDataExportVisitor(),
                _ => null
            };

            if (visitor != null)
            {
                _facade.ExportData(visitor, filePath);
            }
            else
            {
                Console.WriteLine("Ошибка: неверный формат.");
            }
        }
    }
}
