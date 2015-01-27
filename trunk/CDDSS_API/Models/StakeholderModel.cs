using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing a stakeholder
    /// </summary>
    public class StakeholderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StakeholderModel()
        {
            Id = 0;
            Name = "";
        }

        public StakeholderModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public StakeholderModel(string name)
        {
            Name = name;
            Id = 0;
        }
    }
}