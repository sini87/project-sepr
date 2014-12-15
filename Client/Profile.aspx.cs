using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Profile : System.Web.UI.Page
    {
        private bool emailChanged;
        private bool passwordChanged;

        protected void Page_Preload(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
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

                        cell.Text = "<a href="+"/IssueDetail?issueId="+issue.Id+">"+issue.Title+"</a><hr/>";
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

            //Label profilepic = (Label)FindControl("ProfileAcronym");
            //if (profilepic != null)
            //{
                if (rc != null && rc.User != null)
                {
                    if (rc.User.FirstName != "" && rc.User.LastName != "" && rc.User.FirstName != null && rc.User.LastName != null)
                    {
                        ProfileAcronym.Text = rc.User.FirstName.Substring(0, 1) + rc.User.LastName.Substring(0, 1);
                    }
                }
                if (ProfileAcronym.Text.Equals(""))
                {
                    ProfileAcronym.Text = "&nbsp;";
                }
            //}
        }

        protected void saveProfile_Click(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                UserShort us = new UserShort();
                us.FirstName = firstname.Text;
                us.LastName = lastname.Text;
                if (emailChanged)
                {
                    us.Email = emailTxt.Text;
                }
                rc.Method = HttpVerb.POST;
                rc.EndPoint = "api/User";
                rc.PostData = JsonConvert.SerializeObject(us);
                var json = rc.MakeRequest();

                rc.User.FirstName = us.FirstName;
                rc.User.LastName = us.LastName;

                if (emailChanged)
                {
                    RestClient.GetInstance(Session.SessionID).Logout();
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    emailChanged = false;
                    Server.Transfer("Default.aspx");
                }
                else if (passwordChanged)
                {
                    ChangePasswordBindingModel passwordmodel = new ChangePasswordBindingModel();
                    passwordmodel.NewPassword = TextBoxNewPassword.Text;
                    passwordmodel.OldPassword = TextBoxOldPassword.Text;
                    passwordmodel.ConfirmPassword = TextBoxConfirmPassword.Text;
                    rc.EndPoint = "api/Account/ChangePassword";
                    rc.Method = HttpVerb.POST;
                    rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                    rc.PostData = JsonConvert.SerializeObject(passwordmodel);
                    var jsonPW = rc.MakeRequest();
                    if (jsonPW == "OK")
                    {
                        passwordMessageDiv.Visible = true;
                    }
                    else
                    {
                        passwordMessageDefaultDiv.Visible = true;
                    }
                }else{
                    Response.Redirect("Profile.aspx");
                }

               
            }
        }

        protected void emailTxt_TextChanged(object sender, EventArgs e)
        {
            emailChanged = true;
        }

        protected void NewPassword_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxNewPassword.Text != "" && TextBoxNewPassword.Text==TextBoxConfirmPassword.Text)
            {
                passwordChanged = true;
            }
            else
            {
                passwordChanged = false;
            }
        }

        protected void ConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxConfirmPassword.Text != "" && TextBoxNewPassword.Text == TextBoxConfirmPassword.Text)
            {
                passwordChanged = true;
            }
            else
            {
                passwordChanged = false;
            }
        }

         protected void OnChangePassword_Click(object sender, EventArgs e)
         {
             PasswordDiv.Visible = true; 
         }
    }
}