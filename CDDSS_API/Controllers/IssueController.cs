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
        /// returns an issue detailed by issueid - FINISHED
        /// </summary>
        /// <param name="issueId">id of issue</param>
        /// <returns>issueModel</returns>
        public IssueModel GET(int issueId)
        {
            return issueRep.GetIssueDetailed(issueId);
        }

        /// <summary>
        /// Creates a new Issue - FINISHED
        /// Adding Artefacts, Tags, Stakeholders: If you add a tags (artefacts, stakeholders) wihtout an id, then they will be created as new. If You add those things with an id, make sure that you get this id throuth the right API (e.g. api/Tags)
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>TODO - response code</returns>
        public string Create(IssueModel issue)
        {
            issueRep.CreateIssue(issue, RequestContext.Principal.Identity.Name);
            return "ok";
        }

        /// <summary>
        /// returns a list of issues with title - FINISHED
        /// </summary>
        /// <returns></returns>
        public List<IssueModel> GET()
        {
            return issueRep.GetAllIssues();
        }

        /// <summary>
        /// returns a list of issues with title - FINISHED
        /// </summary>
        /// <returns></returns>
        [Route("api/IssuesFromUser")]
        public List<IssueModel> GETIssuesByUser()
        {
            return issueRep.GetUserIssues(RequestContext.Principal.Identity.Name);
        }
    }
}
