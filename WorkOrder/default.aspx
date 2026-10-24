<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="default.aspx.vb" Inherits="WorkOrder._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManger1" runat="server" />

    <!-- MENU -->
    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnLookup">
        <div style="padding-left:5px;">
            <asp:Button ID="btnReset" runat="server" Text="Reset Form" /> |      
            <asp:TextBox ID="txtSearchWorkOrder" runat="server"></asp:TextBox>
            <cc1:AutoCompleteExtender ID="txtSearchWorkOrder_AutoCompleteExtender" runat="server" CompletionInterval="500"
                CompletionSetCount="10" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2"
                ServiceMethod="GetWorkOrder" TargetControlID="txtSearchWorkOrder" UseContextKey="false" >
            </cc1:AutoCompleteExtender>
            <asp:Button ID="btnLookup" runat="server" Text="Lookup" /> | 
            <asp:Button ID="btnNew" runat="server" Text="New WorkOrder" />
        </div>
    </asp:Panel>
    <!-- End MENU -->
    <hr />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary"
                DisplayMode="List" HeaderText="&lt;b&gt;Please correct the following errors :&lt;/b&gt;&lt;br/&gt;" />

            <table style="width:auto;">
                <tr>
                    <td>Work Order #</td>
                    <td><asp:TextBox ID="txtWorkOrder" runat="server" ReadOnly="true" BackColor="#ffff77" BorderColor="black"></asp:TextBox></td>
                    <td>Revision Code</td>
                    <td><asp:DropDownList ID="ddlRevisionCode" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="btnSave" runat="server" Text="Save" Enabled="false" /></td>
                    <td><asp:Label ID="lblSuccess" runat="server" CssClass="Success" Visible="false" Text="Saved successfully."></asp:Label></td>
                </tr>
                <tr>
                    <td>Equipment #</td>
                    <td><asp:TextBox ID="txtEquipNum" runat="server"></asp:TextBox></td>
                    <td>PlannedCost</td>
                    <td><asp:TextBox ID="txtPlannedCost" runat="server"></asp:TextBox></td>
                    <td>AFE #</td>
                    <td><asp:TextBox ID="txtAFE" runat="server"></asp:TextBox></td>
                </tr>
            </table>

            <table style="width:auto;">
                <tr>
                    <td>Work Order Status</td>
                    <td><asp:DropDownList ID="ddlWorkOrderStatus" runat="server">
                        <asp:ListItem Selected="True">Entered</asp:ListItem>
                        <asp:ListItem>Ready to Schedule</asp:ListItem>
                        <asp:ListItem>Approved for Planning</asp:ListItem>
                        <asp:ListItem>Completed</asp:ListItem>
                        <asp:ListItem>Cancelled</asp:ListItem>
                        <asp:ListItem>Packet Completed</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Material Status</td>
                    <td><asp:DropDownList ID="ddlMaterialStatus" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Awaiting Materials</asp:ListItem>
                        <asp:ListItem>Ready for pickup</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Break-ins</td>
                    <td><asp:DropDownList ID="ddlBreakins" runat="server">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Priority</td>
                    <td><asp:DropDownList ID="ddlPriority" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>1 -  Very High</asp:ListItem>
                        <asp:ListItem>2 -  High</asp:ListItem>
                        <asp:ListItem>3 -  Medium</asp:ListItem>
                        <asp:ListItem>4 - Low</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Unit</td>
                    <td><asp:DropDownList ID="ddlUnit" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Phenol 2</asp:ListItem>
                        <asp:ListItem>Phenol 3</asp:ListItem>
                        <asp:ListItem>BPA</asp:ListItem>
                        <asp:ListItem>Utilities</asp:ListItem>
                        <asp:ListItem>Logistics</asp:ListItem>
                        <asp:ListItem>General Plant</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Call-out</td>
                    <td><asp:DropDownList ID="ddlCallOut" runat="server">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Work Order Type</td>
                    <td><asp:DropDownList ID="ddlWorkOrderType" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Regular Plant Maintenance</asp:ListItem>
                        <asp:ListItem>Preventative Maintenance</asp:ListItem>
                        <asp:ListItem>Safety</asp:ListItem>
                        <asp:ListItem>Environmental</asp:ListItem>
                        <asp:ListItem>Special Project</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>MOC Required?</td>
                    <td>
                        <asp:DropDownList ID="ddlMOCrequired" runat="server">
                            <asp:ListItem Selected="True">No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:HiddenField ID="hfOrginal_Id" runat="server" />
                    </td>
                </tr>
            </table>

            <hr />

            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" >
                <!-- tab1 -->
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Description of Work" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtWorkDesc" runat="server" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- tab2 -->
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Questionaire" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtQuestionaire" runat="server" Rows="15" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- Tab3 -->
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Operation">
                    <ContentTemplate>
                        <asp:TextBox ID="txtOperation" runat="server" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox> <br />
                        <asp:Button ID="btnAddOperationStep" runat="server" Text="Add Step" /> <br />

                        <asp:GridView ID="gvOperation" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
                             AutoGenerateColumns="false" Width="100%" DataKeyNames="Id" >
                            <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="PanelCaption"  />
                            <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ShowEditButton="true" ControlStyle-ForeColor="Red" ShowDeleteButton="false">
                                        <ControlStyle ForeColor="Red"></ControlStyle></asp:CommandField>

                                    <asp:BoundField DataField="Step" HeaderText="Oper. Step" ReadOnly="true" />

                                    <asp:TemplateField HeaderText="Craft">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCraft" runat="server" SelectedValue='<%# Bind("Craft")%>'
                                                 DataSourceID="SqlDataSource1" DataTextField="CraftCode" DataValueField="CraftCode"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Craft")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Desc" HeaderText="Short Text" ReadOnly="true" />
                                    <asp:ButtonField ButtonType="Link" HeaderText="LT" Text="View" ControlStyle-ForeColor="Green" CommandName="ViewLongText"/>
                                    <asp:BoundField DataField="Work" HeaderText="Work (hrs)" />
                                    <asp:BoundField DataField="PeopleUsed" HeaderText="# Workers" />
                                    <asp:BoundField DataField="Duration" HeaderText="Duration (hrs)" ReadOnly="true" />
                                    <asp:BoundField DataField="PlannedCost" HeaderText="Planned Cost" DataFormatString="{0:C}" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlOperationWorkStatus" runat="server" SelectedValue='<%# Bind("WorkStatus")%>'>
                                                <asp:ListItem Selected="True" />
                                                <asp:ListItem>Completed</asp:ListItem>
                                                <asp:ListItem>On-going</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("WorkStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                        </asp:GridView>

                        <div style="text-align:right;padding-right:15px;">
                            Total Planned Cost:
                            <asp:TextBox ID="txtTotalPlannedCost" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- Tab4 -->
                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="BOM" >
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnEquipSearch">
                            Equipment <asp:TextBox ID="txtBOMEquipment" runat="server"></asp:TextBox>
                            <asp:Button ID="btnEquipSearch" runat="server" Text="Search" /> 
                            <asp:Button ID="btnTransferBOM" runat="server" Text="Transfer to Material" />
                            <asp:Label ID="lblBOMSuccess" runat="server" CssClass="Success" Visible="false" Text="Item successfully transfered."></asp:Label> 
                            <br />
                            <asp:GridView ID="gvBOM" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
                                 AutoGenerateColumns="false" AllowPaging="true" PageSize="25" >
                                <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="PanelCaption"  />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Equipment" HeaderText="Equipment" />
                                    <asp:BoundField DataField="Component" HeaderText="Component" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Reqmt Qty" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- Tab5 -->
                <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Storeroom">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnMatSearch">
                            Material <asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox>
                            <asp:Button ID="btnMatSearch" runat="server" Text="Search" /> 
                            <asp:Button ID="btnTransferStoreRm" runat="server" Text="Transfer to Material" /> 
                            <asp:Label ID="lblStoreRmSuccess" runat="server" CssClass="Success" Visible="false" Text="Item successfully transfered."></asp:Label>
                            <br />

                            <asp:GridView ID="gvStoreroom" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
                                 AutoGenerateColumns="false" AllowPaging="true" PageSize="25" DataKeyNames="Id">
                                <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="PanelCaption"  />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Material" HeaderText="Store #" ReadOnly="true" />
                                    <asp:BoundField DataField="ShortText" HeaderText="Description" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="StorageBin" HeaderText="Location" />
                                    <asp:BoundField DataField="MaterialGrp" HeaderText="Mat Grp" />
                                    <asp:BoundField DataField="UnrestrictedStock" HeaderText="On-Hand" />

                                    <asp:TemplateField HeaderText="Reserved">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("Reserved")%>' OnClick="DisplayDetails"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Available" HeaderText="Available" />

                                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="false" ControlStyle-ForeColor="Blue"
                                         Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- Tab6 -->
                <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Material">
                    <ContentTemplate>
                        <asp:GridView ID="gvMaterial" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
                             AutoGenerateColumns="false" DataKeyNames="Id" >
                            <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="PanelCaption"  />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowEditButton="true" ControlStyle-ForeColor="Red">
                                    <ControlStyle ForeColor="Red"></ControlStyle></asp:CommandField>

                                <asp:BoundField DataField="Equipment" HeaderText="Equip #" ReadOnly="true" />
                                <asp:BoundField DataField="Material" HeaderText="Component" ReadOnly="true" />
                                <asp:BoundField DataField="ShortText" HeaderText="Description" ReadOnly="true" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="StorageBin" HeaderText="Location" ReadOnly="true" />
                                <asp:BoundField DataField="Quantity" HeaderText="QTY" />
                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                <asp:BoundField DataField="MaterialGrp" HeaderText="Mat Grp" />
                                <asp:BoundField DataField="Price" HeaderText="Price"  DataFormatString="{0:C}" ReadOnly="true" />
                                <asp:BoundField DataField="SubTotal" HeaderText="Sub Total"  DataFormatString="{0:C}" />

                                <asp:TemplateField HeaderText="Status" >
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlOperationWorkStatus" runat="server" SelectedValue='<%# Bind("Status")%>'>
                                                <asp:ListItem Selected="True">Waiting</asp:ListItem>
                                                <asp:ListItem>Delivered</asp:ListItem>
                                                <asp:ListItem>Picked Up</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:BoundField DataField="Comment" HeaderText="Comment" />

                                <asp:CommandField ShowDeleteButton="true" ControlStyle-ForeColor="Red" Visible="false">
                                    <ControlStyle ForeColor="Red"></ControlStyle></asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </cc1:TabPanel>
                <!-- Tab7 -->
                <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="Actual Labor">
                    <ContentTemplate>
                        <asp:Button ID="btnAddLabor" runat="server" Text="Add Labor" /> <br />

                        <asp:GridView ID="gvActualLabor" runat="server" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"
                             AutoGenerateColumns="false" DataKeyNames="Id" >
                            <RowStyle BorderColor="#C2D4DA" BackColor="#EDF3F4" HorizontalAlign="Center" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="PanelCaption"  />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowEditButton="true" ControlStyle-ForeColor="Red">
                                    <ControlStyle ForeColor="Red"></ControlStyle></asp:CommandField>

                                <asp:BoundField DataField="WorkOrderNumber" HeaderText="Work Order" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Date">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDate" runat="server" Enabled="false" Text='<%# Bind("DateWorked", "{0:M/d/yyyy}")%>' ></asp:TextBox>
                                        <asp:ImageButton ID="ibtnCalPickupDate" runat="server" ImageUrl="~/img/calendar.png"
                                            CausesValidation="False" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            PopupButtonID="ibtnCalPickupDate" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateWorked", "{0:M/d/yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Employee" HeaderText="Employee #" />
                                
                                <asp:TemplateField HeaderText="Craft">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCraft" runat="server" SelectedValue='<%# Bind("Craft")%>'
                                                 DataSourceID="SqlDataSource1" DataTextField="CraftCode" DataValueField="CraftCode"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Craft")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:BoundField DataField="RegHrs" HeaderText="Regular" />
                                <asp:BoundField DataField="OtHrs" HeaderText="Over Time" />
                                <asp:BoundField DataField="DtHrs" HeaderText="Double Time" />

                                <asp:CommandField ShowDeleteButton="true" ControlStyle-ForeColor="Red" Visible="false">
                                    <ControlStyle ForeColor="Red"></ControlStyle></asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </cc1:TabPanel>
                
            </cc1:TabContainer>


            <!-- Modal Panel -->
            <asp:ImageButton ID="ImageButton1" ClientIDMode="Static" runat="server" ImageUrl="~/img/spacer.png" />
            <cc1:ModalPopupExtender ID="mpe" runat="server" TargetControlId="ImageButton1" ClientIDMode="Static"
                PopupControlID="ModalPanel" OkControlID="OKButton" />

            <asp:Panel ID="ModalPanel" runat="server" ClientIDMode="Static" CssClass="layerSetting" style="width:100%; display:none; z-index:-1;">
                <div style="width:550px;position:fixed;left:50%;margin-left:-275px; margin-top:100px; text-align:right;">
                    <asp:Button ID="OKButton" runat="server" Text="Close" ClientIDMode="Static" CssClass="button" /> 
                    <br />
                    <asp:TextBox ID="txtFullDesc" runat="server" TextMode="MultiLine" Rows="15" Width="100%"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnSaveDesc" runat="server" Text="Save" />
                    <asp:HiddenField ID="HF1" runat="server" />
                </div> 
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT DISTINCT CraftCode FROM Crafts ORDER BY CraftCode ASC;"></asp:SqlDataSource>
</asp:Content>
