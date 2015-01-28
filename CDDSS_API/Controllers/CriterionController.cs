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
        private IssueRepository iRep;

        public CriterionController()
        {
            cRep = new CriterionRepository();
            iRep = new IssueRepository();
        }

        /// <summary>
        /// Returns a Criterion by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CriterionModel Get(int id)
        {
            CriterionModel criterion = new CriterionModel();
            criterion = cRep.GetCriterion(id);
            return criterion;
        }

        /// <summary>
        /// Returns all Criterias of CDDSS
        /// </summary>
        /// <returns></returns>
        public List<CriterionModel> GetAllCriterias()
        {
            List<CriterionModel> criterionList = new List<CriterionModel>();
            criterionList = cRep.getAllCriterias();
            return criterionList;
        }

        /// <summary>
        /// Adds new Criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(CriterionModel criterion)
        {
           cRep.AddCriterion(criterion);     
           return Request.CreateResponse(HttpStatusCode.OK, "Criterion added!");
                
        }
        
        /// <summary>
        /// Updates a specific Criterion
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="IssueID"></param>
        /// <returns></returns>
        [Route("api/Criterion/Update")]
        [HttpPost]
        public HttpResponseMessage Update(CriterionModel criterion)
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
        public HttpResponseMessage Delete(int id)
        {
            if (cRep.DeleteCriterion(id))
                return Request.CreateResponse(HttpStatusCode.OK, "Criterion deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Criterion not deleted!");
        }

        /// <summary>
        /// Sets Criterion Weight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public HttpResponseMessage SetCriterionWeight(int id, int weight)
        {
            if (cRep.SetCriterionWeight(id, weight))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Criterionweight updatet!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Criterionweight not updatet!");
        }
        
    }
}
