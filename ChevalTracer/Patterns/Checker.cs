using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;
using Cheval.Models;
using static System.MathF;

namespace Cheval.Patterns
{
    public class Checker: Pattern
    {
        public Checker(ChevalColour first, ChevalColour second) : base(first, second)
        {

        }

        public Checker(List<ChevalColour> colours) : base(colours)
        {
        }
        public override ChevalColour ColourAt(ChevalTuple point)
        {
            int placement = (int)(Floor(point.X) + Floor(point.Y) + Floor(point.Z));
            var choice = (int)Abs(placement % Colours.Count);
            return Colours[choice];
        }
    }
}
