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
        public float FOV { get; set; }

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

        public float PixelSize => (HalfWidth * 2) / HSize;

        private float HalfView => MathF.Tan(FOV / 2);
        private float Aspect => (float)HSize / (float)VSize;

        public float HalfWidth => Aspect >= 1 ? HalfView : HalfView * Aspect;
        public float HalfHeight => Aspect >= 1 ? HalfView / Aspect : HalfView;

        public Camera(int hSize, int vSize, float fov)
        {
            HSize = hSize;
            VSize = vSize;
            FOV = fov;
            Transform = Helper.Transform.IdentityMatrix;
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5f) * PixelSize;
            var yOffset = (py + 0.5f) * PixelSize;

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
                    image.WritePixel(x, y, sampler.Sample(x, y));
                }
            });

            //for (var y = 0; y < VSize; y++) 
            //{
            //    var sampler = samplerFactory();
            //    for (var x = 0; x < HSize; x++)
            //    {
            //        // var ray = RayForPixel(x, y);
            //        image.WritePixel(x, y, sampler.Sample(x, y));
            //    }
            //}

            return image;
           
        }
    }
}
