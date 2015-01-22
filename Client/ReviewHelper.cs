using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public class ReviewAPIHandler
    {
        public ReviewAPIHandler()
        {

        }

        public List<IssueModel> GetAllIssues(RestClient rc)
        {
            rc.EndPoint = "api/Issue/";
            rc.Method = HttpVerb.GET;
            var json = rc.MakeRequest();
            return JsonConvert.DeserializeObject<List<IssueModel>>(json);
        }

        public List<IssueModel> GetAllCOIssuesOfUser(RestClient rc)
        {
            rc.EndPoint = "api/AllIssue/OfUser";
            rc.Method = HttpVerb.GET;
            var userissue = rc.MakeRequest();
            return JsonConvert.DeserializeObject<List<IssueModel>>(userissue);
        }

        public List<IssueModel> GetOwnerIssuesOfUser(RestClient rc)
        {
            rc.EndPoint = "api/Issue/OfUser";
            rc.Method = HttpVerb.GET;
            var userissue = rc.MakeRequest();
            return JsonConvert.DeserializeObject<List<IssueModel>>(userissue);
        }

        public String AddReview(RestClient rc, ReviewModel newReview)
        {
            rc.EndPoint = "api/Review/Add";
            rc.Method = HttpVerb.POST;
            rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
            rc.PostData = JsonConvert.SerializeObject(newReview);
            var json = rc.MakeRequest();
            return json;
        }

        public List<ReviewModel> GetIssueReviews(RestClient rc, IssueModel element)
        {
            rc.EndPoint = "api/Review?issueId=" + element.Id;
            rc.Method = HttpVerb.GET;
            var json = rc.MakeRequest();
            return JsonConvert.DeserializeObject<List<ReviewModel>>(json);
        }
    }

    public class ReviewHelper
    {
        List<Panel> reviewAddPanelList;
        List<Panel> reviewShowPanelList;
        List<LinkButton> addReviewLinkButtonList;
        List<LinkButton> showReviewLinkButtonList;
        List<IssueModel> userIssues;
        List<IssueModel> allIssues;

        public ReviewHelper()
        {
            reviewAddPanelList = new List<Panel>();
            reviewShowPanelList = new List<Panel>();
            addReviewLinkButtonList = new List<LinkButton>();
            showReviewLinkButtonList = new List<LinkButton>();
            userIssues = new List<IssueModel>();
            allIssues = new List<IssueModel>();
        }

        public List<Panel> ReviewAddPanelList
        {
            get { return reviewAddPanelList; }
            set { this.reviewAddPanelList = value; }
        }

        public List<Panel> ReviewShowPanelList
        {
            get { return reviewShowPanelList; }
            set { this.reviewShowPanelList = value; }
        }

        public List<LinkButton> AddReviewLinkButtonList
        {
            get { return addReviewLinkButtonList; }
            set { this.addReviewLinkButtonList = value; }
        }

        public List<LinkButton> ShowReviewLinkButtonList
        {
            get { return showReviewLinkButtonList; }
            set { this.showReviewLinkButtonList = value; }
        }

        public List<IssueModel> UserIssues
        {
            get { return userIssues; }
            set { this.userIssues = value; }
        }

        public List<IssueModel> AllIssues
        {
            get { return allIssues; }
            set { this.allIssues = value; }
        }

        public LinkButton CreateShowLinkButton(int hyperlinkid)
        {
            LinkButton showLinkButton = new LinkButton();
            showLinkButton.Text = "Show Review";
            showLinkButton.ID = "LinkButtonshowReview" + hyperlinkid; ;
            SetLinkButtonCSS(showLinkButton);
            ShowReviewLinkButtonList.Add(showLinkButton);
            return showLinkButton;
        }

        public LinkButton CreateAddLinkButton(int hyperlinkid)
        {
            LinkButton addLinkButton = new LinkButton();
            addLinkButton.Text = "Add Review";
            addLinkButton.ID = "LinkButtonaddReview" + hyperlinkid;
            SetLinkButtonCSS(addLinkButton);
            addLinkButton.Style.Add("margin-left", "5px");
            AddReviewLinkButtonList.Add(addLinkButton);
            return addLinkButton;
        }

        public Panel CreateReviewAddSuccessPanel()
        {
            Panel panelOK = new Panel();
            Label labelOK = new Label();
            panelOK.Style.Add("background-color", "#68DB5C");
            SetMessagePanelStyle(panelOK);
            labelOK.Text = "Review added!";
            panelOK.Controls.Add(labelOK);
            return panelOK;
        }

        public Panel CreateReviewAddFailPanel()
        {
            Panel panelNOK = new Panel();
            Label labelNOK = new Label();
            panelNOK.Style.Add("background-color", "#FF6F6B");
            SetMessagePanelStyle(panelNOK);
            labelNOK.Text = "Review NOT added!";
            panelNOK.Controls.Add(labelNOK);
            return panelNOK;
        }

        //Sets ShowPanel style
        public void SetShowReviewPanelCSS(Panel showReviewPanel, int i)
        {
            showReviewPanel.Style.Add("padding-left", "45px");
            showReviewPanel.Style.Add("padding-top", "10px");
            showReviewPanel.Style.Add("font-family", "Arial");
            showReviewPanel.Style.Add("font-size", "14px");
            showReviewPanel.Style.Add("border-bottom", "2px dashed grey");
            if (i % 2 == 0)
            {
                showReviewPanel.Style.Add("background-color", "#4BB3A7");
            }
            else
            {
                showReviewPanel.Style.Add("background-color", "#4BB3A8");
            }
            
        }

        //Sets AddPanel style
        public void SetAddReviewPanelCSS(Panel addReviewPanel)
        {
            addReviewPanel.Style.Add("padding-left", "45px");
            addReviewPanel.Style.Add("padding-top", "10px");
            addReviewPanel.Style.Add("padding-bottom", "5px");
            addReviewPanel.Style.Add("background-color", "#4BB3A7");
        }

        //Sets LinkButton style
        public void SetLinkButtonCSS(LinkButton button)
        {
            button.Style.Add("font-size", "14px");
            button.Style.Add("margin-left", "35px");
            button.Style.Add("margin-bottom", "5px");
            button.Style.Add("padding", "10px");
        }

        //Returns idNumber of ControlID-String
        public int getIDNumberOfControl(String tableRowID)
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

        //Sets MessagePanel style
        public void SetMessagePanelStyle(Panel panel)
        {
            panel.Style.Add("margin-right", "5%");
            panel.Style.Add("margin-top", "10px");
            panel.Style.Add("margin-bottom", "5px");
            panel.Style.Add("padding", "5px");
        }

        //Creates AddReview Panel
        public Panel CreateAddReviewPanel(IssueModel element, int hyperlinkid, MyIssues site)
        {
            Panel reviewPanel = new Panel();
            reviewPanel.ID = "addReviewPanel" + hyperlinkid;
            SetAddReviewPanelCSS(reviewPanel);
            reviewAddPanelList.Add(reviewPanel);
            Table reviewTable = new Table();
            for (int i = 0; i < 3; i++)
            {
                TableRow issueRow = new TableRow();
                TableCell issueLabelCell = new TableCell();
                Label issueLabel;
                issueLabelCell.Style.Add("padding", "3px");
                issueLabelCell.Style.Add("padding", "3px");
                switch (i)
                {
                    case 0:
                        //reviewTextBox.Text = element.Id.ToString();
                        HiddenField issuehiddenfield = new HiddenField();
                        issuehiddenfield.Value = element.Id.ToString();
                        issuehiddenfield.ID = "issueHiddenField" + hyperlinkid;
                        issueLabelCell.Controls.Add(issuehiddenfield);
                        //reviewTextBox.ID = "issueTextBox" + hyperlinkid;
                        break;
                    case 1:
                        TextBox ratingTextBox = new TextBox();
                        issueLabel = new Label();
                        issueLabel.Width = 120;
                        ratingTextBox.Width = 500;
                        issueLabel.Text = "Rating (1-5)";
                        //ratingTextBox.Value = element.Id.ToString();
                        ratingTextBox.ID = "ratingTextBox" + hyperlinkid;
                        issueLabelCell.Controls.Add(issueLabel);
                        issueLabelCell.Controls.Add(ratingTextBox);
                        //reviewTextBox.Text = element.ReviewRating.ToString();
                        //reviewTextBox.ID = "ratingTextBox" + hyperlinkid;
                        break;
                    case 2:
                        TextBox reviewTextBox = new TextBox();
                        issueLabel = new Label();
                        issueLabel.Width = 120;
                        reviewTextBox.Width = 500;
                        issueLabel.Text = "Explanation";
                        reviewTextBox.Text = element.Description;
                        reviewTextBox.TextMode = TextBoxMode.MultiLine;
                        reviewTextBox.ID = "explanationTextBox" + hyperlinkid;
                        issueLabelCell.Controls.Add(issueLabel);
                        issueLabelCell.Controls.Add(reviewTextBox);
                        break;
                    //case 3: issueLabel.Text = "UserFirstName";
                    //  reviewTextBox.ID = "userFirstNameTextBox" + hyperlinkid; break;
                    //case 4: issueLabel.Text = "UserLastName";
                    //  reviewTextBox.ID = "userLastNameTextBox" + hyperlinkid; break;
                }
                issueRow.Controls.Add(issueLabelCell);
                reviewTable.Controls.Add(issueRow);
            }
            reviewPanel.Controls.Add(reviewTable);
            TableRow issueRowButton = new TableRow();
            TableCell issueLabelCellButton = new TableCell();
            //issueLabelCellButton.Style.Add("padding-left", "122px");
            issueLabelCellButton.Style.Add("padding-top", "10px");
            Button saveReviewButton = new Button();
            saveReviewButton.ID = "saveReviewButton" + hyperlinkid;
            saveReviewButton.Click += new EventHandler(site.OnSaveReviewButton_Click);
            saveReviewButton.Text = "Save Review";
            issueLabelCellButton.Controls.Add(saveReviewButton);
            issueRowButton.Controls.Add(issueLabelCellButton);
            reviewTable.Controls.Add(issueRowButton);
            reviewPanel.Controls.Add(reviewTable);
            return reviewPanel;
        }

        //Creates AddReview Panel
        public Panel CreateAddReviewPanel(IssueModel element, int hyperlinkid, AllIssues site)
        {
            Panel reviewPanel = new Panel();
            reviewPanel.ID = "addReviewPanel" + hyperlinkid;
            SetAddReviewPanelCSS(reviewPanel);
            reviewAddPanelList.Add(reviewPanel);
            Table reviewTable = new Table();
            for (int i = 0; i < 3; i++)
            {
                TableRow issueRow = new TableRow();
                TableCell issueLabelCell = new TableCell();
                Label issueLabel;
                issueLabelCell.Style.Add("padding", "3px");
                issueLabelCell.Style.Add("padding", "3px");
                switch (i)
                {
                    case 0:
                        //reviewTextBox.Text = element.Id.ToString();
                        HiddenField issuehiddenfield = new HiddenField();
                        issuehiddenfield.Value = element.Id.ToString();
                        issuehiddenfield.ID = "issueHiddenField" + hyperlinkid;
                        issueLabelCell.Controls.Add(issuehiddenfield);
                        //reviewTextBox.ID = "issueTextBox" + hyperlinkid;
                        break;
                    case 1:
                        TextBox ratingTextBox = new TextBox();
                        issueLabel = new Label();
                        issueLabel.Width = 120;
                        ratingTextBox.Width = 500;
                        issueLabel.Text = "Rating (1-5)";
                        //ratingTextBox.Value = element.Id.ToString();
                        ratingTextBox.ID = "ratingTextBox" + hyperlinkid;
                        issueLabelCell.Controls.Add(issueLabel);
                        issueLabelCell.Controls.Add(ratingTextBox);
                        //reviewTextBox.Text = element.ReviewRating.ToString();
                        //reviewTextBox.ID = "ratingTextBox" + hyperlinkid;
                        break;
                    case 2: 
                        TextBox reviewTextBox = new TextBox();
                        issueLabel = new Label();
                        issueLabel.Width = 120;
                        reviewTextBox.Width = 500;
                        issueLabel.Text = "Explanation";
                        reviewTextBox.Text = element.Description;
                        reviewTextBox.TextMode = TextBoxMode.MultiLine;
                        reviewTextBox.ID = "explanationTextBox" + hyperlinkid;
                        issueLabelCell.Controls.Add(issueLabel);
                        issueLabelCell.Controls.Add(reviewTextBox);
                        break;
                    //case 3: issueLabel.Text = "UserFirstName";
                      //  reviewTextBox.ID = "userFirstNameTextBox" + hyperlinkid; break;
                    //case 4: issueLabel.Text = "UserLastName";
                      //  reviewTextBox.ID = "userLastNameTextBox" + hyperlinkid; break;
                }
                issueRow.Controls.Add(issueLabelCell);
                reviewTable.Controls.Add(issueRow);
            }
            reviewPanel.Controls.Add(reviewTable);
            TableRow issueRowButton = new TableRow();
            TableCell issueLabelCellButton = new TableCell();
            //issueLabelCellButton.Style.Add("padding-left", "122px");
            issueLabelCellButton.Style.Add("padding-top", "10px");
            Button saveReviewButton = new Button();
            saveReviewButton.ID = "saveReviewButton" + hyperlinkid;
            saveReviewButton.Click += new EventHandler(site.OnSaveReviewButton_Click);
            saveReviewButton.Text = "Save Review";
            issueLabelCellButton.Controls.Add(saveReviewButton);
            issueRowButton.Controls.Add(issueLabelCellButton);
            reviewTable.Controls.Add(issueRowButton);
            reviewPanel.Controls.Add(reviewTable);
            return reviewPanel;
        }

        public void HandleLinkButtonClick(LinkButton link, List<Panel> panelList, int pressedlinkbuttonid, Table dataTable, String buttontype)
        {
            InvisiblePanels(pressedlinkbuttonid, buttontype);
            foreach (LinkButton button in AddReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            foreach (LinkButton button in ShowReviewLinkButtonList)
            {
                button.Style.Add("background-color", "none");
            }
            link.Style.Add("background-color", "#4BB3A7");
            int i = 0;
            while (i <= dataTable.Rows.Count - 1)
            {
                if (dataTable.Rows[i].ID != null && pressedlinkbuttonid == getIDNumberOfControl(dataTable.Rows[i].ID)) //pressedlinkbuttonid == getIDNumberOfControl(panelList[i].ID)
                {
                    Panel panel = panelList.Find(x => getIDNumberOfControl(x.ID)==pressedlinkbuttonid);
                    panel.Visible = true;
                    //panelList[i].Visible = true;
                }
                i++;
            }
            
        }

        //renders all reviews of an specific issue
        public Panel CreateShowReviewPanel(IssueModel element, RestClient rc, int hyperlinkid, Page site)
        {
            if (site.User.Identity.IsAuthenticated && rc != null)
            {
                Panel showReviewPanel = new Panel();
                showReviewPanel.ID = "showReviewPanel" + hyperlinkid;
                //ReviewModel issueReview = new ReviewModel();
                //rc.EndPoint = "api/Review?issueId=" + element.Id;
                //rc.Method = HttpVerb.GET;
                //var json = rc.MakeRequest();
                ReviewAPIHandler apiHandler=new ReviewAPIHandler();
                List<ReviewModel> issueReviewsList = apiHandler.GetIssueReviews(rc, element);
                if (issueReviewsList.Count < 1)
                {
                    Panel rowPanel = new Panel();
                    Label noReviewLabel = new Label();
                    noReviewLabel.Text = "No Reviews added!";
                    rowPanel.Controls.Add(noReviewLabel);
                    showReviewPanel.Controls.Add(rowPanel);
                    SetShowReviewPanelCSS(showReviewPanel, 0);
                }else{
                int i = 0;
                foreach (var review in issueReviewsList)
                {
                    Panel rowPanel = new Panel();
                    SetShowReviewPanelCSS(rowPanel, i);
                    Table showReviewTable = new Table();
                    //showReviewTable.ID = "showReviewTable" + hyperlinkid;
                    TableRow UserNameRow = new TableRow();
                    TableCell UserNameCell = new TableCell();
                    UserNameCell.Style.Add("font-size", "16px");
                    Label firstNameLabel = new Label();
                    firstNameLabel.Text = review.UserFirstName + " " + review.UserLastName;
                    UserNameCell.Controls.Add(firstNameLabel);
                    UserNameRow.Controls.Add(UserNameCell);
                    showReviewTable.Controls.Add(UserNameRow);


                    TableRow explanationRow = new TableRow();
                    TableCell explanationCell = new TableCell();
                    explanationCell.Style.Add("font-size", "12px");
                    explanationCell.Style.Add("padding-top", "10px");
                    Label explanationLabel = new Label();
                    explanationLabel.Text = review.Explanation;
                    explanationCell.Controls.Add(explanationLabel);
                    explanationRow.Controls.Add(explanationCell);
                    showReviewTable.Controls.Add(explanationRow);

                    rowPanel.Controls.Add(showReviewTable);
                    showReviewPanel.Controls.Add(rowPanel);
                    i++;
                }
                }
                
                reviewShowPanelList.Add(showReviewPanel);
                return showReviewPanel;
            }
            else
            {
                return null;
            }
        }

        //invisibles all non-clicked panels
        public void InvisiblePanels(int pressedlinkbuttonid, string linktype)
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
                        //if (i != pressedlinkbuttonid) 
                        panel.Visible = false;
                        i++;
                    } break;
                case "addReviewLink":
                    foreach (Panel panel in reviewShowPanelList)
                    {
                        panel.Visible = false;
                    }
                    foreach (Panel panel in reviewAddPanelList)
                    {
                        //if (i != pressedlinkbuttonid)
                        panel.Visible = false;
                        i++;
                    } break;
                default: break;
            }
        }

        //Creates a new ReviewModel
        public void SetNewReviewModel(ReviewModel newReview, int pressedbuttonid, RestClient rc)
        {
            Panel addPanel = reviewAddPanelList.Find(x => x.ID == "addReviewPanel" + pressedbuttonid);
            if (addPanel != null)
            {
                //TextBox issueTextBox = (TextBox)addPanel.FindControl("issueTextBox" + pressedbuttonid);
                HiddenField issuehiddenfield = (HiddenField)addPanel.FindControl("issueHiddenField" + pressedbuttonid);
                TextBox ratingTextBox= (TextBox)addPanel.FindControl("ratingTextBox" + pressedbuttonid);
                TextBox explanationTextBox = (TextBox)addPanel.FindControl("explanationTextBox" + pressedbuttonid);
                //TextBox userFirstNameTextBox = (TextBox)addPanel.FindControl("userFirstNameTextBox" + pressedbuttonid);
                //TextBox userLastNameTextBox = (TextBox)addPanel.FindControl("userLastNameTextBox" + pressedbuttonid);
                newReview.Issue = Convert.ToInt32(issuehiddenfield.Value);
                int rating = Convert.ToInt32(ratingTextBox.Text);
                if(rating >=0 && rating <=5){
                    newReview.Rating = rating;
                }
                
                newReview.Explanation = explanationTextBox.Text;
                newReview.UserFirstName = rc.User.FirstName;
                newReview.UserLastName = rc.User.LastName;
                //newReview.UserFirstName = userFirstNameTextBox.Text;
                //newReview.UserLastName = userLastNameTextBox.Text;
            }

            
        }

    }
}