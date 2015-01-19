<%@ Page Title="Issue" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="IssueDetail.aspx.cs" Inherits="Client.IssueDetail" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">

    <webopt:BundleReference runat="server" Path="~/issueDetail/css" /> 

    <script src="../Scripts/AddCriteria.js"></script>
    <script src="../Scripts/AddCriteriaWeight.js"></script>
    <script src="../Scripts/AddAlternative.js"></script>

    <script src="../Scripts/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript"> 
       
        $(document).ready(function () {
            getData();
        });
        
        function getData() {
            $('#save').click(function () {
                var hiddenValues = "";
                $(".getCriteria").each(function () {
                    hiddenValues += $(this).val() + ";";
                });
                $("#hiddenCriteria").val(hiddenValues);// var test = $("#hiddenCriteria").val(); alert(test);

                hiddenValues = "";
                $(".getCriteriaWeight").each(function () {
                    hiddenValues += $(this).val() + ";";
                });
                $("#hiddenCriteriaWeight").val(hiddenValues);// var test = $("#hiddenCriteriaWeight").val(); alert(test);

                hiddenValues = "";
                $(".getAlternative").each(function () {
                    hiddenValues += $(this).val() + ";";
                });
                $("#hiddenAlternatives").val(hiddenValues);// var test = $("#hiddenAlternatives").val(); alert(test);

            });
        }
                
    </script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <h1>Issue</h1>

    <hr />

    <h3>Title</h3>

    <asp:Panel runat="server" ID="issueTitle"></asp:Panel><br />
    <h3>Rating</h3>
    <asp:Panel runat="server" ID="rating" /><br />
    <h3>Tags</h3>
    <asp:Panel runat="server" ID="tag" class="tag"></asp:Panel><br />
    <h3>Status</h3>
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
    <asp:Panel runat="server" ID="users"></asp:Panel>
    
    <hr />

    <h3>Criteria</h3>
    <asp:Panel runat="server" ID="criteriaPanel"></asp:Panel>
    <div id="criteria">
        <a id="btAddCriteria" class="crit">add criteria</a>
        <a id="btRemoveCriteria" class="crit">remove criteria</a>
        <a id="btRemoveAllCriteria" class="crit">remove all</a><br />
    </div>
    <asp:HiddenField ID="hiddenCriteria" runat="server" ClientIDMode="Static"/>
    <hr />

    <h3>Criteria Weight</h3>
    <asp:Panel runat="server" ID="criteriaWeightPanel"></asp:Panel>
    <%--<div id="criteriaWeight">
        <a id="btAddCriteriaWeight" class="critWeight">add criteria weight</a>
        <a id="btRemoveCriteriaWeight" class="critWeight">remove criteria weight</a>
        <a id="btRemoveAllCriteriaWeight" class="critWeight">remove all</a><br />
    </div>--%>
    <asp:HiddenField ID="hiddenCriteriaWeight" runat="server" ClientIDMode="Static"/> 
    <hr />
       
    <h3>Alternatives</h3>
    <asp:Panel runat="server" ID="alternativesPanel"></asp:Panel>
    <div id="alternative">
        <a id="btAddAlt" class="alt">add alternative</a>
        <a id="btRemoveAlt" class="alt">remove alternative</a>
        <a id="btRemoveAllAlt" class="alt">remove all</a><br />
    </div>
    <asp:HiddenField ID="hiddenAlternatives" runat="server" ClientIDMode="Static"/>

    <br />
    <br />
    
<asp:Button ID="save" runat="server" Text="Save" OnClientClick="return getData(); return false;" ClientIDMode="Static" OnClick="save_Click" Visible="false"/>
<asp:Button ID="saveNext" runat="server" Text="Save and Next Stage" OnClientClick="return getData(); return false;" ClientIDMode="Static" Visible="false" OnClick="saveNext_Click"/>

</asp:Content>

