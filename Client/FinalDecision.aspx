<%@ Page Title="Final Decision" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="FinalDecision.aspx.cs" Inherits="Client.FinalDecision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <webopt:BundleReference runat="server" Path="~/FinalDecision/css" />  

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Final Decision</h1>

    <hr />

    <div class="row">

        <div class="col-lg-12">

            <div class="row">

                <asp:Panel ID="mainPanel" runat="server">
                    <asp:Table ID="radioTable" runat="server">
                    </asp:Table>
                    <asp:TextBox ID="description_TextBox" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                </asp:Panel>
                <asp:Panel ID="controlPanel" runat="server">
                    <asp:Button ID="save_button" runat="server" Text="Button" OnClick="save_button_Click" />
                </asp:Panel>

            </div>

        </div>

    </div>

</asp:Content>