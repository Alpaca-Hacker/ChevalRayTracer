using System.Linq;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Primitives;
using Cheval.Services;
using static Cheval.Models.ChevalTuple;

namespace Cheval
{
    public class Cheval
    {
        public const double Epsilon = 0.00001;

        static void Main(string[] args)
        {
            var cameraOrigin = Point(0,0,-5);
            var wallZ = 10;
            var canvasSize = 200;
            double wallSize = 7.0;
            double pixelSize = wallSize / canvasSize;
            var halfCanvas = wallSize / 2;

            var canvas = new Canvas(canvasSize,canvasSize);
            var scene = new Sphere();
            var mat = new Material(new ChevalColour(1, 0.2, 1),.1, .9, .9,300);
            scene.Material = mat;
              //  Transform = Transform.Shearing(1, 0, 0, 0, 0, 0)
           
            var lightPos = Point(-10, 10, 1);
            var lightColour = new ChevalColour(1,1,1);
            var light = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };

            for (var y = 0; y < canvasSize; y++)
            {
                var worldY = halfCanvas - pixelSize * y;
                for (var x = 0; x < canvasSize; x++)
                {
                    var worldX = -halfCanvas + pixelSize * x;
                    var wallHit = Point(worldX, worldY, wallZ);

                    var ray = new Ray(cameraOrigin, Normalize(wallHit - cameraOrigin));
                    var inters = new Intersections(ray.Intersect(scene));
                    var hit = inters.Hit();
                    if (hit != null)
                    {
                        var point = ray.Position(hit.T);
                        var normal = scene.NormalAt(point);
                        var eyeV = -ray.Direction;
                        var colour = scene.Material.Lighting(light, point, eyeV, normal);

                        canvas.WritePixel(x, y, colour);
                    }
                }
            }
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
