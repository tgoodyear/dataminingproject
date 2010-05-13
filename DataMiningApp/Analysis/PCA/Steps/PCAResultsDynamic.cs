using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public class PCAResultsDynamic : Step
    {
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            session["stepid"] = 3;
            response.Redirect("~/Default.aspx");
            //response.Redirect("~/Analysis/PCA/Steps/PCA_Results.aspx");
        }
    }
}