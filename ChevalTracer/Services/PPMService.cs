using System;
using System.IO;
using Cheval.Models;

namespace Cheval.Services
{
    public static class PPMService
    {
        private const int MaxColour = 255;

        public static void ToPPM(this Canvas canvas, string filename = "scene.ppm")
        {
            using (var s = File.OpenWrite(filename))
            using (var w = new StreamWriter(s))
            {
                w.WriteLine($"P3");
                w.WriteLine($"{canvas.Width} {canvas.Height}");
                w.WriteLine($"255");

                for (var j = 0; j < canvas.Height; j++)
                {
                    for (var i = 0; i < canvas.Width; i++)
                    {
                        var rgb = GetColorBytes(canvas.GetPixel(i, j));
                        w.WriteLine($"{rgb.Item1} {rgb.Item2} {rgb.Item3}");
                    }
                }
            }
        }

        private static int Clamp(int v, int min, int max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

        public static Tuple<int, int, int> GetColorBytes(ChevalColour c) =>
            Tuple.Create(
                Clamp((int)(c.Red * MaxColour), 0, MaxColour),
                Clamp((int)(c.Green * MaxColour), 0, MaxColour),
                Clamp((int)(c.Blue * MaxColour), 0, MaxColour));
    }

}
