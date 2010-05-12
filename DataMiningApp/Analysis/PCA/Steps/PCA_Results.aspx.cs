using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MathNet.Numerics.LinearAlgebra;
using System.Data;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public partial class PCA_Results : System.Web.UI.Page
    {
        ParameterStream stream;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (stream.contains("algRunTime"))
                AlgorithmTime.Text = "PCA Algorithm took: " + stream.get("algRunTime").ToString() + " ms";
            else
                AlgorithmTime.Text = "";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            stream = ParameterStream.getStream(Session);
        }


        protected void PCView_Init(object sender, EventArgs e)
        {
            Matrix PCmatrix = (Matrix)stream.get("PCmatrix");
            Vector Weights = (Vector)stream.get("Weights");
            String[] features = (String[])stream.get("selectedFeatures");
            
            for (int i = 0; i< Weights.Length;i++)
                VariancePlot.Series[0].Points.InsertY(i,Weights[i]);

            DataSet ds = new DataSet("temp");
            DataTable dt = new DataTable();
            // Declare your Columns

            //PC Weight
            DataColumn dc = new DataColumn("Weight", Type.GetType("System.Double"));
            dt.Columns.Add(dc);

            //PC Coefficients
            foreach(String feature in features){
                dc = new DataColumn(feature, Type.GetType("System.Double"));
                dt.Columns.Add(dc);
            }

            // Add the DataTable to your DataSet
            ds.Tables.Add(dt);

            DataRow dr;
            for (int i = 0; i < PCmatrix.ColumnCount; i++)
            {
                dr = ds.Tables[0].NewRow();
                dt.Rows.Add(dr);
                dt.Rows[i][0] = Weights[i];
                for (int j = 0;j<PCmatrix.RowCount;j++)
                    dt.Rows[i][j+1]= PCmatrix[j,i];

            }
            /*
            dr = ds.Tables[0].NewRow();
            dt.Rows.Add(dr);
            double[] dataArray = new double[Weights.Length];
            dataArray[0] = 1232.21321;
            dr[0] = 321.12321;

            //dt.Rows[0].ItemArray[1] = 333.32;
            */
            PCView.DataSource = dt;
            PCView.DataBind();

        }

        protected void Project_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Analysis/PCA/Steps/PCA_2D_Projection.aspx");
        }
    }
}