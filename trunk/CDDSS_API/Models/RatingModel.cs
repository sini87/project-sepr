using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    [DataContractAttribute]
    public class RatingModel
    {
        [DataMember]
        public int CriterionID { get; set; }
        [DataMember]
        public int AlternativeID { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public Double Rating1 { get; set; }
        //[DataMember]
        //internal int UserID { get; set; }

        public RatingModel()
        {
            CriterionID = -1;
            AlternativeID = -1;
            User = "";
            Rating1 = 0.0;
          //  UserID = -1;
        }

        internal RatingModel(int criterionID, int alternativeID, String user, Double rating)
        {
            CriterionID = criterionID;
            AlternativeID = alternativeID;
            User = user;
            Rating1 = rating;
         //   UserID = userID;
        }

        internal RatingModel(int criterionID, int alternativeID, Double rating)
        {
            CriterionID = criterionID;
            AlternativeID = alternativeID;
            Rating1 = rating;
        }
    }
}