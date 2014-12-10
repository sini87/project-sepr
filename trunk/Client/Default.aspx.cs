using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                rc.EndPoint = "api/Issue/OfUser";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<IssueModel> user = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                Table dataTable = generateTable(user);

                if (dataTable == null){
                    issuesOwnedText.Text = "No Issues existing";
                    issuesOwnedText.Visible = true;
                }else{
                    OwnedIssueTable.Controls.Add(dataTable);
                }

                rc.EndPoint = "api/Issue";
                rc.Method = HttpVerb.GET;
                json = rc.MakeRequest();
                List<IssueModel> allIssues = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                dataTable = generateTable(allIssues);

                if (dataTable == null)
                {
                    involvedIssuesText.Text = "No Issues existing";
                    involvedIssuesText.Visible = true;
                }
                else
                {
                    InvolvedIssueTable.Controls.Add(dataTable);
                }

                headingIssuesOwned.Visible = true;
                headingInvolvedIssues.Visible = true;
                btnNewIssue.Visible = true;
            }
        }

        protected Table generateTable(List<IssueModel> data)
        {
            if (data.Count > 0)
            {
                Table dataTable = new Table();
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell headerCellTitle = new TableHeaderCell();
                headerCellTitle.Text = "Title";
                TableHeaderCell headerCellTags = new TableHeaderCell();
                headerCellTags.Text = "Tags";
                TableHeaderCell headerCellRating = new TableHeaderCell();
                headerCellRating.Text = "Rating";
                TableHeaderCell headerCellStatus = new TableHeaderCell();
                headerCellStatus.Text = "Status";
                TableHeaderCell headerCellDetail = new TableHeaderCell();
                headerCellDetail.Text = "Details";
                
                headerRow.Cells.Add(headerCellTitle);
                headerRow.Cells.Add(headerCellTags);
                headerRow.Cells.Add(headerCellRating);
                headerRow.Cells.Add(headerCellStatus);
                headerRow.Cells.Add(headerCellDetail);

                dataTable.Rows.Add(headerRow);

                TableRow row;
                TableCell tTitle;
                TableCell tTags;
                TableCell tRating;
                TableCell tStatus;
                TableCell tDetail;

                foreach (IssueModel element in data)
                {
                    row = new TableRow();

                    tTitle = new TableCell();
                    tTitle.Text = element.Title;
                    row.Cells.Add(tTitle);

                    tTags = new TableCell();
                    foreach (TagModel tagElement in element.Tags)
                    {
                        tTags.Text += tagElement.Name + ";";
                    }

                    if (tTags.Text != "" && tTags.Text != null)
                        tTags.Text = tTags.Text.Substring(0, tTags.Text.Length - 1);

                    row.Cells.Add(tTags);

                    tRating = new TableCell();
                    tRating.Text = "" + element.ReviewRating;
                    row.Cells.Add(tRating);

                    tStatus = new TableCell();
                    tStatus.Text = element.Status;
                    row.Cells.Add(tStatus);

                    tDetail = new TableCell();
                    HyperLink lnk = new HyperLink();
                    lnk.Text = "Details";
                    lnk.NavigateUrl = "~/IssueDetail?issueId=" + element.Id;
                    tDetail.Controls.Add(lnk);

                    row.Cells.Add(tDetail);
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

        protected void brnNewIssue_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateIssue.aspx");
        }
    }
}