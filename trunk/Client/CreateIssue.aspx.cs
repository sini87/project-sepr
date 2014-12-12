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
    public partial class CreateIssue : System.Web.UI.Page
    {
        DropDownList tagsList = new DropDownList();
        RestClient rc;
        static List<TagModel> tagList;
        List<InfluenceFactorModel> factorList;
        List<UserShort> userList = new List<UserShort>();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);
            if (rc == null)
            {
                RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                rc = RestClient.GetInstance(Session.SessionID);
            }
            if (rc.TagRows.Count > 0)
            {
                DropDownList ddlist;
                Button b;
                TextBox tagTextBox;
                foreach (TableRow tr in rc.TagRows)
                {
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ddlist = (DropDownList)tr.Cells[0].Controls[0];
                        ddlist.SelectedIndexChanged += tagsDDList_SelectedIndexChanged;
                    }
                    else if (tr.Cells[0].Controls[0].GetType() == typeof(TextBox))
                    {
                        tagTextBox = (TextBox)tr.Cells[0].Controls[0];
                        tagTextBox.TextChanged += tagTextBox_TextChanged;
                    }
                    b = (Button)tr.Cells[1].Controls[0];
                    b.Click += delTagButton_Click;
                }
            }

            if (rc.StakeholderRows.Count > 0)
            {
                LinkButton b;
                DropDownList ddl;
                int i = 0;
                foreach (TableRow tr in rc.StakeholderRows)
                {
                    if (rc.StakeholderRows[i].Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        ddl = (DropDownList)rc.StakeholderRows[i].Controls[0].Controls[0];
                        ddl.SelectedIndexChanged += stakeholderDDL_SelectedIndexChanged;
                    }

                    b = (LinkButton)tr.Cells[1].Controls[0];
                    b.Click += delStakeholderButton_Click;

                    i++;
                }
            }

            if (rc.UsersRows.Count > 0)
            {
                LinkButton b;
                int i = 0;
                foreach (TableRow tr in rc.UsersRows)
                {
                    b = (LinkButton)rc.UsersRows[i].Cells[2].Controls[0];
                    b.Click += delUserButton_Click;
                    i++;
                }
            }
        }

        void tagTextBox_TextChanged(object sender, EventArgs e)
        {
            TableRow tr = (TableRow)((TextBox)sender).Parent.Parent;
            Table t = (Table)tr.Parent;
            if (rc.Issue.Tags[t.Rows.GetRowIndex(tr) - 1] == null)
            {
                TagModel tag = new TagModel();
                tag.Name = ((TextBox)sender).Text;
                rc.Issue.Tags[t.Rows.GetRowIndex(tr) - 1] = tag;
            }
            else
            {
                rc.Issue.Tags[t.Rows.GetRowIndex(tr)].Name = ((TextBox)sender).Text;
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);

            if (!Page.IsPostBack)
            {
                if (this.User.Identity.IsAuthenticated && rc != null)
                {
                    rc.EndPoint = "api/Tags";
                    tagList = JsonConvert.DeserializeObject<List<TagModel>>(rc.MakeRequest());
                    tagsList.Items.Add(" ");
                    foreach (TagModel tag in tagList)
                    {
                        tagsList.Items.Add(tag.Name);
                    }
                    tagsList.Items.Add("New...");

                    
                }
                else
                {
                    //RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                    //rc = RestClient.GetInstance(Session.SessionID);
                    //rc.EndPoint = "api/Tags";
                    //tagList = JsonConvert.DeserializeObject<List<TagModel>>(rc.MakeRequest());
                    //tagsList.Items.Add(" ");
                    //foreach (TagModel tag in tagList)
                    //{
                    //    tagsList.Items.Add(tag.Name);
                    //}
                    //tagsList.Items.Add("New...");
                    Server.Transfer("MyIssues.aspx");
                }
            }
            else
            {
                if (rc.TagRows.Count > 0)
                {
                    tagsTable.Visible = true;
                    foreach (TableRow tr in rc.TagRows)
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

                if (rc.StakeholderRows.Count > 0)
                {
                    stakeholderTable.Visible = true;
                    foreach (TableRow tr in rc.StakeholderRows)
                    {
                        stakeholderTable.Rows.Add(tr);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RestClient rc = RestClient.GetInstance(Session.SessionID);
                rc.Issue = new IssueModel();
            }
        }

        protected void fillTagDropDown()
        {

            ListItem item = new ListItem();
            item.Value = "new";
            item.Text = "new";

            drpRelation.Items.Add(item);
        }

        protected void addTags_Click(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);
            TableRow tr = new TableRow();
            TableCell dropTC = new TableCell();
            DropDownList tagsDDList = new DropDownList();
            Button delTagButton = new Button();
            delTagButton.Text = "X";
            delTagButton.CssClass = "delete_button";
            delTagButton.Click += delTagButton_Click;
            tagsDDList.AutoPostBack = true;
            tagsDDList.Items.Add("");
            tagsDDList.CssClass = "dropdown_tag";
            foreach (TagModel tag in tagList)
            {
                tagsDDList.Items.Add(tag.Name);
            }
            tagsDDList.Items.Add("New...");
            tagsDDList.SelectedIndex = 0;
            TableCell delTC = new TableCell();
            delTC.Controls.Add(delTagButton);
            dropTC.Controls.Add(tagsDDList);
            tr.Cells.Add(dropTC);
            tr.Cells.Add(delTC);
            tagsTable.Rows.Add(tr);
            tagsTable.Visible = true;
            rc.TagRows.Add(tr);
            rc.Issue.Tags.Add(null);
        }

        void delTagButton_Click(object sender, EventArgs e)
        {
            Button dpList = (Button)sender;
            TableRow tr = (TableRow)dpList.Parent.Parent;
            Table t = (Table)tr.Parent;
            t.Rows.Remove(tr);
        }

        void tagsDDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dpList = (DropDownList)sender;
            TableRow tr = (TableRow)dpList.Parent.Parent;
            TableCell tc = tr.Cells[0];
            rc = RestClient.GetInstance(Session.SessionID);

            if (dpList.SelectedIndex > 0 && dpList.SelectedIndex < dpList.Items.Count - 1)
            {
                Table t = (Table)tr.Parent;
                rc.Issue.Tags[t.Rows.GetRowIndex(tr) - 1] = tagList[dpList.SelectedIndex - 1];
            }
            else if (dpList.SelectedIndex == dpList.Items.Count - 1)
            {
                tc.Controls.RemoveAt(0);
                TextBox t = new TextBox();
                t.CssClass = "maxwidth";
                tc.Controls.Add(t);
            }
        }

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

        void delUserButton_Click(object sender, EventArgs e)
        {
            int idx;
            TableRow tr = (TableRow)((TableCell)((LinkButton)sender).Parent).Parent;
            idx = usersTable.Rows.GetRowIndex(tr);
            usersTable.Rows.Remove(tr);
            rc.UsersRows.RemoveAt(idx);
        }

        protected void addStakeholders_Click(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);
            rc.EndPoint = "api/Stakeholders";
            List<StakeholderModel> skList = JsonConvert.DeserializeObject<List<StakeholderModel>>(rc.MakeRequest());

            TableRow tr = new TableRow();
            TableCell delTC = new TableCell();

            DropDownList stakeholderDDL = new DropDownList();
            stakeholderDDL.CssClass = "dropdown_tag";
            stakeholderDDL.Items.Add("");
            foreach(StakeholderModel stakeholder in skList){
                stakeholderDDL.Items.Add(stakeholder.Name);
            }
            stakeholderDDL.Items.Add("New...");
            stakeholderDDL.SelectedIndexChanged += stakeholderDDL_SelectedIndexChanged;
            stakeholderDDL.SelectedIndex = 0;
            TableCell stakeTC = new TableCell();
            stakeTC.Controls.Add(stakeholderDDL);

            LinkButton delStakeholderButton = new LinkButton();
            delStakeholderButton.Text = "X";
            delStakeholderButton.CssClass = "delete_button";
            delStakeholderButton.Click += delStakeholderButton_Click;
            delTC.Controls.Add(delStakeholderButton);

            tr.Cells.Add(stakeTC);
            tr.Cells.Add(delTC);

            stakeholderTable.Rows.Add(tr);
            stakeholderTable.Visible = true;

            rc.StakeholderRows.Add(tr);
        }

        void stakeholderDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex == ddl.Items.Count - 1)
            {
                TextBox txtBox = new TextBox();
                txtBox.Width = 150;
                int idx;
                TableRow tr = (TableRow)((TableCell)((DropDownList)sender).Parent).Parent;
                idx = stakeholderTable.Rows.GetRowIndex(tr);
                stakeholderTable.Rows[idx].Cells[0].Controls.RemoveAt(0);
                stakeholderTable.Rows[idx].Cells[0].Controls.Add(txtBox);
            }
        }

        void delStakeholderButton_Click(object sender, EventArgs e)
        {
            int idx;
            TableRow tr = (TableRow)((TableCell)((LinkButton)sender).Parent).Parent;
            idx = stakeholderTable.Rows.GetRowIndex(tr);
            stakeholderTable.Rows.Remove(tr);
            rc.StakeholderRows.RemoveAt(idx);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            AddIssue();
            Server.Transfer("MyIssues.aspx");
            rc.Issue = null;
            rc.TagRows = null;
            rc.UsersRows = new List<TableRow>();
            rc.StakeholderRows = new List<TableRow>();
        }

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

        protected void addDocument_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            UploadButton.Visible = true;
        }

        protected void addFactor_Click(object sender, EventArgs e)
        {
            TextBox t = new TextBox();
            t.ID = "factorTxt";
            t.Width = 200;
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.Controls.Add(t);
            tr.Cells.Add(tc);
            Table1.Rows.Add(tr);
            Table1.Visible = true;
        }

        protected void addArtefact_Click(object sender, EventArgs e)
        {

        }

        protected void addRelation_Click(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = new byte[FileUpload1.FileContent.Length];
                FileUpload1.FileContent.Read(bytes, 0, (int)FileUpload1.FileContent.Length);
                ms.Write(bytes, 0, (int)FileUpload1.FileContent.Length);
                rc.AddFile(FileUpload1.FileName, ms);
            }
        }

        private int AddIssue()
        {
            IssueModel issue = rc.Issue;
            issue.Title = title.Text;
            issue.Description = description.Text;

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

            rc = RestClient.GetInstance(Session.SessionID);
            rc.EndPoint = "api/Stakeholders";
            List<StakeholderModel> skList = JsonConvert.DeserializeObject<List<StakeholderModel>>(rc.MakeRequest());
            DropDownList stakeholderDDL;
            TextBox stakeholderTxt;
            foreach (TableRow tr in rc.StakeholderRows)
            {
                if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                {
                    stakeholderDDL = (DropDownList)tr.Cells[0].Controls[0];
                    if (stakeholderDDL.SelectedIndex < stakeholderDDL.Items.Count - 1 && stakeholderDDL.SelectedIndex > 0)
                    {
                        issue.Stakeholders.Add(skList[stakeholderDDL.SelectedIndex - 1]);
                    }
                }
                else
                {
                    stakeholderTxt = (TextBox)tr.Cells[0].Controls[0];
                    issue.Stakeholders.Add(new StakeholderModel(stakeholderTxt.Text));
                }
            }

            rc.EndPoint = "api/Issue/Create";
            rc.Method = HttpVerb.POST;
            rc.PostData = JsonConvert.SerializeObject(issue);
            string response = rc.MakeRequest();
            int issueID = JsonConvert.DeserializeObject<int>(response);
            rc.UploadFilesToRemoteUrl(issueID);

            return issueID;
        }

        private void RetrieveUsers()
        {
            rc.EndPoint = "api/User";
            userList = JsonConvert.DeserializeObject<List<UserShort>>(rc.MakeRequest());
        }
    }
}