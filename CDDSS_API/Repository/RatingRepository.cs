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
            DataClassesDataContext ctx = new DataClassesDataContext();
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
            return rating;
        }

        /// <summary>
        /// Returns all Ratings
        /// </summary>
        /// <returns></returns>
        internal List<RatingModel> getAllRatings()
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
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
                ratingList.Add(rating);
            }
            return ratingList;
        }

        /// <summary>
        /// Returns all Ratings for an Issue of current user
        /// </summary>
        /// <returns></returns>
        internal List<RatingModel> getAllRatings(int issueID, string eMail)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<User> q = from Users in ctx.Users
                                      where Users.Email == eMail
                                      select Users;

            string usr = q.First().Id;

            List<RatingModel> ratingList = new List<RatingModel>();
            CriterionRepository cRep = new CriterionRepository();


            foreach (var r in ctx.Rating.Where(x => x.Alternative1.Issue == issueID && x.User == usr))
            {
                RatingModel rating = new RatingModel();
                rating.CriterionID = r.Criterion;
                rating.AlternativeID = r.Alternative;
                rating.User = r.User;
                rating.Rating1 = r.Rating1;
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
            DataClassesDataContext ctx = new DataClassesDataContext();
            //TODO TEST
            Rating rating = new Rating();
            rating.Alternative = ratingModel.AlternativeID;
            rating.Criterion = ratingModel.CriterionID;
            IEnumerable<User> query = from Users in ctx.Users
                                      where Users.Email==ratingModel.User
                                        select Users;
            rating.User = query.First().Id;
            rating.Rating1 = ratingModel.Rating1;
            ctx.Rating.InsertOnSubmit(rating);
            ctx.SubmitChanges();
            return true;
        }

        internal bool AddRatingForIssue(List<RatingModel> rl, string eMail)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<User> query = from Users in ctx.Users
                                      where Users.Email == eMail
                                      select Users;

            foreach (RatingModel ratingModel in rl)
            {
                Rating rating = new Rating();
                rating.Alternative = ratingModel.AlternativeID;
                rating.Criterion = ratingModel.CriterionID;
                rating.User = query.First().Id;
                rating.Rating1 = ratingModel.Rating1;
                ctx.Rating.InsertOnSubmit(rating);
                ctx.SubmitChanges();
            }
            
            return true;
        }

        internal bool UpdateRatingForIssue(List<RatingModel> rl, string eMail)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<User> query = from Users in ctx.Users
                                      where Users.Email == eMail
                                      select Users;

            Rating rating;
            IQueryable<Rating> qu;
            string usr = query.First().Id;
            foreach (RatingModel ratingModel in rl)
            {
                qu = ctx.Rating.Where(x => x.User == usr && x.Alternative == ratingModel.AlternativeID && x.Criterion == ratingModel.CriterionID);
                if (qu.Count() > 0)
                {
                    rating = qu.First();
                    rating.Rating1 = ratingModel.Rating1;
                    ctx.SubmitChanges();
                }
            }
            
            return true;
        }

        /// <summary>
        /// Updates Rating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        internal Boolean UpdateRating(RatingModel rating)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<Rating> query = from Rating in ctx.Rating
                                        where Rating.Criterion==rating.CriterionID&&
                                              Rating.Alternative==rating.AlternativeID
                                        select Rating;
            query.First().Criterion = rating.CriterionID;
            query.First().Alternative = rating.AlternativeID;
            IEnumerable<User> query1 = from Users in ctx.Users
                                      where Users.Email == rating.User
                                      select Users;
            rating.User = query1.First().Id;
            query.First().Rating1 = rating.Rating1;
            ctx.SubmitChanges();
            IEnumerable<Rating> query2 = from Rating in ctx.Rating
                                        where Rating.Criterion==rating.CriterionID&&
                                              Rating.Alternative==rating.AlternativeID&&
                                              Rating.User==rating.User&&
                                              Rating.Rating1==rating.Rating1
                                        select Rating;
            if (query2.Count() > 0) return true;
            else return false;
        }

        /// <summary>
        /// Deletes a Rating
        /// </summary>
        /// <param name="criterionid"></param>
        /// <param name="alternativeid"></param>
        /// <returns></returns>
        internal Boolean DeleteRating(int criterionid, int alternativeid)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<Rating> query = from Rating in ctx.Rating
                                        where Rating.Criterion == criterionid &&
                                              Rating.Alternative == alternativeid
                                        select Rating;
            if (query.Count() > 0)
            {
                ctx.Rating.DeleteOnSubmit(query.First());
                ctx.SubmitChanges();
                query = from Rating in ctx.Rating
                                            where Rating.Criterion == criterionid &&
                                                  Rating.Alternative == alternativeid
                                            select Rating;
                if (query.Count() == 0) return true;
                else return false;       
            }
            else return false;
        }
    }
}