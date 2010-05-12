using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MathNet.Numerics.LinearAlgebra;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public partial class PCA_2D_Projection : System.Web.UI.Page
    {
        ParameterStream stream;
        Registry.Registry registry;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            stream = ParameterStream.getStream(Session);
            registry = Registry.Registry.getRegistry(Session);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Matrix PCmatrix = (Matrix)stream.get("PCmatrix");
            String[] features = (String[])stream.get("selectedFeatures");

            System.Data.DataSet ds = (System.Data.DataSet)registry.GetDataset((string)stream.get("dataSetName"));

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
                X.SetColumnVector(columnVector.Subtract(columnVector.Average()), i);
            }

            //get first two PCs
            Matrix xy = new Matrix(PCmatrix.RowCount,2);
            xy.SetColumnVector(PCmatrix.GetColumnVector(0),0);
            xy.SetColumnVector(PCmatrix.GetColumnVector(1),1);

            
            //project
            Matrix projected = X.Multiply(xy);

            DataPoint point;
            Projection.Series.Clear();
            Projection.Legends.Clear();


            //if a label column is selected
            String LabelColumnName = LabelColumn.Text;

            if (!LabelColumnName.Equals(""))
            {

                //get labels
                int labelColumnIndex = dt.Columns[LabelColumnName].Ordinal;
                List<String> labels = new List<String>();
                String item;
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = (String)dt.Rows[i].ItemArray.ElementAt(labelColumnIndex);
                    if (!labels.Contains(item))
                        labels.Add(item);
                }
                Projection.Legends.Add(LabelColumnName);
                System.Drawing.Font font = Projection.Legends[LabelColumnName].Font = new System.Drawing.Font(Projection.Legends[LabelColumnName].Font.Name, 14);

                //Configure series
                foreach (String label in labels)
                {
                    Projection.Series.Add(label);
                    Projection.Series[label].LegendText = label;
                    Projection.Series[label].IsXValueIndexed = false;
                    Projection.Series[label].ChartType = SeriesChartType.Point;
                    Projection.Series[label].MarkerSize = 8;
                }

                //Add points
                for (int i = 0; i < projected.RowCount; i++)
                {
                    point = new DataPoint(projected[i, 0], projected[i, 1]);
                    String label = dt.Rows[i].ItemArray[labelColumnIndex].ToString();
                    Projection.Series[label].Points.Add(point);
                }

            }
            else
            {
                //Single plot graph
                Projection.Series.Add("series1");
                Projection.Series[0].IsXValueIndexed = false;
                Projection.Series[0].ChartType = SeriesChartType.Point;
                Projection.Series[0].MarkerSize = 8;

                for (int i = 0; i < projected.RowCount; i++)
                {
                    point = new DataPoint(projected[i, 0], projected[i, 1]);
                    Projection.Series[0].Points.Add(point);
                }
            }
        }

        protected void LabelColumn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LabelColumn_Init(object sender, EventArgs e)
        {

            DataSet ds = (DataSet)registry.GetDataset((string)stream.get("dataSetName"));
            DataTable dt  = ds.Tables[0];
            LabelColumn.Items.Clear();
            LabelColumn.Items.Add("");
            for (int i = 0; i < dt.Columns.Count; i++)
                if (dt.Columns[i].DataType == System.Type.GetType("System.String") )
                    LabelColumn.Items.Add(dt.Columns[i].ColumnName);
        }

    }
}