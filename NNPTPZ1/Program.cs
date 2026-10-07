using System;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// Entry point: parses CLI arguments and produces a Newton fractal image.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FractalRenderRequest request = CommandLineArgumentsParser.Parse(args);
                var renderer = new NewtonFractalRenderer();
                Bitmap bitmap = renderer.Render(request);
                bitmap.Save(request.OutputPath ?? "../../../out.png");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
