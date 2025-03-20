using System;

namespace HSE_Bank.Commands
{
    /// <summary>
    /// Определяет интерфейс команды, которая может быть выполнена.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполняет команду.
        /// </summary>
        void Execute();
    }
}
