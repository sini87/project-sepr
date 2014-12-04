using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class IssueDetail : System.Web.UI.Page
    {
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
                    var issue = JsonConvert.DeserializeObject<IssueModel>(json);

                    Label lTitle = new Label();
                    lTitle.Text = issue.Title;
                    issueTitle.Controls.Add(lTitle);

                    Label lTags;

                    foreach (TagModel tTag in issue.Tags)
                    {
                        lTags = new Label();
                        lTags.Text += tTag.Name;
                        tag.Controls.Add(lTags);
                    }

                    Label lStatus = new Label();
                    lStatus.Text = issue.Status;
                    status.Controls.Add(lStatus);

                    Label lDescription = new Label();
                    lDescription.Text = issue.Description;
                    description.Controls.Add(lDescription);

                    rc.EndPoint = "api/Issue?issueId=" + issue.RelatedTo;
                    rc.Method = HttpVerb.GET;
                    var jsonRel = rc.MakeRequest();
                    var issueRel = JsonConvert.DeserializeObject<IssueModel>(json);

                    Label lRelation = new Label();

                    if (issueRel != null)
                    {
                        lRelation.Text = issueRel.Title;
                    }
                    else
                    {
                        lRelation.Text = "No relations to other Issues";
                    }

                    relations.Controls.Add(lRelation);

                    Label lStakeholder;
                    if (issue.Stakeholders.Count > 0)
                    {
                        foreach (StakeholderModel stake in issue.Stakeholders)
                        {
                            lStakeholder = new Label();
                            lStakeholder.Text = stake.Name;
                            stakeholder.Controls.Add(lStakeholder);
                        }
                    }
                    else
                    {
                        lStakeholder = new Label();
                        lStakeholder.Text = "No stakeholders";
                        stakeholder.Controls.Add(lStakeholder);
                    }

                    Label lFactor;
                    if (issue.InfluenceFactors.Count > 0)
                    {
                        foreach (InfluenceFactorModel factor in issue.InfluenceFactors)
                        {
                            lFactor = new Label();
                            lFactor.Text = factor.Name;
                            factors.Controls.Add(lFactor);
                        }
                    }
                    else
                    {
                        lFactor = new Label();
                        lFactor.Text = "No incluence factors";
                        factors.Controls.Add(lFactor);
                    }

                    Label lArtefacts;
                    if (issue.Artefacts.Count > 0)
                    {
                        foreach (ArtefactModel artefact in issue.Artefacts)
                        {
                            lArtefacts = new Label();
                            lArtefacts.Text = artefact.Name;
                            artefacts.Controls.Add(lArtefacts);
                        }
                    }
                    else
                    {
                        lArtefacts = new Label();
                        lArtefacts.Text = "No artefacts";
                        artefacts.Controls.Add(lArtefacts);
                    }

                    Label lDocuments;
                    if (issue.Documents.Count > 0)
                    {
                        foreach (String doc in issue.Documents)
                        {
                            lDocuments = new Label();
                            lDocuments.Text = doc;
                            documents.Controls.Add(lDocuments);
                        }
                    }
                    else
                    {
                        lDocuments = new Label();
                        lDocuments.Text = "No documents";
                        documents.Controls.Add(lDocuments);
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        protected void addAlternative_Click(object sender, EventArgs e)
        {
            lAlternativeName.Enabled = true;
            lAlternativeDescription.Enabled = true;
            lAlternativeReason.Enabled = true;

            TextBox tName = new TextBox();
            TextBox tDescrption = new TextBox();
            TextBox tReason = new TextBox();

            alternatives.Controls.Add(tName);
            alternatives.Controls.Add(tDescrption);
            alternatives.Controls.Add(tReason);
        }

        protected void addCriteria_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(HiddenFieldCriteria.Value);

            for (int i = 0; i < rows; i++)
            {
                TextBox tbName = new TextBox();
                tbName.ID = "criteriaName" + (i+1);

                TextBox tbDescrption = new TextBox();
                tbDescrption.ID = "criteriaDescription" + (i+1);

                TableRow row = new TableRow();
                TableCell cellName = new TableCell();
                TableCell cellDesc = new TableCell();

                cellName.Controls.Add(tbName);
                row.Cells.Add(cellName);
                cellDesc.Controls.Add(tbDescrption);
                row.Cells.Add(cellDesc);

                row.Cells.Add(cellName);
                row.Cells.Add(cellDesc);

                criteriaTable.Rows.Add(row);

            }
            HiddenFieldCriteria.Value = ""+rows++;
            criteriaTable.Visible = true;
        }
    }
}