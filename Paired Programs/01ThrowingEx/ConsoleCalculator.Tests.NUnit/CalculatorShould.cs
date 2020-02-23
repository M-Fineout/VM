using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleCalculator.Tests.NUnit
{
    public class CalculatorShould
    {
        [Test]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Calculator();

            //Throws.TypeOf expects the exact exception type to be thrown
            Assert.That(() => sut.Calculate(1, 1, "+"), 
                Throws.TypeOf<CalculationOperationNotSupportedException>());

            Assert.That(() => sut.Calculate(1, 1, "+"),
                Throws.TypeOf<CalculationOperationNotSupportedException>()
                      .With
                      .Property("Operation").EqualTo("+"));

            //the Throws.InstanceOf method -- because a CalculationOperationNotSupportedException is an instance of a CalculationException, this test will now once again pass. Because the InstanceOf method also allows for derived exceptions.
            Assert.That(() => sut.Calculate(1, 1, "+"),
                Throws.InstanceOf<CalculationException>());

            //Same as MS.TEST
            Assert.Throws<CalculationOperationNotSupportedException>(
            () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<CalculationOperationNotSupportedException>(
            () => sut.Calculate(1, 1, "+"));

            Assert.That(ex.Operation, Is.EqualTo("+"));
        }

    }
}
