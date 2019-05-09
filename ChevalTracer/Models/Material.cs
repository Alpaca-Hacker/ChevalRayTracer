using Cheval.Patterns;

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

    }
}
