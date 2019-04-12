using System;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
   public abstract class Shape
    {
        public abstract ChevalTuple NormalAt(ChevalTuple point);
        public abstract Guid Id { get; }
        public abstract ChevalTuple Origin { get; set; }
        public abstract double Size { get; set; }
        public abstract Matrix Transform { get; set; }
        public abstract Material Material { get; set; }
    }
}
