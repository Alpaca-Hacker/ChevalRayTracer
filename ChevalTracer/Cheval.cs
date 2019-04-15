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
        public const int Size = 3;

        public static void Run()
        {
            var cameraOrigin = Point(0,1.5,-5);
            var cameraDirection = Point(0, 1, 0);
            var up = Vector(0, 1, 0);
            var fov = Math.PI / 3;
            var viewTransform = Transform.ViewTransform(cameraOrigin, cameraDirection, up);

            var scene = SceneBuilder.Build();
            var camera = new Camera(150* Size, 75*Size, fov)
            {
                Transform = viewTransform
            };
            var canvas = camera.Render(scene);
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
