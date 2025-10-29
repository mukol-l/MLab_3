using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextFileAnalysisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1 - Проаналізувати текст з input.txt");
                Console.WriteLine("2 - Видалити файл output.txt");
                Console.WriteLine("0 - Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.WriteLine("До побачення!");
                        return;

                    case "1":
                        AnalyzeTextFile();
                        break;

                    case "2":
                        DeleteOutputFile();
                        break;

                    default:
                        Console.WriteLine("⚠ Неправильний вибір! Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void AnalyzeTextFile()
        {
            Console.Clear();
            string inputFile = "input.txt";
            string outputFile = "output.txt";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"⚠ Файл '{inputFile}' не знайдено! Створіть його і додайте текст.");
                Pause();
                return;
            }

            try
            {
                string text = File.ReadAllText(inputFile);

                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("⚠ Файл порожній!");
                    Pause();
                    return;
                }

                string[] words = Regex.Matches(text, @"\b\w+\b")
                                      .Select(m => m.Value)
                                      .ToArray();

                // (а) Слова, що починаються з голосної
                int vowelsCount = words.Count(w => !string.IsNullOrEmpty(w) && IsVowel(w[0]));

                // (б) Слова з непарною кількістю приголосних
                var oddConsonantWords = words.Where(w => CountConsonants(w) % 2 == 1)
                                             .ToArray();

                // Вивід на екран
                Console.WriteLine($"\nКількість слів, що починаються з голосної: {vowelsCount}");
                Console.WriteLine("\nСлова з непарною кількістю приголосних:");
                if (oddConsonantWords.Length == 0)
                    Console.WriteLine("(немає таких слів)");
                else
                    foreach (var w in oddConsonantWords)
                        Console.WriteLine($"- {w}");

                // Запис у output.txt
                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    sw.WriteLine($"Кількість слів, що починаються з голосної: {vowelsCount}");
                    sw.WriteLine("Слова з непарною кількістю приголосних:");
                    foreach (var w in oddConsonantWords)
                        sw.WriteLine(w);
                }

                Console.WriteLine($"\n✅ Результати записані у файл '{outputFile}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка: {ex.Message}");
            }

            Pause();
        }

        static void DeleteOutputFile()
        {
            Console.Clear();
            string outputFile = "output.txt";

            if (!File.Exists(outputFile))
            {
                Console.WriteLine($"⚠ Файл '{outputFile}' не знайдено — нічого видаляти.");
                Pause();
                return;
            }

            try
            {
                File.Delete(outputFile);
                Console.WriteLine($"✅ Файл '{outputFile}' успішно видалено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка при видаленні файлу: {ex.Message}");
            }

            Pause();
        }

        static bool IsVowel(char c)
        {
            char lower = char.ToLower(c);
            return "aeiou".Contains(lower);
        }

        static int CountConsonants(string word)
        {
            return word.Count(c => char.IsLetter(c) && !"aeiou".Contains(char.ToLower(c)));
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення до меню...");
            Console.ReadKey();
        }
    }
}
