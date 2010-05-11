using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMiningApp.Registry
{
    public class Registry
    {
        private List<Analysis.Analysis> algorithms;
        private List<System.Data.DataSet> datasets;
        private static HttpApplicationState Application;
        private const String appName = "appRegistry";
        private const String sessionName = "sessionRegistry";
        
        public Registry (HttpApplicationState application) {
        Application = application;
        Application.Add(appName, this);
        algorithms = new List<Analysis.Analysis>();
        datasets = new List<System.Data.DataSet>();
        }

        public Registry(System.Web.SessionState.HttpSessionState session)
        {
            session.Add(sessionName, this);
            algorithms = new List<Analysis.Analysis>();
            datasets = new List<System.Data.DataSet>();
        }


        public void registerAlgorithm(Analysis.Analysis algorithm)
        {
            algorithms.Add(algorithm);
        }

        public List<String> GetAlgorithmNames()
        {
            List<String> algNames = new List<string>();
            foreach (Analysis.Analysis alg in algorithms){
                algNames.Add(alg.getDisplayName());
            }
            return algNames;
        }


        public Analysis.Analysis GetAlgorithm(int index)
        {
            return algorithms.ElementAt(index);
        }

        public void registerDataset(System.Data.DataSet dataset)
        {
            datasets.Add(dataset);
        }

        public List<String> GetDatasetNames()
        {
            List<String> datasetNames = new List<string>();
            foreach (System.Data.DataSet dataset in datasets)
            {
                datasetNames.Add(dataset.DataSetName);
            }
            return datasetNames;
        }


        public System.Data.DataSet GetDataset(int index)
        {
            return datasets.ElementAt(index);
        }

        public System.Data.DataSet GetDataset(String name)
        {
            foreach (System.Data.DataSet ds in datasets)
            {
                if (ds.DataSetName.Equals(name))
                    return ds;
            }
            return null;
        }

        //Helper static

        public static Registry getRegistry(System.Web.SessionState.HttpSessionState session)
        {
            return (Registry)session[sessionName];
        }

        public static Registry getRegistry(HttpApplicationState application)
        {
            return (Registry)application[appName];
        }
    }
}