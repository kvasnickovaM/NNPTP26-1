using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Polynomial with complex coefficients.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Coefficients from lowest to highest power.
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Builds a polynomial from real coefficients (lowest power first).
        /// </summary>
        public static Polynomial FromRealCoefficients(params double[] realCoefficients)
        {
            var polynomial = new Polynomial();
            foreach (double coefficient in realCoefficients)
            {
                polynomial.Coefficients.Add(new ComplexNumber { Re = coefficient, Imaginary = 0 });
            }

            return polynomial;
        }

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int power = 1; power < Coefficients.Count; power++)
            {
                derivative.Coefficients.Add(Coefficients[power].Multiply(new ComplexNumber() { Re = power }));
            }

            return derivative;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { Re = x, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber powerOfX = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        powerOfX = powerOfX.Multiply(x);

                    coefficient = coefficient.Multiply(powerOfX);
                }

                sum = sum.Add(coefficient);
            }

            return sum;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
