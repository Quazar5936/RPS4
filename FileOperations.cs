using System;
using System.Collections.Generic;
using System.Globalization;
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
            string formatData(double val) => val.ToString(CultureInfo.InvariantCulture);

            string newFileData = $"{formatData(userTriangle.FirstVertex.x)};{formatData(userTriangle.FirstVertex.y)};{formatData(userTriangle.FirstVertex.z)}\t" +
                $"{formatData(userTriangle.SecondVertex.x)};{formatData(userTriangle.SecondVertex.y)};{formatData(userTriangle.SecondVertex.z)}\t" +
                $"{formatData(userTriangle.ThirdVertex.x)};{formatData(userTriangle.ThirdVertex.y)};{formatData(userTriangle.ThirdVertex.z)}\t" +
                $"{formatData(userRay.StartPointOfRay.x)};{formatData(userRay.StartPointOfRay.y)};{formatData(userRay.StartPointOfRay.z)}\t" +
                $"{formatData(userRay.HorizontalAngle)}\t{formatData(userRay.VerticalAngle)}";
           
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

                bool fileAccess = CanAccessFile(filePath);
                if (!fileAccess && File.Exists(filePath)) 
                {
                    if (!HandleFileError())
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
                    if (!HandleFileError())
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
            if (!CanAccessFile(filePath))
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

                bool fileAccess = CanAccessFile(filePath);
                if (!fileAccess)
                {
                    if (!HandleFileError())
                    {
                        return false;
                    }
                    continue;
                }

                try 
                {
                    allFileData = File.ReadAllText(filePath);
                    if (!FileParser(allFileData, out userTriangle, out userRay))
                    {
                        if (!HandleFileError())
                        {
                            return false;
                        }
                        continue;
                    }
                }
                catch (Exception)
                {
                    if (!HandleFileError())
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
            return double.TryParse(xStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double x) && double.TryParse(yStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double y) && double.TryParse(zStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double z);
        }
        
        static bool ThisDataIsCorrect(string angleStr)
        {
            return double.TryParse(angleStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double angle);
        }
        
        static bool HandleFileError()
        {
            return EnterFilePathAgainOrNotChoice();
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
                    fileData.Add(new List<string>() { "" });
                }
                else
                {
                    fileData.Add(new List<string>() { "", "", "" });
                }
            }

            int j = 0, k = 0;
            
            for (int i = 0;i < allFileData.Length;i++)
            {
                if (allFileData[i] == '\t')
                {
                    ++j;
                    k = 0;
                }
                else if (allFileData[i] == ';')
                {
                    ++k;
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

            for (int i = 0; i < requiredNumberOfComponentsInFile; i++)
            {
                if (i == 0 && ThisDataIsCorrect(fileData[i][0], fileData[i][1], fileData[i][2]))
                {
                    newTriangle.FirstVertex.x = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                    newTriangle.FirstVertex.y = Convert.ToDouble(fileData[i][1], CultureInfo.InvariantCulture);
                    newTriangle.FirstVertex.z = Convert.ToDouble(fileData[i][2], CultureInfo.InvariantCulture);
                }
                else if (i == 1 && ThisDataIsCorrect(fileData[i][0], fileData[i][1], fileData[i][2]))
                {
                    newTriangle.SecondVertex.x = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                    newTriangle.SecondVertex.y = Convert.ToDouble(fileData[i][1], CultureInfo.InvariantCulture);
                    newTriangle.SecondVertex.z = Convert.ToDouble(fileData[i][2], CultureInfo.InvariantCulture);
                }
                else if (i == 2 && ThisDataIsCorrect(fileData[i][0], fileData[i][1], fileData[i][2]))
                {
                    newTriangle.ThirdVertex.x = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                    newTriangle.ThirdVertex.y = Convert.ToDouble(fileData[i][1], CultureInfo.InvariantCulture);
                    newTriangle.ThirdVertex.z = Convert.ToDouble(fileData[i][2], CultureInfo.InvariantCulture);
                }
                else if (i == 3 && ThisDataIsCorrect(fileData[i][0], fileData[i][1], fileData[i][2]))
                {
                    newRay.StartPointOfRay.x = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                    newRay.StartPointOfRay.y = Convert.ToDouble(fileData[i][1], CultureInfo.InvariantCulture);
                    newRay.StartPointOfRay.z = Convert.ToDouble(fileData[i][2], CultureInfo.InvariantCulture);
                }
                else if (i == 4 && ThisDataIsCorrect(fileData[i][0]))
                {
                    horizontalAngle = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                }
                else if (i == 5 && ThisDataIsCorrect(fileData[i][0]))
                {
                    verticalAngle = Convert.ToDouble(fileData[i][0], CultureInfo.InvariantCulture);
                }
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
