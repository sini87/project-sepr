﻿using CDDSS_API;
using Client.Model;
using Newtonsoft.Json;
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
        private string email;
        private static CDDSS_API.Models.RegisterBindingModel m;


        protected void Page_Load(object sender, EventArgs e)
        {
            //CreateUserWizard.Answer = "hallo";
            //CreateUserWizard.Email = "hallo";
            //CreateUserWizard.Question = "hallo";
            CreateUserWizard.CreateUserButtonText = "SIGN UP";
            CreateUserWizard.StepNextButtonText = "CONTINUE";
        }

        protected void ActiveStepChanged(object sender, EventArgs e)
        {
            /* solution for sometimes init step is 2 */
            if (m == null)
            {
                CreateUserWizard.ActiveStepIndex = 0;
            }
            if (CreateUserWizard.ActiveStepIndex == 1)
            {
                DataClassesDataContext ctx = new DataClassesDataContext();
                ctx.Memberships.DeleteAllOnSubmit(ctx.Memberships.ToList<Membership>());
                ctx.SubmitChanges();
                ctx.Users.DeleteAllOnSubmit(ctx.Users.ToList());
                ctx.SubmitChanges();
            }else if (CreateUserWizard.ActiveStepIndex == 2){
                CreateUserWizard.UserName = email;
                RestClient.Login(m.Email, m.Password, Session.SessionID);
                RestClient rc = RestClient.GetInstance(Session.SessionID);
                rc.Method = HttpVerb.POST;

                rc.EndPoint = "api/User";
                UserShort us = new UserShort();
                us.FirstName = FirstName.Text;
                us.LastName = LastName.Text;
                us.SecretQuestion = Question2.Text;
                us.Answer = Answer2.Text;
                rc.PostData = JsonConvert.SerializeObject(us);   
                var json = rc.MakeRequest();
                rc.User.Email = email;
                rc.User.LastName = LastName.Text;
                rc.User.FirstName = FirstName.Text;
                System.Console.WriteLine(json);
                
            }
        }

        protected void CreatingUser(object sender, LoginCancelEventArgs e)
        {
            //CreateUserWizard.Email = CreateUserWizard.UserName.Split('@')[0];
            email = CreateUserWizard.UserName;
            CreateUserWizard.UserName = CreateUserWizard.Email.Split('@')[0];
            
            m = new CDDSS_API.Models.RegisterBindingModel();
            m.Email = CreateUserWizard.Email;
            m.Password = CreateUserWizard.Password;
            m.ConfirmPassword = CreateUserWizard.ConfirmPassword;
            if (RestClient.Register(m))
            {
                registering = true;
            }
            
        }

        protected void CreateUserWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void CreateUserWizard_StepNextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex==1 && e.NextStepIndex==2)
            {
                //Handmade validation for step edit user
                TextBox firstname = (TextBox)CreateUserWizard.FindControl("FirstName");
                TextBox lastname = (TextBox)CreateUserWizard.FindControl("LastName");
                TextBox question = (TextBox)CreateUserWizard.FindControl("Question2");
                TextBox answer = (TextBox)CreateUserWizard.FindControl("Answer2");

                Literal errormessage = (Literal)CreateUserWizard.FindControl("ErrorMessage2");

                bool isValid = true;
                String errormessagetemp = "<b>Please correct the following fields:</b>"; 

                if (firstname != null && firstname.Text.Equals(""))
                {
                    isValid = false;
                    errormessagetemp += "<br/>First Name is required.";
                }
                if (lastname != null && lastname.Text.Equals(""))
                {
                    isValid = false;
                    errormessagetemp += "<br/>Last Name is required.";
                }
                if (question != null && question.Text.Equals(""))
                {
                    isValid = false;
                    errormessagetemp += "<br/>Security question is required.";
                }
                if (answer != null && answer.Text.Equals(""))
                {
                    isValid = false;
                    errormessagetemp += "<br/>Security answer is required.";
                }

                if (!isValid)
                {
                    errormessage.Text = errormessagetemp;
                    e.Cancel = true;
                    return;
                }

            }
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard_ContinueButtonClick(object sender, EventArgs e)
        {
            registering = false;
            m = null;
            Server.Transfer("~/MyIssues.aspx");
        }

        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            registering = false;
            m = null;
            Server.Transfer("~/Default.aspx");
        }

        protected void HtmlAnchor_Click2(Object sender, EventArgs e)
        {
            m = null;
            Server.Transfer("~/Default.aspx");
        }

        protected void Email_TextChanged(object sender, EventArgs e)
        {

        }


    }
}