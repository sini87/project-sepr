<%@ Page Title="CDDSS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Client._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="container">
        <div class="helper">
    <div id="landingpage_content">
        <div id="landingpage_text_big">
            COLLABORATIVE DESIGN DECISION
            <br/>
            SUPPORT SYSTEM
        </div>

        <div id="landingpage_text_small">
            A Decision Making, Collaborating & Workflow Plattform
        </div>
        
        <h2 runat="server" id="headingIssuesOwned" visible="false">Owned Issues</h2>
        <asp:Label ID="issuesOwnedText" runat="server" Visible="false"></asp:Label>

        <asp:Panel ID="OwnedIssueTable" runat="server"> 
           <%-- <asp:TableHeaderRow>                
                <asp:TableCell>Title</asp:TableCell>
                <asp:TableCell>Tags</asp:TableCell>
                <asp:TableCell>Rating</asp:TableCell>
                <asp:TableCell>Status</asp:TableCell>
                <asp:TableCell>Details</asp:TableCell>
            </asp:TableHeaderRow>--%>
        </asp:Panel><br />
        
        <h2 runat="server" id="headingInvolvedIssues" visible="false">Involved Issues</h2>
        <asp:Label ID="involvedIssuesText" runat="server" Visible="false"></asp:Label>

        <asp:Panel ID="InvolvedIssueTable" runat="server"> 
            <%--<asp:TableHeaderRow>                
                <asp:TableCell>Title</asp:TableCell>
                <asp:TableCell>Tags</asp:TableCell>
                <asp:TableCell>Rating</asp:TableCell>
                <asp:TableCell>Status</asp:TableCell>
                <asp:TableCell>Details</asp:TableCell>
            </asp:TableHeaderRow>--%>
        </asp:Panel><br />

            <a id="landingpage_button" runat="server" href="~/MyAccount/Register">
                GETTING STARTED
            </a>
        
    </div>
    
</asp:Content>