using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Profile : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                rc.EndPoint = "api/User/Current/Detailed";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                UserShort user = JsonConvert.DeserializeObject<UserShort>(json);

                name.Text = user.FirstName;
                email.Text = user.Email;
                emailTxt.Text = user.Email;

                firstname.Text = user.FirstName;
                lastname.Text = user.LastName;
                
                rc.EndPoint = "api/Issue/OfUser";
                rc.Method = HttpVerb.GET;
                json = rc.MakeRequest();
                List<IssueModel> userIssue = JsonConvert.DeserializeObject<List<IssueModel>>(json);


                TableRow row;
                TableCell cell; 

                if (userIssue.Count > 0)
                {
                    foreach (IssueModel issue in userIssue)
                    {
                        row = new TableRow();
                        cell = new TableCell();

                        cell.Text = "<a href="+"/IssueDetail?issueId="+issue.Id+">"+issue.Title+"</a>";
                        row.Cells.Add(cell);
                        ownedByMeTable.Rows.Add(row);
                    }
                }
                else
                {
                    row = new TableRow();
                    cell = new TableCell();

                    cell.Text = "No Issues owned";
                    row.Cells.Add(cell);
                    ownedByMeTable.Rows.Add(row);
                }

            }
        }
    }
}