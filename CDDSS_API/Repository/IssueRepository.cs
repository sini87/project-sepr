using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class IssueRepository : RepositoryBase
    {
        public List<IssueShort> GetAllIssues()
        {
            List<Issue> issueList = ctx.Issues.ToList<Issue>();
            List<IssueShort> list = new List<IssueShort>();
            IssueShort issueShort;

            foreach (Issue issue in issueList){
                issueShort = new IssueShort(issue.Id, issue.Title);
                foreach (Tag_Issue ti in issue.Tag_Issues.ToList<Tag_Issue>())
                {
                    issueShort.Tags.Add(ti.Tag1);
                }
                list.Add(issueShort);
            }
            return list;
        }

        public List<IssueShort> GetUserIssues(int accessObject)
        {
            return null;
        }
    }
}