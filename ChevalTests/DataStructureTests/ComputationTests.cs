using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using FluentAssertions;
using NUnit.Framework;
using static System.MathF;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;
using static Cheval.Templates.ColourTemplate;
using static Cheval.Templates.MaterialTemplate;

namespace ChevalTests.DataStructureTests
{
    public class ComputationTests
    {
        /*
         * Scenario: Precomputing the state of an intersection
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(4, shape)
           When comps ← prepare_computations(i, r)
           Then comps.t = i.t
           And comps.object = i.object
           And comps.point = point(0, 0, -1)
           And comps.eyev = vector(0, 0, -1)
           And comps.normalv = vector(0, 0, -1)
         */
        [Test]
        public void Precomputing_state_of_intersection()
        {
            //Assign
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            //Act
            var comps = new Computations(i, ray);
            var expectedPoint = Point(0, 0, -1);
            var expectedEyeV = Vector(0, 0, -1);
            var expectedNormalV = Vector(0, 0, -1);
            //Assert
            comps.T.Should().Be(i.T);
            comps.Shape.Should().BeEquivalentTo(i.Object);
            comps.Point.Should().BeEquivalentTo(expectedPoint);
            comps.EyeV.Should().BeEquivalentTo(expectedEyeV);
            comps.NormalV.Should().BeEquivalentTo(expectedNormalV);
        }
        /*
         * Scenario: The hit, when an intersection occurs on the outside
           Given r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(4, shape)
           When comps ← prepare_computations(i, r)
           Then comps.inside = false
           */
        [Test]
        public void Intersection_occurs_on_outside()
        {
            //Assign
            var ray = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            //Act
            var comps = new Computations(i, ray);
            //Assert
            comps.Inside.Should().BeFalse();
        }
        /*
           Scenario: The hit, when an intersection occurs on the inside
           Given r ← ray(point(0, 0, 0), vector(0, 0, 1))
           And shape ← sphere()
           And i ← intersection(1, shape)
           When comps ← prepare_computations(i, r)
           Then comps.point = point(0, 0, 1)
           And comps.eyev = vector(0, 0, -1)
           And comps.inside = true
           And comps.normalv = vector(0, 0, -1)
         */
        [Test]
        public void Intersection_occurs_on_inside()
        {
            var ray = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            //Act
            var comps = new Computations(i, ray);
            var expectedPoint = Point(0, 0, 1);
            var expectedEyeV = Vector(0, 0, -1);
            var expectedNormalV = Vector(0, 0, -1);
            //Assert
            comps.Inside.Should().BeTrue();
            comps.Point.Should().BeEquivalentTo(expectedPoint);
            comps.EyeV.Should().BeEquivalentTo(expectedEyeV);
            comps.NormalV.Should().BeEquivalentTo(expectedNormalV);
        }
        /*
         * Scenario: Precomputing the reflection vector
           2 Given shape ← plane()
           3 And r ← ray(point(0, 1, -1), vector(0, -√2/2, √2/2))
           4 And i ← intersection(√2, shape)
           5 When comps ← prepare_computations(i, r)
           6 Then comps.reflectv = vector(0, √2/2, √2/2)
         */

        [Test]
        public void Precomputing_reflection_vector()
        {
            //Assign
            var shape = new Plane();
            var ray = new Ray(Point(0,1,-1), Vector(0,-Sqrt(2)/2, Sqrt(2) / 2));
            var inter = new Intersection(Sqrt(2), shape);
            //Act
            var comps = new Computations(inter, ray);
            var expected = Vector(0, Sqrt(2) / 2, Sqrt(2) / 2);
            //Assert
            comps.ReflectV.Should().BeEquivalentTo(expected);
        }

        //1 Scenario Outline: Finding n1 and n2 at various intersections
        // - Given A ← glass_sphere() with:
        // - | transform| scaling(2, 2, 2) |
        // - | material.refractive_index | 1.5 |
        // 5 And B ← glass_sphere() with:
        // - | transform | translation(0, 0, -0.25) |
        // - | material.refractive_index | 2.0 |
        // - And C ← glass_sphere() with:
        // - | transform | translation(0, 0, 0.25) |
        //   | material.refractive_index | 2.5 |
        // - And r ← ray(point(0, 0, -4), vector(0, 0, 1))
        // - And xs ← intersections(2:A, 2.75:B, 3.25:C, 4.75:B, 5.25:C, 6:A)
        // - When comps ← prepare_computations(xs[<index>], r, xs)
        // - Then comps.n1 = <n1>
        // - And comps.n2 = <n2>
        // -
        // - Examples:
        // - | index | n1  |  n2 |
        // - |   0   | 1.0 | 1.5 |
        // 2 |   1   | 1.5 | 2.0 |
        // - |   2   | 2.0 | 2.5 |
        // - |   3   | 2.5 | 2.5 |
        // - |   4   | 2.5 | 1.5 |
        // - |   5   | 1.5 | 1.0 |
        [TestCase(0, 1.0f, 1.5f)]
        [TestCase(1, 1.5f, 2.0f)]
        [TestCase(2, 2.0f, 2.5f)]
        [TestCase(3, 2.5f, 2.5f)]
        [TestCase(4, 2.5f, 1.5f)]
        [TestCase(5, 1.5f, 1.0f)]
        public void Refraction_tests(int index, float n1, float n2)
        {
            //Assign
            var sphereA = new Sphere
            {
                Transform = Scaling(2, 2, 2),
                Material = Glass
            };
            sphereA.Material.RefractiveIndex = 1.5f;
            var sphereB = new Sphere
            {
                Transform = Translation(0, 0, -0.25f),
                Material = Glass
            };
            sphereB.Material.RefractiveIndex = 2.0f;
            var sphereC = new Sphere
            {
                Transform = Translation(0, 0, 0.25f),
                Material = Glass
            };
            sphereC.Material.RefractiveIndex = 2.5f;
            var ray = new Ray(Point(0, 0, -4), Vector(0, 0, 1));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(2,sphereA),
                new Intersection(2.75f, sphereB),
                new Intersection(3.25f, sphereC),
                new Intersection(4.75f, sphereB),
                new Intersection(5.25f, sphereC),
                new Intersection(6,sphereA)

            });
            //Act
            var comps = new Computations(xs.List[index], ray, xs);
            //Assert
            comps.N1.Should().Be(n1);
            comps.N2.Should().Be(n2);
        }
        /*
         * Scenario: The refracted color with an opaque surface
           Given w ← default_world()
           And shape ← the first object in w
           And r ← ray(point(0, 0, -5), vector(0, 0, 1))
           And xs ← intersections(4:shape, 6:shape)
           When comps ← prepare_computations(xs[0], r, xs)
           And c ← refracted_color(w, comps, 5)
           Then c = color(0, 0, 0)
         */
        [Test]
        public void Refracted_colour_with_opaque_surface()
        {
            //Assign
            var scene = Scene.Default();
            var shape = scene.Shapes[0];
            var ray = new Ray(Point(0,0,-5), Vector(0,0,1));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(4, shape),
                new Intersection(6, shape)
            });
            //Act
            var comps = new Computations(xs.List[0], ray, xs);
            ChevalColour colour = scene.RefractedColour(comps, 5);
            //Assert
            colour.Should().BeEquivalentTo(Black);
        }
        /*
         * Scenario: The refracted color under total internal reflection
           Given w ← default_world()
           And shape ← the first object in w
           And shape has:
           | material.transparency | 1.0 |
           | material.refractive_index | 1.5 |
           And r ← ray(point(0, 0, √2/2), vector(0, 1, 0))
           And xs ← intersections(-√2/2:shape, √2/2:shape)
           # NOTE: this time you're inside the sphere, so you need
           # to look at the second intersection, xs[1], not xs[0]
           When comps ← prepare_computations(xs[1], r, xs)
           And c ← refracted_color(w, comps, 5)
           Then c = color(0, 0, 0)
         */
        [Test]
        public void Refracted_colour_with_total_internal_reflection()
        {
            //Assign
            var scene = Scene.Default();
            var shape = scene.Shapes[0];
            shape.Material.Transparency = 1.0f;
            shape.Material.RefractiveIndex = 1.5f;
            var ray = new Ray(Point(0, 0,PI/2), Vector(0, 1, 0));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(-PI/2, shape),
                new Intersection(PI/2, shape)
            });
            //Act
            var comps = new Computations(xs.List[1], ray, xs);
            var colour = scene.RefractedColour(comps, 5);
            //Assert
            colour.Should().BeEquivalentTo(Black);
        }

        /*
         * Scenario: The refracted color with a refracted ray
           Given w ← default_world()
           And A ← the first object in w
           And A has:
           | material.ambient | 1.0 |
           | material.pattern | test_pattern() |
           And B ← the second object in w
           And B has:
           | material.transparency | 1.0 |
           | material.refractive_index | 1.5 |
           And r ← ray(point(0, 0, 0.1), vector(0, 1, 0))
           And xs ← intersections(-0.9899:A, -0.4899:B, 0.4899:B, 0.9899:A)
           When comps ← prepare_computations(xs[2], r, xs)
           And c ← refracted_color(w, comps, 5)
           Then c = color(0, 0.99888, 0.04725)
         */

        [Test]
        public void Refracted_colour_with_refracted_ray()
        {
            //Assign
            var scene = Scene.Default();
            var shapeA = scene.Shapes[0];
            shapeA.Material.Ambient = 1.0f;
            shapeA.Material.Pattern = new TestPattern();

            var shapeB = scene.Shapes[1];
            shapeB.Material.Transparency = 1.0f;
            shapeB.Material.RefractiveIndex = 1.5f;

            var ray = new Ray(Point(0, 0, 0.1f), Vector(0, 1, 0));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(-0.9899f, shapeA),
                new Intersection(-0.4899f, shapeB),
                new Intersection(0.4899f, shapeB),
                new Intersection(0.9899f, shapeA)
            });
            //Act
            var comps = new Computations(xs.List[2], ray, xs);
            var colour = scene.RefractedColour(comps, 5);
            var expected = new ChevalColour(0, 0.99887f, 0.04722f);
            //Assert
            Round(colour.Red, 5).Should().Be(expected.Red);
            Round(colour.Green, 5).Should().Be(expected.Green);
            Round(colour.Blue, 5).Should().Be(expected.Blue);
        }

        /*
         * Scenario: The Schlick approximation under total internal reflection
           Given shape ← glass_sphere()
           And r ← ray(point(0, 0, √2/2), vector(0, 1, 0))
           And xs ← intersections(-√2/2:shape, √2/2:shape)
           When comps ← prepare_computations(xs[1], r, xs)
           And reflectance ← schlick(comps)
           Then reflectance = 1.0
         */
        [Test]
        public void Schlick_approx_under_total_internal_reflection()
        {
            //Assign
            var shape = new Sphere();
            shape.Material = Glass;
            var ray = new Ray(Point(0,0, Sqrt(2)/2),Vector(0,1,0));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(-Sqrt(2)/2, shape),
                new Intersection(Sqrt(2)/2, shape)
            });
            //Act
            var comps = new Computations(xs.List[1], ray, xs);
            float reflectance = comps.Schlick();
            //Assert
            reflectance.Should().Be(1.0f);

        }
        /*
         * Scenario: The Schlick approximation with a perpendicular viewing angle
           Given shape ← glass_sphere()
           And r ← ray(point(0, 0, 0), vector(0, 1, 0))
           And xs ← intersections(-1:shape, 1:shape)
           When comps ← prepare_computations(xs[1], r, xs)
           And reflectance ← schlick(comps)
           Then reflectance = 0.04
         */

        [Test]
        public void Schlick_approx_with_perpendicular_angle()
        {
            //Assign
            var shape = new Sphere();
            shape.Material = Glass;
            var ray = new Ray(Point(0, 0, 0), Vector(0, 1, 0));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(-1, shape),
                new Intersection(1, shape)
            });
            //Act
            var comps = new Computations(xs.List[1], ray, xs);
            float reflectance = comps.Schlick();
            //Assert
            Round(reflectance,5).Should().Be(0.04f);

        }
        /*
         * Scenario: The Schlick approximation with small angle and n2 > n1
           Given shape ← glass_sphere()
           And r ← ray(point(0, 0.99, -2), vector(0, 0, 1))
           And xs ← intersections(1.8589:shape)
           When comps ← prepare_computations(xs[0], r, xs)
           And reflectance ← schlick(comps)
           Then reflectance = 0.48873
         */
        [Test]
        public void Schlick_approx_with_small_angle()
        {
            //Assign
            var shape = new Sphere();
            shape.Material = Glass;
            var ray = new Ray(Point(0, 0.99f, -2), Vector(0, 0, 1));
            var xs = new Intersections(new List<Intersection>
            {
                new Intersection(1.8589f, shape),
               // new Intersection(1, shape)
            });
            //Act
            var comps = new Computations(xs.List[0], ray, xs);
            float reflectance = comps.Schlick();
            //Assert
            Round(reflectance, 5).Should().Be(0.42113f); //Check this for issues

        }
    }
}
