using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private static ComplexNumber C(double re, double imaginary = 0)
        {
            return new ComplexNumber { Re = re, Imaginary = imaginary };
        }

        [TestMethod]
        public void EvalAtRealPointTest()
        {
            Polynomial polynomial = Polynomial.FromRealCoefficients(1, 0, 1);

            Assert.AreEqual(C(1), polynomial.Eval(0));
            Assert.AreEqual(C(2), polynomial.Eval(1));
            Assert.AreEqual(C(5), polynomial.Eval(2));

            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", polynomial.ToString());
        }

        [TestMethod]
        public void EvalAtComplexPointTest()
        {
            Polynomial polynomial = Polynomial.FromRealCoefficients(1, 0, 1);
            ComplexNumber point = C(1, 1);

            ComplexNumber actual = polynomial.Eval(point);
            ComplexNumber expected = C(2, 3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EvalUsingComplexNumberOverloadTest()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(C(1));
            polynomial.Coefficients.Add(C(0));
            polynomial.Coefficients.Add(C(1));

            Assert.AreEqual(C(1), polynomial.Eval(C(0)));
            Assert.AreEqual(C(2), polynomial.Eval(C(1)));
            Assert.AreEqual(C(5), polynomial.Eval(C(2)));
        }

        [TestMethod]
        public void DeriveTest()
        {
            Polynomial polynomial = Polynomial.FromRealCoefficients(1, 0, 0, 1);
            Polynomial derivative = polynomial.Derive();

            Assert.AreEqual(C(0), derivative.Eval(0));
            Assert.AreEqual(C(3), derivative.Eval(1));
            Assert.AreEqual(C(12), derivative.Eval(2));

            Polynomial quadratic = Polynomial.FromRealCoefficients(1, 1, 1);
            Polynomial quadraticDerivative = quadratic.Derive();
            Assert.AreEqual(C(1), quadraticDerivative.Eval(0));
            Assert.AreEqual(C(3), quadraticDerivative.Eval(1));
            Assert.AreEqual(C(5), quadraticDerivative.Eval(2));
        }
    }
}
