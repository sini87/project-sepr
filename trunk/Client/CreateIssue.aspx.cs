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
                    Server.Transfer("Default.aspx");
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
            delTagButton.Click += delTagButton_Click;
            tagsDDList.AutoPostBack = true;
            tagsDDList.Items.Add("");
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
                tc.Controls.Add(t);
            }
        }

        protected void addUser_Click(object sender, EventArgs e)
        {

        }

        protected void addStakeholders_Click(object sender, EventArgs e)
        {

        }

        protected void save_Click(object sender, EventArgs e)
        {
            AddIssue();
            Server.Transfer("MyIssues.aspx");
            rc.Issue = null;
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
            rc.EndPoint = "api/Issue/Create";
            rc.Method = HttpVerb.POST;
            rc.PostData = JsonConvert.SerializeObject(issue);
            string response = rc.MakeRequest();
            int issueID = JsonConvert.DeserializeObject<int>(response);
            rc.UploadFilesToRemoteUrl(issueID);
            return issueID;
        }
    }
}