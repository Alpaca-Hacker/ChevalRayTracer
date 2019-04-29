using Cheval.Models;

namespace Cheval.Templates
{
    public class MaterialTemplate
    {
        public static Material Default => new Material()
        {
            Colour = ColourTemplate.White,
            Ambient = 0.1f,
            Diffuse = 0.9f,
            Specular = 0.9f,
            Shininess = 200,
            Reflective = 0,
            Transparency = 0.0f,
            RefractiveIndex = 1.0f
        };
        public static Material ColouredGlass => new Material()
        {
            Colour = new ChevalColour(0, 0, 0.2f),
            Ambient = 0,
            Diffuse = 0.4f,
            Specular = 0.9f,
            Shininess = 300,
            Reflective = 0.9f,
            Transparency = 0.9f,
            RefractiveIndex = 1.5f
        };
        public static Material Glass => new Material()
        {
            Colour = ColourTemplate.White,
            Ambient = 0,
            Diffuse = 0.4f,
            Specular = 0.9f,
            Shininess = 300,
            Reflective = 0.9f,
            Transparency = 1.0f,
            RefractiveIndex = 1.5f
        };
    }
}
