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
    public partial class MyIssues : Page
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
        int hyperlinkid = 0;
        int rowID = 0;
        RestClient rc;
        ReviewHelper reviewHelper = new ReviewHelper();
        ReviewAPIHandler apiHandler = new ReviewAPIHandler();
        
        protected void Page_Preload(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);

            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                reviewHelper.UserIssues = apiHandler.GetOwnerIssuesOfUser(rc);
                Table dataTable = generateTable(reviewHelper.UserIssues);

                if (dataTable == null){
                    issuesOwnedText.Text = "No Issues existing";
                    issuesOwnedText.Visible = true;
                }else{
                    OwnedIssueTable.Controls.Add(dataTable);
                }

                btnNewIssue.Visible = true;
            }
        }

        protected Table generateTable(List<IssueModel> data)
        {
            if (data.Count > 0)
            {             

                foreach (IssueModel element in data)
                {
                    row = new TableRow();
                    row.ID = "tablerow" + rowID;
                    rowID++;
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
                    tbRating.Attributes.Add("readonly","readonly");
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

                    if (element.Status.ToUpper().Equals("FINISHED"))
                    {
                        LinkButton showLinkButton = reviewHelper.CreateShowLinkButton(hyperlinkid);
                        showLinkButton.CssClass = "review_link";
                        showLinkButton.Click += new EventHandler(OnShowReviews_Click);
                        tTitle.Controls.Add(showLinkButton);

                        LinkButton addLinkButton = reviewHelper.CreateAddLinkButton(hyperlinkid);
                        addLinkButton.CssClass = "review_link";
                        addLinkButton.Click += new EventHandler(OnAddReviews_Click);
                        tTitle.Controls.Add(addLinkButton);
                    }
                    Panel addReviewPanel = reviewHelper.CreateAddReviewPanel(element, hyperlinkid, this);
                    addReviewPanel.Visible = false;
                    tTitle.Controls.Add(addReviewPanel);
                    Panel showReviewPanel = reviewHelper.CreateShowReviewPanel(element, rc, hyperlinkid, this);
                    showReviewPanel.Visible = false;
                    tTitle.Controls.Add(showReviewPanel);

                    row.Cells.Add(tTitle);

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                        Request.ApplicationPath.TrimEnd('/') + "/";
                    pTitle.Attributes["onclick"] = "javascript:location.href='"+baseUrl+"IssueDetail?issueId=" + element.Id + "'";
                    

                    dataTable.Rows.Add(row);

                    row = new TableRow();
                    row.ID = "tablerow" + rowID;
                    rowID++;
                    row.CssClass = "row spacer";
                    tTitle = new TableCell();
                    row.Cells.Add(tTitle);
                    dataTable.Rows.Add(row);

                    dataTable.Rows.Add(row);
                    hyperlinkid++;
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

        public void OnSaveReviewButton_Click(object sender, EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                Button link = (Button)sender;
                int pressedbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
                ReviewModel newReview = new ReviewModel();
                reviewHelper.SetNewReviewModel(newReview, pressedbuttonid, rc);

                if (apiHandler.AddReview(rc, newReview) == "OK")
                {
                    Panel panelOK = reviewHelper.CreateReviewAddSuccessPanel();
                    reviewHelper.ReviewAddPanelList[pressedbuttonid].Controls.Add(panelOK);
                }
                else
                {
                    Panel panelNOK = reviewHelper.CreateReviewAddFailPanel();
                    reviewHelper.ReviewAddPanelList[pressedbuttonid].Controls.Add(panelNOK);
                }
            }
        }

        public void OnAddReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            reviewHelper.HandleLinkButtonClick(link, reviewHelper.ReviewAddPanelList, pressedlinkbuttonid, dataTable, "addReviewLink");
        }

        public void OnShowReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            reviewHelper.HandleLinkButtonClick(link, reviewHelper.ReviewShowPanelList, pressedlinkbuttonid, dataTable, "showReviewLink");
        }
    }
}