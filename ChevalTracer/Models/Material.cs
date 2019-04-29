using System;
using Cheval.DataStructure;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models
{
    public class Material
    {
        public float Ambient { get; set; }
        public float Diffuse { get; set; }
        public float Specular { get; set; } 
        public float Shininess { get; set; }
        public ChevalColour Colour { get; set; }
        public Pattern Pattern { get; set; }
        public float Reflective { get; set; } 
        public float RefractiveIndex { get; set; }
        public float Transparency { get; set; }


        public Material()
        {
            Colour = new ChevalColour(1,1,1);
            Ambient = 0.1f;
            Diffuse = 0.9f;
            Specular = 0.9f;
            Shininess = 200;
            Reflective = 0;
            Transparency = 0.0f;
            RefractiveIndex = 1.0f;
        }



        public Material(ChevalColour colour, float ambient, float diffuse, float specular, float shininess)
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
            var diffuse = new ChevalColour(0.0f,0.0f,0.0f);
            var specular = new ChevalColour(0.0f, 0.0f, 0.0f);

            if (lightDotNormal >= 0)
            {
                diffuse = effectiveColour * Diffuse * lightDotNormal;

                var reflectV = Reflect(-lightV, normalV);
                var reflectDotEye = Dot(reflectV, eyeV);
                if (reflectDotEye > 0)
                {
                    var factor = MathF.Pow(reflectDotEye, Shininess);
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
