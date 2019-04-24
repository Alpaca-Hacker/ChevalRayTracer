

using System;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;
using Vector = System.Numerics.Vector;

namespace ChevalTests.ShapeTests
{
    public class CubeTests
    {
        /*
         * Scenario Outline: A ray intersects a cube
           Given c ← cube()
           And r ← ray(<origin>, <direction>)
           When xs ← local_intersect(c, r)
           Then xs.count = 2
           And xs[0].t = <t1>
           And xs[1].t = <t2>
           Examples:
           |    | origin           | direction        | t1| t2|
           | +x | point(5, 0.5, 0) | vector(-1, 0, 0) | 4 | 6 |
           | -x | point(-5, 0.5, 0)| vector(1, 0, 0)  | 4 | 6 |
           | +y | point(0.5, 5, 0) | vector(0, -1, 0) | 4 | 6 |
           | -y | point(0.5, -5, 0)| vector(0, 1, 0)  | 4 | 6 |
           | +z | point(0.5, 0, 5) | vector(0, 0, -1) | 4 | 6 |
           | -z | point(0.5, 0, -5)| vector(0, 0, 1)  | 4 | 6 |
           | in | point(0, 0.5, 0) | vector(0, 0, 1)  | -1| 1 |
         */
        [TestCase(new double[] { 5, 0.5, 0 }, new double[] { -1, 0, 0 }, 4, 6)]
        [TestCase(new double[] { -5, 0.5, 0 }, new double[] { 1, 0, 0 }, 4, 6)]
        [TestCase(new double[] { 0.5, 5, 0 }, new double[] { 0, -1, 0 }, 4, 6)]
        [TestCase(new double[] { 0.5, -5, 0 }, new double[] { 0, 1, 0 }, 4, 6)]
        [TestCase(new double[] { 0.5, 0, 5 }, new double[] { 0, 0, -1 }, 4, 6)]
        [TestCase(new double[] { 0.5, 0, -5 }, new double[] { 0, 0, 1 }, 4, 6)]
        [TestCase(new double[] { 0, 0.5, 0 }, new double[] { 0, 0, 1 }, -1, 1)]
        public void Cube_ray_tests(double[] point, double[] dir, double t1, double t2) {
            //Assign
            var cube = new Cube();
            var ray = new Ray(Point(point[0], point[1], point[2]),
                Vector(dir[0], dir[1], dir[2]));
            
            //Act
            var xs = cube.Intersect(ray);
            
            //Assert
            xs[0].T.Should().Be(t1);
            xs[1].T.Should().Be(t2);
        }
        //Scenario Outline: A ray misses a cube
        // Given c ← cube()
        // And r ← ray(<origin>, <direction>)
        // When xs ← local_intersect(c, r)
        // Then xs.count = 0
        // Examples:
        // |      origin     |        direction               |
        // | point(-2, 0, 0) | vector(0.2673, 0.5345, 0.8018) |
        // | point(0, -2, 0) | vector(0.8018, 0.2673, 0.5345) |
        // | point(0, 0, -2) | vector(0.5345, 0.8018, 0.2673) |
        // | point(2, 0, 2)  | vector(0, 0, -1)               |
        // | point(2, 0, 2)  | vector(0, -1, 0)               |
        // | point(2, 2, 0)  | vector(-1, 0, 0)               |
        [TestCase(new double[] { -2, 0, 0 }, new double[] { 0.2673, 0.5345, 0.8018 })]
        [TestCase(new double[] { 0, -2, 0 }, new double[] { 0.8018, 0.2673, 0.5345 })]
        [TestCase(new double[] { 0, 0, -2 }, new double[] { 0.5345, 0.8018, 0.2673 })]
        [TestCase(new double[] { 2, 0, 2 }, new double[] { 0, 0, -1 })]
        [TestCase(new double[] { 2, 0, 2 }, new double[] { 0, -1, 0 })]
        [TestCase(new double[] { 2, 2, 0 }, new double[] { -1, 0, 0 })]

        public void Cube_ray_missing_tests(double[] point, double[] dir)
        {
            //Assign
            var cube = new Cube();
            var ray = new Ray(Point(point[0], point[1], point[2]),
                Vector(dir[0], dir[1], dir[2]));

            //Act
            var xs = cube.Intersect(ray);

            //Assert
            xs.Should().BeEmpty();
        }
        //Scenario Outline: The normal on the surface of a cube
        // Given c ← cube()
        // And p ← <point>
        // When normal ← local_normal_at(c, p)
        // Then normal = <normal>
        // Examples:
        // | point | normal |
        // | point(1, 0.5, -0.8) | vector(1, 0, 0) |
        // | point(-1, -0.2, 0.9) | vector(-1, 0, 0) |
        // | point(-0.4, 1, -0.1) | vector(0, 1, 0) |
        // | point(0.3, -1, -0.7) | vector(0, -1, 0) |
        // | point(-0.6, 0.3, 1) | vector(0, 0, 1) |
        // | point(0.4, 0.4, -1) | vector(0, 0, -1) |
        // | point(1, 1, 1) | vector(1, 0, 0) |
        // | point(-1, -1, -1) | vector(-1, 0, 0) |

        [TestCase(new double[] {1, 0.5, -0.8}, new double[] {1, 0, 0})]
        [TestCase(new double[] {-1, -0.2, 0.9}, new double[] {-1, 0, 0})]
        [TestCase(new double[] {-0.4, 1, -0.1}, new double[] {0, 1, 0})]
        [TestCase(new double[] {0.3, -1, -0.7}, new double[] {0, -1, 0})]
        [TestCase(new double[] {-0.6, 0.3, 1}, new double[] {0, 0, 1})]
        [TestCase(new double[] {0.4, 0.4, -1}, new double[] {0, 0, -1})]
        [TestCase(new double[] {1, 1, 1}, new double[] {1, 0, 0})]
        [TestCase(new double[] {-1, -1, -1}, new double[] {-1, 0, 0})]
        public void Cube_normal_tests(double[] point, double[] vector)
        {
            //Assign
            var cube = new Cube();
            var p = Point(point[0], point[1], point[2]);
            //Act
            var result = cube.NormalAt(p);
            var expected = Vector(vector[0], vector[1], vector[2]);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
