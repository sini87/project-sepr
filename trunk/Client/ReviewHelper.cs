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
    public class ReviewHelper
    {
        public ReviewHelper()
        {

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
        public Panel CreateAddReviewPanel(IssueModel element, List<Panel>reviewAddPanelList, int hyperlinkid, MyIssues issues)
        {
            Panel reviewPanel = new Panel();
            reviewPanel.ID = "addReviewPanel" + hyperlinkid;
            SetAddReviewPanelCSS(reviewPanel);
            reviewAddPanelList.Add(reviewPanel);
            Table reviewTable = new Table();
            for (int i = 0; i < 5; i++)
            {
                TableRow issueRow = new TableRow();
                TableCell issueLabelCell = new TableCell();
                issueLabelCell.Style.Add("padding", "3px");
                Label issueLabel = new Label();
                TextBox reviewTextBox = new TextBox();
                reviewTextBox.Width = 300;
                issueLabelCell.Style.Add("padding", "3px");
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
                        reviewTextBox.TextMode = TextBoxMode.MultiLine;
                        reviewTextBox.ID = "explanationTextBox" + hyperlinkid; break;
                    case 3: issueLabel.Text = "UserFirstName";
                        reviewTextBox.ID = "userFirstNameTextBox" + hyperlinkid; break;
                    case 4: issueLabel.Text = "UserLastName";
                        reviewTextBox.ID = "userLastNameTextBox" + hyperlinkid; break;
                }
                issueLabel.Width = 120;

                issueLabelCell.Controls.Add(issueLabel);
                issueLabelCell.Controls.Add(reviewTextBox);
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
            saveReviewButton.Click += new EventHandler(issues.OnSaveReviewButton_Click);
            saveReviewButton.Text = "Save Review";
            issueLabelCellButton.Controls.Add(saveReviewButton);
            issueRowButton.Controls.Add(issueLabelCellButton);
            reviewTable.Controls.Add(issueRowButton);
            reviewPanel.Controls.Add(reviewTable);
            return reviewPanel;
        }

        //renders all reviews of an specific issue
        public Panel CreateShowReviewPanel(IssueModel element, RestClient rc, List<Panel> reviewShowPanelList, int hyperlinkid, Page site)
        {
            if (site.User.Identity.IsAuthenticated && rc != null)
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
                    SetShowReviewPanelCSS(rowPanel, i);
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

        //invisibles all non-clicked panels
        public void InvisiblePanels(int pressedlinkbuttonid, string linktype, List<Panel> reviewAddPanelList,  List<Panel> reviewShowPanelList)
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

        //Creates a new ReviewModel
        public void SetNewReviewModel(ReviewModel newReview, int pressedbuttonid, List<Panel> reviewAddPanelList)
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

    }
}