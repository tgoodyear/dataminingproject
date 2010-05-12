using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public class Svd_PCA :Step
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
            System.Diagnostics.Stopwatch watch  = new Stopwatch();

            //Run algorithm and time it
            watch.Start();
            SingularValueDecomposition svd =  SVD(X);
            watch.Stop();
            stream.set("algRunTime",watch.ElapsedMilliseconds);
            
            /*
            response.Buffer = true;
            response.Write(PCmatrix.ToString() + "\n");
            response.Write(Weights.ToString() + "\n");
            response.Flush();
            */
            Debug.WriteLine("Done with PCA");
            stream.set("PCmatrix", svd.RightSingularVectors);
            stream.set("Weights", svd.SingularValues);
            parent.next(response, session);
        }

        static SingularValueDecomposition SVD(Matrix X)
        {
            return X.SingularValueDecomposition;         
        }

    }
}