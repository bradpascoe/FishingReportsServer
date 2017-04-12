using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    [DataContract]
    public class ImageEntity
    {
        [DataMember]
        public int ImageID
        {
            get;
            set;
        }

        [DataMember]
        public int? ReportID
        {
            get;
            set;
        }

        [DataMember]
        public string ImageName
        {
            get;
            set;
        }

        [DataMember]
        public string ThumbNailName
        {
            get;
            set;
        }
    }
}
