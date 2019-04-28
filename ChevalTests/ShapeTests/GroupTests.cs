using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ShapeTests
{
    public class GroupTests
    {
        /*
 * Scenario: Creating a new group
   Given g ← group()
   Then g.transform = identity_matrix
   And g is empty
   */

        [Test]
        public void Empty_group_test()
        {
            //Assign
            var group = new Group();
            //Assert
            group.Transform.Should().BeEquivalentTo(Transform.IdentityMatrix);
            group.Should().BeEmpty();
        }

        /*
         * Scenario: Adding a child to a group
           Given g ← group()
           And s ← test_shape()
           When add_child(g, s)
           Then g is not empty
           And g includes s
           And s.parent = g
         */
        [Test]
        public void Add_child_to_group()
        {
            //Assign
            var group = new Group();
            var s = new Sphere();

            //Act
            group.Add(s);
            //Assert
            group.Should().NotBeEmpty();
            group.Should().Contain(s);
            s.Parent.Should().BeEquivalentTo(group);
        }
        /*
         * Scenario: Intersecting a ray with an empty group
           Given g ← group()
           And r ← ray(point(0, 0, 0), vector(0, 0, 1))
           When xs ← local_intersect(g, r)
           Then xs is empty
         */
        [Test]
        public void Ray_misses_group()
        {
            //Assign
            var group = new Group();
            var ray = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
            //Act
            var xs = group.Intersect(ray);
            //Assert
            xs.Should().BeEmpty();
        }
        /*

         * Scenario: Intersecting a ray with a nonempty group
           Given g ← group()
           And s1 ← sphere()
           And s2 ← sphere()
           And set_transform(s2, translation(0, 0, -3))
           And s3 ← sphere()
           And set_transform(s3, translation(5, 0, 0))
           And add_child(g, s1)
           And add_child(g, s2)
           And add_child(g, s3)
           When r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And xs ← local_intersect(g, r)
           Then xs.count = 4
           And xs[0].object = s2
           And xs[1].object = s2
           And xs[2].object = s1
           And xs[3].object = s1
         */
        [Test]
        public void Ray_intersects_group_correctly()
        {
            //Assign
            var group = new Group();

            var sphere1 = new Sphere();
            var sphere2 = new Sphere
            {
                Transform = Transform.Translation(0, 0, -3)
            };
            var sphere3 = new Sphere
            {
                Transform = Transform.Translation(5, 0, 0)
            };

            group.Add(sphere1);
            group.Add(sphere2);
            group.Add(sphere3);
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            //Act
            var xs = group.Intersect(ray);
            //Assert
            xs.Should().HaveCount(4);
            xs[0].Object.Should().BeEquivalentTo(sphere2);
            xs[1].Object.Should().BeEquivalentTo(sphere2);
            xs[2].Object.Should().BeEquivalentTo(sphere1);
            xs[3].Object.Should().BeEquivalentTo(sphere1);
        }
        /*
         * Scenario: Intersecting a transformed group
           Given g ← group()
           And set_transform(g, scaling(2, 2, 2))
           And s ← sphere()
           And set_transform(s, translation(5, 0, 0))
           And add_child(g, s)
           When r ← ray(point(10, 0, -10), vector(0, 0, 1))
           And xs ← intersect(g, r)
           Then xs.count = 2
         */
        [Test]
        public void Intersecting_transformed_group()
        {
            //Assign
            var group = new Group();
            group.Transform = Transform.Scaling(2, 2, 2);

            var sphere = new Sphere
            {
                Transform = Transform.Translation(5, 0, 0)
            };

            group.Add(sphere);
            var ray = new Ray(Point(10, 0, -10), Vector(0, 0, 1));
            //Act
            var xs = group.Intersect(ray);
            //Assert
            xs.Should().HaveCount(2);
        }
        /*
         * Scenario: Converting a point from world to object space
           Given g1 ← group()
           And set_transform(g1, rotation_y(π/2))
           And g2 ← group()
           And set_transform(g2, scaling(2, 2, 2))
           And add_child(g1, g2)
           And s ← sphere()
           And set_transform(s, translation(5, 0, 0))
           And add_child(g2, s)
           When p ← world_to_object(s, point(-2, 0, -10))
           Then p = point(0, 0, -1)
         */

    }
}
