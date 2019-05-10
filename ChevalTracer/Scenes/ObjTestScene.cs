using System;
using System.Collections.Generic;
using System.IO;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Services;
using Cheval.Templates;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;

namespace Cheval.Scenes
{
    public class ObjTestSceneBuilder :ISceneBuilder
    {
        public World Build(float size = 1)
        {
            var world = new World();
            
            var scene = new Scene();

            var cameraOrigin = Point(-2.6f, 1.5f, -4.9f);
            var cameraDirection = Point(-0.6f, 1, -0.8f);
            var up = Vector(0, 1, 0);
            var fov = 1.152f;
            var viewTransform = ViewTransform(cameraOrigin, cameraDirection, up);

            world.Camera = new Camera((int)(400 * size), (int)(200 * size), fov)
            {
                Transform = viewTransform
            };


            var wallMaterial = new Material
            {
                Ambient = 0,
                Diffuse = 0.4f,
                Specular = 0,
               // Reflective = 0.3f,
                Pattern = new Stripe(new ChevalColour(0.45f,0.45f,0.45f), new ChevalColour(0.55f, 0.55f, 0.55f))
                {
                    Transform = Scaling(0.25f,0.25f,0.25f) * RotationY(1.5708f)
                }
            };

            var floor = new Plane
            {
                Transform = RotationY(0.31415f),
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new Checker(new List<ChevalColour>{
                        new ChevalColour(0.65f, 0.25f, 0.15f), 
                        new ChevalColour(0.25f, 0.15f, 0.65f),
                        new ChevalColour(0.15f, 0.65f, 0.25f)

                    }),
                    Ambient = 0.3f
                    // Reflective = 0.4f
                }
            };

            var ceiling = new Plane
            {
                Transform = Translation(0, 5, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.8f,0.8f,0.8f),
                    Specular = 0,
                    Ambient = 0.3f
                }
            };

            var westWall = new Plane
            {
                Transform = Translation(-5, 0, 0) *
                            RotationZ(1.5708f) *
                            RotationY(1.5708f),

                Material = wallMaterial
            };

            var eastWall = new Plane
            {
                Transform = Translation(5, 0, 0) *
                            RotationZ(1.5708f) *
                            RotationY(1.5708f),

                Material = wallMaterial
            };

            var northWall = new Plane
            {
                Transform = Translation(0, 0, 5) *
                            RotationX(1.5708f),

                Material = wallMaterial
            };

            var southWall = new Plane
            {
                Transform = Translation(0, 0, -5) *
                            RotationX(1.5708f),

                Material = wallMaterial
            };

            var objFile  = File.ReadAllText(@"dragonegg.obj");
            var parser = new ObjService();
            parser.ParseString(objFile);
            var obj = parser.GetAllGroups;

            obj.Transform = //Translation(0, 1.2f, 0) *
                                 Scaling(.25f, .25f, .25f);

            //foreach (var triangle in (Group)ducky[0])
            //{
            //    triangle.Material = MaterialTemplate.Glass;
            //}

            var lightPos = Point(-4.9f, 4.9f, -1);
            var lightColour = new ChevalColour(1, 1, 1);

            var light1 = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
            var light2 = new Light
            {
                Position = Point(4.9f, 4.9f, -1),
                Intensity = new ChevalColour(0.25f, 0.25f, 0.25f)
        };
            scene.Lights.Add(light1);
            scene.Lights.Add(light2);

            var room = new List<Shape>
            {
                floor,
                ceiling,
                eastWall,
                westWall,
                northWall,
                southWall
            };

            scene.Shapes.AddRange(room);
            scene.Shapes.AddRange(obj);

            world.Scene = scene;

            return world;
        }
    }
}
