using System;
using System.Runtime.Serialization;

using FishingReports.Model;

namespace FishingReportsServices
{
	[DataContract]
	public class ReportListItem
	{
		#region ReportListItem Members

		[DataMember]
		public string Access
		{
			get;
			set;
		}

		[DataMember]
		public Guid? AccessId
		{
			get;
			set;
		}

		[DataMember]
		public int? FlowRate
		{
			get;
			set;
		}

		[DataMember]
		public string Location
		{
			get;
			set;
		}

		[DataMember]
		public int NumberOfImages
		{
			get;
			set;
		}

		[DataMember]
		public DateTime ReportDate
		{
			get;
			set;
		}

		[DataMember]
		public string ReportDescription
		{
			get;
			set;
		}

		[DataMember]
		public int ReportId
		{
			get;
			set;
		}

		[DataMember]
		public ReportType ReportType
		{
			get;
			set;
		}

		[DataMember]
		public string State
		{
			get;
			set;
		}

		[DataMember]
		public int? TotalFish
		{
			get;
			set;
		}

		[DataMember]
		public string Username
		{
			get;
			set;
		}

		[DataMember]
		public string WaterConditions
		{
			get;
			set;
		}

		[DataMember]
		public string WeatherConditions
		{
			get;
			set;
		}

		#endregion ReportListItem Members

	}
}