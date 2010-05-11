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

/*

            DataTable dt = new DataTable();
            //dt.Columns.Add("Weights")
            foreach (String feature in features){
                dt.Columns.Add(feature);
            }
            for (int i = 0; i< PCmatrix.ColumnCount;i++){
                dt.Rows.Add(PCmatrix.GetColumnVector(i).ToArray().Cast<Object[]>());
            
            }
            dt.Rows[0].ItemArray = PCmatrix.GetColumnVector(i).ToArray();
            PCView.DataSource = dt;
            PCView.DataBind();
*/

        }
    }
}