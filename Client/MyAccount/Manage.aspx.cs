﻿using CDDSS_API;
using Newtonsoft.Json;
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
        TextBoxValues value = new TextBoxValues();

        internal class TextBoxValues
        {
            internal String TextBoxEmail {get; set;}
            internal String TextBoxFirstname {get; set;}
            internal String TextBoxLastname {get; set;}
            internal String TextBoxUsername {get; set;}
            internal String TextBoxSecretQuestion {get; set;}
            internal String TextBoxAnswer { get; set; }

            internal TextBoxValues()
            {
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            RestClient rc = RestClient.GetInstance(Session.SessionID);
            value.TextBoxFirstname = TextBoxFirstname.Text;
            value.TextBoxLastname = TextBoxLastname.Text;
            value.TextBoxUsername = TextBoxUsername.Text;
            value.TextBoxSecretQuestion = TextBoxSecretQuerstion.Text;
            value.TextBoxAnswer = TextBoxAnswer.Text;
            if (rc != null)
            {
                UserShort user = new UserShort();
                rc.EndPoint = "api/User/Current/Detailed";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                user = JsonConvert.DeserializeObject<UserShort>(json);
               // TextBox_FirstName.Text = rc.User.FirstName;
                TextBoxFirstname.Text = user.FirstName;
                TextBoxLastname.Text = user.LastName;
                TextBoxUsername.Text = user.UserName;
                TextBoxSecretQuerstion.Text=user.SecretQuestion;
                TextBoxAnswer.Text = user.Answer;
                
            }

        }

        protected void OnSubmitButtonClick(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            Boolean isedited=false;
            rc.EndPoint = "api/User";
            rc.Method = HttpVerb.POST;
            rc.ContentType = "application/json";
            rc.PostData = "firstName=" + value.TextBoxFirstname + "&lastName=" + value.TextBoxLastname + "&secretQuestion=" + value.TextBoxSecretQuestion + "&answer=" + value.TextBoxAnswer;
            var json = rc.MakeRequest();
            isedited = JsonConvert.DeserializeObject<Boolean>(json);
        }
    }
}