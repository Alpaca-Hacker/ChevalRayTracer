using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ShapeTests
{
    public class TriangleTests
    {
        /*
         * Scenario: Constructing a triangle
           Given p1 ← point(0, 1, 0)
           And p2 ← point(-1, 0, 0)
           And p3 ← point(1, 0, 0)
           And t ← triangle(p1, p2, p3)
           Then t.p1 = p1
           And t.p2 = p2
           And t.p3 = p3
           And t.e1 = vector(-1, -1, 0)
           And t.e2 = vector(1, -1, 0)
           And t.normal = vector(0, 0, -1)
         */
        [Test]
        public void Constructing_a_triangle()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            //Act
            var triangle = new Triangle(p1, p2, p3);
            var expectedEdge1 = Vector(-1, -1, 0);
            var expectedEdge2 = Vector(1, -1, 0);
            var expectedNormal = Vector(0, 0, -1);
            //Assert
            triangle.Point1.Should().BeEquivalentTo(p1);
            triangle.Point2.Should().BeEquivalentTo(p2);
            triangle.Point3.Should().BeEquivalentTo(p3);

            triangle.Edge1.Should().BeEquivalentTo(expectedEdge1);
            triangle.Edge2.Should().BeEquivalentTo(expectedEdge2);
            triangle.Normal.Should().BeEquivalentTo(expectedNormal);
        }

        /*
         * Scenario: Finding the normal on a triangle
           Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           When n1 ← local_normal_at(t, point(0, 0.5, 0))
           And n2 ← local_normal_at(t, point(-0.5, 0.75, 0))
           And n3 ← local_normal_at(t, point(0.5, 0.25, 0))
           Then n1 = t.normal
           And n2 = t.normal
           And n3 = t.normal
         */
        [Test]
        public void Normal_on_a_triangle()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            //Act
            var result1 = triangle.NormalAt(Point(0, 0.5f, 0));
            var result2 = triangle.NormalAt(Point(-0.5f, 0.75f, 0));
            var result3 = triangle.NormalAt(Point(-0.5f, 0.25f, 0));

            var expected = triangle.Normal;
            //Assert
            result1.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
            result3.Should().BeEquivalentTo(expected);
        }

        /*
         *Scenario: Intersecting a ray parallel to the triangle
           Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           And r ← ray(point(0, -1, -2), vector(0, 1, 0))
           When xs ← local_intersect(t, r)
           Then xs is empty
         */
        [Test]
        public void Intersecting_ray_parallel_to_triangle()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            var ray = new Ray(Point(0, -1, -2), Vector(0, 1, 0));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }

        /*
         * Scenario: A ray misses the p1-p3 edge
           Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           And r ← ray(point(1, 1, -2), vector(0, 0, 1))
           When xs ← local_intersect(t, r)
           Then xs is empty
         */
        [Test]
        public void Ray_misses_p1_p3_edge()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            var ray = new Ray(Point(1, 1, -2), Vector(0, 0, 1));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*
         * Scenario: A ray misses the p1-p2 edge
           Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           And r ← ray(point(-1, 1, -2), vector(0, 0, 1))
           When xs ← local_intersect(t, r)
           Then xs is empty
           */
        [Test]
        public void Ray_misses_p1_p2_edge()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            var ray = new Ray(Point(-1, 1, -2), Vector(0, 0, 1));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*
          Scenario: A ray misses the p2-p3 edge
          Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
          And r ← ray(point(0, -1, -2), vector(0, 0, 1))
          When xs ← local_intersect(t, r)
          Then xs is empty
        */
        [Test]
        public void Ray_misses_p2_p3_edge()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            var ray = new Ray(Point(0, -1, -2), Vector(0, 0, 1));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*
         * Scenario: A ray strikes a triangle
           Given t ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           And r ← ray(point(0, 0.5, -2), vector(0, 0, 1))
           When xs ← local_intersect(t, r)
           Then xs.count = 1
           And xs[0].t = 2
         */
        [Test]
        public void Ray_hits_triangle()
        {
            //Assign
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var triangle = new Triangle(p1, p2, p3);
            var ray = new Ray(Point(0, 0.5f, -2), Vector(0, 0, 1));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs.Should().HaveCount(1);
            xs[0].T.Should().Be(2);

        }
    }
}
