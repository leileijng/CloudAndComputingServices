using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Diagnostics;

namespace TLTTSaaSWebApp.Models
{
    public class RDSContext : DbContext
    {
        public RDSContext()
          : base(GetRDSConnectionString())
        {
        }

        public static RDSContext Create()
        {
            return new RDSContext();
        }

        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["talents"];

            if (string.IsNullOrEmpty(dbname))
            {
                Debug.WriteLine("Cannot connect");
                return null;
            }

            string username = appConfig["CSCCA2"];
            string password = appConfig["cscassignment2"];
            string hostname = appConfig["talents.c1ve6rpyvqat.us-east-1.rds.amazonaws.com"];
            string port = appConfig["3306"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}