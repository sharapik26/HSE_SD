using System;
using HSE_Bank.Facade;
using HSE_Bank.Domain;

namespace HSE_Bank.Commands
{
    /// <summary>
    /// Команда для создания банковского счета.
    /// </summary>
    public class CreateBankAccountCommand : ICommand
    {
        private readonly FinancialFacade _facade;
        private readonly string _name;
        private readonly decimal _initialBalance;
        private BankAccount? _createdAccount;

        /// <summary>
        /// Инициализирует новую команду для создания банковского счета.
        /// </summary>
        /// <param name="facade">Фасад финансовой системы</param>
        /// <param name="name">Название банковского счета</param>
        /// <param name="initialBalance">Начальный баланс</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="facade"/> равно null</exception>
        public CreateBankAccountCommand(FinancialFacade facade, string name, decimal initialBalance)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _name = name;
            _initialBalance = initialBalance;
        }

        /// <summary>
        /// Выполняет команду создания банковского счета.
        /// </summary>
        public void Execute()
        {
            _createdAccount = _facade.CreateBankAccount(_name, _initialBalance);
            Console.WriteLine($"Создан счет: {_createdAccount.Name} (ID: {_createdAccount.Id}) с балансом {_createdAccount.Balance}");
        }
    }
}
