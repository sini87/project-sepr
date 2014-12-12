<%@ Page Title="My Issues" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="MyIssues.aspx.cs" Inherits="Client.MyIssues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/myIssues/css" /> 

    <script src="../Scripts/rating.js"></script>
    <script src="../Scripts/myrating.js"></script>

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="row">

        <div class="float-left extra_padding_left">
            
            <h1>My Issues</h1>

        </div>

        <div class="float-right extra_padding_right notright">
            <asp:Button ID="btnNewIssue" runat="server" Text="create new issue" OnClick="brnNewIssue_Click" Visible="false"/>

        </div>

    </div>

    <div class="row">

        <div class="col-lg-12 less_padding">
        
            <asp:Label ID="issuesOwnedText" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="OwnedIssueTable" runat="server"></asp:Panel>

        </div>
        
    </div>
    
</asp:Content>