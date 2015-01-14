<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Descision.aspx.cs" Inherits="Client.Descission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Final Decision</h1>
    <hr />

    <div class="row">

            
            <asp:HiddenField ID="rowCount" runat="server" Value="1" />
            <asp:Panel ID="Table" runat="server"></asp:Panel>
            <br />
            <asp:TextBox ID="reason" runat="server" Width="100%"></asp:TextBox><br /><br />
            <asp:Button runat="server" ID="makeDescision" OnClick="makeDescision_Click" Text="Save"></asp:Button>

   </div>
</asp:Content>
