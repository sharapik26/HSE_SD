using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Data
{
    /// <summary>
    /// Реализация репозитория данных, которая сохраняет и извлекает информацию о банковских счетах и операциях.
    /// </summary>
    public class DatabaseRepository : IDataRepository
    {
        private readonly List<BankAccount> _accounts = new();
        private readonly List<Operation> _operations = new();

        /// <summary>
        /// Сохраняет информацию о банковском счете.
        /// </summary>
        /// <param name="account">Банковский счет для сохранения.</param>
        public void SaveAccount(BankAccount account)
        {
            _accounts.Add(account);
        }

        /// <summary>
        /// Получает информацию о банковском счете по его идентификатору.
        /// </summary>
        /// <param name="accountId">Идентификатор банковского счета.</param>
        /// <returns>Банковский счет с указанным идентификатором, либо null, если счет не найден.</returns>
        public BankAccount GetAccount(Guid accountId)
        {
            return _accounts.FirstOrDefault(acc => acc.Id == accountId);
        }

        /// <summary>
        /// Получает все банковские счета.
        /// </summary>
        /// <returns>Список всех банковских счетов.</returns>
        public List<BankAccount> GetAllAccounts()
        {
            return _accounts;
        }

        /// <summary>
        /// Сохраняет информацию об операции.
        /// </summary>
        /// <param name="operation">Операция для сохранения.</param>
        public void SaveOperation(Operation operation)
        {
            _operations.Add(operation);
        }

        /// <summary>
        /// Получает все операции.
        /// </summary>
        /// <returns>Список всех операций.</returns>
        public List<Operation> GetAllOperations()
        {
            return _operations;
        }
    }
}
