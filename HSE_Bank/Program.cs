using System;
using HSE_Bank.Facade;
using HSE_Bank.Managers;
using HSE_Bank.Import;
using HSE_Bank.Export;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main()
    {
        // 1. Создаем и настраиваем DI-контейнер
        var serviceProvider = new ServiceCollection()
            .AddSingleton<FinancialFacade>() // Singleton - фасад должен быть один на всё приложение
            .AddTransient<FinancialManager>() // Transient - создается при каждом вызове
            .AddTransient<AnalyticsManager>()
            .AddTransient<DataManager>()

            // Импорт и экспорт
            .AddTransient<CsvDataImporter>()
            .AddTransient<JsonDataImporter>()
            .AddTransient<YamlDataImporter>()
            .AddTransient<CsvDataExportVisitor>()
            .AddTransient<JsonDataExportVisitor>()
            .AddTransient<YamlDataExportVisitor>()

            .BuildServiceProvider();

        // 2. Разрешаем зависимости
        var financialManager = serviceProvider.GetRequiredService<FinancialManager>();
        var analyticsManager = serviceProvider.GetRequiredService<AnalyticsManager>();
        var dataManager = serviceProvider.GetRequiredService<DataManager>();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Создать банковский счет");
            Console.WriteLine("2. Создать категорию");
            Console.WriteLine("3. Добавить операцию");
            Console.WriteLine("4. Аналитика");
            Console.WriteLine("5. Импорт данных");
            Console.WriteLine("6. Экспорт данных");
            Console.WriteLine("7. Показать список счетов");
            Console.WriteLine("8. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    financialManager.CreateBankAccount();
                    break;
                case "2":
                    financialManager.CreateCategory();
                    break;
                case "3":
                    financialManager.CreateOperation();
                    break;
                case "4":
                    analyticsManager.ShowAnalytics();
                    break;
                case "5":
                    dataManager.ImportData();
                    break;
                case "6":
                    dataManager.ExportData();
                    break;
                case "7":
                    financialManager.ShowAccounts();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }
        }
    }
}
