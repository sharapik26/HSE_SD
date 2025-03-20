using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Domain
{
    /// <summary>
    /// Статический класс, предоставляющий методы для создания различных финансовых сущностей.
    /// </summary>
    public static class FinancialEntityFactory
    {
        /// <summary>
        /// Создает новый банковский счет с указанным именем и начальным балансом.
        /// </summary>
        /// <param name="name">Название счета.</param>
        /// <param name="initialBalance">Начальный баланс счета.</param>
        /// <returns>Новый объект банковского счета.</returns>
        /// <exception cref="ArgumentException">Бросается, если название счета пустое или начальный баланс отрицательный.</exception>
        public static BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не может быть пустым.");
            if (initialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным.");

            return new BankAccount(name, initialBalance);
        }

        /// <summary>
        /// Создает новую категорию с указанным типом и именем.
        /// </summary>
        /// <param name="type">Тип категории (доход или расход).</param>
        /// <param name="name">Название категории.</param>
        /// <returns>Новый объект категории.</returns>
        /// <exception cref="ArgumentException">Бросается, если название категории пустое.</exception>
        public static Category CreateCategory(CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым.");

            return new Category(name, type);
        }

        /// <summary>
        /// Создает новую операцию с указанными параметрами.
        /// </summary>
        /// <param name="bankAccountId">Идентификатор банковского счета, с которым связана операция.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="type">Тип операции (доход или расход).</param>
        /// <param name="categoryId">Идентификатор категории, к которой относится операция.</param>
        /// <param name="description">Описание операции.</param>
        /// <returns>Новый объект операции.</returns>
        /// <exception cref="ArgumentException">Бросается, если сумма операции неположительная.</exception>
        public static Operation CreateOperation(Guid bankAccountId, decimal amount, DateTime date, OperationType type, Guid categoryId, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма операции должна быть положительной.");

            return new Operation(bankAccountId, amount, date, type, categoryId, description);
        }
    }
}
