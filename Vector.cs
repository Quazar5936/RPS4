using System;
using System.Collections.Generic;
using System.Text;

namespace KR1
{
    public class Vector
    {
        public double x {  get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Vector()
        {
            x = 0.0; y = 0.0; z = 0.0;
        }

        public Vector(double userX, double userY, double userZ)
        {
            x = userX;
            y = userY;
            z = userZ;
        }

    }

}
