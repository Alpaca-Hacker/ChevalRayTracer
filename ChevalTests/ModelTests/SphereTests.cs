using System;
using System.Collections.Generic;
using System.Text;
using Cheval.Helper;
using Cheval.Models.Primitives;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    public class SphereTests
    {
        /*
         * Scenario: A sphere's default transformation
           Given s ← sphere()
           Then s.transform = identity_matrix
           */
        [Test]
        public void sphere_default_translation_is_identity()
        {
            //Assign
            var s = new Sphere();
            //Assert
            s.Transform = Transform.IdentityMatrix;
        }
        /*
           Scenario: Changing a sphere's transformation
           Given s ← sphere()
           And t ← translation(2, 3, 4)
           When set_transform(s, t)
           Then s.transform = t
         */
        [Test]
        public void Changing_spheres_transform_test()
        {
            //Assign
            var s = new Sphere();
            var t = Transform.Translation(2, 3, 4);
            //Act
            s.Transform = t;
            //Assert
            s.Transform.Should().BeEquivalentTo(t);
        }
    }
}
