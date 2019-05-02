using System;
using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ShapeTests
{
    public class SmoothTriangleTests
    {
        /*Background
         *Given p1 ← point(0, 1, 0)
           And p2 ← point(-1, 0, 0)
           And p3 ← point(1, 0, 0)
           And n1 ← vector(0, 1, 0)
           And n2 ← vector(-1, 0, 0)
           And n3 ← vector(1, 0, 0)
           When tri ← smooth_triangle(p1, p2, p3, n1, n2, n3)
         */
        public SmoothTriangle GetTestTriangle()
        {
            var p1 = Point(0, 1, 0);
            var p2 = Point(-1, 0, 0);
            var p3 = Point(1, 0, 0);
            var n1 = Vector(0, 1, 0);
            var n2 = Vector(-1, 0, 0);
            var n3 = Vector(1, 0, 0);
            return new SmoothTriangle(p1, p2, p3, n1, n2, n3);
        }
        /*
         * Scenario: An intersection with a smooth triangle stores u/v
           When r ← ray(point(-0.2, 0.3, -2), vector(0, 0, 1))
           And xs ← local_intersect(tri, r)
           Then xs[0].u = 0.45
           And xs[0].v = 0.25
         */
        [Test]
        public void Intersection_with_smooth_triangle_stores_u_v()
        {
            //Assign
            var triangle = GetTestTriangle();
            var ray = new Ray(Point(-0.2f, 0.3f, -2), Vector(0, 0, 1));
            //Act
            var xs = triangle.Intersect(ray);
            //Assert
            xs[0].U.Should().Be(0.45f);
            xs[0].V.Should().Be(0.25f);
        }
        /*
         * Scenario: Preparing the normal on a smooth triangle
           When i ← intersection_with_uv(1, tri, 0.45, 0.25)
           And r ← ray(point(-0.2, 0.3, -2), vector(0, 0, 1))
           And xs ← intersections(i)
           And comps ← prepare_computations(i, r, xs)
           Then comps.normalv = vector(-0.5547, 0.83205, 0)
         */
        [Test]
        public void Preparing_normal_on_smooth_triangle()
        {
            //Assign
            var tri = GetTestTriangle();
            var i = new Intersection(1, tri, 0.45f, 0.25f);
            var ray = new Ray(Point(-0.2f,0.3f,-2), Vector(0,0,1));
            var xs = new Intersections(new List<Intersection>{i});
            //Act
            var comps = new Computations(i, ray, xs);
            var expected = Vector(-0.5547f, 0.83205f, 0);
            //Assert
            comps.NormalV.Should().BeEquivalentTo(expected);
        }

    }
}

