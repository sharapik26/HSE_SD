using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Domain
{
    /// <summary>
    /// Тип категории, определяющий, является ли категория доходом или расходом.
    /// </summary>
    public enum CategoryType
    {
        /// <summary>
        /// Категория доходов.
        /// </summary>
        Income,

        /// <summary>
        /// Категория расходов.
        /// </summary>
        Expense
    }

    /// <summary>
    /// Класс, представляющий категорию (доход или расход) для операций в банковской системе.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Тип категории (доход или расход).
        /// </summary>
        public CategoryType Type { get; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр категории с заданным именем и типом.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <param name="type">Тип категории (доход или расход).</param>
        /// <exception cref="ArgumentException">Бросается, если название категории пустое или состоит только из пробелов.</exception>
        public Category(string name, CategoryType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Переименовывает категорию.
        /// </summary>
        /// <param name="newName">Новое имя категории.</param>
        /// <exception cref="ArgumentException">Бросается, если новое имя категории пустое или состоит только из пробелов.</exception>
        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Название категории не может быть пустым.", nameof(newName));

            Name = newName;
        }
    }
}
