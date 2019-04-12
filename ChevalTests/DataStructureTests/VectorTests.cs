using System;
using Cheval.DataStructure;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.DataStructureTests
{
    [TestFixture]
    public class VectorTests
    {
        /*
         * Scenario: Computing the magnitude of vector(1, 0, 0)
           Given v ← vector(1, 0, 0)
           Then magnitude(v) = 1
           Scenario: Computing the magnitude of vector(0, 1, 0)
           Given v ← vector(0, 1, 0)
           Then magnitude(v) = 1
           Scenario: Computing the magnitude of vector(0, 0, 1)
           Given v ← vector(0, 0, 1)
           Then magnitude(v) = 1
           Scenario: Computing the magnitude of vector(1, 2, 3)
           Given v ← vector(1, 2, 3)
           Then magnitude(v) = √14
           Scenario: Computing the magnitude of vector(-1, -2, -3)
           Given v ← vector(-1, -2, -3)
           Then magnitude(v) = √14
         */

        [TestCase(1D, 0D, 0D, ExpectedResult = 1D)]
        [TestCase(0D, 01, 0D, ExpectedResult = 1D)]
        [TestCase(0D, 0D, 1D, ExpectedResult = 1D)]
        [TestCase(1D, 2D, 3D, ExpectedResult = 3.7416573867739413D)] // √14
        [TestCase(-1D, -2D, -3D, ExpectedResult = 3.7416573867739413D)]
        [TestCase(-2D, -3D, -6D, ExpectedResult = 7)]
        public double MagnitudeTest(double x, double y, double z)
        {
            //Assign
            var test = ChevalTuple.Vector(x, y, z);
            //Act
            return ChevalTuple.Magnitude(test);
        }

        /*
           Scenario: Normalizing vector(4, 0, 0) gives (1, 0, 0)
           Given v ← vector(4, 0, 0)
           Then normalize(v) = vector(1, 0, 0)

           Scenario: Normalizing vector(1, 2, 3)
           Given v ← vector(1, 2, 3)
                                              # vector(1/√14, 2/√14, 3/√14)
           Then normalize(v) = approximately vector(0.26726, 0.53452, 0.80178)
         */
       [Test]
        public void NormalizeTests()
        {
            //Assign
            var v1 = ChevalTuple.Vector(4, 0, 0);
            var v2 = ChevalTuple.Vector(1, 2, 3);

            //Act
            var expected1 = ChevalTuple.Vector(1, 0, 0);
            var result1 = ChevalTuple.Normalize(v1);
            var expected2 = ChevalTuple.Vector(1/Math.Sqrt(14), 2 / Math.Sqrt(14), 3 / Math.Sqrt(14));
            var result2 = ChevalTuple.Normalize(v2);
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
        }
        /*
         *Scenario: The magnitude of a normalized vector
           Given v ← vector(1, 2, 3)
           When norm ← normalize(v)
           Then magnitude(norm) = 1
         */
        [Test]
        public void Magnitude_of_normalized_vector_is_one()
        {
            //Assign
            var v1 = ChevalTuple.Vector(1,2,3);
            //Act
            var norm = ChevalTuple.Normalize(v1);
            var result = ChevalTuple.Magnitude(norm);
            //Assert
            result.Should().Be(1);
        }
        /*
         * Scenario: The dot product of two tuples
           Given a ← vector(1, 2, 3)
           And b ← vector(2, 3, 4)
           Then dot(a, b) = 20
         */
        [Test]
        public void Dot_product_test()
        {
            //Assign
            var a = ChevalTuple.Vector(1,2,3);
            var b = ChevalTuple.Vector(2,3,4);
            //Act
            var result = ChevalTuple.Dot(a, b);
            //Assert
            result.Should().Be(20);
        }
        /*
         * Scenario: The cross product of two vectors
           Given a ← vector(1, 2, 3)
           And b ← vector(2, 3, 4)
           Then cross(a, b) = vector(-1, 2, -1)
           And cross(b, a) = vector(1, -2, 1)
         */
        [Test]
        public void Cross_product_tests()
        {
            //Assign
            var a = ChevalTuple.Vector(1,2,3);
            var b = ChevalTuple.Vector(2,3,4);
            //Act
            var expected1 = ChevalTuple.Vector(-1, 2, -1);
            var result1 = ChevalTuple.Cross(a, b);
            var expected2 = ChevalTuple.Vector(1,-2,1);
            var result2 = ChevalTuple.Cross(b, a);
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
        }
    }

}

