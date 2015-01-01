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
                    tTitle.Controls.Add(addReviewHyperlink);
                    LinkButton showReviewHyperlink = new LinkButton();
                    showReviewHyperlink.Text = "Show Review";
                    showReviewHyperlink.ID = "hyperlinkshowReview" + hyperlinkid; ;
                    showReviewHyperlink.Click += new EventHandler(OnShowReviews_Click);
                    tTitle.Controls.Add(showReviewHyperlink);

                    Panel reviewPanel = CreateAddReviewPanel(element);
                    reviewPanel.Visible = false;
                    
                    tTitle.Controls.Add(reviewPanel);
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

        protected Panel CreateAddReviewPanel(IssueModel element)
        {
            Panel reviewPanel = new Panel();
            reviewPanel.ID = "ReviewPanel" + hyperlinkid;
            reviewAddPanelList.Add(reviewPanel);
            Table reviewTable = new Table();
            reviewTable.Style.Add("margin", "10px");
            for (int i = 0; i < 5; i++)
            {
                TableRow issueRow = new TableRow();
                TableCell issueLabelCell = new TableCell();
                Label issueLabel = new Label();
                TextBox reviewTextBox = new TextBox();
                issueRow.Style.Add("margin-top", "10px");
                issueLabelCell.Style.Add("padding", "10px");
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

        public void OnShowReviews_Click(object sender, EventArgs e)
        {
            
        }

        public void DummyMethod_Click(object sender, EventArgs e)
        {

        }

        public void OnSaveReviewButton_Click(object sender, EventArgs e)
        {
            Button link = (Button)sender;
            int pressedbuttonid = getIDNumberOfControl(link.ID);
            TextBox issueTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("issueTextBox" + pressedbuttonid);
            HiddenField issuehiddenfield = (HiddenField)reviewAddPanelList[pressedbuttonid].FindControl("issueHiddenField" + pressedbuttonid);
            TextBox ratingTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("ratingTextBox" + pressedbuttonid);
            TextBox explanationTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("explanationTextBox" + pressedbuttonid);
            TextBox userFirstNameTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("userFirstNameTextBox" + pressedbuttonid);
            TextBox userLastNameTextBox = (TextBox)reviewAddPanelList[pressedbuttonid].FindControl("userLastNameTextBox" + pressedbuttonid);
            ReviewModel newReview = new ReviewModel();
            newReview.Issue = Convert.ToInt32(issuehiddenfield.Value);
            newReview.Rating = Convert.ToInt32(ratingTextBox.Text);
            newReview.Explanation = explanationTextBox.Text;
            newReview.UserFirstName = userFirstNameTextBox.Text;
            newReview.UserLastName = userLastNameTextBox.Text;
        }

        public void OnAddReviews_Click(object sender, EventArgs e)
        {

            LinkButton link = (LinkButton)sender;
            int pressedlinkbuttonid = getIDNumberOfControl(link.ID);
            int i = 0;
            while (i<=dataTable.Rows.Count-1)
            {
                if (dataTable.Rows[i].ID != null && pressedlinkbuttonid == getIDNumberOfControl(dataTable.Rows[i].ID) && pressedlinkbuttonid == getIDNumberOfControl(reviewAddPanelList[i].ID))
                {
                    reviewAddPanelList[i].Visible = true;
                }
                i++;
            }
            
        }

        protected int getIDNumberOfControl(String tableRowID)
        {
            String pressedTableRowNumber=null;
            for (int i = 0; i <= tableRowID.Length-1; i++)
            {
                switch (tableRowID[i])
                {
                    case '0': pressedTableRowNumber = pressedTableRowNumber+"0"; break;
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