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
using CDDSS_API.Models;

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
        Boolean isRightPWChange = false;

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

            internal Boolean CommitChangedUserDatas(UserShort user, Boolean isRightPWChange, String OldPassword="", String NewPassword="", String NewPasswordConfirm="")
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

            internal Boolean ChangeUserPassword(UserShort user, String OldPassword, String NewPassword, String NewConfirmedPassword)
            {
                ChangePasswordBindingModel passwordmodel = new ChangePasswordBindingModel();
                passwordmodel.NewPassword = NewPassword;
                passwordmodel.OldPassword = OldPassword;
                passwordmodel.ConfirmPassword = NewConfirmedPassword;
                rc.EndPoint = "api/Account/ChangePassword";
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                rc.PostData = JsonConvert.SerializeObject(passwordmodel);
                var jsonPW = rc.MakeRequest();
                return true;
            }
        }

        protected void Exit(object sender, EventArgs e)
        {
            client.rc = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxUsername.Enabled = false;
            MessagesDiv.Visible = false;
            client = new RestClientProgram();
            client.rc = RestClient.GetInstance(Session.SessionID);
            user = new UserShort();
            hasChangedDatas = false;
            user = client.getUserDetailed();
            TextBoxEmail.Text = user.Email;          
       }

        protected void OnSubmitButtonClick(object sender, EventArgs e)
        {     
            if (hasChangedDatas)
            {
                UserShort user = new UserShort(TextBoxEmail.Text, TextBoxFirstname.Text, TextBoxLastname.Text, TextBoxUsername.Text, TextBoxSecretQuestion.Text, TextBoxAnswer.Text, TextBoxPassword.Text);
                if (client.CommitChangedUserDatas(user, isRightPWChange, user.Password, TextBoxPassword.Text, TextBoxConfirmPassword.Text))
                {
                    MessagesDiv.Visible = true;
                    user = client.getUserDetailed();
                    //Label1.BackColor = System.Drawing.ColorTranslator.FromHtml("#66FF99");
                    Label1.Text = "Changes commitet<br />";
                    Label1.Text = Label1.Text +"Email: "+ user.Email+"<br />";
                    Label1.Text = Label1.Text +"Firstname: "+ user.FirstName + "<br />";
                    Label1.Text = Label1.Text + "Lastname: " + user.LastName + "<br />";
                    Label1.Text = Label1.Text +"Username: "+ user.UserName + "<br />";
                    Label1.Text = Label1.Text +"SecretQuestion: "+ user.SecretQuestion + "<br />";
                    Label1.Text = Label1.Text +"Answer: "+ user.Answer + "<br />";
                    if (isRightPWChange)
                    {
                        client.ChangeUserPassword(user, TextBoxOldPassword.Text, TextBoxPassword.Text, TextBoxConfirmPassword.Text);
                        Label1.Text = Label1.Text + "Password: " + TextBoxPassword.Text + "<br />";
                    }
                    else
                    {
                        Label1.Text = Label1.Text + "Password Error: Invalid password entries!<br />";
                    }
                }
                else
                {
                    //Label1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    Label1.Text = "Changes not commitet!";
                }
            }                              
        }

        protected void TextBoxFirstnameChanged(object sender, EventArgs e)
        {
            if (TextBoxFirstname.Text != "")
            {
                hasChangedDatas = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text =Label1.Text+ "Invalid Firstname entry!";
            }
                   
             
        }

        protected void TextBoxLastnameChanged(object sender, EventArgs e)
        {
            if (TextBoxLastname.Text != "")
            {
                hasChangedDatas = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = Label1.Text + "Invalid Lastname entry!";
            }
                   
        }

        protected void TextBoxUsernameChanged(object sender, EventArgs e)
        {
            if (TextBoxUsername.Text != "")
            {
                hasChangedDatas = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = Label1.Text + "Invalid Username entry!";
            }
                    
        }

        protected void TextBoxSecretQuestionChanged(object sender, EventArgs e)
        {
            if (TextBoxSecretQuestion.Text != "")
            {
                hasChangedDatas = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = Label1.Text + "Invalid SecretQuestion entry!";
            }
                    
        }

        protected void TextBoxAnswerChanged(object sender, EventArgs e)
        {
            if (TextBoxAnswer.Text != "")
            {
                hasChangedDatas = true;
            }
            
                    
        }

        protected void TextBoxPasswordChanged(object sender, EventArgs e)
        {
            if (TextBoxPassword.Text != "" && TextBoxPassword.Text == TextBoxConfirmPassword.Text && TextBoxOldPassword.Text != "")
            {
                hasChangedDatas = true;
                isRightPWChange = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = "Invalid password entries!";
            }
                
        }

        protected void TextBoxPasswordConfirmChanged(object sender, EventArgs e)
        {
            if (TextBoxConfirmPassword.Text != "" && TextBoxPassword.Text == TextBoxConfirmPassword.Text && TextBoxOldPassword.Text!="")
            {
                hasChangedDatas = true;
                isRightPWChange = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = "Invalid password entries!";
            }
                
        }

        protected void TextBoxOldPasswordChanged(object sender, EventArgs e)
        {
            if (TextBoxConfirmPassword.Text != "" && TextBoxPassword.Text == TextBoxConfirmPassword.Text && TextBoxOldPassword.Text != "")
            {
                hasChangedDatas = true;
                isRightPWChange = true;
            }
            else
            {
                MessagesDiv.Visible = true;
                Label1.Text = "Invalid password entries!";
            }

        }

        protected void OnNextButtonClick(object sender, EventArgs e)
        {
            Server.Transfer("~/Default.aspx");

        }

    }
}