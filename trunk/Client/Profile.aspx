<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Client.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Profile</h1>
<hr />
<asp:Image ID="profilePicture" runat="server" Height="120px" Width="120px" src="../images/avatar_woman.png" alt="Logo CDDSS"/>
<asp:Label ID="name" runat="server"></asp:Label>
<asp:Label ID="email" runat="server"></asp:Label>
<br />
<h2>Owned by me:</h2><br />
<asp:Table ID="ownedByMeTable" runat="server">
    <asp:TableHeaderRow>
        <asp:TableCell></asp:TableCell>
    </asp:TableHeaderRow>
</asp:Table>
<br />
<h2>User Info</h2>
<label>Firstname</label><br />
<asp:TextBox ID="firstname" runat="server"></asp:TextBox><br />
<label>Lastname</label><br />
<asp:TextBox ID="lastname" runat="server"></asp:TextBox><br />
<label>Email</label><br />
<asp:TextBox ID="emailTxt" runat="server"></asp:TextBox><br />
<a>Change Password</a>
</asp:Content>
