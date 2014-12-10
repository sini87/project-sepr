<%@ Page Title="My Issues" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyIssues.aspx.cs" Inherits="Client.MyIssues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/myIssues/css" /> 
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="row">

        <div class="float-left">
            
            <h1>My Issues</h1>

        </div>

        <div class="float-right">
            
            <asp:Button ID="btnNewIssue" runat="server" Text="create new issue" OnClick="brnNewIssue_Click" Visible="false"/>

        </div>

    </div>

    <div class="row">
        <hr />
    </div>

    <div class="row">

        <div class="col-lg-12">
        
            <h2 runat="server" id="headingIssuesOwned" visible="false">My Issues</h2>
            <asp:Label ID="issuesOwnedText" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="OwnedIssueTable" runat="server"></asp:Panel><br />
        
            <h2 runat="server" id="headingInvolvedIssues" visible="false">All Issues</h2>
            <asp:Label ID="involvedIssuesText" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="InvolvedIssueTable" runat="server"></asp:Panel><br />
            <br />
            

        </div>
        
    </div>
    
</asp:Content>