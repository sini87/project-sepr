<%@ Page Title="Issue" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="IssueDetail.aspx.cs" Inherits="Client.IssueDetail" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">

    <webopt:BundleReference runat="server" Path="~/issueDetail/css" /> 

    <script src="../Scripts/AddCriteria.js"></script>
    <script src="../Scripts/AddCriteriaWeight.js"></script>
    <script src="../Scripts/AddAlternative.js"></script>

    <script src="../Scripts/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript"> 
       
                
    </script>

    <script src="../Scripts/rating.js"></script>
    <script src="../Scripts/myrating.js"></script>

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row">

        <div class="col-lg-12">

            <div class="row">

                <h1>Issue</h1>

                <hr />
    
                <asp:Panel runat="server" ID="issueTitlePanel">
                    <asp:TextBox ID="titleText" runat="server" Text="IssueTitle" Font-Size="X-Large"></asp:TextBox>
                </asp:Panel>

                <asp:Panel runat="server" ID="ratingPanel">
                    <asp:TextBox ID="ratingLabel" runat="server" Text="RATING" CssClass="rating rating5"></asp:TextBox>
                </asp:Panel>

                <asp:Panel runat="server" ID="tagPanel" class="tag"></asp:Panel>
                <asp:LinkButton ID="addTagButton" runat="server" OnClick="addTagButton_Click">add</asp:LinkButton>

                <asp:Panel runat="server" ID="statusPanel">
                    <asp:Label ID="statusLabel" runat="server" Text="Status"></asp:Label>
                </asp:Panel>

                <h3>Description</h3>
                <asp:Panel runat="server" ID="descriptionPanel">
                    <asp:TextBox ID="descriptionText" runat="server" TextMode="MultiLine" Text="Description"></asp:TextBox>
                </asp:Panel>

                <h3>Relation</h3>
                <asp:Panel runat="server" ID="relationsPanel">
                    <asp:Label ID="relationTypeLabel" runat="server" Text="type"></asp:Label>
                    <asp:HyperLink ID="relatedIssueLink" runat="server" Text="RelationIssue"></asp:HyperLink>
                </asp:Panel>

                <hr />

            </div>

        </div>

        <hr />

        <div class="col-lg-12">

            <div class="row">

                    <h3>Stakeholder</h3>
                    <asp:Panel runat="server" ID="stakeholderPanel">
                        <asp:Table ID="stakeholderTable" runat="server"></asp:Table>
                        <asp:LinkButton ID="addStakeholder" runat="server" OnClick="addStakeholder_Click">add</asp:LinkButton>
                    </asp:Panel><br />

                    <h3>Factors</h3>
                    <asp:Panel runat="server" ID="factorsPanel">
                        <asp:Table ID="factorsTable" runat="server">
                            <asp:TableHeaderRow runat="server" ForeColor="Red">  
                                <asp:TableHeaderCell>Name</asp:TableHeaderCell>  
                                <asp:TableHeaderCell>Characteristics</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Number?</asp:TableHeaderCell>
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                            </asp:TableHeaderRow>  
                        </asp:Table>
                        <asp:LinkButton ID="addFactor" runat="server" OnClick="addFactor_Click">add</asp:LinkButton>
                    </asp:Panel>

                    <h3>Artefacts</h3>
                    <asp:Panel runat="server" ID="artefactsPanel">
                        <asp:Table ID="artefactsTable" runat="server"></asp:Table>
                        <asp:LinkButton ID="addArtefact" runat="server" OnClick="addArtefact_Click">add</asp:LinkButton>
                    </asp:Panel><br />
    
                    <h3>Documents</h3>
                    <asp:Panel runat="server" ID="documentsPanel">
                        <asp:Table ID="documentsTable" runat="server"></asp:Table>
                        <asp:LinkButton ID="addDocumentBtn" runat="server" OnClick="addDocumentBtn_Click">add</asp:LinkButton>
                    </asp:Panel>

                    <h3>User</h3>
                    <asp:Panel runat="server" ID="usersPanel">
                        <asp:Table ID="usersTable" runat="server">
                            <asp:TableHeaderRow runat="server" ForeColor="Red">  
                                <asp:TableHeaderCell>User</asp:TableHeaderCell>  
                                <asp:TableHeaderCell>Right</asp:TableHeaderCell>
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                            </asp:TableHeaderRow>  
                        </asp:Table>
                        <asp:LinkButton ID="addUser" runat="server" OnClick="addUser_Click">add</asp:LinkButton>
                    </asp:Panel>

            </div>

        </div>

    </div>




    <h3>Criteria</h3>
    <asp:Panel runat="server" ID="criteriaPanel"></asp:Panel>
        <asp:Table ID="criteriaTable" runat="server"></asp:Table>
        <asp:LinkButton ID="addCriteriaButton" runat="server" OnClick="addCriteriaButton_Click">add</asp:LinkButton>
    <asp:HiddenField ID="hiddenCriteria" runat="server" ClientIDMode="Static"/>

    <asp:Panel runat="server" ID="criteriaWeightPanel">
        <h3>Criteria Weight</h3>
        <asp:Table ID="criteriaWeightTable" runat="server"></asp:Table>
    </asp:Panel>

    <asp:HiddenField ID="hiddenCriteriaWeight" runat="server" ClientIDMode="Static"/> 
       
    
    <asp:Panel runat="server" ID="alternativesPanel">
        <h3>Alternatives</h3>
        <asp:Table ID="alternativesTable" runat="server">
            <asp:TableHeaderRow runat="server" ForeColor="Red">  
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>  
                <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                <asp:TableHeaderCell>Reason</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
                <asp:TableHeaderCell>Rating</asp:TableHeaderCell>
            </asp:TableHeaderRow>  
        </asp:Table>
        <asp:LinkButton ID="addAlternativeButton" runat="server" OnClick="addAlternativeButton_Click">add</asp:LinkButton>
    </asp:Panel>
    <asp:HiddenField ID="hiddenAlternatives" runat="server" ClientIDMode="Static"/>
    
    <asp:Panel ID="evaluationPanel" runat="server" Visible="false">
        <h3>Rating</h3>
        <asp:Table ID="evaluationTable" runat="server"></asp:Table>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static"/>

    <asp:Panel ID="decisionPanel" runat="server" Visible="false">
        <h3>Decision</h3>
        <asp:Label ID="decisionLBL" runat="server" Text="Label"></asp:Label></br>
        <asp:TextBox ID="decisionTXT" runat="server" TextMode="MultiLine" Height ="50" Enabled="false"></asp:TextBox>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static"/>
    
    <asp:Panel ID="reviewPanel" runat="server" Visible="false">
        <h3>Reviews</h3>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField3" runat="server" ClientIDMode="Static"/>

    <asp:Button ID="save" runat="server" Text="Save" OnClick="save_Click" Visible="false"/>
    <asp:Button ID="saveNext" runat="server" Text="Save and Next Stage" Visible="false" OnClick="saveNext_Click"/>

</asp:Content>

