using System;
using Cheval.Models;
using Cheval.Models.Shapes;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;

namespace Cheval
{
    public class SceneBuilder
    {
        public static Scene Build()
        {
            var scene = new Scene();

            var floor = new Sphere
            {
                Transform = Scaling(10, 0.01, 10),
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.9, 0.9),
                    Specular = 0
                }
            };
            scene.Shapes.Add(floor);
            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(0, 0, 5) *
                            RotationY(-Math.PI /4) *
                            RotationX(Math.PI /2) *
                            Scaling(10,0.01,10),
                Material = floor.Material
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(0, 0, 5) *
                            RotationY(Math.PI / 4) *
                            RotationX(Math.PI / 2) *
                            Scaling(10, 0.01, 10),
                Material = floor.Material
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Colour = new ChevalColour(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(1.5, 0.5, -0.5) * Scaling(0.5, 0.5, 0.5),
                Material = new Material
                {
                    Colour = new ChevalColour(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            });

            scene.Shapes.Add(new Sphere
            {
                Transform = Translation(-1.5, 0.33, -0.75) * Scaling(0.33, 0.33, 0.33),
                Material = new Material
                {
                    Colour = new ChevalColour(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            });

            var lightPos = Point(-10, 10, -10);
            var lightColour = new ChevalColour(1, 1, 1);

            var light = new Light
            {
                Position = lightPos,
                Intensity = lightColour
            };
            scene.Lights.Add(light);

            return scene;
        }
    }
}
