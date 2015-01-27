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
    /// <summary>
    /// code behind for final decision
    /// </summary>
    public partial class FinalDecision : System.Web.UI.Page
    {
        /// <summary>
        /// page load event
        /// site is build here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }

            int issueId;
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            RestClient rc = RestClient.GetInstance(Session.SessionID);

            if (!IsPostBack)
            {
                us.FinDecAlternativesRBs.Clear();
                issueId = int.Parse(Request["IssueId"]);

                us.FinalDecisionIssueId = issueId;
                rc.EndPoint = "api/Alternative";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest("?issueId=" + issueId.ToString());
                List<AlternativeModel> alternativeList = JsonConvert.DeserializeObject<List<AlternativeModel>>(json);
                List<RadioButton> rbList = new List<RadioButton>();

                foreach (AlternativeModel am in alternativeList)
                {
                    RadioButton rb = new RadioButton();
                    rb.Text = am.Name;
                    rb.ID = am.Id.ToString();
                    rb.GroupName = "Alternative";
                    rb.AutoPostBack = false;
                    rbList.Add(rb);
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tr.Cells.Add(tc);
                    tc.Controls.Add(rb);
                    radioTable.Rows.Add(tr);
                    us.FinDecAlternativesRBs.Add(rb);
                }

                rc.EndPoint = "api/Decision";
                rc.Method = HttpVerb.GET;
                string result = rc.MakeRequest("?issueID=" + issueId.ToString());
                if (!result.Equals("null"))
                {
                    us.FinalDecisionExists = true;
                    DecisionModel decision = JsonConvert.DeserializeObject<DecisionModel>(result);
                    foreach (RadioButton rb in rbList)
                    {
                        if (int.Parse(rb.ID) == decision.AlternativeID)
                        {
                            rb.Checked = true;
                        }
                        rb.Enabled = false;
                    }
                    description_TextBox.Text = decision.Explanation;
                    save_button.Visible = false;
                    description_TextBox.ReadOnly = true;
                }
                else
                {
                    us.FinalDecisionExists = false;
                    save_button.Text = "Save Decision";
                }
            }
            else
            {
                foreach (RadioButton rb in us.FinDecAlternativesRBs)
                {
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tr.Cells.Add(tc);
                    tc.Controls.Add(rb);
                    radioTable.Rows.Add(tr);
                }
            }


        }

        /// <summary>
        /// save decision event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void save_button_Click(object sender, EventArgs e)
        {
            UserSession us = SessionManager.GetUserSession(Session.SessionID);
            RestClient rc = RestClient.GetInstance(Session.SessionID);

            if (!us.FinalDecisionExists)
            {
                rc.EndPoint = "api/Decision/Create";
            }
            else
            {
                rc.EndPoint = "api/Decision/Edit";
            }
            rc.Method = HttpVerb.POST;
            DecisionModel dm = new DecisionModel();
            dm.IssueID = us.FinalDecisionIssueId;
            dm.Explanation = description_TextBox.Text;

            foreach (RadioButton rb in us.FinDecAlternativesRBs)
            {
                if (rb.Checked)
                {
                    dm.AlternativeID = int.Parse(rb.ID);
                }
            }
            rc.PostData = JsonConvert.SerializeObject(dm);
            string ret = rc.MakeRequest();
            Server.Transfer("MyIssues.aspx");
        }
    }
}