using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{

    public class CriterionRepository : RepositoryBase
    {
        /// <summary>
        /// returns a Criterion by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal CriterionModel GetCriterion(int id)
        {
            CriterionModel criterion = new CriterionModel();
            IssueRepository iRep = new IssueRepository();
            IEnumerable<Criterion> query1 = from Criterion in ctx.Criterion
                                        where
                                          Criterion.Id == id
                                        select Criterion;

            criterion.Id = query1.First().Id;
            criterion.Name = query1.First().Name;
            criterion.Description = query1.First().Description;
            criterion.Issue = query1.First().Issue;
            criterion.Issue1 = iRep.GetIssueDetailed(criterion.Issue);
            criterion.Weight = (Double)query1.First().Weight;
            return criterion;
        }//EndGetCriterion(id)

        /// <summary>
        /// Returns all Criterias
        /// </summary>
        /// <returns></returns>
        internal List<CriterionModel> getAllCriterias()
        {
            List<CriterionModel> criterionList = new List<CriterionModel>();
            IssueRepository iRep = new IssueRepository();
            var query = from Criterion in ctx.Criterion
                        select new
                        {
                            Id = Criterion.Id,
                            Name = Criterion.Name,
                            Description = Criterion.Description,
                            Issue = Criterion.Issue,
                            Weight = Criterion.Weight
                        };
            foreach (var c in query)
            {
                CriterionModel criterionListItem = new CriterionModel();
                criterionListItem.Id = c.Id;
                criterionListItem.Name = c.Name;
                criterionListItem.Description = c.Description;
                criterionListItem.Issue = c.Issue;
                criterionListItem.Weight = (Double)c.Weight;
                foreach (var cl in criterionList)
                {
                    cl.Issue1 = iRep.GetIssueDetailed(cl.Issue);
                }
                criterionList.Add(criterionListItem);
            }
            return criterionList;
        }

        /// <summary>
        /// returns true if Criterion is duplicated, else return false
        /// </summary>
        /// <param name="name"></param>
        /// <param name="issueId"></param>
        /// <returns></returns>
        internal bool isDuplicate(CriterionModel criterion)
        {
            int i = 0;
            var query = from Criterion in ctx.Criterion
                        where
                          Criterion.Description == criterion.Description &&
                          Criterion.Issue == criterion.Issue
                        select new
                        {
                            Name = Criterion.Name,
                            Issue = Criterion.Issue,
                            Weight = Criterion.Weight
                        };
            foreach (var c in query) i++;
            if (i > 0) return true;
            else return false;
        }

        /// <summary>
        /// Adds new Criterion
        /// </summary>
        /// <param name="criterion"></param>
        internal void AddCriterion(CriterionModel criterion)
        {
            Criterion criterionLinq = new Criterion();
            var query = from Criterion in
                            (from Criterion in ctx.Criterion
                             select new
                             {
                                 Criterion.Id,
                                 Dummy = "x"
                             })
                        group Criterion by new { Criterion.Dummy } into g
                        select new
                        {
                            Column1 = (int?)g.Max(p => p.Id)
                        };
            foreach (var c in query)
            {
                criterion.Id = c.Column1.Value;
            }
            criterionLinq.Id = criterion.Id+1;
            criterionLinq.Name = criterion.Name;
            criterionLinq.Description = criterion.Description;
            criterionLinq.Weight = criterion.Weight;
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
            IEnumerable<Criterion> query1 = from Criterion in ctx.Criterion
                                            where
                                              Criterion.Id == criterion.Id
                                            select Criterion;
            IEnumerable<Issue> query2 = from Issues in ctx.Issues
                                        where
                                          Issues.Id == criterion.Id
                                        select Issues;
            if (query1.Count() > 0 && query2.Count() >0)
            {
                query1.First().Name = criterion.Name;
                query1.First().Description = criterion.Description;
                query1.First().Issue = criterion.Issue;
                query1.First().Issue1 = query2.First();
                query1.First().Weight=criterion.Weight;
                ctx.SubmitChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Deletes a Criterion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean DeleteCriterion(int id, String name)
        {
            Criterion criterion = new Criterion();
            IEnumerable<Criterion> query1 = from Criterion in ctx.Criterion
                                            where
                                              Criterion.Id == id &&
                                              Criterion.Name == name
                                            select Criterion;
            if (query1.Count() > 0)
            {
                ctx.Criterion.DeleteOnSubmit(query1.First());
                ctx.SubmitChanges();
                return true;
            }
            else return false;
        }

        public Boolean SetCriterionWeight(int id, Double weight)
        {
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