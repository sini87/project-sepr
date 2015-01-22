using CDDSS_API.Models;
using CDDSS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Controllers
{
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class TagsController : ApiController
    {
        private TagsRepository tagsRepo;
        public TagsController()
        {
            tagsRepo = new TagsRepository();
        }

        /// <summary>
        /// returns al tags
        /// </summary>
        /// <returns></returns>
        public List<TagModel> GET()
        {
            return tagsRepo.GetAllTags();
        }

        /// <summary>
        /// returns all Tags of an Issue
        /// </summary>
        /// <param name="Issueid"></param>
        /// <returns></returns>
        public List<TagModel> GetTagByIssue(int Issueid)
        {
           return tagsRepo.getTagsOfIssue(Issueid);
        }

        /// <summary>
        /// Edits a specific Tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(TagModel tag)
        {
            if (tagsRepo.EditTag(tag))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Tag updated!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Tag NOT updated!");
            
        }

        /// <summary>
        /// Delets a specific Tag
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteTag(int tagID)
        {
            if (tagsRepo.DeleteTag(tagID)) return Request.CreateResponse(HttpStatusCode.OK, "Tag deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Tag NOT deleted!");
        }
    }
}
