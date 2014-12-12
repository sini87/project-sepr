using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                //Server.Transfer("~/MyIssues.aspx");
                Response.Redirect("~/MyIssues");
            }
        }

        protected void Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (RestClient.Login(Login1.UserName, Login1.Password,Session.SessionID))
            {
                e.Authenticated = true;
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (RestClient.Login(Login1.UserName, Login1.Password,Session.SessionID))
            {
                e.Authenticated = true;
            }
        }

        protected void UserName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}