<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalDecision.aspx.cs" Inherits="Client.FinalDecision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <asp:Panel ID="mainPanel" runat="server">
        <asp:Table ID="radioTable" runat="server">
        </asp:Table>
        <asp:TextBox ID="description_TextBox" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
    </asp:Panel>
    <asp:Panel ID="controlPanel" runat="server">
        <asp:Button ID="save_button" runat="server" Text="Button" OnClick="save_button_Click" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    </asp:Content>
