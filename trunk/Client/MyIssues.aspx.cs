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
        List<Panel> reviewAddPanelList = new List<Panel>();
        List<Panel> reviewShowPanelList = new List<Panel>();
        List<LinkButton> addReviewHyperlinkList = new List<LinkButton>();
        List<LinkButton> showReviewHyperlinkList = new List<LinkButton>();
        RestClient rc;
        ReviewHelper reviewHelper = new ReviewHelper();
        
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

                    LinkButton addReviewHyperlink = new LinkButton();
                    addReviewHyperlink.Text = "Add Review";
                    addReviewHyperlink.ID = "hyperlinkaddReview" + hyperlinkid;
                    addReviewHyperlink.Click += new EventHandler(OnAddReviews_Click);
                    addReviewHyperlink.Style.Add("font-size", "14px");
                    addReviewHyperlink.Style.Add("margin-left", "45px");
                    addReviewHyperlink.Style.Add("padding", "5px");
                    addReviewHyperlinkList.Add(addReviewHyperlink);
                                        
                    tTitle.Controls.Add(addReviewHyperlink);
                    LinkButton showReviewHyperlink = new LinkButton();
                    showReviewHyperlink.Text = "Show Review";
                    showReviewHyperlink.ID = "hyperlinkshowReview" + hyperlinkid; ;
                    showReviewHyperlink.Click += new EventHandler(OnShowReviews_Click);
                    showReviewHyperlink.Style.Add("font-size", "14px");
                    showReviewHyperlink.Style.Add("padding", "5px");
                    tTitle.Controls.Add(showReviewHyperlink);
                    showReviewHyperlinkList.Add(showReviewHyperlink);

                    Panel addReviewPanel = CreateAddReviewPanel(element); //creates the add-panel
                    Panel showReviewPanel = CreateShowReviewPanel(element); //creates added reviews
                    addReviewPanel.Visible = false;
                    showReviewPanel.Visible = false;
                    
                    tTitle.Controls.Add(addReviewPanel);
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

        protected Panel CreateShowReviewPanel(IssueModel element)
        {
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                Panel showReviewPanel = new Panel();
                showReviewPanel.ID = "showReviewPanel" + hyperlinkid;
                reviewHelper.SetShowReviewPanelCSS(showReviewPanel);
                Table showReviewTable = new Table();
                showReviewTable.ID = "showReviewTable" + hyperlinkid;
                ReviewModel issueReview = new ReviewModel();
                rc.EndPoint = "api/Review?issueId=" + element.Id;
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<ReviewModel> issueReviewsList = JsonConvert.DeserializeObject<List<ReviewModel>>(json);
                foreach (var review in issueReviewsList)
                {
                    TableRow UserNameRow = new TableRow();
                    TableCell UserNameCell = new TableCell();
                    //firstNameCell.Style.Add("padding-left", "5px");
                    UserNameCell.Style.Add("font-size", "14px");
                    Label firstNameLabel = new Label();
                    firstNameLabel.Text = review.UserFirstName+" "+review.UserLastName;
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

                    showReviewPanel.Controls.Add(showReviewTable);


                }
                reviewShowPanelList.Add(showReviewPanel);
                return showReviewPanel;
            }
            else
            {
                return null;
            }
        }

        protected Panel CreateAddReviewPanel(IssueModel element)
        {
            Panel reviewPanel = new Panel();
            reviewPanel.ID = "addReviewPanel" + hyperlinkid;
            reviewHelper.SetAddReviewPanelCSS(reviewPanel);
            reviewAddPanelList.Add(reviewPanel);
            Table reviewTable = new Table();
            reviewTable.Style.Add("margin", "5px");
            for (int i = 0; i < 5; i++)
            {
                TableRow issueRow = new TableRow();
                TableCell issueLabelCell = new TableCell();
                Label issueLabel = new Label();
                TextBox reviewTextBox = new TextBox();
                issueLabelCell.Style.Add("padding", "5px");
                switch (i)
                {
                    case 0: issueLabel.Text = "IssueID"; reviewTextBox.Text = element.Id.ToString();
                        HiddenField issuehiddenfield = new HiddenField();
                        issuehiddenfield.Value = element.Id.ToString();
                        issuehiddenfield.ID = "issueHiddenField" + hyperlinkid;
                        issueLabelCell.Controls.Add(issuehiddenfield);
                        reviewTextBox.ID = "issueTextBox" + hyperlinkid; break;
                    case 1: issueLabel.Text = "Rating";
                        reviewTextBox.Text = element.ReviewRating.ToString();
                        reviewTextBox.ID = "ratingTextBox" + hyperlinkid; break;
                    case 2: issueLabel.Text = "Explanation";
                        reviewTextBox.Text = element.Description;
                        reviewTextBox.ID = "explanationTextBox" + hyperlinkid; break;
                    case 3: issueLabel.Text = "UserFirstName";
                        reviewTextBox.ID = "userFirstNameTextBox" + hyperlinkid; break;
                    case 4: issueLabel.Text = "UserLastName";
                        reviewTextBox.ID = "userLastNameTextBox" + hyperlinkid; break;
                }
                issueLabel.Width = 150;

                issueLabelCell.Controls.Add(issueLabel);
                issueLabelCell.Controls.Add(reviewTextBox);
                issueRow.Controls.Add(issueLabelCell);
                reviewTable.Controls.Add(issueRow);
            }
            reviewPanel.Controls.Add(reviewTable);
            Button saveReviewButton = new Button();
            saveReviewButton.ID = "saveReviewButton" + hyperlinkid;
            saveReviewButton.Click += new EventHandler(OnSaveReviewButton_Click);
            saveReviewButton.Text = "Save Review";
            reviewPanel.Controls.Add(saveReviewButton);
            return reviewPanel;
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
                SetNewReviewModel(newReview, pressedbuttonid);

                rc.EndPoint = "api/Review/Add";
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                rc.PostData = JsonConvert.SerializeObject(newReview);
                var json = rc.MakeRequest();
                if (json == "OK")
                {
                    Label labelOK = new Label();
                    labelOK.Text = "Review added!";
                    reviewAddPanelList[pressedbuttonid].Controls.Add(labelOK);
                }
                else
                {
                    Label labelNOK = new Label();
                    labelNOK.Text = "Review NOT added!";
                    reviewAddPanelList[pressedbuttonid].Controls.Add(labelNOK);
                }
            }
        }

        private void SetNewReviewModel(ReviewModel newReview, int pressedbuttonid)
        {
            TextBox issueTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("issueTextBox" + pressedbuttonid);
            HiddenField issuehiddenfield = (HiddenField)reviewAddPanelList[pressedbuttonid].FindControl("issueHiddenField" + pressedbuttonid);
            TextBox ratingTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("ratingTextBox" + pressedbuttonid);
            TextBox explanationTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("explanationTextBox" + pressedbuttonid);
            TextBox userFirstNameTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("userFirstNameTextBox" + pressedbuttonid);
            TextBox userLastNameTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("userLastNameTextBox" + pressedbuttonid);
            newReview.Issue = Convert.ToInt32(issuehiddenfield.Value);
            newReview.Rating = Convert.ToInt32(ratingTextBox.Text);
            newReview.Explanation = explanationTextBox.Text;
            newReview.UserFirstName = userFirstNameTextBox.Text;
            newReview.UserLastName = userLastNameTextBox.Text;
        }

        public void OnAddReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            foreach (LinkButton button in addReviewHyperlinkList)
            {
                button.Style.Add("background-color", "none");
            }
            foreach (LinkButton button in showReviewHyperlinkList)
            {
                button.Style.Add("background-color", "none");
            }

            if (reviewHelper.getIDNumberOfControl(link.ID) % 2 == 1)
            {
                link.Style.Add("background-color", "#4BB3A7");
            }
            else
            {
                link.Style.Add("background-color", "#7AD0C7");
            }
            int i = 0;
            while (i<=dataTable.Rows.Count-1)
            {
                if (dataTable.Rows[i].ID != null && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(dataTable.Rows[i].ID) && pressedlinkbuttonid == reviewHelper.getIDNumberOfControl(reviewAddPanelList[i].ID))
                {
                    reviewAddPanelList[i].Visible = true;
                }
                i++;
            }
            InvisiblePanels(pressedlinkbuttonid, "addReviewLink");    
        }

        public void OnShowReviews_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = reviewHelper.getIDNumberOfControl(link.ID);
            foreach (LinkButton button in addReviewHyperlinkList)
            {
                button.Style.Add("background-color", "none");
            }
            foreach (LinkButton button in showReviewHyperlinkList)
            {
                button.Style.Add("background-color", "none");
            }
            if (reviewHelper.getIDNumberOfControl(link.ID) % 2 == 1)
            {
                link.Style.Add("background-color", "#4BB3A7");
            }
            else
            {
                link.Style.Add("background-color", "#7AD0C7");
            }
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
    }
}