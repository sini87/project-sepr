using CDDSS_API.Models;
using CDDSS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CDDSS_API.Controllers
{
    public class RatingController : ApiController
    {
        RatingRepository rRep;
        CriterionRepository cRep;

        public RatingController()
        {
            rRep = new RatingRepository();
        }

        /// <summary>
        /// Returns a specific Rating
        /// </summary>
        /// <param name="criterionId"></param>
        /// <param name="alternativeId"></param>
        /// <returns></returns>
        public RatingModel getRating(int criterionId, int alternativeId)
        {
            RatingModel rating = rRep.getRating(criterionId, alternativeId);
            return rating;
        }

        /// <summary>
        /// Returns all Ratings
        /// </summary>
        /// <returns></returns>
        public List<RatingModel> getAllRatings()
        {
            return rRep.getAllRatings();
        }

        /// <summary>
        /// Inserts new Rating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RatingModel rating)
        {
            if (rRep.AddRating(rating))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Rating created!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Rating NOT created!");

        }

        /// <summary>
        /// Updates a Rating
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(RatingModel rating)
        {
            if (rRep.UpdateRating(rating)) return Request.CreateResponse(HttpStatusCode.OK, "Rating updated!");
            else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Rating NOT updated!");
        }

        /// <summary>
        /// Deletes a Rating
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int criterionid, int alternativeid)
        {
            if(rRep.DeleteRating(criterionid, alternativeid)) return Request.CreateResponse(HttpStatusCode.OK, "Rating deleted!"); 
            else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Rating NOT deleted!");
        }
    }
}
