using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    public class ColourTests
    {
        /*
         Scenario: Colors are (red, green, blue) tuples
         Given c ← color(-0.5, 0.4, 1.7)
           Then c.red = -0.5
           And c.green = 0.4
           And c.blue = 1.7
           */
        [Test]
        public void Colours_are_Tuples()
        {
            //Assign
            var c1 = new ChevalColour(-0.5, 0.4, 1.7);

            //Assert
            c1.Red.Should().Be(-0.5);
            c1.Green.Should().Be(0.4);
            c1.Blue.Should().Be(1.7);
        }
        /*
         * Scenario: Adding colors
           Given c1 ← color(0.9, 0.6, 0.75)
           And c2 ← color(0.7, 0.1, 0.25)
           Then c1 + c2 = color(1.6, 0.7, 1.0)
           */
        [Test]
        public void Colours_can_be_added()
        {
            //Assign
            var c1 = new ChevalColour(0.9, 0.6, 0.75);
            var c2 = new ChevalColour(0.7, 0.1, 0.25);
            //Act
            var expected = new ChevalColour(1.6, 0.7, 1.0);
            var result = c1 + c2;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
           Scenario: Subtracting colors
           Given c1 ← color(0.9, 0.6, 0.75)
           And c2 ← color(0.7, 0.1, 0.25)
           Then c1 - c2 = color(0.2, 0.5, 0.5)
          */
        [Test]
        public void Colours_can_be_subtracted()
        {
            //Assign
            var c1 = new ChevalColour(0.9, 0.6, 0.75);
            var c2 = new ChevalColour(0.7, 0.1, 0.25);
            //Act
            var expected = new ChevalColour(0.2, 0.5, 0.5);
            var result = c1 - c2;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
           Scenario: Multiplying a color by a scalar
           Given c ← color(0.2, 0.3, 0.4)
           Then c * 2 = color(0.4, 0.6, 0.8)
         */
        [Test]
        public void Colours_can_be_multiplied_by_a_scaler()
        {
            //Assign
            var c1 = new ChevalColour(0.2, 0.3, 0.4);
           // var c2 = new ChevalColour(0.7, 0.1, 0.25);
            //Act
            var expected = new ChevalColour(0.4, 0.6, 0.8);
            var result = c1 * 2;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
