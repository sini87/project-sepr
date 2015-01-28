using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    /// <summary>
    /// code behind for CreateIssue.aspx
    /// </summary>
    public partial class CreateIssue : System.Web.UI.Page
    {
        RestClient rc;
        List<UserShort> userList = new List<UserShort>();
        List<TagModel> tagsList = new List<TagModel>();
        List<StakeholderModel> stakeholdersList = new List<StakeholderModel>();
        List<ArtefactModel> artefactsList = new List<ArtefactModel>();

        /// <summary>
        /// page PreInit event
        /// registers events from dynamically created controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
                return;
            }
            rc = RestClient.GetInstance(Session.SessionID);
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            
            if (!Page.IsPostBack)
            {
                session.CreateIssueEntered();
            }
            if (rc == null)
            {
                RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                rc = RestClient.GetInstance(Session.SessionID);
            }

            DropDownList ddlist;
            LinkButton b;
            if (session.TagsTRs.Count > 0)
            {
                foreach (TableRow tr in session.TagsTRs)
                {
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ddlist = (DropDownList)tr.Cells[0].Controls[0];
                        ddlist.SelectedIndexChanged += tagsDDList_SelectedIndexChanged;
                    }
                    b = (LinkButton)tr.Cells[1].Controls[0];
                    b.Click += delTagButton_Click;
                }
            }

            if (session.StakeholdersTRs.Count > 0)
            {
                foreach (TableRow tr in session.StakeholdersTRs)
                {
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ddlist = (DropDownList)tr.Cells[0].Controls[0];
                        ddlist.SelectedIndexChanged += stakeholdersDDL_SelectedIndexChanged;
                    }
                    b = (LinkButton)tr.Cells[1].Controls[0];
                    b.Click += delStakeholderButton_Click;
                }
            }

            if (session.FactorTRs.Count > 0)
            {
                foreach (TableRow tr in session.FactorTRs)
                {
                    b = (LinkButton)tr.Cells[3].Controls[0];
                    b.Click += delFactorBtn_Click;
                }
            }

            foreach (TableRow tr in session.ArtefactsTRs)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    ddlist = (DropDownList)tr.Cells[0].Controls[0];
                    ddlist.SelectedIndexChanged += artefactsDDL_SelectedIndexChanged;
                }
                b = (LinkButton)tr.Cells[1].Controls[0];
                b.Click += delArtefactButton_Click;
            }

            if (rc.UsersRows.Count > 0)
            {
                int i = 0;
                foreach (TableRow tr in rc.UsersRows)
                {
                    b = (LinkButton)rc.UsersRows[i].Cells[2].Controls[0];
                    b.Click += delUserButton_Click;
                    i++;
                }
            }

            foreach (TableRow tr in session.DocumentsTRs)
            {
                b = (LinkButton)tr.Cells[1].Controls[0];
                b.Click += delFileBtn_Click;
            }

            if (UploadButton != null && UploadButton.Visible)
            {
                UploadButton.Click += UploadButton_Click;
            }            
        }

        /// <summary>
        /// page init events
        /// creates gui events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);
            UserSession session = SessionManager.GetUserSession(Session.SessionID);

            if (!Page.IsPostBack)
            {
                session.TagsTRs.Clear();
                if (!this.User.Identity.IsAuthenticated)
                {
                    RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                    rc = RestClient.GetInstance(Session.SessionID);
                    //Server.Transfer("MyIssues.aspx");
                }

                List<IssueModel> issueList = RetrieveIssues();
                relationIssuesDDL.Items.Add(new ListItem("", "0"));
                foreach (IssueModel iss in issueList)
                {
                    relationIssuesDDL.Items.Add(new ListItem(iss.Title, iss.Id.ToString()));
                }
                relationIssuesDDL.SelectedIndex = 0;
            }
            else
            {
                if (session.TagsTRs.Count > 0)
                {
                    tagsTable.Visible = true;
                    foreach (TableRow tr in session.TagsTRs)
                    {
                        tagsTable.Rows.Add(tr);
                    }
                }
                if (rc.UsersRows.Count > 0)
                {
                    usersTable.Visible = true;
                    foreach (TableRow tr in rc.UsersRows)
                    {
                        usersTable.Rows.Add(tr);
                    }
                }

                if (session.StakeholdersTRs.Count > 0)
                {
                    stakeholdersTable.Visible = true;
                    foreach (TableRow tr in session.StakeholdersTRs)
                    {
                        stakeholdersTable.Rows.Add(tr);
                    }
                }

                if (session.ArtefactsTRs.Count > 0)
                {
                    artefactTable.Visible = true;
                    foreach (TableRow tr in session.ArtefactsTRs)
                    {
                        artefactTable.Rows.Add(tr);
                    }
                }

                if (session.FactorTRs.Count > 0)
                {
                    factorsTable.Visible = true;
                    foreach (TableRow tr in session.FactorTRs)
                    {
                        factorsTable.Rows.Add(tr);
                    }
                }

                if (session.DocumentsTRs.Count > 0)
                {
                    documentTable.Visible = true;
                    foreach (TableRow tr in session.DocumentsTRs)
                    {
                        documentTable.Rows.Add(tr);
                    }
                }
            }
        }

        /// <summary>
        /// page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RestClient rc = RestClient.GetInstance(Session.SessionID);
                rc.Issue = new IssueModel();
            }
        }

        /// <summary>
        /// add tag event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addTags_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            TableRow tr = new TableRow();
            TableCell tagTC = new TableCell();
            TableCell buttonTC = new TableCell();

            if (tagsList == null || tagsList.Count == 0) RetrieveTags();

            DropDownList tagsDDL = new DropDownList();
            tagsDDL.Items.Add(new ListItem("","-1"));
            tagsDDL.SelectedIndexChanged += tagsDDList_SelectedIndexChanged;
            tagsDDL.CssClass = "dropdown_tag";
            tagsDDL.AutoPostBack = true;
            tagsDDL.ID = "tagDDL" + session.NextTTRID;
            foreach (TagModel tag in tagsList)
            {
                tagsDDL.Items.Add(new ListItem(tag.Name,tag.Id.ToString()));
            }
            tagsDDL.Items.Add(new ListItem("New...","-2"));
            tagTC.Controls.Add(tagsDDL);

            LinkButton delTagButton = new LinkButton();
            delTagButton.Text = "X";
            delTagButton.ID = tagsDDL.ID.Replace("tagDDL", "tagBTN");
            delTagButton.CssClass = "delete_button";
            delTagButton.Click += delTagButton_Click;
            buttonTC.Controls.Add(delTagButton);

            tr.Cells.Add(tagTC);
            tr.Cells.Add(buttonTC);
            
            tagsTable.Rows.Add(tr);
            tagsTable.Visible = true;
            
            session.TagsTRs.Add(tr);
        }

        /// <summary>
        /// del tag event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delTagButton_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            LinkButton delBtn = (LinkButton)sender;
            TableRow tr = (TableRow)delBtn.Parent.Parent;
            tagsTable.Rows.Remove(tr);
            session.TagsTRs.Clear();
            foreach (TableRow trow in tagsTable.Rows)
            {
                session.TagsTRs.Add(trow);
            }
            
            if (tagsTable.Rows.Count == 0)
            {
                tagsTable.Visible = false;
            }
        }

        /// <summary>
        /// tags drop down listbox index changed event
        /// adds a existing tag to issue or creates a textbox for a new tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tagsDDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            DropDownList dpList = (DropDownList)sender;
            if (dpList.Parent == null) return;
            TableRow tr = (TableRow)dpList.Parent.Parent;
            TableCell tc = tr.Cells[0];

            if (dpList.SelectedIndex == dpList.Items.Count - 1)
            {
                TextBox t = new TextBox();
                t.CssClass = "maxwidth";
                tc.Controls.RemoveAt(0);
                t.ID = dpList.ID.Replace("tagDDL", "tagTXT");
                tc.Controls.Add(t);
                session.TagsTRs.Clear();
                foreach (TableRow trow in tagsTable.Rows)
                {
                    session.TagsTRs.Add(trow);
                }
            }
        }

        /// <summary>
        /// add user to issue event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addUser_Click(object sender, EventArgs e)
        {
            if (userList.Count == 0)
            {
                RetrieveUsers();
            }

            rc = RestClient.GetInstance(Session.SessionID);
            TableRow tr = new TableRow();
            TableCell buttonTC = new TableCell();

            DropDownList userDDL = new DropDownList();
            TableCell userTC = new TableCell();
            userDDL.Items.Add("");
            userDDL.CssClass = "dropdown_tag";
            foreach (UserShort us in userList)
            {
                userDDL.Items.Add(us.FirstName + " " + us.LastName);
            }
            userTC.Controls.Add(userDDL);

            DropDownList rightDDL = new DropDownList();
            rightDDL.CssClass = "dropdown_tag";
            rightDDL.Items.Add("Contributor");
            rightDDL.Items.Add("Viewer");
            rightDDL.SelectedIndex = 0;
            TableCell rightTC = new TableCell();
            rightTC.Controls.Add(rightDDL);

            LinkButton delUserButton = new LinkButton();
            delUserButton.Text = "X";
            delUserButton.CssClass = "delete_button";
            delUserButton.Click += delUserButton_Click;
            buttonTC.Controls.Add(delUserButton);

            tr.Cells.Add(userTC);
            tr.Cells.Add(rightTC);
            tr.Cells.Add(buttonTC);

            usersTable.Rows.Add(tr);
            usersTable.Visible = true;

            rc.UsersRows.Add(tr);
            rc.Issue.AccessUserList.Add(null);
        }

        /// <summary>
        /// delete user event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delUserButton_Click(object sender, EventArgs e)
        {
            int idx;
            TableRow tr = (TableRow)((TableCell)((LinkButton)sender).Parent).Parent;
            idx = usersTable.Rows.GetRowIndex(tr);
            usersTable.Rows.Remove(tr);
            rc.UsersRows.RemoveAt(idx);
            if (usersTable.Rows.Count == 0)
            {
                usersTable.Visible = false;
            }
        }

        /// <summary>
        /// add stakeholder to issue event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addStakeholders_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            TableRow tr = new TableRow();
            TableCell stakeholderTC = new TableCell();
            TableCell buttonTC = new TableCell();

            if (stakeholdersList == null || stakeholdersList.Count == 0) RetrieveStakeholders();

            DropDownList stakeholderDDL = new DropDownList();
            stakeholderDDL.Items.Add(new ListItem("", "-1"));
            stakeholderDDL.SelectedIndexChanged += stakeholdersDDL_SelectedIndexChanged;
            stakeholderDDL.CssClass = "dropdown_tag";
            stakeholderDDL.AutoPostBack = true;
            stakeholderDDL.ID = "stakeholderDDL" + session.NextSTRID;
            foreach (StakeholderModel stakeholder in stakeholdersList)
            {
                stakeholderDDL.Items.Add(new ListItem(stakeholder.Name,stakeholder.Id.ToString()));
            }
            stakeholderDDL.Items.Add(new ListItem("New...", "-2"));
            stakeholderTC.Controls.Add(stakeholderDDL);

            LinkButton delStakeholderButton = new LinkButton();
            delStakeholderButton.Text = "X";
            delStakeholderButton.ID = stakeholderDDL.ID.Replace("stakeholderDDL", "stakeholderBTN");
            delStakeholderButton.CssClass = "delete_button";
            delStakeholderButton.Click += delStakeholderButton_Click;
            buttonTC.Controls.Add(delStakeholderButton);

            tr.Cells.Add(stakeholderTC);
            tr.Cells.Add(buttonTC);

            stakeholdersTable.Rows.Add(tr);
            stakeholdersTable.Visible = true;

            session.StakeholdersTRs.Add(tr);            
        }

        /// <summary>
        /// stakeholder drop down listbox index changed event
        /// adds existing stakeholder to issue or creates textbox for new stakeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void stakeholdersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            DropDownList dpList = (DropDownList)sender;
            if (dpList.Parent == null) return;
            TableRow tr = (TableRow)dpList.Parent.Parent;
            TableCell tc = tr.Cells[0];

            if (dpList.SelectedIndex == dpList.Items.Count - 1)
            {
                TextBox t = new TextBox();
                t.CssClass = "maxwidth";
                tc.Controls.RemoveAt(0);
                t.ID = dpList.ID.Replace("DDL", "TXT");
                tc.Controls.Add(t);
                session.StakeholdersTRs.Clear();
                foreach (TableRow trow in stakeholdersTable.Rows)
                {
                    session.StakeholdersTRs.Add(trow);
                }
            }
        }

        /// <summary>
        /// delete stakeholder event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delStakeholderButton_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            LinkButton delBtn = (LinkButton)sender;
            TableRow tr = (TableRow)delBtn.Parent.Parent;
            stakeholdersTable.Rows.Remove(tr);
            session.StakeholdersTRs.Clear();
            foreach (TableRow trow in stakeholdersTable.Rows)
            {
                session.StakeholdersTRs.Add(trow);
            }
            if (stakeholdersTable.Rows.Count == 0)
            {
                stakeholdersTable.Visible = false;
            }
        }

        /// <summary>
        /// add influence factor event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addFactor_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            string factID = session.NextFTRID;
            
            TextBox factorNameTxt = new TextBox();
            factorNameTxt.ID = "factNameTXT" + factID;
            factorNameTxt.Width = 100;
            TableCell nameTC = new TableCell();
            nameTC.Controls.Add(factorNameTxt);

            TextBox factorCharacteristicsTxt = new TextBox();
            factorCharacteristicsTxt.ID = "factCharTXT" + factID;
            factorCharacteristicsTxt.Width = 130;
            TableCell characteristicsTC = new TableCell();
            characteristicsTC.Controls.Add(factorCharacteristicsTxt);

            CheckBox numericChck = new CheckBox();
            numericChck.ID = "factNumCHK" + factID;
            numericChck.Width = 50;
            TableCell numericTC = new TableCell();
            numericTC.Controls.Add(numericChck);

            LinkButton delFactorBtn = new LinkButton();
            delFactorBtn.ID = "delFactBTN" + factID;
            delFactorBtn.Text = "X";
            delFactorBtn.CssClass = "delete_button";
            delFactorBtn.Click += delFactorBtn_Click;
            TableCell delTC = new TableCell();
            delTC.Controls.Add(delFactorBtn);

            TableRow tr = new TableRow();
            tr.Cells.Add(nameTC);
            tr.Cells.Add(characteristicsTC);
            tr.Cells.Add(numericTC);
            tr.Cells.Add(delTC);

            factorsTable.Rows.Add(tr);
            factorsTable.Visible = true;

            session.FactorTRs.Add(tr);
        }

        /// <summary>
        /// delete influence factor event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delFactorBtn_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            LinkButton delBtn = (LinkButton)sender;
            TableRow tr = (TableRow)delBtn.Parent.Parent;
            factorsTable.Rows.Remove(tr);
            session.FactorTRs.Clear();
            foreach (TableRow trow in factorsTable.Rows)
            {
                if (trow.Cells[0].Controls.Count > 0) 
                    session.FactorTRs.Add(trow);
            }
            if (factorsTable.Rows.Count == 0)
            {
                factorsTable.Visible = false;
            }
        }

        /// <summary>
        /// add artefact event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addArtefact_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            string artefactID = session.NextATRID;
            TableRow tr = new TableRow();
            TableCell artefactTC = new TableCell();
            TableCell buttonTC = new TableCell();

            if (artefactsList == null || artefactsList.Count == 0) RetrieveArtefacts();

            DropDownList artefactsDDL = new DropDownList();
            artefactsDDL.Items.Add(new ListItem("", "-1"));
            artefactsDDL.SelectedIndexChanged += artefactsDDL_SelectedIndexChanged;
            artefactsDDL.CssClass = "dropdown_tag";
            artefactsDDL.AutoPostBack = true;
            artefactsDDL.ID = "artefactDDL" + artefactID;
            foreach (ArtefactModel artefact in artefactsList)
            {
                artefactsDDL.Items.Add(new ListItem(artefact.Name, artefact.Id.ToString()));
            }
            artefactsDDL.Items.Add(new ListItem("New...", "-2"));
            artefactTC.Controls.Add(artefactsDDL);

            LinkButton delArtefactButton = new LinkButton();
            delArtefactButton.Text = "X";
            delArtefactButton.ID = "artefactBTN" + artefactID;
            delArtefactButton.CssClass = "delete_button";
            delArtefactButton.Click += delArtefactButton_Click;
            buttonTC.Controls.Add(delArtefactButton);

            tr.Cells.Add(artefactTC);
            tr.Cells.Add(buttonTC);

            artefactTable.Rows.Add(tr);
            artefactTable.Visible = true;

            session.ArtefactsTRs.Add(tr);            
        }

        /// <summary>
        /// delete artefact event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delArtefactButton_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            LinkButton delBtn = (LinkButton)sender;
            TableRow tr = (TableRow)delBtn.Parent.Parent;
            artefactTable.Rows.Remove(tr);
            session.ArtefactsTRs.Clear();
            foreach (TableRow trow in artefactTable.Rows)
            {
                session.ArtefactsTRs.Add(trow);
            }
            if (artefactTable.Rows.Count == 0)
            {
                artefactTable.Visible = false;
            }
        }

        /// <summary>
        /// artefacts drop down listbox index change event
        /// adds an existing artefact to issue or creates textbox for new artefact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void artefactsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            DropDownList dpList = (DropDownList)sender;
            if (dpList.Parent == null) return;
            TableRow tr = (TableRow)dpList.Parent.Parent;
            TableCell tc = tr.Cells[0];

            if (dpList.SelectedIndex == dpList.Items.Count - 1)
            {
                TextBox t = new TextBox();
                t.CssClass = "maxwidth";
                tc.Controls.RemoveAt(0);
                t.ID = dpList.ID.Replace("DDL", "TXT");
                tc.Controls.Add(t);
                session.ArtefactsTRs.Clear();
                foreach (TableRow trow in artefactTable.Rows)
                {
                    session.ArtefactsTRs.Add(trow);
                }
            }
        }
        
        /// <summary>
        /// save button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void save_Click(object sender, EventArgs e)
        {
            AddIssue();
            Server.Transfer("MyIssues.aspx");
            rc.Issue = null;
            rc.TagRows = null;
            rc.UsersRows = new List<TableRow>();
            rc.StakeholderRows = new List<TableRow>();
        }

        /// <summary>
        /// save publish button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void savePublish_Click(object sender, EventArgs e)
        {
            int issueId = AddIssue();
            rc.EndPoint = "api/Issue/" + issueId + "/nextStage";
            rc.Method = HttpVerb.POST;
            rc.MakeRequest();
            Server.Transfer("MyIssues.aspx");
            rc.Issue = null;
            rc.TagRows = null;
            rc.UsersRows = new List<TableRow>();
            rc.StakeholderRows = new List<TableRow>();
        }

        /// <summary>
        /// add document event
        /// enables the relevant controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addDocument_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            UploadButton.Visible = true;
        }

        /// <summary>
        /// file upload clicked event
        /// loads file to RestClient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = new byte[FileUpload1.FileContent.Length];
                FileUpload1.FileContent.Read(bytes, 0, (int)FileUpload1.FileContent.Length);
                ms.Write(bytes, 0, (int)FileUpload1.FileContent.Length);
                rc.AddFile(FileUpload1.FileName, ms);
            }

            FileUpload1.Visible = false;
            UploadButton.Visible = false;

            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            Label fileNameLbl = new Label();
            fileNameLbl.Text = FileUpload1.FileName;
            TableCell fileTC = new TableCell();
            fileTC.Controls.Add(fileNameLbl);

            LinkButton delFileBtn = new LinkButton();
            delFileBtn.Text = "X";
            delFileBtn.CssClass = "delete_button";
            delFileBtn.Click += delFileBtn_Click;
            TableCell delTC = new TableCell();
            delTC.Controls.Add(delFileBtn);

            TableRow tr = new TableRow();
            tr.Cells.Add(fileTC);
            tr.Cells.Add(delTC);
            
            documentTable.Rows.Add(tr);
            documentTable.Visible = true;

            session.DocumentsTRs.Add(tr);
        }

        /// <summary>
        /// delete file click event
        /// removes uploaded file from RestClient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void delFileBtn_Click(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Session.SessionID);
            if (rc == null)
            {
                rc = RestClient.GetInstance(Session.SessionID);
            }
            LinkButton delBtn = (LinkButton)sender;
            TableRow tr = (TableRow)delBtn.Parent.Parent;
            Label fileLbl = (Label)tr.Cells[0].Controls[0];
            rc.RemoveFile(fileLbl.Text);
            documentTable.Rows.Remove(tr);
            session.DocumentsTRs.Clear();
            foreach (TableRow trow in documentTable.Rows)
            {
                session.DocumentsTRs.Add(trow);
            }
            if (documentTable.Rows.Count == 0)
            {
                documentTable.Visible = false;
            }
        }

        /// <summary>
        /// adds the issue to API
        /// </summary>
        /// <returns></returns>
        private int AddIssue()
        {
            IssueModel issue = new IssueModel();
            issue.Title = title.Text;
            issue.Description = description.Text;

            DropDownList ddl;
            TextBox txt;
            foreach (TableRow tr in tagsTable.Rows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    ddl = (DropDownList)tr.Cells[0].Controls[0];
                    if (Convert.ToInt32(ddl.SelectedValue) > 0)
                    {
                        issue.Tags.Add(new TagModel(Convert.ToInt32(ddl.SelectedValue), ""));
                    }
                }
                else
                {
                    txt = (TextBox)tr.Cells[0].Controls[0];
                    issue.Tags.Add(new TagModel(txt.Text));
                }
            }
            
            
            if (userList.Count == 0) RetrieveUsers();

            DropDownList userDDL, rightDDL;
            foreach (TableRow tr in rc.UsersRows)
            {
                userDDL = (DropDownList)tr.Cells[0].Controls[0];
                rightDDL = (DropDownList)tr.Cells[1].Controls[0];

                if (userDDL.SelectedIndex > 0 && !issue.AccessRights.ContainsKey((userList[userDDL.SelectedIndex - 1].AccessObject)))
                {
                    issue.AccessRights.Add(userList[userDDL.SelectedIndex - 1].AccessObject, rightDDL.SelectedValue.ToCharArray()[0]);
                }
            }

            foreach (TableRow tr in stakeholdersTable.Rows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    ddl = (DropDownList)tr.Cells[0].Controls[0];
                    if (Convert.ToInt32(ddl.SelectedValue) > 0)
                    {
                        issue.Stakeholders.Add(new StakeholderModel(Convert.ToInt32(ddl.SelectedValue), ""));
                    }
                }
                else
                {
                    txt = (TextBox)tr.Cells[0].Controls[0];
                    issue.Stakeholders.Add(new StakeholderModel(txt.Text));
                }
            }

            double ch;
            foreach (TableRow tr in factorsTable.Rows)
            {
                if (tr.Cells[0].Controls.Count > 0)
                {
                    InfluenceFactorModel ifm = new InfluenceFactorModel();
                    if (((CheckBox)tr.Cells[2].Controls[0]).Enabled && double.TryParse(((TextBox)tr.Cells[1].Controls[0]).Text, out ch))
                    {
                        ifm.Type = true;
                    }
                    else
                    {
                        ifm.Type = false;
                    }

                    ifm.Name = ((TextBox)tr.Cells[0].Controls[0]).Text;
                    ifm.Characteristic = ((TextBox)tr.Cells[1].Controls[0]).Text;
                    issue.InfluenceFactors.Add(ifm);
                }
            }

            foreach (TableRow tr in artefactTable.Rows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    ddl = (DropDownList)tr.Cells[0].Controls[0];
                    if (Convert.ToInt32(ddl.SelectedValue) > 0)
                    {
                        issue.Artefacts.Add(new ArtefactModel(Convert.ToInt32(ddl.SelectedValue), ""));
                    }
                }
                else
                {
                    txt = (TextBox)tr.Cells[0].Controls[0];
                    issue.Artefacts.Add(new ArtefactModel(txt.Text));
                }
            }

            if (relationIssuesDDL.SelectedValue != null && relationIssuesDDL.SelectedIndex > 0 && relationTypeDDL.SelectedIndex > 0)
            {
                issue.RelatedTo = Convert.ToInt32(relationIssuesDDL.SelectedValue);
                issue.RelationType = Convert.ToChar(relationTypeDDL.SelectedValue);
            }
            else
            {
                issue.RelatedTo = 0;
            }

            rc.EndPoint = "api/Issue/Create";
            rc.Method = HttpVerb.POST;
            rc.PostData = JsonConvert.SerializeObject(issue);
            string response = rc.MakeRequest();
            int issueID = JsonConvert.DeserializeObject<int>(response);
            rc.UploadFilesToRemoteUrl(issueID);

            return issueID;
        }

        /// <summary>
        /// retrieves users from API
        /// </summary>
        private void RetrieveUsers()
        {
            rc.EndPoint = "api/User";
            userList = JsonConvert.DeserializeObject<List<UserShort>>(rc.MakeRequest());
        }

        /// <summary>
        /// retrieves tags from API
        /// </summary>
        private void RetrieveTags()
        {
            rc.EndPoint = "api/Tags";
            tagsList = JsonConvert.DeserializeObject<List<TagModel>>(rc.MakeRequest());
        }

        /// <summary>
        /// retrieves stakeholders form API
        /// </summary>
        private void RetrieveStakeholders()
        {
            rc.EndPoint = "api/Stakeholders";
            stakeholdersList = JsonConvert.DeserializeObject<List<StakeholderModel>>(rc.MakeRequest());
        }

        /// <summary>
        /// retrieves all artefacts from api
        /// </summary>
        private void RetrieveArtefacts()
        {
            rc.EndPoint = "api/Artefacts";
            artefactsList = JsonConvert.DeserializeObject<List<ArtefactModel>>(rc.MakeRequest());
        }

        /// <summary>
        /// retrieves all issues from API
        /// </summary>
        /// <returns></returns>
        private List<IssueModel> RetrieveIssues()
        {
            if (rc == null)
            {
                rc = RestClient.GetInstance(Session.SessionID);
            }
            rc.EndPoint = "api/Issue";
            return JsonConvert.DeserializeObject<List<IssueModel>>(rc.MakeRequest());
        }
    }
}