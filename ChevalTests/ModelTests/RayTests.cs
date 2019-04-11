
using System.Collections.Generic;
using Cheval.Models;
using Cheval.Models.Primitives;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    public class RayTests
    {
        /*
         * Scenario: Creating and querying a ray
           Given origin ← point(1, 2, 3)
           And direction ← vector(4, 5, 6)
           When r ← ray(origin, direction)
           Then r.origin = origin
           And r.direction = direction
         */
        [Test]
        public void Creating_ray_test()
        {
            //Assign
            var origin = new ChevalPoint(1, 2, 3);
            var dir = new ChevalVector(4,5,6);
            //Act
            var ray = new Ray(origin, dir);
            //Assert
            ray.Origin.Should().BeEquivalentTo(origin);
            ray.Direction.Should().BeEquivalentTo(dir);
        }
        /*
         * Scenario: Computing a point from a distance
           Given r ← ray(point(2, 3, 4), vector(1, 0, 0))
           Then position(r, 0) = point(2, 3, 4)
           And position(r, 1) = point(3, 3, 4)
           And position(r, -1) = point(1, 3, 4)
           And position(r, 2.5) = point(4.5, 3, 4)
         */
        [Test]
        public void Computing_a_point_from_a_distance()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(2,3,4), new ChevalVector(1,0,0) );
            //Act
            var result0 = ray.Position(0);
            var expected0 = new ChevalPoint(2,3,4);
            var result1 = ray.Position(1);
            var expected1 = new ChevalPoint(3, 3, 4);
            var resultM1 = ray.Position(-1);
            var expectedM1 = new ChevalPoint(1, 3, 4);
            var result25 = ray.Position(2.5);
            var expected25 = new ChevalPoint(4.5, 3, 4);
            //Assert
            result0.Should().BeEquivalentTo(expected0);
            result1.Should().BeEquivalentTo(expected1);
            resultM1.Should().BeEquivalentTo(expectedM1);
            result25.Should().BeEquivalentTo(expected25);
        }
        /*
         * Scenario: A ray intersects a sphere at two points
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0] = 4.0
           And xs[1] = 6.0
         */
        [Test]
        public void Ray_interacts_with_sphere_twice()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0,0, -5), new ChevalVector(0,0,1) );
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].T.Should().Be(4.0);
            xs[1].T.Should().Be(6.0);
        }
        /*
         * Scenario: A ray intersects a sphere at a tangent
           Given r ← ray(point(0, 1, -5), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0] = 5.0
           And xs[1] = 5.0
         */
        [Test]
        public void Ray_interacts_with_sphere_at_tangent()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0, 1, -5), new ChevalVector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].T.Should().Be(5.0);
            xs[1].T.Should().Be(5.0);
        }
        /*
         * Scenario: A ray misses a sphere
           Given r ← ray(point(0, 2, -5), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 0
         */
        [Test]
        public void Ray_misses_sphere()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0, 2, -5), new ChevalVector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(0);
        }
        /*
         * Scenario: A ray originates inside a sphere
           Given r ← ray(point(0, 0, 0), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0] = -1.0
           And xs[1] = 1.0
         */
        [Test]
        public void Ray_interacts_with_sphere_when_inside()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0, 0, -0), new ChevalVector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].T.Should().Be(-1.0);
            xs[1].T.Should().Be(1.0);
        }
        /*
         * Scenario: A sphere is behind a ray
           Given r ← ray(point(0, 0, 5), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0] = -6.0
           And xs[1] = -4.0
         */
        [Test]
        public void Ray_interacts_with_sphere_when_behind()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0, 0, 5), new ChevalVector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].T.Should().Be(-6.0);
            xs[1].T.Should().Be(-4.0);
        }
        /*
         * Scenario: Intersect sets the object on the intersection
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And s ← sphere()
           When xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0].object = s
           And xs[1].object = s
         */
        public void Intersect_sets_object_on_intersection()
        {
            //Assign
            var ray = new Ray(new ChevalPoint(0, 0, -5), new ChevalVector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].Object.Should().BeEquivalentTo(s);
            xs[1].Object.Should().BeEquivalentTo(s);
        }
    }
}
