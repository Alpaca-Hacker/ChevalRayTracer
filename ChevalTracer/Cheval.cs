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

        static void Main(string[] args)
        {
            var cameraOrigin = Point(-2,0,-10);
            var wallZ = 10;
            var canvasSize = 200;
            double wallSize = 9.0;
            double pixelSize = wallSize / canvasSize;
            var halfCanvas = wallSize / 2;

            var canvas = new Canvas(canvasSize,canvasSize);
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
            var camera = new Camera(canvasSize,canvasSize,Math.PI/2);
            System.IO.File.WriteAllText(@".\Scene.ppm", canvas.ToPPM());
        }

    }

}
