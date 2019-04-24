using System;
using System.Threading.Tasks;
using Cheval.DataStructure;
using Cheval.Samplers;

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

        public Matrix InverseTransform => _inverseTransform;

        public double PixelSize => (HalfWidth * 2) / HSize;

        private double HalfView => Math.Tan(FOV / 2);
        private double Aspect => (double)HSize / (double)VSize;

        public double HalfWidth => Aspect >= 1 ? HalfView : HalfView * Aspect;
        public double HalfHeight => Aspect >= 1 ? HalfView / Aspect : HalfView;

        public Camera(int hSize, int vSize, double fov)
        {
            HSize = hSize;
            VSize = vSize;
            FOV = fov;
            Transform = Helper.Transform.IdentityMatrix;
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5) * PixelSize;
            var yOffset = (py + 0.5) * PixelSize;

            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;

            var pixel = _inverseTransform * ChevalTuple.Point(worldX, worldY, -1);
            var origin = _inverseTransform * ChevalTuple.Point(0, 0, 0);
            var direction = ChevalTuple.Normalize(pixel - origin);

            return new Ray(origin, direction);
        }

        public Canvas Render(Scene s) => Render(s, () => new DefaultSampler(s, this));
        public Canvas Render(Scene scene, Func<ISampler> samplerFactory)
        {
            var image = new Canvas(HSize, VSize);
            Parallel.For(0, VSize, (y) =>
            {
                var sampler = samplerFactory();
                for (var x = 0; x < HSize; x++)
                {
                   // var ray = RayForPixel(x, y);
                    image.WritePixel(x, y, sampler.Sample(x,y));
                }
            });

            return image;
           
        }
    }
}
