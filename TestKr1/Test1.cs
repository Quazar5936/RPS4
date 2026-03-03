using System.Runtime.InteropServices;

namespace TestKR1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            KR1.Triangle testTriangle1 = new(new KR1.Point(0, 0, 5), new KR1.Point(0, 5, 0), new KR1.Point(5, 0, 0));
            KR1.Point hitPoint;
            KR1.Ray testRay1 = new(0, 0, 0, 0, 45);

            Assert.IsTrue(KR1.GeometryUtils.RayTriangleIntersect(testRay1, testTriangle1, out hitPoint));

        }

        [TestMethod]
        public void TestMethod2()
        {

            KR1.Triangle testTriangle2 = new(new KR1.Point(0, 0, 0), new KR1.Point(0, 5, 0), new KR1.Point(5, 0, 0));
            KR1.Point hitPoint;
            KR1.Ray testRay2 = new(0, 0, 0, 0, 45);

            Assert.IsFalse(KR1.GeometryUtils.RayTriangleIntersect(testRay2, testTriangle2, out hitPoint));

        }

        [TestMethod]
        public void TestMethod3()
        {

            KR1.Triangle testTriangle2 = new(new KR1.Point(0, 10, 0), new KR1.Point(0, 51, 0), new KR1.Point(15, 50, 70));
            KR1.Point hitPoint;
            KR1.Ray testRay2 = new(0, 0, 0, 0, 90);

            Assert.IsFalse(KR1.GeometryUtils.RayTriangleIntersect(testRay2, testTriangle2, out hitPoint));

        }
    }
}
