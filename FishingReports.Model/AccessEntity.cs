using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    [DataContract]
    public class AccessEntity
    {
        [DataMember]
        public string Access
        {
            get;
            set;
        }

        [DataMember]
        public Guid AccessID
        {
            get;
            set;
        }

        [DataMember]
        public Guid LocationID
        {
            get;
            set;
        }
    }
}
