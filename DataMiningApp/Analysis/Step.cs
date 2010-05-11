using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningApp.Analysis
{
    public interface Step
    {
        void start(Analysis parent, System.Web.HttpResponse response, System.Web.SessionState.HttpSessionState session);
    }
}
