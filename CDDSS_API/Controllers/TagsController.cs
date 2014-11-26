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
    }
}
