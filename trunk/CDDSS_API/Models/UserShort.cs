using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API
{
    //[DataContractAttribute]
    public class UserShort
    {
        //[DataMember]
        public string FirstName { get; set; }
        //[DataMember]
        public string LastName { get; set; }

        public UserShort(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}