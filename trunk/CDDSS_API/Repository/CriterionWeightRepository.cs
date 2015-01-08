using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class CriterionWeightRepository : RepositoryBase
    {
        public bool AddCriterionWeights(List<CriterionWeightModel> list, string email)
        {
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

        public bool DeleteCriterionWeights(int issueId, string email)
        {            
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

        public bool UpdateCriterionWeights(List<CriterionWeightModel> list, string email)
        {
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

        public List<CriterionWeightModel> GetCriterionWeights(int issueId)
        {
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