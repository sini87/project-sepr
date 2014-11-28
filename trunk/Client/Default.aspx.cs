using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.GET;
                //RestClient.Instance.GetType = "";
                var json = RestClient.Instance.MakeRequest();
                List<IssueModel> user = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                //TODO
            }
            else
            {
                //ListBox.Visible = false;
            }
        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = true;
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}