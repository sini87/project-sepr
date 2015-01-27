using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding an Review
    /// </summary>
    public class ReviewRepository : RepositoryBase
    {
        /// <summary>
        /// Adds a review for an Issue
        /// </summary>
        /// <param name="rm">ReviewModel</param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succeded</returns>
        public bool AddReview(ReviewModel rm, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            try { 
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                if (ctx.Reviews.Where(x => x.User == userId && x.Issue == rm.Issue).Count() == 0) { 
                    Review r = new Review();
                    r.User = userId;
                    r.Rating = rm.Rating;
                    r.Explanation = rm.Explanation;
                    r.Issue = rm.Issue;
                    ctx.Reviews.InsertOnSubmit(r);
                    ctx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// returns all reviews of an Issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public List<ReviewModel> GetReviewsOfIssue(int issueId)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            List<ReviewModel> rmList = new List<ReviewModel>();
            ReviewModel rm;

            try
            {
                foreach (Review r in ctx.Reviews.Where(x => x.Issue == issueId))
                {
                    rm = new ReviewModel();
                    rm.UserLastName = r.User1.LastName;
                    rm.UserFirstName = r.User1.FirstName;
                    rm.Explanation = r.Explanation;
                    rm.Rating = r.Rating;
                    rm.Issue = issueId;
                    rmList.Add(rm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rmList;
        }

        /// <summary>
        /// edits an review for an Issue
        /// </summary>
        /// <param name="rm">ReviewModel</param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succeded</returns>
        public bool EditReview(ReviewModel rm, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            try
            {
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                List<Review> rList = ctx.Reviews.Where(x => x.Issue == rm.Issue && x.User == userId).ToList();
                if (rList.Count() > 0)
                {
                    Review r = rList.First();
                    r.Rating = rm.Rating;
                    r.Explanation = rm.Explanation;
                    ctx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// deletes a review
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succededs</returns>
        public bool DeleteReview(int issueId, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            try
            {
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                List<Review> rList = ctx.Reviews.Where(x => x.Issue == issueId && x.User == userId).ToList();
                if (rList.Count() > 0)
                {
                    Review r = rList.First();
                    ctx.Reviews.DeleteOnSubmit(r);
                    ctx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}