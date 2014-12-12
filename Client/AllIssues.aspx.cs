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
    public partial class AllIssues : System.Web.UI.Page
    {
        protected void Page_Preload(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                rc.EndPoint = "api/Issue/";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<IssueModel> allIssues = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                Table dataTable = generateTable(allIssues);

                if (dataTable == null)
                {
                    InvolvedIssuesText.Text = "No Issues existing";
                    InvolvedIssuesText.Visible = true;
                }
                else
                {
                    InvolvedIssueTable.Controls.Add(dataTable);
                }
                btnNewIssue.Visible = true;
            }
        }

        protected Table generateTable(List<IssueModel> data)
        {
            if (data.Count > 0)
            {
                Table dataTable = new Table();

                TableRow row;
                TableCell tTitle;

                Panel pTitle;
                Panel pTags;
                Panel pTag;
                Panel pRating;
                Panel pStatus, pStatusParent;
                Panel pDetail;

                foreach (IssueModel element in data)
                {
                    row = new TableRow();
                    row.CssClass = "row table_row";

                    tTitle = new TableCell();

                    pTitle = new Panel();
                    pTitle.CssClass = "table_title col-lg-12";
                    pTitle.Controls.Add(new LiteralControl(element.Title));
                    tTitle.Controls.Add(pTitle);

                    pTags = new Panel();
                    pTags.CssClass = "table_tags col-lg-12";
                    foreach (TagModel tagElement in element.Tags)
                    {
                        pTag = new Panel();
                        pTag.CssClass = "table_tag";
                        pTag.Controls.Add(new LiteralControl(tagElement.Name));
                        pTags.Controls.Add(pTag);
                    }
                    tTitle.Controls.Add(pTags);

                    pRating = new Panel();
                    pRating.CssClass = "table_rating col-lg-12";
                    TextBox tbRating = new TextBox();
                    tbRating.CssClass = "rating rating5";
                    tbRating.Attributes.Add("readonly", "readonly");
                    tbRating.Text = element.ReviewRating.ToString();
                    pRating.Controls.Add(tbRating);
                    tTitle.Controls.Add(pRating);

                    pStatusParent = new Panel();
                    pStatusParent.CssClass = "col-lg-12 table_status";
                    pStatus = new Panel();
                    pStatus.Controls.Add(new LiteralControl(element.Status));
                    if (element != null)
                    {
                        if (element.Status.ToUpper().Equals("CREATING"))
                        {
                            pStatus.CssClass = "status_creating";
                        }
                        else if (element.Status.ToUpper().Equals("BRAINSTORMING"))
                        {
                            pStatus.CssClass = "status_brainstorming";
                        }
                        else if (element.Status.ToUpper().Equals("FINISHED"))
                        {
                            pStatus.CssClass = "status_finished";
                        }
                        else
                        {
                            pStatus.CssClass = "status_reviewed";
                        }
                    }
                    pStatusParent.Controls.Add(pStatus);
                    tTitle.Controls.Add(pStatusParent);

                    row.Cells.Add(tTitle);

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                        Request.ApplicationPath.TrimEnd('/') + "/";
                    row.Attributes["onclick"] = "javascript:location.href='" + baseUrl + "IssueDetail?issueId=" + element.Id + "'";

                    dataTable.Rows.Add(row);

                    row = new TableRow();
                    row.CssClass = "row spacer";
                    tTitle = new TableCell();
                    row.Cells.Add(tTitle);
                    dataTable.Rows.Add(row);
                }
                dataTable.Width = Unit.Percentage(100);
                return dataTable;
            }
            return null;
        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = true;
        }

        protected void btnNewIssue_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateIssue.aspx");
        }
    }
}