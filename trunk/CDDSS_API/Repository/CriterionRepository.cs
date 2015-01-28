using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding criterion
    /// </summary>
    public class CriterionRepository : RepositoryBase
    {
        /// <summary>
        /// returns a Criterion by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal CriterionModel GetCriterion(int id)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            CriterionModel criterion = new CriterionModel();
            IEnumerable<Criterion> query1 = from Criterion in ctx.Criterion
                                        where
                                          Criterion.Id == id
                                        select Criterion;

            criterion.Id = query1.First().Id;
            criterion.Name = query1.First().Name;
            criterion.Description = query1.First().Description;
            criterion.Issue = query1.First().Issue;
            criterion.Weight = (Double)query1.First().Weight;
            IEnumerable<Rating> query2 = from Rating in ctx.Rating
                                            where
                                              Rating.Criterion == id
                                            select Rating;
            foreach (var r in query2)
            {
                criterion.Ratings.Add(new RatingModel(r.Criterion, r.Alternative, r.User, r.Rating1));
            }
            return criterion;
        }//EndGetCriterion(id)

        /// <summary>
        /// Returns all Criterias
        /// </summary>
        /// <returns></returns>
        internal List<CriterionModel> getAllCriterias()
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            List<CriterionModel> criterionList = new List<CriterionModel>();
            var query = from Criterion in ctx.Criterion
                        select Criterion;
            foreach (var c in query)
            {
               criterionList.Add(GetCriterion(c.Id));
            }
            return criterionList;
        }

        /// <summary>
        /// Adds new Criterion
        /// </summary>
        /// <param name="criterion"></param>
        internal void AddCriterion(CriterionModel criterion)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            Criterion criterionLinq = new Criterion();
            int id;
            if (ctx.Criterion.ToList().Count > 0)
            {
                var query = (from t in ctx.Criterion select t.Id).Max();
                if (query != null)
                {
                    id = query + 1;
                }
                else
                {
                    id = 1;
                }
            }
            else
            {
                id = 1;
            }
            
            
            criterionLinq.Id = id;
            criterionLinq.Name = criterion.Name;
            criterionLinq.Description = criterion.Description;
            criterionLinq.Weight = 0;
            criterionLinq.Issue = criterion.Issue;
            
            //TODO CREATE RATING
            ctx.Criterion.InsertOnSubmit(criterionLinq);
            ctx.SubmitChanges();
        }

        /// <summary>
        /// Updates a Criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns></returns>
        internal Boolean UpdateCriterion(CriterionModel criterion)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            try
            {
                Criterion c;
                IQueryable<Criterion> iq;
                iq = ctx.Criterion.Where(x => x.Id == criterion.Id);
                if (iq.Count() > 0)
                {
                    c = iq.First();
                    c.Name = criterion.Name;
                    c.Description = criterion.Description;
                    ctx.SubmitChanges(); 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes a Criterion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean DeleteCriterion(int id)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            Criterion criterion = new Criterion();
            IEnumerable<Criterion> query1 = from Criterion in ctx.Criterion
                                            where
                                              Criterion.Id == id
                                            select Criterion;
            if (query1.Count() > 0)
            {
                ctx.Criterion.DeleteOnSubmit(query1.First());
                ctx.SubmitChanges();
                query1 = from Criterion in ctx.Criterion
                                            where
                                              Criterion.Id == id
                                              select Criterion;
                if (query1.Count() == 0) return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Sets Criterionweight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public Boolean SetCriterionWeight(int id, Double weight)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IEnumerable<Criterion> query = from Criterion in ctx.Criterion
                        where
                          Criterion.Weight == Criterion.Weight
                        select Criterion;
            if (query.Count() > 0)
            {
                query.First().Weight = weight;
                ctx.SubmitChanges();
                return true;
            }
            else return false;
        }
    }//EndClass
}