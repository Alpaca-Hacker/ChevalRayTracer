using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.DataStructureTests
{
    public class IntersectionTests
    {
        /*
         * Scenario: An intersection encapsulates t and object
           Given s ← sphere()
           When i ← intersection(3.5, s)
           Then i.t = 3.5
           And i.object = s
         */

        [Test]
        public void Intersection_test()
        {
            //Assign
            var s = new Sphere();
            //Act
            var i = new Intersection(3.5f, s);
            //Assert
            i.T.Should().Be(3.5f);
            i.Object.Should().BeEquivalentTo(s);
        }
        /*
         * Scenario: Aggregating intersections
           Given s ← sphere()
           And i1 ← intersection(1, s)
           And i2 ← intersection(2, s)
           When xs ← intersections(i1, i2)
           Then xs.count = 2
           And xs[0].t = 1
           And xs[1].t = 2
         */
        [Test]
        public void Aggregation_interactions_test()
        {
            //Assign
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2,s);
            //Act
            var xs = new Intersections(new List<Intersection>{i1, i2});
            //Assert
            xs.List.Should().HaveCount(2);
            xs.List[0].T.Should().Be(1);
            xs.List[1].T.Should().Be(2);
        }
        /*
         * Scenario: The hit, when all intersections have positive t
           Given s ← sphere()
           And i1 ← intersection(1, s)
           And i2 ← intersection(2, s)
           And xs ← intersections(i2, i1)
           When i ← hit(xs)
           Then i = i1
           */
        [Test]
        public void Hit_when_all_intersections_positive()
        {
            //Assign
            var s = new Sphere();
            var i1 = new Intersection(1,s);
            var i2 = new Intersection(2,s);
            var xs = new Intersections(new List<Intersection>{i1, i2});
            //Act
            Intersection hit = xs.Hit();
            //Assert
            hit.Should().BeEquivalentTo(i1);
        }
        /*
           Scenario: The hit, when some intersections have negative t
           Given s ← sphere()
           And i1 ← intersection(-1, s)
           And i2 ← intersection(1, s)
           And xs ← intersections(i2, i1)
           When i ← hit(xs)
           Then i = i2
            */
        [Test]
        public void Hit_when_sme_intersections_are_negative()
        {
            //Assign
            var s = new Sphere();
            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(1, s);
            var xs = new Intersections(new List<Intersection> { i2, i1 });
            //Act
            Intersection hit = xs.Hit();
            //Assert
            hit.Should().BeEquivalentTo(i2);
        }
        /*
         Scenario: The hit, when all intersections have negative t
         Given s ← sphere()
         And i1 ← intersection(-2, s)
         And i2 ← intersection(-1, s)
         And xs ← intersections(i2, i1)
         When i ← hit(xs)
         Then i is nothing
        */
        [Test]
        public void Hit_when_all_intersections_negative()
        {
            //Assign
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = new Intersections(new List<Intersection> { i1, i2 });
            //Act
            Intersection hit = xs.Hit();
            //Assert
            hit.Should().BeNull();
        }
        /*
        Scenario: The hit is always the lowest nonnegative intersection
        Given s ← sphere()
        And i1 ← intersection(5, s)
        And i2 ← intersection(7, s)
        And i3 ← intersection(-3, s)
        And i4 ← intersection(2, s)
        And xs ← intersections(i1, i2, i3, i4)
        When i ← hit(xs)
        Then i = i4
      */
        [Test]
        public void Hit_is_always_lowest_nonnegative_intersection()
        {
            //Assign
            var s = new Sphere();
            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);
            var xs = new Intersections(new List<Intersection> { i1, i2, i3, i4 });
            //Act
            Intersection hit = xs.Hit();
            //Assert
            hit.Should().BeEquivalentTo(i4);
        }
        /*
         * Scenario: An intersection can encapsulate `u` and `v`
           Given s ← triangle(point(0, 1, 0), point(-1, 0, 0), point(1, 0, 0))
           When i ← intersection_with_uv(3.5, s, 0.2, 0.4)
           Then i.u = 0.2
           And i.v = 0.4
         */
        [Test]
        public void Intersection_has_u_and_v()
        {
            //Assign
            var shape = new Triangle(Point(0,1,0), Point(-1, 0, 0), Point(1, 0, 0));
            //Act
            var i = new Intersection(3.5f,shape,0.2f, 0.4f);
            //Assert
            i.U.Should().Be(0.2f);
            i.V.Should().Be(0.4f);
        }
    }
}
