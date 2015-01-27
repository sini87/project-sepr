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
    /// <summary>
    /// Controller for Alternative
    /// </summary>
    [Authorize]
    [EnableCors("http://localhost:51853", "*", "*")]
    public class AlternativeController : ApiController
    {
        private AlternativeRepository altRepo;

        public AlternativeController()
        {
            altRepo = new AlternativeRepository();
        }

        /// <summary>
        /// returns all alternatives of an issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<AlternativeModel> Get(int issueId)
        {
            return altRepo.GetAlternativesByIssueId(issueId);
        }

        /// <summary>
        /// creates a new alternative
        /// </summary>
        /// <param name="altModel">Alternative Model</param>
        /// <returns>HTTP OK if successfully created, else HTTP not acceptable</returns>
        [HttpPost]
        [Route("api/Alternative/Create")]
        public HttpResponseMessage Create(AlternativeModel altModel)
        {
            if (altRepo.CreateAlternativeForIssue(altModel))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// updates an alternative
        /// </summary>
        /// <param name="altModel">Alternative Model</param>
        /// <returns>HTTP OK if successfully updated, else HTTP not acceptable</returns>
        [HttpPost]
        [Route("api/Alternative/Update")]
        public HttpResponseMessage Update(AlternativeModel altModel)
        {
            if (altRepo.UpdateAlternative(altModel))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// deletes an alternative
        /// </summary>
        /// <param name="alternativeId">integer id of the alternative</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int alternativeId)
        {
            if (altRepo.DeleteAlternative(alternativeId))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }
    }
}
