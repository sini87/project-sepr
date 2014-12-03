using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.MyAccount
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (rc != null)
            {
                TextBox_FirstName.Text = rc.User.FirstName;
            }
        }
    }
}