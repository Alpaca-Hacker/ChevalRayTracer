using System;
using System.Collections.Generic;
using System.Text;
using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    public class MaterialTests
    {
        /*
         * Scenario: The default material
           Given m ← material()
           Then m.color = color(1, 1, 1)
           And m.ambient = 0.1
           And m.diffuse = 0.9
           And m.specular = 0.9
           And m.shininess = 200.0
         */
        [Test]
        public void Default_material_tests()
        {
            //Assign
            var m = new Material();
            //Assert
            m.Colour.Should().Be(new ChevalColour(1, 1, 1));
            m.Ambient.Should().Be(0.1);
            m.Diffuse.Should().Be(0.9);
            m.Specular.Should().Be(0.9);
            m.Shininess.Should().Be(200.0);
        }
    }
}
