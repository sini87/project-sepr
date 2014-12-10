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
                <asp:TableCell><asp:DropDownList ID="drpTags" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    <asp:LinkButton runat="server" ID="addTags" OnClick="addTags_Click">add tag</asp:LinkButton>
<h2>Description</h2>
<asp:TextBox runat="server" ID="description" Width="80%"></asp:TextBox><br />

    <h2>Stakeholder</h2>
    <asp:HiddenField ID="rowCountStakeholder" runat="server" Value="1" />
    <asp:Table ID="stakeholderTable" runat="server" Width="50%" Visible="false">  
        <asp:TableRow>                
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="drpStakeholders" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addStakeholders" OnClick="addStakeholders_Click">add stakeholder</asp:LinkButton><br />

    <h2>Artefacts</h2>
    <asp:HiddenField ID="rowCountArtefacts" runat="server" Value="1" />
    <asp:Table ID="artefactTable" runat="server" Width="50%" Visible="false">  
        <asp:TableRow>                
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="drpArtefacts" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addArtefact" OnClick="addArtefact_Click">add artefact</asp:LinkButton><br />

    <h2>Factors</h2>
    <asp:HiddenField ID="rowCountFactors" runat="server" Value="1" />
    <asp:Table ID="Table1" runat="server" Width="50%" Visible="false">  
        <asp:TableRow>                
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="drpFactors" runat="server"></asp:DropDownList></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:LinkButton runat="server" ID="addFactor" OnClick="addFactor_Click">add factor</asp:LinkButton><br />

<h2>User</h2>
<asp:Panel ID="user" runat="server">
<asp:LinkButton runat="server" ID="addUser" OnClick="addUser_Click">add User</asp:LinkButton><br />
</asp:Panel><br />

<br />
<h2>Relations</h2>
<asp:Panel ID="relations" runat="server">
<asp:TextBox ID="txtRelation" runat="server"></asp:TextBox><asp:DropDownList ID="drpRelation" runat="server"></asp:DropDownList><br />
<asp:LinkButton runat="server" ID="addRelation" OnClick="addRelation_Click">add relation</asp:LinkButton><br />
</asp:Panel><br />



<asp:FileUpload ID="FileUpload1" runat="server" Visible="false"/><asp:Button ID="UploadButton" runat="server" Text="Upload" Visible="false" OnClick="UploadButton_Click"/><br />
<asp:LinkButton runat="server" ID="addDocument" OnClick="addDocument_Click">add Document</asp:LinkButton>
    
    <br />

    <asp:Button runat="server" ID="save" Text="Save" OnClick="save_Click" />
    <asp:Button runat="server" ID="savePublish" Text="Save & Publish" OnClick="savePublish_Click" />
</asp:Content>

