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
        public int AccessObject { get; set; }
        public string Email { get; set; }

        public UserShort()
        {

        }
        public UserShort(string firstName, string lastName, int accessObject)
        {
            FirstName = firstName;
            LastName = lastName;
            AccessObject = accessObject;
        }

        public UserShort(string firstName, string lastName, int accessObject, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            AccessObject = accessObject;
            Email = email;
        }
    }
}