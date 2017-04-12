using System.Runtime.Serialization;

namespace FishingReports.Model
{
	[DataContract]
	public enum ReportType
	{
		[EnumMember]
		Wade = 0,

		[EnumMember]
		Float = 1,

		[EnumMember]
		Lake = 2,

		[EnumMember]
		SaltFlats = 3
	}
}
