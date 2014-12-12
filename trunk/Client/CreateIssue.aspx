<%@ Page Title="Create New Issue" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="CreateIssue.aspx.cs" Inherits="Client.CreateIssue" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">

    <webopt:BundleReference runat="server" Path="~/createNewIssue/css" /> 

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Create New Issue</h1>

    <hr />

    <div class="col-lg-4">

        <div class="row">

            <label>Title</label><br />
            <asp:TextBox runat="server" ID="title"></asp:TextBox>

        </div>

        <div class="row">

            <label>Description</label><br />
            <asp:TextBox runat="server" ID="description"></asp:TextBox>

        </div>

        <div class="row">

            <label>Tags</label><br />
            <asp:HiddenField ID="rowCountTags" runat="server" Value="1" />
            <asp:Table ID="tagsTable" runat="server" Width="100%" Visible="false">  
                <asp:TableRow>                
                    <%--<asp:TableCell></asp:TableCell>
                    <asp:TableCell><asp:DropDownList ID="drpTags" runat="server"></asp:DropDownList></asp:TableCell>--%>
                </asp:TableRow>
            </asp:Table>
    
            <asp:LinkButton runat="server" ID="addTags" class="mya" OnClick="addTags_Click">add tag</asp:LinkButton>

        </div>

    </div>

    <div class="col-lg-4">

        <div class="row">

            <label>Stakeholder</label><br />
            <asp:HiddenField ID="rowCountStakeholder" runat="server" Value="1" />
            <asp:Table ID="stakeholderTable" runat="server" Width="100%" Visible="false">  
                <asp:TableRow>                
                        <%--<asp:TableCell></asp:TableCell>
                        <asp:TableCell><asp:DropDownList ID="drpStakeholders" runat="server"></asp:DropDownList></asp:TableCell>--%>
                </asp:TableRow>
            </asp:Table>

            <asp:LinkButton runat="server" ID="addStakeholder" class="mya" OnClick="addStakeholders_Click">add stakeholder</asp:LinkButton>
        </div>

        <div class="row">

            <label>Artefacts</label><br />
            <asp:HiddenField ID="rowCountArtefacts" runat="server" Value="1" />
            <asp:Table ID="artefactTable" runat="server" Width="100%" Visible="false">  
                <asp:TableRow>                
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell><asp:DropDownList ID="drpArtefacts" runat="server"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:LinkButton runat="server" ID="addArtefact" class="mya" OnClick="addArtefact_Click">add artefact</asp:LinkButton>

        </div>

        <div class="row">

            <label>Factors</label><br />
            <asp:HiddenField ID="rowCountFactors" runat="server" Value="1" />
            <asp:Table ID="Table1" runat="server" Width="100%" Visible="false">  
                <asp:TableRow>                
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell><asp:DropDownList ID="drpFactors" runat="server"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:LinkButton runat="server" ID="addFactor" class="mya" OnClick="addFactor_Click">add factor</asp:LinkButton>

        </div>

    </div>

    <div class="col-lg-4">

        <div class="row">

            <label>User</label><br />
            <asp:HiddenField ID="UserHiddenField" runat="server" Value="1" />
            <asp:Table ID="usersTable" runat="server" Width="100%" Visible="false">  
                
            </asp:Table>
            <asp:Panel ID="user" runat="server">
                <asp:LinkButton runat="server" ID="addUser" class="mya" OnClick="addUser_Click">add user</asp:LinkButton>
            </asp:Panel>

        </div>

        <div class="row">

            <label>Relations</label><br />
            <asp:Panel ID="relations" runat="server">
                <asp:TextBox ID="txtRelation" runat="server"></asp:TextBox><asp:DropDownList ID="drpRelation" class="dropdown_tag_small" runat="server"></asp:DropDownList><br/ />
                <asp:LinkButton runat="server" ID="addRelation" class="mya" OnClick="addRelation_Click">add relation</asp:LinkButton>
            </asp:Panel>

        </div>

        <div class="row margintopsmall">
          
            <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"/><asp:Button ID="UploadButton" runat="server" Text="Upload" Visible="false" OnClick="UploadButton_Click"/><br />
            <asp:LinkButton runat="server" ID="addDocument1" class="mya" OnClick="addDocument_Click">add a document</asp:LinkButton>

        </div>

    </div>

    <div class="col-lg-12">
        <div class="row text-right margintop">

            <asp:Button runat="server" ID="save" Text="Save" OnClick="save_Click" />
            <asp:Button runat="server" ID="savePublish" Text="Save & Publish" OnClick="savePublish_Click" />

        </div>
    </div>


 <%--<h2>Criteria</h2>
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
<asp:Button runat="server" ID="BtnCreateIssue" Text="Create Issue" />--%>
</asp:Content>

