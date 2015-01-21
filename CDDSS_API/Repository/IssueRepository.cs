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
            foreach (AccessRight ar in ctx.AccessRights.Where(x => x.AccessObject == user.AccessObject).ToList())
            {
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
            Issue issue = iL.First();
            model.Id = issue.Id;
            model.Title = issue.Title;
            model.Status = issue.Status;
            model.Description = issue.Description;
            if (issue.ReviewRating != null) model.ReviewRating = (double)issue.ReviewRating;
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
                model.InfluenceFactors.Add(new InfluenceFactorModel(inf.Id, inf.Name, inf.Type, inf.Characteristic));
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
                if (cr.Weight != null)
                {
                    cm.Weight = (double)cr.Weight;
                }
                else
                {
                    cm.Weight = -1;
                }
                
                model.Criterions.Add(cm);
                foreach (CriterionWeight cw in ctx.CriterionWeights.Where(x => x.Criterion == cm.Id))
                {
                    cwm = new CriterionWeightModel();
                    cwm.Criterion = cm.Id;
                    cwm.UserAccesObject = (int)cw.User1.AccessObject;
                    cwm.UserId = cw.User;
                    cwm.Weight = cw.Weight;
                    cwm.Acronym = cw.User1.FirstName.Substring(0,1) + cw.User1.LastName.Substring(0,1);
                    model.CriterionWeights.Add(cwm);
                }
            }

            if (ctx.Decisions.Where(x => x.IssueId == issueID).Count() > 0)
            {
                Decision dec = ctx.Decisions.Where(x => x.IssueId == issueID).First();
                DecisionModel dm = new DecisionModel(dec.IssueId, dec.AlternativeId, dec.Explanation);

                if (ctx.Alternatives.Where(x => x.Id == dm.AlternativeID).Count() > 0)
                {
                    Alternative alt = ctx.Alternatives.Where(x => x.Id == dm.AlternativeID).First();
                    AlternativeModel am = new AlternativeModel(alt.Id, alt.Issue, alt.Name, alt.Description, alt.Reason);
                    dm.Alternative = am;
                }

                model.Decision = dm;
            }

            return model;
        }

        public List<IssueModel> GetUserIssues(string email)
        {
            User user = ctx.Users.First(x => x.Email == email);


            var q1 = 
                        from us in ctx.Users
                        from ar in ctx.AccessRights
                        where
                            us.Email == email &&
                            ar.Right == 'O' &&
                            ar.AccessObject == us.AccessObject
                        select new
                        {
                            Id = (int?)ar.Issue1.Id,
                            ar.Issue1.Title,
                            ar.Issue1.Status,
                            Rating = ((System.Double?)ar.Issue1.ReviewRating ?? (System.Double?)0)
                        };


            List<IssueModel> list = new List<IssueModel>();
            IssueModel issueShort;
            Tag tag;
            foreach(var t in q1)
            {
                issueShort = new IssueModel((int)t.Id, t.Title, t.Status, (double)t.Rating);
                var query = from Tag_Issue in ctx.Tag_Issues
                            where
                              Tag_Issue.Issue == issueShort.Id
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
            }

            DBConnection.Instance.Connection.Close();
            return list;
        }

        /// <summary>
        /// creates a new issue and returns the issue id of the new issue
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CreateIssue(IssueModel issue, string email)
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
            if (issue.RelatedTo != 0)
            {
                issuePersistent.RelatedTo = issue.RelatedTo;
                issuePersistent.RelationType = issue.RelationType;
            }
            ctx.Issues.InsertOnSubmit(issuePersistent);
            ctx.SubmitChanges();

            int aoID = ctx.Users.First(x => x.Email == email).AccessObject1.Id;
            AccessRight r = new AccessRight()
            {
                Issue = issuePersistent.Id,
                AccessObject = aoID,
                Right = 'O'
            };
            ctx.AccessRights.InsertOnSubmit(r);
            ctx.SubmitChanges();


            //Influence
            ModifyInfluenceFactors(issueID, issue.InfluenceFactors);

            //TAGS
            ModifyTags(issueID, issue.Tags);

            //stakeholder
            ModifyStakeholders(issueID, issue.Stakeholders);


            //artefact
            ModifyArtefacts(issueID, issue.Artefacts);

            //accessRights
            ModifyAccessRights(issueID, issue.AccessRights, email);

            return issueID;
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
                        issue.Status = "Brainstorming1";
                    }
                    else if (issue.Status.ToUpper().Equals("BRAINSTORMING1") || issue.Status.ToUpper().Equals("BRAINSTORMING"))
                    {
                        issue.Status = "Brainstorming2";
                    }
                    else if (issue.Status.ToUpper().Equals("BRAINSTORMING2"))
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

        public bool EditIssue(IssueModel im, string email)
        {
            try
            {
                Issue i = ctx.Issues.Where(x => x.Id == im.Id).First();

                i.Title = im.Title;
                i.Description = im.Description;
                i.RelatedTo = im.RelatedTo;
                i.RelationType = im.RelationType;
                ctx.SubmitChanges();

                IQueryable<Tag_Issue> delTags = ctx.Tag_Issues.Where(x => x.Issue == im.Id);
                ctx.Tag_Issues.DeleteAllOnSubmit(delTags);

                IQueryable<Issue_stakeholder> delStk = ctx.Issue_stakeholders.Where(x => x.Issue == im.Id);
                ctx.Issue_stakeholders.DeleteAllOnSubmit(delStk);

                IQueryable<Issue_artefact> delArt = ctx.Issue_artefacts.Where(x => x.Issue == im.Id);
                ctx.Issue_artefacts.DeleteAllOnSubmit(delArt);
                
                IQueryable<InfluenceFactor> delInf = ctx.InfluenceFactors.Where(x => x.Issue == im.Id);
                ctx.InfluenceFactors.DeleteAllOnSubmit(delInf);

                IQueryable<AccessRight> delAR = ctx.AccessRights.Where(x => x.Issue == im.Id);
                ctx.AccessRights.DeleteAllOnSubmit(delAR);

                ctx.SubmitChanges();

                ModifyTags(im.Id, im.Tags);
                ModifyStakeholders(im.Id, im.Stakeholders);
                ModifyArtefacts(im.Id, im.Artefacts);
                ModifyInfluenceFactors(im.Id, im.InfluenceFactors);
                ModifyAccessRights(im.Id, im.AccessRights, email);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        private void ModifyStakeholders(int issueID, List<StakeholderModel> stakeholders)
        {
            var query = from Stakeholders in
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
            foreach (StakeholderModel stakeholder in stakeholders)
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
        }

        private void ModifyTags(int issueID, List<TagModel> tags)
        {
            var query = from Tags in
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
            int tagID, lastTagId;
            if (query.ToList().Count == 0)
            {
                tagID = 1;
            }
            else
            {
                tagID = query.ToList()[0].MaxId + 1;
            }
            foreach (TagModel tag in tags)
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
                if (ctx.Tag_Issues.Where(x => x.Tag == tagIssue.Tag && x.Issue == tagIssue.Issue).Count() == 0)
                {
                    ctx.Tag_Issues.InsertOnSubmit(tagIssue);
                    ctx.SubmitChanges();
                }
                if (tag.Id == 0)
                {
                    lastTagId++;
                }

            }

            ctx.SubmitChanges();
        }

        private void ModifyArtefacts(int issueID, List<ArtefactModel> artefacts)
        {
            var query = from Artefacts in
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
            foreach (ArtefactModel artefact in artefacts)
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
        }

        private void ModifyInfluenceFactors(int issueID, List<InfluenceFactorModel> influenceFactors){
            var query = from InfluenceFactors in
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
            foreach (InfluenceFactorModel iF in influenceFactors)
            {
                InfluenceFactor iFPersistent = new InfluenceFactor
                {
                    Id = ifID,
                    Issue = issueID,
                    Name = iF.Name,
                    Type = iF.Type,
                    Characteristic = iF.Characteristic
                };
                ctx.InfluenceFactors.InsertOnSubmit(iFPersistent);
                ctx.SubmitChanges();
                ifID++;
            }
        }

        private void ModifyAccessRights(int issueID, Dictionary<int,char> accessList, string email)
        {
            AccessRight ar;
            int aoID;
            if (email != null)
            {
                aoID = ctx.Users.First(x => x.Email == email).AccessObject1.Id;
            }
            else
            {
                aoID = -1;
            }
            foreach (int accessObjectID in accessList.Keys)
            {
                ar = new AccessRight()
                {
                    AccessObject = accessObjectID,
                    Issue = issueID,
                    Right = accessList[accessObjectID]
                };
                if (aoID < 0 || accessObjectID != aoID)
                {
                    ctx.AccessRights.InsertOnSubmit(ar);
                    ctx.SubmitChanges();
                }
            }
        }

        public List<IssueModel> GetAllUserIssues(string email)
        {
            User user = ctx.Users.First(x => x.Email == email);


            var q1 =
                        from us in ctx.Users
                        from ar in ctx.AccessRights
                        where
                            us.Email == email &&
                            (ar.Right == 'O' || ar.Right == 'C') &&
                            ar.AccessObject == us.AccessObject
                        select new
                        {
                            Id = (int?)ar.Issue1.Id,
                            ar.Issue1.Title,
                            ar.Issue1.Status,
                            Rating = ((System.Double?)ar.Issue1.ReviewRating ?? (System.Double?)0)
                        };


            List<IssueModel> list = new List<IssueModel>();
            IssueModel issueShort;
            Tag tag;
            foreach (var t in q1)
            {
                issueShort = new IssueModel((int)t.Id, t.Title, t.Status, (double)t.Rating);
                var query = from Tag_Issue in ctx.Tag_Issues
                            where
                              Tag_Issue.Issue == issueShort.Id
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
            }

            DBConnection.Instance.Connection.Close();
            return list;
        }
    }
}