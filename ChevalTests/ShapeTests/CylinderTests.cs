using System;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ShapeTests
{
    public class CylinderTests
    {
        //Scenario Outline: A ray misses a cylinder
        // Given cyl ← cylinder()
        // And direction ← normalize(<direction>)
        // And r ← ray(<origin>, direction)
        // When xs ← local_intersect(cyl, r)
        // Then xs.count = 0
        // Examples:
        // | origin | direction |
        // | point(1, 0, 0)  | vector(0, 1, 0) |
        // | point(0, 0, 0)  | vector(0, 1, 0) |
        // | point(0, 0, -5) | vector(1, 1, 1) |

        [TestCase(new double[] {1, 0, 0}, new double[] {0, 1, 0})]
        [TestCase(new double[] {0, 0, 0}, new double[] {0, 1, 0})]
        [TestCase(new double[] {0, 0, -5}, new double[] {1, 1, 1})]
        public void Ray_misses_cylinder(double[] point, double[] vector)
        {
            //Assign
            var cyl = new Cylinder();
            var dir = Vector(vector[0], vector[1], vector[2]);
            var ray = new Ray(Point(point[0], point[1], point[2]), dir);
            //Act
            var xs = cyl.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }

        /*Scenario Outline: A ray strikes a cylinder
           Given cyl ← cylinder()
           And direction ← normalize(<direction>)
           And r ← ray(<origin>, direction)
           When xs ← local_intersect(cyl, r)
           Then xs.count = 2
           And xs[0].t = <t0>
           And xs[1].t = <t1>
           Examples:
           |     origin        | direction         | t0      | t1      |
           | point(1, 0, -5)   | vector(0, 0, 1)   | 5       | 5       |
           | point(0, 0, -5)   | vector(0, 0, 1)   | 4       | 6       |
           | point(0.5, 0, -5) | vector(0.1, 1, 1) | 6.80798 | 7.08872 |
           */
        [TestCase(new double[] {1, 0, -5}, new double[] {0, 0, 1}, 5, 5)]
        [TestCase(new double[] {0, 0, -5}, new double[] {0, 0, 1}, 4, 6)]
        [TestCase(new double[] {0.5, 0, -5}, new double[] {0.1, 1, 1}, 6.80798, 7.08872)]
        public void Ray_hits_cylinder(double[] point, double[] vector, double t0, double t1)
        {
            //Assign
            var cyl = new Cylinder();
            var dir = Normalize(Vector(vector[0], vector[1], vector[2]));
            var ray = new Ray(Point(point[0], point[1], point[2]), dir);
            //Act
            var xs = cyl.Intersect(ray);
            //Assert
            Math.Round(xs[0].T, 5).Should().Be(t0);
            Math.Round(xs[1].T, 5).Should().Be(t1);
        }

        /*
         * Scenario Outline: Intersecting a constrained cylinder
           Given cyl ← cylinder()
           And cyl.minimum ← 1
           And cyl.maximum ← 2
           And direction ← normalize(<direction>)
           And r ← ray(<point>, direction)
           When xs ← local_intersect(cyl, r)
           Then xs.count = <count>
           Examples:
           |   |     point         |    direction       | count |
           | 1 | point(0, 1.5, 0)  | vector(0.1, 1, 0)  |   0   |
           | 2 | point(0, 3, -5)   | vector(0, 0, 1)    |   0   |
           | 3 | point(0, 0, -5)   | vector(0, 0, 1)    |   0   |
           | 4 | point(0, 2, -5)   | vector(0, 0, 1)    |   0   |
           | 5 | point(0, 1, -5)   | vector(0, 0, 1)    |   0   |
           | 6 | point(0, 1.5, -2) | vector(0, 0, 1)    |   2   |
         */
        [TestCase(new double[] {0, 1.5, 0}, new double[] {0.1, 1, 0}, 0)]
        [TestCase(new double[] {0, 3, -5}, new double[] {0, 0, 1}, 0)]
        [TestCase(new double[] {0, 0, -5}, new double[] {0, 0, 1}, 0)]
        [TestCase(new double[] {0, 2, -5}, new double[] {0, 0, 1}, 0)]
        [TestCase(new double[] {0, 1, -5}, new double[] {0, 0, 1}, 0)]
        [TestCase(new double[] {0, 1.5, -2}, new double[] {0, 0, 1}, 2)]
        public void Ray_hits_short_cylinder(double[] point, double[] dir, int count)
        {
            //Assign
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            var direct = Normalize(Vector(dir[0], dir[1], dir[2]));
            var ray = new Ray(Point(point[0], point[1], point[2]), direct);
            //Act
            var xs = cyl.Intersect(ray);
            //Assert
            xs.Count.Should().Be(count);
        }

        /*
         * Scenario Outline: Intersecting the caps of a closed cylinder
           Given cyl ← cylinder()
           And cyl.minimum ← 1
           And cyl.maximum ← 2
           And cyl.closed ← true
           And direction ← normalize(<direction>)
           And r ← ray(<point>, direction)
           When xs ← local_intersect(cyl, r)
           Then xs.count = <count>
           Examples:
           | | point | direction | count |
           | 1 | point(0, 3, 0) | vector(0, -1, 0) | 2 |
           | 2 | point(0, 3, -2) | vector(0, -1, 2) | 2 |
           | 3 | point(0, 4, -2) | vector(0, -1, 1) | 2 | # corner case
           | 4 | point(0, 0, -2) | vector(0, 1, 2) | 2 |
           | 5 | point(0, -1, -2) | vector(0, 1, 1) | 2 | # corner case
         */
        [TestCase(new double[] { 0, 3, 0 }, new double[] { 0, -1, 0 }, 2)]
        [TestCase(new double[] { 0, 3, -2 }, new double[] { 0, -1, 2 }, 2)]
        [TestCase(new double[] { 0, 4, -2 }, new double[] { 0, -1, 1 }, 2)]
        [TestCase(new double[] { 0, 0, -2 }, new double[] { 0, 1, 2 }, 2)]
        [TestCase(new double[] { 0, -1, -2 }, new double[] { 0, 1, 1 }, 2)]
        public void Capped_cylinder_tests(double[] point, double[] dir, int count)
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            cyl.Closed = true;
            var direct = Normalize(Vector(dir[0], dir[1], dir[2]));
            var ray = new Ray(Point(point[0], point[1], point[2]), direct);
            //Act
            var xs = cyl.Intersect(ray);
            //Assert
            xs.Count.Should().Be(count);
        }
        /*
         * Scenario Outline: The normal vector on a cylinder's end caps
           Given cyl ← cylinder()
           And cyl.minimum ← 1
           And cyl.maximum ← 2
           And cyl.closed ← true
           When n ← local_normal_at(cyl, <point>)
           Then n = <normal>
           Examples:
           |      point       |     normal       |
           | point(0, 1, 0)   | vector(0, -1, 0) |
           | point(0.5, 1, 0) | vector(0, -1, 0) |
           | point(0, 1, 0.5) | vector(0, -1, 0) |
           | point(0, 2, 0)   | vector(0, 1, 0)  |
           | point(0.5, 2, 0) | vector(0, 1, 0)  |
           | point(0, 2, 0.5) | vector(0, 1, 0)  |
         */
        [TestCase(new double[] { 0, 1, 0 },   new double[] { 0, -1, 0 })]
        [TestCase(new double[] { 0.5, 1, 0 },  new double[] { 0, -1, 0 })]
        [TestCase(new double[] { 0, 1, 0.5 },  new double[] { 0, -1, 0 })]
        [TestCase(new double[] { 0, 2, 0 },  new double[] { 0, 1, 0 })]
        [TestCase(new double[] { 0.5, 2, 0 }, new double[] { 0, 1, 0 })]
        [TestCase(new double[] { 0, 2, 0.5 }, new double[] { 0, 1, 0 })]
        public void Capped_cylinder_normal_tests(double[] point, double[] norm)
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            cyl.Closed = true;
            //Act
            var expected = Vector(norm[0], norm[1], norm[2]);
            var result = cyl.NormalAt(Point(point[0], point[1], point[2]));
            
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
