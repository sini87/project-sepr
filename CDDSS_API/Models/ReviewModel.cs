using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    [DataContract]
    public class ReviewModel
    {
        [DataMember]
        public int Issue { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public string Explanation { get; set; }
        [DataMember]
        public string UserFirstName { get; set; }
        [DataMember]
        public string UserLastName { get; set; }

        public ReviewModel()
        {
            UserFirstName = "";
            UserLastName = "";
            Rating = -1;
            Explanation = "";
            Issue = -1;
        }
        
        public ReviewModel(int issueId, int rating, string explanation)
        {
            Issue = issueId;
            UserFirstName = "";
            UserLastName = "";
            Rating = rating;
            Explanation = explanation;
        }
    }
}