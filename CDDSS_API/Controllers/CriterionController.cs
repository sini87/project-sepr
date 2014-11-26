using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CDDSS_API.Repository;

namespace CDDSS_API.Controllers
{
    [EnableCors("http://localhost:51853", "*", "*")]
    public class CriterionController : ApiController
    {
        private CriterionRepository cRep;

        public CriterionController()
        {
            cRep = new CriterionRepository();
        }

        /// <summary>
        /// Returns a Criterion by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Criterion Get(int id)
        {
            return cRep.GetCriterion(id);
        }

        /// <summary>
        /// Returns all Criterions of one issue by issueID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public List<Criterion> GetIssueCriterias(int issueID)
        {
            return cRep.GetCriteriasByIssue(issueID);
        }

        /// <summary>
        /// BRAINSTORMING
        /// Adds new Criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(Criterion criterion)
        {
            return new HttpResponseMessage();
        }
        
        /// <summary>
        /// BRAINSTORMING
        /// Edit a specific Criterion
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="IssueID"></param>
        /// <returns></returns>
        public Criterion Edit(int ID, String name, int IssueID)
        {
            return new Criterion();
        }

        /// <summary>
        /// Deletes a specific Criterion from Issue
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, String name)
        {
            return new HttpResponseMessage(); 
        }
        
    }
}
