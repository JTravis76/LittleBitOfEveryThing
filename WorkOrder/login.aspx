<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="login.aspx.vb" Inherits="WorkOrder.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <!-- class="col-md-4" -->
    <div  style="width: 320px; margin-left: auto; margin-right: auto; padding: 10px 10px 10px 10px" >
        <table style="border-collapse:collapse;" class="Panel">
            <tr>
                <td class="PanelCaption">
                    Login
                </td>
            </tr>
            <tr>
                <td>
                   
                    <table style="border-collapse: collapse; border:0; width:100%;">
                        <tr>
                            <td style="text-align:center;">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False">Your session has expired. Please login again.</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 317px; border:none;border-collapse: collapse;">
                                    <tr>
                                        <td style="text-align:right;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 29px;text-align:right;">
                                            Username:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsername" runat="server" Columns="20" 
                                                CssClass="textbox" MaxLength="20"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vUsername" runat="server" 
                                                ControlToValidate="txtUsername" ForeColor="Red">Required</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 29px;text-align:right;">
                                            Password:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                                Columns="21" MaxLength="20"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vPassword" runat="server" 
                                                ControlToValidate="txtPassword" ForeColor="Red">Required</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="color: red; height: 21px; text-align:center;">
                                            <asp:Label ID="lblFailureText" runat="server" ForeColor="Red" 
                                                Text="Invalid User name / Password. Please try again." Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:center;">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:center;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
