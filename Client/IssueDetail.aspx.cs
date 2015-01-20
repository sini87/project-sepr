using CDDSS_API;
using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
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

        protected void Page_Preload(object sender, EventArgs e)
        {
            //if (!this.User.Identity.IsAuthenticated)
            //{
            //    Server.Transfer("Default.aspx");
            //}


            if (Request["issueId"] == null || Request["issueId"].Length == 0)
            {
                Server.Transfer("IssueDetail.aspx?issueId=1");
                return;
            }
            if (!IsPostBack)
            {
                RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                SessionManager.AddUserSession(Session.SessionID);
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int issueID = int.Parse(Request["issueId"]);
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            IssueModel issue;

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
                    tagPanel.Controls.Add(tagButton);
                    us.IssueTags.Add(tagButton);
                }

                buildAccessRights(issue, us);
                buildStakeholders(issue, us);
                buildArtefacts(issue, us);
                buildFactors(issue, us);
                buildDocuments(issue, us);
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
                }
            }


            doPermissions(issue, rc);
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
            Label userLabel;
            DropDownList userRightDDL;
            Button delUserBtn;
            foreach (AccessRightModel ar in issue.AccessUserList)
            {
                tr = new TableRow();
                tr.ID = "usrTR" + ar.User.AccessObject;
                userLabel = new Label();
                userLabel.ID = "usrLBL" + ar.User.AccessObject;
                userLabel.Text = ar.User.FirstName + " " + ar.User.LastName;
                userTC = new TableCell();
                userTC.ID = "usrTC" + ar.User.AccessObject;
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

                descriptionText.Enabled = false;
                titleText.Enabled = false;
                save.Visible = false;
                addArtefact.Visible = false;
                addStakeholder.Visible = false;
                addFactor.Visible = false;
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
            IssueModel issue = new IssueModel();
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
            rc.EndPoint = "api/Issue/Edit";
            rc.Method = HttpVerb.POST;
            rc.PostData = JsonConvert.SerializeObject(issue);
            string res = rc.MakeRequest();
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
        }

    }
}
