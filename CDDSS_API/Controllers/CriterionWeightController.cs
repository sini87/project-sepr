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
    public class CriterionWeightController : ApiController
    {
        CriterionWeightRepository cwRepo;

        public CriterionWeightController()
        {
            cwRepo = new CriterionWeightRepository();
        }

        /// <summary>
        /// Adds criterion weights to a group of criteria of the logged in user
        /// </summary>
        /// <param name="list">group of criterion weights, user and accessobject must not be set</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/CriterionWeight/Add")]
        public HttpResponseMessage AddCriterionWeights(List<CriterionWeightModel> list)
        {
            if (cwRepo.AddCriterionWeights(list, RequestContext.Principal.Identity.Name))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Something went wrong");
            }
        }

        /// <summary>
        /// Returns all criterion weights of an issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CriterionWeightModel> GetCriterionWeightsForIssue(int issueId)
        {
            return cwRepo.GetCriterionWeights(issueId);
        }

        /// <summary>
        /// updates criterion wieghts of the current logged user
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/CriterionWeight/Update")]
        public HttpResponseMessage EditCriterionWeights(List<CriterionWeightModel> list)
        {
            if (cwRepo.UpdateCriterionWeights(list, RequestContext.Principal.Identity.Name))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Something went wrong");
            }
        }

        /// <summary>
        /// deletes criterion weights for issue of logged in user
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteCriterionWeights(int issueId)
        {
            if (cwRepo.DeleteCriterionWeights(issueId, RequestContext.Principal.Identity.Name))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Something went wrong");
            }
        }
    }
}
