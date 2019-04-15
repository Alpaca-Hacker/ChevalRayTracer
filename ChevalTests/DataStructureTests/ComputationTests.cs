using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static System.Math;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.DataStructureTests
{
    public class ComputationTests
    {
        /*
         * Scenario: Precomputing the state of an intersection
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(4, shape)
           When comps ← prepare_computations(i, r)
           Then comps.t = i.t
           And comps.object = i.object
           And comps.point = point(0, 0, -1)
           And comps.eyev = vector(0, 0, -1)
           And comps.normalv = vector(0, 0, -1)
         */
        [Test]
        public void Precomputing_state_of_intersection()
        {
            //Assign
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            //Act
            var comps = new Computations(i, ray);
            var expectedPoint = Point(0, 0, -1);
            var expectedEyeV = Vector(0, 0, -1);
            var expectedNormalV = Vector(0, 0, -1);
            //Assert
            comps.T.Should().Be(i.T);
            comps.Shape.Should().BeEquivalentTo(i.Object);
            comps.Point.Should().BeEquivalentTo(expectedPoint);
            comps.EyeV.Should().BeEquivalentTo(expectedEyeV);
            comps.NormalV.Should().BeEquivalentTo(expectedNormalV);
        }
        /*
         * Scenario: The hit, when an intersection occurs on the outside
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(4, shape)
           When comps ← prepare_computations(i, r)
           Then comps.inside = false
           */
        [Test]
        public void Intersection_occurs_on_outside()
        {
            //Assign
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            //Act
            var comps = new Computations(i, ray);
            //Assert
            comps.Inside.Should().BeFalse();
        }
        /*
           Scenario: The hit, when an intersection occurs on the inside
           Given r ← ray(point(0, 0, 0), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(1, shape)
           When comps ← prepare_computations(i, r)
           Then comps.point = point(0, 0, 1)
           And comps.eyev = vector(0, 0, -1)
           And comps.inside = true
           And comps.normalv = vector(0, 0, -1)
         */
        [Test]
        public void Intersection_occurs_on_inside()
        {
            var ray = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            //Act
            var comps = new Computations(i, ray);
            var expectedPoint = Point(0, 0, 1);
            var expectedEyeV = Vector(0, 0, -1);
            var expectedNormalV = Vector(0, 0, -1);
            //Assert
            comps.Inside.Should().BeTrue();
            comps.Point.Should().BeEquivalentTo(expectedPoint);
            comps.EyeV.Should().BeEquivalentTo(expectedEyeV);
            comps.NormalV.Should().BeEquivalentTo(expectedNormalV);
        }
        /*
         * Scenario: Precomputing the reflection vector
           2 Given shape ← plane()
           3 And r ← ray(point(0, 1, -1), vector(0, -√2/2, √2/2))
           4 And i ← intersection(√2, shape)
           5 When comps ← prepare_computations(i, r)
           6 Then comps.reflectv = vector(0, √2/2, √2/2)
         */

        [Test]
        public void Precomputing_reflection_vector()
        {
            //Assign
            var shape = new Plane();
            var ray = new Ray(Point(0,1,-1), Vector(0,-Sqrt(2)/2, Sqrt(2) / 2));
            var inter = new Intersection(Sqrt(2), shape);
            //Act
            var comps = new Computations(inter, ray);
            var expected = Vector(0, Sqrt(2) / 2, Sqrt(2) / 2);
            //Assert
            comps.ReflectV.Should().BeEquivalentTo(expected);
        }


    }
}
