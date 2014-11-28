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
        public Criterion GetCriterion(int id)
        {
            Criterion criterion = new Criterion();;
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
        /// Return all Criterias by IssueID
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public List<Criterion> GetCriteriasByIssue(int issueID)
        {
            List<Criterion> allIssueCriterias = new List<Criterion>();
            var query = from Criterion in ctx.Criterion
                        where
                          Criterion.Issue == issueID
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
                Criterion criterion = new Criterion();
                criterion.Id = c.Id;
                criterion.Issue = c.Issue;
                criterion.Name = c.Name;
                criterion.Weight = c.Weight;
                criterion.Description = c.Description;
                allIssueCriterias.Add(criterion);
            }
            return allIssueCriterias;
        }




        internal bool isDuplicate(string p1, int p2)
        {
            throw new NotImplementedException();
        }

        internal void AddCriterion(Criterion criterion)
        {
            throw new NotImplementedException();
        }
    }//EndClass
}