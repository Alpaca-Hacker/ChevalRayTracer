using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models.Shapes;
using static System.Math;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Models.Light;
using static Cheval.Templates.ColourTemplate;

namespace Cheval.Models
{
    public class Scene
    {
        public List<Light> Lights { get; set; }
        public List<Shape> Shapes { get; set; }

        public Scene()
        {
            Shapes = new List<Shape>();
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
            var shapeList = new List<Shape> {sphere1, sphere2};
            var lights = new List<Light> {light};
            var defaultScene = new Scene
            {
                Shapes = shapeList,
                Lights = lights
            };
            return defaultScene;
        }

        public ChevalColour ShadeHit(Computations comps, int remaining)
        {

            var lighting = new ChevalColour(0,0,0);
            foreach (var light in Lights)
            {
                var inShadow = IsShadowed(comps.OverPoint, light);
                lighting += comps.Shape.Material.Lighting(comps.Shape, light, comps.OverPoint, comps.EyeV, comps.NormalV, inShadow);
             
            }
            //Todo For each light?
            var reflected = ReflectedColour(comps, remaining);
            var refracted = RefractedColour(comps, remaining);
            return lighting + reflected + refracted;
        }

        public ChevalColour ColourAt(Ray ray, int remaining)
        {
            var colour = new ChevalColour(0,0,0);
            var inters = new Intersections(ray.Intersect(this));
            var hit = inters.Hit();
            if (hit != null)
            {
                var comps = new Computations(hit,ray, inters);
                colour = ShadeHit(comps, remaining);
            }

            return colour;

        }

        public bool IsShadowed(ChevalTuple point, Light light)
        {
            var v = light.Position - point;
            var distance = Magnitude(v);
            var direction = Normalize(v);
            var ray = new Ray(point, direction);
            var inters = new Intersections(ray.Intersect(this));
            var hit = inters.Hit();
            return hit != null && hit.T < distance;
        }

        public ChevalColour ReflectedColour(Computations comps, int remaining)
        {

            if (remaining < 1 || Abs(comps.Shape.Material.Reflective) < Cheval.Epsilon)
            {
                return Black;
            }

            remaining--;

            var reflectRay = new Ray(comps.OverPoint, comps.ReflectV);
            var colour = ColourAt(reflectRay, remaining);
            return colour * comps.Shape.Material.Reflective;
        }

        public ChevalColour RefractedColour(Computations comps, int remaining)
        {
            if (remaining < 1 || Abs(comps.Shape.Material.Transparency) < Cheval.Epsilon)
            {
                return Black;

            }

            var nRatio = comps.N1 / comps.N2;
            var cosI = Dot(comps.EyeV, comps.NormalV);
            var sin2T = Pow(nRatio, 2) * (1 - Pow(cosI, 2));

            if (sin2T > 1)
            {
                return Black;
            }

            var cosT = Sqrt(1.0 - sin2T);
            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;
            var refractRay = new Ray(comps.UnderPoint, direction);
            remaining--;
            var resultColour = ColourAt(refractRay, remaining) * comps.Shape.Material.Transparency;

            return resultColour;
        }

    }
}
