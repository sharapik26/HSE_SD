using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSE_Bank.Domain;
using HSE_Bank.Export;

namespace HSE_Bank.Facade
{
    /// <summary>
    /// Класс фасада для работы с финансовыми сущностями, такими как счета, категории и операции.
    /// </summary>
    public class FinancialFacade
    {
        private readonly Dictionary<Guid, BankAccount> _accounts = new();
        private readonly Dictionary<Guid, Category> _categories = new();
        private readonly List<Operation> _operations = new();

        /// <summary>
        /// Создает новый банковский счет.
        /// </summary>
        /// <param name="name">Название счета.</param>
        /// <param name="initialBalance">Начальный баланс счета.</param>
        /// <returns>Созданный банковский счет.</returns>
        public BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            var account = FinancialEntityFactory.CreateBankAccount(name, initialBalance);
            _accounts[account.Id] = account;
            return account;
        }

        /// <summary>
        /// Создает новую категорию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <param name="type">Тип категории (доход или расход).</param>
        /// <returns>Созданная категория.</returns>
        public Category CreateCategory(string name, CategoryType type)
        {
            var category = FinancialEntityFactory.CreateCategory(type, name);
            _categories[category.Id] = category;
            return category;
        }

        /// <summary>
        /// Создает новую операцию для счета.
        /// </summary>
        /// <param name="accountId">ID счета, на который будет произведена операция.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="type">Тип операции (доход или расход).</param>
        /// <param name="categoryId">ID категории операции.</param>
        /// <param name="description">Описание операции (необязательный параметр).</param>
        /// <returns>Созданная операция.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если счет или категория не найдены.</exception>
        public Operation CreateOperation(Guid accountId, decimal amount, DateTime date, OperationType type, Guid categoryId, string? description = null)
        {
            if (!_accounts.ContainsKey(accountId))
                throw new ArgumentException("Счет не найден.");

            if (!_categories.ContainsKey(categoryId))
                throw new ArgumentException("Категория не найдена.");

            var operation = FinancialEntityFactory.CreateOperation(accountId, amount, date, type, categoryId, description);
            _operations.Add(operation);

            // Автоматическое обновление баланса счета
            if (type == OperationType.Income)
                _accounts[accountId].Deposit(amount);
            else
                _accounts[accountId].Withdraw(amount);

            return operation;
        }

        /// <summary>
        /// Получает список всех операций.
        /// </summary>
        /// <returns>Список операций.</returns>
        public List<Operation> GetOperations()
        {
            return _operations;
        }

        /// <summary>
        /// Получает список всех счетов.
        /// </summary>
        /// <returns>Перечень всех счетов.</returns>
        public IEnumerable<BankAccount> GetAccounts()
        {
            return _accounts.Values;
        }

        /// <summary>
        /// Получает список всех категорий.
        /// </summary>
        /// <returns>Перечень всех категорий.</returns>
        public IEnumerable<Category> GetCategories()
        {
            return _categories.Values;
        }

        /// <summary>
        /// Подсчитывает разницу между доходами и расходами за указанный период.
        /// </summary>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endDate">Дата конца периода.</param>
        /// <returns>Разница между доходами и расходами.</returns>
        public decimal CalculateBalanceDifference(DateTime startDate, DateTime endDate)
        {
            var income = _operations.Where(o => o.Type == OperationType.Income && o.Date >= startDate && o.Date <= endDate).Sum(o => o.Amount);
            var expense = _operations.Where(o => o.Type == OperationType.Expense && o.Date >= startDate && o.Date <= endDate).Sum(o => o.Amount);
            return income - expense;
        }

        /// <summary>
        /// Экспортирует данные о операциях в указанный файл с использованием заданного формата.
        /// </summary>
        /// <param name="visitor">Экспортер данных, реализующий формат экспорта (например, CSV, JSON, YAML).</param>
        /// <param name="filePath">Путь к файлу, в который будут экспортированы данные.</param>
        public void ExportData(IDataExportVisitor visitor, string filePath)
        {
            var data = visitor.Export(GetOperations());
            System.IO.File.WriteAllText(filePath, data);
            Console.WriteLine($"Данные экспортированы в {filePath}");
        }
    }
}
