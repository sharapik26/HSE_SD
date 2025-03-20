using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Data
{
    /// <summary>
    /// Прокси-класс, который кэширует данные и взаимодействует с реальным репозиторием данных.
    /// </summary>
    public class InMemoryRepositoryProxy : IDataRepository
    {
        private readonly IDataRepository _realRepository;
        private List<BankAccount> _cachedAccounts;
        private List<Operation> _cachedOperations;

        /// <summary>
        /// Инициализирует новый экземпляр прокси-репозитория с указанным реальным репозиторием.
        /// </summary>
        /// <param name="realRepository">Реальный репозиторий, с которым прокси будет взаимодействовать.</param>
        public InMemoryRepositoryProxy(IDataRepository realRepository)
        {
            _realRepository = realRepository;
            _cachedAccounts = new List<BankAccount>();
            _cachedOperations = new List<Operation>();
        }

        /// <summary>
        /// Сохраняет информацию о банковском счете в реальном репозитории и кэширует его.
        /// </summary>
        /// <param name="account">Банковский счет для сохранения.</param>
        public void SaveAccount(BankAccount account)
        {
            _realRepository.SaveAccount(account);
            _cachedAccounts.Add(account);
        }

        /// <summary>
        /// Получает информацию о банковском счете из кэша, если она там есть, или из реального репозитория, если нет.
        /// </summary>
        /// <param name="accountId">Идентификатор банковского счета.</param>
        /// <returns>Банковский счет с указанным идентификатором или null, если счет не найден.</returns>
        public BankAccount GetAccount(Guid accountId)
        {
            var account = _cachedAccounts.FirstOrDefault(acc => acc.Id == accountId);
            if (account == null)
            {
                account = _realRepository.GetAccount(accountId);
                if (account != null)
                {
                    _cachedAccounts.Add(account);
                }
            }
            return account;
        }

        /// <summary>
        /// Получает все банковские счета. Если кэш пуст, извлекает данные из реального репозитория.
        /// </summary>
        /// <returns>Список всех банковских счетов.</returns>
        public List<BankAccount> GetAllAccounts()
        {
            if (_cachedAccounts.Count == 0)
            {
                _cachedAccounts = _realRepository.GetAllAccounts();
            }
            return _cachedAccounts;
        }

        /// <summary>
        /// Сохраняет информацию об операции в реальном репозитории и кэширует её.
        /// </summary>
        /// <param name="operation">Операция для сохранения.</param>
        public void SaveOperation(Operation operation)
        {
            _realRepository.SaveOperation(operation);
            _cachedOperations.Add(operation);
        }

        /// <summary>
        /// Получает все операции. Если кэш пуст, извлекает данные из реального репозитория.
        /// </summary>
        /// <returns>Список всех операций.</returns>
        public List<Operation> GetAllOperations()
        {
            if (_cachedOperations.Count == 0)
            {
                _cachedOperations = _realRepository.GetAllOperations();
            }
            return _cachedOperations;
        }
    }
}
