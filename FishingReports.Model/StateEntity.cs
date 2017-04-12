using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    [DataContract]
    public class StateEntity
    {
        [DataMember]
        public string State
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
