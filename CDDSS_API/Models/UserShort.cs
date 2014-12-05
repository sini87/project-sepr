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
        public string PhoneNumber {get; set;}
        public string UserName {get; set;}
        public string SecretQuestion {get; set;}
        public string Answer {get; set;}

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

        public UserShort(string email, string firstName, string lastName, string username, string secretquestion, string answer)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            SecretQuestion = secretquestion;
            Answer = answer;
        }
    }
}