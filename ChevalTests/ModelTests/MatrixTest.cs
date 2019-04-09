using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ModelTests
{
    public class MatrixTest
    {
        /*
         * Scenario: Constructing and inspecting a 4x4 matrix
           Given the following 4x4 matrix M:
           | 1    | 2    | 3    | 4    |
           | 5.5  | 6.5  | 7.5  | 8.5  |
           | 9    | 10   | 11   | 12   |
           | 13.5 | 14.5 | 15.5 | 16.5 |
           Then M[0,0] = 1
           And M[0,3] = 4
           And M[1,0] = 5.5
           And M[1,2] = 7.5
           And M[2,2] = 11
           And M[3,0] = 13.5
           And M[3,2] = 15.5
         */
        [Test]
        public void Four_by_Four_matrix_is_correct()
        {
            //Assign
            var matrix = new Matrix(new double[,]{
                 {1,2,3,4}
                ,{5.5,6.5,7.5,8.5}
                ,{9,10,11,12}
                ,{13.5,14.5,15.5,16.5}
            });
            //Assert
            matrix[0, 0].Should().Be(1);
            matrix[0, 3].Should().Be(4);
            matrix[1, 0].Should().Be(5.5);
            matrix[1, 2].Should().Be(7.5);
            matrix[2, 2].Should().Be(11);
            matrix[3, 0].Should().Be(13.5);
            matrix[3, 2].Should().Be(15.5);
            matrix.Size.Should().Be(4);
        }

        [Test]
        public void Matrix_can_be_created_by_size_without_data()
        {
            //Assign
            var matrix = new Matrix(4);
            //Act
            matrix[1, 2] = 4.5D;
            //Assert
            matrix[0, 0].Should().Be(0);
            matrix[1, 2].Should().Be(4.5);
            matrix.Size.Should().Be(4);
        }
        [Test]
        public void Matrix_does_not_blow_up_without_data()
        {
            //Assign
            var matrix = new Matrix(new double [4,4]);
            //Act
            matrix[1, 2] = 4.5D;
            //Assert
            matrix[0, 0].Should().Be(0);
            matrix[1, 2].Should().Be(4.5);
        }

        /*
         * Scenario: A 2x2 matrix ought to be representable
           Given the following 2x2 matrix M:
           | -3 | 5 |
           | 1 | -2 |
           Then M[0,0] = -3
           And M[0,1] = 5
           And M[1,0] = 1
           And M[1,1] = -2
         */
        [Test]
        public void Two_by_two_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new double[,]
            {
                {-3, 5},
                {1, -2}
            });
            //Assert
            matrix[0, 0].Should().Be(-3);
            matrix[0, 1].Should().Be(5);
            matrix[1, 0].Should().Be(1);
            matrix[1, 1].Should().Be(-2);
        }
        /*
         * Scenario: A 3x3 matrix ought to be representable
           Given the following 3x3 matrix M:
           | -3 | 5 | 0  |
           | 1 | -2 | -7 |
           | 0 | 1  | 1  |
           Then M[0,0] = -3
           And M[1,1] = -2
           And M[2,2] = 1
         */
        [Test]
        public void Three_by_three_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new double [,]
            {
                {-3, 5,   0 },
                { 1 ,-2, -7 },
                { 0,  1,  1 }
            });
            //Assert
            matrix[0, 0].Should().Be(-3);
            matrix[1, 1].Should().Be(-2);
            matrix[2, 2].Should().Be(1);
        }
    }
}
