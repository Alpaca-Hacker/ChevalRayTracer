
using System.Collections.Generic;
using System.Xml.Linq;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Primitives;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.Models.ChevalTuple;

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
            var origin = Point(1, 2, 3);
            var dir = Vector(4,5,6);
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
            var ray = new Ray(Point(2,3,4), Vector(1,0,0) );
            //Act
            var result0 = ray.Position(0);
            var expected0 = Point(2,3,4);
            var result1 = ray.Position(1);
            var expected1 = Point(3, 3, 4);
            var resultM1 = ray.Position(-1);
            var expectedM1 = Point(1, 3, 4);
            var result25 = ray.Position(2.5);
            var expected25 = Point(4.5, 3, 4);
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
            var ray = new Ray(Point(0,0, -5), Vector(0,0,1) );
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
            var ray = new Ray(Point(0, 1, -5), Vector(0, 0, 1));
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
            var ray = new Ray(Point(0, 2, -5), Vector(0, 0, 1));
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
            var ray = new Ray(Point(0, 0, -0), Vector(0, 0, 1));
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
            var ray = new Ray(Point(0, 0, 5), Vector(0, 0, 1));
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
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var s = new Sphere();
            //Act
            var xs = ray.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].Object.Should().BeEquivalentTo(s);
            xs[1].Object.Should().BeEquivalentTo(s);
        }
        /*
         * Scenario: Translating a ray
           Given r ← ray(point(1, 2, 3), vector(0, 1, 0))
           And m ← translation(3, 4, 5)
           When r2 ← transform(r, m)
           Then r2.origin = point(4, 6, 8)
           And r2.direction = vector(0, 1, 0)
         */
        [Test]
        public void Translating_a_ray_tests()
        {
            //Assign
            var r = new Ray(Point(1,2,3), Vector(0,1,0));
            var m = Transform.Translation(3, 4, 5);
            //Act
            var result = r.Transform(m);
            var expectedOrigin = Point(4,6,8);
            var expectedDirection = Vector(0,1,0);
            //Assert
            result.Origin.Should().BeEquivalentTo(expectedOrigin);
            result.Direction.Should().BeEquivalentTo(expectedDirection);
        }
        /*
         *Scenario: Scaling a ray
           Given r ← ray(point(1, 2, 3), vector(0, 1, 0))
           And m ← scaling(2, 3, 4)
           When r2 ← transform(r, m)
           Then r2.origin = point(2, 6, 12)
           And r2.direction = vector(0, 3, 0)
         */
        [Test]
        public void scaling_a_ray_test()
        {
            //Assign
            var r = new Ray(Point(1, 2, 3), Vector(0, 1, 0));
            var m = Transform.Scaling(2,3, 4);
            //Act
            var result = r.Transform(m);
            var expectedOrigin = Point(2, 6, 12);
            var expectedDirection = Vector(0, 3, 0);
            //Assert
            result.Origin.Should().BeEquivalentTo(expectedOrigin);
            result.Direction.Should().BeEquivalentTo(expectedDirection);
        }
        /*
         * Scenario: Intersecting a scaled sphere with a ray
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And s ← sphere()
           When set_transform(s, scaling(2, 2, 2))
           And xs ← intersect(s, r)
           Then xs.count = 2
           And xs[0].t = 3
           And xs[1].t = 7
         */
        [Test]
        public void Intersecting_a_scaled_sphere_with_a_ray()
        {
            //Assign
            var r= new Ray(Point(0,0,-5), Vector(0,0,1));
            var s = new Sphere();
            //Act
            s.Transform = Transform.Scaling(2, 2, 2);
            var xs = r.Intersect(s);
            //Assert
            xs.Should().HaveCount(2);
            xs[0].T.Should().Be(3);
            xs[1].T.Should().Be(7);
        }

        /*
          Scenario: Intersecting a translated sphere with a ray
          Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
          And s ← sphere()
          When set_transform(s, translation(5, 0, 0))
          And xs ← intersect(s, r)
          Then xs.count = 0
        */
        [Test]
        public void Intersecting_a_translated_sphere_with_a_ray()
        {
            //Assign
            var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var s = new Sphere();
            //Act
            s.Transform = Transform.Translation(5, 0, 0);
            var xs = r.Intersect(s);
            //Assert
            xs.Should().BeEmpty();
        }
    }

}
