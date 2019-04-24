﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Cheval.Models;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Samplers
{
    public class DefaultSampler : ISampler
    {
        private readonly Scene _scene;
        private readonly Camera _camera;

        public DefaultSampler(Scene world, Camera camera)
        {
            _scene = world;
            _camera = camera;
        }

        public Ray RayForPixel(int px, int py)
        {
            var pixelSize = _camera.PixelSize;

            var halfWidth = _camera.HalfWidth;
            var halfHeight = _camera.HalfHeight;

            var xOffset = (px + 0.5) * pixelSize;
            var yOffset = (py + 0.5) * pixelSize;

            var worldX = halfWidth - xOffset;
            var worldY = halfHeight - yOffset;

            var inv = _camera.InverseTransform;

            var pixel = inv * Point(worldX, worldY, -1);
            var origin = inv * Point(0, 0, 0);
            var direction = Normalize(pixel - origin);

            return new Ray(origin, direction);
        }

        public ChevalColour Sample(int x, int y)
        {
            //Interlocked.Increment(ref Stats.PrimaryRays);
            var ray = RayForPixel(x, y);
            return _scene.ColourAt(ray, Cheval.MaxNoOfReflections);
        }
    }
}