using CDDSS_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Drawing;

namespace Client.MyAccount
{

    internal class PersistentDatas
    {
        internal string SessionId { get; set; }
        internal string PersistentFirstname { get; set;}
        internal string PersistentLastname { get; set;}
        internal string PersistentUsername { get; set;}
        internal string PersistentSecretQuestion { get; set;}
        internal string PersistentAnswer { get; set; }
        internal int loadedtimes { get; set; }
    }

    public partial class Manage : System.Web.UI.Page
    {
        UserShort user = null;
        RestClientProgram client;
        internal static List<PersistentDatas> persistentDatasList = new List<PersistentDatas>();
        Boolean hasChangedDatas = false;
        PersistentDatas persistentDataItem = new PersistentDatas();

        internal class RestClientProgram
        {
            internal RestClient rc {get; set;}

            internal RestClientProgram()
            {
                rc = null;
            }

            internal UserShort getUserDetailed()
            {
                UserShort user = new UserShort();
                rc.EndPoint = "api/User/Current/Detailed";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                user = JsonConvert.DeserializeObject<UserShort>(json);
                return user;
            }

            internal Boolean CommitChangedUserDatas(UserShort user)
            {
                rc.EndPoint = "api/User";
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                rc.PostData = JsonConvert.SerializeObject(user);
                var json = rc.MakeRequest();
                if (json == "OK"){
                    return true;
                }else{
                    return false;
                }
            }
        }

        protected void Exit(object sender, EventArgs e)
        {
            client.rc = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxEmail.Visible = true;
            TextBoxEmail.Enabled = false;
            string sendertext = e.ToString();
            client = new RestClientProgram();
            client.rc = RestClient.GetInstance(Session.SessionID);
            user = new UserShort();
            Label1.Visible = false;
            hasChangedDatas = false;
            user = client.getUserDetailed();
            TextBoxEmail.Text = user.Email;          
       }

        protected void OnSubmitButtonClick(object sender, EventArgs e)
        {     
            if (hasChangedDatas)
            {
                UserShort user = new UserShort(TextBoxEmail.Text, TextBoxFirstname.Text, TextBoxLastname.Text, TextBoxUsername.Text, TextBoxSecretQuestion.Text, TextBoxAnswer.Text);
                if (client.CommitChangedUserDatas(user))
                {
                    user = client.getUserDetailed();
                    Label1.Visible = true;
                    Label1.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FF99");
                    Label1.Text = "Änderungen übernommen<br />";
                    Label1.Text = Label1.Text +"Email: "+ user.Email+"<br />";
                    Label1.Text = Label1.Text +"Firstname: "+ user.FirstName + "<br />";
                    Label1.Text = Label1.Text + "Lastname: " + user.LastName + "<br />";
                    Label1.Text = Label1.Text +"Username: "+ user.UserName + "<br />";
                    Label1.Text = Label1.Text +"SecretQuestion: "+ user.SecretQuestion + "<br />";
                    Label1.Text = Label1.Text +"Answer: "+ user.Answer + "<br />";
                }
                else
                {
                    Label1.Visible = true;
                    Label1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    Label1.Text = "Änderungen konnten nicht übernommen werden";
                }
            }                              
        }

        protected void TextBoxFirstnameChanged(object sender, EventArgs e)
        {
            if (TextBoxFirstname.Text != "")
                    hasChangedDatas = true;
             
        }

        protected void TextBoxLastnameChanged(object sender, EventArgs e)
        {
            if (TextBoxLastname.Text != "")
                    hasChangedDatas = true;
        }

        protected void TextBoxUsernameChanged(object sender, EventArgs e)
        {
            if (TextBoxUsername.Text != "")
                    hasChangedDatas = true;
        }

        protected void TextBoxSecretQuestionChanged(object sender, EventArgs e)
        {
            if (TextBoxSecretQuestion.Text != "")
                    hasChangedDatas = true;
        }

        protected void TextBoxAnswerChanged(object sender, EventArgs e)
        {
            if (TextBoxAnswer.Text != "")
                    hasChangedDatas = true;
        }

    }
}