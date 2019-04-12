using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models.Shapes;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Models.Light;

namespace Cheval.Models
{
    public class Scene
    {
        public Light Light { get; set; }
        public List<Sphere> Shapes { get; set; }

        public Scene()
        {
            Shapes = new List<Sphere>();
        }

        public static Scene Default()
        {
            var light = PointLight(Point(-10, 10, -10), new ChevalColour(1, 1, 1));
            var sphere1 = new Sphere
            {
                Material =
                {
                    Colour = new ChevalColour(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            };
            var sphere2 = new Sphere
            {
                Transform = Transform.Scaling(0.5, 0.5, 0.5)
            };
            var shapeList = new List<Sphere> {sphere1, sphere2};
            var defaultScene = new Scene
            {
                Shapes = shapeList,
                Light = light
            };
            return defaultScene;
        }

        public ChevalColour ShadeHit(Computations comps)
        {
            //TODO Add more lights!
            var lighting = comps.Object.Material.Lighting(Light, comps.Point, comps.EyeV, comps.NormalV);
            return lighting;
        }

        public ChevalColour ColourAt(Ray ray)
        {
            var colour = new ChevalColour(0,0,0);
            var inter = new Intersections(ray.Intersect(this));
            var hit = inter.Hit();
            if (hit != null)
            {
                var comps = new Computations(hit,ray);
                colour = ShadeHit(comps);
            }

            return colour;

        }
    }
}
