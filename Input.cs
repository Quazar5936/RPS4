using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static KR2.ConsoleInterface;

namespace KR2
{
    static internal class Input
    {
        public static T NumberInput<T>() where T : IParsable<T>
        {
            T number = default;
            bool parseResult = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out number);

            while (!parseResult)
            {
                Console.Write("Введенное вами число некорректно, введите, пожалуйста, новое: ");

                parseResult = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out number);
            }

            return number;
        }

        public static T EnumInput<T>() where T : struct, Enum
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

        public static BigNumber BigNumberInput()
        {
            BigNumber userNumber = new BigNumber();
           
            userNumber.Number = Console.ReadLine() ?? "";
            string allowedSymbols = "1234567890";
            
            while (!HelpingFunctions.ThisIsCorrectNumber(userNumber, allowedSymbols))
            {
                Console.Write("Введенное вами число некорректно, введите пожалуйста новое: ");
                userNumber.Number = Console.ReadLine() ?? "";
            }

            if (userNumber.Number[0] == '-')
            {
                userNumber.Negative = true;
                userNumber.Number = userNumber.Number.Remove(0, 1);
            }

            return userNumber;
        }

        public static BigNumber RandomBigNumberInput()
        {
            const int numberWillBeNegative = 1;
            BigNumber userNumber = new BigNumber();
            
            if (HelpingFunctions.GetRandomNumber(0, 1) == numberWillBeNegative)
            {
                userNumber.Negative = true;
            }

            int RandomLenOfNumber = HelpingFunctions.GetRandomNumber(2, 100);

            for(int i = 0;i < RandomLenOfNumber; i++)
            {
                if (i == 0)
                {
                    userNumber.Number += (HelpingFunctions.GetRandomNumber(1, 9)).ToString();
                    continue;
                }
                userNumber.Number += (HelpingFunctions.GetRandomNumber(0, 9)).ToString();
            }

            return userNumber;
        }
    }
}
