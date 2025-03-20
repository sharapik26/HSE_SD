using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Domain
{
    /// <summary>
    /// Тип операции, определяющий, является ли операция доходом или расходом.
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Операция дохода.
        /// </summary>
        Income,

        /// <summary>
        /// Операция расхода.
        /// </summary>
        Expense
    }

    /// <summary>
    /// Класс, представляющий операцию на банковском счете.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Уникальный идентификатор операции.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Тип операции (доход или расход).
        /// </summary>
        public OperationType Type { get; }

        /// <summary>
        /// Идентификатор банковского счета, с которым связана операция.
        /// </summary>
        public Guid BankAccountId { get; }

        /// <summary>
        /// Сумма операции.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Дата операции.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Описание операции (может быть пустым).
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Идентификатор категории, к которой относится операция.
        /// </summary>
        public Guid CategoryId { get; }

        /// <summary>
        /// Инициализирует новый экземпляр операции с заданными параметрами.
        /// </summary>
        /// <param name="bankAccountId">Идентификатор банковского счета, с которым связана операция.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="type">Тип операции (доход или расход).</param>
        /// <param name="categoryId">Идентификатор категории операции.</param>
        /// <param name="description">Описание операции (по умолчанию равно null).</param>
        /// <exception cref="ArgumentException">Бросается, если сумма операции неположительная.</exception>
        public Operation(Guid bankAccountId, decimal amount, DateTime date, OperationType type, Guid categoryId, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма операции должна быть положительной.", nameof(amount));

            Id = Guid.NewGuid();
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Type = type;
            CategoryId = categoryId;
            Description = description;
        }
    }
}
