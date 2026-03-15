using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace KR1
{
    public static class GeometryUtils
    {
        // Допустимая погрешность
        const double EPSILON = 1e-6;

        public static bool RayTriangleIntersect(Ray ray, Triangle triangle, out Point hitPoint)
        {
            hitPoint = new Point();
           
            // Получение вершин треугольника
            Point firstVertex = triangle.FirstVertex;
            Point secondVertex = triangle.SecondVertex;
            Point thirdVertex = triangle.ThirdVertex;

            // Получение данных луча
            Point origin = ray.StartPointOfRay;
            Vector direction = ray.Direction;

            // Ребра треугольника: edgeV2V1 = vertex2 - vertex1, edgeV3V1 = vertex3 - vertex1
            Vector edgeV2V1 = Subtract(secondVertex, firstVertex);
            Vector edgeV3V1 = Subtract(thirdVertex, firstVertex);

            // Векторное произведение: crossProductDirectionAndEdgeV3V1 = direction × edgeV3V1
            Vector crossProductDirectionAndEdgeV3V1 = CrossProduct(direction, edgeV3V1);

            // Детерминант(для проверки параллельности луча и плоскости треугольника)
            // det = edgeV2V1 · crossProductDirectionAndEdgeV3V1
            double determinant = DotProduct(edgeV2V1, crossProductDirectionAndEdgeV3V1);
            
            // Детерминант = 0 - луч и плоскость треугольника параллельны
            if (Math.Abs(determinant) < EPSILON)
            {
                return false;
            }

            double inverseDeterminant = 1.0 / determinant;

            // Вектор от V1 до начала луча: edgeV1toOriginVec = origin - V1
            Vector edgeV1toOriginVec = Subtract(origin, firstVertex);

            // Барицентрическая координата u: u = invDet * (edgeV1toOriginVec · crossProductDirectionAndEdgeV3V1)
            double u = inverseDeterminant * DotProduct(edgeV1toOriginVec, crossProductDirectionAndEdgeV3V1);
            
            // Проверка выхода за пределы треугольника по u
            if (u < 0.0)
            {
                return false;
            } 
            
            // Векторное произведение: crossProductedgeV1toOriginVecAndEdgeV2V1 = edgeV1toOriginVec × edgeV2V1
            Vector crossProductDirectionAndEdgeV3V1AndEdgeV2V1 = CrossProduct(edgeV1toOriginVec, edgeV2V1);

            // Барицентрическая координата v: v = invDet * (direction · crossProductDirectionAndEdgeV3V1AndEdgeV2V1)
            double v = inverseDeterminant * DotProduct(direction, crossProductDirectionAndEdgeV3V1AndEdgeV2V1);

            // Проверка выхода за пределы треугольника по V и сумме U+V
            if (v < 0.0 || u + v > 1.0)
            {
                return false;
            }

            // Расстояние до пересечения: distance = invDet * (edgeV3V1 · crossProductDirectionAndEdgeV3V1AndEdgeV2V1)
            double distance = inverseDeterminant * DotProduct(edgeV3V1, crossProductDirectionAndEdgeV3V1AndEdgeV2V1);
            
            // Проверка: пересечение должно быть впереди луча (distance > 0)
            if (distance > EPSILON)
            {
                hitPoint = ray.GetPointAtDistance(distance);
                return true;
            }

            return false;
        }

        // Вспомогательные векторные методы

        // Разность координат (если разность между координатами двух точек - нахождение вектора, указывающего с a2 на a1)
        static Vector Subtract(Point a1, Point a2)
        {
            return new Vector(a1.x - a2.x, 
                              a1.y - a2.y, 
                              a1.z - a2.z);
        }
        
        //Скалярное произведение
        static double DotProduct(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        //Векторное произведение
        static Vector CrossProduct(Vector a, Vector b)
        {
            return new Vector(a.y * b.z - a.z * b.y,
                              a.z * b.x - a.x * b.z,
                              a.x * b.y - a.y * b.x);
        }

        static double VectorLen(Vector vec)
        {
            return Math.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        }
        
        // Проверка на существование треугольника
        public static bool ThisTriangleIsExists(Triangle triangle)
        {
            List<double> edgeLens = new();

            Vector edgeV2V1vec = Subtract(triangle.SecondVertex, triangle.FirstVertex);
            edgeLens.Add(VectorLen(edgeV2V1vec));

            Vector edgeV3V1vec = Subtract(triangle.ThirdVertex, triangle.FirstVertex);
            edgeLens.Add(VectorLen(edgeV3V1vec));

            Vector edgeV3V2vec = Subtract(triangle.ThirdVertex, triangle.SecondVertex);
            edgeLens.Add(VectorLen(edgeV3V2vec));

            double maxEdgeLen = 0.0;
            int maxLenIndex = 0;
            for(int i = 0;i < edgeLens.Count; i++)
            {
                if (maxEdgeLen < edgeLens[i])
                {
                    maxEdgeLen = edgeLens[i];
                    maxLenIndex = i;
                }
            }
          
            double notMaxEdgeLensSum = 0.0;
            for(int i = 0;i < edgeLens.Count; i++)
            {
                if (i != maxLenIndex)
                {
                    notMaxEdgeLensSum += edgeLens[i];
                }
            }

            if (notMaxEdgeLensSum > maxEdgeLen + EPSILON)
            {
                return true;
            }

            return false;
        }

    }

}
