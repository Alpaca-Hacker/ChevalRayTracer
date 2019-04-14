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

        public static void Render()
        {
            var cameraOrigin = Point(-2,0,-10);
            var cameraDirection = Point(-1, 0, 0);
            var up = Point(0, 1, 1);
            var fov = Math.PI / 10;
            var viewTransform = Transform.ViewTransform(cameraOrigin, cameraDirection, up);

            var canvasSize = 200;

            var scene = Scene.Default();
          //  var mat = new Material(new ChevalColour(1, 0.2, 1),.1, .9, .9,300);
           // scene.Material = mat;
              //  Transform = Transform.Shearing(1, 0, 0, 0, 0, 0)
           
            var lightPos = Point(-10, -10, 1);
            var lightColour = new ChevalColour(1,1,1);
            var light = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
            scene.Lights.Add(light);
            scene.Shapes[1].Transform = Transform.Translation(-2, 1, 2);

            var camera = new Camera(canvasSize,canvasSize,fov);
            camera.Transform = viewTransform;
            var canvas = camera.Render(scene);
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
