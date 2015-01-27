using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding a Tag
    /// </summary>
    public class TagsRepository :RepositoryBase
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        /// <summary>
        /// returns all Tags
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns all Tags of an Issue
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public List<TagModel> getTagsOfIssue(int issueID)
        {
            List<TagModel> tagsList = new List<TagModel>();
            IEnumerable<Tag> issueTags = from Tag in ctx.Tags
                                              where
                                                Tag.Id == issueID
                                              select Tag;
            foreach (var titem in issueTags)
            {
                tagsList.Add(new TagModel(titem.Id, titem.Name));
            }
            return tagsList;
        }

        /// <summary>
        /// Edits a specific Tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Boolean EditTag(TagModel tag)
        {
            IEnumerable<Tag> Tagquery = from Tag in ctx.Tags
                                         where
                                           Tag.Id == tag.Id
                                         select Tag;
            if (Tagquery.First().Name != tag.Name)
            {
                Tagquery.First().Name = tag.Name;
                ctx.SubmitChanges();
                return true;
            }
            else return false;            
        }

        /// <summary>
        /// Deletes a specific Tag
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public Boolean DeleteTag(int tagID)
        {
            IEnumerable<Tag> Tagquery = from Tag in ctx.Tags
                                        where
                                          Tag.Id == tagID
                                        select Tag;
            if (Tagquery.Count() > 0)
            {
                ctx.Tags.DeleteOnSubmit(Tagquery.First());
                ctx.SubmitChanges();
                Tagquery = from Tag in ctx.Tags
                           where
                             Tag.Id == tagID
                           select Tag;
                if (Tagquery.Count() == 0) return true;
                else return false;
            }
            else return false;
        }

    }
}