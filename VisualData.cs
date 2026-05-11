using System;
using System.Collections.Generic;
using System.Text;

namespace KR33
{
    public class VisualData
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal minusY { get; set; }
        public VisualData(decimal newX, decimal newY, decimal newMinusY)
        {
            X = newX;
            Y = newY;
            minusY = newMinusY;
        }


    }
}
