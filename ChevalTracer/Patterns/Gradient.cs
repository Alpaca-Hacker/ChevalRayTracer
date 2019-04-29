using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Models;

namespace Cheval.Patterns
{
    public class Gradient :Pattern
    {
        public Gradient(ChevalColour first, ChevalColour second): base(first, second)
        {

        }
        public Gradient(List<ChevalColour> colours) : base(colours)
        {
            //TODO More than 2 colours
        }

        public override ChevalColour ColourAt(ChevalTuple point)
        {
            var distance = Colours[1] - Colours[0];
            var fraction = point.X - MathF.Floor(point.X);
            return Colours[0] + distance * fraction;
        }
    }
}
