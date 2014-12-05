using CDDSS_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.MyAccount
{
    internal class PersistentTextBoxValues
    {
        internal String PersistentEmail { get; set; }
        internal String PersistentFirstname { get; set; }
        internal String PersistentLastname { get; set; }
        internal String PersistentUsername { get; set; }
        internal String PersistentSecretQuestion { get; set; }
        internal String PersistentAnswer { get; set; }

        internal PersistentTextBoxValues()
        {

        }
    }

    public partial class Manage : System.Web.UI.Page
    {
        PersistentTextBoxValues persistentDatas = null;
        UserShort user = null;
        RestClientProgram client;

        

        internal class RestClientProgram
        {
            internal RestClient rc {get; set;}

            internal RestClientProgram(string sessionID)
            {
                rc = RestClient.GetInstance(sessionID);
            }

            internal UserShort getUserDetailed()
            {
                rc.EndPoint = "api/User/Current/Detailed";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                return JsonConvert.DeserializeObject<UserShort>(json);
            }

            internal void CommitChangedUserDatas(UserShort user)
            {
                rc.EndPoint = "api/User";
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json"; //+ value.TextBoxFirstname + 
                rc.PostData = JsonConvert.SerializeObject(user);
                var json = rc.MakeRequest();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            client = new RestClientProgram(Session.SessionID);
            if (client.rc != null && persistentDatas==null)
            {
                        persistentDatas = new PersistentTextBoxValues();
                        user = new UserShort();
                        user = client.getUserDetailed();
                        TextBoxFirstname.Text = user.FirstName;
                        TextBoxLastname.Text = user.LastName;
                        TextBoxUsername.Text = user.UserName;
                        TextBoxSecretQuerstion.Text = user.SecretQuestion;
                        TextBoxAnswer.Text = user.Answer;
                        persistentDatas.PersistentFirstname = TextBoxFirstname.Text;
                        persistentDatas.PersistentLastname = TextBoxLastname.Text;
                        persistentDatas.PersistentUsername = TextBoxUsername.Text;
                        persistentDatas.PersistentSecretQuestion = TextBoxSecretQuerstion.Text;
                        persistentDatas.PersistentAnswer = TextBoxAnswer.Text;
             }
        }

        protected void OnSubmitButtonClick(object sender, EventArgs e)
        {
            Boolean hasChangedValues = false;
            if (TextBoxFirstname.Text != persistentDatas.PersistentFirstname ||
                TextBoxLastname.Text != persistentDatas.PersistentLastname ||
                TextBoxUsername.Text != persistentDatas.PersistentUsername ||
                TextBoxSecretQuerstion.Text != persistentDatas.PersistentSecretQuestion ||
                TextBoxAnswer.Text != persistentDatas.PersistentAnswer) hasChangedValues = true;

            if (hasChangedValues) {
                RestClientProgram client = new RestClientProgram(Session.SessionID);
                client.CommitChangedUserDatas(user);
            }
            //isedited = JsonConvert.DeserializeObject<Boolean>(json);
        }


    }
}