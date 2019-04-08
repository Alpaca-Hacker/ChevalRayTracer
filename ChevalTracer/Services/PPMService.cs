using System;
using Cheval.Models;

namespace Cheval.Services
{
    public static class PPMService
    {
        private const int MaxColour = 255;
        private const int MaxLength = 66;
        public static string ToPPM(this Canvas canvas)
        {
            var PPMString = "P3\n";
            PPMString += $"{canvas.Width} {canvas.Height}\n";
            PPMString += $"{MaxColour}\n";

            for (var i = 0; i < canvas.Height; i++)
            {
                var rowColours = "";
                for (var j = 0; j < canvas.Width; j++)
                {
                    var colour = canvas.GetPixel(j, i);
                    rowColours += $"{(int)Math.Ceiling(Math.Clamp(colour.Red * MaxColour,0, MaxColour))} " +
                                  $"{(int)Math.Ceiling(Math.Clamp(colour.Green * MaxColour,0, MaxColour))} " +
                                  $"{(int)Math.Ceiling(Math.Clamp(colour.Blue * MaxColour, 0, MaxColour))}";

                    if (rowColours.Length > MaxLength)
                    {
                        PPMString += rowColours + "\n";
                        rowColours = "";
                    }
                    else
                    {
                        rowColours += " ";
                    }
                }

                if (!string.IsNullOrEmpty(rowColours))
                {
                    PPMString += rowColours.Trim() + "\n";
                }
            }

            PPMString += "\n";

            return PPMString;
        }
    }
}
