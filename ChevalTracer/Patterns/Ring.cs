using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;
using Cheval.Models;
using static System.MathF;

namespace Cheval.Patterns
{
    public class Ring : Pattern
    {
        public Ring(ChevalColour first, ChevalColour second) :base(first, second)
        {

        }
        public Ring(List<ChevalColour> colours) : base(colours)
        {
        }
        public override ChevalColour ColourAt(ChevalTuple point)
        {
            var placement = Sqrt(Pow(point.X, 2) + Pow(point.Z, 2));
            var choice = (int)Abs(Floor(placement) % Colours.Count);
            return Colours[choice];
        }
    }
}
