using System;
using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;
using Cheval.Models;

namespace Cheval.Patterns
{
    public class TestPattern : Pattern
    {

        public TestPattern(ChevalColour first, ChevalColour second) : base(first, second)
        {

        }

        public TestPattern(List<ChevalColour> colours) : base(colours)
        {
        }

        public TestPattern()
        {

        }

        public override ChevalColour ColourAt(ChevalTuple point)
        {
            return new ChevalColour(point.X, point.Y, point.Z);
        }
    }
}
