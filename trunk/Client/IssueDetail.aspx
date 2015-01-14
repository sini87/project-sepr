<%@ Page Title="Issue" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="IssueDetail.aspx.cs" Inherits="Client.IssueDetail" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">

    <webopt:BundleReference runat="server" Path="~/issueDetail/css" /> 

    <script src="../Scripts/rating.js"></script>
    <script src="../Scripts/myrating.js"></script>

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
    
    <div class="row">

        <div class="col-lg-12">
        
            <h1>Issue</h1>

            <hr />

            <asp:Panel runat="server" ID="issueTitle"></asp:Panel>
            <asp:Panel runat="server" ID="rating" />
            <asp:Panel runat="server" ID="tag" class="tag"></asp:Panel>
            <asp:Panel runat="server" ID="status"></asp:Panel>

            <h3>Description</h3>
            <asp:Panel runat="server" ID="description"></asp:Panel>

            <h3>Relation</h3>
            <asp:Panel runat="server" ID="relations"></asp:Panel>

            <hr />

        </div>

    </div>

    <div class="row">

        <asp:Panel runat="server" ID="Panel1" CssClass="col-lg-4 table_stakeholder_factors">
            <h3>Stakeholder</h3>
            <asp:Panel runat="server" ID="stakeholder"></asp:Panel>
        
            <h3>Factors</h3>
            <asp:Panel runat="server" ID="factors"></asp:Panel>

        </asp:Panel>

        <asp:Panel runat="server" ID="Panel2" CssClass="col-lg-4 table_artefacts_documents">
    
            <h3>Artefacts</h3>
            <asp:Panel runat="server" ID="artefacts"></asp:Panel>
    
            <h3>Documents</h3>
            <asp:Panel runat="server" ID="documents"></asp:Panel>

        </asp:Panel>

        <asp:Panel runat="server" ID="Panel3" CssClass="col-lg-4 table_users">

            <h3>User</h3>
            <asp:Panel runat="server" ID="users"></asp:Panel>

        </asp:Panel>

    </div>

    <div class="row">

        <hr />

        <div class="col-lg-12">

            <h3 runat="server" visible="false" id="headingCriteria">Criteria</h3>
            <asp:Panel runat="server" ID="criteriaPanel"></asp:Panel>
            <div id="criteria">
                <a id="btAddCriteria" class="crit">add criteria</a>
                <a id="btRemoveCriteria" class="crit">remove criteria</a>
                <a id="btRemoveAllCriteria" class="crit">remove all</a><br />
            </div>
            <asp:HiddenField ID="hiddenCriteria" runat="server" ClientIDMode="Static"/>
            <hr />

            <h3 runat="server" visible="false" id="headingCriteriaWeight">Criteria Weight</h3>
            <asp:Panel runat="server" ID="criteriaWeightPanel"></asp:Panel>
            <div id="criteriaWeight">
                <a id="btAddCriteriaWeight" class="critWeight">add criteria weight</a>
                <a id="btRemoveCriteriaWeight" class="critWeight">remove criteria weight</a>
                <a id="btRemoveAllCriteriaWeight" class="critWeight">remove all</a><br />
            </div>
            <asp:HiddenField ID="hiddenCriteriaWeight" runat="server" ClientIDMode="Static"/> 
            <hr />
       
            <h3 runat="server" visible="false" id="headingAlternatives">Alternatives</h3>
            <asp:Panel runat="server" ID="alternativesPanel"></asp:Panel>
            <div runat="server" id="alternative">
                <a id="btAddAlt" class="alt">add alternative</a>
                <a id="btRemoveAlt" class="alt">remove alternative</a>
                <a id="btRemoveAllAlt" class="alt">remove all</a><br />
            </div>
            <asp:HiddenField ID="hiddenAlternatives" runat="server" ClientIDMode="Static"/>

            <br />
            <br />

        </div>

    </div>
    
<asp:Button ID="save" runat="server" Text="Save" OnClientClick="return getData(); return false;" ClientIDMode="Static" OnClick="save_Click" Visible="false"/>
<asp:Button ID="saveNext" runat="server" Text="Save and Next Stage" OnClientClick="return getData(); return false;" ClientIDMode="Static" Visible="false" OnClick="saveNext_Click"/>

</asp:Content>

