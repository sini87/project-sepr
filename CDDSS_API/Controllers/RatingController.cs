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
            return null;
        }

        /// <summary>
        /// Returns all Ratings
        /// </summary>
        /// <returns></returns>
        public List<RatingModel> getAllRatings()
        {
            return null;
        }

        /// <summary>
        /// Inserts new Rating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RatingModel rating)
        {
            return null;
        }

        /// <summary>
        /// Updates a Rating
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int alternativeId, int criterionId)
        {
            return null;
        }

        /// <summary>
        /// Deletes a Rating
        /// </summary>
        /// <param name="alternativeId"></param>
        /// <param name="criterionId"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int alternativeId, int criterionId)
        {
            return null;
        }
    }
}
