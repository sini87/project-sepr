<%@ Page Title="Login" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Client.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/Login/css" /> 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="MainContent">
    <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
        <LayoutTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td align="center" id="td_icon" colspan="2">
                                    <a runat="server" href="~/">
                                        <img src="../images/cddss_logo.png" id="logo_cddss" alt="Logo CDDSS" />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <img src="../images/login_bg.png" id="login_bg" alt="login background" />
                                </td>
                            </tr>
                            <tr>
                            <td align="center" class="td_failure" colspan="2">
                                    <asp:ValidationSummary runat="server" ID="validationSummary" 
	                                   DisplayMode="List" ValidationGroup="Login1" 
	                                   HeaderText="<b>Please correct the following fields:</b>" 
	                                   ShowMessageBox="False" ShowSummary="true" />
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_bold" colspan="2">Get your issue solved in a<br />collaborative way.</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="UserName" runat="server" ToolTip="Your Email Address" placeholder="Your Email Address" OnTextChanged="UserName_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="UserName" Display="None"
                                        ErrorMessage="Email is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" ToolTip="Your Password" placeholder="Your Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" Display="None"
                                        ErrorMessage="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" id="td_failure" colspan="2" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" id="td_link" colspan="2">
                                    <a id="noaccount" runat="server" href="~/MyAccount/Register">No account yet?</a>
                                </td>
                            </tr>
                            <tr>
                                <td id="td_button" align="center" colspan="2">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="LOG IN" ValidationGroup="Login1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>

