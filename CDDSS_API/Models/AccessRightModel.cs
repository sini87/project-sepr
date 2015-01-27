using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing an accessright
    /// </summary>
    [DataContract]
    public class AccessRightModel
    {
        [DataMember]
        public char Right { get; set; }
        [DataMember]
        public UserShort User { get; set; }

        public AccessRightModel()
        {

        }

        public AccessRightModel(UserShort user, char accessRight)
        {
            Right = accessRight;
            User = user;
        }
    }
}