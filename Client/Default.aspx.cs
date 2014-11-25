using CDDSS_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        List<UserShort> uList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var client = RestClient.Instance;
                client.EndPoint = @"http://localhost:51853/api/User";

                client.Method = HttpVerb.GET;
                client.ContentType = "text/json";
                var json = client.MakeRequest();
                List<UserShort> userList = JsonConvert.DeserializeObject<List<UserShort>>(json);
                foreach (UserShort u in userList)
                {
                    ListBox.Items.Add(new ListItem(u.LastName + " " + u.FirstName));
                    System.Console.WriteLine(u.LastName + " " + u.FirstName);
                }
            }
        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {

            
            e.Authenticated = true;
        }
    }
}