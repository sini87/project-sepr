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
        int hyperlinkid = 0;
        int rowID = 0;
        RestClient rc;
        ReviewAPIHandler apiHandler = new ReviewAPIHandler();
        ReviewHelper reviewHelper = new ReviewHelper();
        Table dataTable = new Table();
        TableRow row;
        TableCell tTitle;
        Panel pTitle;
        Panel pTags;
        Panel pTag;
        Panel pRating;
        Panel pStatus, pStatusParent;
        Panel pDetail;
        

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
                reviewHelper.AllIssues = apiHandler.GetAllIssues(rc);
                reviewHelper.UserIssues = apiHandler.GetAllCOIssuesOfUser(rc);
                Table dataTable = generateTable(reviewHelper.AllIssues);

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
                foreach (IssueModel element in data)
                {
                    row = new TableRow();
                    row.CssClass = "row table_row";
                    row.ID = "tablerow" + rowID;
                    rowID++;

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

                    if (element.Status.ToUpper().Equals("FINISHED"))
                    {
                        LinkButton showLinkButton = reviewHelper.CreateShowLinkButton(hyperlinkid);
                        showLinkButton.Click += new EventHandler(OnShowReviews_Click);
                        tTitle.Controls.Add(showLinkButton);

                        bool contains = reviewHelper.UserIssues.Any(p => p.Id == element.Id);
                        if (contains)
                        {
                            LinkButton addLinkButton = reviewHelper.CreateAddLinkButton(hyperlinkid);
                            addLinkButton.Click += new EventHandler(OnAddReviews_Click);
                            tTitle.Controls.Add(addLinkButton);
                        }
                        Panel addReviewPanel = reviewHelper.CreateAddReviewPanel(element, hyperlinkid, this);
                        addReviewPanel.Visible = false;
                        tTitle.Controls.Add(addReviewPanel);
                        Panel showReviewPanel = reviewHelper.CreateShowReviewPanel(element, rc, hyperlinkid, this);
                        showReviewPanel.Visible = false;
                        tTitle.Controls.Add(showReviewPanel);
                    }
                    row.Cells.Add(tTitle);

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                        Request.ApplicationPath.TrimEnd('/') + "/";
                    pTitle.Attributes["onclick"] = "javascript:location.href='" + baseUrl + "IssueDetail?issueId=" + element.Id + "'";

                    dataTable.Rows.Add(row);

                    row = new TableRow();
                    row.CssClass = "row spacer";
                    row.ID = "tablerow" + rowID;
                    rowID++;
                    tTitle = new TableCell();
                    row.Cells.Add(tTitle);
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

        protected void btnNewIssue_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateIssue.aspx");
        }

        public void OnAddReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            reviewHelper.HandleLinkButtonClick(link, reviewHelper.ReviewAddPanelList, pressedlinkbuttonid, dataTable, "addReviewLink");
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
                    Panel addPanel = reviewHelper.ReviewAddPanelList.Find(x => x.ID == "addReviewPanel" + pressedbuttonid);
                    if (addPanel != null)
                    {
                        addPanel.Controls.Add(panelOK);
                    }
                }
                else
                {
                    Panel panelNOK = reviewHelper.CreateReviewAddFailPanel();
                    Panel addPanel = reviewHelper.ReviewAddPanelList.Find(x => x.ID == "addReviewPanel" + pressedbuttonid);
                    if (addPanel != null)
                    {
                        addPanel.Controls.Add(panelNOK);
                    }
                }
            }
        }

        public void OnShowReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            reviewHelper.HandleLinkButtonClick(link, reviewHelper.ReviewShowPanelList, pressedlinkbuttonid, dataTable, "showReviewLink");
        }
    }
}