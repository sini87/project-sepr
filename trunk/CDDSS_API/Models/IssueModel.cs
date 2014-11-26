﻿using CDDSS_API.Models.Domain;
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

        public IssueModel()
        {
            Tags = new List<TagModel>();
            Artefacts = new List<ArtefactModel>();
            Stakeholders = new List<StakeholderModel>();
            InfluenceFactors = new List<InfluenceFactorModel>();
            Documents = new List<string>();
            ReviewRating = 0.0;
            Status = "";
            Title = "";
            Description = "";
        }

        public IssueModel(int id, string title, string status, double reviewRating)
        {
            Tags = new List<TagModel>();
            Artefacts = new List<ArtefactModel>();
            Stakeholders = new List<StakeholderModel>();
            InfluenceFactors = new List<InfluenceFactorModel>();
            Documents = new List<string>();
            Id = id;
            Title = title;
            ReviewRating = reviewRating;
            Status = status;
        }      
    }
}