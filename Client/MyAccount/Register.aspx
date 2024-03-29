﻿<%@ Page Title="Sign up" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Client.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/Register/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:CreateUserWizard ID="CreateUserWizard" runat="server" OnActiveStepChanged="ActiveStepChanged" OnCreatingUser="CreatingUser" OnCreatedUser="CreateUserWizard_CreatedUser" OnFinishButtonClick="CreateUserWizard_FinishButtonClick" OnContinueButtonClick="CreateUserWizard_ContinueButtonClick" OnNextButtonClick="CreateUserWizard_StepNextButtonClick" ActiveStepIndex="1">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="CreateUserWizard_SignUp">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center" id="td_icon" colspan="2">
                                <a runat="server" href="~/">
                                    <img src="../images/cddss_logo.png" id="logo_cddss" alt="Logo CDDSS" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center"colspan="2">
                                <img src="../images/login_bg.png" id="login_bg" alt="login background" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="td_failure" colspan="2">
                                <asp:ValidationSummary runat="server" ID="validationSummary" 
	                               DisplayMode="List" ValidationGroup="CreateUserWizard" 
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
                                <asp:TextBox ID="Email" runat="server" ToolTip="Your Email Address" placeholder="Your Email Address" OnTextChanged="Email_TextChanged" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" Display="None"
                                        ErrorMessage="Email is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" ToolTip="Create a Password" placeholder="Create a Password" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" Display="None"
                                        ErrorMessage="Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" ToolTip="Confirm your Password" placeholder="Confirm your Password"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPassword" Display="None"
                                        ErrorMessage="Password confirmation is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Visible="False">User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" Visible="False" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="None" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" id="td_link" colspan="2">
                                <a id="noaccount" runat="server" href="~/MyAccount/Login">Already have an account?</a>
                            </td>
                        </tr>

                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:WizardStep ID="CreateUserWizard_EditUser" runat="server" Title="Edit User" StepType="Step">

                    <table>
                        <tr>
                            <td align="center" id="td_icon">
                                <a runat="server" href="~/" onserverclick="HtmlAnchor_Click">
                                    <img src="../images/cddss_logo.png" id="logo_cddss" alt="Logo CDDSS" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src="../images/login_bg.png" id="login_bg" alt="login background" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="td_failure">
                                <asp:ValidationSummary runat="server" ID="validationSummary" 
	                                DisplayMode="List" ValidationGroup="CreateUserWizard" 
	                                HeaderText="<b>Please correct the following fields:</b>" 
	                                ShowMessageBox="False" ShowSummary="true" />
                                <asp:Literal ID="ErrorMessage2" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="td_bold">Edit your personal data.<br />It's safe with us.</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="FirstName" runat="server" ToolTip="Your First Name" placeholder="Your First Name" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName" ErrorMessage="First Name is required." ToolTip="First Name is required." ValidationGroup="CreateUserWizard" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="LastName" runat="server" ToolTip="Your Last Name" placeholder="Your Last Name" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName" ErrorMessage="Last Name is required." ToolTip="Last Name is required." ValidationGroup="CreateUserWizard1" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="Question2" runat="server" Visible="True" ToolTip="Your Security Question" placeholder="Your Security Question" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired2" runat="server" ControlToValidate="Question2" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="CreateUserWizard1" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="Answer2" runat="server" Visible="True" ToolTip="Your Security Answer" placeholder="Your Security Answer" ValidateRequestMode="Enabled" ValidationGroup="CreateUserWizard1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired2" runat="server" ControlToValidate="Answer2" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1" Display="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

            </asp:WizardStep>
            <asp:CompleteWizardStep runat="server" ID="CreateUserWizard_Complete">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center" id="td_icon">
                                <a runat="server" href="~/" onserverclick="HtmlAnchor_Click2">
                                    <img src="../images/cddss_logo.png" id="logo_cddss" alt="Logo CDDSS" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src="../images/login_bg.png" id="login_bg" alt="login background" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="td_failure">
                                <asp:ValidationSummary runat="server" ID="validationSummary" 
	                                DisplayMode="List" ValidationGroup="CreateUserWizard1" 
	                                HeaderText="<b>Please correct the following fields:</b>" 
	                                ShowMessageBox="False" ShowSummary="true" />
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="td_bold">Your account has been created.</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="ContinueButton" runat="server" CommandName="MoveComplete" Text="FINISH" OnClick="CreateUserWizard_ContinueButtonClick" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>     
</asp:Content>
