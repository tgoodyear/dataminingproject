using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DataMiningApp.Analysis.PCA.Steps
{
    public class PCAConfig : Step

    {
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            response.Redirect("~/Analysis/PCA/Steps/PCA_Config.aspx");
        }
    }
}
