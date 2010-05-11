using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public class NIPALS_PCA :Step
    {
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            ParameterStream stream = ParameterStream.getStream(session);
            String[] features = (String[]) stream.get("selectedFeatures");
            int PCs = (int) stream.get("numberOfPCs");
            Registry.Registry registry = Registry.Registry.getRegistry(session);
            System.Data.DataSet ds = (System.Data.DataSet) registry.GetDataset((string) stream.get("dataSetName"));

            //retrieve dataset table (assume one for now)
            System.Data.DataTable dt = ds.Tables[0];
            
            
            //raw data
            double[,] rawData = new double[dt.Rows.Count, features.Count()];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < features.Count(); j++)
                    rawData[i, j] = (double)dt.Rows[i].ItemArray.ElementAt(dt.Columns[features[j]].Ordinal);
            }

            //Create matrix to hold data for PCA
            Matrix X = new Matrix(rawData);

            //Remove mean
            Vector columnVector;
            for (int i = 0; i < X.ColumnCount; i++)
            {
                columnVector = X.GetColumnVector(i);
                X.SetColumnVector(columnVector.Subtract(columnVector.Average()),i);
            }

            Matrix PCmatrix = new Matrix(X.ColumnCount, PCs, 0);
            Vector Weights = new Vector(PCs);

            NIPALS(X, PCs, PCmatrix, Weights);
            /*
            response.Buffer = true;
            response.Write(PCmatrix.ToString() + "\n");
            response.Write(Weights.ToString() + "\n");
            response.Flush();
            */
            Debug.WriteLine("Done with PCA");
            stream.set("PCmatrix", PCmatrix);
            stream.set("Weights", Weights);
            parent.next(response, session);
        }

        static void NIPALS(Matrix X, int PCs, Matrix PCmatrix, Vector EigenValues)
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
                //printMatrix(E, "E");
                //1.    u := x(i) 	Select a column vector xi of the matrix X and copy it to the vector u
                //The vector must be such that the self-inner product is not zero 

                double ut_u = 0;
                Boolean valid = false;
                while (!valid && initialVector < E.ColumnCount)
                {
                    u = E.GetColumnVector(initialVector);
                    initialVector++;
                    if (u.ScalarMultiply(u) != 0)
                        valid = true;
                }
                if (!valid)
                    throw new Exception("Could not find " + PCs + " principal components");

                E_transposed = E.Clone();
                E_transposed.Transpose();
                //printMatrix(E_transposed, "E transposed");

                //int step = 1;
                double error = 1;
                while (error > threshold)
                {
                    //  Console.Out.WriteLine("PC " + (i+1) + " Step : " + step++);

                    //2. 	v := (X'u)/(u'u) 	Project the matrix X onto u in order to find the corresponding loading vs
                    //Console.Out.WriteLine("u: " + u.ToColumnMatrix().ToString());
                    ut_u = u.ScalarMultiply(u);
                    if (ut_u == 0)
                        throw new Exception("Principal component results in complex answer");

                    //Console.Out.WriteLine("u'u: " + ut_u);

                    Matrix v_prime = E_transposed.Multiply(u.ToColumnMatrix());
                    //printMatrix(v_prime, "v prime");
                    v = v_prime.GetColumnVector(0) / ut_u;
                    //Console.Out.WriteLine("v: " + v.ToString());

                    //3.    v := v/|v| 	Normalize the loading vector v to length 1
                    v = v.Normalize();
                    //v = v / v.Norm();
                    //Console.Out.WriteLine("v after normalization: " + v.ToString());


                    //4.1 	u_old := u  Store the score vector u into uold 
                    Vector u_old = u.Clone();
                    //Console.Out.WriteLine("u old: " + u_old.ToString());

                    //4.2      u := (Xp)/(v'v) 	and project the matrix X onto v in order to find corresponding score vector u
                    Matrix u_prime = E.Multiply(v.ToColumnMatrix());
                    //Console.Out.WriteLine("u_prime: ");
                    //printMatrix(u_prime);

                    Vector u_primeColumn = u_prime.GetColumnVector(0);
                    //Console.Out.WriteLine("u_primeColumn: " + u_primeColumn.ToString());

                    double v_v = v.ScalarMultiply(v);

                    //Console.Out.WriteLine("v_v: " + v_v);

                    u = u_primeColumn / v_v;
                    //Console.Out.WriteLine("new u: " + u.ToString());

                    //5. 	d := uold-u 	In order to check for the convergence of the process 
                    //calculate the difference vector d as the difference between the previous scores
                    //and the current scores. If the difference |d| is larger than a pre-defined threshold,
                    //then return to step 2.
                    Vector d = u_old.Subtract(u);
                    //Console.Out.WriteLine("d: " + d.ToString());
                    error = d.Norm();
                    //Console.Out.WriteLine("Error: " + error.ToString());

                }
                //6. 	E := X - tp' 	Remove the estimated PCA component (the product of the scores and the loadings) from X
                Matrix tp = u.ToColumnMatrix().Multiply(v.ToRowMatrix());
                //printMatrix(tp, "tp'");
                E.Subtract(tp);

                PCmatrix.SetColumnVector(v, i);
                EigenValues[i] = u.Norm();
                //7. 	X := E 	In order to estimate the other PCA components repeat this procedure from step 1 using the matrix E as the new X

            }
        }

    }
}