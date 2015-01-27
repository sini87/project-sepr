using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing an alternative
    /// </summary>
    [DataContract]
    public class AlternativeModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Issue { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public double Rating { get; set; }

        public AlternativeModel()
        {
            Id = -1;
            Issue = -1;
            Name = "";
            Description = "";
            Reason = "";
            Rating = -1.0;
        }

        public AlternativeModel(int issue, string name)
        {
            Id = -1;
            Issue = issue;
            Name = name;
            Description = "";
            Reason = "";
            Rating = -1;
        }

        public AlternativeModel(int issue, string name, string description, string reason)
        {
            Issue = issue;
            Id = -1;
            Name = name;
            Description = description;
            Reason = reason;
            Rating = -1;
        }

        public AlternativeModel(int id, int issue, string name, string description, string reason)
        {
            Id = id;
            Issue = issue;
            Name = name;
            Description = description;
            Reason = reason;
            Rating = -1;
        }

        public AlternativeModel(int id, int issue, string name, string description, string reason, double rating)
        {
            Issue = issue;
            Id = id;
            Name = name;
            Description = description;
            Reason = reason;
            Rating = rating;
        }
    }
}