using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class DecisionRepository : RepositoryBase
    {
        public bool CreateDecision(DecisionModel decision)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (ctx.Decisions.Where(x => x.IssueId == decision.IssueID).Count() == 0)
            {
                Decision decPersistent = new Decision();
                decPersistent.IssueId = decision.IssueID;
                decPersistent.AlternativeId = decision.AlternativeID;
                decPersistent.Explanation = decision.Explanation;
                ctx.Decisions.InsertOnSubmit(decPersistent);
                ctx.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool DeleteDecision(int issueID)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (ctx.Decisions.Where(x => x.IssueId == issueID).Count() > 0)
            {
                Decision dec = ctx.Decisions.Where(x => x.IssueId == issueID).First();
                ctx.Decisions.DeleteOnSubmit(dec);
                ctx.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool EditDecision(DecisionModel decision)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (ctx.Decisions.Where(x => x.IssueId == decision.IssueID).Count() > 0)
            {
                Decision dec = ctx.Decisions.Where(x => x.IssueId == decision.IssueID).First();
                dec.AlternativeId = decision.AlternativeID;
                dec.Explanation = decision.Explanation;
                ctx.SubmitChanges();
                return true;
            }
            return false;
        }

        public DecisionModel GetDecision(int issueID)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (ctx.Decisions.Where(x => x.IssueId == issueID).Count() > 0)
            {
                Decision dec = ctx.Decisions.Where(x => x.IssueId == issueID).First();
                DecisionModel d = new DecisionModel();
                d.AlternativeID = dec.AlternativeId;
                d.Explanation = dec.Explanation;
                d.IssueID = dec.IssueId;
                return d;
            }
            return null;
        }
    }
}