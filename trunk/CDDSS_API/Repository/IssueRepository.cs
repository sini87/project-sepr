using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CDDSS_API.Repository
{
    public class IssueRepository : RepositoryBase
    {
        public List<IssueModel> GetAllIssues(string email)
        {
            List<IssueModel> list = new List<IssueModel>();

            User user = ctx.Users.Where(x => x.Email == email).First();
            IssueModel issueShort;
            Issue issue;
            foreach (AccessRight ar in ctx.AccessRights.Where(x => x.AccessObject == user.AccessObject).ToList()){
                issue = ar.Issue1;
                if (issue.ReviewRating == null)
                {
                    issueShort = new IssueModel(issue.Id, issue.Title, issue.Status, 0);
                }
                else
                {
                    issueShort = new IssueModel(issue.Id, issue.Title, issue.Status, (double)issue.ReviewRating);
                }
                foreach (Tag_Issue ti in issue.Tag_Issues.ToList<Tag_Issue>())
                {
                    issueShort.Tags.Add(new TagModel(ti.Tag1.Id, ti.Tag1.Name));
                }
                list.Add(issueShort);
            }
            return list;
        }

        public IssueModel GetIssueDetailed(int issueID)
        {
            IssueModel model = new IssueModel();
            IEnumerable<Issue> iL = ctx.Issues.Where(x => x.Id == issueID);
            if (iL.Count() <= 0)
            {
                return null;
            }
            Issue issue = iL.First() ;
            model.Id = issue.Id;
            model.Title = issue.Title;
            model.Status = issue.Status;
            model.Description = issue.Description;
            if (issue.RelatedTo != null)
                model.RelatedTo = (int)issue.RelatedTo;
            if (issue.RelationType != null)
                model.RelationType = (char)issue.RelationType;

            foreach (Tag_Issue tagissue in ctx.Tag_Issues.Where(x => x.Issue == issueID).ToList())
            {
                model.Tags.Add(new TagModel(tagissue.Tag1.Id, tagissue.Tag1.Name));
            }
            foreach (Issue_artefact issueart in ctx.Issue_artefacts.Where(x => x.Issue == issueID).ToList())
            {
                model.Artefacts.Add(new ArtefactModel(issueart.Artefact1.Id, issueart.Artefact1.Name));
            }
            foreach (Issue_stakeholder issuestake in ctx.Issue_stakeholders.Where(x => x.Issue == issueID).ToList())
            {
                model.Stakeholders.Add(new StakeholderModel(issuestake.Stakeholder1.Id, issuestake.Stakeholder1.Name));
            }
            foreach (InfluenceFactor inf in ctx.InfluenceFactors.Where(x => x.Issue == issueID).ToList())
            {
                model.InfluenceFactors.Add(new InfluenceFactorModel(inf.Id,inf.Name,inf.Type,inf.Characteristic));
            }
            foreach (Document doc in issue.Documents.ToList())
            {
                model.Documents.Add(doc.Name);
            }
            UserShort us;
            User u;
            foreach (AccessRight ar in ctx.AccessRights.Where(x => x.Issue == issueID).ToList())
            {
                u = ctx.Users.Where(x => x.AccessObject == ar.AccessObject).First();
                us = new UserShort(u.FirstName, u.LastName, (int)u.AccessObject);
                us.Email = u.Email;
                model.AccessUserList.Add(new AccessRightModel(us, ar.Right));
                model.AccessRights.Add(ar.AccessObject, ar.Right);
            }
            CriterionModel cm;
            CriterionWeightModel cwm;
            foreach (Criterion cr in ctx.Criterion.Where(x => x.Issue == issueID))
            {
                cm = new CriterionModel();
                cm.Id = cr.Id;
                cm.Issue = cr.Issue;
                cm.Name = cr.Name;
                cm.Description = cr.Description;
                cm.Weight = (double)cr.Weight;
                model.Criterions.Add(cm);
                foreach (CriterionWeight cw in ctx.CriterionWeights.Where(x => x.Criterion == cm.Id))
                {
                    cwm = new CriterionWeightModel();
                    cwm.Criterion = cm.Id;
                    cwm.UserAccesObject = (int) cw.User1.AccessObject;
                    cwm.UserId = cw.User;
                    cwm.Weight = cw.Weight;
                }
            }
            
            return model;
        }

        public List<IssueModel> GetUserIssues(string email)
        {
            User user = ctx.Users.First(x => x.Email == email);

            DBConnection.Instance.Connection.Open();
            SqlCommand cmd = DBConnection.Instance.Connection.CreateCommand();
            cmd.CommandText = "select iss.Id, iss.Title, iss.[Status], ISNULL(iss.ReviewRating,0) from " +
                "[Issue] iss, [AccessRight] ar, AccessObject ao, [User] us WHERE ao.Id " + 
                "= ar.AccessObject and us.AccessObject = ao.id AND us.Email LIKE '" + email + "' and ar.[Right] = 'O' and iss.Id = ar.Issue";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<IssueModel> list = new List<IssueModel>();
            IssueModel issueShort;           
            Tag tag;
            while (reader.Read())
            {
                issueShort = new IssueModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3));
                var query = from Tag_Issue in ctx.Tag_Issues
                            where
                              Tag_Issue.Issue == 1
                            select new
                            {
                                Tag = Tag_Issue.Tag,
                                Issue = Tag_Issue.Issue
                            };
                foreach (var ti in query)
                {
                    tag = ctx.Tags.First(x => x.Id == ti.Tag);
                    issueShort.Tags.Add(new TagModel(tag.Id, tag.Name));
                }
                list.Add(issueShort);
                reader.Read();
            }

            DBConnection.Instance.Connection.Close();
            return list;
        }

        public void CreateIssue(IssueModel issue, string email)
        {
            var query = from Issues in
                            (from Issues in ctx.Issues
                             select new
                             {
                                 Issues.Id,
                                 Dummy = "x"
                             })
                        group Issues by new { Issues.Dummy } into g
                        select new
                        {
                            MaxId = (int)g.Max(p => p.Id)
                        };

            int issueID;
            if (query.ToList().Count == 0)
            {
                issueID = 1;
            }
            else
            {
                issueID = query.ToList()[0].MaxId + 1;
            }

            Issue issuePersistent = new Issue()
            {
                Id = issueID
            };

            issuePersistent.Status = "CREATING";
            issuePersistent.Title = issue.Title;
            issuePersistent.Description = issue.Description;
            ctx.Issues.InsertOnSubmit(issuePersistent);
            ctx.SubmitChanges();

            int aoID = ctx.Users.First(x => x.Email == email).AccessObject1.Id;
            AccessRight r = new AccessRight(){
                Issue = issuePersistent.Id,
                AccessObject = aoID,
                Right = 'O'
            };
            ctx.AccessRights.InsertOnSubmit(r);
            ctx.SubmitChanges();


            query = from InfluenceFactors in
                        (from InfluenceFactors in ctx.InfluenceFactors
                         select new
                         {
                             InfluenceFactors.Id,
                             Dummy = "x"
                         })
                    group InfluenceFactors by new { InfluenceFactors.Dummy } into g
                    select new
                    {
                        MaxId = (int)g.Max(p => p.Id)
                    };

            int ifID;
            if (query.ToList().Count == 0)
            {
                ifID = 1;
            }
            else
            {
                ifID = query.ToList()[0].MaxId + 1;
            }
            foreach (InfluenceFactorModel iF in issue.InfluenceFactors)
            {
                InfluenceFactor iFPersistent = new InfluenceFactor
                {
                    Id = ifID,
                    Issue = issuePersistent.Id,
                    Name = iF.Name,
                    Type = iF.Type,
                    Characteristic = iF.Characteristic
                };
                ctx.InfluenceFactors.InsertOnSubmit(iFPersistent);
                ctx.SubmitChanges();
                ifID++;
            }

            //TAGS
            query = from Tags in
                        (from Tags in ctx.Tags
                         select new
                         {
                             Tags.Id,
                             Dummy = "x"
                         })
                    group Tags by new { Tags.Dummy } into g
                    select new
                    {
                        MaxId = (int)g.Max(p => p.Id)
                    };
            int tagID,lastTagId;
            if (query.ToList().Count == 0)
            {
                tagID = 1;
            }
            else
            {
                tagID = query.ToList()[0].MaxId + 1;
            }
            foreach (TagModel tag in issue.Tags)
            {
                if (tag.Id == 0)
                {
                    Tag tagPersistent = new Tag
                    {
                        Id = tagID,
                        Name = tag.Name
                    };
                    ctx.Tags.InsertOnSubmit(tagPersistent);
                    lastTagId = tagID;
                }
                else
                {
                    lastTagId = tag.Id;
                }
                Tag_Issue tagIssue = new Tag_Issue
                {
                    Tag = lastTagId,
                    Issue = issueID
                };
                ctx.Tag_Issues.InsertOnSubmit(tagIssue);
                ctx.SubmitChanges();
            }

            //stakeholder
            query = from Stakeholders in
                        (from Stakeholders in ctx.Stakeholders
                         select new
                         {
                             Stakeholders.Id,
                             Dummy = "x"
                         })
                    group Stakeholders by new { Stakeholders.Dummy } into g
                    select new
                    {
                        MaxId = (int)g.Max(p => p.Id)
                    };

            int stakeholderID, lastStakeholderId;
            if (query.ToList().Count == 0)
            {
                stakeholderID = 1;
            }
            else
            {
                stakeholderID = query.ToList()[0].MaxId + 1;
            }
            foreach (StakeholderModel stakeholder in issue.Stakeholders)
            {
                if (stakeholder.Id == 0)
                {
                    Stakeholder stakeholderPersistent = new Stakeholder
                    {
                        Id = stakeholderID,
                        Name = stakeholder.Name
                    };
                    ctx.Stakeholders.InsertOnSubmit(stakeholderPersistent);
                    lastStakeholderId = stakeholderID;
                    stakeholderID++;
                }
                else
                {
                    lastStakeholderId = stakeholder.Id;
                }
                Issue_stakeholder issueStakeholder = new Issue_stakeholder
                {
                    Stakeholder = lastStakeholderId,
                    Issue = issueID
                };
                ctx.Issue_stakeholders.InsertOnSubmit(issueStakeholder);
                ctx.SubmitChanges();
            }


            //artefact
            query = from Artefacts in
                        (from Artefacts in ctx.Artefacts
                         select new
                         {
                             Artefacts.Id,
                             Dummy = "x"
                         })
                    group Artefacts by new { Artefacts.Dummy } into g
                    select new
                    {
                        MaxId = (int)g.Max(p => p.Id)
                    };

            int artefactID, lastartefactId;
            if (query.ToList().Count == 0)
            {
                artefactID = 1;
            }
            else
            {
                artefactID = query.ToList()[0].MaxId + 1;
            }
            foreach (ArtefactModel artefact in issue.Artefacts)
            {
                if (artefact.Id == 0)
                {
                    Artefact artefactPersistent = new Artefact
                    {
                        Id = artefactID,
                        Name = artefact.Name
                    };
                    ctx.Artefacts.InsertOnSubmit(artefactPersistent);
                    lastartefactId = artefactID;
                    artefactID++;
                }
                else
                {
                    lastartefactId = artefact.Id;
                }
                Issue_artefact issueartefact = new Issue_artefact
                {
                    Artefact = lastartefactId,
                    Issue = issueID
                };
                ctx.Issue_artefacts.InsertOnSubmit(issueartefact);
                ctx.SubmitChanges();
            }

            //accessRights
            AccessRight ar;
            foreach (int accessObjectID in issue.AccessRights.Keys)
            {
                ar = new AccessRight()
                {
                    AccessObject = accessObjectID,
                    Issue = issueID,
                    Right = issue.AccessRights[accessObjectID]
                };
                ctx.AccessRights.InsertOnSubmit(ar);
                ctx.SubmitChanges();
            }
        }

        
        public bool DeleteIssue(int issueId, string ownerEmail)
        {
            IEnumerable<AccessRight> ars = ctx.Users.Where(x => x.Email == ownerEmail).First().AccessObject1.AccessRights.Where(x => x.Issue == issueId);
            if (ars.Count() > 0)
            {
                ars = ars.Where(x => x.Right == 'O');
                if (ars.Count() > 0)
                {
                    ctx.Issues.DeleteOnSubmit(ars.First().Issue1);
                    ctx.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public bool NextStage(int issueId, string ownerEmail)
        {
            IEnumerable<AccessRight> ars = ctx.Users.Where(x => x.Email == ownerEmail).First().AccessObject1.AccessRights.Where(x => x.Issue == issueId);
            if (ars.Count() > 0)
            {
                ars = ars.Where(x => x.Right == 'O');
                if (ars.Count() > 0)
                {
                    Issue issue = ars.First().Issue1;
                    if (issue.Status.ToUpper().Equals("CREATING"))
                    {
                        issue.Status = "Brainstorming";
                    }
                    else if (issue.Status.ToUpper().Equals("BRAINSTORMING"))
                    {
                        issue.Status = "Evaluating";
                    }
                    else if (issue.Status.ToUpper().Equals("EVALUATING"))
                    {
                        issue.Status = "Finished";
                    }
                    else
                    {
                        return false;
                    }
                    ctx.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public bool EditIssue(IssueModel im)
        {
            try
            {
                Issue i = ctx.Issues.Where(x => x.Id == im.Id).First();

                i.Title = im.Title;
                i.Description = im.Description;
                i.RelatedTo = im.RelatedTo;
                i.RelationType = im.RelationType;
                ctx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}