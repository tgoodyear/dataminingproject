using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;

namespace DataMiningApp.Analysis.Common.Steps
{
    public class SelectDatasetDynamic : Step
    {
        Analysis caller;
        public void start(Analysis parent, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            caller = parent;
            Debug.WriteLine("SelectData step of " + caller.getDisplayName() + " started");
            session["stepid"] = 1;
            response.Redirect("~/Default.aspx");
        }

    }
}