using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace DataMiningApp
{
    public partial class Start : System.Web.UI.Page
    {
        protected Registry.Registry registry;

        protected void Page_Init(object sender, EventArgs e)
        {
            //TODO: Move registration to app initialization, not page load
            registry = (Registry.Registry) Application.Get("appRegistry");
             
            //Display in dropdown
            foreach (String name in registry.GetAlgorithmNames())
                AlgDropDown.Items.Add(name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AlgDocLink_Init(object sender, EventArgs e)
        {
            AlgDocLink.Text = "";
            AlgDocLink.Target = "_blank";
        }

        protected void AlgDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Drop down changed to " + AlgDropDown.SelectedIndex);
            AlgDocLink.Text = ("Documentation on " + registry.GetAlgorithm(AlgDropDown.SelectedIndex).getDisplayName());
            AlgDocLink.NavigateUrl = registry.GetAlgorithm(AlgDropDown.SelectedIndex).getDocumentationURL().ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //TODO: Use a copy instead of object from registry
            Analysis.Analysis analysis = registry.GetAlgorithm(AlgDropDown.SelectedIndex);
            Session["analysis"] = analysis;
            analysis.run(Response, Session);
            //Response.Redirect("PCASelectData.aspx");

        }

    }
}
