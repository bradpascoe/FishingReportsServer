using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    /// <summary>
    /// Represents a location object.
    /// </summary>
    [DataContract]
    public class LocationEntity
    {
        [DataMember]
        public string Location
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

        [DataMember]
        public Guid StateID
        {
            get;
            set;
        }
    }
}
