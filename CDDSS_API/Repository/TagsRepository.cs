using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class TagsRepository :RepositoryBase
    {
        public List<TagModel> GetAllTags()
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            List<TagModel> list = new List<TagModel>();
            foreach (Tag tag in ctx.Tags.ToList())
            {
                list.Add(new TagModel(tag.Id, tag.Name));
            }
            return list;
        }
    }
}