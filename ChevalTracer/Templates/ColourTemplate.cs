using System;
using System.Collections.Generic;
using System.Text;
using Cheval.Models;

namespace Cheval.Templates
{
    public class ColourTemplate
    {
        public static ChevalColour Black => new ChevalColour(0, 0, 0);
        public static ChevalColour White => new ChevalColour(1, 1, 1);
        public static ChevalColour Blue => new ChevalColour(0, 0, 1);
        public static ChevalColour Red => new ChevalColour(1, 0, 0);
        public static ChevalColour Green => new ChevalColour(0, 1, 0);
    }
}
