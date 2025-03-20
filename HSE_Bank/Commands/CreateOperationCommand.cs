using System;
using HSE_Bank.Domain;
using HSE_Bank.Facade;

namespace HSE_Bank.Commands
{
    /// <summary>
    /// Команда для создания финансовой операции.
    /// </summary>
    public class CreateOperationCommand : ICommand
    {
        private readonly FinancialFacade _facade;
        private readonly Guid _accountId;
        private readonly decimal _amount;
        private readonly DateTime _date;
        private readonly OperationType _type;
        private readonly Guid _categoryId;
        private readonly string? _description;

        /// <summary>
        /// Инициализирует новую команду для создания операции.
        /// </summary>
        /// <param name="facade">Фасад финансовой системы.</param>
        /// <param name="accountId">Идентификатор банковского счета.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="type">Тип операции (Доход или Расход).</param>
        /// <param name="categoryId">Идентификатор категории операции.</param>
        /// <param name="description">Описание операции (необязательно).</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="facade"/> равно null.</exception>
        public CreateOperationCommand(FinancialFacade facade, Guid accountId, decimal amount, DateTime date, OperationType type, Guid categoryId, string? description = null)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
            _accountId = accountId;
            _amount = amount;
            _date = date;
            _type = type;
            _categoryId = categoryId;
            _description = description;
        }

        /// <summary>
        /// Выполняет команду создания операции и выводит информацию в консоль.
        /// </summary>
        public void Execute()
        {
            var operation = _facade.CreateOperation(_accountId, _amount, _date, _type, _categoryId, _description);
            Console.WriteLine($"Создана операция: {_type} на сумму {_amount} (ID: {operation.Id}, {_description ?? "Без описания"})");
        }
    }
}
