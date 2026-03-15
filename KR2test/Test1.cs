namespace KR2Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            KR2.BigNumber number1 = new() { Number = "1321321321321", Negative = true };
            KR2.BigNumber number2 = new() { Number = "1321321321321", Negative = true };
            KR2.BigNumber result = new();

            KR2.BigNumber answer = new() { Number = "1745890034177473329185041", Negative = false };
            string allowedSymbols = "1234567890";

            Assert.IsTrue(KR2.HelpingFunctions.ThisIsCorrectNumber(number1, allowedSymbols));
            Assert.IsTrue(KR2.HelpingFunctions.ThisIsCorrectNumber(number2, allowedSymbols));

            KR2.Algorithm.AlgorithmRecIntMult(number1, number2, out result);

            Assert.AreEqual(answer.Number, result.Number);
            Assert.AreEqual(answer.Negative, result.Negative);
        }

        [TestMethod]
        public void TestMethod2()
        {
            KR2.BigNumber number1 = new() { Number = "13213     21321321", Negative = true };
            KR2.BigNumber number2 = new() { Number = "1     321321321321", Negative = true };
            KR2.BigNumber result = new();

            KR2.BigNumber answer = new() { Number = "1745890034177473329185041", Negative = false };
            string allowedSymbols = "1234567890";

            Assert.IsFalse(KR2.HelpingFunctions.ThisIsCorrectNumber(number1, allowedSymbols));
            Assert.IsFalse(KR2.HelpingFunctions.ThisIsCorrectNumber(number2, allowedSymbols));
        }

        [TestMethod]
        public void TestMethod3()
        {
            KR2.BigNumber number1 = new() { Number = "37462387464748932901429482904324948932749823749823743487238947389432789", Negative = false };
            KR2.BigNumber number2 = new() { Number = "19837583758937538953895348957458943758943758934754389574389573489", Negative = true };
            KR2.BigNumber result = new();

            KR2.BigNumber answer = new() { Number = "743163249141728676427352273565934684770186851697851100634517230115220794239115826367772061585710509890391596653974243506530659341730821", Negative = true };
            string allowedSymbols = "1234567890";

            Assert.IsTrue(KR2.HelpingFunctions.ThisIsCorrectNumber(number1, allowedSymbols));
            Assert.IsTrue(KR2.HelpingFunctions.ThisIsCorrectNumber(number2, allowedSymbols));

            KR2.Algorithm.AlgorithmRecIntMult(number1, number2, out result);

            Assert.AreEqual(answer.Number, result.Number);
            Assert.AreEqual(answer.Negative, result.Negative);
        }
    }
}
