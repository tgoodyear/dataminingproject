using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMiningApp.Analysis
{
    public class ParameterStream
    {
        private Dictionary<String,Object> parameters = new Dictionary<String,object>();
        private const String sessionName = "parameterStream";
        private System.Web.SessionState.HttpSessionState Session;

        public ParameterStream()
        {
            parameters = new Dictionary<String,object>();
        }

        public ParameterStream(System.Web.SessionState.HttpSessionState Session)
        {
            Session.Add(sessionName, this);
            this.Session = Session;
        }

        public bool contains (String parameterName)
        {
            return parameters.ContainsKey(parameterName);
        }

        public Object get(String parameterName)
        {
            return parameters[parameterName];
        }

        public void set(String parameterName, Object value)
        {
            if (contains(parameterName))
                parameters[parameterName] = value;
            else
               parameters.Add(parameterName,value);
        }

        public static ParameterStream getStream(System.Web.SessionState.HttpSessionState session)
        {
            return (ParameterStream) session[sessionName];
        }
    }
}