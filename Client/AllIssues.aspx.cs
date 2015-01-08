﻿using CDDSS_API.Models;
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
        List<Panel> reviewAddPanelList = new List<Panel>();
        List<Panel> reviewShowPanelList = new List<Panel>();
        List<LinkButton> addReviewLinkButtonList = new List<LinkButton>();
        List<LinkButton> showReviewLinkButtonList = new List<LinkButton>();
        RestClient rc;
        Table dataTable = new Table();
        ReviewHelper reviewHelper = new ReviewHelper();
        TableRow row;
        TableCell tTitle;
        Panel pTitle;
        Panel pTags;
        Panel pTag;
        Panel pRating;
        Panel pStatus, pStatusParent;
        Panel pDetail;
        List<IssueModel> userIssues;

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
                rc.EndPoint = "api/Issue/";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<IssueModel> allIssues = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                rc.EndPoint = "api/Issue/OfUser";
                rc.Method = HttpVerb.GET;
                var userissue = rc.MakeRequest();
                userIssues = JsonConvert.DeserializeObject<List<IssueModel>>(userissue);

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
                        LinkButton showLinkButton = new LinkButton();
                        showLinkButton.Text = "Show Review";
                        showLinkButton.ID = "LinkButtonshowReview" + hyperlinkid; ;
                        showLinkButton.Click += new EventHandler(OnShowReviews_Click);
                        reviewHelper.SetLinkButtonCSS(showLinkButton);
                        tTitle.Controls.Add(showLinkButton);
                        showReviewLinkButtonList.Add(showLinkButton);

                        bool contains = userIssues.Any(p => p.Id == element.Id);
                        if (contains)
                        {
                            LinkButton addLinkButton = new LinkButton();
                            addLinkButton.Text = "Add Review";
                            addLinkButton.ID = "LinkButtonaddReview" + hyperlinkid;
                            addLinkButton.Click += new EventHandler(OnAddReviews_Click);
                            reviewHelper.SetLinkButtonCSS(addLinkButton);
                            addLinkButton.Style.Add("margin-left", "5px");
                            addReviewLinkButtonList.Add(addLinkButton);
                            tTitle.Controls.Add(addLinkButton);
                        }
                        Panel addReviewPanel = reviewHelper.CreateAddReviewPanel(element, reviewAddPanelList, hyperlinkid, this);
                        addReviewPanel.Visible = false;
                        tTitle.Controls.Add(addReviewPanel);
                        Panel showReviewPanel = reviewHelper.CreateShowReviewPanel(element, rc, reviewShowPanelList, hyperlinkid, this);
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
            foreach (LinkButton button in addReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            foreach (LinkButton button in showReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            link.Style.Add("background-color", "#4BB3A7");
            int i = 0;
            while (i <= dataTable.Rows.Count - 1)
            {
                if (dataTable.Rows[i].ID != null && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(dataTable.Rows[i].ID) && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(reviewAddPanelList[i].ID))
                {
                    reviewAddPanelList[i].Visible = true;
                }
                i++;
            }
            reviewHelper.InvisiblePanels(pressedlinkbuttonid, "addReviewLink", reviewAddPanelList, reviewShowPanelList);
        }

        public void OnSaveReviewButton_Click(object sender, EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                Button link = (Button)sender;
                int pressedbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
                ReviewModel newReview = new ReviewModel();
                reviewHelper.SetNewReviewModel(newReview, pressedbuttonid, reviewAddPanelList);

                rc.EndPoint = "api/Review/Add";
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                rc.PostData = JsonConvert.SerializeObject(newReview);
                var json = rc.MakeRequest();
                if (json == "OK")
                {
                    Panel panelOK = new Panel();
                    Label labelOK = new Label();
                    panelOK.Style.Add("background-color", "#68DB5C");
                    reviewHelper.SetMessagePanelStyle(panelOK);
                    labelOK.Text = "Review added!";
                    reviewAddPanelList[pressedbuttonid].Controls.Add(panelOK);
                }
                else
                {
                    Panel panelNOK = new Panel();
                    Label labelNOK = new Label();
                    panelNOK.Style.Add("background-color", "#FF6F6B");
                    reviewHelper.SetMessagePanelStyle(panelNOK);
                    labelNOK.Text = "Review NOT added!";
                    panelNOK.Controls.Add(labelNOK);
                    reviewAddPanelList[pressedbuttonid].Controls.Add(panelNOK);
                }
            }
        }

        protected Panel CreateShowReviewPanel(IssueModel element)
        {
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                Panel showReviewPanel = new Panel();
                showReviewPanel.ID = "showReviewPanel" + hyperlinkid;
                ReviewModel issueReview = new ReviewModel();
                rc.EndPoint = "api/Review?issueId=" + element.Id;
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<ReviewModel> issueReviewsList = JsonConvert.DeserializeObject<List<ReviewModel>>(json);
                int i = 0;
                foreach (var review in issueReviewsList)
                {
                    Panel rowPanel = new Panel();
                    rowPanel.Style.Add("background-color", "black");
                    reviewHelper.SetShowReviewPanelCSS(rowPanel, i);
                    Table showReviewTable = new Table();
                    //showReviewTable.ID = "showReviewTable" + hyperlinkid;
                    TableRow UserNameRow = new TableRow();
                    TableCell UserNameCell = new TableCell();
                    UserNameCell.Style.Add("font-size", "14px");
                    Label firstNameLabel = new Label();
                    firstNameLabel.Text = review.UserFirstName + " " + review.UserLastName;
                    UserNameCell.Controls.Add(firstNameLabel);
                    UserNameRow.Controls.Add(UserNameCell);
                    showReviewTable.Controls.Add(UserNameRow);


                    TableRow explanationRow = new TableRow();
                    TableCell explanationCell = new TableCell();
                    explanationCell.Style.Add("font-size", "12px");
                    Label explanationLabel = new Label();
                    explanationLabel.Text = review.Explanation;
                    explanationCell.Controls.Add(explanationLabel);
                    explanationRow.Controls.Add(explanationCell);
                    showReviewTable.Controls.Add(explanationRow);

                    rowPanel.Controls.Add(showReviewTable);
                    showReviewPanel.Controls.Add(rowPanel);
                    i++;
                }
                reviewShowPanelList.Add(showReviewPanel);
                return showReviewPanel;
            }
            else
            {
                return null;
            }
        }

        public void OnShowReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            foreach (LinkButton button in addReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            foreach (LinkButton button in showReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            link.Style.Add("background-color", "#7AD0C7");
            int i = 0;
            while (i <= dataTable.Rows.Count - 1)
            {
                if (dataTable.Rows[i].ID != null && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(dataTable.Rows[i].ID) && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(reviewShowPanelList[i].ID))
                {
                    reviewShowPanelList[i].Visible = true;
                }
                i++;
            }
            InvisiblePanels(pressedlinkbuttonid, "showReviewLink");
        }

        private void InvisiblePanels(int pressedlinkbuttonid, string linktype)
        {
            int i = 0;
            switch (linktype)
            {
                case "showReviewLink":
                    foreach (Panel panel in reviewAddPanelList)
                    {
                        panel.Visible = false;
                    }
                    foreach (Panel panel in reviewShowPanelList)
                    {
                        if (i != pressedlinkbuttonid) panel.Visible = false;
                        i++;
                    } break;
                case "addReviewLink":
                    foreach (Panel panel in reviewShowPanelList)
                    {
                        panel.Visible = false;
                    }
                    foreach (Panel panel in reviewAddPanelList)
                    {
                        if (i != pressedlinkbuttonid) panel.Visible = false;
                        i++;
                    } break;
                default: break;
            }
        }

        protected int getIDNumberOfControl(String tableRowID)
        {
            String pressedTableRowNumber = null;
            for (int i = 0; i <= tableRowID.Length - 1; i++)
            {
                switch (tableRowID[i])
                {
                    case '0': pressedTableRowNumber = pressedTableRowNumber + "0"; break;
                    case '1': pressedTableRowNumber = pressedTableRowNumber + "1"; break;
                    case '2': pressedTableRowNumber = pressedTableRowNumber + "2"; break;
                    case '3': pressedTableRowNumber = pressedTableRowNumber + "3"; break;
                    case '4': pressedTableRowNumber = pressedTableRowNumber + "4"; break;
                    case '5': pressedTableRowNumber = pressedTableRowNumber + "5"; break;
                    case '6': pressedTableRowNumber = pressedTableRowNumber + "6"; break;
                    case '7': pressedTableRowNumber = pressedTableRowNumber + "7"; break;
                    case '8': pressedTableRowNumber = pressedTableRowNumber + "8"; break;
                    case '9': pressedTableRowNumber = pressedTableRowNumber + "9"; break;
                }
            }
            return Convert.ToInt32(pressedTableRowNumber);
        }
    }
}