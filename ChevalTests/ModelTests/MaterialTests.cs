using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Models.ChevalColour;
using static Cheval.Models.Light;
using static Cheval.Templates.ColourTemplate;

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
        /*For the next few tests
         Background:
           Given m ← material()
           And position ← point(0, 0, 0)
         * Scenario: Lighting with the eye between the light and the surface
           Given eyev ← vector(0, 0, -1)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 0, -10), color(1, 1, 1))
           When result ← lighting(m, light, position, eyev, normalv)
           Then result = color(1.9, 1.9, 1.9)
           */
        [Test]
        public void Lighting_with_eye_between_light_and_surface()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 0, -10), new ChevalColour(1, 1, 1));
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV);
            var expected = new ChevalColour(1.9,1.9,1.9);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with the eye between light and surface, eye offset 45°
           Given eyev ← vector(0, √2/2, -√2/2)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 0, -10), color(1, 1, 1))
           When result ← lighting(m, light, position, eyev, normalv)
           Then result = color(1.0, 1.0, 1.0)
         */
        [Test]
        public void Lighting_with_eye_between_light_and_surface_offset_45()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 0, -10), new ChevalColour(1, 1, 1));
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV);
            var expected = new ChevalColour(1.0, 1.0, 1.0);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with eye opposite surface, light offset 45°
           Given eyev ← vector(0, 0, -1)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 10, -10), color(1, 1, 1))
           When result ← lighting(m, light, position, eyev, normalv)
           Then result = color(0.7364, 0.7364, 0.7364)
         */
        [Test]
        public void Lighting_with_eye_opposite_surface_light_offset_45()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, 0,-1);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 10, -10), new ChevalColour(1, 1, 1));
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV);
            var expected = new ChevalColour(0.7364, 0.7364, 0.7364);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with eye in the path of the reflection vector
           Given eyev ← vector(0, -√2/2, -√2/2)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 10, -10), color(1, 1, 1))
           When result ← lighting(m, light, position, eyev, normalv)
           Then result = color(1.6364, 1.6364, 1.6364)
         */
        [Test]
        public void Lighting_with_eye_in_path_of_offset_vector()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 10, -10), new ChevalColour(1, 1, 1));
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV);
            var expected = new ChevalColour(1.6364, 1.6364, 1.6364);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with the light behind the surface
           Given eyev ← vector(0, 0, -1)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 0, 10), color(1, 1, 1))
           When result ← lighting(m, light, position, eyev, normalv)
           Then result = color(0.1, 0.1, 0.1)
         */
        [Test]
        public void Lighting_with_eye_behind_surface()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 0, 10), new ChevalColour(1, 1, 1));
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV);
            var expected = new ChevalColour(0.1, 0.1, 0.1);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with the surface in shadow
           Given eyev ← vector(0, 0, -1)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 0, -10), color(1, 1, 1))
           And in_shadow ← true
           When result ← lighting(m, light, position, eyev, normalv, in_shadow)
           Then result = color(0.1, 0.1, 0.1)
         */
        [Test]
        public void Lighting_with_surface_in_shadow()
        {
            //Assign
            var m = new Material();
            var position = Point(0, 0, 0);
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 0, -10), new ChevalColour(1, 1, 1));
            var inShadow = true;
            //Act
            var result = m.Lighting(new Sphere(), light, position, eyeV, normalV, inShadow);
            var expected = new ChevalColour(0.1, 0.1, 0.1);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Lighting with a pattern applied
           Given m.pattern ← stripe_pattern(color(1, 1, 1), color(0, 0, 0))
           And m.ambient ← 1
           And m.diffuse ← 0
           And m.specular ← 0
           And eyev ← vector(0, 0, -1)
           And normalv ← vector(0, 0, -1)
           And light ← point_light(point(0, 0, -10), color(1, 1, 1))
           When c1 ← lighting(m, light, point(0.9, 0, 0), eyev, normalv, false)
           And c2 ← lighting(m, light, point(1.1, 0, 0), eyev, normalv, false)
           Then c1 = color(1, 1, 1)
           And c2 = color(0, 0, 0)
         */
        [Test]
        public void Pattern_test()
        {
            //Assign
            var pattern = new Stripe(White, Black);
            var mat = new Material
            {
                Ambient = 1,
                Diffuse = 0,
                Specular = 0,
                Pattern = pattern
            };
            var eyeV = Vector(0, 0, -1);
            var normalV = Vector(0, 0, -1);
            var light = PointLight(Point(0, 0, -10), White);
            //Act
            var result1 = mat.Lighting(new Sphere(), light, Point(0.9, 0, 0), eyeV, normalV, false);
            var result2 = mat.Lighting(new Sphere(), light, Point(1.1, 0, 0), eyeV, normalV, false);
            //Assert
            result1.Should().BeEquivalentTo(White);
            result2.Should().BeEquivalentTo(Black);
        }

    }
}
