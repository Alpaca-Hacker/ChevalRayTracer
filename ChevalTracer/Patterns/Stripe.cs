using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;

namespace Cheval.Patterns
{
    public class Stripe : Pattern
    {


        public Stripe(ChevalColour first, ChevalColour second) : base(first, second)
        {

        }

        public Stripe(List<ChevalColour> colours) :base (colours)
        {
        }

        public override ChevalColour ColourAt(ChevalTuple point)
        {
            var choice = (int)MathF.Abs(MathF.Floor(point.X) % Colours.Count);
            return Colours[choice];
        }

    }

}
