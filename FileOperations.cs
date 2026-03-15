using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using static KR2.ConsoleInterface;
using static KR2.FileOperations;

namespace KR2
{
    static internal class FileOperations
    {
        static void RewriteFileData(string filePath, BigNumber number1, BigNumber number2, BigNumber resNum)
        {
            if (number1.Negative)
            {
                number1.Number = "-" + number1.Number;
            }
            if (number2.Negative)
            {
                number2.Number = "-" + number2.Number;
            }
            if (resNum.Negative)
            {
                resNum.Number = "-" + resNum.Number;
            }
            string newFileData = $"{number1.Number}\n{number2.Number}\n{resNum.Number}";
            File.WriteAllText(filePath, newFileData); 
        }

        static bool CanAccessFile(string filePath)
        {
            try
            {
                using FileStream fileStream = File.OpenRead(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        static bool EnterFilePathAgainOrNotChoice()
        {
            Console.WriteLine("Ошибка работы с файлом...Ввести путь снова?\n1 - Да, ввести путь снова\n2 - Нет, не вводить, выйти в главное меню\n");
 
            bool ChoiceProcess = true;
            while (ChoiceProcess)
            {
                Console.Write("Ваш выбор: ");
                EnterFilePathAgainOrNot userChoice = Input.EnumInput<EnterFilePathAgainOrNot>();
                switch (userChoice)
                {
                    case EnterFilePathAgainOrNot.EnterFilePathAgain:
                        return true;
                    case EnterFilePathAgainOrNot.DontEnterFilePathAgain:
                        return false;
                    default:
                        Console.WriteLine("Такого пункта не существует, введите пожалуйста новый\n");
                        break;
                }
            }

            return true;
        }

        public static bool SaveDataToFile(BigNumber number1, BigNumber number2, BigNumber resNum)
        {
            bool SaveDataProcess = true;
            while (SaveDataProcess)
            {
                Console.Write("Введите путь к файлу, в который будут сохраняться данные: ");
                string filePath = Console.ReadLine();

                if (!CanAccessFile(filePath) && File.Exists(filePath))
                {
                    if (!EnterFilePathAgainOrNotChoice())
                    {
                        return false;
                    }
                    continue;
                }

                try
                {
                    if (!WriteDataChoice(filePath))
                    {
                        continue;
                    }

                    RewriteFileData(filePath, number1, number2, resNum);
                    Console.WriteLine("Данные успешно сохранены");
                    SaveDataProcess = false;
                }
                catch (Exception)
                {
                    if (!EnterFilePathAgainOrNotChoice())
                    {
                        return false;
                    }
                    continue;
                }
            }

            return true;
        }

        static bool WriteDataChoice(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return true;
            }
            else if (!CanAccessFile(filePath) && File.Exists(filePath))
            {
                return false;
            }
            string fileData = File.ReadAllText(filePath);

            if (fileData.Length != 0)
            {
                bool userInput = true;
                while (userInput)
                {
                    Console.WriteLine("В файле есть данные, перезаписать?\n1 - Да, перезаписать\n2 - Нет, выбрать другой файл");
                    Console.Write("Ваш выбор: ");
                    RewriteFileDataOrNot userChoice = Input.EnumInput<RewriteFileDataOrNot>();

                    switch (userChoice)
                    {
                        case RewriteFileDataOrNot.RewriteData:
                            return true;
                        case RewriteFileDataOrNot.DontRewriteData:
                            return false;
                        default:
                            Console.WriteLine("Такого пункта не существует, введите пожалуйста новый\n");
                            break;
                    }
                }
            }

            return true;
        }

        public static bool LoadDataFromFile(out BigNumber number1, out BigNumber number2, out BigNumber resNum)
        {
            BigNumber newNumber1 = new();
            BigNumber newNumber2 = new();
            BigNumber newResNum = new();

            number1 = new();
            number2 = new();
            resNum = new();

            string allFileData = "";
            bool LoadDataProcess = true;

            while (LoadDataProcess)
            {
                Console.Write("Введите путь к файлу, из которого будут загружаться данные: ");
                string filePath = Console.ReadLine();
                if (filePath == null)
                {
                    Environment.Exit(0);
                }

                if (!CanAccessFile(filePath))
                {
                    if (!EnterFilePathAgainOrNotChoice())
                    {
                        return false;
                    }
                    continue;
                }

                try 
                {
                    allFileData = File.ReadAllText(filePath);
                    if (!FileParser(allFileData, out number1, out number2, out resNum))
                    {
                        if (!EnterFilePathAgainOrNotChoice())
                        {
                            return false;
                        }
                        continue;
                    }
                }
                catch (Exception)
                {
                    if (!EnterFilePathAgainOrNotChoice())
                    {
                        return false;
                    }
                    continue;
                }

                Console.WriteLine("Данные из файла успешно загружены\n");
                return true;
            }

            return false;
        }

        static bool FileParser(string allFileData, out BigNumber number1, out BigNumber number2, out BigNumber resNum)
        {
            number1 = new BigNumber();
            number2 = new BigNumber();
            resNum = new BigNumber();

            // индекс 0 списка ниже - первый множитель
            // индекс 1 списка ниже - второй множитель
            // индекс 2 списка ниже - результат произведения
            List<BigNumber> numbersFromFile = new List<BigNumber>(){new BigNumber(), new BigNumber(), new BigNumber()};

            int minusCount = 0, escapeSequenceCount = 0;
            string allowedSymbols = "1234567890";

            for (int i = 0;i < allFileData.Length; i++)
            {
                if (allFileData[i] == '\n')
                {
                    ++escapeSequenceCount;
                }
                else if(allFileData[i] == '-')
                {
                    ++minusCount;
                }
            }

            if (minusCount > 3 || escapeSequenceCount != 2)
            {
                return false;
            }

            int currentNumberIndex = 0;
            for(int i = 0;i < allFileData.Length; i++)
            {
                if (allFileData[i] == '\n')
                {
                    ++currentNumberIndex;
                    continue;
                }
                numbersFromFile[currentNumberIndex].Number += allFileData[i];
            }

            for(int i = 0;i < numbersFromFile.Count; i++)
            {
                if (!HelpingFunctions.ThisIsCorrectNumber(numbersFromFile[i], allowedSymbols))
                {
                    return false;
                }
                else if (numbersFromFile[i].Number[0] == '-')
                {
                    numbersFromFile[i].Negative = true;
                    numbersFromFile[i].Number = numbersFromFile[i].Number.Remove(0, 1);
                }
            }

            number1 = numbersFromFile[0];
            number2 = numbersFromFile[1];
            resNum = numbersFromFile[2];

            return true;
        }

        public enum RewriteFileDataOrNot
        {
            RewriteData = 1,
            DontRewriteData
        }

        public enum EnterFilePathAgainOrNot
        {
            EnterFilePathAgain = 1,
            DontEnterFilePathAgain 
        }

    }

}
