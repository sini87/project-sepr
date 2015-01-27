using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing an InfluenceFactor
    /// </summary>
    public class InfluenceFactorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Type { get; set;}
        public string Characteristic { get; set; }

        public InfluenceFactorModel()
        {
            Id = 0;
            Name = "";
            Type = false;
            Characteristic = "";
        }

        public InfluenceFactorModel(string name, bool type, string characteristic)
        {
            Name = name;
            Type = type;
            Characteristic = characteristic;
        }

        public InfluenceFactorModel(string name, bool type)
        {
            Name = name;
            Type = type;
            Characteristic = "";
        }

        public InfluenceFactorModel(int id, string name, bool type, string characteristic)
        {
            Id = id;
            Name = name;
            Type = type;
            Characteristic = "";
            Characteristic = characteristic;
        }
    }
}