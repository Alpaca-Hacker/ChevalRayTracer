using System.Collections.Generic;
using Cheval.Helper;
using Cheval.Models.Shapes;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Models.Light;

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
                    Colour = new ChevalColour(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f
                }
            };
            var sphere2 = new Sphere
            {
                Transform = Transform.Scaling(0.5f, 0.5f, 0.5f)
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

    }
}
