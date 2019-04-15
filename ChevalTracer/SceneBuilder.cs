﻿using System.Collections.Generic;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using static System.Math;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;

namespace Cheval
{
    public class SceneBuilder
    {
        public static Scene Build()
        {
            var scene = new Scene();

            var floor = new Plane
            {
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.9, 0.9),
                    Specular = 0,
                    Pattern = new Gradient(new ChevalColour(0.1, 0.1, 0.5), new ChevalColour(0.5, 1, 0.1))

                }
            };

            scene.Shapes.Add(floor);
            //scene.Shapes.Add(new Sphere
            //{
            //    Transform = Translation(0, 0, 5) *
            //                RotationY(-Math.PI / 4) *
            //                RotationX(Math.PI / 2) *
            //                Scaling(10, 0.01, 10),
            //    Material = floor.Material
            //});

            //scene.Shapes.Add(new Sphere
            //{
            //    Transform = Translation(0, 0, 5) *
            //                RotationY(Math.PI / 4) *
            //                RotationX(Math.PI / 2) *
            //                Scaling(10, 0.01, 10),
            //    Material = floor.Material
            //});

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Colour = new ChevalColour(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.3,
                    Pattern = new Stripe(new List<ChevalColour>
                    {
                        new ChevalColour(1,0,0),
                        new ChevalColour(0,1,0),
                        new ChevalColour(0,0,1)
                    })
                    {
                        Transform = RotationZ(PI/4)*Scaling(.5, .5, .5)
                    }
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(1.5, 0.5, -0.5) * Scaling(0.5, 0.5, 0.5),
                Material = new Material
                {
                    Colour = new ChevalColour(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3,
                    Pattern = new Ring(new List<ChevalColour>
                    {
                    new ChevalColour(1,0,0),
                    new ChevalColour(0,1,0),
                    new ChevalColour(0,0,1)
                    })
                    {
                        Transform = RotationX(PI/1.5)*Scaling(.25,.25,.25)
                    }
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-1.5, 0.33, -0.75) * Scaling(0.33, 0.33, 0.33),
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3,
                    Pattern = new Gradient(new ChevalColour(0.1, 0.1, 0.5), new ChevalColour(0.5, 1, 0.1))
                }
            });

            var lightPos = Point(-10, 10, -10);
            var lightColour = new ChevalColour(1, 1, 1);

            var light1 = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
            var light2 = new Light
            {
                Position = Point(10, 10, -10),
                Intensity = new ChevalColour(0.5, 0.5, 0.5)
        };
            scene.Lights.Add(light1);
            scene.Lights.Add(light2);

            return scene;
        }
    }
}