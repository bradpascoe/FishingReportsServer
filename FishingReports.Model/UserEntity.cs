using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FishingReports.Model
{
    /// <summary>
    /// Represents a user object.
    /// </summary>
    [DataContract]
    public class UserEntity
    {
        [DataMember]
        public string Username
        {
            get;
            set;
        }

        [DataMember]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Only used when creating new objects or changing a password.
        /// </summary>
        [DataMember]
        public string Password2
        {
            get;
            set;
        }

        [DataMember]
        public string Email
        {
            get;
            set;
        }

        [DataMember]
        public string Residence
        {
            get;
            set;
        }
    }
}