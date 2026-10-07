using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        private static ComplexNumber C(double re, double imaginary = 0)
        {
            return new ComplexNumber { Re = re, Imaginary = imaginary };
        }

        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = C(10, 20);
            ComplexNumber b = C(1, 2);

            ComplexNumber actual = a.Add(b);
            ComplexNumber expected = C(11, 22);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("(10 + 20i)", a.ToString());
            Assert.AreEqual("(1 + 2i)", b.ToString());

            a = C(1, -1);
            b = ComplexNumber.Zero;
            expected = C(1, -1);
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("(1 + -1i)", a.ToString());
            Assert.AreEqual("(0 + 0i)", b.ToString());
        }

        [TestMethod]
        public void SubtractTest()
        {
            ComplexNumber a = C(10, 20);
            ComplexNumber b = C(1, 2);

            ComplexNumber actual = a.Subtract(b);
            ComplexNumber expected = C(9, 18);
            Assert.AreEqual(expected, actual);

            actual = b.Subtract(a);
            expected = C(-9, -18);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            ComplexNumber a = C(1, 2);
            ComplexNumber b = C(3, 4);

            ComplexNumber actual = a.Multiply(b);
            ComplexNumber expected = C(-5, 10);
            Assert.AreEqual(expected, actual);

            actual = a.Multiply(ComplexNumber.Zero);
            Assert.AreEqual(ComplexNumber.Zero, actual);
        }

        [TestMethod]
        public void DivideTest()
        {
            ComplexNumber dividend = C(1, 2);
            ComplexNumber divisor = C(1, 1);

            ComplexNumber actual = dividend.Divide(divisor);
            ComplexNumber expected = C(1.5, 0.5);
            Assert.AreEqual(expected, actual);

            ComplexNumber reconstructed = actual.Multiply(divisor);
            Assert.AreEqual(dividend, reconstructed);
        }
    }
}
