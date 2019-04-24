﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly double _oneOverN;

        public RandomSuperSampler(Scene scene, Camera camera, int n = 4)
        {
            _scene = scene;
            _camera = camera;
            _N = n;
            _oneOverN = 1.0 / n;
        }

        public IEnumerable<Ray> Supersample(int px, int py)
        {
            var inv = _camera.InverseTransform;
            var origin = inv * Point(0, 0, 0);

            var pixelSize = _camera.PixelSize;
            var halfWidth = _camera.HalfWidth;
            var halfHeight = _camera.HalfHeight;

            for (var i = 0; i < _N; i++)
            {
                var xOffset = (px + 0.5);
                var yOffset = (py + 0.5);

                var rx = Rng.NextDouble();
                var ry = Rng.NextDouble();

                xOffset += (0.5 - rx);
                yOffset += (0.5 - ry);

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
            var rays = Supersample(x, y).ToList();
            foreach (var ray in rays)
            {
               // Interlocked.Increment(ref Stats.PrimaryRays);
                color += _scene.ColourAt(ray, Cheval.MaxNoOfReflections);
            }

            color *= _oneOverN;
            return color;
        }
    }
}
