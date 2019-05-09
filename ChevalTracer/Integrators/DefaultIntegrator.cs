using System;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Templates;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Integrators
{
    public class DefaultIntegrator : IIntegrator
    {
        public ChevalColour ShadeHit(Computations comps, int remaining, Scene scene)
        {

            var lighting = new ChevalColour(0, 0, 0);
            

            foreach (var light in scene.Lights)
            {
                var inShadow = IsShadowed(comps.OverPoint, light, scene);

                lighting += Lighting(comps.Shape, light, comps.OverPoint, comps.EyeV, comps.NormalV, inShadow);

            }
            //Todo For each light?
            var reflected = ReflectedColour(comps, remaining, scene);
            var refracted = RefractedColour(comps, remaining, scene);

            var material = comps.Shape.Material;
            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = comps.Schlick();
                return lighting + reflected * reflectance + refracted * (1 - reflectance);
            }

            return lighting + reflected + refracted;
        }

        public ChevalColour ColourAt(Ray ray, int remaining, Scene scene)
        {
            var colour = new ChevalColour(0, 0, 0);
            var inters = new Intersections(ray.Intersect(scene));
            var hit = inters.Hit();
            if (hit != null)
            {
                var comps = new Computations(hit, ray, inters);
                colour = ShadeHit(comps, remaining, scene);
            }

            return colour;

        }

        public bool IsShadowed(ChevalTuple point, Light light, Scene scene)
        {
            var v = light.Position - point;
            var distance = Magnitude(v);
            var direction = Normalize(v);
            var ray = new Ray(point, direction);
            var inters = new Intersections(ray.Intersect(scene));
            var hit = inters.Hit();
            return hit != null && hit.T < distance && !hit.Object.NoShadow;
        }

        public ChevalColour ReflectedColour(Computations comps, int remaining, Scene scene)
        {

            if (remaining < 1 || MathF.Abs(comps.Shape.Material.Reflective) < Cheval.Epsilon)
{
                return ColourTemplate.Black;
            }

            remaining--;

            var reflectRay = new Ray(comps.OverPoint, comps.ReflectV);
            var colour = ColourAt(reflectRay, remaining, scene);
            return colour * comps.Shape.Material.Reflective;
        }

        public ChevalColour RefractedColour(Computations comps, int remaining, Scene scene)
        {
            if (remaining < 1 || MathF.Abs(comps.Shape.Material.Transparency) < Cheval.Epsilon)
{
                return ColourTemplate.Black;

            }

            var nRatio = comps.N1 / comps.N2;
            var cosI = Dot(comps.EyeV, comps.NormalV);
            var sin2T = MathF.Pow(nRatio, 2) * (1 - MathF.Pow(cosI, 2));

            if (sin2T > 1)
            {
                return ColourTemplate.Black;
            }

            var cosT = MathF.Sqrt(1.0f - sin2T);
            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;
            var refractRay = new Ray(comps.UnderPoint, direction);
            remaining--;
            var resultColour = ColourAt(refractRay, remaining, scene) * comps.Shape.Material.Transparency;

            return resultColour;
        }


        public ChevalColour Lighting(Shape shape, Light light, ChevalTuple point, ChevalTuple eyeV, ChevalTuple normalV, bool inShadow = false)
        {
            var material = GetMaterial(shape);
            var localColour = material.Colour;
            if (material.Pattern != null)
            {
                localColour = material.Pattern.ColourAtObject(shape, point);
            }
            var effectiveColour = localColour * light.Intensity;
            var lightV = Normalize(light.Position - point);
            var ambient = effectiveColour * material.Ambient;

            var lightDotNormal = Dot(lightV, normalV);
            var diffuse = new ChevalColour(0.0f, 0.0f, 0.0f);
            var specular = new ChevalColour(0.0f, 0.0f, 0.0f);

            if (lightDotNormal >= 0)
            {
                diffuse = effectiveColour * material.Diffuse * lightDotNormal;

                var reflectV = Reflect(-lightV, normalV);
                var reflectDotEye = Dot(reflectV, eyeV);
                if (reflectDotEye > 0)
                {
                    var factor = MathF.Pow(reflectDotEye, material.Shininess);
                    specular = light.Intensity * material.Specular * factor;
                }
            }

            var returnValue = ambient;
            if (!inShadow)
            {
                returnValue += diffuse + specular;
            }


            return returnValue;
        }

        private Material GetMaterial(Shape shape)
        {
            //Find Mat for object (parent-parent)

            return shape.Material;
        }
    }
}
