using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishingReports.DataAccess
{
    public static class DALFactory
    {
        public static ILoginDAL CreateLoginDAL()
        {
            return new LoginDAL();
        }

        public static ILocationDAL CreateLocationDAL()
        {
            return new LocationDAL();
        }

        public static IReportDAL CreateReportDAL()
        {
            return new ReportDAL();
        }
    }
}
