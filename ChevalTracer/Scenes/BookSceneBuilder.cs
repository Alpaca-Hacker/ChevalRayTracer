using System;
using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Templates;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;

namespace Cheval.Scenes
{
    public class BookSceneBuilder : ISceneBuilder
    {
        public World Build(float size = 1)
        {
            var world = new World();

            var scene = new Scene();


            var cameraOrigin = Point(9.5, 2, 2.5);
            var cameraDirection = Point(3, 0.5, 0.65);
            var up = Vector(0, 1, 0);
            var fov = 0.436332; //25 * Math.PI / 180; //Radians?
            var viewTransform = ViewTransform(cameraOrigin, cameraDirection, up);

            world.Camera = new Camera((int)(400 * size), (int)(200 * size), fov)
            {
                Transform = viewTransform
            };

            var floor = new Plane
            {
                Transform = Transform.RotationY(0.31415), 
                Material = new Material
                {
                    Specular = 0,
                    //Pattern = new Checker(new List<ChevalColour>{
                    //    new ChevalColour(0.35, 0.35, 0.35),
                    //    new ChevalColour(0.65, 0.65, 0.65),
                    //    new ChevalColour(0.95, 0.95, 0.95)

                    //}),
                    Colour = new ChevalColour(0.5,0.5,0.5),
                    Reflective = 0,
                    Ambient = 0,
                    Diffuse = 0
                }
            };

            var sky = new Sphere
            {
                Transform = Scaling(100,100,100),
                Material = new Material
                {
                    Specular = 0,
                    //Pattern = new Checker(new List<ChevalColour>{
                    //    new ChevalColour(0.35, 0.35, 0.35),
                    //    new ChevalColour(0.65, 0.65, 0.65),
                    //    new ChevalColour(0.95, 0.95, 0.95)

                    //}),
                    Colour = new ChevalColour(1, 1, 1),
                    Reflective = 0,
                    Ambient = 0
                }
            };
            scene.Shapes.Add(sky);

            scene.Shapes.Add( new Sphere
            {
                Transform = Translation(0,1,0),
                Material = MaterialTemplate.Glass
                
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-4, 1, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.4f, 0.2f, 0.1f),
                    Diffuse = 0,
                    Shininess = 0,
                    Specular =  0,
                    Ambient = .5
                }

            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(4, 1, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.7f, 0.6f, 0.5f),
                    Reflective = 0.8
                }

            });

            var lightPos = ChevalTuple.Point(0, 50, 0);
            var lightColour = new ChevalColour(1, 1, 1);

            var light1 = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
            var light2 = new Light
            {
                Position = Point(10, 10, -10),
                Intensity = new ChevalColour(1, 1, 1)
            };
            scene.Lights.Add(light1);
           // scene.Lights.Add(light2);

            scene.Shapes.Add(floor);

            world.Scene = scene;

            return world;

        }
    }
}
