using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace KR2
{
    public static class Algorithm
    {
        // 18 446 744 073 709 551 615 - максимальное число для ulong, 20 цифр
        // 999 999 999 * 999 999 999 = 999 999 998 000 000 001, 18 знаков
        // 9 999 999 999 * 9 999 999 999 = 99 999 999 980 000 000 001, 20 знаков (но уже выходит за пределы ulong);
        private const int MAX_DIGITS_FOR_ULONG = 18;
        public static void AlgorithmRecIntMult(BigNumber number1, BigNumber number2, out BigNumber resNum)
        {
            resNum = Algorithm.RecIntMult(number1, number2);
            if (number1.Negative ^ number2.Negative)
            {
                resNum.Negative = true;
            }
        }

        // Алгоритм рекурсивного умножения
        
        static BigNumber RecIntMult(BigNumber number1, BigNumber number2)
        {
            int num1Len = number1.Number.Length;
            int num2Len = number2.Number.Length;

            if ((num1Len + num2Len) <= MAX_DIGITS_FOR_ULONG) 
            {
                ulong val1 = ulong.Parse(number1.Number);
                ulong val2 = ulong.Parse(number2.Number);
                return new BigNumber { Number = (val1 * val2).ToString() };
            }

            int maxLen = Math.Max(num1Len, num2Len);
            if (maxLen % 2 != 0) ++maxLen;
          
            string num1Str = number1.Number.PadLeft(maxLen, '0');
            string num2Str = number2.Number.PadLeft(maxLen, '0');

            int m = maxLen / 2;
            BigNumber a = new () { Number = num1Str.Substring(0, m) };
            BigNumber b = new () { Number = num1Str.Substring(m) };
            BigNumber c = new () { Number = num2Str.Substring(0, m) };
            BigNumber d = new () { Number = num2Str.Substring(m) };
            
            a.Trim(); b.Trim(); c.Trim(); d.Trim();

            BigNumber ac = RecIntMult(a, c);
            BigNumber ad = RecIntMult(a, d);
            BigNumber bc = RecIntMult(b, c);
            BigNumber bd = RecIntMult(b, d);

            BigNumber finalResult = new BigNumber(Add(Add(ShiftLeft(ac, 2 * m), ShiftLeft(Add(ad, bc), m)), bd));
            
            return finalResult;
        }

        // Сложение больших чисел
        static BigNumber Add(BigNumber number1, BigNumber number2)
        {
            char[] resStr = new char[Math.Max(number1.Number.Length, number2.Number.Length)];
            
            string num1Str = number1.Number.PadLeft(Math.Max(number1.Number.Length, number2.Number.Length), '0');
            string num2Str = number2.Number.PadLeft(Math.Max(number1.Number.Length, number2.Number.Length), '0');

            int carry = 0;
            
            for (int i = resStr.Length - 1;i >= 0; i--)
            {
                int digit1 = num1Str[i] - '0';
                int digit2 = num2Str[i] - '0';

                int result = digit1 + digit2 + carry;
                
                if (result > 9)
                {
                    carry = result / 10;
                    result %= 10;
                }
                else
                {
                    carry = 0;
                }

                resStr[i] = Convert.ToChar(result + '0');
            }

            BigNumber finalResult = new() { Number = new string(resStr) };
            
            if (carry != 0)
            {
                finalResult.Number = carry.ToString() + finalResult.Number;
            }
            finalResult.Trim();
            return finalResult;
        }

        // Добавление нулей справа к числу(10 ^ n)
        static BigNumber ShiftLeft(BigNumber number, int shift)
        {
            number.Trim();
            if (number.Number == "0") return new BigNumber { Number = "0" };
            return new BigNumber { Number = number.Number + new string('0', shift) };
        }
    }
}
