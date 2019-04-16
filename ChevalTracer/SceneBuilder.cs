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

            var floor = new Plane
            {
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.9, 0.9),
                    Specular = 0,
                    Pattern = new Checker(Red, White),
                    Reflective = 0.3
                }
            };

            scene.Shapes.Add(floor);

            scene.Shapes.Add(new Plane
            {
                Transform = Translation(0, 0, 5) *
                            RotationY(-PI / 4) *
                            RotationX(PI / 2),
                Material = new Material
                {
                    Colour = Black,
                    Specular = 0,
                    // Pattern = new Checker(ColourRed, White),
                    Reflective = 1
                }
            });

            scene.Shapes.Add(new Plane()
            {
                Transform = Translation(0, 0, 5) *
                            RotationY(Math.PI / 4) *
                            RotationX(Math.PI / 2),
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.9, 0.9),
                    Specular = 0,
                    Pattern = new Checker(Red, White)
                    {
                        Transform = Translation(0, 0, 5) *
                                RotationY(Math.PI / 4) *
                                RotationX(Math.PI / 2) *
                                Scaling(.25, .25, .25)
                    }
                    //Reflective = 1
                }
            });
            //scene.Shapes.Add(new Plane()
            //{
            //    Transform = Translation(-12, 0, -12) *
            //                RotationY(Math.PI / 4) *
            //                RotationX(Math.PI / 2),
            //    Material = new Material
            //    {
            //        Colour = new ChevalColour(1, 0.9, 0.9),
            //        Specular = 0,
            //        Pattern = new Checker(Green, White)
            //        {
            //            Transform = Translation(0, 0, 15) *
            //                        RotationY(Math.PI / 4) *
            //                        RotationX(Math.PI / 2) *
            //                        Scaling(.25, .25, .25)
            //        }
            //        //Reflective = 1
            //    }
            //});

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-0.5, 1, 0.5),
                Material = 
                    new Material
                {
                    Colour = new ChevalColour(0, 0.0, 0.0),
                    Ambient = 0.1,
                    Specular = 0.0,
                    Shininess = 200,
                    Diffuse = 0.7,
                    Reflective = 0.8
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
                        Transform = RotationX(PI / 1.5) * Scaling(.25, .25, .25)
                    }
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-1.5, 0.33, -0.75) * Scaling(0.33, 0.33, 0.33),
                Material = MaterialTemplate.Glass
                //    new Material
                //{
                //    Colour = new ChevalColour(0.1, 1, 0.5),
                //    Diffuse = 0.7,
                //    Specular = 0.3,

                //    Pattern = new Stripe(new List<ChevalColour>
                //{
                //    new ChevalColour(1,0,0),
                //    new ChevalColour(0,1,0),
                //    new ChevalColour(0,0,1)
                //})
                //    {
                //        Transform = RotationZ(PI / 4) * Scaling(.5, .5, .5)
                //    }
                //}
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
