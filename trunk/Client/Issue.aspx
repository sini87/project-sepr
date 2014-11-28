<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Issue.aspx.cs" Inherits="Client.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <h1>Issue</h1>
    <hr />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="issueTitle"></h2><br />
    <asp:Image ID="ratingImg" runat="server"/><br />
    <asp:Label ID="tags" runat="server">Tag;Tag2</asp:Label><br />
    <asp:Label ID="status" runat="server">CREATING</asp:Label><br /><br />

    <h3>Description</h3><br />
    <asp:Label ID="description" runat="server" Width="100%"></asp:Label><br /><br />

    <h3>Relations</h3><br />
    <asp:Label ID="relations" runat="server" Width="100%"></asp:Label><br /><br />

</asp:Content>
