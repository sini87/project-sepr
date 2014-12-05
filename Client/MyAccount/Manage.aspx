<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Client.MyAccount.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <span class="auto-style1">Here you can edit your userprofile</span><br />
    <asp:Table ID="Table1" runat="server" CellSpacing="10">
        <asp:TableRow >
            <asp:TableCell>Email: </asp:TableCell>
            <asp:TableCell ID="Cell1"><asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Firstname: </asp:TableCell>
            <asp:TableCell ID="Cell2"><asp:TextBox OnTextChanged="TextBoxFirstnameChanged" ID="TextBoxFirstname" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
       <asp:TableRow>
            <asp:TableCell>Lastname: </asp:TableCell>
            <asp:TableCell ID="Cell3"><asp:TextBox OnTextChanged="TextBoxLastnameChanged" ID="TextBoxLastname" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Username: </asp:TableCell>
            <asp:TableCell ID="Cell4"><asp:TextBox OnTextChanged="TextBoxUsernameChanged" ID="TextBoxUsername" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Geheimfrage: </asp:TableCell>
            <asp:TableCell ID="Cell7"><asp:TextBox OnTextChanged="TextBoxSecretQuestionChanged"  ID="TextBoxSecretQuestion" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Antwort: </asp:TableCell>
            <asp:TableCell ID="Cell8"><asp:TextBox OnTextChanged="TextBoxAnswerChanged"  ID="TextBoxAnswer" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button OnClick="OnSubmitButtonClick" ID="Button1" runat="server" Text="Ändern"/>
            </asp:TableCell> 
        </asp:TableRow>
    </asp:Table>   
    <br />
    
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
</asp:Content>
