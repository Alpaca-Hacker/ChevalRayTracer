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
        public List<Light> Lights { get; set; }
        public List<Sphere> Shapes { get; set; }

        public Scene()
        {
            Shapes = new List<Sphere>();
            Lights = new List<Light>();
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
            var lights = new List<Light> {light};
            var defaultScene = new Scene
            {
                Shapes = shapeList,
                Lights = lights
            };
            return defaultScene;
        }

        public ChevalColour ShadeHit(Computations comps)
        {
            var lighting = new ChevalColour(0,0,0);
            foreach (var light in Lights)
            {
                var inShadow = IsShadowed(comps.OverPoint);
                lighting += comps.Object.Material.Lighting(light, comps.Point, comps.EyeV, comps.NormalV, inShadow);
             
            }
            return lighting;
        }

        public ChevalColour ColourAt(Ray ray)
        {
            var colour = new ChevalColour(0,0,0);
            var inters = new Intersections(ray.Intersect(this));
            var hit = inters.Hit();
            if (hit != null)
            {
                var comps = new Computations(hit,ray);
                colour = ShadeHit(comps);
            }

            return colour;

        }

        public bool IsShadowed(ChevalTuple point)
        {
            //TODO Add lights
            var v = Lights[0].Position - point;
            var distance = Magnitude(v);
            var direction = Normalize(v);
            var ray = new Ray(point, direction);
            var inters = new Intersections(ray.Intersect(this));
            var hit = inters.Hit();
            return hit != null && hit.T < distance;
        }
    }
}
