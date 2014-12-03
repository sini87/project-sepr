using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
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
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class IssueController : ApiController
    {
        IssueRepository issueRep;

        /// <summary>
        /// constructor
        /// </summary>
        public IssueController()
        {
            issueRep = new IssueRepository();
        }

        /// <summary>
        /// returns an issue detailed by issueid
        /// </summary>
        /// <param name="issueId">id of issue</param>
        /// <returns>issueModel</returns>
        public IssueModel GET(int issueId)
        {
            return issueRep.GetIssueDetailed(issueId);
        }

        /// <summary>
        /// Creates a new Issue
        /// Adding Artefacts, Tags, Stakeholders: If you add a tags (artefacts, stakeholders) wihtout an id, then they will be created as new. If You add those things with an id, make sure that you get this id throuth the right API (e.g. api/Tags)
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>TODO - response code</returns>
        [Route("api/Issue/Create")]
        public string Create(IssueModel issue)
        {
            issueRep.CreateIssue(issue, RequestContext.Principal.Identity.Name);
            return "ok";
        }

        /// <summary>
        /// returns a list of issues which the current user is allowed to see(only id, title, tags and rieview fields are populated)
        /// </summary>
        /// <returns></returns>
        public List<IssueModel> GET()
        {
            return issueRep.GetAllIssues(RequestContext.Principal.Identity.Name);
        }

        /// <summary>
        /// returns a list of issues with title
        /// </summary>
        /// <returns></returns>
        [Route("api/Issue/OfUser")]
        public List<IssueModel> GETIssuesByUser()
        {
            return issueRep.GetUserIssues(RequestContext.Principal.Identity.Name);
        }

        /// <summary>
        /// deletes an issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>returns statuscode ok if issue is deleted, if issue does not exist or user is not owner then status code Not Acceptable</returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int issueId)
        {
            if (issueRep.DeleteIssue(issueId, RequestContext.Principal.Identity.Name))
                return new HttpResponseMessage(HttpStatusCode.OK);
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        /// <summary>
        /// the proccess gets to the next stage
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>OK if accomplished, else NotAcceptable</returns>
        [HttpPost]
        [Route("api/Issue/{issueId}/nextStage")]
        public HttpResponseMessage NextStage(int issueId)
        {
            if (issueRep.NextStage(issueId, RequestContext.Principal.Identity.Name)){
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        /// <summary>
        /// Edits an issue, only title, description, relatedTo and relationType.
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Issue/Edit")]
        public HttpResponseMessage Edit(IssueModel issue)
        {
            if (issueRep.EditIssue(issue))
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
