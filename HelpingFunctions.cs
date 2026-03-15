using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KR2
{
    public static class HelpingFunctions
    {
        public static bool ThisIsCorrectNumber(BigNumber number, string allowedSymbols)
        {
            
            if (string.IsNullOrWhiteSpace(number.Number) || number.Number.Length > 100)
            {
                return false; 
            }
            else if (number.Number == "-")
            {
                return false;
            }
            
            for(int i = 0;i < number.Number.Length; i++)
            {
                if (!(allowedSymbols.Contains(number.Number[i]) || (i == 0 && number.Number[i] == '-')))
                {
                    return false;
                }
            }
           
            return true;
        }

        public static int GetRandomNumber(int min, int max)
        {
            return Random.Shared.Next(min, max + 1);
        }
    }
}
