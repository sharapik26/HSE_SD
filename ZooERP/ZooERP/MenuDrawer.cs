using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooERP
{
    public static class MenuDrawer
    {
        public static void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           Добро пожаловать в ZooERP!                            ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    _");
            Console.WriteLine("   /.)");
            Console.WriteLine("  /)\\| ");
            Console.WriteLine(" // / ");
            Console.WriteLine("/'\" \" ");
            Console.WriteLine("Воробей для красоты.");
        }

        public static void DrawMainMenu()
        {
            Console.Clear();
            DrawHeader();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     Главное меню                       ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Добавить животное                                   ║");
            Console.WriteLine("║ 2. Добавить предмет                                    ║");
            Console.WriteLine("║ 3. Посмотреть отчет о потреблении еды                  ║");
            Console.WriteLine("║ 4. Посмотреть список животных для контактного зоопарка ║");
            Console.WriteLine("║ 5. Посмотреть инвентаризацию                           ║");
            Console.WriteLine("║ 6. Выйти                                               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        }

        public static void PromptChoice()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nВыберите действие (1-6): ");
        }
    }
}
