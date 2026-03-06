using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static KR1.ConsoleInterface;

namespace KR1
{
    static internal class Input
    {
        public static T NumberInput<T>() where T : IParsable<T>
        {
            T? number = default;
            bool parseResult = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out number);

            while (!parseResult || number == null)
            {
                Console.Write("Введенное вами число некорректно, введите, пожалуйста, новое: ");

                parseResult = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out number);
                
            }

            return number;
        }

        public static T EnumInput<T>() where T: struct, Enum
        {
            T choice = default;
            bool parseResult = Enum.TryParse<T>(Console.ReadLine(), out choice);

            while (!parseResult)
            {
                Console.Write("Введенный вами пункт меню не существует, введите, пожалуйста, новый: ");

                parseResult = Enum.TryParse<T>(Console.ReadLine(), out choice);
            }

            return choice;
        }

        public static void TriangleInput(out Triangle userTriangle)
        {
            userTriangle = new Triangle();

            bool triangleInput = true;
            while (triangleInput)
            {
                Point firstVertex = new();
                Console.Write("Введите координаты первой вершины треугольника\nКоордината x: ");
                firstVertex.x = NumberInput<double>();

                Console.Write("(первая вершина)Координата y: ");
                firstVertex.y = NumberInput<double>();

                Console.Write("(первая вершина)Координата z: ");
                firstVertex.z = NumberInput<double>();

                Point secondVertex = new();

                Console.Write("\nВведите координаты второй вершины треугольника\nКоордината x: ");
                secondVertex.x = NumberInput<double>();

                Console.Write("(вторая вершина)Координата y: ");
                secondVertex.y = NumberInput<double>();

                Console.Write("(вторая вершина)Координата z: ");
                secondVertex.z = NumberInput<double>();

                Point thirdVertex = new();

                Console.Write("Введите координаты третьей вершины\nКоордината x: ");
                thirdVertex.x = NumberInput<double>();

                Console.Write("(третья вершина)Координата y: ");
                thirdVertex.y = NumberInput<double>();

                Console.Write("(третья вершина)Координата z: ");
                thirdVertex.z = NumberInput<double>();

                Triangle newTriangle = new(firstVertex, secondVertex, thirdVertex);
                if (GeometryUtils.ThisTriangleIsExists(newTriangle))
                {
                    userTriangle = newTriangle;
                    return;
                }
            }
        }

        public static void RayInput(out Ray userRay)
        {
            Ray newRay = new();

            Console.Write("Введите точку начала луча\nКоордината x: ");
            newRay.StartPointOfRay.x = NumberInput<double>();

            Console.Write("Координата y: ");
            newRay.StartPointOfRay.y = NumberInput<double>();

            Console.Write("Координата z: ");
            newRay.StartPointOfRay.z = NumberInput<double>();

            Console.Write("\nВведите горизонтальный угол луча(в градусах): ");
            newRay.HorizontalAngle = NumberInput<double>();

            Console.Write("Введите вертикальный угол луча(в градусах): ");
            newRay.VerticalAngle = NumberInput<double>();

            userRay = newRay;
        }

    }

}
