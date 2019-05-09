using System;
using System.Collections.Generic;
using System.Linq;
using Cheval.Integrators;
using Cheval.Models;
using Cheval.Templates;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Samplers
{
    public class RandomSuperSampler : ISampler
    {
        private static readonly Random Rng = new Random();
        private readonly Scene _scene;
        private readonly Camera _camera;
        private readonly int _N;
        private readonly IIntegrator _integrator;
        private readonly float _oneOverN;

        public RandomSuperSampler(Scene scene, Camera camera,  IIntegrator integrator, int n = 4)
        {
            _scene = scene;
            _camera = camera;
            _N = n;
            _integrator = integrator;
            _oneOverN = 1.0f / n;
        }

        public IEnumerable<Ray> SuperSample(int px, int py)
        {
            var inv = _camera.InverseTransform;
            var origin = inv * Point(0, 0, 0);

            var pixelSize = _camera.PixelSize;
            var halfWidth = _camera.HalfWidth;
            var halfHeight = _camera.HalfHeight;

            for (var i = 0; i < _N; i++)
            {
                var xOffset = (px + 0.5f);
                var yOffset = (py + 0.5f);

                var rx = (float)Rng.NextDouble();
                var ry = (float)Rng.NextDouble();

                xOffset += (0.5f - rx);
                yOffset += (0.5f - ry);

                xOffset *= pixelSize;
                yOffset *= pixelSize;

                var worldX = halfWidth - xOffset;
                var worldY = halfHeight - yOffset;

                var pixel = inv * Point(worldX, worldY, -1);
                var direction = Normalize(pixel - origin);

                yield return new Ray(origin, direction);
            }
        }

        public ChevalColour Sample(int x, int y)
        {
            var color = ColourTemplate.Black;
            var rays = SuperSample(x, y).ToList();
            foreach (var ray in rays)
            {
               // Interlocked.Increment(ref Stats.PrimaryRays);
                color += _integrator.ColourAt(ray, Cheval.MaxNoOfReflections, _scene);
            }

            color *= _oneOverN;
            return color;
        }
    }
}
