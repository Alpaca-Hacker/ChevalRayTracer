using System;
using System.Diagnostics;
using Cheval.Helper;
using Cheval.Integrators;
using Cheval.Models;
using Cheval.Samplers;
using Cheval.Scenes;
using Cheval.Services;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval
{
    public class Cheval
    {
        public const float Epsilon = 0.001F;
        public const int MaxNoOfReflections = 5;
        public const float Size = 0.25f;

        public static void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //var builder = new DefaultSceneBuilder();
            //var builder = new BookSceneBuilder();
            var builder = new ObjTestSceneBuilder();

            var world = builder.Build(Size);

 
            stopwatch.Start();

             //var canvas = world.Camera.Render(world.Scene, () => new RandomSuperSampler(world.Scene, world.Camera, new DefaultIntegrator(), 4));

             var canvas = world.Camera.Render(world.Scene, new DefaultIntegrator());

            stopwatch.Stop();
            Console.WriteLine($"Render Time elapsed: {stopwatch.Elapsed}");

            canvas.ToPPM(@".\EggScene.ppm");
        }

    }

}
