using KR33;

namespace KR3_TEST
{
    [TestClass]
    public sealed class Test1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var expected = new List<KR33.VisualData>
            {
            new KR33.VisualData(1, 7.0574363197557374352155502841m, -7.0574363197557374352155502841m),
            new KR33.VisualData(2, 6.2609903369994111499456862724m, -6.2609903369994111499456862724m),
            new KR33.VisualData(3, 5.2337933272715873063666001467m, -5.2337933272715873063666001467m),
            new KR33.VisualData(4, 3.7869170496250298432863928354m, -3.7869170496250298432863928354m)
            };

            var actual = new List<KR33.VisualData>();
            decimal leftBound = 1, rightBound = 4, a = 5, step = 1;
            decimal delta = 1e-20m;

            for (decimal x = leftBound; x <= rightBound; x += step)
            {
                decimal y = KR33.MainForm.FormulaCalc(x, a);
                actual.Add(new KR33.VisualData(x, y, -y));
            }

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Y, actual[i].Y, delta);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var expected = new List<KR33.VisualData>
            {
            new KR33.VisualData(1, 0, 0),

            };

            var actual = new List<KR33.VisualData>();
            decimal leftBound = 1, rightBound = 1, a = 1, step = 1;
            decimal delta = 1e-20m;

            for (decimal x = leftBound; x <= rightBound; x += step)
            {
                decimal y = KR33.MainForm.FormulaCalc(x, a);
                actual.Add(new KR33.VisualData(x, y, -y));
            }

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Y, actual[i].Y, delta);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expected = new List<KR33.VisualData>();

            var actual = new List<KR33.VisualData>();
            decimal leftBound = 1, rightBound = 0, a = 1, step = 1;
            decimal delta = 1e-20m;

            for (decimal x = leftBound; x <= rightBound; x += step)
            {
                decimal y = KR33.MainForm.FormulaCalc(x, a);
                actual.Add(new KR33.VisualData(x, y, -y));
            }

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Y, actual[i].Y, delta);
            }
        }
    }
}
