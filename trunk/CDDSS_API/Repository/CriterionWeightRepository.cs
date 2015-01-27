using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding CriterionWeight
    /// </summary>
    public class CriterionWeightRepository : RepositoryBase
    {
        /// <summary>
        /// adds list of criterions weights to issue
        /// </summary>
        /// <param name="list">CriterionWeight List</param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succeded</returns>
        public bool AddCriterionWeights(List<CriterionWeightModel> list, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            CriterionWeight cw;

            try
            {
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                foreach (CriterionWeightModel cwm in list)
                {
                    cw = new CriterionWeight();
                    cw.User = userId;
                    cw.Criterion = cwm.Criterion;
                    cw.Weight = cwm.Weight;
                    ctx.CriterionWeights.InsertOnSubmit(cw);
                }
                ctx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// deletes CriterionWeights of current user to some issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succeded</returns>
        public bool DeleteCriterionWeights(int issueId, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IQueryable<CriterionWeight> cwList;
            try
            {
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                List<Criterion> cList = ctx.Criterion.Where(x => x.Issue == issueId).ToList();
                foreach (Criterion c in cList)
                {
                    cwList = ctx.CriterionWeights.Where(x => x.Criterion == c.Id && x.User == userId);
                    if (cwList.Count() > 0){
                        ctx.CriterionWeights.DeleteOnSubmit(cwList.First());
                    }
                }
                ctx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// updates CriterionWeights of current user to some issue
        /// </summary>
        /// <param name="list">list of CriterionWeights</param>
        /// <param name="email">E-Mail of current user</param>
        /// <returns>true if operation succeded</returns>
        public bool UpdateCriterionWeights(List<CriterionWeightModel> list, string email)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            IQueryable<CriterionWeight> cwList;
            CriterionWeight cw;
            try
            {
                string userId = ctx.Users.Where(x => x.Email == email).First().Id;
                foreach (CriterionWeightModel cwm in list)
                {
                    cwList = ctx.CriterionWeights.Where(x => x.Criterion == cwm.Criterion && x.User == userId);
                    if (cwList.Count() > 0)
                    {
                        cw = cwList.First();
                        cw.Weight = cwm.Weight;
                    }
                }
                ctx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// returns all CriterionWeight of an Issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public List<CriterionWeightModel> GetCriterionWeights(int issueId)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            List<CriterionWeightModel> list = new List<CriterionWeightModel>();
            IEnumerable<Criterion> cList;
            IEnumerable<CriterionWeight> cwList;
            CriterionWeightModel cwm;
            try
            {
                cList = ctx.Criterion.Where(x => x.Issue == issueId);
                if (cList.Count() > 0)
                {
                    foreach (Criterion c in cList)
                    {
                        cwList = ctx.CriterionWeights.Where(x => x.Criterion == c.Id);
                        if (cwList.Count() > 0)
                        {
                            foreach (CriterionWeight cw in cwList)
                            {
                                cwm = new CriterionWeightModel((int)cw.User1.AccessObject, cw.User, cw.Criterion, cw.Weight);
                                cwm.Acronym = "";
                                if (cw.User1.FirstName != null && cw.User1.FirstName.Length > 0)
                                    cwm.Acronym = cw.User1.FirstName.PadLeft(1);
                                if (cw.User1.FirstName != null && cw.User1.LastName.Length > 0)
                                    cwm.Acronym = cwm.Acronym + cw.User1.LastName.PadLeft(1); ;
                                list.Add(cwm);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }
    }
}