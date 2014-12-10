using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class CreateIssue : System.Web.UI.Page
    {
        DropDownList tagsList = new DropDownList();

        protected void Page_Preload(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                Server.Transfer("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);

            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                
            }

            fillTagDropDown();
        }

        protected void fillTagDropDown()
        {

            ListItem item = new ListItem();
            item.Value = "new";
            item.Text = "new";

            drpRelation.Items.Add(item);
        }
        protected void addTags_Click(object sender, EventArgs e)
        {

        }

        protected void addUser_Click(object sender, EventArgs e)
        {

        }

        protected void addStakeholders_Click(object sender, EventArgs e)
        {

        }
        
        protected void save_Click(object sender, EventArgs e)
        {

        }

        protected void savePublish_Click(object sender, EventArgs e)
        {

        }

        protected void addDocument_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            UploadButton.Visible = true;
        }

        protected void addFactor_Click(object sender, EventArgs e)
        {

        }

        protected void addArtefact_Click(object sender, EventArgs e)
        {

        }

        protected void addRelation_Click(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {

        }
    }
}