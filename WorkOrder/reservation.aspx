<%@ Page Title="Reservation Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="reservation.aspx.vb" Inherits="WorkOrder.reservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvReservation" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
        AutoGenerateColumns="false" >
            <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle CssClass="PanelCaption"  />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order" />
                <asp:BoundField DataField="Material" HeaderText="Material" />
                <asp:BoundField DataField="ShortText" HeaderText="Description" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
    </asp:GridView>
</asp:Content>
