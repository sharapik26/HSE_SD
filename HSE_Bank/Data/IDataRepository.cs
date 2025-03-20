using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Data
{
    /// <summary>
    /// Интерфейс для репозитория данных, который управляет сохранением и извлечением данных о банковских счетах и операциях.
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Сохраняет информацию о банковском счете.
        /// </summary>
        /// <param name="account">Банковский счет для сохранения.</param>
        void SaveAccount(BankAccount account);

        /// <summary>
        /// Получает информацию о банковском счете по его идентификатору.
        /// </summary>
        /// <param name="accountId">Идентификатор банковского счета.</param>
        /// <returns>Банковский счет с указанным идентификатором.</returns>
        BankAccount GetAccount(Guid accountId);

        /// <summary>
        /// Получает все банковские счета.
        /// </summary>
        /// <returns>Список всех банковских счетов.</returns>
        List<BankAccount> GetAllAccounts();

        /// <summary>
        /// Сохраняет информацию об операции.
        /// </summary>
        /// <param name="operation">Операция для сохранения.</param>
        void SaveOperation(Operation operation);

        /// <summary>
        /// Получает все операции.
        /// </summary>
        /// <returns>Список всех операций.</returns>
        List<Operation> GetAllOperations();
    }
}
