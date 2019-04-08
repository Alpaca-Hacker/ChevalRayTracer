using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    [TestFixture]
    public class TupleTests
    {
        /*
        Scenario ​: A tuple with w = 1.0 is a point
        Given ​ a ← tuple( 4.3, -4.2, 3.1, 1.0) ​   ​ 
        Then ​ a.x = 4.3 ​   ​ 
        And ​ a.y = -4.2 ​   ​ 
        And ​ a.z = 3.1 ​   ​ 
        And ​ a.w = 1.0 ​   ​ 
        And ​ a is a point ​   ​ 
        And ​ a is not a vector

            Jamis Buck.The Ray Tracer Challenge(Kindle Locations 314-325). The Pragmatic Bookshelf, LLC.
        */
        [Test]
        public void A_tuple_with_w_equal_1_is_a_point()
        {
            //Assign
            var a = new ChevalTuple(4.3, -4.2, 3.1, 1.0);

            //Assert
            a.X.Should().Be(4.3);
            a.Y.Should().Be(-4.2);
            a.Z.Should().Be(3.1);
            a.W.Should().Be(1.0);

            a.IsPoint.Should().BeTrue();
            a.IsVector.Should().BeFalse();
        }

        /*
        Scenario ​: A tuple with w = 0 is a vector
        Given ​ a ← tuple( 4.3, -4.2, 3.1, 0.0)
        Then ​ a.x = 4.3
        And ​ a.y = -4.2
        And ​ a.z = 3.1
        And ​ a.w = 0.0
        And ​ a is not a point
        And ​ a is a vector

        Jamis Buck.The Ray Tracer Challenge (Kindle Locations 328-339). The Pragmatic Bookshelf, LLC.
        */

        [Test]
        public void A_tuple_with_w_equal_0_is_a_vector()
        {
            //Assign
            var a = new ChevalTuple(4.3, -4.2, 3.1, 0.0);

            //Assert
            a.X.Should().Be(4.3);
            a.Y.Should().Be(-4.2);
            a.Z.Should().Be(3.1);
            a.W.Should().Be(0.0);

            a.IsPoint.Should().BeFalse();
            a.IsVector.Should().BeTrue();
        }

        /*
         Scenario ​: point() creates tuples with w = 1
         Given ​ p ← point( 4, -4, 3) ​   ​ 
         Then ​ p = tuple( 4, -4, 3, 1) ​   ​   ​ 


Jamis Buck. The Ray Tracer Challenge (Kindle Locations 345-354). The Pragmatic Bookshelf, LLC. 
         */
        [Test]
        public void A_point_is_a_tuple_with_w_equal_1()
        {
            //Assign
            var p = new ChevalPoint(4, -4, 3);
            //Assert
            p.Should().BeAssignableTo<ChevalTuple>();
            p.W.Should().Be(1.0);
            p.IsPoint.Should().BeTrue();
        }

        /*
         Scenario ​: vector() creates tuples with w = 0 ​   ​ 
         Given ​ v ← vector( 4, -4, 3) ​   ​ 
         Then ​ v = tuple( 4, -4, 3, 0)
         */

        [Test]
        public void A_vector_is_a_tuple_with_w_equal_0()
        {
            //Assign
            var v = new ChevalVector(4, -4, 3);
            //Assert
            v.Should().BeAssignableTo<ChevalTuple>();
            v.W.Should().Be(0.0);
            v.IsVector.Should().BeTrue();
        }

        [Test]
        public void Equality_is_correct()
        {
            //Assign
            var t1 = new ChevalTuple(4, -4, 3, 1.0);
            var t2 = new ChevalTuple(4, -4, 3, 1.0);
            var t3 = new ChevalTuple(4, 4, 3, 0);

            var p1 = new ChevalPoint(4, -4, 3);
            var p2 = new ChevalPoint(4, -4, 3);
            var p3 = new ChevalPoint(4, 4, 3);

            var v1 = new ChevalVector(4, -4, 3);
            var v2 = new ChevalVector(4, -4, 3);
            var v3 = new ChevalVector(4, 4, 3);
            //Assert

            Assert.IsTrue(t1 == p2);
            Assert.IsFalse(t1 == v1);

            Assert.IsTrue(p2 == t1);
            Assert.IsFalse(v1 == t1);
            Assert.IsFalse(v1 == p1);

            Assert.IsTrue(t1 == t2);
            Assert.IsFalse(t1 == t3);

            Assert.IsTrue(p1 == p2);
            Assert.IsFalse(p1 == p3);

            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 == v3);

            Assert.IsTrue(v1.Equals(v2));
            Assert.IsFalse(v1.Equals(v3));
        }
        /*
         Scenario: Adding two tuples
         Given a1 ← tuple(3, -2, 5, 1)
         And a2 ← tuple(-2, 3, 1, 1)
         Then a1 + a2 = tuple(1, 1, 6, 2)
         */

        [Test]
        public void Adding_Two_tuples()
        {
            //Assign
            var a1 = new ChevalTuple(3, -2, 5, 1);
            var a2 = new ChevalTuple(-2, 3, 1, 1);

            //Act
            var expected = new ChevalTuple(1, 1, 6, 2);
            var result = a1 + a2;

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Adding_Two_Vectors_Results_In_Vector()
        {
            //Assign
            var a1 = new ChevalVector(3, -2, 5);
            var a2 = new ChevalVector(-2, 3, 1);

            //Act
            var expected = new ChevalVector(1, 1, 6);
            var result = a1 + a2;

            //Assert
            result.Should().BeEquivalentTo(expected);
            result.IsVector.Should().BeTrue();
        }

        [Test]
        public void Adding_Vectors_To_Point_Results_In_Point()
        {
            //Assign
            var a1 = new ChevalPoint(3, -2, 5);
            var a2 = new ChevalVector(-2, 3, 1);

            //Act
            var expected = new ChevalPoint(1, 1, 6);
            var result = a1 + a2;

            //Assert
            result.Should().BeEquivalentTo(expected);
            result.IsPoint.Should().BeTrue();
        }

        /*
        Scenario: Subtracting two points
        Given p1 ← point(3, 2, 1)
        And p2 ← point(5, 6, 7)
        Then p1 - p2 = vector(-2, -4, -6)
        */

        [Test]
        public void Subtracting_two_points_results_in_vector()
        {
            //Assign
            var p1 = new ChevalPoint(3, 2, 1);
            var p2 = new ChevalPoint(5, 6, 7);

            //Act
            var expected = new ChevalVector(-2, -4, -6);
            var result = p1 - p2;

            //Assert
            result.Should().BeEquivalentTo(expected);
            result.IsVector.Should().BeTrue();
        }

        /*
         * Scenario: Subtracting a vector from a point
           Given p ← point(3, 2, 1)
           And v ← vector(5, 6, 7)
           Then p - v = point(-2, -4, -6)
         */
        [Test]
        public void Subtracting_point_from_a_vector_results_in_point()
        {
            //Assign
            var p1 = new ChevalPoint(3, 2, 1);
            var v1 = new ChevalVector(5, 6, 7);

            //Act
            var expected = new ChevalPoint(-2, -4, -6);
            var result = p1 - v1;

            //Assert
            result.Should().BeEquivalentTo(expected);
            result.IsPoint.Should().BeTrue();
        }
        /*
         * Scenario: Subtracting two vectors
           Given v1 ← vector(3, 2, 1)
           And v2 ← vector(5, 6, 7)
           Then v1 - v2 = vector(-2, -4, -6)
         */

        [Test]
        public void Subtracting_two_vectors_results_in_vector()
        {
            //Assign
            var v1 = new ChevalVector(3, 2, 1);
            var v2 = new ChevalVector(5, 6, 7);

            //Act
            var expected = new ChevalVector(-2, -4, -6);
            var result = v1 - v2;

            //Assert
            result.Should().BeEquivalentTo(expected);
            result.IsVector.Should().BeTrue();
        }

        /*
         * Scenario: Negating a tuple
           Given a ← tuple(1, -2, 3, -4)
           Then -a = tuple(-1, 2, -3, 4)
         */
        [Test]
        public void Negating_tuple()
        {
            //Assign
            var t1 = new ChevalTuple(1, -2, 3, -4);
            //Act
            var expected = new ChevalTuple(-1, 2, -3, 4);
            var result = -t1;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario: Multiplying a tuple by a scalar
           Given a ← tuple(1, -2, 3, -4)
           Then a * 3.5 = tuple(3.5, -7, 10.5, -14)

         */
        [Test]
        public void Multiply_a_tuple()
        {
            //Assign
            var t1 = new ChevalTuple(1, -2, 3, -4);
            //Act
            var expected = new ChevalTuple(3.5, -7, 10.5, -14);
            var result1 = 3.5 * t1;
            var result2 = t1 * 3.5;
            //Assert
            result1.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
        }

        /*
         * 
           Scenario: Multiplying a tuple by a fraction
           Given a ← tuple(1, -2, 3, -4)
           Then a * 0.5 = tuple(0.5, -1, 1.5, -2)
         */
        [Test]
        public void Multiply_a_tuple_by_fraction()
        {
            //Assign
            var t1 = new ChevalTuple(1, -2, 3, -4);
            //Act
            var expected = new ChevalTuple(0.5, -1, 1.5, -2);
            var result1 = 0.5 * t1;
            var result2 = t1 * 0.5;
            //Assert
            result1.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario: Dividing a tuple by a scalar
           Given a ← tuple(1, -2, 3, -4)
           Then a / 2 = tuple(0.5, -1, 1.5, -2)
         */
        [Test]
        public void Dividing_a_tuple()
        {
            //Assign
            var t1 = new ChevalTuple(1, -2, 3, -4);
            //Act
            var expected = new ChevalTuple(0.5, -1, 1.5, -2);
            var result = t1 / 2;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}

