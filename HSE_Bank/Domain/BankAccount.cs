using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Domain
{
    /// <summary>
    /// Класс, представляющий банковский счет.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Уникальный идентификатор банковского счета.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название банковского счета.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Баланс банковского счета.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр банковского счета с указанным именем и начальным балансом.
        /// </summary>
        /// <param name="name">Название счета.</param>
        /// <param name="initialBalance">Начальный баланс счета.</param>
        /// <exception cref="ArgumentException">Бросается, если имя счета пустое или начальный баланс отрицательный.</exception>
        public BankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не может быть пустым.", nameof(name));

            if (initialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным.", nameof(initialBalance));

            Id = Guid.NewGuid();
            Name = name;
            Balance = initialBalance;
        }

        /// <summary>
        /// Вносит деньги на счет.
        /// </summary>
        /// <param name="amount">Сумма депозита.</param>
        /// <exception cref="ArgumentException">Бросается, если сумма депозита отрицательная или нулевая.</exception>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма депозита должна быть положительной.", nameof(amount));

            Balance += amount;
        }

        /// <summary>
        /// Снимает деньги со счета.
        /// </summary>
        /// <param name="amount">Сумма снятия.</param>
        /// <exception cref="ArgumentException">Бросается, если сумма снятия отрицательная или нулевая.</exception>
        /// <exception cref="InvalidOperationException">Бросается, если на счете недостаточно средств для снятия.</exception>
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма снятия должна быть положительной.", nameof(amount));

            if (Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счете.");

            Balance -= amount;
        }

        /// <summary>
        /// Переименовывает банковский счет.
        /// </summary>
        /// <param name="newName">Новое имя счета.</param>
        /// <exception cref="ArgumentException">Бросается, если новое имя счета пустое.</exception>
        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Название счета не может быть пустым.", nameof(newName));

            Name = newName;
        }
    }
}
