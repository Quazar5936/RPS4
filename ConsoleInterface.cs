using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace KR1
{
    internal class ConsoleInterface
    {
        Triangle UserTriangle;
        Ray UserRay;
        
        public ConsoleInterface()
        {
            UserTriangle = new Triangle();
            UserRay = new Ray();
        }

        static void Greeting()
        {
            String greetingStr = "Программа для определения координат точки пересечения луча и треугольника\n" +
                                 "Автор: Заиграев С.С., группа 444\n" +
                                 "Решаемая задача: автоматизировать определение координат точки пересечения луча и треугольника\n" +
                                 "Результат: Координаты точки пересечения или сообщение о том, что такой точки нет\n\n";
            Console.WriteLine(greetingStr);
        }

        void PrintAllData()
        {
            Console.WriteLine($"Луч(x,y,z)\nТочка начала луча: [{UserRay.StartPointOfRay.x}, {UserRay.StartPointOfRay.y}, {UserRay.StartPointOfRay.z}]");
            Console.WriteLine($"Горизонтальный угол луча(в градусах): {UserRay.HorizontalAngle}\nВертикальный угол луча(в градусах): {UserRay.VerticalAngle}\n");

            Console.WriteLine($"Треугольник(x,y,z)\nV1: [{UserTriangle.FirstVertex.x}, {UserTriangle.FirstVertex.y}, {UserTriangle.FirstVertex.z}]");
            Console.WriteLine($"V2: [{UserTriangle.SecondVertex.x}, {UserTriangle.SecondVertex.y}, {UserTriangle.SecondVertex.z}]");
            Console.WriteLine($"V3: [{UserTriangle.ThirdVertex.x}, {UserTriangle.ThirdVertex.y}, {UserTriangle.ThirdVertex.z}]\n");
        }

        static double GetRandomDouble(double min, double max)
        {
            return Random.Shared.NextDouble() * (max - min) + min;
        }

        void SaveData(out bool dataSavingChoice)
        {
            dataSavingChoice = !FileOperations.SaveDataToFile(UserTriangle, UserRay);
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
            Input.RayInput(out UserRay);

            Input.TriangleInput(out UserTriangle);

            PrintAllData();
            Console.WriteLine("Данные введены, идет поиск точки...");

            PrintAllData();
            if (GeometryUtils.RayTriangleIntersect(UserRay,UserTriangle, out Point hitPoint))
            {
                Console.WriteLine("Найдено пересечение!\n");
                Console.WriteLine($"Точка пересечения(x,y,z): [{hitPoint.x}, {hitPoint.y}, {hitPoint.z}]");
            }
            else
            {
                Console.WriteLine("Пересечение не найдено\n");
            }

            SaveToFileChoice();
        }

        void AutoInput()
        {
            UserRay.StartPointOfRay.x = GetRandomDouble(-100, 100);
            UserRay.StartPointOfRay.y = GetRandomDouble(-100, 100);
            UserRay.StartPointOfRay.z = GetRandomDouble(-100, 100);
            UserRay.HorizontalAngle = GetRandomDouble(0, 360);
            UserRay.VerticalAngle = GetRandomDouble(0, 360);
            
            do
            {
                UserTriangle.FirstVertex.x = GetRandomDouble(-3, 3);
                UserTriangle.FirstVertex.y = GetRandomDouble(-3, 3);
                UserTriangle.FirstVertex.z = GetRandomDouble(-3, 3);

                UserTriangle.SecondVertex.x = GetRandomDouble(-2, 2);
                UserTriangle.SecondVertex.y = GetRandomDouble(-2, 2);
                UserTriangle.SecondVertex.z = GetRandomDouble(-2, 2);

                UserTriangle.ThirdVertex.x = GetRandomDouble(-1, 1);
                UserTriangle.ThirdVertex.y = GetRandomDouble(-1, 1);
                UserTriangle.ThirdVertex.z = GetRandomDouble(-1, 1);
                
            } while (!GeometryUtils.ThisTriangleIsExists(UserTriangle));

            PrintAllData();
            
            if (GeometryUtils.RayTriangleIntersect(UserRay, UserTriangle, out Point hitPoint))
            {
                Console.WriteLine("Найдено пересечение!\n");

                Console.WriteLine($"Точка пересечения(x,y,z): [{hitPoint.x}, {hitPoint.y}, {hitPoint.z}]");
            }
            else
            {
                Console.WriteLine("Пересечение не найдено\n");
            }

            SaveToFileChoice();
        }
       
        void InputDataFromFile()
        {
            bool DataIsLoaded = FileOperations.LoadDataFromFile(out UserTriangle, out UserRay);
            if (DataIsLoaded)
            {
                PrintAllData();

                if (GeometryUtils.RayTriangleIntersect(UserRay, UserTriangle, out Point hitPoint))
                {
                    Console.WriteLine("Найдено пересечение!\n");

                    Console.WriteLine($"Точка пересечения(x,y,z): [{hitPoint.x}, {hitPoint.y}, {hitPoint.z}]");
                }
                else
                {
                    Console.WriteLine("Пересечение не найдено\n");
                }

                SaveToFileChoice();
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
            Quit
        }

        public enum SaveDataToFileOrNot {
            SaveDataToFile = 1,
            DontSaveDataToFile
        }
    }
}
