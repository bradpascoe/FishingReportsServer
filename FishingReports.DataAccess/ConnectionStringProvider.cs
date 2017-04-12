using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace FishingReports.DataAccess
{
    public class ConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["FishingReportsConnectionString"].ConnectionString;
        }
    }
}