using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RestClient rc = RestClient.GetInstance(Session.SessionID);
            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                rc.EndPoint = "api/Issue/OfUser";
                rc.Method = HttpVerb.GET;
                var json = rc.MakeRequest();
                List<IssueModel> user = JsonConvert.DeserializeObject<List<IssueModel>>(json);

                TableRow row;
                TableCell tTitle;
                TableCell tTags;
                TableCell tStatus;
                TableCell tDetail;

                if (user.Count > 0)
                {
                    foreach (IssueModel element in user)
                    {

                        row = new TableRow();

                        tTitle = new TableCell();
                        tTitle.Text = element.Title;
                        row.Cells.Add(tTitle);

                        tTags = new TableCell();
                        foreach (TagModel tagElement in element.Tags)
                        {
                            tTags.Text += tagElement.Name + ";";
                        }
                        tTags.Text = tTags.Text.Substring(0, tTags.Text.Length - 1);
                        row.Cells.Add(tTags);

                        tStatus = new TableCell();
                        tStatus.Text = element.Status;
                        row.Cells.Add(tStatus);

                        tDetail = new TableCell();
                        HyperLink lnk = new HyperLink();
                        lnk.Text = "Details";
                        lnk.NavigateUrl = "~/IssueDetail?issueId=" + element.Id;
                        tDetail.Controls.Add(lnk);

                        row.Cells.Add(tDetail);

                        IssueTable.Rows.Add(row);
                    }

                    IssueTable.Visible = true;
                }
                else
                {

                    text.Text = "No Issues existing";
                    text.Visible = true;
                }
            }
        }



        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = true;
        }
    }
}