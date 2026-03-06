using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using static KR1.ConsoleInterface;
using static KR1.FileOperations;

namespace KR1
{
    static internal class FileOperations
    {
        static void RewriteFileData(string filePath, Triangle userTriangle, Ray userRay)
        {
            string newFileData = $"{userTriangle.FirstVertex.x};{userTriangle.FirstVertex.y};{userTriangle.FirstVertex.z}\t" +
                $"{userTriangle.SecondVertex.x};{userTriangle.SecondVertex.y};{userTriangle.SecondVertex.z}\t" +
                $"{userTriangle.ThirdVertex.x};{userTriangle.ThirdVertex.y};{userTriangle.ThirdVertex.z}\t" +
                $"{userRay.StartPointOfRay.x};{userRay.StartPointOfRay.y};{userRay.StartPointOfRay.z}\t" +
                $"{userRay.HorizontalAngle}\t{userRay.VerticalAngle}";

            File.WriteAllText(filePath, newFileData); 
        }

        static bool ThisFileIsCorrect(string filePath)
        {
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                fileStream?.Close();
            }
            catch (FileNotFoundException)
            {
                return true;
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
            Console.Write("Ваш выбор: ");
            EnterFilePathAgainOrNot userChoice = Input.EnumInput<EnterFilePathAgainOrNot>();

            bool ChoiceProcess = true;
            while (ChoiceProcess)
            {
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
        public static bool SaveDataToFile(Triangle userTriangle, Ray userRay)
        {
            bool SaveDataProcess = true;
            while (SaveDataProcess)
            {
                Console.Write("Введите путь к файлу, в который будут сохраняться данные: ");
                string? filePath = Console.ReadLine();
                if (filePath == null)
                {
                    Environment.Exit(0);
                }

                bool fileIsCorrect = ThisFileIsCorrect(filePath);
                if (!fileIsCorrect)
                {
                    bool EnterFilePathAgain = EnterFilePathAgainOrNotChoice();
                    if (!EnterFilePathAgain)
                    {
                        return false;
                    }
                    continue;
                }
                
                if (File.Exists(filePath))
                {
                    bool RewriteFile = RewriteDataChoice(filePath);
                    if (!RewriteFile)
                    {
                        continue;
                    }
                }

                try
                { 
                    RewriteFileData(filePath, userTriangle, userRay);
                    Console.WriteLine("Данные успешно сохранены");
                    SaveDataProcess = false;
                }
                catch (Exception)
                {
                    bool EnterFilePathAgain = EnterFilePathAgainOrNotChoice();
                    if (!EnterFilePathAgain)
                    {
                        return false;
                    }
                    continue;
                }
            }

            return true;
        }

        static bool RewriteDataChoice(string filePath)
        {
            if (!ThisFileIsCorrect(filePath))
            {
                return true;
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

        public static bool LoadDataFromFile(out Triangle userTriangle, out Ray userRay)
        {
            Triangle newTriangle = new();
            Ray newRay = new();

            userTriangle = newTriangle;
            userRay = newRay;

            string allFileData = "";
            bool LoadDataProcess = true;

            while (LoadDataProcess)
            {
                Console.Write("Введите путь к файлу, из которого будут загружаться данные: ");
                string? filePath = Console.ReadLine();
                if (filePath == null)
                {
                    Environment.Exit(0);
                }

                bool fileIsCorrect = ThisFileIsCorrect(filePath);
                if (!fileIsCorrect || !File.Exists(filePath))
                {
                    bool EnterFilePathAgain = EnterFilePathAgainOrNotChoice();
                    if (!EnterFilePathAgain)
                    {
                        return false;
                    }
                    continue;
                }

                try 
                {
                    allFileData = File.ReadAllText(filePath);
                }
                catch (Exception)
                {
                    bool EnterFilePathAgain = EnterFilePathAgainOrNotChoice();
                    if (!EnterFilePathAgain)
                    {
                        return false;
                    }
                    continue;
                }
                
                if (!FileParser(allFileData, out userTriangle, out userRay))
                {
                    bool EnterFilePathAgain = EnterFilePathAgainOrNotChoice();
                    if (!EnterFilePathAgain)
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

       static bool ThisDataIsCorrect(string xStr, string yStr, string zStr)
        {
            return double.TryParse(xStr, out double x) && double.TryParse(yStr, out double y) && double.TryParse(zStr, out double z);
        }
        
        static bool ThisDataIsCorrect(string angleStr)
        {
            return double.TryParse(angleStr, out double angle);
        }

        static bool FileParser(string allFileData, out Triangle userTriangle, out Ray userRay)
        {
            userTriangle = new Triangle();
            userRay = new Ray();

            Triangle newTriangle = new();
            Ray newRay = new();
            const int requiredNumberOfComponentsInFile = 6;

            // Константа снизу была введена для того чтобы создавать по 1 строке(вместо 3) на список списка(для горизонтального угла и вертикального угла)
            const int OneStringBound = 3;
            List<List<string>> fileData = new();
            
            for (int i = 0;i < requiredNumberOfComponentsInFile; i++)
            {
                if (i > OneStringBound)
                {
                    fileData.Add(new List<String>() { "" });
                }
                else
                {
                    fileData.Add(new List<String>() { "", "", "" });
                }
            }

            int j = 0, k = 0;

            for (int i = 0;i < allFileData.Length;i++)
            {
                if (allFileData[i] == '\t')
                {
                    ++j;
                    k = 0;
                    continue;
                }
                else if (allFileData[i] == ';')
                {
                    ++k;
                    continue;
                }
                else if (j < fileData.Count && k < fileData[j].Count)
                {
                    fileData[j][k] += allFileData[i];
                }
                else
                {
                    return false;
                }
            }

            double horizontalAngle = 0.0, verticalAngle = 0.0;

            try
            {
                for (int i = 0; i < requiredNumberOfComponentsInFile; i++)
                {
                    if (i == 0 && ThisDataIsCorrect(fileData[i][k], fileData[i][k + 1], fileData[i][k + 2]))
                    {
                        newTriangle.FirstVertex.x = Convert.ToDouble(fileData[i][k]);
                        newTriangle.FirstVertex.y = Convert.ToDouble(fileData[i][k + 1]);
                        newTriangle.FirstVertex.z = Convert.ToDouble(fileData[i][k + 2]);
                    }
                    else if (i == 1 && ThisDataIsCorrect(fileData[i][k], fileData[i][k + 1], fileData[i][k + 2]))
                    {
                        newTriangle.SecondVertex.x = Convert.ToDouble(fileData[i][k]);
                        newTriangle.SecondVertex.y = Convert.ToDouble(fileData[i][k + 1]);
                        newTriangle.SecondVertex.z = Convert.ToDouble(fileData[i][k + 2]);
                    }
                    else if (i == 2 && ThisDataIsCorrect(fileData[i][k], fileData[i][k + 1], fileData[i][k + 2]))
                    {
                        newTriangle.ThirdVertex.x = Convert.ToDouble(fileData[i][k]);
                        newTriangle.ThirdVertex.y = Convert.ToDouble(fileData[i][k + 1]);
                        newTriangle.ThirdVertex.z = Convert.ToDouble(fileData[i][k + 2]);
                    }
                    else if (i == 3 && ThisDataIsCorrect(fileData[i][k], fileData[i][k + 1], fileData[i][k + 2]))
                    {
                        newRay.StartPointOfRay.x = Convert.ToDouble(fileData[i][k]);
                        newRay.StartPointOfRay.y = Convert.ToDouble(fileData[i][k]);
                        newRay.StartPointOfRay.z = Convert.ToDouble(fileData[i][k]);
                    }
                    else if (i == 4 && ThisDataIsCorrect(fileData[i][k]))
                    {
                        horizontalAngle = Convert.ToDouble(fileData[i][k]);
                    }
                    else if (i == 5 && ThisDataIsCorrect(fileData[i][k]))
                    {
                        verticalAngle = Convert.ToDouble(fileData[i][k]);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            newRay.HorizontalAngle = horizontalAngle;
            newRay.VerticalAngle = verticalAngle;

            if (!GeometryUtils.ThisTriangleIsExists(newTriangle))
            {
                return false;
            }

            userTriangle = newTriangle;
            userRay = newRay;

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
