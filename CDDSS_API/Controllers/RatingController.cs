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
    /// <summary>
    /// controller for rating
    /// </summary>
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
        /// Inserts new Ratings for an issue of the current user
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Rating/All")]
        public HttpResponseMessage Post(List<RatingModel> rating)
        {
            if (rRep.AddRatingForIssue(rating, User.Identity.Name))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Rating created!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Rating NOT created!");

        }

        /// <summary>
        /// Returns all Ratings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rating/All")]
        public List<RatingModel> getAllRatings(int issueID)
        {
            return rRep.getAllRatings(issueID, User.Identity.Name);
        }

        /// <summary>
        /// Updates rating for an issue of the current user
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(RatingModel rating)
        {
            if (rRep.UpdateRating(rating)) return Request.CreateResponse(HttpStatusCode.OK, "Rating updated!");
            else return Request.CreateResponse(HttpStatusCode.NotImplemented, "Rating NOT updated!");
        }

        // <summary>
        /// Updates a Rating
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Rating/UpdateAllForIssue")]
        public HttpResponseMessage Put(List<RatingModel> rating)
        {
            if (rRep.UpdateRatingForIssue(rating, User.Identity.Name)) return Request.CreateResponse(HttpStatusCode.OK, "Rating updated!");
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

        /// <summary>
        /// returns user rating for an criterion
        /// </summary>
        /// <param name="criterionID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rating/CriterionUser")]
        public List<RatingModel> GetUserCriterionRating(int criterionID, string user)
        {
            return rRep.GetUserIssueRatings(criterionID, user);
        }

        [HttpGet]
        [Route("api/Rating/ResultRatings")]
        public List<RatingModel> GetResultRatings(int issueID)
        {
            return rRep.GetIssueResRating(issueID);
        }
    }
}
