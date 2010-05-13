using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public class PCAConfigDynamic : Step

    {
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            session["stepid"] = 2;
            response.Redirect("~/Default.aspx");
            //response.Redirect("~/Analysis/PCA/Steps/PCA_Config.aspx");
        }
    }
}
