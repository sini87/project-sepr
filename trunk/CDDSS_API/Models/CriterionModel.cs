using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using CDDSS_API.Models.Domain;

namespace CDDSS_API.Models
{
    [DataContractAttribute]
    public class CriterionModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public int Issue { get; set; }
        [DataMember]
        public Double Weight { get; set; }
        [DataMember]
        public List<RatingModel> Ratings { get; set; }

        public CriterionModel()
        {
            Name = "";
            Description = "";
            Issue = -1;
            Weight = -1;
            Ratings = new List<RatingModel>();
        }

       
    }

    
}