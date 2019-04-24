

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
        //PG check below
        [TestCase(new double[] { 0.5, 0, 5 }, new double[] { -1, 0, 0 }, 4, 6)]
        [TestCase(new double[] { 5, 0.5, 0 }, new double[] { -1, 0, 0 }, 4, 6)]
        [TestCase(new double[] { 5, 0.5, 0 }, new double[] { -1, 0, 0 }, 4, 6)]
        public void Cube_ray_tests(double[] point, double[] dir, double t1, double t2) {
            //Assign
            var cube = new Cube();
            var ray = new Ray(Point(point[0], point[1], point[2]),
                Vector(dir[0], dir[1], dir[2]));
            
            //Act
            var xs = cube.Intersect(ray);
            
            //Assert
            xs[0].Should().Be(t1);
            xs[1].Should().Be(t2);

        }

    }
}
