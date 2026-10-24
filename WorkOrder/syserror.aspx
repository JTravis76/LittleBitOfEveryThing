<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="syserror.aspx.vb" Inherits="WorkOrder.syserror" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:15px;">
        <h2>System Error</h2>
        <p>
            We are unable to process the current request due to an unexpected error. The system
            administrator has been notified of the error.
        </p>
        <p>
            Please click the "Back" button on your browser, and try again.
            If the error persists, please try again later.
        </p>
        <p>We apologize for the inconvenience.</p>
    </div>
</asp:Content>
