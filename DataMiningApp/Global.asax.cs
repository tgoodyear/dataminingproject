using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DataMiningApp
{
    public class Global : System.Web.HttpApplication
    {
        Registry.Registry appRegistry;
        Registry.Registry sessionRegistry;
        int stepid;


        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            appRegistry = new Registry.Registry(Application);

            //Add algorithms
            Analysis.Analysis myPCA1 = new Analysis.PCA.NipalsPCA("Principal Component Analysis - NIPALs");
            appRegistry.registerAlgorithm(myPCA1);
   
            Analysis.Analysis myPCA2 = new Analysis.PCA.svdPCA("Principal Component Analysis - SVD");
            appRegistry.registerAlgorithm(myPCA2);

            //Add existing data sets 
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            // Session data
            //Selected analysis
            Session.Add("analysis", null);
            Analysis.ParameterStream stream = new Analysis.ParameterStream(Session);
            sessionRegistry = new Registry.Registry(Session);
            Session.Add("stepid", stepid);
            stepid = 1;


            //Add pre-existing user data sets
            //Add pre-existing user-defined scripts
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
