<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateIssue.aspx.cs" Inherits="Client.CreateIssue" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h1>Create New Issue</h1>
<br /><hr />
<h2>Title</h2><br />
<asp:TextBox runat="server" ID="title"></asp:TextBox><br />
<h2>Tags</h2>
    <asp:HiddenField ID="rowCountTags" runat="server" Value="1" />
    <asp:Table ID="tagsTable" runat="server" Width="50%" Visible="false">  
        <asp:TableRow>                
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addTags" OnClick="addTags_Click">add more tags</asp:LinkButton><br />
<h2>Description</h2>
<asp:TextBox runat="server" ID="description" Width="80%"></asp:TextBox><br />
<h2>Add People</h2>
<asp:Panel ID="user" runat="server">
<asp:LinkButton runat="server" ID="addUser" OnClick="addUser_Click">add User</asp:LinkButton><br />
</asp:Panel><br />
<h2>Stakeholder</h2>
    <asp:HiddenField ID="rowCountStakeholder" runat="server" Value="1" />
    <asp:Table ID="stakeholderTable" runat="server" Width="50%" Visible="false">  
        <asp:TableRow>                
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addStakeholders" OnClick="addStakeholders_Click">add more stakeholders</asp:LinkButton><br />
<h2>Document Upload</h2>
<asp:Panel runat="server" ID="documentUpload">
    <asp:FileUpload ID="FileUpload" runat="server" />
</asp:Panel><br />

 <h2>Criteria</h2>
    <asp:HiddenField ID="rowCountCriteria" runat="server" Value="1" />
    <asp:Table ID="criteriaTable" runat="server" Width="50%" Visible="false">  
        <asp:TableHeaderRow>                
                <asp:TableCell>Name</asp:TableCell>
                <asp:TableCell>Description</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <asp:LinkButton runat="server" ID="addCriteria" OnClick="addCriteria_Click">add criteria</asp:LinkButton>
    <hr />

    <h2>Criteria Weight</h2>
    <asp:Panel runat="server" ID="criteriaWeight"></asp:Panel><br />
    <asp:LinkButton runat="server" ID="addComment" OnClick="addComment_Click">add comment</asp:LinkButton>
    <hr />

    <h2>Alternatives</h2>
    <asp:HiddenField ID="rowCountAlternatives" runat="server" Value="1" />
    <asp:Table ID="alternativesTable" runat="server" Width="50%" Visible="false">  
        <asp:TableHeaderRow>                
                <asp:TableCell>Name</asp:TableCell>
                <asp:TableCell>Description</asp:TableCell>
                <asp:TableCell>Reason</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addAlternative" OnClick="addAlternative_Click">add alternative</asp:LinkButton>
<br /><br />
<asp:Button runat="server" ID="BtnCreateIssue" Text="Create Issue" />
</asp:Content>

