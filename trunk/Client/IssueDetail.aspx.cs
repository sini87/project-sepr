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

                    Label lCriteriaName;
                    Label lCriteriaDesc;
                    Label lCriteriaWeight;
                    if (issue.Criterions.Count > 0)
                    {
                        foreach (CriterionModel crit in issue.Criterions)
                        {
                            lCriteriaName = new Label();
                            lCriteriaName.Text = crit.Name;
                            criteriaPanel.Controls.Add(lCriteriaName);

                            lCriteriaDesc = new Label();
                            lCriteriaDesc.Text = crit.Description;
                            criteriaPanel.Controls.Add(lCriteriaDesc);

                            lCriteriaWeight = new Label();
                            lCriteriaWeight.Text += ""+crit.Weight;
                            criteriaPanel.Controls.Add(lCriteriaWeight);
                        }
                    }
                    else{
                        lCriteriaName = new Label();
                        lCriteriaName.Text = "No Criterions";
                        criteriaPanel.Controls.Add(lCriteriaName);
                    }

                    save.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
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

    }
}
