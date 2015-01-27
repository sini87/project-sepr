using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding a Decision
    /// </summary>
    public class DecisionRepository : RepositoryBase
    {
        /// <summary>
        /// creates a Decision
        /// </summary>
        /// <param name="decision"></param>
        /// <returns>true if operation succeded</returns>
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

        /// <summary>
        /// deletes an Decision
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns>true if succeded</returns>
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

        /// <summary>
        /// edits an decision
        /// </summary>
        /// <param name="decision"></param>
        /// <returns>true if opearation succeded</returns>
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

        /// <summary>
        /// returns an Decision
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
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