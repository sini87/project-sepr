using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing CriterionWeight
    /// </summary>
    [DataContractAttribute]
    public class CriterionWeightModel
    {
        /// <summary>
        /// Access Object ID of User
        /// </summary>
        [DataMember]
        public int UserAccesObject { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public int Criterion { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string Acronym { get; set;}

        public CriterionWeightModel()
        {
            UserId = "";
            UserAccesObject = -1;
            Criterion = -1;
            Weight = -1;
            Acronym = "";
        }

        /// <summary>
        /// Creates a new CriterionWeightModel
        /// </summary>
        /// <param name="userId">Access Object ID of User</param>
        /// <param name="criterion">Criterion ID</param>
        /// <param name="weight"></param>
        public CriterionWeightModel(int userAccesObject, int criterion, double weight)
        {
            UserId = "";
            UserAccesObject = userAccesObject;
            Criterion = criterion;
            Weight = weight;
            Acronym = "";
        }

        /// <summary>
        /// Creates a new CriterionWeightModel
        /// </summary>
        /// <param name="user">Access Object ID of User</param>
        /// <param name="criterion">Criterion ID</param>
        /// <param name="weight"></param>
        public CriterionWeightModel(string userId, int criterion, double weight)
        {
            UserAccesObject = -1;
            UserId = userId;
            Criterion = criterion;
            Weight = weight;
            Acronym = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criterion">criterion id</param>
        /// <param name="weight"></param>
        public CriterionWeightModel(int criterion, double weight)
        {
            Criterion = criterion;
            Weight = weight;
            Acronym = "";
        }

        public CriterionWeightModel(int userAccesObject, string userId, int criterion, double weight)
        {
            UserAccesObject = userAccesObject;
            UserId = userId;
            Criterion = criterion;
            Weight = weight;
            Acronym = "";
        }
    }
}