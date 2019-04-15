using Cheval.Models;

namespace Cheval.Templates
{
    public class MaterialTemplate
    {
        public static Material Default => new Material()
        {
            Colour = ColourTemplate.White,
            Ambient = 0.1,
            Diffuse = 0.9,
            Specular = 0.9,
            Shininess = 200,
            Reflective = 0,
            Transparency = 0.0,
            RefractiveIndex = 1.0
        };
        public static Material ColouredGlass => new Material()
        {
            Colour = new ChevalColour(0, 0, 0.2),
            Ambient = 0,
            Diffuse = 0.4,
            Specular = 0.9,
            Shininess = 300,
            Reflective = 0.9,
            Transparency = 0.9,
            RefractiveIndex = 1.5
        };
        public static Material Glass => new Material()
        {
            Colour = ColourTemplate.White,
            Ambient = 0,
            Diffuse = 0.4,
            Specular = 0.9,
            Shininess = 300,
            Reflective = 0.9,
            Transparency = 1.0,
            RefractiveIndex = 1.5
        };
    }
}
