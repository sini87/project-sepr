using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    [DataContractAttribute]
    public class IssueModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public List<TagModel> Tags { get; set; }
        [DataMember]
        public double ReviewRating { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ArtefactModel> Artefacts {get; set; }
        [DataMember]
        public List<StakeholderModel> Stakeholders { get; set; }
        [DataMember]
        public List<InfluenceFactorModel> InfluenceFactors { get; set; }
        [DataMember]
        public List<string> Documents { get; set; }
        [DataMember]
        public Dictionary<int,char> AccessRights { get; set; }
        [DataMember]
        public int RelatedTo { get; set; }
        [DataMember]
        public char RelationType { get; set; }
        [DataMember]
        public List<CriterionModel> Criterions { get; set; }
        [DataMember]
        public List<CriterionWeightModel> CriterionWeights { get; set; }
        [DataMember]
        public List<AccessRightModel> AccessUserList { get; set; }

        public IssueModel()
        {
            AccessUserList = new List<AccessRightModel>();
            CriterionWeights = new List<CriterionWeightModel>();
            Tags = new List<TagModel>();
            Artefacts = new List<ArtefactModel>();
            Stakeholders = new List<StakeholderModel>();
            InfluenceFactors = new List<InfluenceFactorModel>();
            Documents = new List<string>();
            AccessRights = new Dictionary<int, char>();
            Criterions = new List<Models.CriterionModel>();
            ReviewRating = 0.0;
            Status = "";
            Title = "";
            Description = "";
        }

        public IssueModel(int id, string title, string status, double reviewRating)
        {
            AccessUserList = new List<AccessRightModel>();
            CriterionWeights = new List<CriterionWeightModel>();
            Criterions = new List<Models.CriterionModel>();
            Tags = new List<TagModel>();
            Artefacts = new List<ArtefactModel>();
            Stakeholders = new List<StakeholderModel>();
            InfluenceFactors = new List<InfluenceFactorModel>();
            Documents = new List<string>();
            AccessRights = new Dictionary<int, char>();
            Id = id;
            Title = title;
            ReviewRating = reviewRating;
            Status = status;
        }      
    }
}