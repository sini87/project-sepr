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
        
        <asp:TextBox ID="text" runat="server" Visible="false"></asp:TextBox>

        <asp:Table ID="IssueTable" runat="server" Width="100%" Visible="false"> 
            <asp:TableRow>
                <asp:TableCell>Id</asp:TableCell>
                <asp:TableCell>Title</asp:TableCell>
                <asp:TableCell>Tags</asp:TableCell>
                <asp:TableCell>Status</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         
            <a id="landingpage_button" runat="server" href="~/MyAccount/Register">
                GETTING STARTED
            </a>
        
    </div>
    
</asp:Content>