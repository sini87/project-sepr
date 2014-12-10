<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Client.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <webopt:BundleReference runat="server" Path="~/Profile/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Profile</h1>

    <hr />

    <div class="row">

        <div class="col-lg-6">

            <div class="row">

                <div class="col-lg-5 vcenter textcenter">
                    <asp:Image ID="profilePicture" runat="server" src="../images/avatar_woman_big.png" alt="profile pic"/>
                    <asp:Label ID="ProfileAcronym" runat="server"></asp:Label>
                </div>
        
                <div class="col-lg-6 vcenter">  
                      
                    <div class="row">
                        <asp:Label ID="name" runat="server"></asp:Label>
                    </div>

                    <div class="row">
                        <hr />
                    </div>

                    <div class="row">
                        <asp:Label ID="email" runat="server"></asp:Label>
                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-lg-12">   
                    <h2>Owned by me:</h2>
                    <asp:Table ID="ownedByMeTable" runat="server">
                        <asp:TableHeaderRow>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </div>

            </div>

        </div>

        <div class="col-lg-6"> 

            <div class="row">

                <div id="info">User Info</div>

                <label>Firstname</label><br />
                <asp:TextBox ID="firstname" runat="server"></asp:TextBox><br />

                <label>Lastname</label><br />
                <asp:TextBox ID="lastname" runat="server"></asp:TextBox><br />

                <label>Email</label><br />
                <asp:TextBox ID="emailTxt" runat="server" OnTextChanged="emailTxt_TextChanged"></asp:TextBox><br />

            </div>

            <div class="row changePassword">

                <a id="changePassword">Change Password</a>

            </div>

            <div class="row text-right margintop">
                <asp:Button ID="saveProfile" runat="server" text="Save & Update" OnClick="saveProfile_Click"/>
            </div>

        </div>

    </div>
</asp:Content>
