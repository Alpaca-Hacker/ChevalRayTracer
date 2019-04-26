using System;
using Cheval.DataStructure;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models
{
    public class Material
    {
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; } 
        public double Shininess { get; set; }
        public ChevalColour Colour { get; set; }
        public Pattern Pattern { get; set; }
        public double Reflective { get; set; } 
        public double RefractiveIndex { get; set; }
        public double Transparency { get; set; }


        public Material()
        {
            Colour = new ChevalColour(1,1,1);
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200;
            Reflective = 0;
            Transparency = 0.0;
            RefractiveIndex = 1.0;
        }



        public Material(ChevalColour colour, double ambient, double diffuse, double specular, double shininess)
        {
            Colour = colour;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        public ChevalColour Lighting(Shape shape, Light light, ChevalTuple point, ChevalTuple eyeV, ChevalTuple normalV, bool inShadow = false)
        {
            var localColour = Colour;
            if (Pattern != null)
            {
                localColour = Pattern.ColourAtObject(shape, point);
            }
            var effectiveColour = localColour * light.Intensity;
            var lightV = Normalize(light.Position - point);
            var ambient = effectiveColour * Ambient;

            var lightDotNormal = Dot(lightV, normalV);
            var diffuse = new ChevalColour(0.0,0.0,0.0);
            var specular = new ChevalColour(0.0, 0.0, 0.0);

            if (lightDotNormal >= 0)
            {
                diffuse = effectiveColour * Diffuse * lightDotNormal;

                var reflectV = Reflect(-lightV, normalV);
                var reflectDotEye = Dot(reflectV, eyeV);
                if (reflectDotEye > 0)
                {
                    var factor = Math.Pow(reflectDotEye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }

            var returnValue = ambient;
            if (!inShadow)
            {
                returnValue += diffuse + specular;
            }


            return returnValue;
        }
    }
}
