using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Complex number with real and imaginary parts.
    /// </summary>
    public class ComplexNumber
    {
        public double Re { get; set; }
        public double Imaginary { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Re = 0,
            Imaginary = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber other = (ComplexNumber)obj;
                return other.Re == Re && other.Imaginary == Imaginary;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Re.GetHashCode();
                hash = hash * 23 + Imaginary.GetHashCode();
                return hash;
            }
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            ComplexNumber a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                Re = a.Re * other.Re - a.Imaginary * other.Imaginary,
                Imaginary = a.Re * other.Imaginary + a.Imaginary * other.Re
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(SquaredMagnitude());
        }

        /// <summary>
        /// Returns |z|² without taking the square root.
        /// </summary>
        public double SquaredMagnitude()
        {
            return Re * Re + Imaginary * Imaginary;
        }

        /// <summary>
        /// Squared Euclidean distance to another complex number.
        /// </summary>
        public double SquaredDistanceTo(ComplexNumber other)
        {
            double deltaRe = Re - other.Re;
            double deltaImaginary = Imaginary - other.Imaginary;
            return deltaRe * deltaRe + deltaImaginary * deltaImaginary;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Re = a.Re + other.Re,
                Imaginary = a.Imaginary + other.Imaginary
            };
        }

        /// <summary>
        /// Argument (phase angle) in degrees, using atan2 for correct quadrant.
        /// </summary>
        public double GetAngleInDegrees()
        {
            return Math.Atan2(Imaginary, Re) * (180.0 / Math.PI);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Re = a.Re - other.Re,
                Imaginary = a.Imaginary - other.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Re} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            var numerator = this.Multiply(new ComplexNumber() { Re = divisor.Re, Imaginary = -divisor.Imaginary });
            var denominator = divisor.Re * divisor.Re + divisor.Imaginary * divisor.Imaginary;

            return new ComplexNumber()
            {
                Re = numerator.Re / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }
    }
}
