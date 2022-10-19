using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models.Commons
{        
    public class WebConfigConnectionString
        {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
    public class getWebConnectionString {
       
        public List<WebConfigConnectionString> WCCS = new List<WebConfigConnectionString>();

        public   List<WebConfigConnectionString>getDefaultConnStrings(){
           List<string> names = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().Select(v => v.Name).ToList();
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] != "LocalSqlServer")
                {
                    WebConfigConnectionString wccs = new WebConfigConnectionString()
                    {
                        Name = names[i],
                        ConnectionString = ConfigurationManager.ConnectionStrings[$"{names[i]}"].ConnectionString
                    };
                    WCCS.Add(wccs);
                }

            }
            return WCCS;
        }
    }
}