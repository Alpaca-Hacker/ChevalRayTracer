using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;

namespace Cheval.Patterns
{
    public abstract class Pattern
    {
        private Matrix _transform;
        private Matrix _inverseTransform;
        public abstract ChevalColour ColourAt(ChevalTuple point);
        public Matrix Transform
        {
            get => _transform;
            set
            {
                _transform = value;
                _inverseTransform = Matrix.Inverse(value);
            }
        }

        public List<ChevalColour> Colours { get; set; }

        public Matrix InverseTransform => _inverseTransform;

        protected Pattern()
        {
            Transform = Helper.Transform.IdentityMatrix;
            Colours = new List<ChevalColour>();
        }

        protected Pattern(ChevalColour first, ChevalColour second)
        {
            Transform = Helper.Transform.IdentityMatrix;
            Colours = new List<ChevalColour>
            {
                first,
                second
            };
        }

        protected Pattern(List<ChevalColour> colours)
        {
            Colours = colours;
            Transform = Helper.Transform.IdentityMatrix;
        }

        public ChevalColour ColourAtObject(Shape shape, ChevalTuple point)
        {
            var objectPoint = shape.InverseTransform * point;
            var patternPoint = InverseTransform * objectPoint;
            return ColourAt(patternPoint);
        }
    }
}