using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public partial class PCA_Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 

        }

        protected void FeatureList_Init(object sender, EventArgs e)
        {
            String dataSetParameterName = "dataSetName";
            ParameterStream stream = ParameterStream.getStream(Session);
            if (stream.contains(dataSetParameterName))
            {
                Registry.Registry appRegistry = Registry.Registry.getRegistry(Session);
                DataSet ds = appRegistry.GetDataset((String)stream.get(dataSetParameterName));

                foreach (DataColumn dc in ds.Tables[0].Columns)
                    FeatureList.Items.Add(dc.ColumnName);

            }
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            if (int.Parse(PCs.Text) > FeatureList.GetSelectedIndices().Count() || PCs.Text.Equals("") || int.Parse(PCs.Text) <1)
                return;
            Analysis analysis = (Analysis)Session["analysis"];
            ParameterStream stream = ParameterStream.getStream(Session);
            String[] features = new String[FeatureList.GetSelectedIndices().Count()];
            for(int i=0; i< FeatureList.GetSelectedIndices().Count();i++){
                features[i] = FeatureList.Items[FeatureList.GetSelectedIndices()[i]].Text;
            }
            stream.set("selectedFeatures", features);
            stream.set("numberOfPCs", int.Parse(PCs.Text));

            analysis.next(Response, Session);
        }

        protected void PCs_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(PCs.Text) > FeatureList.GetSelectedIndices().Count())
                Warning.Text = "Cannot extract more principal components than selected features";
            else
                Warning.Text = "";

        }

        protected void PCs_Init(object sender, EventArgs e)
        {
            PCs.Text = String.Format("%d",FeatureList.Items.Count);
        }
    }
}