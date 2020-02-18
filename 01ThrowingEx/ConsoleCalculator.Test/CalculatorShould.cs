using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleCalculator.Test
{
    [TestClass]
    public class CalculatorShould
    {
        [TestMethod]
        public void ThrowWhenUnsupportedOperation()
        {
            //sut = system under test
            var sut = new Calculator();

            Assert.ThrowsException<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));


            //Assert.ThrowsException method requires that the exact exception is thrown. //this will fail
            //Assert.ThrowsException<CalculationException>(
            //   () => sut.Calculate(1, 1, "+"));

            var ex = Assert.ThrowsException<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.AreEqual("+", ex.Operation);

        }
    }
}
