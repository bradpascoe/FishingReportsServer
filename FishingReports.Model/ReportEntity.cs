using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    /// <summary>
    /// Represents a report object.
    /// </summary>
    [DataContract]
    public class ReportEntity
    {
        [DataMember]
        public DateTime ReportDate
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
        public Guid? AccessID
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
        public string Username
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
        public string Access
        {
            get;
            set;
        }

        [DataMember]
        public int ReportID
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
        public int? FlowRate
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

        [DataMember]
        public string ReportDescription
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
    }
}