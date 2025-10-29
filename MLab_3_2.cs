using System;
using System.IO;

namespace MaxNumberInFileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1 - Створити файл і записати числа");
                Console.WriteLine("2 - Знайти найбільше число у файлі");
                Console.WriteLine("3 - Видалити файл");
                Console.WriteLine("0 - Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.WriteLine("До побачення!");
                        return;

                    case "1":
                        CreateFileWithNumbers();
                        break;

                    case "2":
                        FindMaxInFile();
                        break;

                    case "3":
                        DeleteFile();
                        break;

                    default:
                        Console.WriteLine("⚠ Неправильний вибір! Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        // === (1) Створення файлу з числами ===
        static void CreateFileWithNumbers()
        {
            Console.Clear();
            Console.WriteLine("=== Створення файлу чисел ===");

            int count;
            while (true)
            {
                Console.Write("Введіть кількість елементів: ");
                if (int.TryParse(Console.ReadLine(), out count) && count > 0)
                    break;
                else
                    Console.WriteLine("⚠ Помилка! Введіть додатнє ціле число.");
            }

            int[] numbers = new int[count];
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    Console.Write($"Введіть елемент №{i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out numbers[i]))
                        break;
                    else
                        Console.WriteLine("⚠ Помилка! Введіть ціле число.");
                }
            }

            string fileName = "numbers.txt";
            try
            {
                File.WriteAllLines(fileName, Array.ConvertAll(numbers, n => n.ToString()));
                Console.WriteLine($"\n✅ Файл '{fileName}' успішно створено і записано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка при записі файлу: {ex.Message}");
            }

            Pause();
        }

        // === (2) Знаходження найбільшого числа ===
        static void FindMaxInFile()
        {
            Console.Clear();
            Console.WriteLine("=== Пошук найбільшого числа ===");

            string fileName = "numbers.txt";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("⚠ Файл 'numbers.txt' не знайдено! Спочатку створіть його (пункт 1).");
                Pause();
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                if (lines.Length == 0)
                {
                    Console.WriteLine("⚠ Файл порожній!");
                    Pause();
                    return;
                }

                int max = int.MinValue;
                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int number))
                    {
                        if (number > max)
                            max = number;
                    }
                    else
                    {
                        Console.WriteLine($"⚠ Пропущено некоректне значення у файлі: '{line}'");
                    }
                }

                Console.WriteLine($"\n✅ Найбільше число у файлі: {max}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка при читанні файлу: {ex.Message}");
            }

            Pause();
        }

        // === (3) Видалення файлу ===
        static void DeleteFile()
        {
            Console.Clear();
            Console.WriteLine("=== Видалення файлу ===");

            string fileName = "numbers.txt";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("⚠ Файл 'numbers.txt' не знайдено — нічого видаляти.");
                Pause();
                return;
            }

            try
            {
                File.Delete(fileName);
                Console.WriteLine($"✅ Файл '{fileName}' успішно видалено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка при видаленні файлу: {ex.Message}");
            }

            Pause();
        }

        // === Пауза для зручності ===
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення до меню...");
            Console.ReadKey();
        }
    }
}
