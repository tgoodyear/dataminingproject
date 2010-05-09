using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace PCA1
{
    class Program
    {
        //private static MathNet.Numerics.Algorithms.LinearAlgebra.Atlas.AtlasLinearAlgebraProvider provider;
        static void Main(string[] args)
        {
            Console.Out.WriteLine ("Hello...");
            
            //provider = new MathNet.Numerics.Algorithms.LinearAlgebra.Atlas.AtlasLinearAlgebraProvider();

            /*
            double[,] A = new double[2,2];
            A[0, 0] = 5;
            A[0, 1] = 0;
            A[1, 0] = 0;
            A[1, 1] = 10;
            
            /*
            double[,] A = new double[3, 2];
            A[0, 0] = 5;
            A[0, 1] = 0;
            A[1, 0] = 0;
            A[1, 1] = 10;
            A[2, 0] = 5;
            A[2, 1] = 15;
            */

            double[,] A = new double[4, 3];
            A[0, 0] = 1;
            A[0, 1] = 2;
            A[0, 2] = 3;
            A[1, 0] = 4;
            A[1, 1] = 5;
            A[1, 2] = 6;
            A[2, 0] = 6;
            A[2, 1] = 5;
            A[2, 2] = 4;
            A[3, 0] = 3;
            A[3, 1] = 2;
            A[3, 2] = 1;
            Matrix X = new Matrix(A);

            //remove mean
            for (int i=0;i<X.ColumnCount;i++)
                X.SetColumnVector(X.GetColumnVector(i).Subtract(X.GetColumnVector(i).Average()),i);


            printMatrix(X, "X");
            int PCs = 2;
            Matrix PCmatrix = new Matrix(X.ColumnCount, PCs, 0);
            Vector EigenValues = new Vector(PCs);

            try
            {
                NIPALS(X, PCs, PCmatrix, EigenValues);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
                Console.In.ReadLine();
                return;
            }
            
            printMatrix(PCmatrix, "Principal Components");
            double projection = PCmatrix.GetColumnVector(0).ScalarMultiply(PCmatrix.GetColumnVector(1));
            Console.Out.WriteLine("projection:  " + projection);
            printMatrix(EigenValues.ToRowMatrix(), "Weights");
            
            Console.Out.WriteLine("SVD:");
            SingularValueDecomposition svd = X.SingularValueDecomposition;
            Console.Out.WriteLine("LSV: ");
            printMatrix(svd.LeftSingularVectors);
            Console.Out.WriteLine("RSV: ");
            printMatrix(svd.RightSingularVectors);
            Console.Out.WriteLine("S: ");
            printMatrix(svd.S);
            Console.Out.WriteLine("Singular Values: ");
            printMatrix(svd.SingularValues.ToRowMatrix());
            Console.In.ReadLine();
        }
        
        static void NIPALS (Matrix X, int PCs, Matrix PCmatrix, Vector EigenValues) 
        {

            //TODO:
            //Check that PCs<min(X.rows,X.columns)
            //Check that PCMatrix size is (X.columns, PCs) , i.e. (features, lower number of dimensions)
            //Change name of eigenvalues to something more appropriate
            //Check that eigenvalues vector passed in is of size PCs.


            //X is the zero-mean data matrix
            //E is the residual error after i itereations
            int i;
            //PCmatrix = new Matrix(PCs, X.ColumnCount, 0);
            //EigenValues = new Vector(PCs);
            Matrix E = X.Clone();
            Vector u = new Vector(E.RowCount);
            Vector v = new Vector(E.ColumnCount);
            Matrix E_transposed;
            int initialVector = 0;
            //convergence threshold
            const double threshold = 0.0000001;

            //from http://www.vias.org/tmdatanaleng/dd_nipals_algo.html
             for (i = 0; i < PCs; i++)
            {
                printMatrix(E, "E");
                //1.    u := x(i) 	Select a column vector xi of the matrix X and copy it to the vector u
                //The vector must be such that the self-inner product is not zero 
                
                double ut_u=0;
                Boolean valid = false;
                while (!valid && initialVector<E.ColumnCount){
                    u = E.GetColumnVector(initialVector);
                    initialVector++;
                    if (u.ScalarMultiply(u)!=0)
                        valid = true;
                }
                if(!valid)
                    throw new Exception("Could not find " + PCs +  " principal components");
  
                E_transposed = E.Clone();
                E_transposed.Transpose();
                printMatrix(E_transposed, "E transposed");
                
                int step = 1;
                double error = 1;
                while (error > threshold)
                {
                    Console.Out.WriteLine("PC " + (i+1) + " Step : " + step++);

                    //2. 	v := (X'u)/(u'u) 	Project the matrix X onto u in order to find the corresponding loading vs
                    Console.Out.WriteLine("u: " + u.ToColumnMatrix().ToString());
                    ut_u = u.ScalarMultiply(u);
                    if (ut_u == 0)
                        throw new Exception("Principal component results in complex answer");
  
                    Console.Out.WriteLine("u'u: " + ut_u);
 
                    Matrix v_prime = E_transposed.Multiply(u.ToColumnMatrix());
                    printMatrix(v_prime, "v prime");
                    v = v_prime.GetColumnVector(0) / ut_u;
                    Console.Out.WriteLine("v: " + v.ToString());

                    //3.    v := v/|v| 	Normalize the loading vector v to length 1
                    v = v.Normalize();
                    //v = v / v.Norm();
                    Console.Out.WriteLine("v after normalization: " + v.ToString());


                    //4.1 	u_old := u  Store the score vector u into uold 
                    Vector u_old = u.Clone();
                    Console.Out.WriteLine("u old: " + u_old.ToString());

                    //4.2      u := (Xp)/(v'v) 	and project the matrix X onto v in order to find corresponding score vector u
                    Matrix u_prime = E.Multiply(v.ToColumnMatrix());
                    Console.Out.WriteLine("u_prime: ");
                    printMatrix(u_prime);

                    Vector u_primeColumn = u_prime.GetColumnVector(0);
                    Console.Out.WriteLine("u_primeColumn: " + u_primeColumn.ToString());

                    double v_v = v.ScalarMultiply(v);

                    Console.Out.WriteLine("v_v: " + v_v);

                    u = u_primeColumn/ v_v;
                    Console.Out.WriteLine("new u: " + u.ToString());

                    //5. 	d := uold-u 	In order to check for the convergence of the process 
                    //calculate the difference vector d as the difference between the previous scores
                    //and the current scores. If the difference |d| is larger than a pre-defined threshold,
                    //then return to step 2.
                    Vector d = u_old.Subtract(u);
                    Console.Out.WriteLine("d: " + d.ToString());
                    error = d.Norm();
                    Console.Out.WriteLine("Error: " + error.ToString());

                }
                //6. 	E := X - tp' 	Remove the estimated PCA component (the product of the scores and the loadings) from X
                Matrix tp = u.ToColumnMatrix().Multiply(v.ToRowMatrix());
                printMatrix(tp, "tp'");
                E.Subtract(tp);

                PCmatrix.SetColumnVector(v,i);
                EigenValues[i] = u.Norm();
                //7. 	X := E 	In order to estimate the other PCA components repeat this procedure from step 1 using the matrix E as the new X
            
             }
        }

        static void printMatrix(Matrix matrix, String name)
        {
            Console.Out.WriteLine(name + ":");
            printMatrix(matrix);
        }

        static void printMatrix(Matrix matrix)
        {
            Console.Out.Write(matrix.ToString());
            Console.Out.WriteLine("\tSize: (" + matrix.RowCount + ", " + matrix.ColumnCount + ")");
        }

        public static SingularValueDecomposition  SVD(Matrix X)
        {
            return X.SingularValueDecomposition;
        }

    }
}
