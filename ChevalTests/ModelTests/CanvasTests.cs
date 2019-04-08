using System.Security;
using Cheval.Models;
using Cheval.Services;
using FluentAssertions;
using NUnit.Framework;


namespace ChevalTests.ModelTests
{
    public class CanvasTests
    {
        /*
         *Scenario ​: Creating a canvas ​   ​
         * Given ​ c ← canvas( 10, 20) ​   ​
         * Then ​ c.width = 10 ​   ​
         * And ​ c.height = 20 ​   ​
         * And ​ every pixel of c is color( 0, 0, 0)
           
           Jamis Buck. The Ray Tracer Challenge (Kindle Locations 779-786). The Pragmatic Bookshelf, LLC. 
         */
        [Test]
        public void Canvas_created_as_expected()
        {
            //Assign
            var c = new Canvas(10, 20);
            var black = new ChevalColour(0,0,0);
            //Assert
            c.Width.Should().Be(10);
            c.Height.Should().Be(20);

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    c.GetPixel(i, j).Should().BeEquivalentTo(black);
                } 
            }
        }
        /*
         * Scenario ​: Writing pixels to a canvas ​   ​
         * Given ​ c ← canvas( 10, 20) ​   ​
         * And ​ red ← color( 1, 0, 0) ​   ​
         * When ​ write_pixel( c, 2, 3, red) ​   ​
         * Then ​ pixel_at( c, 2, 3) = red
           
           Jamis Buck. The Ray Tracer Challenge (Kindle Locations 791-797). The Pragmatic Bookshelf, LLC. 
         */
        [Test]
        public void Writing_pixels_test()
        {
            //Assign
            var canvas = new Canvas(10, 20);
            var red = new ChevalColour(1,0,0);
            //Act
            canvas.WritePixel(2,3,red);
            //Assert
            canvas.GetPixel(2, 3).Should().Be(red);
        }
        /*
         * Scenario ​: Constructing the PPM header ​   ​
         * Given ​ c ← canvas( 5, 3) ​   ​
         * When ​ ppm ← canvas_to_ppm( c) ​   ​
         * Then ​ lines 1-3 of ppm are ​  
         * ​""" ​ ​   ​
         * P3 ​ ​   ​
         * 5 3
         * 255
         * """

Jamis Buck. The Ray Tracer Challenge (Kindle Locations 829-838). The Pragmatic Bookshelf, LLC. 
         */

        [Test]
        public void PPM_header_is_correct()
        {
            //Assign
            var canvas = new Canvas(5,3);
            //Act
            var result = canvas.ToPPM();
            var resultLines = result.Split('\n');
            //Assert
            resultLines[0].Should().Be("P3");
            resultLines[1].Should().Be("5 3");
            resultLines[2].Should().Be("255");

        }

        /*
         * Scenario ​: Constructing the PPM pixel data ​   ​
         * Given ​ c ← canvas( 5, 3) ​   ​
         * And ​ c1 ← color( 1.5, 0, 0)
         * And ​ c2 ← color( 0, 0.5, 0)
         * ​And ​ c3 ← color(-0.5, 0, 1) ​   ​
         * When ​ write_pixel( c, 0, 0, c1) ​   ​
         * And ​ write_pixel( c, 2, 1, c2) ​   ​
         * And ​ write_pixel( c, 4, 2, c3) ​   ​
         * And ​ ppm ← canvas_to_ppm( c) ​   ​
         * Then ​ lines 4-6 of ppm are ​   ​
         * """ ​ ​   ​
         * 255 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ​ ​   ​
         * 0 0 0 0 0 0 0 128 0 0 0 0 0 0 0 ​ ​   ​
         * 0 0 0 0 0 0 0 0 0 0 0 0 0 0 255 ​ ​   ​
         * """

Jamis Buck. The Ray Tracer Challenge (Kindle Locations 853-871). The Pragmatic Bookshelf, LLC.   
         */
        [Test]
        public void Pixel_data_written_correctly()
        {
            //Assign
            var canvas = new Canvas(5, 3);
            var c1 = new ChevalColour(1.5,0,0);
            var c2 = new ChevalColour(0,0.5,0);
            var c3 = new ChevalColour(-0.5, 0, 1);
            //Act
            canvas.WritePixel(0, 0, c1);
            canvas.WritePixel(2, 1, c2);
            canvas.WritePixel(4, 2, c3);

            var result = canvas.ToPPM();
            var resultLines = result.Split('\n');
            //Assert
            resultLines[3].Should().Be("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
            resultLines[4].Should().Be("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0");
            resultLines[5].Should().Be("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");

        }
    }
}
