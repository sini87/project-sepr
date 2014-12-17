using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    [DataContract]
    public class DecisionModel
    {
        [DataMember]
        public int IssueID { get; set; }

        [DataMember]
        public int AlternativeID { get; set; }

        [DataMember]
        public string Explanation { get; set; }

        [DataMember]
        public AlternativeModel Alternative { get; set; }

        public DecisionModel()
        {
            AlternativeID = -1;
            IssueID = -1;
            Explanation = "";
            Alternative = null;
        }

        public DecisionModel(int issueID, int alternativeID, string explanation)
        {
            IssueID = issueID;
            AlternativeID = alternativeID;
            Explanation = explanation;
            Alternative = null;
        }
    }
}