using HSE_Bank.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HSE_Bank.Export
{
    /// <summary>
    /// Класс для экспорта данных о операциях в формат CSV.
    /// </summary>
    public class CsvDataExportVisitor : IDataExportVisitor
    {
        /// <summary>
        /// Экспортирует список операций в строку в формате CSV.
        /// </summary>
        /// <param name="operations">Список операций для экспорта.</param>
        /// <returns>Строка, представляющая данные операций в формате CSV.</returns>
        public string Export(List<Operation> operations)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BankAccountId,Amount,Date,Type,CategoryId,Description");

            foreach (var op in operations)
            {
                // Экранирование значений
                string description = EscapeCsv(op.Description);

                sb.AppendLine($"{op.BankAccountId},{op.Amount.ToString(CultureInfo.InvariantCulture)},{op.Date:yyyy-MM-dd},{op.Type},{op.CategoryId},{description}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Метод для экранирования данных CSV, чтобы корректно обрабатывать специальные символы.
        /// </summary>
        /// <param name="value">Строка, которую необходимо экранировать.</param>
        /// <returns>Экранированная строка.</returns>
        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                value = value.Replace("\"", "\"\""); 
                return $"\"{value}\"";
            }

            return value;
        }
    }
}
