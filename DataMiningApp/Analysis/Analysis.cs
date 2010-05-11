using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace DataMiningApp.Analysis
{
    public abstract class Analysis
    {
        //members
        protected String displayName;
        protected List<Step> steps;
        protected Uri docURL;
        protected int currentStep;
        protected HttpResponse response;
        
        //constructor
        public Analysis(String name)
        {
            displayName = name;
            Debug.WriteLine("This is the display name at the Analysis constructor " + displayName);
            currentStep = 0;
            steps = new List<Step>();
        }

        //interface

        public virtual void run(HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            this.response = response;
        }

        public abstract void next(HttpResponse response, System.Web.SessionState.HttpSessionState session);

        public void registerProgressCallback() { }
        public void registerStepDoneClalback() { }
        
        //Accessors
        public String getDisplayName()
        {
            return displayName;
        }

        public void setDocumentationURL (Uri URL){
            docURL = URL;
        }

        public Uri getDocumentationURL (){
            return docURL;
        }

    }
}