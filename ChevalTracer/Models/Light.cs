using System;
using Cheval.DataStructure;

namespace Cheval.Models
{
    public class Light
    {
        public ChevalColour Intensity { get; set; }
        public ChevalTuple Position { get; set; }
        public string Type { get; private set; }

        public Light()
        {
            
        }

        public Light(ChevalTuple position, ChevalColour intensity, string type = "unknown")
        {
            Position = position;
            Intensity = intensity;
            Type = type;
        }
        public static Light PointLight(ChevalTuple position, ChevalColour intensity)
        {
            var newLight = new Light
            {
                Position = position,
                Intensity = intensity,
                Type = "point"
            };
            return newLight;

        }
    }
}
