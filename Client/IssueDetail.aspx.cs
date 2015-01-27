using CDDSS_API;
using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class IssueDetail : System.Web.UI.Page
    {

        protected void Page_Preload(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }

            RestClient rc = RestClient.GetInstance(Session.SessionID);

            if (Request["issueId"] == null || Request["issueId"].Length == 0)
            {
                Server.Transfer("IssueDetail.aspx?issueId=1");
                return;
            }
            if (!IsPostBack)
            {
                //if (!User.Identity.IsAuthenticated)
                //{
                //    RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                //    SessionManager.AddUserSession(Session.SessionID);
                //}
                
                UserSession us = SessionManager.GetUserSession(Session.SessionID);
                us.DetailIssue = null;
                us.CreateIssueEntered();
            }
            else
            {
                UserSession us = SessionManager.GetUserSession(Session.SessionID);
                
                //events for tags
                foreach (Control c in us.IssueTags)
                {
                    if (c.GetType() == typeof(Button))
                    {
                        ((Button)c).Click += tagButton_Click;
                    }
                    else if (c.GetType() == typeof(DropDownList))
                    {
                        ((DropDownList)c).SelectedIndexChanged += tagsDDL_SelectedIndexChanged;
                    }
                    tagPanel.Controls.Add(c);
                }

                //events for user access rights
                foreach (TableRow tr in us.AccessRTRs)
                {
                    ((Button)tr.Cells[2].Controls[0]).Click += delUserBtn_Click;
                }

                //events for stakeholders
                foreach (TableRow tr in us.StakeholdersTRs)
                {
                    ((Button)tr.Cells[1].Controls[0]).Click += delStkBtn_Click;
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ((DropDownList)tr.Cells[0].Controls[0]).SelectedIndexChanged += stkDropList_SelectedIndexChanged;
                    }
                }

                //events for artefacts
                foreach (TableRow tr in us.ArtefactsTRs)
                {
                    ((Button)tr.Cells[1].Controls[0]).Click += delArtBtn_Click;
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ((DropDownList)tr.Cells[0].Controls[0]).SelectedIndexChanged += artDropList_SelectedIndexChanged;
                    }
                }

                //events for factors
                foreach (TableRow tr in us.FactorTRs)
                {
                    ((Button)tr.Cells[3].Controls[0]).Click += delFactorBtn_Click;
                }

                //events for documents
                foreach (TableRow tr in us.DocumentsTRs)
                {
                    ((Button)tr.Cells[1].Controls[0]).Click += delDocBtn_Click;
                }

                //events for criteria
                foreach (TableRow tr in us.CriteriaTRs)
                {
                    ((Button)tr.Cells[2].Controls[0]).Click += delCritBtn_Click;
                }

                //events for alternatives
                for (int i = 1; i < us.AlternativesTRs.Count; i++ )
                {
                    ((Button)us.AlternativesTRs[i].Cells[3].Controls[0]).Click += delAltBtn_Click;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int issueID = int.Parse(Request["issueId"]);
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            IssueModel issue;
            List<AlternativeModel> altList = new List<AlternativeModel>();

            if (!IsPostBack) { 
                rc.EndPoint = "api/Issue?issueId=" + issueID;
                rc.Method = HttpVerb.GET;
                issue = JsonConvert.DeserializeObject<IssueModel>(rc.MakeRequest());
                us.DetailIssue = issue;
            }
            else
            {
                issue = us.DetailIssue;
            }
            ratingLabel.Attributes.Add("readonly", "readonly");
            ratingLabel.Text = issue.ReviewRating.ToString();

            statusLabel.Text = issue.Status;
            
            if ( issue.RelatedTo > 0)
            {
                rc.EndPoint = "api/Issue?issueId=" + issue.RelatedTo;
                rc.Method = HttpVerb.GET;
                IssueModel relIss = JsonConvert.DeserializeObject<IssueModel>(rc.MakeRequest());
                if (issue.RelationType.Equals('C'))
                {
                    relationTypeLabel.Text = "Child: ";
                }
                else
                {
                    relationTypeLabel.Text = "Parent: ";
                }
                relatedIssueLink.Text = relIss.Title;
                relatedIssueLink.NavigateUrl = "/IssueDetail?issueId=" + issue.RelatedTo;
                relatedIssueLink.Visible = true ;
            }
            else
            {
                relationTypeLabel.Text = "no relation";
                relatedIssueLink.Visible = false;
            }

            if (!IsPostBack) { 
                Button tagButton;
                titleText.Text = issue.Title;
                descriptionText.Text = issue.Description;
                us.DescriptionText = descriptionText;
                us.TitleText = titleText;

                foreach (TagModel tm in issue.Tags)
                {
                    tagButton = new Button();
                    tagButton.ID = "TAGBTN" + tm.Id;
                    tagButton.Click += tagButton_Click;
                    tagButton.ToolTip = "Remove";
                    tagButton.Text = tm.Name;
                    tagButton.CssClass = "table_tag";
                    tagPanel.Controls.Add(tagButton);
                    us.IssueTags.Add(tagButton);
                }

                buildAccessRights(issue, us);
                buildStakeholders(issue, us);
                buildArtefacts(issue, us);
                buildFactors(issue, us);
                buildDocuments(issue, us);
                buildCriterias(issue, us);
                if (!statusLabel.Text.ToUpper().Equals("CREATING"))
                {
                    if (!statusLabel.Text.ToUpper().Equals("BRAINSTORMING1"))
                    {
                        buildCriteriaWeights(issue, us);
                    }
                    altList = buildAlternatives(issue, us);

                    if (statusLabel.Text.ToUpper().Equals("EVALUATING"))
                    {
                        buildRating(issue, us, altList);
                    }
                }


                if (statusLabel.Text.ToUpper().Equals("CREATING"))
                {
                    statusLabel.CssClass = "status_creating";
                }
                else if (statusLabel.Text.ToUpper().Equals("BRAINSTORMING1") || statusLabel.Text.ToUpper().Equals("BRAINSTORMING2"))
                {
                    statusLabel.CssClass = "status_brainstorming";
                }
                else if (statusLabel.Text.ToUpper().Equals("EVALUATING"))
                {
                    statusLabel.CssClass = "status_evaluating";
                }
                else if (statusLabel.Text.ToUpper().Equals("FINISHED"))
                {
                    statusLabel.CssClass = "status_finished";
                }
                else
                {
                    statusLabel.CssClass = "status_reviewed";
                }
                
            }
            else
            {
                foreach (Control c in us.IssueTags)
                {
                    tagPanel.Controls.Add(c);
                }

                foreach (TableRow tr in us.AccessRTRs)
                {
                    usersTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.StakeholdersTRs)
                {
                    stakeholderTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.ArtefactsTRs)
                {
                    artefactsTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.FactorTRs)
                {
                    factorsTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.DocumentsTRs)
                {
                    documentsTable.Rows.Add(tr);
                    if (tr.Cells[0].Controls[0].GetType() == typeof(FileUpload))
                    {
                        FileUpload fu = (FileUpload)tr.Cells[0].Controls[0];
                        if (fu.HasFile)
                        {
                            string fn = fu.FileName;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                byte[] bytes = new byte[fu.FileContent.Length];
                                fu.FileContent.Read(bytes, 0, (int)fu.FileContent.Length);
                                ms.Write(bytes, 0, (int)fu.FileContent.Length);
                                rc.AddFile(fu.FileName, ms);
                            }
                            tr.Cells[0].Controls.Clear();
                            Label lbl = new Label();
                            lbl.ID = "docLBL" + fn;
                            lbl.Text = fn;
                            tr.Cells[0].Controls.Add(lbl);
                        }
                    }
                }

                foreach (TableRow tr in us.CriteriaTRs)
                {
                    criteriaTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.CriterionWeightTRs)
                {
                    criteriaWeightTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.AlternativesTRs)
                {
                    alternativesTable.Rows.Add(tr);
                }

                foreach (TableRow tr in us.RatingsTRs)
                {
                    evaluationTable.Rows.Add(tr);
                }
            }


            doPermissions(issue, rc);

            foreach (string str in us.Messages)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),"Warning!","alert('" + str + "');",true);
            }
            us.Messages.Clear();

            //final decision && reviews
            if (issue.Status.ToUpper().Equals("FINISHED"))
            {
                rc.EndPoint = "api/Decision";
                rc.Method = HttpVerb.GET;
                DecisionModel dm = JsonConvert.DeserializeObject<DecisionModel>(rc.MakeRequest("?issueID=" + issue.Id));
                if (dm != null)
                {
                    foreach (AlternativeModel am in altList)
                    {
                        if (am.Id == dm.AlternativeID)
                        {
                            decisionLBL.Text = am.Name;
                        }
                    }
                    
                    decisionTXT.Text = dm.Explanation;
                    decisionPanel.Visible = true;
                }
               
                rc.EndPoint = "api/Review";
                rc.Method = HttpVerb.GET;
                List<ReviewModel> rList = JsonConvert.DeserializeObject<List<ReviewModel>>(rc.MakeRequest("?issueId=" + issue.Id));
                if (rList.Count > 0)
                {
                    foreach (ReviewModel rm in rList)
                    {
                        Label lbl = new Label();
                        lbl.Text = rm.UserFirstName + " " + rm.UserLastName;
                        TextBox txt = new TextBox();
                        txt.Text = rm.Explanation;
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.Height = 50;
                        txt.Enabled = false;
                        reviewPanel.Controls.Add(lbl);
                        reviewPanel.Controls.Add(new LiteralControl("<br />"));
                        reviewPanel.Controls.Add(txt);
                        reviewPanel.Controls.Add(new LiteralControl("<br />"));

                    }
                    reviewPanel.Visible = true;
                }
            }
            
        }

        private void buildRating(IssueModel issue, UserSession us, List<AlternativeModel> altList)
        {
            TableHeaderRow thr = new TableHeaderRow();
            TableHeaderCell thc;
            TableRow tr;
            TableCell tc;
            TextBox txt;
            List<RatingModel> ratings;
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            Dictionary<string,double> dict = new Dictionary<string,double>();

            evaluationTable.Rows.Clear();
            us.RatingsTRs.Clear();

            evaluationPanel.Visible = true;

            rc.EndPoint = "api/Rating/All";
            rc.Method = HttpVerb.GET;
            ratings = JsonConvert.DeserializeObject<List<RatingModel>>(rc.MakeRequest("?issueID=" + issue.Id));
            
            foreach (RatingModel rat in ratings)
            {
                dict.Add(rat.CriterionID + "x" + rat.AlternativeID, rat.Rating1);
            }

            
            thc = new TableHeaderCell();
            Random random = new Random();

            thc.ID = "evalTHC" + random.Next(1, 100000);
            thr.Cells.Add(thc);
            foreach (AlternativeModel alt in altList)
            {
                thc = new TableHeaderCell();
                thc.ID = "evalAltTHC" + alt.Id;
                thc.Text = alt.Name;
                thr.Cells.Add(thc);
            }
            thr.ID = "ratingTHR-1";
            us.RatingsTRs.Add(thr);
            evaluationTable.Rows.Add(thr);
            
            foreach (CriterionModel cm in issue.Criterions)
            {
                tr = new TableRow();
                thc = new TableHeaderCell();
                thc.ID = "evalCrTHC" + cm.Id;
                thc.Text = cm.Name;
                tr.Cells.Add(thc);
                foreach (AlternativeModel alt in altList)
                {
                    tc = new TableCell();
                    tc.ID = cm.Id + "x" + alt.Id;
                    txt = new TextBox();
                    txt.ID = "evalTXT" + tc.ID;
                    tc.Controls.Add(txt);
                    tr.Cells.Add(tc);

                    if (dict.Keys.Contains(tc.ID))
                    {
                        txt.Text = dict[tc.ID].ToString();
                        thr.ID = "ratingTHR1";
                    }
                }
                us.RatingsTRs.Add(tr);
                evaluationTable.Rows.Add(tr);
            }
        }

        private List<AlternativeModel> buildAlternatives(IssueModel issue, UserSession us)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            TableRow tr;
            TableCell altTC, descAltTC, altReasonTC, delAltTC, altRatTC;
            TextBox altTXT, descAltTXT, altReasonTXT;
            Button delAltBtn;
            Label altRatLbl;
            List<AlternativeModel> altList;
            rc.EndPoint = "api/Alternative";
            rc.Method = HttpVerb.GET;
            altList = JsonConvert.DeserializeObject<List<AlternativeModel>>(rc.MakeRequest("?issueId="+issue.Id));

            foreach (AlternativeModel alt in altList)
            {
                tr = new TableRow();
                tr.ID = "altTR" + alt.Id;
                altTC = new TableCell();
                altTC.ID = "altTC" + alt.Id;
                altTXT = new TextBox();
                altTXT.ID = "altTXT" + alt.Id;
                altTXT.Text = alt.Name;
                altTC.Controls.Add(altTXT);
                tr.Cells.Add(altTC);

                descAltTC = new TableCell();
                descAltTC.ID = "desAltTC" + alt.Id;
                descAltTXT = new TextBox();
                descAltTXT.ID = "desActTXT" + alt.Id;
                descAltTXT.Text = alt.Description;
                descAltTC.Controls.Add(descAltTXT);
                tr.Cells.Add(descAltTC);

                altReasonTC = new TableCell();
                altReasonTC.ID = "altReasonTC" + alt.Id;
                altReasonTXT = new TextBox();
                altReasonTXT.ID = "altReasonTXT" + alt.Id;
                altReasonTXT.Text = alt.Reason;
                altReasonTC.Controls.Add(altReasonTXT);
                tr.Cells.Add(altReasonTC);

                delAltBtn = new Button();
                delAltBtn.ID = "delAltBtn" + alt.Id;
                delAltBtn.Text = "X";
                delAltBtn.Click += delAltBtn_Click;
                delAltTC = new TableCell();
                delAltTC.ID = "delAltTC" + alt.Id;
                delAltTC.Controls.Add(delAltBtn);
                tr.Cells.Add(delAltTC);

                altRatTC = new TableCell();
                altRatTC.ID = "altRatTC" + alt.Id;
                altRatLbl = new Label();
                altRatLbl.ID = "altRatLbl" + alt.Id;
                altRatLbl.Text = alt.Rating.ToString();
                altRatTC.Controls.Add(altRatLbl);
                tr.Cells.Add(altRatTC);

                alternativesTable.Rows.Add(tr);
                us.AlternativesTRs.Add(tr);
            }

            return altList;
        }

        private void buildCriteriaWeights(IssueModel issue, UserSession us)
        {
            TableRow tr;
            TableCell critTC, weightTC; 
            TextBox weightTXT;
            Label critLbl, weightLbl;
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            int userAO = int.Parse(usersTable.ID.Replace("USR",""));

            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "critTHR";
            TableHeaderCell thc = new TableHeaderCell();
            thc.ID = "critTHC";
            thr.Cells.Add(thc);
            thc = new TableHeaderCell();
            thc.ID = rc.User.AccessObject.ToString();
            thc.Text = rc.User.FirstName.Substring(0, 1) + rc.User.LastName.Substring(0, 1);
            thr.Cells.Add(thc);
            criteriaWeightTable.Rows.Add(thr);
            
            foreach (CriterionModel cm in issue.Criterions)
            {
                tr = new TableRow();
                tr.ID = "critWTR" + cm.Id;
                critTC = new TableCell();
                critTC.ID = "critNameWTC" + cm.Id;
                critLbl = new Label();
                critLbl.ID = "critWLBL" + cm.Id;
                if (issue.Status.Equals("Brainstorming2"))
                {
                    critLbl.Text = cm.Name + " ";
                }
                else
                {
                    critLbl.Text = (cm.Weight * 100) + "% " + cm.Name;
                }
                weightTXT = new TextBox();
                weightTXT.ID = "critWTXT" + cm.Id;
                RegularExpressionValidator validator = new RegularExpressionValidator();
                validator.ErrorMessage = "For Criteria Weight only numbers are allowed";
                validator.ControlToValidate = weightTXT.ID;
                validator.ValidationExpression = @"\d+,\d+|\d+";
                weightTC = new TableCell();
                weightTC.ID = "critWeigtWTC" + cm.Id;
                critTC.Controls.Add(critLbl);
                weightTC.Controls.Add(weightTXT);
                weightTC.Controls.Add(validator);
                tr.Cells.Add(critTC);
                tr.Cells.Add(weightTC);
                criteriaWeightTable.Rows.Add(tr);
            }

            tr = new TableRow();
            foreach (CriterionWeightModel cwm in issue.CriterionWeights)
            {
                foreach (TableRow t in criteriaWeightTable.Rows)
                {
                    if (t.ID.Equals("critWTR" + cwm.Criterion))
                    {
                        tr = t;
                        break;
                    }
                }

                thc = null;
                foreach (TableHeaderCell tc in thr.Cells)
                {
                    if (tc.ID.Equals(cwm.UserAccesObject.ToString()))
                    {
                        thc = tc;
                    }
                }
                
                if (thc == null)
                {
                    thc = new TableHeaderCell();
                    thc.ID = cwm.UserAccesObject.ToString();
                    thc.Text = cwm.Acronym;
                    thr.Cells.Add(thc);
                }

                if (cwm.UserAccesObject == userAO)
                {
                    weightTXT = (TextBox)tr.Cells[1].Controls[0];
                    ((TableCell)weightTXT.Parent).ID = "critWeigtWTC-" + cwm.Criterion;
                    weightTXT.Text = (cwm.Weight * 100).ToString();
                }
                else
                {
                    weightLbl = new Label();
                    weightLbl.ID = cwm.Criterion + "-" + cwm.UserAccesObject;
                    weightLbl.Text = (cwm.Weight * 100).ToString();
                    weightTC = new TableCell();
                    weightTC.ID = "weightWTC" + weightLbl.ID;
                    weightTC.Controls.Add(weightLbl);
                    tr.Cells.Add(weightTC);
                }
            }

            foreach (TableRow t in criteriaWeightTable.Rows)
            {
                us.CriterionWeightTRs.Add(t);
            }
            
        }

        private void buildCriterias(IssueModel issue, UserSession us)
        {
            TableRow tr;
            TableCell critDescTC, critTC, delCritTC;
            TextBox critTXT, critDescTXT;
            Button delCritBtn;

            foreach (CriterionModel crit in issue.Criterions)
            {
                tr = new TableRow();
                tr.ID = "critTR" + crit.Id;
                critTC = new TableCell();
                critTC.ID = "critTC" + crit.Id;
                critDescTC = new TableCell();
                critDescTC.ID = "critDescTC" + crit.Id;
                delCritTC = new TableCell();
                delCritTC.ID = "delCritTC" + crit.Id;
                critTXT = new TextBox();
                critTXT.ID = "critTXT" + crit.Id;
                critTXT.Text = crit.Name;
                critDescTXT = new TextBox();
                critDescTXT.ID = "critDescTXT" + crit.Id;
                critDescTXT.Text = crit.Description;
                delCritBtn = new Button();
                delCritBtn.Text = "X";
                delCritBtn.ID = "delCritBtn" + crit.Id;
                delCritBtn.Click += delCritBtn_Click;
                critDescTC.Controls.Add(critDescTXT);
                critTC.Controls.Add(critTXT);
                delCritTC.Controls.Add(delCritBtn);
                tr.Cells.Add(critTC);
                tr.Cells.Add(critDescTC);
                tr.Cells.Add(delCritTC);
                criteriaTable.Rows.Add(tr);
                us.CriteriaTRs.Add(tr);
            }
        }

        private void buildDocuments(IssueModel issue, UserSession us)
        {
            HyperLink link;
            TableRow tr;
            TableCell docTC, delDocTC;
            Button delDocBtn;
            foreach (string doc in issue.Documents)
            {
                link = new HyperLink();
                link.NavigateUrl = "http://54.93.154.67:51853/api/Document?issueId=" + issue.Id + "&filename=" + doc;
                link.Text = doc;
                link.Target="_blank";
                link.ID = "LINK" + doc;
                docTC = new TableCell();
                docTC.ID = "docTC" + doc;
                docTC.Controls.Add(link);
                delDocBtn = new Button();
                delDocBtn.Text = "X";
                delDocBtn.Click += delDocBtn_Click;
                delDocBtn.ID = "delDocBtn" + doc;
                delDocTC = new TableCell();
                delDocTC.ID = "delDocTC" + doc;
                delDocTC.Controls.Add(delDocBtn);
                tr = new TableRow();
                tr.ID = "DOCTR" + doc;
                tr.Cells.Add(docTC);
                tr.Cells.Add(delDocTC);
                documentsTable.Rows.Add(tr);
                us.DocumentsTRs.Add(tr);
            }
        }

        private void buildStakeholders(IssueModel issue, UserSession us)
        {
            TableRow tr;
            TableCell stkTC, delTC;
            Label userLabel;
            Button delStkBtn;

            foreach (StakeholderModel sm in issue.Stakeholders)
            {
                tr = new TableRow();
                tr.ID = "stkTR" + sm.Id;
                userLabel = new Label();
                userLabel.ID = "stkLBL" + sm.Id;
                userLabel.Text = sm.Name;
                stkTC = new TableCell();
                stkTC.ID = "stkTC" + sm.Id;
                stkTC.Controls.Add(userLabel);
                delStkBtn = new Button();
                delStkBtn.ID = "delStkBTN" + sm.Id;
                delStkBtn.Text = "X";
                delStkBtn.Click += delStkBtn_Click;
                delTC = new TableCell();
                delTC.ID = "delStkTC" + sm.Id;
                delTC.Controls.Add(delStkBtn);
                tr.Cells.Add(stkTC);
                tr.Cells.Add(delTC);
                stakeholderTable.Rows.Add(tr);
                us.StakeholdersTRs.Add(tr);
            }  
        }

        private void buildArtefacts(IssueModel issue, UserSession us)
        {
            TableRow tr;
            TableCell artTC, delArtTC;
            Label artLabel;
            Button delArtBtn;

            foreach (ArtefactModel artefact in issue.Artefacts)
            {
                tr = new TableRow();
                tr.ID = "artTR" + artefact.Id;
                artLabel = new Label();
                artLabel.ID = "artLBL" + artefact.Id;
                artLabel.Text = artefact.Name;
                artTC = new TableCell();
                artTC.ID = "artTC" + artefact.Id;
                artTC.Controls.Add(artLabel);
                delArtBtn = new Button();
                delArtBtn.ID = "delartBTN" + artefact.Id;
                delArtBtn.Text = "X";
                delArtBtn.Click += delArtBtn_Click;
                delArtTC = new TableCell();
                delArtTC.ID = "delArtTC" + artefact.Id;
                delArtTC.Controls.Add(delArtBtn);
                tr.Cells.Add(artTC);
                tr.Cells.Add(delArtTC);
                artefactsTable.Rows.Add(tr);
                us.ArtefactsTRs.Add(tr);
            }
        }

        private void buildFactors(IssueModel issue, UserSession us)
        {
            us = SessionManager.GetUserSession(Session.SessionID);
            foreach (InfluenceFactorModel fact in issue.InfluenceFactors)
            {
                TextBox factorNameTxt = new TextBox();
                factorNameTxt.ID = "factNameTXT" + fact.Id;
                factorNameTxt.Width = 100;
                factorNameTxt.Text = fact.Name;
                TableCell nameTC = new TableCell();
                nameTC.Controls.Add(factorNameTxt);

                TextBox factorCharacteristicsTxt = new TextBox();
                factorCharacteristicsTxt.ID = "factCharTXT" + fact.Id;
                factorCharacteristicsTxt.Width = 130;
                factorCharacteristicsTxt.Text = fact.Characteristic;
                TableCell characteristicsTC = new TableCell();
                characteristicsTC.Controls.Add(factorCharacteristicsTxt);

                CheckBox numericChck = new CheckBox();
                numericChck.ID = "factNumCHK" + fact.Id;
                numericChck.Width = 50;
                numericChck.Checked = fact.Type;
                TableCell numericTC = new TableCell();
                numericTC.Controls.Add(numericChck);

                Button delFactorBtn = new Button();
                delFactorBtn.ID = "delFactBTN" + fact.Id;
                delFactorBtn.Text = "X";
                delFactorBtn.CssClass = "delete_button";
                delFactorBtn.Click += delFactorBtn_Click;
                TableCell delTC = new TableCell();
                delTC.Controls.Add(delFactorBtn);

                TableRow tr = new TableRow();
                tr.ID = "factTR" + fact.Id;
                tr.Cells.Add(nameTC);
                tr.Cells.Add(characteristicsTC);
                tr.Cells.Add(numericTC);
                tr.Cells.Add(delTC);

                factorsTable.Rows.Add(tr);
                us.FactorTRs.Add(tr);
            }
        }

        private void buildAccessRights(IssueModel issue, UserSession us)
        {
            TableRow tr;
            TableCell userTC, rightTC, delTC;
            Label userLabel, acLabel;
            DropDownList userRightDDL;
            Button delUserBtn;
            foreach (AccessRightModel ar in issue.AccessUserList)
            {
                tr = new TableRow();
                tr.ID = "usrTR" + ar.User.AccessObject;

                acLabel = new Label();
                acLabel.ID = "acLBL" + ar.User.AccessObject;
                acLabel.CssClass = "UserAcronym";
                if (ar != null && ar.User != null && ar.User.FirstName != null && ar.User.LastName != null)
                {
                    if (ar.User.FirstName != "" && ar.User.LastName != "")
                    {
                        acLabel.Text = ar.User.FirstName.Substring(0, 1) + ar.User.LastName.Substring(0, 1);
                    }
                }
                if (acLabel.Text.Equals(""))
                {
                    acLabel.Text = "&nbsp;";
                }

                userLabel = new Label();
                userLabel.ID = "usrLBL" + ar.User.AccessObject;
                userLabel.Text = ar.User.FirstName + " " + ar.User.LastName;
                userTC = new TableCell();
                userTC.ID = "usrTC" + ar.User.AccessObject;
                userTC.Controls.Add(acLabel);
                userTC.Controls.Add(userLabel);
                userRightDDL = new DropDownList();
                userRightDDL.Items.Add("Contributor");
                userRightDDL.Items.Add("Viewer");
                userRightDDL.Items.Add("Owner");
                if (ar.Right.Equals('C'))
                {
                    userRightDDL.SelectedIndex = 0;
                }
                else if (ar.Right.Equals('V'))
                {
                    userRightDDL.SelectedIndex = 1;
                }
                else
                {
                    userRightDDL.SelectedIndex = 2;
                }
                rightTC = new TableCell();
                rightTC.ID = "rightTC" + ar.User.AccessObject;
                rightTC.Controls.Add(userRightDDL);
                delUserBtn = new Button();
                delUserBtn.ID = "delUsrBTN" + ar.User.AccessObject;
                delUserBtn.Text = "X";
                delUserBtn.Click += delUserBtn_Click;
                delTC = new TableCell();
                delTC.ID = "delTC" + ar.User.AccessObject;
                delTC.Controls.Add(delUserBtn);
                tr.Cells.Add(userTC);
                tr.Cells.Add(rightTC);
                tr.Cells.Add(delTC);
                usersTable.Rows.Add(tr);
                us.AccessRTRs.Add(tr);

                if (User.Identity.IsAuthenticated && ar.User.Email.Equals(User.Identity.Name))
                {
                    usersTable.ID = "USR" + ar.User.AccessObject.ToString();
                }
            }

            //delete
            if (!User.Identity.IsAuthenticated)
            {
                usersTable.ID = "USR1";
            }
        }

        void delUserBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;

            if (((DropDownList)t.Cells[1].Controls[0]).SelectedIndex == 2)
            {
                return;
            }
            
            usersTable.Rows.Remove(t);
            us.AccessRTRs.Clear();
            
            foreach (TableRow tr in usersTable.Rows)
            {
                if (tr.GetType() != typeof(TableHeaderRow))
                {
                    us.AccessRTRs.Add(tr);
                }
            }
        }

        void delStkBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;

            stakeholderTable.Rows.Remove(t);
            us.StakeholdersTRs.Clear();

            foreach (TableRow tr in stakeholderTable.Rows)
            {
                us.StakeholdersTRs.Add(tr);
            }
        }

        void delArtBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;

            artefactsTable.Rows.Remove(t);
            us.ArtefactsTRs.Clear();

            foreach (TableRow tr in artefactsTable.Rows)
            {
                us.ArtefactsTRs.Add(tr);
            }
        }

        void delFactorBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;

            factorsTable.Rows.Remove(t);
            us.FactorTRs.Clear();

            for (int i = 1; i < factorsTable.Rows.Count;i++ )
            {
                us.FactorTRs.Add(factorsTable.Rows[i]);
            }
        }

        void delDocBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;
            RestClient rc = RestClient.GetInstance(Session.SessionID);

            if (t.Cells[0].Controls[0].GetType() == typeof(HyperLink)){
                us.DocsToDelete.Add(((HyperLink)t.Cells[0].Controls[0]).Text);
            }
            else
            {
                rc.RemoveFile(((HyperLink)t.Cells[0].Controls[0]).Text);
            }

            documentsTable.Rows.Remove(t);
            us.DocumentsTRs.Clear();

            foreach(TableRow tr in documentsTable.Rows)
            {
                us.DocumentsTRs.Add(tr);
            }
        }

        void delCritBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            int id = int.Parse(t.ID.Replace("critTR",""));

            if (id > 0)
            {
                us.CriteriasToDelete.Add(id);
            }

            criteriaTable.Rows.Remove(t);
            us.CriteriaTRs.Clear();

            foreach (TableRow tr in criteriaTable.Rows)
            {
                us.CriteriaTRs.Add(tr);
            }
        }

        protected void delAltBtn_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow t = (TableRow)((TableCell)((Button)sender).Parent).Parent;
            int id = int.Parse(t.ID.Replace("altTR", ""));

            if (id > 0)
            {
                us.AlternativesToDelete.Add(id);
            }

            alternativesTable.Rows.Remove(t);
            us.AlternativesTRs.Clear();

            for (int i = 1; i < alternativesTable.Rows.Count; i++ )
            {
                us.AlternativesTRs.Add(alternativesTable.Rows[i]);
            }
        }
        
        protected void doPermissions(IssueModel issue, RestClient rc)
        {
            char accessRight;
            accessRight = 'V';
            foreach (AccessRightModel ar in issue.AccessUserList)
            {
                if (ar.User.AccessObject == rc.User.AccessObject)
                {
                    accessRight = ar.Right;
                }
            }

            if (accessRight.Equals('V'))
            {
                //disable ALL

                disableArtefactsEdit();
                disableDocumentsEdit();
                disableStakeholdersEdit();
                disableAlternativesEdit();
                disableFactorsEdit();

                descriptionText.Enabled = false;
                titleText.Enabled = false;
                
                

                save.Visible = false;

                //
                for (int i = 1; i < criteriaWeightTable.Rows.Count; i++)
                {
                    ((TextBox)criteriaWeightTable.Rows[i].Cells[1].Controls[0]).Visible = false;
                }

                //disable delete tags
                disableTags();

                disableCriteria();
                

                //critweight TC
                foreach (TableRow tr in criteriaWeightTable.Rows)
                {
                    tr.Cells[1].Visible = false;
                }
            }
            else
            {
                //depeinding on stage
                descriptionText.Enabled = true;
                titleText.Enabled = true;
                save.Visible = true;
                addArtefact.Visible = true;
                addStakeholder.Visible = true;
                addFactor.Visible = true;

                if (issue.Status.ToUpper().Equals("CREATING"))
                {
                    criteriaWeightPanel.Visible = false;
                }
                if (issue.Status.Equals("Brainstorming1"))
                {
                    addCriteriaButton.Visible = true;
                    criteriaWeightPanel.Visible = false;
                }
                else
                {
                    addCriteriaButton.Visible = false;
                    foreach (TableRow tr in criteriaTable.Rows)
                    {
                        ((TextBox)tr.Cells[0].Controls[0]).Enabled = false;
                        ((TextBox)tr.Cells[1].Controls[0]).Enabled = false;
                        ((Button)tr.Cells[2].Controls[0]).Visible = false;
                    }
                    criteriaWeightPanel.Visible = true;
                }

                if (issue.Status.ToUpper().Equals("EVALUATING"))
                {
                    evaluationPanel.Visible = true;
                }
            }

            if (accessRight.Equals('O'))
            {
                saveNext.Visible = true;
                foreach (TableRow tr in usersTable.Rows)
                {
                    if (tr.GetType() != typeof(TableHeaderRow))
                    {
                        ((DropDownList)tr.Cells[1].Controls[0]).Enabled = true;
                        ((Button)tr.Cells[2].Controls[0]).Visible = true;
                    }
                }

                addUser.Visible = true;
            }
            else
            {
                saveNext.Visible = false;
                foreach (TableRow tr in usersTable.Rows)
                {
                    if (tr.GetType() != typeof(TableHeaderRow)) { 
                        ((DropDownList)tr.Cells[1].Controls[0]).Enabled = false;
                        ((Button)tr.Cells[2].Controls[0]).Visible = false;
                    }
                }
                addUser.Visible = false;
            }

            //STATUS ONLY - disable criteria weight textboxes when status not in BR2
            if (!issue.Status.ToUpper().Equals("BRAINSTORMING2"))
            {
                for (int i = 1; i < criteriaWeightTable.Rows.Count; i++ )
                {
                    TableRow tr = criteriaWeightTable.Rows[i];
                    ((TextBox)tr.Cells[1].Controls[0]).Enabled = false;
                }
            }

            if (issue.Status.ToUpper().Equals("CREATING"))
            {
                alternativesPanel.Visible = false;
            }

            if (!issue.Status.ToUpper().Equals("FINISHED"))
            {
                for (int i = 1; i < alternativesTable.Rows.Count; i++)
                {
                    alternativesTable.Rows[i].Cells[4].Visible = false;
                }
            }

            if (issue.Status.ToUpper().Equals("FINISHED"))
            {
                for (int i = 1; i < alternativesTable.Rows.Count; i++)
                {
                    alternativesTable.Rows[i].Cells[3].Visible = false;
                }
                save.Visible = false;
                saveNext.Visible = false;
                descriptionPanel.Enabled = false;
                titleText.Enabled = false;
                addUser.Visible = false;
            }

            if (issue.Status.ToUpper().Equals("FINISHED") || issue.Status.ToUpper().Equals("EVALUATING"))
            {
                disableStakeholdersEdit();
                disableFactorsEdit();
                disableDocumentsEdit();
                disableAlternativesEdit();
                disableArtefactsEdit();
                disableCriteria();
                disableTags();
            }
        }

        public void disableCriteria()
        {
            foreach (TableRow tr in criteriaTable.Rows)
            {
                ((Button)tr.Cells[2].Controls[0]).Visible = false;
                ((TextBox)tr.Cells[0].Controls[0]).Enabled = false;
                ((TextBox)tr.Cells[1].Controls[0]).Enabled = false;
            }
            addCriteriaButton.Visible = false;
        }

        private void disableTags()
        {
            foreach (Control c in tagPanel.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    ((Button)c).Enabled = false;
                }
            }
            addTagButton.Visible = false;
        }

        private void disableStakeholdersEdit() {
            foreach (TableRow tr in stakeholderTable.Rows)
            {
                ((Button)tr.Cells[1].Controls[0]).Visible = false;
            }
            addStakeholder.Visible = false;
        }

        private void disableFactorsEdit()
        {
            int cnt = 0;
            foreach (TableRow tr in factorsTable.Rows)
            {
                if (cnt > 0)
                {
                    ((Button)tr.Cells[3].Controls[0]).Visible = false;
                    ((CheckBox)tr.Cells[2].Controls[0]).Enabled = false;
                    ((TextBox)tr.Cells[1].Controls[0]).Enabled = false;
                    ((TextBox)tr.Cells[0].Controls[0]).Enabled = false;
                }
                cnt++;
            }
            addFactor.Visible = false;
        }

        private void disableDocumentsEdit()
        {
            foreach (TableRow tr in documentsTable.Rows)
            {
                ((Button)tr.Cells[1].Controls[0]).Visible = false;
            }
            addDocumentBtn.Visible = false;
        }

        private void disableArtefactsEdit()
        {
            foreach (TableRow tr in artefactsTable.Rows)
            {
                ((Button)tr.Cells[1].Controls[0]).Visible = false;
            }
            addArtefact.Visible = false;
        }

        private void disableAlternativesEdit()
        {
            //alternatives
            for (int i = 1; i < alternativesTable.Rows.Count; i++)
            {
                ((TextBox)alternativesTable.Rows[i].Cells[0].Controls[0]).Enabled = false;
                ((TextBox)alternativesTable.Rows[i].Cells[1].Controls[0]).Enabled = false;
                ((TextBox)alternativesTable.Rows[i].Cells[2].Controls[0]).Enabled = false;
                ((Button)alternativesTable.Rows[i].Cells[3].Controls[0]).Visible = false;
            }
            addAlternativeButton.Visible = false;
        }

        void tagButton_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            Button b = (Button)sender;
            us.IssueTags.Remove(b);
            tagPanel.Controls.Remove(b);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            saveIssue();
            Response.Redirect("IssueDetail?issueId=" + Request["issueId"]);
         }

        protected void saveNext_Click(object sender, EventArgs e)
        {
            saveIssue();
            RestClient rc = RestClient.GetInstance(Session.SessionID);

            rc.EndPoint = "api/Issue/" + Request["issueId"] + "/nextStage";
            rc.Method = HttpVerb.POST;
            var json = rc.MakeRequest();

            Response.Redirect("IssueDetail?issueId=" + Request["issueId"]);
        }

        protected void saveIssue()
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            IssueModel issue = new IssueModel();

            if (statusLabel.Text.Equals("Brainstorming2") && !checkCriteriaWeights())
            {
                us.Messages.Add("Changes not saved! Sum of Crieria Weights must be 100");
                return;
            }

            issue.Id = int.Parse(Request["issueId"]);
            issue.Title = titleText.Text;
            issue.Description = descriptionText.Text;

            if (relatedIssueLink.Visible)
            {
                issue.RelatedTo = int.Parse(relatedIssueLink.NavigateUrl.Replace("/IssueDetail?issueId=", ""));
                issue.RelationType = relationTypeLabel.Text[0];
            }

            //retrieve tag infos
            TagModel tm;
            foreach (Control c in tagPanel.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    tm = new TagModel(int.Parse(c.ID.Replace("TAGBTN","")), ((Button)c).Text);
                    issue.Tags.Add(tm);
                }
                else if (c.GetType() == typeof(TextBox))
                {
                    tm = new TagModel(((TextBox)c).Text);
                    issue.Tags.Add(tm);
                }
            }

            //retrieve access right infos
            int aoid;
            Dictionary<int, char> dict = new Dictionary<int, char>();
            foreach (TableRow tr in usersTable.Rows)
            {
                if (tr.GetType() != typeof(TableHeaderRow))
                {
                    aoid = int.Parse(tr.ID.Replace("usrTR", ""));
                    if (aoid < 0)
                    {
                        aoid = int.Parse(((DropDownList)tr.Cells[0].Controls[0]).SelectedItem.Value);
                    }
                    dict.Add(aoid, ((DropDownList)tr.Cells[1].Controls[0]).SelectedItem.Text[0]);
                }
            }
            issue.AccessRights = dict;

            //retrieve stakeholder infos
            List<StakeholderModel> smList = new List<StakeholderModel>();
            foreach (TableRow tr in stakeholderTable.Rows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    DropDownList stkList = (DropDownList)tr.Cells[0].Controls[0];
                    if (stkList.SelectedIndex > 0)
                    {
                        smList.Add(new StakeholderModel(int.Parse(stkList.SelectedItem.Value), ""));
                    }
                }
                else if (tr.Cells[0].Controls[0].GetType() == typeof(TextBox))
                {
                    smList.Add(new StakeholderModel(((TextBox)tr.Cells[0].Controls[0]).Text));
                }
                else
                {
                    smList.Add(new StakeholderModel(int.Parse(tr.ID.Replace("stkTR", "")), ""));
                }
            }
            issue.Stakeholders = smList;

            //retrieve artefacts infos
            List<ArtefactModel> artList = new List<ArtefactModel>();
            foreach (TableRow tr in artefactsTable.Rows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    DropDownList stkList = (DropDownList)tr.Cells[0].Controls[0];
                    if (stkList.SelectedIndex > 0)
                    {
                        artList.Add(new ArtefactModel(int.Parse(stkList.SelectedItem.Value), ""));
                    }
                }
                else if (tr.Cells[0].Controls[0].GetType() == typeof(TextBox))
                {
                    artList.Add(new ArtefactModel(((TextBox)tr.Cells[0].Controls[0]).Text));
                }
                else
                {
                    artList.Add(new ArtefactModel(int.Parse(tr.ID.Replace("artTR", "")), ""));
                }
            }
            issue.Artefacts = artList;
            
            //retrieve factor infos
            List<InfluenceFactorModel> factors = new List<InfluenceFactorModel>();
            TableRow t;
            for (int i = 1; i < factorsTable.Rows.Count; i++)
            {
                t = factorsTable.Rows[i];
                factors.Add(new InfluenceFactorModel(((TextBox)t.Cells[0].Controls[0]).Text, ((CheckBox)t.Cells[2].Controls[0]).Checked, ((TextBox)t.Cells[1].Controls[0]).Text));
            }
            issue.InfluenceFactors = factors;

            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (!issue.Status.ToUpper().Equals("EVALUATING") && !issue.Status.ToUpper().Equals("FINISHED"))
            {
                rc.EndPoint = "api/Issue/Edit";
                rc.Method = HttpVerb.POST;
                rc.PostData = JsonConvert.SerializeObject(issue);
                string res = rc.MakeRequest();
                saveDocuments(rc, issue, us);
            }

            if (statusLabel.Text.Equals("Brainstorming2"))
            {
                saveCriteriaWeights(rc);
            }

            if (statusLabel.Text.ToUpper().Equals("BRAINSTORMING1") || statusLabel.Text.ToUpper().Equals("BRAINSTORMING2"))
            {
                saveAlternatives(rc);
                saveCriterias(rc, issue, us);
            }

            if (statusLabel.Text.ToUpper().Equals("EVALUATING"))
            {
                saveRating(rc);
            }
        }

        private void saveDocuments(RestClient rc, IssueModel issue, UserSession us)
        {
            rc.UploadFilesToRemoteUrl(issue.Id);
            foreach (string doc in us.DocsToDelete)
            {
                rc.EndPoint = "api/Document?issueId=" + issue.Id + "&filename=" + doc;
                rc.Method = HttpVerb.DELETE;
                rc.MakeRequest();
            }
        }

        private void saveCriterias(RestClient rc, IssueModel issue, UserSession us)
        {
            foreach (TableRow tr in criteriaTable.Rows)
            {
                int id = int.Parse(tr.ID.Replace("critTR", ""));
                CriterionModel cm = new CriterionModel();
                rc.EndPoint = "api/Criterion";
                cm.Name = ((TextBox)tr.Cells[0].Controls[0]).Text;
                cm.Description = ((TextBox)tr.Cells[1].Controls[0]).Text;
                cm.Issue = issue.Id;
                if (id < 0)
                {
                    rc.Method = HttpVerb.POST;
                }
                else
                {
                    rc.Method = HttpVerb.PUT;
                    cm.Id = id;
                }
                rc.PostData = JsonConvert.SerializeObject(cm);
                rc.MakeRequest();
            }

            foreach (int id in us.CriteriasToDelete)
            {
                rc.EndPoint = "api/Criterion/" + id;
                rc.Method = HttpVerb.DELETE;
                rc.MakeRequest();
            }
        }

        private void saveRating(RestClient rc)
        {
            TableRow tr;
            TextBox txt;
            List<RatingModel> ratList = new List<RatingModel>();
            RatingModel rat;

            if (evaluationTable.Rows.Count == 0)
            {
                return;
            }

            for(int i = 1; i < evaluationTable.Rows.Count; i++){

                for (int j = 1; j < evaluationTable.Rows[i].Cells.Count; j++ )
                {
                    txt = (TextBox)evaluationTable.Rows[i].Cells[j].Controls[0];
                    rat = new RatingModel();
                    rat.Rating1 = double.Parse(txt.Text);
                    rat.AlternativeID = int.Parse(evaluationTable.Rows[i].Cells[j].ID.Split('x')[1]);
                    rat.CriterionID = int.Parse(evaluationTable.Rows[i].Cells[j].ID.Split('x')[0]);
                    ratList.Add(rat);
                }
            }

            
            rc.PostData = JsonConvert.SerializeObject(ratList);
            rc.Method = HttpVerb.POST;
            if (int.Parse(evaluationTable.Rows[0].ID.Replace("ratingTHR","")) < 0){
                rc.EndPoint = "api/Rating/All";
            }else{
                rc.EndPoint = "api/Rating/UpdateAllForIssue";
            }
            rc.MakeRequest();
        }

        private void saveAlternatives(RestClient rc)
        {
            AlternativeModel alt;
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow tr;
            for (int i = 1; i < alternativesTable.Rows.Count; i++)
            {
                tr = alternativesTable.Rows[i];
                alt = new AlternativeModel();
                alt.Id = int.Parse(tr.ID.Replace("altTR", ""));
                alt.Name = ((TextBox)tr.Cells[0].Controls[0]).Text;
                alt.Description = ((TextBox)tr.Cells[1].Controls[0]).Text;
                alt.Reason = ((TextBox)tr.Cells[2].Controls[0]).Text;
                alt.Issue = int.Parse(Request["issueId"]);
                if (alt.Id >= 0)
                {
                    rc.EndPoint = "api/Alternative/Update";
                }
                else
                {
                    rc.EndPoint = "api/Alternative/Create";
                }

                rc.Method = HttpVerb.POST;
                rc.PostData = JsonConvert.SerializeObject(alt);
                rc.MakeRequest();
            }

            foreach (int altId in us.AlternativesToDelete)
            {
                rc.EndPoint = "api/Alternative?alternativeId=" + altId;
                rc.Method = HttpVerb.DELETE;
                rc.MakeRequest();
            }
        }

        private void saveCriteriaWeights(RestClient rc)
        {
            //saveCriteriaWeigts
            List<CriterionWeightModel> cwmList = new List<CriterionWeightModel>();
            bool insert;
            insert = true;
            int crID;
            if (criteriaWeightTable.Rows.Count > 1 && int.Parse(criteriaWeightTable.Rows[1].Cells[1].ID.Replace("critWeigtWTC","")) < 0)
            {
                insert = false;
            }
            for (int i = 1; i < criteriaWeightTable.Rows.Count; i++)
            {
                TableRow tr = criteriaWeightTable.Rows[i];
                crID = int.Parse(tr.ID.Replace("critWTR", ""));
                cwmList.Add(new CriterionWeightModel(crID, double.Parse(((TextBox)tr.Cells[1].Controls[0]).Text) / 100));
            }
            if (insert)
            {
                rc.EndPoint = "api/CriterionWeight/Add";
            }
            else
            {
                rc.EndPoint = "api/CriterionWeight/Update";
            }
            rc.Method = HttpVerb.POST;
            rc.PostData = JsonConvert.SerializeObject(cwmList);
            rc.MakeRequest();
        }

        protected void addTagButton_Click(object sender, EventArgs e)
        {
            List<TagModel> tagsList;
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            UserSession userSession = SessionManager.GetUserSession(Session.SessionID);

            rc.EndPoint = "api/Tags";
            tagsList = JsonConvert.DeserializeObject<List<TagModel>>(rc.MakeRequest());

            DropDownList tagsDDL = new DropDownList();
            tagsDDL.Items.Add(new ListItem("", "-1"));
            tagsDDL.SelectedIndexChanged += tagsDDL_SelectedIndexChanged;
            tagsDDL.CssClass = "dropdown_tag";
            tagsDDL.AutoPostBack = true;
            tagsDDL.ID = "tagDDL" + userSession.NextTTRID;
            foreach (TagModel tag in tagsList)
            {
                tagsDDL.Items.Add(new ListItem(tag.Name, tag.Id.ToString()));
            }
            tagsDDL.Items.Add(new ListItem("New...", "-2"));
            tagPanel.Controls.Add(new LiteralControl("<br />"));
            tagPanel.Controls.Add(tagsDDL);

            userSession.IssueTags.Add(new LiteralControl("<br />"));
            userSession.IssueTags.Add(tagsDDL);
        }

        void tagsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            DropDownList dpList = (DropDownList)sender;
            

            if (dpList.SelectedIndex == dpList.Items.Count - 1)
            {
                TextBox t = new TextBox();
                t.CssClass = "maxwidth";
                int idx = tagPanel.Controls.IndexOf(dpList);
                t.ID = dpList.ID.Replace("tagDDL", "tagTXT");
                tagPanel.Controls.AddAt(idx, t);
                tagPanel.Controls.Remove(dpList);

                session.IssueTags.Clear();
                foreach(Control c in tagPanel.Controls){
                    session.IssueTags.Add(c);
                }
            }
            else if (dpList.SelectedIndex > 0)
            {
                if (FindControl("TAGBTN" + dpList.SelectedItem.Value) != null)
                {
                    dpList.SelectedIndex = 0;
                    return;
                }
                string del = dpList.ID;
                tagPanel.Controls.Remove(dpList);


                Button tagButton = new Button();
                tagButton = new Button();
                tagButton.ID = "TAGBTN" + dpList.SelectedItem.Value;
                tagButton.Click += tagButton_Click;
                tagButton.ToolTip = "Remove";
                tagButton.Text = dpList.SelectedItem.Text ;
                tagPanel.Controls.Add(tagButton);

                foreach (Control c in session.IssueTags)
                {
                    if (c.ID != null && c.ID.Equals(del))
                    {
                        session.IssueTags.Remove(c);
                        break;
                    }
                }

                session.IssueTags.Add(tagButton);
            }
        }

        protected void addUser_Click(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow tr;
            TableCell userTC, rightTC, delTC;
            DropDownList userDrpList;
            DropDownList userRightDDL;
            Button delUserBtn;
            int key = us.NextAccesTRKey;
            tr = new TableRow();
            tr.ID = "usrTR-" + key;

            rc.EndPoint = "api/User";
            List<UserShort> userList = JsonConvert.DeserializeObject<List<UserShort>>(rc.MakeRequest());
            userDrpList = new DropDownList();
            userDrpList.ID = "USRList-" + key;
            userDrpList.Items.Add(new ListItem(""));
            foreach (UserShort u in userList)
            {
                userDrpList.Items.Add(new ListItem(u.FirstName + " " + u.LastName, u.AccessObject.ToString()));
            }
            
            userTC = new TableCell();
            userTC.ID = "usrTC-" + key;
            userTC.Controls.Add(userDrpList);
            
            userRightDDL = new DropDownList();
            userRightDDL.Items.Add("Contributor");
            userRightDDL.Items.Add("Viewer");
            userRightDDL.Items.Add("Owner");
            rightTC = new TableCell();
            rightTC.ID = "rightTC-" + us.NextATRID;
            rightTC.Controls.Add(userRightDDL);
            delUserBtn = new Button();
            
            delUserBtn.ID = "delUsrBTN-" + key;
            delUserBtn.Text = "X";
            delUserBtn.Click += delUserBtn_Click;
            delTC = new TableCell();
            delTC.ID = "delTC-" + key;
            delTC.Controls.Add(delUserBtn);
            tr.Cells.Add(userTC);
            tr.Cells.Add(rightTC);
            tr.Cells.Add(delTC);
            usersTable.Rows.Add(tr);
            us.AccessRTRs.Add(tr);
        }

        protected void addStakeholder_Click(object sender, EventArgs e)
        {
            TableRow tr;
            TableCell stkTC, delTC;
            DropDownList stkDropList;
            Button delStkBtn;
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            rc.EndPoint = "api/Stakeholders";
            List<StakeholderModel> stakeholdersList = JsonConvert.DeserializeObject<List<StakeholderModel>>(rc.MakeRequest());
            string id = "-" + us.NextSTRID;

            tr = new TableRow();
            tr.ID = "stkTR" + id;


            stkDropList = new DropDownList();
            stkDropList.ID = "stkDrp" + id;
            stkDropList.Items.Add("");
            foreach (StakeholderModel sm in stakeholdersList)
            {
                stkDropList.Items.Add(new ListItem(sm.Name, sm.Id.ToString()));
            }
            stkDropList.Items.Add("new...");
            stkDropList.SelectedIndexChanged += stkDropList_SelectedIndexChanged;
            stkDropList.AutoPostBack = true;

            stkTC = new TableCell();
            stkTC.ID = "stkTC" + id;
            stkTC.Controls.Add(stkDropList);
            delStkBtn = new Button();
            delStkBtn.ID = "delStkBTN" + id;
            delStkBtn.Text = "X";
            delStkBtn.Click += delStkBtn_Click;
            delTC = new TableCell();
            delTC.ID = "delStkTC" + id;
            delTC.Controls.Add(delStkBtn);
            tr.Cells.Add(stkTC);
            tr.Cells.Add(delTC);
            stakeholderTable.Rows.Add(tr);
            us.StakeholdersTRs.Add(tr);
        }

        protected void addArtefact_Click(object sender, EventArgs e)
        {
            TableRow tr;
            TableCell artTC, delArtTC;
            DropDownList artDropList;
            Button delArtBtn;
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            rc.EndPoint = "api/Artefacts";
            List<ArtefactModel> artefactsList = JsonConvert.DeserializeObject<List<ArtefactModel>>(rc.MakeRequest());
            string id = "-" + us.NextATRID;

            tr = new TableRow();
            tr.ID = "artTR" + id;


            artDropList = new DropDownList();
            artDropList.ID = "artDrp" + id;
            artDropList.Items.Add("");
            foreach (ArtefactModel art in artefactsList)
            {
                artDropList.Items.Add(new ListItem(art.Name, art.Id.ToString()));
            }
            artDropList.Items.Add("new...");
            artDropList.SelectedIndexChanged += artDropList_SelectedIndexChanged;
            artDropList.AutoPostBack = true;

            artTC = new TableCell();
            artTC.ID = "artTC" + id;
            artTC.Controls.Add(artDropList);
            delArtBtn = new Button();
            delArtBtn.ID = "delArtBTN" + id;
            delArtBtn.Text = "X";
            delArtBtn.Click += delArtBtn_Click;
            delArtTC = new TableCell();
            delArtTC.ID = "delArtTC" + id;
            delArtTC.Controls.Add(delArtBtn);
            tr.Cells.Add(artTC);
            tr.Cells.Add(delArtTC);
            artefactsTable.Rows.Add(tr);
            us.ArtefactsTRs.Add(tr);
        }

        void artDropList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            DropDownList drpList = (DropDownList)sender;
            if (drpList.SelectedValue.Equals("new..."))
            {
                TableCell tc = ((TableCell)drpList.Parent);
                tc.Controls.Remove(drpList);
                TextBox txt = new TextBox();
                txt.ID = "artTXT-" + tc.ID.Replace("artTC", "");
                tc.Controls.Add(txt);
            }
        }

        void stkDropList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            DropDownList drpList = (DropDownList) sender;
            if (drpList.SelectedValue.Equals("new..."))
            {
                TableCell tc = ((TableCell)drpList.Parent);
                tc.Controls.Remove(drpList);
                TextBox txt = new TextBox();
                txt.ID = "stkTXT-" + tc.ID.Replace("stkTC","");
                tc.Controls.Add(txt);
            }
        }

        protected void addFactor_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            string id = us.NextFTRID;
            TextBox factorNameTxt = new TextBox();
            factorNameTxt.ID = "factNameTXT-" + id;
            factorNameTxt.Width = 100;
            TableCell nameTC = new TableCell();
            nameTC.Controls.Add(factorNameTxt);

            TextBox factorCharacteristicsTxt = new TextBox();
            factorCharacteristicsTxt.ID = "factCharTXT-" + id;
            factorCharacteristicsTxt.Width = 130;
            TableCell characteristicsTC = new TableCell();
            characteristicsTC.Controls.Add(factorCharacteristicsTxt);

            CheckBox numericChck = new CheckBox();
            numericChck.ID = "factNumCHK-" + id;
            numericChck.Width = 50;
            TableCell numericTC = new TableCell();
            numericTC.Controls.Add(numericChck);

            Button delFactorBtn = new Button();
            delFactorBtn.ID = "delFactBTN-" + id;
            delFactorBtn.Text = "X";
            delFactorBtn.CssClass = "delete_button";
            delFactorBtn.Click += delFactorBtn_Click;
            TableCell delTC = new TableCell();
            delTC.Controls.Add(delFactorBtn);

            TableRow tr = new TableRow();
            tr.ID = "factTR-" + id;
            tr.Cells.Add(nameTC);
            tr.Cells.Add(characteristicsTC);
            tr.Cells.Add(numericTC);
            tr.Cells.Add(delTC);

            factorsTable.Rows.Add(tr);
            us.FactorTRs.Add(tr);
        }

        protected void addDocumentBtn_Click(object sender, EventArgs e)
        {
            FileUpload fileUpload = new FileUpload();
            fileUpload.Visible = true;
            fileUpload.Attributes.Add("onchange", "this.form.submit()");
            
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            string id = us.NextDocTRKey;
            TableRow tr = new TableRow();
            tr.ID = "docTR" + id;
            TableCell docTC = new TableCell();
            docTC.ID = "docTC" + id;
            docTC.Controls.Add(fileUpload);
            Button delDocBtn = new Button();
            delDocBtn.Click += delDocBtn_Click;
            delDocBtn.ID = "delDocBTN" + id;
            delDocBtn.Text = "X";
            TableCell delDocTC = new TableCell();
            delDocTC.ID = "delDocTC" + id;
            delDocTC.Controls.Add(delDocBtn);
            tr.Cells.Add(docTC);
            tr.Cells.Add(delDocTC);
            documentsTable.Rows.Add(tr);
            us.DocumentsTRs.Add(tr);
        }

        protected void addCriteriaButton_Click(object sender, EventArgs e)
        {
            TableRow tr;
            TableCell critDescTC, critTC, delCritTC;
            TextBox critTXT, critDescTXT;
            Button delCritBtn;
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            string id = us.NextCritTRKey;

            tr = new TableRow();
            tr.ID = "critTR-" + id;
            critTC = new TableCell();
            critTC.ID = "critTC-" + id;
            critDescTC = new TableCell();
            critDescTC.ID = "critDescTC-" + id;
            delCritTC = new TableCell();
            delCritTC.ID = "delCritTC-" + id;
            critTXT = new TextBox();
            critTXT.ID = "critTXT-" + id;
            critDescTXT = new TextBox();
            critDescTXT.ID = "critDescTXT-" + id;
            delCritBtn = new Button();
            delCritBtn.Text = "X";
            delCritBtn.ID = "delCritBtn-" + id;
            delCritBtn.Click += delCritBtn_Click;
            critDescTC.Controls.Add(critDescTXT);
            critTC.Controls.Add(critTXT);
            delCritTC.Controls.Add(delCritBtn);
            tr.Cells.Add(critTC);
            tr.Cells.Add(critDescTC);
            tr.Cells.Add(delCritTC);
            criteriaTable.Rows.Add(tr);
            us.CriteriaTRs.Add(tr);
        }

        

        private bool checkCriteriaWeights()
        {
            double sum = 0;
            TextBox txt;
            TableRow tr;
            for (int i = 1; i < criteriaWeightTable.Rows.Count; i++ )
            {
                tr = criteriaWeightTable.Rows[i];
                txt = (TextBox)tr.Cells[1].Controls[0];
                if (txt.Text.Length > 0)
                {
                    sum = sum + double.Parse(txt.Text);
                }
            }

            if (sum == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void addAlternativeButton_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            TableRow tr;
            TableCell altTC, descAltTC, altReasonTC, delAltTC, altRatTC;
            TextBox altTXT, descAltTXT, altReasonTXT;
            Button delAltBtn;
            Label altRatLbl;
            int id = us.NextAltKey;

            tr = new TableRow();
            tr.ID = "altTR-" + id;
            altTC = new TableCell();
            altTC.ID = "altTC-" + id;
            altTXT = new TextBox();
            altTXT.ID = "altTXT-" + id;
            altTC.Controls.Add(altTXT);
            tr.Cells.Add(altTC);

            descAltTC = new TableCell();
            descAltTC.ID = "desAltTC-" + id;
            descAltTXT = new TextBox();
            descAltTXT.ID = "desActTXT-" + id;
            descAltTC.Controls.Add(descAltTXT);
            tr.Cells.Add(descAltTC);

            altReasonTC = new TableCell();
            altReasonTC.ID = "altReasonTC-" + id;
            altReasonTXT = new TextBox();
            altReasonTXT.ID = "altReasonTXT-" + id;
            altReasonTC.Controls.Add(altReasonTXT);
            tr.Cells.Add(altReasonTC);

            delAltBtn = new Button();
            delAltBtn.ID = "delAltBtn-" + id;
            delAltBtn.Text = "X";
            delAltBtn.Click += delAltBtn_Click;
            delAltTC = new TableCell();
            delAltTC.ID = "delAltTC-" + id;
            delAltTC.Controls.Add(delAltBtn);
            tr.Cells.Add(delAltTC);

            altRatTC = new TableCell();
            altRatTC.ID = "altRatTC-" + id;
            altRatLbl = new Label();
            altRatLbl.ID = "altRatLbl-" + id;
            altRatTC.Controls.Add(altRatLbl);
            tr.Cells.Add(altRatTC);

            alternativesTable.Rows.Add(tr);
            us.AlternativesTRs.Add(tr);
        }

    }
}
