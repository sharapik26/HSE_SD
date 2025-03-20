using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSE_Bank.Commands
{
    /// <summary>
    /// Этот класс декорирует команду <see cref="ICommand"/> и измеряет время её выполнения.
    /// </summary>
    public class TimingDecorator : ICommand
    {
        private readonly ICommand _command;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TimingDecorator"/> с указанной командой.
        /// </summary>
        /// <param name="command">Команда, которая будет декорирована и для которой будет измеряться время выполнения.</param>
        public TimingDecorator(ICommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Выполняет декорированную команду и выводит в консоль время, затраченное на её выполнение.
        /// </summary>
        public void Execute()
        {
            var startTime = DateTime.Now;

            _command.Execute();

            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Время выполнения: {timeTaken.TotalMilliseconds} мс");
        }
    }
}
