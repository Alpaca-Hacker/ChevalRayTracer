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

        /*
         * Scenario: Matrix equality with identical matrices
           Given the following matrix A:
           | 1 | 2 | 3 | 4 |
           | 5 | 6 | 7 | 8 |
           | 9 | 8 | 7 | 6 |
           | 5 | 4 | 3 | 2 |
           And the following matrix B:
           | 1 | 2 | 3 | 4 |
           | 5 | 6 | 7 | 8 |
           | 9 | 8 | 7 | 6 |
           | 5 | 4 | 3 | 2 |
           Then A = B
         */
        [Test]
        public void Matrix_equality_with_identical_matrices()
        {
            //Assign
            var mat1 = new Matrix(new double[,]{
                 {1 ,2 ,3 ,4 }
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                ,{13,14,15,16}
            });
            var mat2 = new Matrix(new double[,]{
                 {1 ,2 ,3 ,4 }
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                ,{13,14,15,16}
            });
            //Act
            var result = mat1 == mat2;
            //Assert
            result.Should().BeTrue();
        }
        /*
         * Scenario: Matrix equality with different matrices
           Given the following matrix A:
           | 1 | 2 | 3 | 4 |
           | 5 | 6 | 7 | 8 |
           | 9 | 8 | 7 | 6 |
           | 5 | 4 | 3 | 2 |
           And the following matrix B:
           | 2 | 3 | 4 | 5 |
           | 6 | 7 | 8 | 9 |
           | 8 | 7 | 6 | 5 |
           | 4 | 3 | 2 | 1 |
           Then A != B
         */
        [Test]
        public void Matrix_equality_with_differnt_matrices()
        {
            //Assign
            var mat1 = new Matrix(new double[,]{
                {1 ,2 ,3 ,4 }
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                ,{13,14,15,16}
            });
            var mat2 = new Matrix(new double[,]{
                {1 ,2 ,3 ,4 }
                ,{13,14,15,16}
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                
            });
            //Act
            var result = mat1 == mat2;
            //Assert
            result.Should().BeFalse();
        }
        /*
         * Scenario: Multiplying two matrices
           Given the following matrix A:
           | 1 | 2 | 3 | 4 |
           | 5 | 6 | 7 | 8 |
           | 9 | 8 | 7 | 6 |
           | 5 | 4 | 3 | 2 |
           And the following matrix B:
           | -2 | 1 | 2 | 3 |
           | 3 | 2 | 1 | -1 |
           | 4 | 3 | 6 | 5 |
           | 1 | 2 | 7 | 8 |
           Then A * B is the following 4x4 matrix:
           | 20| 22 | 50 | 48 |
           | 44| 54 | 114 | 108 |
           | 40| 58 | 110 | 102 |
           | 16| 26 | 46 | 42 |
         */
        [Test]
        public void Matrix_multiplication_test()
        {
            //Assign
            var mat1 = new Matrix(new double[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 8, 7, 6},
                {5, 4, 3, 2}
            });
            var mat2 = new Matrix(new double[,]
            {
                {-2, 1, 2, 3},
                {3, 2, 1, -1},
                {4, 3, 6, 5},
                {1, 2, 7, 8}
            });
            //Act
            var expected = new Matrix(new double[,]
            {
                {20, 22, 50, 48},
                {44, 54, 114, 108},
                {40, 58, 110, 102},
                {16, 26, 46, 42}
            });
            var result = mat1 * mat2;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: A matrix multiplied by a tuple
           Given the following matrix A:
           | 1 | 2 | 3 | 4 |
           | 2 | 4 | 4 | 2 |
           | 8 | 6 | 4 | 1 |
           | 0 | 0 | 0 | 1 |
           And b ← tuple(1, 2, 3, 1)
           Then A * b = tuple(18, 24, 33, 1)
         */
        [Test]
        public void Multiply_matrix_by_tuple()
        {
            //Assign
            var mat = new Matrix(new double[,]
            {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
                {0, 0, 0, 1}
            });
            var tup = new ChevalTuple(1,2,3,1);
            //Act
            var expected = new ChevalTuple(18,24,33,1);
            var result1 = mat * tup;
            var result2 = tup * mat;
            //Assert
            result1.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario: Multiplying a matrix by the identity matrix
           Given the following matrix A:
           | 0 | 1 | 2 | 4 |
           | 1 | 2 | 4 | 8 |
           | 2 | 4 | 8 | 16 |
           | 4 | 8 | 16 | 32 |
           Then A * identity_matrix = A

         */
        [Test]
        public void Multiplying_a_matrix_by_identity_matrix_test()
        {
            //Assign
            var mat = new Matrix(new double[,]
            {
                {0, 2, 3, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32}
            });
            //Act
            var expected = mat;
            var result = mat * Matrix.Identity;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Multiplying the identity matrix by a tuple
           Given a ← tuple(1, 2, 3, 4)
           Then identity_matrix * a = a
         */
        [Test]
        public void Multiplying_a_tuple_by_identity_matrix_test()
        {
            //Assign
            var tuple = new ChevalTuple(1,2,3,4);
            //Act
            var expected = tuple;
            var result = tuple * Matrix.Identity;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
