<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Client.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="MainContent">
    <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
    </asp:Login>
</asp:Content>

