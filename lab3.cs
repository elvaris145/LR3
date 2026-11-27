
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        bool exit = false;

        while (exit == false)
        {
            Console.WriteLine();
            Console.WriteLine("---- МЕНЮ ----");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите пункт: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GuessAnswerMenu();
                    break;

                case "2":
                    ShowAuthor();
                    break;

                case "3":
                    SortArrayMenu();
                    break;

                case "4":
                    exit = ConfirmExit();
                    break;

                default:
                    Console.WriteLine("Ошибка: выберите пункт 1, 2, 3 или 4!");
                    break;
            }
        }

        Console.WriteLine("Программа завершена.");
    }

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            bool isNumber = int.TryParse(Console.ReadLine(), out int value);

            if (!isNumber)
            {
                Console.WriteLine("Ошибка: нужно ввести целое число!");
            }
            else if (value < min || value > max)
            {
                Console.WriteLine("Ошибка: число должно быть от " + min + " до " + max + "!");
            }
            else
            {
                return value;
            }
        }
    }

    static int ReadArrayLength()
    {
        return ReadInt("Введите длину массива (больше 0): ", 1, 1000000);
    }

    static int[] CreateArray(int length)
    {
        int[] array = new int[length];
        Random random = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(-100, 101);
        }

        return array;
    }

    static int[] CopyArray(int[] source)
    {
        int[] copy = new int[source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            copy[i] = source[i];
        }

        return copy;
    }

    static void PrintArray(int[] array)
    {
        if (array.Length > 10)
        {
            Console.WriteLine("Массив не выводится, так как его длина больше 10!");
            return;
        }

        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }

        Console.WriteLine();
    }

    static int[] BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        return array;
    }

    static int[] ShellSort(int[] array)
    {
        int n = array.Length;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j = i;

                while (j >= gap && array[j - gap] > temp)
                {
                    array[j] = array[j - gap];
                    j = j - gap;
                }

                array[j] = temp;
            }
        }

        return array;
    }

    static void SortArrayMenu()
    {
        int n = ReadArrayLength();

        int[] original = CreateArray(n);

        Console.WriteLine("Исходный массив:");
        PrintArray(original);

        int[] bubbleArray = CopyArray(original);
        int[] shellArray = CopyArray(original);

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        BubbleSort(bubbleArray);
        stopwatch.Stop();

        double bubbleTime = stopwatch.Elapsed.TotalMilliseconds;

        stopwatch.Restart();
        ShellSort(shellArray);
        stopwatch.Stop();

        double shellTime = stopwatch.Elapsed.TotalMilliseconds;

        Console.WriteLine("После сортировки пузырьком:");
        PrintArray(bubbleArray);

        Console.WriteLine("После сортировки Шелла:");
        PrintArray(shellArray);

        Console.WriteLine("Время сортировки пузырьком: "
            + bubbleTime.ToString("F3") + " мс");

        Console.WriteLine("Время сортировки Шелла: "
            + shellTime.ToString("F3") + " мс");

        if (bubbleTime < shellTime)
        {
            Console.WriteLine("Быстрее работает сортировка пузырьком.");
        }
        else if (shellTime < bubbleTime)
        {
            Console.WriteLine("Быстрее работает сортировка Шелла.");
        }
        else
        {
            Console.WriteLine("Время выполнения сортировок одинаковое.");
        }
    }

    static double CalculateFunction(double a, double b)
    {
        double n = Math.Pow(Math.Cos(Math.PI), 7)
            + Math.Sqrt(Math.Log(Math.Pow(b, 4)));

        double d = Math.Pow(Math.Sin(Math.PI / 2 + a), 2);

        return n / d;
    }

    static void PlayGuessGame(double correct)
    {
        int attempts = 3;
        bool win = false;

        while (attempts > 0 && win == false)
        {
            double answer = ReadDouble(
                "Ваш ответ: ",
                double.MinValue,
                double.MaxValue
            );

            answer = Math.Round(answer, 2);

            if (answer == correct)
            {
                win = true;
                Console.WriteLine("Ответ верный! Вы выиграли!");
            }
            else
            {
                attempts = attempts - 1;
                Console.WriteLine("Ответ неверный. Осталось попыток: " + attempts);
            }
        }

        if (win == false)
        {
            Console.WriteLine("Вы проиграли. Правильный ответ: " + correct.ToString("F2"));
        }
    }

    static void GuessAnswerMenu()
    {
        double a;

        while (true)
        {
            a = ReadDouble(
                "Введите a в радианах (от -100 до 100): ",
                -100,
                100
            );

            if (Math.Abs(Math.Sin(Math.PI / 2 + a)) < 0.001)
            {
                Console.WriteLine("Ошибка: при таком a знаменатель равен 0! Введите другое число.");
            }
            else
            {
                break;
            }
        }

        double b;

        while (true)
        {
            b = ReadDouble(
                "Введите b (от -100 до 100, по модулю не меньше 1): ",
                -100,
                100
            );

            if (Math.Abs(b) < 1)
            {
                Console.WriteLine("Ошибка: |b| должно быть не меньше 1.");
            }
            else
            {
                break;
            }
        }

        double correct = Math.Round(CalculateFunction(a, b), 2);

        PlayGuessGame(correct);
    }

    static void ShowAuthor()
    {
        Console.WriteLine("Автор программы: студент.");
    }

    static bool ConfirmExit()
    {
        while (true)
        {
            Console.Write("Вы точно хотите выйти? (д/н): ");

            string sExit = Console.ReadLine();

            if (sExit == "д")
            {
                return true;
            }
            else if (sExit == "н")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Ошибка: введите «д» или «н»!");
            }
        }
    }

    static double ReadDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write(prompt);

            bool isNumber = double.TryParse(Console.ReadLine(), out double value);

            if (!isNumber || double.IsNaN(value) || double.IsInfinity(value))
            {
                Console.WriteLine("Ошибка: нужно ввести корректное число!");
            }
            else if (value < min || value > max)
            {
                Console.WriteLine("Ошибка: число должно быть от " + min + " до " + max + "!");
            }
            else
            {
                return value;
            }
        }
    }
}
