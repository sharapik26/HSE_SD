using HSE_Bank.Facade;
using HSE_Bank.Commands;
using HSE_Bank.Domain;
using System;

namespace HSE_Bank.Managers
{
    /// <summary>
    /// Класс для управления финансовыми операциями, такими как создание счетов, категорий и операций.
    /// </summary>
    public class FinancialManager
    {
        private readonly FinancialFacade _facade;

        /// <summary>
        /// Конструктор класса <see cref="FinancialManager"/>.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public FinancialManager(FinancialFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Метод для создания нового банковского счета.
        /// </summary>
        public void CreateBankAccount()
        {
            Console.Write("Введите название счета: ");
            string name = Console.ReadLine();
            Console.Write("Введите начальный баланс: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                var command = new CreateBankAccountCommand(_facade, name, initialBalance);
                new TimingDecorator(command).Execute(); // Декоратор для отслеживания времени выполнения команды
            }
            else
            {
                Console.WriteLine("Ошибка: некорректная сумма.");
            }
        }

        /// <summary>
        /// Метод для создания новой категории.
        /// </summary>
        public void CreateCategory()
        {
            Console.Write("Введите название категории: ");
            string name = Console.ReadLine();
            Console.Write("Введите тип (0 - Доход, 1 - Расход): ");

            if (Enum.TryParse(Console.ReadLine(), out CategoryType type))
            {
                var category = _facade.CreateCategory(name, type);
                Console.WriteLine($"Категория успешно создана. ID: {category.Id}");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный тип категории.");
            }
        }

        /// <summary>
        /// Метод для создания новой операции.
        /// </summary>
        public void CreateOperation()
        {
            Console.Write("Введите ID счета: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid accountId))
            {
                Console.Write("Введите сумму: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.Write("Введите дату (гггг-мм-дд): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        Console.Write("Введите тип операции (0 - Доход, 1 - Расход): ");
                        if (Enum.TryParse(Console.ReadLine(), out OperationType type))
                        {
                            Console.Write("Введите ID категории: ");
                            if (Guid.TryParse(Console.ReadLine(), out Guid categoryId))
                            {
                                Console.Write("Введите описание (необязательно): ");
                                string description = Console.ReadLine();

                                var command = new CreateOperationCommand(_facade, accountId, amount, date, type, categoryId, description);
                                new TimingDecorator(command).Execute(); // Декоратор для отслеживания времени выполнения команды
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка: некорректный ввод.");
            }
        }

        /// <summary>
        /// Метод для отображения списка всех счетов.
        /// </summary>
        public void ShowAccounts()
        {
            var accounts = _facade.GetAccounts();
            if (!accounts.Any())
            {
                Console.WriteLine("Счетов пока нет.");
                return;
            }

            Console.WriteLine("\nСписок счетов:");
            foreach (var account in accounts)
            {
                Console.WriteLine($"ID: {account.Id} | Название: {account.Name} | Баланс: {account.Balance}");
            }
        }
    }
}
