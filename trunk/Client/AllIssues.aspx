<%@ Page Title="All Issues" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllIssues.aspx.cs" Inherits="Client.AllIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/myIssues/css" /> 
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="row">

        <div class="float-left">
            
            <h1>All Issues</h1>

        </div>

        <div class="float-right">
            
            <asp:Button ID="btnNewIssue" runat="server" Text="create new issue" OnClick="btnNewIssue_Click" Visible="false"/>

        </div>

    </div>

    <div class="row">
        <hr />
    </div>

    <div class="row">

        <div class="col-lg-12">
        
                  
            <asp:Label ID="InvolvedIssuesText" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="InvolvedIssueTable" runat="server"></asp:Panel><br />
            <br />
            

        </div>
        
    </div>
    
</asp:Content>