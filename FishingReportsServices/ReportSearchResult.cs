using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FishingReportsServices
{
	[DataContract]
	public class ReportSearchResult
	{
		#region ReportSearchResult Members

		[DataMember]
		public double AverageFish
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfReports
		{
			get;
			set;
		}

		[DataMember]
		public ReportListItem[] Results
		{
			get;
			set;
		}

		[DataMember]
		public int TotalFish
		{
			get;
			set;
		}

		#endregion ReportSearchResult Members

	}
}