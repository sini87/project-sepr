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
    public class ReviewController : ApiController
    {
        private ReviewRepository revRepo;

        public ReviewController()
        {
            revRepo = new ReviewRepository();
        }

        /// <summary>
        /// adds a new review to the issue of the logged user
        /// ReviewModel: Fields Issue and Rating are required Explanation is optional, other fields are not processed
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns>OK if added</returns>
        [HttpPost]
        [Route("api/Review/Add")]
        public HttpResponseMessage AddReview(ReviewModel reviewModel)
        {
            if (revRepo.AddReview(reviewModel, RequestContext.Principal.Identity.Name))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// returns all reviews of an issue
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>List of reviews</returns>
        public List<ReviewModel> GetReviewsFOfIssue(int issueId)
        {
            return revRepo.GetReviewsOfIssue(issueId);
        }

        /// <summary>
        /// edits a review of the issue
        /// ReviewModel: Fields Issue and Rating are required Explanation is optional, other fields are not processed
        /// </summary>
        /// <param name="reviewModel"></param>
        /// <returns>OK if edited</returns>
        [HttpPost]
        [Route("api/Review/Edit")]
        public HttpResponseMessage EditReview(ReviewModel reviewModel)
        {
            if (revRepo.EditReview(reviewModel, RequestContext.Principal.Identity.Name))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// Deletes an review for an issue of logged user
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>OK if deleted</returns>
        [HttpDelete]
        public HttpResponseMessage DeleteReview(int issueId)
        {
            if (revRepo.DeleteReview(issueId,RequestContext.Principal.Identity.Name))
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
