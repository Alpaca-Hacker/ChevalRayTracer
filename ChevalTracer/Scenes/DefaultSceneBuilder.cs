using System;
using System.Collections.Generic;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Templates;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Scenes
{
    public class DefaultSceneBuilder :ISceneBuilder
    {
        public World Build(float size = 1)
        {
            var world = new World();
            
            var scene = new Scene();

            var cameraOrigin = Point(-2.6f, 1.5f, -3.9f);
            var cameraDirection = Point(-0.6f, 1, -0.8f);
            var up = Vector(0, 1, 0);
            var fov = 1.152f;
            var viewTransform = Transform.ViewTransform(cameraOrigin, cameraDirection, up);

            world.Camera = new Camera((int)(400 * size), (int)(200 * size), fov)
            {
                Transform = viewTransform
            };


            var wallMaterial = new Material
            {
                Ambient = 0,
                Diffuse = 0.4f,
                Specular = 0,
                Reflective = 0.3f,
                Pattern = new Stripe(new ChevalColour(0.45f,0.45f,0.45f), new ChevalColour(0.55f, 0.55f, 0.55f))
                {
                    Transform = Transform.Scaling(0.25f,0.25f,0.25f) * Transform.RotationY(1.5708f)
                }
            };

            var floor = new Plane
            {
                Transform = Transform.RotationY(0.31415f),
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new Checker(new List<ChevalColour>{
                        new ChevalColour(0.35f, 0.35f, 0.35f), 
                        new ChevalColour(0.65f, 0.65f, 0.65f),
                        new ChevalColour(0.95f, 0.95f, 0.95f)

                    }),
                    Reflective = 0.4f
                }
            };

            var ceiling = new Plane
            {
                Transform = Transform.Translation(0, 5, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.8f,0.8f,0.8f),
                    Specular = 0,
                    Ambient = 0.3f
                }
            };

            var westWall = new Plane
            {
                Transform = Transform.Translation(-5, 0, 0) *
                            Transform.RotationZ(1.5708f) *
                            Transform.RotationY(1.5708f),

                Material = wallMaterial
            };

            var eastWall = new Plane
            {
                Transform = Transform.Translation(5, 0, 0) *
                            Transform.RotationZ(1.5708f) *
                            Transform.RotationY(1.5708f),

                Material = wallMaterial
            };

            var northWall = new Plane
            {
                Transform = Transform.Translation(0, 0, 5) *
                            Transform.RotationX(1.5708f),

                Material = wallMaterial
            };

            var southWall = new Plane
            {
                Transform = Transform.Translation(0, 0, -5) *
                            Transform.RotationX(1.5708f),

                Material = wallMaterial
            };

            //Background
            var background1 = new List<Shape>();

            background1.Add(new Cube
            {
                Transform = Transform.Translation(4.6f, 0.4f, 1) *
                            Transform.Scaling(0.4f,0.4f,0.4f),

                Material = 
                    new Material
                {
                    Colour = new ChevalColour(0.8f, 0.5f, 0.3f),
                    Shininess = 50
                }
            });

            background1.Add(new Sphere
            {
                Transform = Transform.Translation(4.7f, 0.3f, 0.4f) *
                            Transform.Scaling(0.3f, 0.3f, 0.3f),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.9f, 0.4f, 0.5f),
                        Shininess = 50
                    }
            });

            var background2= new List<Shape>();
            background2.Add(new Cube
            {
                Transform = Transform.Translation(-1, 0.5f, 4.5f) *
                            Transform.Scaling(0.5f, 0.5f, 0.5f),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4f, 0.9f, 0.6f),
                        Shininess = 50
                    }
            });

            background2.Add(new Cone
            {
                Transform = Transform.Translation(-1.7f, 0.5f, 2.7f) *
                            Transform.RotationZ(MathF.PI),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4f, 0.6f, 0.9f),
                        Shininess = 50
                    },
                Maximum = 0.5f,
                Minimum = 0
                
            });

            // Foreground

            var redSphere = new Sphere
            {
                Transform = Transform.Translation(-0.6f, 1, 0.6f),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(1, 0.3f, 0.2f),
                        Shininess = 5,
                        Specular = 0.4f
                    }
            };

            var blueSphere = new Cylinder
            {
                Transform = Transform.Translation(0.6f, 0.0f, -0.6f) *
                            Transform.Scaling(0.7f, 0.7f, 0.7f),

                Material = MaterialTemplate.ColouredGlass,
                Maximum = 2,
                Minimum = 0
               
            };
            blueSphere.Material.Colour = new ChevalColour(0,0,0.2f);


            var greenSphere = new Sphere
            {
                Transform = Transform.Translation(-0.7f, 0.5f, -0.8f) *
                            Transform.Scaling(0.5f, 0.5f, 0.5f),

                Material = MaterialTemplate.ColouredGlass

            };
            greenSphere.Material.Colour = new ChevalColour(0, 0.2f, 0);


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

            var foreground = new List<Shape>
            {
                redSphere,
                greenSphere,
                blueSphere
            };

            scene.Shapes.AddRange(room);
            scene.Shapes.AddRange(foreground);
            scene.Shapes.AddRange(background1);
            scene.Shapes.AddRange(background2);

            world.Scene = scene;

            return world;
        }
    }
}
