using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Text;

namespace KR1
{
    public class Ray
    {
        public Point StartPointOfRay { get; set; }

        public Vector Direction { get; set; }

        public double HorizontalAngle 
        {   
            get;
            set 
            {
                field = value;
                CalculateDirection();
            } 
        }

        public double VerticalAngle { 
            get; 
            set
            {
                field = value;
                CalculateDirection();
            }
        }

        public Ray()
        {
            StartPointOfRay = new Point();
            Direction = new Vector();
        }

        public Ray(double x, double y, double z, double userHorizontalAngle = 0, double userVerticalAngle = 0)
        {
            StartPointOfRay = new Point(x, y, z);
            Direction = new Vector();

            HorizontalAngle = userHorizontalAngle;
            VerticalAngle = userVerticalAngle;
            
            CalculateDirection();
        }

        public Point GetPointAtDistance(double distance)
        {
            Point point = new();

            point.x = StartPointOfRay.x + distance * Direction.x;
            point.y = StartPointOfRay.y + distance * Direction.y;
            point.z = StartPointOfRay.z + distance * Direction.z;

            return point;
        }

        // Приведение вектора к нормализованному вектору
        void Normalize()
        {
            double length = Math.Sqrt(Direction.x * Direction.x + Direction.y * Direction.y + Direction.z * Direction.z);
            if (length > 0)
            {
                Direction.x /= length;
                Direction.y /= length;
                Direction.z /= length;
            }
        }
        void CalculateDirection()
        {
            // Перевод из градусов в радианы
            double horizontalAngleRad = HorizontalAngle * Math.PI / 180;
            double verticalAngleRad = VerticalAngle * Math.PI / 180;

            // Инициализация вектора направления
            double dx = Math.Cos(verticalAngleRad) * Math.Cos(horizontalAngleRad);
            double dy = Math.Cos(verticalAngleRad) * Math.Sin(horizontalAngleRad);
            double dz = Math.Sin(verticalAngleRad);

            Direction.x = dx; Direction.y = dy; Direction.z = dz;

            Normalize();
        }

    }

}
