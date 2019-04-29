using Cheval.DataStructure;
using Cheval.Helper;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.DataStructureTests
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
            var matrix = new Matrix(new float[,]{
                 {1,2,3,4}
                ,{5.5f,6.5f,7.5f,8.5f}
                ,{9,10,11,12}
                ,{13.5f,14.5f,15.5f,16.5f}
            });
            //Assert
            matrix[0, 0].Should().Be(1);
            matrix[0, 3].Should().Be(4);
            matrix[1, 0].Should().Be(5.5f);
            matrix[1, 2].Should().Be(7.5f);
            matrix[2, 2].Should().Be(11);
            matrix[3, 0].Should().Be(13.5f);
            matrix[3, 2].Should().Be(15.5f);
            matrix.Size.Should().Be(4);
        }

        [Test]
        public void Matrix_can_be_created_by_size_without_data()
        {
            //Assign
            var matrix = new Matrix(4);
            //Act
            matrix[1, 2] = 4.5f;
            //Assert
            matrix[0, 0].Should().Be(0);
            matrix[1, 2].Should().Be(4.5f);
            matrix.Size.Should().Be(4);
        }

        [Test]
        public void Matrix_does_not_blow_up_without_data()
        {
            //Assign
            var matrix = new Matrix(new float [4, 4]);
            //Act
            matrix[1, 2] = 4.5f;
            //Assert
            matrix[0, 0].Should().Be(0);
            matrix[1, 2].Should().Be(4.5f);
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
            var matrix = new Matrix(new float[,]
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
            var matrix = new Matrix(new float[,]
            {
                {-3, 5, 0},
                {1, -2, -7},
                {0, 1, 1}
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
            var mat1 = new Matrix(new float[,]{
                 {1 ,2 ,3 ,4 }
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                ,{13,14,15,16}
            });
            var mat2 = new Matrix(new float[,]{
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
            var mat1 = new Matrix(new float[,]{
                {1 ,2 ,3 ,4 }
                ,{5 ,6 ,7 ,8 }
                ,{9 ,10,11,12}
                ,{13,14,15,16}
            });
            var mat2 = new Matrix(new float[,]{
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
            var mat1 = new Matrix(new float[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 8, 7, 6},
                {5, 4, 3, 2}
            });
            var mat2 = new Matrix(new float[,]
            {
                {-2, 1, 2, 3},
                {3, 2, 1, -1},
                {4, 3, 6, 5},
                {1, 2, 7, 8}
            });
            //Act
            var expected = new Matrix(new float[,]
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
            var mat = new Matrix(new float[,]
            {
                {1, 2, 3, 4},
                {2, 4, 4, 2},
                {8, 6, 4, 1},
                {0, 0, 0, 1}
            });
            var tup = new ChevalTuple(1, 2, 3, 1);
            //Act
            var expected = ChevalTuple.Point(18, 24, 33);
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
            var mat = new Matrix(new float[,]
            {
                {0, 2, 3, 4},
                {1, 2, 4, 8},
                {2, 4, 8, 16},
                {4, 8, 16, 32}
            });
            //Act
            var expected = mat;
            var result = mat * Transform.IdentityMatrix;
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
            var tuple = new ChevalTuple(1, 2, 3, 4);
            //Act
            var expected = tuple;
            var result = tuple * Transform.IdentityMatrix;
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario: A submatrix of a 3x3 matrix is a 2x2 matrix
           Given the following 3x3 matrix A:
           | 1  | 5 |  0 |
           | -3 | 2 |  7 |
           | 0  | 6 | -3 |
           Then submatrix(A, 0, 2) is the following 2x2 matrix:
           | -3 | 2 |
           | 0  | 6 |
         */
        [Test]
        public void Submatrix_of_three_by_three_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {1, 5, 0},
                {-3, 2, 7},
                {0, 6, -3}
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {-3, 2},
                {0, 6}

            });
            var result = Matrix.Submatrix(matrix, 0, 2);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        //Scenario: A submatrix of a 4x4 matrix is a 3x3 matrix
        // Given the following 4x4 matrix A:
        // | -6 | 1 | 1  | 6 |
        // | -8 | 5 | 8  | 6 |
        // | -1 | 0 | 8  | 2 |
        // | -7 | 1 | -1 | 1 |
        // Then submatrix(A, 2, 1) is the following 3x3 matrix:
        // | -6 | 1  |  6 |
        // | -8 | 8  |  6 |
        // | -7 | -1 |  1 |
        [Test]
        public void Submatrix_of_four_by_four_tes()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {-6, 1, 1, 6},
                {-8, 5, 8, 6},
                {-1, 0, 8, 2},
                {-7, 1, -1, 1}
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {-6, 1, 6},
                {-8, 8, 6},
                {-7, -1, 1}
            });
            var result = Matrix.Submatrix(matrix, 2, 1);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        /*
         * Scenario ​: Transposing a matrix ​   ​
         * Given ​ the following matrix A: ​  
         * | 0 | 9 | 3 | 0 | ​  
         * | 9 | 8 | 0 | 8 | ​  
         * | 1 | 8 | 5 | 3 | ​  
         * | 0 | 0 | 5 | 8 | ​   ​
         * Then ​
         * transpose(A) is the following matrix: ​  
         * | 0 | 9 | 1 | 0 | ​  
         * | 9 | 8 | 8 | 0 | ​  
         * | 3 | 0 | 5 | 5 | ​  
         * | 0 | 8 | 3 | 8 |
           
           Jamis Buck. The Ray Tracer Challenge (Kindle Locations 1187-1200). The Pragmatic Bookshelf, LLC. 
         */

        [Test]
        public void Transposing_a_matrix_test()
        {
            //Assign
            var mat = new Matrix(new float[,]
            {
                {0, 9, 3, 0},
                {9, 8, 0, 8},
                {1, 8, 5, 3},
                {0, 0, 5, 8}
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {0, 9, 1, 0},
                {9, 8, 8, 0},
                {3, 0, 5, 5},
                {0, 8, 3, 8}
            });
            var result = Matrix.Transpose(mat);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario ​: Transposing the identity matrix ​   ​
         * Given ​ A ← transpose( identity_matrix) ​   ​
         * Then ​ A = identity_matrix

Jamis Buck. The Ray Tracer Challenge (Kindle Locations 1205-1208). The Pragmatic Bookshelf, LLC. 
         */
        [Test]
        public void Transposing_identity_matrix_results_in_identity_matrix()
        {
            //Assign
            var ident = Transform.IdentityMatrix;
            //Act
            var result = Matrix.Transpose(ident);
            //Assert
            result.Should().BeEquivalentTo(Transform.IdentityMatrix);
        }
        /*
         * Scenario ​: Calculating the determinant of a 2x2 matrix ​   ​
         * Given ​the following 2x2 matrix A: ​  
         * | 1 | 5 | ​  
         * |-3 | 2 | ​   ​
         * Then ​determinant(A) = 17
           
           Jamis Buck. The Ray Tracer Challenge (Kindle Locations 1229-1235). The Pragmatic Bookshelf, LLC. 
         */
        [Test]
        public void Determinant_of_two_by_two_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {1 ,5},
                {-3,2}
            });
            //Act
            var result = Matrix.Determinant(matrix);
            //Assert
            result.Should().Be(17);
        }
        /*
         * Scenario: Calculating a minor of a 3x3 matrix
           Given the following 3x3 matrix A:
           | 3 | 5 | 0 |
           | 2 | -1 | -7 |
           | 6 | -1 | 5 |
           And B ← submatrix(A, 1, 0)
           Then determinant(B) = 25
           And minor(A, 1, 0) = 25
         */
        [Test]
        public void Minor_calculation_of_3_by_3_matrix_test()
        {
            //Assign
            var matrix= new Matrix(new float[,]
            {
                {3, 5, 0},
                {2,-1,-7},
                {6,-1, 5}
            });
            //Act
            var sub = Matrix.Submatrix(matrix,1,0);
            var det = Matrix.Determinant(sub);
            var result = Matrix.Minor(matrix, 1, 0);
            //Assert
            det.Should().Be(25);
            result.Should().Be(det);
        }
        /*
         * Scenario: Calculating a cofactor of a 3x3 matrix
           Given the following 3x3 matrix A:
           | 3 | 5  | 0 |
           | 2 | -1 | -7|
           | 6 | -1 | 5 |
           Then minor(A, 0, 0) = -12
           And cofactor(A, 0, 0) = -12
           And minor(A, 1, 0) = 25
           And cofactor(A, 1, 0) = -25
         */
        [Test]
        public void Calculating_cofactor_of_three_by_three_matrix()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {3, 5, 0},
                {2,-1,-7},
                {6,-1, 5}
            });
            //Act
            var result1Minor = Matrix.Minor(matrix, 0, 0);
            var result1Cofactor = Matrix.Cofactor(matrix, 0, 0);
            var result2Minor = Matrix.Minor(matrix, 1, 0);
            var result2Cofactor = Matrix.Cofactor(matrix, 1, 0);
            //Assert
            result1Minor.Should().Be(-12);
            result1Cofactor.Should().Be(-12);
            result2Minor.Should().Be(25);
            result2Cofactor.Should().Be(-25);
        }
        /*
         * Scenario: Calculating the determinant of a 3x3 matrix
           Given the following 3x3 matrix A:
           | 1  | 2 | 6  |
           | -5 | 8 | -4 |
           | 2  | 6 | 4  |
           Then cofactor(A, 0, 0) = 56
           And cofactor(A, 0, 1) = 12
           And cofactor(A, 0, 2) = -46
           And determinant(A) = -196
         */
        [Test]
        public void Determinant_of_three_by_three_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {1, 2, 6},
                {-5,8,-4},
                {2 ,6, 4}
            });
            //Act
            var cofact1 = Matrix.Cofactor(matrix, 0, 0);
            var cofact2 = Matrix.Cofactor(matrix, 0, 1);
            var cofact3 = Matrix.Cofactor(matrix, 0, 2);
            var result = Matrix.Determinant(matrix);
            //Assert
            cofact1.Should().Be(56);
            cofact2.Should().Be(12);
            cofact3.Should().Be(-46);
            result.Should().Be(-196);
        }

        /*
         * Scenario: Calculating the determinant of a 4x4 matrix
           Given the following 4x4 matrix A:
           | -2 | -8 | 3 | 5 |
           | -3 | 1 | 7 | 3 |
           | 1 | 2 | -9 | 6 |
           | -6 | 7 | 7 | -9 |
           Then cofactor(A, 0, 0) = 690
           And cofactor(A, 0, 1) = 447
           And cofactor(A, 0, 2) = 210
           And cofactor(A, 0, 3) = 51
           And determinant(A) = -4071
         */
        [Test]
        public void Determinant_of_four_by_four_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {-2,-8, 3, 5},
                {-3, 1, 7, 3},
                { 1, 2,-9, 6},
                {-6, 7, 7,-9 }
            });
            //Act
            var cofact1 = Matrix.Cofactor(matrix, 0, 0);
            var cofact2 = Matrix.Cofactor(matrix, 0, 1);
            var cofact3 = Matrix.Cofactor(matrix, 0, 2);
            var cofact4 = Matrix.Cofactor(matrix, 0, 3);
            var result = Matrix.Determinant(matrix);
            //Assert
            cofact1.Should().Be(690);
            cofact2.Should().Be(447);
            cofact3.Should().Be(210);
            cofact4.Should().Be(51);
            result.Should().Be(-4071);
        }

        /*
         * Scenario: Testing an invertible matrix for invertibility
           Given the following 4x4 matrix A:
           | 6 | 4  | 4  |  4 |
           | 5 | 5  | 7  |  6 |
           | 4 | -9 | 3  | -7 |
           | 9 | 1  | 7  | -6 |
           Then determinant(A) = -2120
           And A is invertible
         */
        [Test]
        public void Invertible_matrix_is_invertible()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {6, 4, 4, 4},
                {5, 5, 7, 6},
                {4,-9, 3,-7},
                {9, 1, 7,-6}
            });
            //Act
            var det = Matrix.Determinant(matrix);
            bool result = matrix.IsInvertible;
            //Assert
            det.Should().Be(-2120);
            result.Should().BeTrue();
        }
        /*
         * Scenario: Testing a noninvertible matrix for invertibility
           Given the following 4x4 matrix A:
           | -4 | 2 | -2 | -3 |
           | 9 | 6 | 2 | 6 |
           | 0 | -5 | 1 | -5 |
           | 0 | 0 | 0 | 0 |
           Then determinant(A) = 0
           And A is not invertible
         */
        [Test]
        public void NonInvertible_matrix_is_not_invertible()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {-4, 2, -2, -3},
                {9, 6, 2, 6},
                {0,-5, 1,-5},
                {0, 0, 0,0}
            });
            //Act
            var det = Matrix.Determinant(matrix);
            var result = matrix.IsInvertible;
            //Assert
            det.Should().Be(0);
            result.Should().BeFalse();
        }
        /*
         * Scenario: Calculating the inverse of a matrix
           Given the following 4x4 matrix A:
           | -5| 2 | 6 | -8|
           | 1 | -5| 1 | 8 |
           | 7 | 7 | -6| -7|
           | 1 | -3| 7 | 4 |
           And B ← inverse(A)
           Then determinant(A) = 532
           And cofactor(A, 2, 3) = -160
           And B[3,2] = -160/532
           And cofactor(A, 3, 2) = 105
           And B[2,3] = 105/532
           And B is the following 4x4 matrix:
           | 0.21805  |  0.45113 |  0.24060 |-0.04511 |
           | -0.80827 | -1.45677 | -0.44361 | 0.52068 |
           | -0.07895 | -0.22368 | -0.05263 | 0.19737 |
           | -0.52256 | -0.81391 | -0.30075 | 0.30639 |
         */
        [Test]
        public void Inverse_of_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                { -5, 2 , 6 , -8},
                { 1 , -5, 1 , 8 },
                { 7 , 7 , -6, -7},
                { 1 , -3, 7 , 4 }
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {  0.21805f , 0.45113f , 0.24060f ,-0.04511f},
                { -0.80827f ,-1.45677f ,-0.44361f ,0.52068f},
                { -0.07895f ,-0.22368f ,-0.05263f ,0.19737f},
                { -0.52256f ,-0.81391f ,-0.30075f ,0.30639f} 
            });
            var result = Matrix.Inverse(matrix);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Calculating the inverse of another matrix
           Given the following 4x4 matrix A:
           | 8 |-5| 9| 2 |
           | 7 | 5| 6| 1 |
           | -6| 0| 9| 6 |
           | -3| 0|-9|-4 |
           Then inverse(A) is the following 4x4 matrix:
           | -0.15385 | -0.15385 | -0.28205 | -0.53846 |
           | -0.07692 | 0.12308  | 0.02564  | 0.03077  |
           | 0.35897  | 0.35897  | 0.43590  | 0.92308  |
           | -0.69231 | -0.69231 | -0.76923 | -1.92308 |
           */
        [Test]
        public void Inverse_of_another_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                { 8 ,-5, 9, 2},
                { 7 , 5, 6, 1},
                { -6, 0, 9, 6},
                { -3, 0,-9,-4}
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {-0.15385f, -0.15385f,-0.28205f,-0.53846f},
                {-0.07692f,  0.12308f ,0.02564f ,0.03077f },
                {0.35897f,   0.35897f ,0.43590f ,0.92308f },
                {-0.69231f, -0.69231f,-0.76923f,-1.92308f}
            });
            var result = Matrix.Inverse(matrix);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
        Scenario: Calculating the inverse of a third matrix
        Given the following 4x4 matrix A:
        | 9  | 3 | 0 | 9 |
        | -5 | -2|-6 |-3 |
        | -4 | 9 | 6 | 4 |
        | -7 | 6 | 6 | 2 |
        Then inverse(A) is the following 4x4 matrix:
        | -0.04074 | -0.07778| 0.14444 |-0.22222 |
        | -0.07778 | 0.03333 | 0.36667 |-0.33333 |
        | -0.02901 | -0.14630|-0.10926 | 0.12963 |
        | 0.17778  | 0.06667 |-0.26667 | 0.33333 |
      */
        [Test]
        public void Inverse_of_third_matrix_test()
        {
            //Assign
            var matrix = new Matrix(new float[,]
            {
                {9 ,3 , 0, 9},
                {-5,-2,-6,-3},
                {-4,9 , 6, 4},
                {-7,6 , 6, 2}
            });
            //Act
            var expected = new Matrix(new float[,]
            {
                {-0.04074f, -0.07778f, 0.14444f,-0.22222f},
                {-0.07778f,  0.03333f ,0.36667f,-0.33333f},
                {-0.02901f, -0.14630f,-0.10926f, 0.12963f},
                { 0.17778f , 0.06667f,-0.26667f, 0.33333f}
            });
            var result = Matrix.Inverse(matrix);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: Multiplying a product by its inverse
           Given the following 4x4 matrix A:
           | 3 | -9| 7 | 3 |
           | 3 | -8| 2 |-9 |
           | -4| 4 | 4 | 1 |
           | -6| 5 |-1 | 1 |
           And the following 4x4 matrix B:
           | 8 | 2 | 2 | 2 |
           | 3 |-1 | 7 | 0 |
           | 7 | 0 | 5 | 4 |
           | 6 |-2 | 0 | 5 |
           And C ← A * B
           Then C * inverse(B) = A
         */
        [Test]
        public void Multiplying_product_by_its_inverse_test()
        {
            //Assign
            var matrixA = new Matrix(new float[,]
            {
                {3 , -9, 7 , 3},
                {3 , -8, 2 ,-9},
                {-4, 4 , 4 , 1},
                {-6, 5 ,-1 , 1}
            });
            var matrixB = new Matrix(new float[,]
            {
                {8, 2,2,2},
                {3,-1,7,0},
                {7, 0,5,4},
                {6,-2,0,5}
            });
            //Act
            var result = matrixA * matrixB;
            result = result * Matrix.Inverse(matrixB);
            //Assert
            result.Should().BeEquivalentTo(matrixA);
        }

    }
}
