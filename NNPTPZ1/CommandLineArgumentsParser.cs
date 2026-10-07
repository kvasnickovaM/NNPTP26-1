using System;
using System.Globalization;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Parses and validates command-line arguments for fractal rendering.
    /// </summary>
    public static class CommandLineArgumentsParser
    {
        private const int RequiredArgumentCount = 7;

        /// <summary>
        /// Arguments: width height xmin xmax ymin ymax outputPath
        /// [maxIterations stepConvergenceThresholdSquared rootMatchDistanceSquared zeroReplacementEpsilon coeff0 coeff1 ...]
        /// </summary>
        public static FractalRenderRequest Parse(string[] args)
        {
            if (args == null || args.Length < RequiredArgumentCount)
            {
                throw new ArgumentException(
                    $"Expected at least {RequiredArgumentCount} arguments: width height xmin xmax ymin ymax outputPath.");
            }

            var request = new FractalRenderRequest
            {
                Width = ParsePositiveInt(args[0], "width"),
                Height = ParsePositiveInt(args[1], "height"),
                XMin = ParseDouble(args[2], "xmin"),
                XMax = ParseDouble(args[3], "xmax"),
                YMin = ParseDouble(args[4], "ymin"),
                YMax = ParseDouble(args[5], "ymax"),
                OutputPath = args[6]
            };

            if (request.XMin >= request.XMax)
                throw new ArgumentException("xmin must be less than xmax.");
            if (request.YMin >= request.YMax)
                throw new ArgumentException("ymin must be less than ymax.");

            var settings = new NewtonFractalSettings();
            int optionalIndex = RequiredArgumentCount;

            if (args.Length > optionalIndex)
            {
                settings.MaxIterations = ParsePositiveInt(args[optionalIndex++], nameof(settings.MaxIterations));
            }
            if (args.Length > optionalIndex)
            {
                settings.StepConvergenceThresholdSquared = ParseNonNegativeDouble(
                    args[optionalIndex++],
                    nameof(settings.StepConvergenceThresholdSquared));
            }
            if (args.Length > optionalIndex)
            {
                settings.RootMatchDistanceSquared = ParseNonNegativeDouble(
                    args[optionalIndex++],
                    nameof(settings.RootMatchDistanceSquared));
            }
            if (args.Length > optionalIndex)
            {
                settings.ZeroReplacementEpsilon = ParsePositiveDouble(
                    args[optionalIndex++],
                    nameof(settings.ZeroReplacementEpsilon));
            }

            request.Settings = settings;

            if (args.Length > optionalIndex)
            {
                request.Polynomial = ParsePolynomialFromRemainingArgs(args, optionalIndex);
            }
            else
            {
                request.Polynomial = Polynomial.FromRealCoefficients(1, 0, 0, 1);
            }

            ValidatePolynomial(request.Polynomial);
            return request;
        }

        private static Polynomial ParsePolynomialFromRemainingArgs(string[] args, int startIndex)
        {
            var polynomial = new Polynomial();
            for (int i = startIndex; i < args.Length; i++)
            {
                double coefficient = ParseDouble(args[i], $"coefficient[{i - startIndex}]");
                polynomial.Coefficients.Add(new ComplexNumber { Re = coefficient, Imaginary = 0 });
            }

            if (polynomial.Coefficients.Count == 0)
                throw new ArgumentException("At least one polynomial coefficient is required.");

            return polynomial;
        }

        private static void ValidatePolynomial(Polynomial polynomial)
        {
            if (polynomial.Coefficients.Count < 2)
            {
                throw new ArgumentException("Polynomial must have degree at least 1 (at least two coefficients).");
            }
        }

        private static int ParsePositiveInt(string value, string name)
        {
            int parsed = int.Parse(value, CultureInfo.InvariantCulture);
            if (parsed <= 0)
                throw new ArgumentException($"{name} must be positive.");
            return parsed;
        }

        private static double ParseDouble(string value, string name)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsed))
                throw new ArgumentException($"Invalid number for {name}: '{value}'.");
            return parsed;
        }

        private static double ParseNonNegativeDouble(string value, string name)
        {
            double parsed = ParseDouble(value, name);
            if (parsed < 0)
                throw new ArgumentException($"{name} must be non-negative.");
            return parsed;
        }

        private static double ParsePositiveDouble(string value, string name)
        {
            double parsed = ParseDouble(value, name);
            if (parsed <= 0)
                throw new ArgumentException($"{name} must be positive.");
            return parsed;
        }
    }
}
