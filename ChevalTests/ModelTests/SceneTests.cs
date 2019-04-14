using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests.ModelTests
{
    public class SceneTests
    {
        /*
         * Scenario: Creating a world
           Given w ← world()
           Then w contains no objects
           And w has no light source
         */
        [Test]
        public void Creating_scene()
        {
            //Assign
            var w = new Scene();
            //Assert
            w.Lights.Should().BeEmpty();
            w.Shapes.Should().BeEmpty();
        }

        /*
         * Scenario: The default world
           Given light ← point_light(point(-10, 10, -10), color(1, 1, 1))
           And s1 ← sphere() with:
           | material.color | (0.8, 1.0, 0.6) |
           | material.diffuse | 0.7 |
           | material.specular | 0.2 |
           And s2 ← sphere() with:
           | transform | scaling(0.5, 0.5, 0.5) |
           When w ← default_world()
           Then w.light = light
           And w contains s1
           And w contains s2
         */
        [Test]
        public void Default_scene()
        {
            //Assign
            var light = Light.PointLight(Point(-10, 10, -10), new ChevalColour(1, 1, 1));
            var sphere1 = new Sphere
            {
                Material =
                {
                    Colour = new ChevalColour(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            };
            var sphere2 = new Sphere
            {
                Transform = Transform.Scaling(0.5, 0.5, 0.5)
            };
            //Act
            var result = Scene.Default();
            //Assert
            result.Lights[0].Should().BeEquivalentTo(light);
            result.Shapes.Should().HaveCount(2);
            result.Shapes[0].Material.Should().BeEquivalentTo(sphere1.Material);
            result.Shapes[1].Transform.Should().BeEquivalentTo(sphere2.Transform);
        }
        /*
         * Scenario: Intersect a world with a ray
           Given w ← default_world()
           And r ← ray(point(0, 0, -5), vector(0, 0, 1))
           When xs ← intersect_world(w, r)
           Then xs.count = 4
           And xs[0].t = 4
           And xs[1].t = 4.5
           And xs[2].t = 5.5
           And xs[3].t = 6
         */
        [Test]
        public void Intersect_scene_with_a_ray()
        {
            //Assign
            var scene = Scene.Default();
            var ray= new Ray(Point(0,0,-5),Vector(0,0,1));
            //Act
            var xs = ray.Intersect(scene);
            //Assert
            xs.Should().HaveCount(4);
            xs[0].T.Should().Be(4);
            xs[1].T.Should().Be(4.5);
            xs[2].T.Should().Be(5.5);
            xs[3].T.Should().Be(6);
        }
        /*
         * Scenario: There is no shadow when nothing is collinear with point and light
           Given w ← default_world()
           And p ← point(0, 10, 0)
           Then is_shadowed(w, p) is false
         */
        [Test]
        public void No_shadow_when_nothing_is_collinear_with_point_and_light()
        {
            //Assign
            var w = Scene.Default();
            var p = Point(0, 10, 0);
            //Assert
            w.IsShadowed(p, w.Lights[0]).Should().BeFalse();
        }
        /*
         * Scenario: The shadow when an object is between the point and the light
           Given w ← default_world()
           And p ← point(10, -10, 10)
           Then is_shadowed(w, p) is true
         */
        [Test]
        public void Shadow_when_object_is_collinear_with_point_and_light()
        {
            //Assign
            var w = Scene.Default();
            var p = Point(10, -10, 10);
            //Assert
            w.IsShadowed(p,w.Lights[0]).Should().BeTrue();
        }
        /*
         * Scenario: There is no shadow when an object is behind the light
           Given w ← default_world()
           And p ← point(-20, 20, -20)
           Then is_shadowed(w, p) is false
         */
        [Test]
        public void No_shadow_when_object_is_behind_light()
        {
            //Assign
            var w = Scene.Default();
            var p = Point(-20, 20, -20);
            //Assert
            w.IsShadowed(p, w.Lights[0]).Should().BeFalse();
        }
        /*
         *Scenario: There is no shadow when an object is behind the point
           Given w ← default_world()
           And p ← point(-2, 2, -2)
           Then is_shadowed(w, p) is false
         */

        [Test]
        public void Shadow_when_object_is_behind_point()
        {
            //Assign
            var w = Scene.Default();
            var p = Point(-2, 2, -2);
            //Assert
            w.IsShadowed(p,w.Lights[0]).Should().BeFalse();
        }
        /*
         * Scenario: shade_hit() is given an intersection in shadow
           Given w ← world()
           And w.light ← point_light(point(0, 0, -10), color(1, 1, 1))
           And s1 ← sphere()
           And s1 is added to w
           And s2 ← sphere() with:
           | transform | translation(0, 0, 10) |
           And s2 is added to w
           And r ← ray(point(0, 0, 5), vector(0, 0, 1))
           And i ← intersection(4, s2)
           When comps ← prepare_computations(i, r)
           And c ← shade_hit(w, comps)
           Then c = color(0.1, 0.1, 0.1)
         */
        [Test]
        public void ShadeHit_is_given_an_intersection_in_shadow()
        {
            var scene = new Scene();
            scene.Lights.Add(Light.PointLight(Point(0,0,-1), new ChevalColour(1,1,1)));
            var s1 = new Sphere();
            scene.Shapes.Add(s1);
            var s2 = new Sphere
            {
                Transform = Transform.Translation(0, 0, 10)
            };
            scene.Shapes.Add(s2);
            var ray = new Ray(Point(0,0,5), Vector(0,0,1));
            var inter = new Intersection(4, s2);
            //Act
            var comps = new Computations(inter, ray);
            var result = scene.ShadeHit(comps);
            var expected = new ChevalColour(0.1,0.1,0.1);
            //Assert
            result.Should().BeEquivalentTo(expected);

        }
    }
}
