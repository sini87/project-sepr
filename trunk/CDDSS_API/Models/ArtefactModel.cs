using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    /// <summary>
    /// data model class representing an artefact
    /// </summary>
    public class ArtefactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ArtefactModel()
        {
            Id = 0;
            Name = "";
        }

        public ArtefactModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public ArtefactModel(string name)
        {
            Name = name;
            Id = 0;
        }
    }
}