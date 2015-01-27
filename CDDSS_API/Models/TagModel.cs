using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing a tag
    /// </summary>
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagModel()
        {
            Name = "";
            Id = 0;
        }

        public TagModel(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public TagModel(string name)
        {
            Name = name;
            Id = 0;
        }
    }
}