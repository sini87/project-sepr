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
        internal Criterion GetCriterion(int id)
        {
            Criterion criterion = new Criterion();
            var query = from Criterion in ctx.Criterion
                        where
                          Criterion.Id == id
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
                criterion.Id = c.Id;
                criterion.Issue = c.Issue;
                criterion.Name = c.Name;
                criterion.Weight = c.Weight;
                criterion.Description = c.Description;
            }
            return criterion;
        }//EndGetCriterion(id)

        /// <summary>
        /// Returns all Criterias
        /// </summary>
        /// <returns></returns>
        internal List<Criterion> getAllCriterias()
        {
            List<Criterion> criterionList = new List<Criterion>();
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
                Criterion criterionListItem = new Criterion();
                criterionListItem.Id = c.Id;
                criterionListItem.Name = c.Name;
                criterionListItem.Description = c.Description;
                criterionListItem.Issue = c.Issue;
                criterionListItem.Weight = c.Weight;
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

            criterionLinq.Id = criterion.Id;
            criterionLinq.Name = criterion.Name;
            criterionLinq.Description = criterion.Description;
            criterionLinq.Weight = criterion.Weight;
            criterionLinq.Issue = criterion.Issue;
            //criterionLinq.Issue1 = criterion.Issue1;
            ctx.Criterion.InsertOnSubmit(criterionLinq);
            ctx.SubmitChanges();
        }
    }//EndClass
}