<%@ Page Title="All Issues" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="AllIssues.aspx.cs" Inherits="Client.AllIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/allIssues/css" /> 

    <script src="../Scripts/rating.js"></script>
    <script src="../Scripts/myrating.js"></script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="row">

        <div class="float-left extra_padding_left">
            
            <h1>All Issues</h1>

        </div>

        <div class="float-right extra_padding_right notright">
            
            <asp:Button ID="btnNewIssue" runat="server" Text="create new issue" OnClick="btnNewIssue_Click" Visible="false"/>

        </div>

    </div>

    <div class="row">

        <div class="col-lg-12 less_padding">
        
            <asp:Label ID="InvolvedIssuesText" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="InvolvedIssueTable" runat="server"></asp:Panel>

        </div>
        
    </div>
    
</asp:Content>