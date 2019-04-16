using System;
using System.Collections.Generic;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Templates;
using static System.Math;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;
using static Cheval.Models.ChevalColour;
using static Cheval.Templates.ColourTemplate;

namespace Cheval
{
    public class SceneBuilder
    {
        public static Scene Build()
        {
            var scene = new Scene();

            var wallMaterial = new Material
            {
                Ambient = 0,
                Diffuse = 0.4,
                Specular = 0,
                Reflective = 0.3,
                Pattern = new Stripe(new ChevalColour(0.45,0.45,0.45), new ChevalColour(0.55, 0.55, 0.55))
                {
                    Transform = Scaling(0.25,0.25,0.25) * RotationY(1.5708)
                }
            };

            var floor = new Plane
            {
                Transform = RotationY(0.31415),
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new Checker(new ChevalColour(0.35, 0.35, 0.35), 
                        new ChevalColour(0.65, 0.65, 0.65)),
                    Reflective = 0.4
                }
            };

            var ceiling = new Plane
            {
                Transform = Translation(0, 5, 0),
                Material = new Material
                {
                    Colour = new ChevalColour(0.8,0.8,0.8),
                    Specular = 0,
                    Ambient = 0.3
                }
            };

            var westWall = new Plane
            {
                Transform = Translation(-5, 0, 0) *
                            RotationZ(1.5708) *
                            RotationY(1.5708),

                Material = wallMaterial
            };

            var eastWall = new Plane
            {
                Transform = Translation(5, 0, 0) *
                            RotationZ(1.5708) *
                            RotationY(1.5708),

                Material = wallMaterial
            };

            var northWall = new Plane
            {
                Transform = Translation(0, 0, 5) *
                            RotationX(1.5708),

                Material = wallMaterial
            };

            var southWall = new Plane
            {
                Transform = Translation(0, 0, -5) *
                            RotationX(1.5708),

                Material = wallMaterial
            };

            //Background

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(4.6, 0.4, 1) *
                            Scaling(0.4,0.4,0.4),

                Material = 
                    new Material
                {
                    Colour = new ChevalColour(0.8, 0.5, 0.3),
                    Shininess = 50
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(4.7, 0.3, 0.4) *
                            Scaling(0.3, 0.3, 0.3),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.9, 0.4, 0.5),
                        Shininess = 50
                    }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-1, 0.5, 4.5) *
                            Scaling(0.5, 0.5, 0.5),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4, 0.9, 0.6),
                        Shininess = 50
                    }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-1.7, 0.3, 4.7) *
                            Scaling(0.3, 0.3, 0.3),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(0.4, 0.6, 0.9),
                        Shininess = 50
                    }
            });

            // Foregound

            var redSphere = new Sphere
            {
                Transform = Translation(-0.6, 1, 0.6),

                Material =
                    new Material
                    {
                        Colour = new ChevalColour(1, 0.3, 0.2),
                        Shininess = 5,
                        Specular = 0.4
                    }
            };

            var blueSphere = new Sphere
            {
                Transform = Translation(0.6, 0.7, -0.6) *
                            Scaling(0.7, 0.7, 0.7),

                Material = MaterialTemplate.ColouredGlass

            };
            blueSphere.Material.Colour = new ChevalColour(0,0,0.2);

            var greenSphere = new Sphere
            {
                Transform = Translation(-0.7, 0.5, -0.8) *
                            Scaling(0.5, 0.5, 0.5),

                Material = MaterialTemplate.ColouredGlass

            };
            greenSphere.Material.Colour = new ChevalColour(0, 0.2, 0);

            var lightPos = Point(-4.9, 4.9, -1);
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

            return scene;
        }
    }
}
