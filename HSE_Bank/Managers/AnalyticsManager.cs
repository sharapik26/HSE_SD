using HSE_Bank.Facade;
using System;

namespace HSE_Bank.Managers
{
    /// <summary>
    /// Класс для управления аналитикой финансовых данных.
    /// </summary>
    public class AnalyticsManager
    {
        private readonly FinancialFacade _facade;

        /// <summary>
        /// Конструктор класса <see cref="AnalyticsManager"/>.
        /// </summary>
        /// <param name="facade">Фасад для работы с финансовыми данными.</param>
        public AnalyticsManager(FinancialFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Метод для отображения аналитики на основе введенных пользователем дат.
        /// Рассчитывает разницу между доходами и расходами за указанный период.
        /// </summary>
        public void ShowAnalytics()
        {
            Console.Write("Введите начальную дату (гггг-мм-дд): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.Write("Введите конечную дату (гггг-мм-дд): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    decimal difference = _facade.CalculateBalanceDifference(startDate, endDate);
                    Console.WriteLine($"Разница доходов и расходов: {difference}");
                }
                else
                {
                    Console.WriteLine("Ошибка: неверная конечная дата.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: неверная начальная дата.");
            }
        }
    }
}
