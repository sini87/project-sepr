using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class IssueDetail : System.Web.UI.Page
    {
        IssueModel issue;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RestClient rc = RestClient.GetInstance(Session.SessionID);

                if (this.User.Identity.IsAuthenticated && rc != null)
                {
                    
                    String issueId = Request.QueryString["issueId"];

                    rc.EndPoint = "api/Issue?issueId=" + issueId;
                    rc.Method = HttpVerb.GET;
                    var json = rc.MakeRequest();
                    issue = JsonConvert.DeserializeObject<IssueModel>(json);

                    rc.EndPoint = "api/Issue?issueId=" + issue.RelatedTo;
                    rc.Method = HttpVerb.GET;
                    var jsonRel = rc.MakeRequest();
                    var issueRel = JsonConvert.DeserializeObject<IssueModel>(jsonRel);

                    rc.EndPoint = "api/Alternative?issueId=" + issueId;
                    rc.Method = HttpVerb.GET;
                    var jsonAlt = rc.MakeRequest();
                    var issueAlt = JsonConvert.DeserializeObject<List<AlternativeModel>>(jsonAlt);

                    generateTitle(issue);
                    generateStatus(issue);
                    generateTags(issue);
                    generateRating(issue);
                    generateDescription(issue);
                    generateRelations(issueRel);
                    generateStakeholders(issue);
                    generateLFactor(issue);
                    generateArtefacts(issue);
                    generateDocuments(issue);
                    generateUsers(issue);
                    generateCriteria(issue);
                    //generateCriteriaWeight(issue);
                    generateAlternatives(issueAlt);

                    criteria.Visible = true;
                    criteriaWeight.Visible = true;
                    alternatives.Visible = true;

                    save.Visible = true;
                    if (!issue.Status.Equals("FINISHED")){ //AND IF USER IS OWNER
                        saveNext.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        protected void generateTitle(IssueModel issue)
        {
            Label lTitle = new Label();
            lTitle.Text = issue.Title;
            issueTitle.Controls.Add(lTitle);
        }

        protected void generateRating(IssueModel issue)
        {
            Label lRating = new Label();
            lRating.Text = ""+issue.ReviewRating;
            rating.Controls.Add(lRating);
        }

        protected void generateTags(IssueModel issue)
        {
            HtmlGenericControl div;
            Label lTags;

            foreach (TagModel tTag in issue.Tags)
            {
                div = new HtmlGenericControl("div");
                lTags = new Label();
                lTags.Text += tTag.Name;
                div.Controls.Add(lTags);
                tag.Controls.Add(div);
            }
        }

        protected void generateStatus(IssueModel issue)
        {
            Label lStatus = new Label();
            lStatus.Text = issue.Status;
            status.Controls.Add(lStatus);
        }

        protected void generateDescription(IssueModel issue)
        {
            Label lDescription = new Label();
            lDescription.Text = issue.Description;
            description.Controls.Add(lDescription);
        }

        protected void generateRelations(IssueModel issue)
        {
            Label lRelation = new Label();
            HtmlGenericControl div = new HtmlGenericControl("div");
            if (issue != null)
            {                
                lRelation.Text = issue.Title;
            }
            else
            {
                lRelation.Text = "No relations to other Issues";
            }

            div.Controls.Add(lRelation);
            relations.Controls.Add(div);
        }

        protected void generateStakeholders(IssueModel issue)
        {
            Label lStakeholder = new Label();
            HtmlGenericControl div = new HtmlGenericControl("div");
            if (issue.Stakeholders.Count > 0)
            {
                foreach (StakeholderModel stake in issue.Stakeholders)
                {
                    lStakeholder = new Label();
                    div = new HtmlGenericControl("div");
                    lStakeholder.Text = stake.Name;
                    div.Controls.Add(lStakeholder);
                    stakeholder.Controls.Add(div);
                }
            }
            else
            {
                lStakeholder.Text = "No stakeholders";
                div.Controls.Add(lStakeholder);
                stakeholder.Controls.Add(div);
            }
        }

        protected void generateArtefacts(IssueModel issue)
        {
            Label lArtefact = new Label();
            HtmlGenericControl div = new HtmlGenericControl("div");
            if (issue.Artefacts.Count > 0)
            {
                foreach (ArtefactModel stake in issue.Artefacts)
                {
                    lArtefact = new Label();
                    div = new HtmlGenericControl("div");
                    lArtefact.Text = stake.Name;
                    div.Controls.Add(lArtefact);
                    artefacts.Controls.Add(div);
                }
            }
            else
            {
                lArtefact.Text = "No Artefacts";
                div.Controls.Add(lArtefact);
                artefacts.Controls.Add(div);
            }
        }

        protected void generateDocuments(IssueModel issue)
        {
            Label lDocuments = new Label();
            HtmlGenericControl div = new HtmlGenericControl("div");

            if (issue.Documents.Count > 0)
            {
                foreach (String doc in issue.Documents)
                {
                    lDocuments = new Label();
                    div = new HtmlGenericControl("div");
                    lDocuments.Text = doc;
                    div.Controls.Add(lDocuments);
                    documents.Controls.Add(div);
                }
            }
            else
            {
                lDocuments = new Label();
                lDocuments.Text = "No documents";
                div.Controls.Add(lDocuments);
                documents.Controls.Add(div);
            }
        }

        protected void generateCriteria(IssueModel issue)
        {
            if (issue.Criterions.Count > 0)
            {
                Table critTable = new Table();
                critTable.Width = Unit.Percentage(100);

                TableHeaderRow rowHeaderCriteria = new TableHeaderRow();
                TableHeaderCell headerCellName = new TableHeaderCell();
                headerCellName.Text = "Name";
                TableHeaderCell headerCellDesc = new TableHeaderCell();
                headerCellDesc.Text = "Description";
                TableHeaderCell headerCellWeight = new TableHeaderCell();
                headerCellWeight.Text = "Weight";

                rowHeaderCriteria.Cells.Add(headerCellName);
                rowHeaderCriteria.Cells.Add(headerCellDesc);
                rowHeaderCriteria.Cells.Add(headerCellWeight);

                critTable.Rows.Add(rowHeaderCriteria);

                TableRow rowCriteria;
                TableCell criteriaName;
                TableCell criteriaDesc;
                TableCell criteriaWeight;

                foreach (CriterionModel crit in issue.Criterions)
                {
                    rowCriteria = new TableRow();

                    criteriaName = new TableCell();
                    criteriaName.Text = crit.Name;
                    rowCriteria.Cells.Add(criteriaName);

                    criteriaDesc = new TableCell();
                    criteriaDesc.Text = crit.Description;
                    rowCriteria.Cells.Add(criteriaDesc);

                    criteriaWeight = new TableCell();
                    criteriaWeight.Text += "" + crit.Weight;
                    rowCriteria.Cells.Add(criteriaWeight);

                    critTable.Rows.Add(rowCriteria);
                }

                criteriaPanel.Controls.Add(critTable);
            }
            else
            {
                Label noCrit = new Label();
                noCrit.Text = "No Criterions";
                criteriaPanel.Controls.Add(noCrit);
            }
        }

        protected void generateLFactor(IssueModel issue)
        {
            Label lFactor = new Label();
            HtmlGenericControl div = new HtmlGenericControl("div");

            if (issue.InfluenceFactors.Count > 0)
            {
                foreach (InfluenceFactorModel factor in issue.InfluenceFactors)
                {
                    lFactor = new Label();
                    div = new HtmlGenericControl("div");
                    lFactor.Text = factor.Name;
                    div.Controls.Add(lFactor);
                    factors.Controls.Add(lFactor);
                }
            }
            else
            {
                lFactor.Text = "No incluence factors";
                div.Controls.Add(lFactor);
                factors.Controls.Add(lFactor);
            }
        }

        protected void generateUsers(IssueModel issue)
        {
            if (issue.AccessUserList.Count > 0)
            {
                Table userTable = new Table();
                userTable.Width = Unit.Percentage(100);

                TableRow rowUser;
                TableCell userPic;
                TableCell userName;

                foreach (AccessRightModel user in issue.AccessUserList)
                {
                    rowUser = new TableRow();

                    userPic = new TableCell();
                    userPic.Text = "<img src=" + "'~/Images/avatar_woman.png'" + "/>";
                    rowUser.Cells.Add(userPic);

                    userName = new TableCell();
                    userName.Text = user.User.FirstName + " " + user.User.LastName;
                    rowUser.Cells.Add(userName);

                    userTable.Rows.Add(rowUser);
                }

                users.Controls.Add(userTable);
            }
            else
            {
                Label noUsers = new Label();
                noUsers.Text = "No Users";
                users.Controls.Add(noUsers);
            }
        }

        protected void generateCriteriaWeight(IssueModel issue)
        {
            if (issue.Criterions.Count > 0)
            {
                Table critTable = new Table();
                critTable.Width = Unit.Percentage(100);

                TableHeaderRow rowHeaderCriteria = new TableHeaderRow();
                TableHeaderCell headerCellName = new TableHeaderCell();
                headerCellName.Text = "Name";
                TableHeaderCell headerCellDesc = new TableHeaderCell();
                headerCellDesc.Text = "Description";
                TableHeaderCell headerCellWeight = new TableHeaderCell();
                headerCellWeight.Text = "Weight";

                rowHeaderCriteria.Cells.Add(headerCellName);
                rowHeaderCriteria.Cells.Add(headerCellDesc);
                rowHeaderCriteria.Cells.Add(headerCellWeight);

                critTable.Rows.Add(rowHeaderCriteria);

                TableRow rowCriteria;
                TableCell criteriaName;
                TableCell criteriaDesc;
                TableCell criteriaWeight;

                foreach (CriterionModel crit in issue.Criterions)
                {
                    rowCriteria = new TableRow();

                    criteriaName = new TableCell();
                    criteriaName.Text = crit.Name;
                    rowCriteria.Cells.Add(criteriaName);

                    criteriaDesc = new TableCell();
                    criteriaDesc.Text = crit.Description;
                    rowCriteria.Cells.Add(criteriaDesc);

                    criteriaWeight = new TableCell();
                    criteriaWeight.Text += "" + crit.Weight;
                    rowCriteria.Cells.Add(criteriaWeight);

                    critTable.Rows.Add(rowCriteria);
                }

                criteriaPanel.Controls.Add(critTable);
            }
            else
            {
                Label noCrit = new Label();
                noCrit.Text = "No Criterions";
                criteriaPanel.Controls.Add(noCrit);
            }
        }

        protected void generateAlternatives(List<AlternativeModel> alternatives)
        {
            if (alternatives.Count > 0)
            {
                Table altTable = new Table();
                altTable.Width = Unit.Percentage(100);

                TableHeaderRow rowHeaderAlt = new TableHeaderRow();
                TableHeaderCell headerAltName = new TableHeaderCell();
                headerAltName.Text = "Name";
                TableHeaderCell headerAltDesc = new TableHeaderCell();
                headerAltDesc.Text = "Description";
                TableHeaderCell headerAltReason = new TableHeaderCell();
                headerAltReason.Text = "Reason";
                TableHeaderCell headerAltRating = new TableHeaderCell();
                headerAltRating.Text = "Rating";

                rowHeaderAlt.Cells.Add(headerAltName);
                rowHeaderAlt.Cells.Add(headerAltDesc);
                rowHeaderAlt.Cells.Add(headerAltReason);
                rowHeaderAlt.Cells.Add(headerAltRating);

                altTable.Rows.Add(rowHeaderAlt);

                TableRow rowAlt;
                TableCell altName;
                TableCell altDesc;
                TableCell altReas;
                TableCell altRate;

                foreach (AlternativeModel alt in alternatives)
                {
                    rowAlt = new TableRow();

                    altName = new TableCell();
                    altName.Text = alt.Name;
                    rowAlt.Cells.Add(altName);

                    altDesc = new TableCell();
                    altDesc.Text = alt.Description;
                    rowAlt.Cells.Add(altDesc);

                    altReas = new TableCell();
                    altReas.Text = alt.Reason;
                    rowAlt.Cells.Add(altReas);

                    altRate = new TableCell();
                    altRate.Text = ""+alt.Rating;
                    rowAlt.Cells.Add(altRate);

                    altTable.Rows.Add(rowAlt);
                }

                alternativesPanel.Controls.Add(altTable);
            }
            else
            {
                Label noAlt = new Label();
                noAlt.Text = "No Alternatives";
                alternativesPanel.Controls.Add(noAlt);
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
           
            String hidCriteriaValue = hiddenCriteria.Value;
            String hidCritWeightValue = hiddenCriteriaWeight.Value;
            String hidAlternativeValue = hiddenAlternatives.Value;

            if (!hidCriteriaValue.Equals(""))
            {
                hidCriteriaValue = hidCriteriaValue.Remove(hidCriteriaValue.Length - 1, 1);
                String[] crit = hidCriteriaValue.Split(';');

                int i = 0;
                while (i < crit.Length)
                {
                    CriterionModel cm = new CriterionModel();
                    cm.Name = crit[i];
                    cm.Description = crit[i + 1];
                    cm.Issue = issue.Id;
                    cm.Weight = 0;
                    cm.Ratings = null;
                    i = i + 2;

                    rc.EndPoint = "api/Criterion";
                    rc.Method = HttpVerb.POST;
                    var json = JsonConvert.SerializeObject(cm);
                    rc.PostData = json;
                    rc.MakeRequest();
                }
            }

            if (!hidCritWeightValue.Equals(""))
            {
                hidCritWeightValue = hidCritWeightValue.Remove(hidCritWeightValue.Length - 1, 1);
                String[] critWeigt = hidCritWeightValue.Split(';');
            }

            if (!hidAlternativeValue.Equals(""))
            {
                hidAlternativeValue = hidAlternativeValue.Remove(hidAlternativeValue.Length - 1, 1);
                String[] critAlt = hidAlternativeValue.Split(';');
            }
         }

        protected void saveNext_Click(object sender, EventArgs e)
        {

        }

    }
}
