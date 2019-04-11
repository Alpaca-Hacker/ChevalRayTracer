using System;
using Cheval.Helper;
using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.Models.ChevalTuple;

namespace ChevalTests.HelperTests
{
    public class TransformTests
    {
        /*
         * Scenario: Multiplying by a translation matrix
           Given transform ← translation(5, -3, 2)
           And p ← point(-3, 4, 5)
           Then transform * p = point(2, 1, 7)
           */
        [Test]
        public void Multiplying_by_translation_matrix()
        {
            //Assign
            var transform = Transform.Translation(5, -3, 2);
            var p = Point(-3, 4, 5);
            //Act
            var expected = Point(2,1,7);
            var result = transform * p;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: Multiplying by the inverse of a translation matrix
        Given transform ← translation(5, -3, 2)
        And inv ← inverse(transform)
        And p ← point(-3, 4, 5)
        Then inv * p = point(-8, 7, 3)
      */
        [Test]
        public void Multiplying_by_inv_translation_matrix()
        {
            //Assign
            var transform = Transform.Translation(5, -3, 2);
            var p = Point(-3, 4, 5);
            var inv = Matrix.Inverse(transform);
            //Act
            var expected = Point(-8, 7, 3);
            var result = inv * p;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Translation does not affect vectors
              Given transform ← translation(5, -3, 2)
              And v ← vector(-3, 4, 5)
              Then transform * v = v
         */
        [Test]
        public void Multiplying_by_vector_and_translation_matrix()
        {
            //Assign
            var transform = Transform.Translation(5, -3, 2);
            var v = Vector(-3, 4, 5);
            //Act
            var result = transform * v;
            //Assert
            result.Should().BeEquivalentTo(v);
        }
        /*
         * Scenario: A scaling matrix applied to a point
           Given transform ← scaling(2, 3, 4)
           And p ← point(-4, 6, 8)
           Then transform * p = point(-8, 18, 32)
           */
        [Test]
        public void Multiplying_by_scaling_matrix()
        {
            //Assign
            var transform = Transform.Scaling(2, 3, 4);
            var p = Point(-4, 6, 8);
            //Act
            var expected = Point(-8, 18, 32);
            var result = transform * p;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: A scaling matrix applied to a vector
        Given transform ← scaling(2, 3, 4)
        And v ← vector(-4, 6, 8)
        Then transform * v = vector(-8, 18, 32)
        */
        [Test]
        public void Multiplying_by_scaling_matrix_with_vector()
        {
            //Assign
            var transform = Transform.Scaling(2, 3, 4);
            var v = Vector(-4, 6, 8);
            //Act
            var expected = Vector(-8, 18, 32);
            var result = transform * v;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: Multiplying by the inverse of a scaling matrix
        Given transform ← scaling(2, 3, 4)
        And inv ← inverse(transform)
        And v ← vector(-4, 6, 8)
        Then inv * v = vector(-2, 2, 2)
      */
        [Test]
        public void Multiplying_by_inverse_scaling_matrix_with_vector()
        {
            //Assign
            var transform = Transform.Scaling(2, 3, 4);
            var v = Vector(-4, 6, 8); 
            //Act
            var inv = Matrix.Inverse(transform);
            var expected = Vector(-2, 2, 2);
            var result = inv * v;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Reflection is scaling by a negative value
           Given transform ← scaling(-1, 1, 1)
           And p ← point(2, 3, 4)
           Then transform * p = point(-2, 3, 4)
         */
        [Test]
        public void Multiplying_by_scaling_matrix_with_negative_for_reflection()
        {
            //Assign
            var transform = Transform.Scaling(-1, 1, 1);
            var p = Point(2, 3, 4);
            //Act
            var expected = Point(-2, 3, 4);
            var result = transform * p;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Rotating a point around the x axis
           Given p ← point(0, 1, 0)
           And half_quarter ← rotation_x(π / 4)
           And full_quarter ← rotation_x(π / 2)
           Then half_quarter * p = point(0, √2/2, √2/2)
           And full_quarter * p = point(0, 0, 1)
         */
        [Test]
        public void Rotating_around_X_test()
        {
            //Assign
            var p = Point(0,1,0);
            //Act
            var hq = Transform.RotationX(Math.PI / 4);
            var fq = Transform.RotationX(Math.PI / 2);
            var expected1 = Point(0, Math.Sqrt(2)/2, Math.Sqrt(2) / 2);
            var expected2 = Point(0,0,1);
            var result1 = hq * p;
            var result2 = fq * p;
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
        }
        /*Scenario: The inverse of an x-rotation rotates in the opposite direction
           Given p ← point(0, 1, 0)
           And half_quarter ← rotation_x(π / 4)
           And inv ← inverse(half_quarter)
           Then inv * p = point(0, √2/2, -√2/2)
           */
        [Test]
        public void Rotating_around_X_inv_test()
        {
            //Assign
            var p = Point(0, 1, 0);
            //Act
            var hq = Transform.RotationX(Math.PI / 4);
            var inv = Matrix.Inverse(hq);
            var expected = Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            
            var result = inv * p;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Rotating a point around the y axis
           Given p ← point(0, 0, 1)
           And half_quarter ← rotation_y(π / 4)
           And full_quarter ← rotation_y(π / 2)
           Then half_quarter * p = point(√2/2, 0, √2/2)
           And full_quarter * p = point(1, 0, 0)
         */
        [Test]
        public void Rotating_around_Y_test()
        {
            //Assign
            var p = Point(0, 0, 1);
            //Act
            var hq = Transform.RotationY(Math.PI / 4);
            var fq = Transform.RotationY(Math.PI / 2);
            var expected1 = Point(Math.Sqrt(2) / 2,0, Math.Sqrt(2) / 2);
            var expected2 = Point(1, 0, 0);
            var result1 = hq * p;
            var result2 = fq * p;
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
        }
        /*
         * Scenario: Rotating a point around the z axis
           Given p ← point(0, 1, 0)
           And half_quarter ← rotation_z(π / 4)
           And full_quarter ← rotation_z(π / 2)
           Then half_quarter * p = point(-√2/2, √2/2, 0)
           And full_quarter * p = point(-1, 0, 0)
         */
        [Test]
        public void Rotating_around_Z_test()
        {
            //Assign
            var p = Point(0, 1, 0);
            //Act
            var hq = Transform.RotationZ(Math.PI / 4);
            var fq = Transform.RotationZ(Math.PI / 2);
            var expected1 = Point(-Math.Sqrt(2) / 2,  Math.Sqrt(2) / 2,0);
            var expected2 = Point(-1, 0, 0);
            var result1 = hq * p;
            var result2 = fq * p;
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
        }
        /*
         * Scenario: A shearing transformation moves x in proportion to y
           Given transform ← shearing(1, 0, 0, 0, 0, 0)
           And p ← point(2, 3, 4)
           Then transform * p = point(5, 3, 4)
         */
        [Test]
        public void Shearing_trans_moves_x_in_proportion_to_y()
        {
            //Assign
            var transform = Transform.Shearing(1, 0, 0, 0, 0, 0);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(5, 3, 4);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: A shearing transformation moves x in proportion to z
           Given transform ← shearing(0, 1, 0, 0, 0, 0)
           And p ← point(2, 3, 4)
           Then transform * p = point(6, 3, 4)
           */
        [Test]
        public void Shearing_trans_moves_x_in_proportion_to_z()
        {
            //Assign
            var transform = Transform.Shearing(0, 1, 0, 0, 0, 0);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(6,3,4);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
           Scenario: A shearing transformation moves y in proportion to x
           Given transform ← shearing(0, 0, 1, 0, 0, 0)
           And p ← point(2, 3, 4)
           Then transform * p = point(2, 5, 4)
           */
        [Test]
        public void Shearing_trans_moves_y_in_proportion_to_x()
        {
            //Assign
            var transform = Transform.Shearing(0, 0, 1, 0, 0, 0);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(2, 5, 4);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: A shearing transformation moves y in proportion to z
        Given transform ← shearing(0, 0, 0, 1, 0, 0)
        And p ← point(2, 3, 4)
        Then transform * p = point(2, 7, 4)
         */
        [Test]
        public void Shearing_trans_moves_y_in_proportion_to_z()
        {
            //Assign
            var transform = Transform.Shearing(0, 0, 0, 1, 0, 0);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(2, 7, 4);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: A shearing transformation moves z in proportion to x
        Given transform ← shearing(0, 0, 0, 0, 1, 0)
        And p ← point(2, 3, 4)
        Then transform * p = point(2, 3, 6)
        */
        [Test]
        public void Shearing_trans_moves_z_in_proportion_to_x()
        {
            //Assign
            var transform = Transform.Shearing(0, 0, 0, 0, 1, 0);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(2, 3, 6);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: A shearing transformation moves z in proportion to y
        Given transform ← shearing(0, 0, 0, 0, 0, 1)
        And p ← point(2, 3, 4)
        Then transform * p = point(2, 3, 7)
      */
        [Test]
        public void Shearing_trans_moves_z_in_proportion_to_y()
        {
            //Assign
            var transform = Transform.Shearing(0, 0, 0, 0, 0, 1);
            var p = Point(2, 3, 4);
            //Act
            var result = transform * p;
            var expected = Point(2, 3, 7);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
