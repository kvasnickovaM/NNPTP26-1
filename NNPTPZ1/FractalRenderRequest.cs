using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Image bounds, polynomial, and Newton settings for one render pass.
    /// </summary>
    public class FractalRenderRequest
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }
        public Polynomial Polynomial { get; set; }
        public NewtonFractalSettings Settings { get; set; } = new NewtonFractalSettings();
    }
}
