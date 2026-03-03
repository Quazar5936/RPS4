using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace KR1
{
    public class Triangle
    {
        public Point FirstVertex {  get; set; }
        public Point SecondVertex { get; set; }
        public Point ThirdVertex { get; set; }

        public Triangle() 
        {
            FirstVertex = new Point();
            SecondVertex = new Point();
            ThirdVertex = new Point();
        }
        public Triangle(Point firstVertex, Point secondVertex, Point thirdVertex)
        {
            FirstVertex = new Point(firstVertex.x, firstVertex.y, firstVertex.z);
            SecondVertex = new Point(secondVertex.x, secondVertex.y, secondVertex.z);
            ThirdVertex = new Point(thirdVertex.x, thirdVertex.y, thirdVertex.z);
        }
        
    }

}
