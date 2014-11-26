using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Models
{
    public class IssueShort
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Tag> Tags { get; set; }
        public double Review { get; set; }

        public IssueShort()
        {
            Tags = new List<Tag>();
        }

        public IssueShort(int id, string title)
        {
            Tags = new List<Tag>();
            Id = id;
            Title = title;
        }
    }
}