using System;
using Cheval.DataStructure;

namespace Cheval.Models
{
    public class Camera
    {
        private Matrix _transform;
        private Matrix _inverseTransform;
        public int HSize { get; set; }
        public int VSize { get; set; }
        public double FOV { get; set; }

        public Matrix Transform
        {
            get => _transform;
            set
            {
                _transform = value;
                _inverseTransform = Matrix.Inverse(value);
            }
        }

        public double PixelSize => (HalfWidth * 2) / HSize;

        private double HalfView => Math.Tan(FOV / 2);
        private double Aspect => (double)HSize / (double)VSize;

        private double HalfWidth => Aspect >= 1 ? HalfView : HalfView * Aspect;
        private double HalfHeight => Aspect >= 1 ? HalfView / Aspect : HalfView;

        public Camera(int hSize, int vSize, double fov)
        {
            HSize = hSize;
            VSize = vSize;
            FOV = fov;
            Transform = Helper.Transform.IdentityMatrix;
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (double)(px + 0.5) * PixelSize;
            var yOffset = (double)(py + 0.5) * PixelSize;

            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;

            var pixel = _inverseTransform * ChevalTuple.Point(worldX, worldY, -1);
            var origin = _inverseTransform * ChevalTuple.Point(0, 0, 0);
            var direction = ChevalTuple.Normalize(pixel - origin);

            return new Ray(origin, direction);
        }

        public Canvas Render(Scene scene)
        {
            var image = new Canvas(HSize, VSize);
            for (var y = 0; y < VSize ; y++)
            {
                for (var x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x,y);
                    image.WritePixel(x, y, scene.ColourAt(ray, Cheval.MaxNoOfReflections));
                }
            }

            return image;
        }
    }
}
