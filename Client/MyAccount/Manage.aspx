<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Client.MyAccount.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Here you can edit your userprofile (TODO)
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_FirstName" runat="server" Text="First Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
