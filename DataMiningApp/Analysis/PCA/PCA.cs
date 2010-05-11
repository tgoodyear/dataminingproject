using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace DataMiningApp.Analysis.PCA

{
    public class PCA : Analysis 
    {
        public PCA(String Name) : base(Name)
        {
            Debug.WriteLine("This is the display name at the PCA constructor " + displayName);
            steps.Add(new Common.Steps.SelectDataset());
            steps.Add(new Steps.PCAConfig());
            steps.Add(new Steps.NIPALS_PCA());
            steps.Add(new Steps.PCAResults());
        }

        override public void run(HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            base.response = response;
            currentStep=0;
            steps.ElementAt(currentStep).start(this, response, session);
        }


        override public void next(HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            currentStep++;
            steps.ElementAt(currentStep).start(this, response, session);
        }

    }
    
   
}