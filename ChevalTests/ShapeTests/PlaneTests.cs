using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ShapeTests
{
    public class PlaneTests
    {
        /*Scenario: The normal of a plane is constant everywhere
           Given p ← plane()
           When n1 ← local_normal_at(p, point(0, 0, 0))
           And n2 ← local_normal_at(p, point(10, 0, -10))
           And n3 ← local_normal_at(p, point(-5, 0, 150))
           Then n1 = vector(0, 1, 0)
           And n2 = vector(0, 1, 0)
           And n3 = vector(0, 1, 0)
           */
        [Test]
        public void Normal_of_plane_is_constant()
        {
            //Assign
            var plane = new Plane();
            //Act
            var result1 = plane.NormalAt(Point(0, 0, 0));
            var result2 = plane.NormalAt(Point(10, 0, -10));
            var result3 = plane.NormalAt(Point(-5, 0, 150));
            var expected = Vector(0, 1, 0);
            //Assert
            result1.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
            result3.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Intersect with a ray parallel to the plane
           Given p ← plane()
           And r ← ray(point(0, 10, 0), vector(0, 0, 1))
           When xs ← local_intersect(p, r)
           Then xs is empty
           */
        [Test]
        public void Intersect_parallel_to_plane()
        {
            //Assign
            var plane = new Plane();
            var ray = new Ray(Point(0, 10, 0), Vector(0, 0, 1));
            //Act
            var xs = plane.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*
           Scenario: Intersect with a coplanar ray
           Given p ← plane()
           And r ← ray(point(0, 0, 0), vector(0, 0, 1))
           When xs ← local_intersect(p, r)
           Then xs is empty
         */
        [Test]
        public void Intersect_coplaner_to_plane()
        {
            //Assign
            var plane = new Plane();
            var ray = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
            //Act
            var xs = plane.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*
         * Scenario: A ray intersecting a plane from above
           Given p ← plane()
           And r ← ray(point(0, 1, 0), vector(0, -1, 0))
           When xs ← local_intersect(p, r)
           Then xs.count = 1
           And xs[0].t = 1
           And xs[0].object = p
           */
        [Test]
        public void Ray_intersecting_plane_from_above()
        {
            //Assign
            var plane = new Plane();
            var ray = new Ray(Point(0, 1, 0), Vector(0, -1, 0));
            //Act
            var xs = plane.Intersect(ray);
            //Assert
            xs.Should().HaveCount(1);
            xs[0].T.Should().Be(1);
            xs[0].Object.Should().BeEquivalentTo(plane);
        }

        /*
           Scenario: A ray intersecting a plane from below
           Given p ← plane()
           And r ← ray(point(0, -1, 0), vector(0, 1, 0))
           When xs ← local_intersect(p, r)
           Then xs.count = 1
           And xs[0].t = 1
           And xs[0].object = p
         */
        [Test]
        public void Ray_intersecting_plane_from_below()
        {
            //Assign
            var plane = new Plane();
            var ray = new Ray(Point(0, -1, 0), Vector(0, 1, 0));
            //Act
            var xs = plane.Intersect(ray);
            //Assert
            xs.Should().HaveCount(1);
            xs[0].T.Should().Be(1);
            xs[0].Object.Should().BeEquivalentTo(plane);
        }
    }
}
