using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace KR2
{
    internal class ConsoleInterface
    {
        BigNumber ResultBigNumber;
        BigNumber BigNumber1;
        BigNumber BigNumber2;

        public ConsoleInterface()
        {
            ResultBigNumber = new BigNumber();
            BigNumber1 = new BigNumber();
            BigNumber2 = new BigNumber();
        }

        static void Greeting()
        {
            string greetingStr = "Программа для умножения больших чисел\n" +
                                 "Автор: Заиграев С.С., группа 444\n" +
                                 "Решаемая задача: умножение двух больших чисел(до 100 знаков)\n" +
                                 "Результат: большое число, полученное из произведения двух больших чисел\n\n";
            Console.WriteLine(greetingStr);
        }

        void PrintInputData()
        {
            Console.Write("Число 1 = ");
            if (BigNumber1.Negative)
            {
                Console.Write("-");
            }
            Console.WriteLine(BigNumber1.Number);
            Console.Write("Число 2 = ");
            if (BigNumber2.Negative)
            {
                Console.Write("-");
            }
            Console.WriteLine(BigNumber2.Number);
        }

        void PrintResult()
        {
            Console.Write("Результат произведения: ");
            if (ResultBigNumber.Negative)
            {
                Console.Write("-");
            }
            Console.WriteLine(ResultBigNumber.Number + "\n\n");
        }

        void SaveData(out bool dataSavingChoice)
        {
            dataSavingChoice = !FileOperations.SaveDataToFile(BigNumber1, BigNumber2, ResultBigNumber);
        }

        void SaveToFileChoice()
        {
            bool dataSavingChoice = true;
            while (dataSavingChoice)
            {
                Console.WriteLine("Сохранить данные в файл?\n1 - Да, сохранить\n2 - Нет, не сохранять\n");
                Console.Write("Ваш выбор: ");
                SaveDataToFileOrNot userChoice = Input.EnumInput<SaveDataToFileOrNot>();

                switch (userChoice)
                {
                    case SaveDataToFileOrNot.SaveDataToFile:
                        SaveData(out dataSavingChoice);
                        break;
                    case SaveDataToFileOrNot.DontSaveDataToFile:
                        dataSavingChoice = false;
                        break;
                    default:
                        Console.WriteLine("Такого пункта не существует, введите, пожалуйста, новый");
                        break;
                }
            }
        }

        void InputDataFromConsole()
        {
            Console.Write("Введите первое большое число: ");
            BigNumber1 = Input.BigNumberInput();

            Console.Write("Введите второе большое число: ");
            BigNumber2 = Input.BigNumberInput();

            Console.WriteLine("Данные введены, начинается умножение...");

            Algorithm.AlgorithmRecIntMult(BigNumber1, BigNumber2, out ResultBigNumber);

            PrintResult();

            SaveToFileChoice();
        }

        void AutoInput()
        {
            BigNumber1 = Input.RandomBigNumberInput();

            BigNumber2 = Input.RandomBigNumberInput();

            PrintInputData();
            Console.WriteLine("Данные введены, начинается умножение...");

            Algorithm.AlgorithmRecIntMult(BigNumber1, BigNumber2, out ResultBigNumber);

            PrintResult();

            SaveToFileChoice();
        }
       
        void InputDataFromFile()
        {
            bool dataIsLoaded = FileOperations.LoadDataFromFile(out BigNumber1, out BigNumber2, out ResultBigNumber);
            if (dataIsLoaded)
            {
                Console.WriteLine("Данные загружены...\n");
                PrintInputData();
                
                PrintResult();

                SaveToFileChoice();
            }
            else
            {
                Console.WriteLine("Данные не загружены\n\n");
            }
        }

        public void Run()
        {
            Greeting();

            bool programRun = true;

            while (programRun)
            {
                Console.WriteLine("Вы находитесь в главном меню\nВведите 1 чтобы заполнить данные с консоли\n" +
                    "Введите 2 чтобы заполнить данные случайно\nВведите 3 чтобы получить данные из файла\n" +
                    "Введите 4 чтобы выйти из программы");

                Console.Write("Ваш выбор: ");

                MenuChoices userChoice = Input.EnumInput<MenuChoices>();

                switch (userChoice)
                {
                    case MenuChoices.InputDataFromConsole:
                        InputDataFromConsole();
                        break;
                    case MenuChoices.AutoInput:
                        AutoInput();
                        break;
                    case MenuChoices.InputDataFromFile:
                        InputDataFromFile();
                        break;
                    case MenuChoices.QuitTheProgram:
                        programRun = false;
                        break;
                    default:
                        Console.WriteLine("Такого пункта не существует, введите, пожалуйста, новый");
                        break;
                }
            }
        }
        
        public enum MenuChoices
        {
            InputDataFromConsole = 1,
            AutoInput,
            InputDataFromFile,
            QuitTheProgram,
        }

        public enum SaveDataToFileOrNot {
            SaveDataToFile = 1,
            DontSaveDataToFile
        }
    }
}
