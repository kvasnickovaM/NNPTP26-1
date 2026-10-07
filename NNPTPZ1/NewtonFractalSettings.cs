namespace NNPTPZ1
{
    /// <summary>
    /// Tunable parameters for Newton fractal generation (defaults match legacy behavior where sensible).
    /// </summary>
    public class NewtonFractalSettings
    {
        public const int DefaultMaxIterations = 30;
        public const double DefaultStepConvergenceThresholdSquared = 1e-10;
        public const double DefaultRootMatchDistanceSquared = 0.01;
        public const double DefaultZeroReplacementEpsilon = 0.0001;
        public const int DefaultIterationDarkeningStep = 2;

        public int MaxIterations { get; set; } = DefaultMaxIterations;
        public double StepConvergenceThresholdSquared { get; set; } = DefaultStepConvergenceThresholdSquared;
        public double RootMatchDistanceSquared { get; set; } = DefaultRootMatchDistanceSquared;
        public double ZeroReplacementEpsilon { get; set; } = DefaultZeroReplacementEpsilon;
        public int IterationDarkeningStep { get; set; } = DefaultIterationDarkeningStep;
    }
}
