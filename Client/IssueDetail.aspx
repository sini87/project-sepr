<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IssueDetail.aspx.cs" Inherits="Client.IssueDetail" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h1>Issue</h1>
    <br />
    <hr />
    <asp:Panel runat="server" ID="issueTitle"></asp:Panel><br />

    <asp:Panel runat="server" ID="rating" /><br />
    <asp:Panel runat="server" ID="tag"></asp:Panel><br />
    <asp:Panel runat="server" ID="status"></asp:Panel><br />
    <h3>Description</h3><br />
    <asp:Panel runat="server" ID="description"></asp:Panel><br />
    <h3>Relation</h3>
    <asp:Panel runat="server" ID="relations"></asp:Panel><br />
    <hr />

    <h3>Stakeholder</h3><br />
    <asp:Panel runat="server" ID="stakeholder"></asp:Panel><br />


    <h3>Factors</h3><br />
    <asp:Panel runat="server" ID="factors"></asp:Panel><br />

    <h3>Artefacts</h3><br />
    <asp:Panel runat="server" ID="artefacts"></asp:Panel><br />
    
    <h3>Documents</h3><br />
    <asp:Panel runat="server" ID="documents"></asp:Panel><br />

    <h3>User</h3><br />
    <asp:Panel runat="server" ID="users"></asp:Panel><br />

    <hr />

    <h3>Criteria</h3>
    <asp:HiddenField ID="HiddenFieldCriteria" runat="server" Value="1" ViewStateMode="Enabled"/>
    <asp:Table ID="criteriaTable" runat="server" Width="50%" Visible="false">  
        <asp:TableHeaderRow>                
                <asp:TableCell>Name</asp:TableCell>
                <asp:TableCell>Description</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <asp:LinkButton runat="server" ID="addCriteria" OnClick="addCriteria_Click">add criteria</asp:LinkButton>
    <hr />

    <h3>Criteria Weight</h3>
    <asp:Panel runat="server" ID="criteriaWeight"></asp:Panel><br />
    <asp:LinkButton runat="server" ID="addComment">add comment</asp:LinkButton>
    <hr />

    <h3>Alternatives</h3>
    <asp:Label runat="server" ID="lAlternativeName" Enabled="false">Name</asp:Label><asp:label runat="server" ID="lAlternativeDescription" Enabled="false">Description</asp:label><asp:label runat="server" ID="lAlternativeReason" Enabled="false">Reason</asp:label>
    <asp:Panel runat="server" ID="alternatives"></asp:Panel><br />
    <asp:LinkButton runat="server" ID="addAlternative" OnClick="addAlternative_Click">add alternative</asp:LinkButton>

    <button>Save</button> <button>Finish Phase</button>
</asp:Content>

