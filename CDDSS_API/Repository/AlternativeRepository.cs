using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class AlternativeRepository : RepositoryBase
    {
        public List<AlternativeModel> GetAlternativesByIssueId(int issueId)
        {
            List<AlternativeModel> list = new List<AlternativeModel>();
            AlternativeModel altModel;

            foreach (Alternative alt in ctx.Alternatives.Where(x => x.Issue == issueId).ToList())
            {
                altModel = new AlternativeModel();
                altModel.Id = alt.Id;
                altModel.Name = alt.Name;
                altModel.Issue = alt.Issue;

                if (alt.Description != null)
                {
                    altModel.Description = alt.Description;
                }
                if (alt.Reason != null)
                {
                    altModel.Reason = alt.Reason;
                }
                if (alt.Rating != null)
                {
                    altModel.Rating = (double)alt.Rating;
                }
                list.Add(altModel);
            }

            return list;
        }

        public bool CreateAlternativeForIssue(AlternativeModel altModel)
        {
            Alternative alt = new Alternative();

            try
            {
                var query = from Alternatives in
                                (from Alternatives in ctx.Alternatives
                                 select new
                                 {
                                     Alternatives.Id,
                                     Dummy = "x"
                                 })
                            group Alternatives by new { Alternatives.Dummy } into g
                            select new
                            {
                                MaxId = (int)g.Max(p => p.Id)
                            };

                int altId;
                if (query.ToList().Count == 0)
                {
                    altId = 1;
                }
                else
                {
                    altId = query.ToList()[0].MaxId + 1;
                }

                alt.Id = altId;
                alt.Name = altModel.Name;
                alt.Issue = altModel.Issue;
                alt.Description = altModel.Description;
                alt.Reason = altModel.Reason;
                ctx.Alternatives.InsertOnSubmit(alt);
                ctx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public bool UpdateAlternative(AlternativeModel altModel)
        {
            if (ctx.Alternatives.Where(x => x.Id == altModel.Id).ToList().Count <= 0){
                return false;
            }else{
                Alternative alt = ctx.Alternatives.Where(x => x.Id == altModel.Id).First();
                alt.Name = altModel.Name;
                alt.Reason = altModel.Reason;
                alt.Description = altModel.Description;
                ctx.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool DeleteAlternative(int altId)
        {
            if (ctx.Alternatives.Where(x => x.Id == altId).ToList().Count <= 0)
            {
                return false;
            }
            else
            {
                Alternative alt = ctx.Alternatives.Where(x => x.Id == altId).First();
                ctx.Alternatives.DeleteOnSubmit(alt);
                ctx.SubmitChanges();
                return true;
            }

            return false;
        }
    }
}