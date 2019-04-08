using Cheval_Tracer.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests
{
    public class ModelTests
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
    }
}