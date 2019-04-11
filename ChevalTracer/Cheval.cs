using System;
using System.Linq;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Primitives;
using Cheval.Services;
using static Cheval.Models.ChevalVector;

namespace Cheval
{
    public class Cheval
    {
        public const double Epsilon = 0.00001;

        static void Main(string[] args)
        {
            var cameraOrigin = new ChevalPoint(0,0,-5);
            var hits = 0;
            var wallZ = 10;
            var canvasSize = 200;
            double wallSize = 7.0;
            double pixelSize = wallSize / canvasSize;
            var halfCanvas = wallSize / 2;

            var canvas = new Canvas(canvasSize,canvasSize);
            var colour = new ChevalColour(1,0,0);
            var scene = new Sphere();
            scene.Transform = Transform.Shearing(1, 0, 0, 0, 0, 0);
            for (var y = 0; y < canvasSize; y++)
            {
                var worldY = halfCanvas - pixelSize * y;
                for (var x = 0; x < canvasSize; x++)
                {
                    var worldX = -halfCanvas + pixelSize * x;
                    var wallHit = new ChevalPoint(worldX, worldY, wallZ);

                    var ray = new Ray(cameraOrigin, Normalize((ChevalVector)(wallHit - cameraOrigin)));
                    var inters = new Intersections(ray.Intersect(scene));
                    if (inters.List.Any())
                    {
                        hits++;
                    }
                    if (inters.Hit() != null)
                    {
                        canvas.WritePixel(x, y, colour);
                    }
                }
            }
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
