using System;
using System.Collections.Generic;
using System.Text;

namespace Cheval.Models
{
    public class Canvas
    {
        private const bool IgnoreOutOutBounds = true;
        public int Height { get; set; }
        public int Width { get; set; }
        private Pixel[,] CanvasPixels { get; set; }

        public Canvas(int width, int height)
        {
            Height = height;
            Width = width;
            CanvasPixels = new Pixel[Width, Height];
            var defaultColour = new ChevalColour(0,0,0);
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    CanvasPixels[i,j] = new Pixel(defaultColour);
                }
            }
        }

        public ChevalColour GetPixel(int x, int y)
        {
            return CanvasPixels[x, y].Colour;
        }

        public void WritePixel(int x, int y, ChevalColour colour)
        {
            if (x >= Width || x < 0 )
            {
                if (IgnoreOutOutBounds) return;
                throw new ArgumentOutOfRangeException(nameof(x), "Pixel location out of range");
            }

            if (y >= Height || y < 0)
            {
                if (IgnoreOutOutBounds) return;
                throw new ArgumentOutOfRangeException(nameof(y), "Pixel location out of range");
            }
            CanvasPixels[x, y].Colour = colour;
        }

    }

    //Do I need a pixel or a colour here?
    public class Pixel
    {
        public ChevalColour Colour;

        public Pixel(ChevalColour colour)
        {
            if (colour is null)
            {
                colour = new ChevalColour(0,0,0);
            }
            Colour = colour;
        }
    }
}
