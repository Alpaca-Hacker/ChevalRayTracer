using System;
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
        [Test]
        public void Converting_point_from_world_to_object_space()
        {
            //Assign
            var group1 = new Group
            {
                Transform = Transform.RotationY(Math.PI / 2)
            };
            var group2 = new Group
            {
                Transform = Transform.Scaling(2, 2, 2)
            };
            group1.Add(group2);
            var sphere = new Sphere
            {
                Transform = Transform.Translation(5, 0, 0)
            };
            group2.Add(sphere);
            //Act
            var result = sphere.WorldToObject(Point(-2, 0, -10));
            var expected = Point(0, 0, -1);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario: Converting a normal from object to world space
          Given g1 ← group()
          And set_transform(g1, rotation_y(π/2))
          And g2 ← group()
          And set_transform(g2, scaling(1, 2, 3))
          And add_child(g1, g2)
          And s ← sphere()
          And set_transform(s, translation(5, 0, 0))
          And add_child(g2, s)
          When n ← normal_to_world(s, vector(√3/3, √3/3, √3/3))
          Then n = vector(0.2857, 0.4286, -0.8571)
         */
        [Test]
        public void Convert_Normal_From_Object_Space_To_World_Space()
        {
            //Assign
            var g1 = new Group
            {
                Transform = Transform.RotationY(Math.PI / 2),
            };

            var g2 = new Group
            {
                Transform = Transform.Scaling(1, 2, 3),
            };

            g1.Add(g2);

            var s = new Sphere
            {
                Transform = Transform.Translation(5, 0, 0),
            };

            g2.Add(s);

            var v = Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);
            //Act
            var result = s.NormalToWorld(v);
            var expected = Vector(0.2857, 0.4286, -0.8571);
            //Assert
            Math.Round(result.X, 4).Should().Be(expected.X);
            Math.Round(result.Y, 4).Should().Be(expected.Y);
            Math.Round(result.Z, 4).Should().Be(expected.Z);
        }
        /*
         * Scenario: Finding the normal on a child object
           Given g1 ← group()
           And set_transform(g1, rotation_y(π/2))
           And g2 ← group()
           And set_transform(g2, scaling(1, 2, 3))
           And add_child(g1, g2)
           And s ← sphere()
           And set_transform(s, translation(5, 0, 0))
           And add_child(g2, s)
           When n ← normal_at(s, point(1.7321, 1.1547, -5.5774))
           Then n = vector(0.2857, 0.4286, -0.8571)
         */
        [Test]
        public void Find_Normal_On_Child()
        {
            //Assign
            var g1 = new Group()
            {
                Transform = Transform.RotationY(Math.PI / 2),
            };

            var g2 = new Group()
            {
                Transform = Transform.Scaling(1, 2, 3),
            };

            g1.Add(g2);

            var s = new Sphere()
            {
                Transform = Transform.Translation(5, 0, 0),
            };

            g2.Add(s);

            var p = Point(1.7321, 1.1547, -5.5774);
            //Act
            var result = s.NormalAt(p);
            var expected = Vector(0.2857, 0.4285, -0.8572);
            //Assert
            Math.Round(result.X, 4).Should().Be(expected.X);
            Math.Round(result.Y, 4).Should().Be(expected.Y);
            Math.Round(result.Z, 4).Should().Be(expected.Z);
        }
        // Tests taken from Pixie.net (https://github.com/basp/pixie.net)

        [Test]
        public void Group_Has_BoundingBox_That_Contains_Its_Children()
        {
            //Assign
            var sphere = new Sphere()
            {
                Transform =
                    Transform.Translation(2, 5, -3) *
                    Transform.Scaling(2, 2, 2),
            };

            var cyl = new Cylinder()
            {
                Minimum = -2,
                Maximum = 2,
                Transform =
                    Transform.Translation(-4, -1, 4) *
                    Transform.Scaling(0.5, 1, 0.5),
            };

            var group = new Group {sphere, cyl};
            //Act
            var box = group.Bounds();
            var minExpected = Point(-4.5, -3, -5);
            var maxExpected = Point(4, 7, 4.5);
            //Assert
            box.Min.Should().BeEquivalentTo(minExpected);
            box.Max.Should().BeEquivalentTo(maxExpected);
            
        }
    }
}
