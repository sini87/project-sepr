using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class RatingRepository : RepositoryBase
    {
        /// <summary>
        /// Returns a specific Rating
        /// </summary>
        /// <param name="criterionId"></param>
        /// <param name="alternativeId"></param>
        /// <returns></returns>
        internal RatingModel getRating(int criterionId, int alternativeId)
        {
            RatingModel rating = new RatingModel();
            CriterionRepository cRep = new CriterionRepository();
            IEnumerable<Rating> query1 = from Rating in ctx.Rating
                        where
                          Rating.Criterion == criterionId &&
                          Rating.Alternative == alternativeId
                        select Rating;
            rating.CriterionID = query1.First().Criterion;
            rating.AlternativeID = query1.First().Alternative;
            rating.User = query1.First().User;
            rating.Rating1 = query1.First().Rating1;
            rating.Alternative1 = new AlternativeModel(query1.First().Alternative1.Id, query1.First().Alternative1.Issue, query1.First().Alternative1.Name, query1.First().Alternative1.Description, query1.First().Alternative1.Reason, (Double)query1.First().Alternative1.Rating);
            rating.Criterion = cRep.GetCriterion(criterionId);
            return rating;
        }

        /// <summary>
        /// Returns all Ratings
        /// </summary>
        /// <returns></returns>
        internal List<RatingModel> getAllRatings()
        {
            List<RatingModel> ratingList = new List<RatingModel>();
            CriterionRepository cRep = new CriterionRepository();
            IEnumerable<Rating> query = from Rating in ctx.Rating
                        select Rating;
            foreach (var r in query)
            {
                RatingModel rating = new RatingModel();
                rating.CriterionID = r.Criterion;
                rating.AlternativeID = r.Alternative;
                rating.User = r.User;
                rating.Rating1 = r.Rating1;
                rating.Alternative1 = new AlternativeModel(r.Alternative1.Id, r.Alternative1.Issue, r.Alternative1.Name, r.Alternative1.Description, r.Alternative1.Reason, (Double)r.Alternative1.Rating);
                rating.Criterion = cRep.GetCriterion(r.Criterion);
                ratingList.Add(rating);
            }
            return ratingList;
        }

        /// <summary>
        /// Inserts a Rating
        /// </summary>
        /// <param name="ratingModel"></param>
        /// <returns></returns>
        internal Boolean AddRating(RatingModel ratingModel)
        {
            //TODO TEST
            Rating rating = new Rating();
            rating.Alternative = ratingModel.AlternativeID;
            rating.Criterion = ratingModel.CriterionID;
            rating.User = ratingModel.User;
            rating.Rating1 = ratingModel.Rating1;
            ctx.Rating.InsertOnSubmit(rating);
            ctx.SubmitChanges();
            return true;
        }
    }
}