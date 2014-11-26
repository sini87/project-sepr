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
    /// TODO
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class IssueController : ApiController
    {
        IssueRepository issueRep;

        public IssueController()
        {
            issueRep = new IssueRepository();
        }

        /// <summary>
        /// returns an issue by issueid
        /// </summary>
        /// <param name="issueId">id of issue</param>
        /// <returns>issue</returns>
        public string GET(int issueId)
        {
            return null;
        }

        /// <summary>
        /// Creates a new Issue TODO
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>TODO - response code</returns>
        public string Create(Issue issue)
        {
            return "ok";
        }

        /// <summary>
        /// returns a list of issues with title TODO
        /// </summary>
        /// <returns></returns>
        public List<IssueShort> GET()
        {
            issueRep.GetAllIssues();
            return null;
        }

        /// <summary>
        /// returns a list of issues with title TODO
        /// </summary>
        /// <returns></returns>
        [Route("api/IssuesFromUser")]
        public List<IssueShort> GETIssuesByUser()
        {
            return issueRep.GetAllIssues();
        }
    }
}
