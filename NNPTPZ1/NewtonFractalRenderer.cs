using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Renders a Newton fractal for a given polynomial into a bitmap.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    public class NewtonFractalRenderer
    {
        private static readonly Color[] RootColors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        /// <summary>
        /// Builds a Newton fractal image from a validated render request.
        /// </summary>
        public Bitmap Render(FractalRenderRequest request)
        {
            Bitmap bitmap = new Bitmap(request.Width, request.Height);
            double xStep = (request.XMax - request.XMin) / request.Width;
            double yStep = (request.YMax - request.YMin) / request.Height;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial derivative = request.Polynomial.Derive();
            NewtonFractalSettings settings = request.Settings;

            Console.WriteLine(request.Polynomial);
            Console.WriteLine(derivative);

            for (int row = 0; row < request.Width; row++)
            {
                for (int column = 0; column < request.Height; column++)
                {
                    double y = request.YMin + row * yStep;
                    double x = request.XMin + column * xStep;

                    ComplexNumber guess = CreateInitialGuess(x, y, settings.ZeroReplacementEpsilon);
                    int iterationCount = RunNewtonIterations(
                        request.Polynomial,
                        derivative,
                        ref guess,
                        settings);

                    int rootIndex = ResolveRootIndex(roots, guess, settings.RootMatchDistanceSquared);
                    bitmap.SetPixel(column, row, ColorForRoot(rootIndex, iterationCount, settings.IterationDarkeningStep));
                }
            }

            return bitmap;
        }

        private static ComplexNumber CreateInitialGuess(double x, double y, double zeroReplacementEpsilon)
        {
            ComplexNumber guess = new ComplexNumber { Re = x, Imaginary = y };
            if (guess.Re == 0)
                guess.Re = zeroReplacementEpsilon;
            if (guess.Imaginary == 0)
                guess.Imaginary = zeroReplacementEpsilon;
            return guess;
        }

        /// <summary>
        /// Applies Newton's method until convergence or the iteration limit is reached.
        /// </summary>
        private static int RunNewtonIterations(
            Polynomial polynomial,
            Polynomial derivative,
            ref ComplexNumber guess,
            NewtonFractalSettings settings)
        {
            int iterationsUsed = 0;
            for (int iteration = 0; iteration < settings.MaxIterations; iteration++)
            {
                ComplexNumber diff = polynomial.Eval(guess).Divide(derivative.Eval(guess));
                guess = guess.Subtract(diff);
                iterationsUsed = iteration + 1;

                if (diff.SquaredMagnitude() < settings.StepConvergenceThresholdSquared)
                    break;
            }

            return iterationsUsed;
        }

        private static int ResolveRootIndex(
            List<ComplexNumber> roots,
            ComplexNumber guess,
            double rootMatchDistanceSquared)
        {
            for (int rootIndex = 0; rootIndex < roots.Count; rootIndex++)
            {
                if (guess.SquaredDistanceTo(roots[rootIndex]) <= rootMatchDistanceSquared)
                    return rootIndex;
            }

            roots.Add(guess);
            return roots.Count - 1;
        }

        private static Color ColorForRoot(int rootIndex, int iterationCount, int iterationDarkeningStep)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkening = iterationCount * iterationDarkeningStep;
            return Color.FromArgb(
                ClampByte(baseColor.R - darkening),
                ClampByte(baseColor.G - darkening),
                ClampByte(baseColor.B - darkening));
        }

        private static int ClampByte(int value)
        {
            return Math.Min(Math.Max(0, value), 255);
        }
    }
}
