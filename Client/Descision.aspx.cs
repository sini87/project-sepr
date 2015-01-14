using CDDSS_API;
using CDDSS_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Descission : System.Web.UI.Page
    {
        RestClient rc;
        List<AlternativeModel> alternativeList;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                SessionManager.AddUserSession(Session.SessionID);
            }
            rc = RestClient.GetInstance(Session.SessionID);
            UserSession session = SessionManager.GetUserSession(Session.SessionID);

            if (!Page.IsPostBack)
            {
                session.CreateIssueEntered();
            }
            if (rc == null)
            {
                RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                rc = RestClient.GetInstance(Session.SessionID);
            }

            RadioButtonList rbl = new RadioButtonList();
            List<AlternativeModel> altList = getAlternatives();
            if (session.TagsTRs.Count > 0)
            {
                foreach (TableRow tr in session.AlternativesTRs)
                {
                    if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                    {
                        rbl = (RadioButtonList)tr.Cells[0].Controls[0];
                    //    rbl.SelectedIndexChanged += radioButtonList_SelectedIndexChanged;
                    }
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);
            UserSession session = SessionManager.GetUserSession(Session.SessionID);

            if (!Page.IsPostBack)
            {
                session.TagsTRs.Clear();
                if (!this.User.Identity.IsAuthenticated)
                {
                    RestClient.Login("sinisa.zubic@gmx.at", "passme", Session.SessionID);
                    rc = RestClient.GetInstance(Session.SessionID);
                    //Server.Transfer("MyIssues.aspx");
                }


                RadioButtonList rbl = new RadioButtonList();
                List<AlternativeModel> altList = getAlternatives();
                if (session.TagsTRs.Count > 0)
                {
                    foreach (TableRow tr in session.AlternativesTRs)
                    {
                        if (tr.Cells[0].Controls[0].GetType() == typeof(DropDownList))
                        {
                            rbl = (RadioButtonList)tr.Cells[0].Controls[0];
                           // rbl.SelectedIndexChanged += radioButtonList_SelectedIndexChanged;
                        }
                    }
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rc = RestClient.GetInstance(Session.SessionID);

            if (!Page.IsPostBack)
            {
                rc = RestClient.GetInstance(Session.SessionID);
                rc.Issue = new IssueModel();
            }

            if (this.User.Identity.IsAuthenticated && rc != null)
            {
                generateAlternativesTable(getAlternatives());
            }
        }

        private List<AlternativeModel> getAlternatives()
        {
            String issueId = Request.QueryString["issueId"];

            rc.EndPoint = "api/Alternative?issueId=" + issueId;
            return alternativeList = JsonConvert.DeserializeObject<List<AlternativeModel>>(rc.MakeRequest());
        }

        //protected void radioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UserSession session = SessionManager.GetUserSession(Session.SessionID);
        //    RadioButtonList rbList = (RadioButtonList)sender;
        //    if (rbList.Parent == null) return;
        //    TableRow tr = (TableRow)rbList.Parent.Parent;
        //    TableCell tc = tr.Cells[0];

        //    if (rbList.SelectedIndex == rbList.Items.Count - 1)
        //    {
        //        session.TagsTRs.Clear();
        //        foreach (TableRow trow in )
        //        {
        //            session.ArtefactsTRs.Add(trow);
        //        }
        //    }
        //}


        protected void generateAlternativesTable(List<AlternativeModel> alternatives)
        {
            if (alternatives.Count > 0)
            {
                Table altTable = new Table();
                altTable.Width = Unit.Percentage(100);

                TableHeaderRow rowHeaderAlt = new TableHeaderRow();
                TableHeaderCell headerChoice = new TableHeaderCell();
                headerChoice.Text = "Choice";
                TableHeaderCell headerAltName = new TableHeaderCell();
                headerAltName.Text = "Name";
                TableHeaderCell headerAltDesc = new TableHeaderCell();
                headerAltDesc.Text = "Description";
                TableHeaderCell headerAltReason = new TableHeaderCell();
                headerAltReason.Text = "Reason";
                TableHeaderCell headerAltRating = new TableHeaderCell();
                headerAltRating.Text = "Rating";

                rowHeaderAlt.Cells.Add(headerChoice);
                rowHeaderAlt.Cells.Add(headerAltName);
                rowHeaderAlt.Cells.Add(headerAltDesc);
                rowHeaderAlt.Cells.Add(headerAltReason);
                rowHeaderAlt.Cells.Add(headerAltRating);

                altTable.Rows.Add(rowHeaderAlt);

                TableRow rowAlt;
                TableCell altRadioBtn;
                TableCell altName;
                TableCell altDesc;
                TableCell altReas;
                TableCell altRate;

                RadioButtonList rbl = new RadioButtonList();

                foreach (AlternativeModel alt in alternatives)
                {
                    rowAlt = new TableRow();
                    
                    ListItem li = new ListItem("", ""+alt.Id);
                    rbl.Items.Add(li);

                    altRadioBtn = new TableCell();
                    altRadioBtn.Controls.Add(rbl);
                    rowAlt.Cells.Add(altRadioBtn);

                    altName = new TableCell();
                    altName.Text = alt.Name;
                    rowAlt.Cells.Add(altName);

                    altDesc = new TableCell();
                    altDesc.Text = alt.Description;
                    rowAlt.Cells.Add(altDesc);

                    altReas = new TableCell();
                    altReas.Text = alt.Reason;
                    rowAlt.Cells.Add(altReas);

                    altRate = new TableCell();
                    altRate.Text = "" + alt.Rating;
                    rowAlt.Cells.Add(altRate);

                    altTable.Rows.Add(rowAlt);
                }

                Table.Controls.Add(altTable);
            }
            else
            {
                Label noAlt = new Label();
                noAlt.Text = "No Alternatives";
                Table.Controls.Add(noAlt);
            }
        }


        protected void makeDescision_Click(object sender, EventArgs e)
        {

        }
    }
}