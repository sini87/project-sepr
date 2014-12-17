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
    [Authorize]
    [EnableCors("http://localhost:51853", "*", "*")]
    public class DecisionController : ApiController
    {
        private DecisionRepository drep;

        public DecisionController()
        {
            drep = new DecisionRepository();
        }

        /// <summary>
        /// retrieves a decision by issueID
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Decision")]
        public DecisionModel GetDecision(int issueID)
        {
            return drep.GetDecision(issueID);
        }

        /// <summary>
        /// creates a decision
        /// </summary>
        /// <param name="decision"></param>
        /// <returns>statuscode OK if successfull</returns>
        [HttpPost]
        [Route("api/Decision/Create")]
        public HttpResponseMessage CreateDecision(DecisionModel decision)
        {
            if (drep.CreateDecision(decision))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// edits an decision
        /// </summary>
        /// <param name="decision"></param>
        /// <returns>statuscode OK if successfull</returns>
        [HttpPost]
        [Route("api/Decision/Edit")]
        public HttpResponseMessage EditDecision(DecisionModel decision)
        {
            if (drep.EditDecision(decision))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// deletes an decision by issueId
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns>statuscode OK if successfull</returns>
        [HttpDelete]
        [Route("api/Decision")]
        public HttpResponseMessage CreateDecision(int issueID)
        {
            if (drep.DeleteDecision(issueID))
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
