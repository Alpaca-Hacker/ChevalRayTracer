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
            w.Lights.Should().BeNull();
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
    }
}
