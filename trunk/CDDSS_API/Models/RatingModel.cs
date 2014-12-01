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
        internal int CriterionID { get; set; }
        [DataMember]
        internal int AlternativeID { get; set; }
        [DataMember]
        internal string User { get; set; }
        [DataMember]
        internal Double Rating1 { get; set; }
        [DataMember]
        internal AlternativeModel Alternative1 { get; set; }
        [DataMember]
        internal CriterionModel Criterion { get; set; }
        [DataMember]
        internal UserShort User1 { get; set; }

        internal RatingModel()
        {
            CriterionID = -1;
            AlternativeID = -1;
            User = "";
            Rating1 = 0.0;
            Alternative1 = new AlternativeModel();
            Criterion = new CriterionModel();
            User1 = new UserShort();
        }
    }
}