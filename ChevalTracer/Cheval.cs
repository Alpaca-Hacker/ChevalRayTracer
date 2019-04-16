using System;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Services;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval
{
    public class Cheval
    {
        public const double Epsilon = 0.00001;
        public const int MaxNoOfReflections = 5;
        public const int Size = 1;

        public static void Run()
        {
            var cameraOrigin = Point(-2.6,1.5,-3.9);
            var cameraDirection = Point(-0.6, 1, -0.8);
            var up = Vector(0, 1, 0);
            var fov = 1.152;
            var viewTransform = Transform.ViewTransform(cameraOrigin, cameraDirection, up);

            var scene = SceneBuilder.Build();
            var camera = new Camera(400* Size, 200*Size, fov)
            {
                Transform = viewTransform
            };
            var canvas = camera.Render(scene);
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
