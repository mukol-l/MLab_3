using System;
using System.Text.RegularExpressions;

namespace TextAnalyzer
{
    class Program
    {
        static void Main()
        {
            string text = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1 - Ввести текст");
                Console.WriteLine("2 - Підрахувати кількість чисел у тексті");
                Console.WriteLine("3 - Вивести слова, що складаються лише з латинських літер");
                Console.WriteLine("0 - Вихід");
                Console.Write("\nВаш вибір: ");
                string choice = Console.ReadLine();

                Console.Clear(); // очищення перед виведенням результатів

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть текст: ");
                        text = Console.ReadLine();
                        Console.WriteLine("\n✅ Текст збережено!");
                        break;

                    case "2":
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            Console.WriteLine("⚠️ Спочатку введіть текст (пункт 1).");
                        }
                        else
                        {
                            int count = CountNumbers(text);
                            Console.WriteLine($"🔢 Кількість чисел у тексті: {count}");
                        }
                        break;

                    case "3":
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            Console.WriteLine("⚠️ Спочатку введіть текст (пункт 1).");
                        }
                        else
                        {
                            ShowLatinWords(text);
                        }
                        break;

                    case "0":
                        Console.WriteLine("👋 Програму завершено.");
                        return;

                    default:
                        Console.WriteLine("❌ Помилка! Виберіть пункт 0–3.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
                Console.ReadKey();
            }
        }

        // Метод для підрахунку кількості чисел у тексті
        static int CountNumbers(string text)
        {
            MatchCollection matches = Regex.Matches(text, @"\b\d+([.,]\d+)?\b");
            return matches.Count;
        }

        // Метод для виведення слів, що складаються лише з латинських літер
        static void ShowLatinWords(string text)
        {
            MatchCollection matches = Regex.Matches(text, @"\b[a-zA-Z]+\b");

            if (matches.Count == 0)
            {
                Console.WriteLine("❕ У тексті немає слів, що складаються лише з латинських літер.");
            }
            else
            {
                Console.WriteLine("🔤 Слова з латинських літер:");
                foreach (Match m in matches)
                    Console.WriteLine(m.Value);
            }
        }
    }
}
