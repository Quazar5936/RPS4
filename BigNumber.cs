using System;
using System.Collections.Generic;
using System.Text;

namespace KR2
{
    public class BigNumber
    {
        public string Number { get; set; }
        public bool Negative { get; set; }

        public BigNumber()
        {
            Number = "";
            Negative = false;
        }

        public BigNumber(BigNumber bigNumber)
        {
            Number = bigNumber.Number;
            Negative = bigNumber.Negative;
        }

        public void Trim()
        {
            Number = Number.TrimStart('0');
            if (string.IsNullOrEmpty(Number)) Number = "0";
        }


    }
}
