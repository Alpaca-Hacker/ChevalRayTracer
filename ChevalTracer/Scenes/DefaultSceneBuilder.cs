using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Templates;

namespace Cheval.Scenes
{
    public class DefaultSceneBuilder :ISceneBuilder
    {
        public World Build(float size = 1)
        {
            var world = new World();
            
            var scene = new Scene();


            var cameraOrigin = ChevalTuple.Point(-2.6, 1.5, -3.9);
            var cameraDirection = ChevalTuple.Point(-0.6, 1, -0.8);
            var up = ChevalTuple.Vector(0, 1, 0);
            var fov = 1.152;
            var viewTransform = Transform.ViewTransform(cameraOrigin, cameraDirection, up);

            world.Camera = new Camera((int)(400 * size), (int)(200 * size), fov)
            {
                Transform = viewTransform
            };


            var wallMaterial = new Material
            {
                Ambient = 0,
                Diffuse = 0.4,
                Specular = 0,
                Reflective = 0.3,
                Pattern = new Stripe(new ChevalColour(0.45,0.45,0.45), new ChevalColour(0.55, 0.55, 0.55))
                {
                    Transform = Transform.Scaling(0.25,0.25,0.25) * Transform.RotationY(1.5708)
                }
            };

            var floor = new Plane
            {
                Transform = Transform.RotationY(0.31415),
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new Checker(new List<ChevalColour>{
                        new ChevalColour(0.35, 0.35, 0.35), 
                        new ChevalColour(0.65, 0.65, 0.65),
                        new ChevalColour(0.95, 0.95, 0.95)

                    }),
                    Reflective = 0.4
                }
            };

            var ceiling = new Plane
            {
                Transform = Transform.Translation(0, 5, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.8,0.8,0.8),
                    Specular = 0,
                    Ambient = 0.3
                }
            };

            var westWall = new Plane
            {
                Transform = Transform.Translation(-5, 0, 0) *
                            Transform.RotationZ(1.5708) *
                            Transform.RotationY(1.5708),

                Material = wallMaterial
            };

            var eastWall = new Plane
            {
                Transform = Transform.Translation(5, 0, 0) *
                            Transform.RotationZ(1.5708) *
                            Transform.RotationY(1.5708),

                Material = wallMaterial
            };

            var northWall = new Plane
            {
                Transform = Transform.Translation(0, 0, 5) *
                            Transform.RotationX(1.5708),

                Material = wallMaterial
            };

            var southWall = new Plane
            {
                Transform = Transform.Translation(0, 0, -5) *
                            Transform.RotationX(1.5708),

                Material = wallMaterial
            };

            //Background

            scene.Shapes.Add(new Cube
            {
                Transform = Transform.Translation(4.6, 0.4, 1) *
                            Transform.Scaling(0.4,0.4,0.4),

                Material = 
                    new Material
                {
                    Colour = new ChevalColour(0.8, 0.5, 0.3),
                    Shininess = 50
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Transform.Translation(4.7, 0.3, 0.4) *
                            Transform.Scaling(0.3, 0.3, 0.3),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.9, 0.4, 0.5),
                        Shininess = 50
                    }
            });

            scene.Shapes.Add(new Cube
            {
                Transform = Transform.Translation(-1, 0.5, 4.5) *
                            Transform.Scaling(0.5, 0.5, 0.5),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4, 0.9, 0.6),
                        Shininess = 50
                    }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Transform.Translation(-1.7, 0.3, 4.7) *
                            Transform.Scaling(0.3, 0.3, 0.3),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4, 0.6, 0.9),
                        Shininess = 50
                    }
            });

            // Foreground

            var redSphere = new Sphere
            {
                Transform = Transform.Translation(-0.6, 1, 0.6),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(1, 0.3, 0.2),
                        Shininess = 5,
                        Specular = 0.4
                    }
            };

            var blueSphere = new Cylinder
            {
                Transform = Transform.Translation(0.6, 0.0, -0.6) *
                            Transform.Scaling(0.7, 0.7, 0.7),

                Material = MaterialTemplate.ColouredGlass,
                Maximum = 2,
                Minimum = 0
               
            };
            blueSphere.Material.Colour = new ChevalColour(0,0,0.2);


            var greenSphere = new Sphere
            {
                Transform = Transform.Translation(-0.7, 0.5, -0.8) *
                            Transform.Scaling(0.5, 0.5, 0.5),

                Material = MaterialTemplate.ColouredGlass

            };
            greenSphere.Material.Colour = new ChevalColour(0, 0.2, 0);


            var lightPos = ChevalTuple.Point(-4.9, 4.9, -1);
            var lightColour = new ChevalColour(1, 1, 1);

            var light1 = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
        //    var light2 = new Light
        //    {
        //        Position = Point(10, 10, -10),
        //        Intensity = new ChevalColour(0.5, 0.5, 0.5)
        //};
            scene.Lights.Add(light1);
           // scene.Lights.Add(light2);

            scene.Shapes.Add(floor);
            scene.Shapes.Add(ceiling);
            scene.Shapes.Add(eastWall);
            scene.Shapes.Add(westWall);
            scene.Shapes.Add(northWall);
            scene.Shapes.Add(southWall);

            scene.Shapes.Add(redSphere);
            scene.Shapes.Add(greenSphere);
           scene.Shapes.Add(blueSphere);

            world.Scene = scene;

            return world;
        }
    }
}
