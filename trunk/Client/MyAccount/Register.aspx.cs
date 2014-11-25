using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Register : System.Web.UI.Page
    {
        public static bool registering = false;
        private static CDDSS_API.Models.RegisterBindingModel m;


        protected void Page_Load(object sender, EventArgs e)
        {
            CreateUserWizard.Answer = "hallo";
            CreateUserWizard.Email = "hallo";
            CreateUserWizard.Question = "hallo";
        }

        protected void ActiveStepChanged(object sender, EventArgs e)
        {
            if (CreateUserWizard.ActiveStepIndex == 1)
            {
                DataClassesDataContext ctx = new DataClassesDataContext();
                ctx.Memberships.DeleteAllOnSubmit(ctx.Memberships.ToList<Membership>());
                ctx.SubmitChanges();
                ctx.Users.DeleteAllOnSubmit(ctx.Users.ToList());
                ctx.SubmitChanges();
            }else if (CreateUserWizard.ActiveStepIndex == 2){
                RestClient.Instance.Login(m.Email, m.Password);
                RestClient.Instance.Method = HttpVerb.POST;

                RestClient.Instance.EndPoint = "http://localhost:51853/api/User";
                var json = RestClient.Instance.MakeRequest("?firstName=" + FirstName.Text + "&lastName=" + LastName.Text + "&secretQuestion=" + Question2.Text + "&answer=" + Answer2.Text);
                System.Console.WriteLine(json);
                
            }
        }

        protected void CreatingUser(object sender, LoginCancelEventArgs e)
        {
            CreateUserWizard.Email = CreateUserWizard.UserName.Split('@')[0];
            RestClient client = RestClient.Instance;
            client.EndPoint = "http://localhost:51853/api/account/Register";
            client.Method = HttpVerb.POST;
            m = new CDDSS_API.Models.RegisterBindingModel();
            m.Email = CreateUserWizard.UserName;
            m.Password = CreateUserWizard.Password;
            m.ConfirmPassword = CreateUserWizard.ConfirmPassword;
            client.PostData = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            var json = client.MakeRequest();
            if (!json.ToString().Equals("OK"))
            {
                DataClassesDataContext ctx = new DataClassesDataContext();
                User u = new User();
                u.UserName = CreateUserWizard.UserName;
                ctx.Users.InsertOnSubmit(u);
            }
            else
            {
                registering = true;
            }
            
        }

        protected void CreateUserWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            
        }

        protected void CreateUserWizard_ContinueButtonClick(object sender, EventArgs e)
        {
            registering = false;
            Server.Transfer("~/Default.aspx");
        }
    }
}