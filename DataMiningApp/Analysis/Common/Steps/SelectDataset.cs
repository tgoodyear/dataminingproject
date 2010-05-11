using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;

namespace DataMiningApp.Analysis.Common.Steps
{
    public class SelectDataset : Step
    {
        Analysis caller;
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            caller = parent;
            Debug.WriteLine("SelectData step of " + caller.getDisplayName() + " started");
            response.Redirect("~/Analysis/Common/Steps/SelectDataset.aspx");
        }

    }
}