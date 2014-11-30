using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CDDSS_API.Repository;
using CDDSS_API.Models;

namespace CDDSS_API.Controllers
{
    public enum CriterionPostResponseMessage {DuplicateEntry, AddSuccessfull};

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
        /// Returns all Criterias of CDDSS
        /// </summary>
        /// <returns></returns>
        public List<Criterion> GetAllCriterias()
        {
            return cRep.getAllCriterias();
        }

        /// <summary>
        /// BRAINSTORMING
        /// Adds new Criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns></returns>
        public CriterionPostResponseMessage Post(CriterionModel criterion)
        {
            if (cRep.isDuplicate(criterion))
                return CriterionPostResponseMessage.DuplicateEntry;
            else
            {
                cRep.AddCriterion(criterion);
                if (cRep.isDuplicate(criterion)) return CriterionPostResponseMessage.AddSuccessfull;
                else return CriterionPostResponseMessage.DuplicateEntry;
                
            }
        }
        
        /// <summary>
        /// BRAINSTORMING
        /// Updates a specific Criterion
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="IssueID"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(Criterion criterion)
        {
           if (cRep.UpdateCriterion(criterion))
                return Request.CreateResponse(HttpStatusCode.OK, "Criterion updated!");
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Criterion couldn't get updated!");
        }

        /// <summary>
        /// Deletes a specific Criterion from Issue
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, String name)
        {
            if (cRep.DeleteCriterion(id, name))
                return Request.CreateResponse(HttpStatusCode.OK, "Criterion deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Criterion not deleted!");
        }
        
    }
}
