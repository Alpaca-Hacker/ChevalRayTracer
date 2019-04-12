using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ModelTests
{
    public class LightTests
    {
        /*
         * Scenario: A point light has a position and intensity
           Given intensity ← color(1, 1, 1)
           And position ← point(0, 0, 0)
           When light ← point_light(position, intensity)
           Then light.position = position
           And light.intensity = intensity
         */
        [Test]
        public void Point_light_has_position_and_intensity()
        {
            //Assign
            var intensity = new ChevalColour(1, 1, 1);
            var position = Point(0, 0, 0);
            //Act
            var light = Light.PointLight(position, intensity);
            //Assert
            light.Position.Should().BeEquivalentTo(position);
            light.Intensity.Should().BeEquivalentTo(intensity);
        }
    }
}
