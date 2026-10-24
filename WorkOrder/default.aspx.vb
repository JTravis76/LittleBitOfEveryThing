Public Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Build Revision Code list
            ddlRevisionCode.Items.Add(New ListItem("", ""))
            ddlRevisionCode.Items.Add(New ListItem("BPA TA", "BPA TA"))
            ddlRevisionCode.Items.Add(New ListItem("PH1 TA", "PH1 TA"))
            ddlRevisionCode.Items.Add(New ListItem("PH2 TA", "PH2 TA"))
            ddlRevisionCode.Items.Add(New ListItem("PH3 TA", "PH3 TA"))
            For z = 1 To 52
                ddlRevisionCode.Items.Add(New ListItem("Week " & z.ToString & " - 2016", "Week " & z.ToString & " - 2016"))
            Next
            For z = 1 To 52
                ddlRevisionCode.Items.Add(New ListItem("Week " & z.ToString & " - 2017", "Week " & z.ToString & " - 2017"))
            Next
            ddlRevisionCode.SelectedIndex = 0

            btnReset_Click(sender, e)
        End If

        If CInt(Request.QueryString("delete")) = 1 Then
            gvMaterial.Columns(12).Visible = True
            gvActualLabor.Columns(8).Visible = True
        End If

        If Session("IsStoreroom") Then
            gvStoreroom.Columns(8).Visible = True
        End If

        If Session("WorkOrder#") = "" Then
            btnTransferBOM.Enabled = False
            btnTransferStoreRm.Enabled = False
        End If
    End Sub

#Region "Button Clicks"

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        hfOrginal_Id.Value = ""
        txtSearchWorkOrder.Text = ""
        txtWorkOrder.Text = ""
        ddlRevisionCode.SelectedIndex = 0
        txtEquipNum.Text = ""
        txtPlannedCost.Text = ""
        ddlWorkOrderStatus.SelectedIndex = 0
        ddlMaterialStatus.SelectedIndex = 0
        ddlBreakins.SelectedIndex = 0
        ddlPriority.SelectedIndex = 0
        ddlUnit.SelectedIndex = 0
        ddlCallOut.SelectedIndex = 0
        ddlWorkOrderType.SelectedIndex = 0
        ddlMOCrequired.SelectedIndex = 0
        txtWorkDesc.Text = ""
        txtQuestionaire.Text = "WORK ORDER TAG NUMBER" &
            vbCrLf & vbCrLf & "NAME OF PERSON ENTERING NOTIFICATION?" &
            vbCrLf & vbCrLf & "LOCATION OF EQUIPMENT, BE SPECIFIC?" &
            vbCrLf & vbCrLf & "WHAT IS THE PROBLEM?" &
            vbCrLf & vbCrLf & "WHAT TROUBLESHOOTING HAS BEEN DONE?" &
            vbCrLf & vbCrLf & "WHAT NEEDS TO BE DONE TO FIX THE PROBLEM?" &
            vbCrLf & vbCrLf & "IF THIS WO IS A #1 OR #2, EXPLAIN?" &
            vbCrLf & vbCrLf & "WHAT CAN HAPPEN IF THIS WORK IS NOT COMPLETED?" &
            vbCrLf & vbCrLf & "IF A LEAK, WHAT IS LEAKING, WHERE TO, HOW MUCH?" &
            vbCrLf & vbCrLf & "IS THERE AN ENVIRONMENTAL ISSUE?" &
            vbCrLf & vbCrLf & "WHAT OPERATOR POSITION PERMITS THIS JOB?"

        txtOperation.Text = ""
        txtAFE.Text = ""

        lblBOMSuccess.Visible = False
        lblStoreRmSuccess.Visible = False
        lblSuccess.Visible = False

        Session("WorkOrder#") = ""
        btnSave.Enabled = False
        btnNew.Enabled = True

        btnTransferBOM.Enabled = False
        btnTransferStoreRm.Enabled = False

        txtMaterial.Text = ""
        txtBOMEquipment.Text = ""

        Dim dt As New DataTable
        gvBOM.DataSource = dt
        gvBOM.DataBind()
        gvMaterial.DataSource = dt
        gvMaterial.DataBind()
        gvOperation.DataSource = dt
        gvOperation.DataBind()
        gvStoreroom.DataSource = dt
        gvStoreroom.DataBind()
        gvActualLabor.DataSource = dt
        gvActualLabor.DataBind()
    End Sub

    Private Sub btnLookup_Click(sender As Object, e As EventArgs) Handles btnLookup.Click
        If txtSearchWorkOrder.Text = "" Then
            Dim err As New CustomValidator
            err.IsValid = False
            err.ErrorMessage = "Please enter a valid work order to lookup."
            Page.Validators.Add(err)
            Exit Sub
        Else
            FillDataByWorkOrder()
            FillOperationData()
            FillMaterialData()
            FillActualLabor()

            CalculatePlannedCostTotal()

            btnNew.Enabled = False
            btnSave.Enabled = True

            lblBOMSuccess.Visible = False
            lblStoreRmSuccess.Visible = False
            lblSuccess.Visible = False

            btnTransferBOM.Enabled = True
            btnTransferStoreRm.Enabled = True
        End If
    End Sub

    Private Sub btnTransferBOM_Click(sender As Object, e As EventArgs) Handles btnTransferBOM.Click
        'If view/edit a active Workorder, loop through GridView and if item is selected,
        'query DB based on Equipment and Component values then insert to MaterialList table
        If Not Session("WorkOrder#") = "" Then

            For Each RW As GridViewRow In gvBOM.Rows
                Dim cb As CheckBox = RW.FindControl("CheckBox1")
                If cb.Checked Then
                    '#query Inventory and copy to Material List
                    Dim ds As New dsWorkOrder
                    Dim da As New dsWorkOrderTableAdapters.InventoryTableAdapter
                    da.FillByMaterial(ds.Inventory, RW.Cells(2).Text)

                    Dim daMat As New dsWorkOrderTableAdapters.MaterialListTableAdapter
                    daMat.Insert(Session("WorkOrder#").ToString,
                                 RW.Cells(1).Text,
                                 ds.Inventory.Rows(0)("ShortText"),
                                 ds.Inventory.Rows(0)("StorageBin"),
                                 0,
                                 ds.Inventory.Rows(0)("Price"),
                                 0,
                                 ds.Inventory.Rows(0)("Material"),
                                 Now,
                                 Session("UserName").ToString,
                                 ds.Inventory.Rows(0)("UOM"),
                                 ds.Inventory.Rows(0)("MaterialGrp"),
                                 "Waiting",
                                 "")

                    'display success msg
                    lblBOMSuccess.Visible = True
                End If
            Next
            'reload Material data
            FillMaterialData()
        Else
            Dim err As New CustomValidator
            err.IsValid = False
            err.ErrorMessage = "You must select a work order first."
            Page.Validators.Add(err)
            Exit Sub
        End If
    End Sub

    Private Sub btnMatSearch_Click(sender As Object, e As EventArgs) Handles btnMatSearch.Click
        FillStoreRmData()
        lblStoreRmSuccess.Visible = False
        lblBOMSuccess.Visible = False
    End Sub

    Private Sub btnEquipSearch_Click(sender As Object, e As EventArgs) Handles btnEquipSearch.Click
        FillBOM_Data()
        lblStoreRmSuccess.Visible = False
        lblBOMSuccess.Visible = False
    End Sub

    Private Sub btnTransferStoreRm_Click(sender As Object, e As EventArgs) Handles btnTransferStoreRm.Click
        'If view/edit a active Workorder, loop through GridView and if item is selected,
        'query DB based on Equipment and Component values then insert to MaterialList table
        If Not Session("WorkOrder#") = "" Then

            For Each RW As GridViewRow In gvStoreroom.Rows
                Dim cb As CheckBox = RW.FindControl("CheckBox1")
                If cb.Checked Then

                    Dim ds As New dsWorkOrder
                    Dim da As New dsWorkOrderTableAdapters.InventoryTableAdapter
                    da.FillByMaterial(ds.Inventory, RW.Cells(1).Text)

                    Dim daMat As New dsWorkOrderTableAdapters.MaterialListTableAdapter
                    daMat.Insert(Session("WorkOrder#").ToString,
                                 "",
                                 ds.Inventory.Rows(0)("ShortText"),
                                 ds.Inventory.Rows(0)("StorageBin"),
                                 0,
                                 ds.Inventory.Rows(0)("Price"),
                                 0,
                                 ds.Inventory.Rows(0)("Material"),
                                 Now,
                                 Session("UserName").ToString,
                                 ds.Inventory.Rows(0)("UOM"),
                                 ds.Inventory.Rows(0)("MaterialGrp"),
                                 "Waiting",
                                 "")

                    'display success msg
                    lblStoreRmSuccess.Visible = True
                End If
            Next
            'reload Material/Inventory data
            FillMaterialData()
            FillStoreRmData()

        End If
    End Sub

    Private Sub btnAddOperationStep_Click(sender As Object, e As EventArgs) Handles btnAddOperationStep.Click
        If Not Session("WorkOrder#") = "" Then
            Dim ds As New dsWorkOrder
            Dim da As New dsWorkOrderTableAdapters.OperationsTableAdapter
            Dim LastStep As Integer = 0

            'add 10 to the last step to set the next operation step value
            If da.LastStepByWorkOrderNumber(Session("WorkOrder#").ToString) > 0 Then
                LastStep = da.LastStepByWorkOrderNumber(Session("WorkOrder#").ToString) + 10
            Else
                LastStep = 10
            End If

            da.Insert(Session("WorkOrder#").ToString, LastStep, "", 0, 0, 0, "", Now, Session("UserName").ToString, 0)

            da.Dispose()
            ds.Dispose()

            FillOperationData()
        End If
    End Sub

    Private Sub btnSaveDesc_Click(sender As Object, e As EventArgs) Handles btnSaveDesc.Click
        Dim temp As String = txtFullDesc.Text.Trim
        temp = temp.Replace("'", "''") '<- to avoid SQL issues related to single quote

        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim cmd As SqlClient.SqlCommand = Con.CreateCommand()
            cmd.CommandText = "UPDATE [dbo].[Operations] SET [Desc]='" & temp & "' WHERE id=" & HF1.Value.ToString
            cmd.ExecuteNonQuery()

            Con.Close()
            mpe.Hide()
            FillOperationData()
        End Using
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim err As New CustomValidator

        If ddlWorkOrderType.SelectedValue = "" Then
            err.IsValid = False
            err.ErrorMessage = "Please select 'Work Order Type' before starting a new work order."
            Page.Validators.Add(err)
            Exit Sub
        End If

        Dim a As Boolean = IIf(ddlBreakins.SelectedValue = "Yes", True, False)
        Dim b As Boolean = IIf(ddlCallOut.SelectedValue = "Yes", True, False)
        Dim c As Boolean = IIf(ddlMOCrequired.SelectedValue = "Yes", True, False)
        txtPlannedCost.Text = "0.0"

        'Insert leaving WorkOrderNumber blank, return ID 
        Dim ta As New dsWorkOrderTableAdapters.OrdersTableAdapter
        Dim id As Integer = ta.InsertAndReturnOrders("",
            txtEquipNum.Text, ddlUnit.SelectedValue, ddlWorkOrderStatus.SelectedValue, ddlMaterialStatus.SelectedValue,
            ddlPriority.SelectedValue, a, b, ddlWorkOrderType.SelectedValue,
            txtWorkDesc.Text, txtQuestionaire.Text, txtOperation.Text, txtPlannedCost.Text, ddlRevisionCode.SelectedValue, Now.ToLongTimeString,
            Session("UserName").ToString, c, txtAFE.Text)

        hfOrginal_Id.Value = id

        'Format ID
        Dim OrderNum As String = id 'String.Format("{0:00000000}", id)

        'Assign Prefix based on Selected work order type
        If ddlWorkOrderType.SelectedValue = "Preventative Maintenance" Then
            OrderNum = "PM" & OrderNum
        Else
            OrderNum = "A" & OrderNum
        End If

        'update DB with proper WorkOrderNumber
        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim cmd As SqlClient.SqlCommand = Con.CreateCommand()
            cmd.CommandText = "UPDATE [dbo].[Orders] SET [WorkOrderNumber]='" & OrderNum & "' WHERE id=" & id.ToString
            cmd.ExecuteNonQuery()

            Con.Close()

            Session("WorkOrder#") = OrderNum
            txtWorkOrder.Text = OrderNum
            btnSave.Enabled = True
            btnNew.Enabled = False

            btnTransferBOM.Enabled = True
            btnTransferStoreRm.Enabled = True

        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Session("WorkOrder#") = "" Then
            Try
                Dim ds As New dsWorkOrder
                Dim da As New dsWorkOrderTableAdapters.OrdersTableAdapter

                Dim a As Boolean = IIf(ddlBreakins.SelectedValue = "Yes", True, False)
                Dim b As Boolean = IIf(ddlCallOut.SelectedValue = "Yes", True, False)
                Dim c As Boolean = IIf(ddlMOCrequired.SelectedValue = "Yes", True, False)

                Dim d As Double = 0
                If txtPlannedCost.Text.Length > 0 Then
                    d = CDbl(String.Format("{0:0.00}", txtPlannedCost.Text))
                End If

                da.Update(txtWorkOrder.Text,
                              txtEquipNum.Text,
                              ddlUnit.SelectedValue,
                              ddlWorkOrderStatus.SelectedValue,
                              ddlMaterialStatus.SelectedValue,
                              ddlPriority.SelectedValue,
                              a,
                              b,
                              ddlWorkOrderType.SelectedValue,
                              txtWorkDesc.Text,
                              txtQuestionaire.Text,
                              txtOperation.Text,
                              d,
                              ddlRevisionCode.SelectedValue,
                              Now,
                              Session("UserName").ToString,
                              c,
                              txtAFE.Text,
                              hfOrginal_Id.Value.ToString)

                da.Dispose()
                ds.Dispose()
                lblSuccess.Visible = True
            Catch ex As Exception
                Dim err As New CustomValidator
                err.IsValid = False
                err.ErrorMessage = ex.Message
                Page.Validators.Add(err)
                Exit Sub
            End Try

        End If
    End Sub

    Private Sub btnAddLabor_Click(sender As Object, e As EventArgs) Handles btnAddLabor.Click
        If Not Session("WorkOrder#") = "" Then
            Dim ds As New dsWorkOrder
            Dim da As New dsWorkOrderTableAdapters.ActualLaborTableAdapter
            da.FillWorkOrderNumber(ds.ActualLabor, Session("WorkOrder#").ToString)

            da.Insert(Session("WorkOrder#").ToString, Now, "", "", Now, Session("UserName").ToString, 0, 0, 0)

            FillActualLabor()
        End If
    End Sub

#End Region

#Region "Fill Data Methods"

    Private Sub FillDataByWorkOrder()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.OrdersTableAdapter
        da.FillByWorkOrderNumber(ds.Orders, txtSearchWorkOrder.Text)

        If ds.Orders.Rows.Count > 0 Then
            hfOrginal_Id.Value = ds.Orders.Rows(0)("Id")
            txtWorkOrder.Text = ds.Orders.Rows(0)("WorkOrderNumber")
            ddlRevisionCode.SelectedValue = ds.Orders.Rows(0)("RevisionCode")
            txtEquipNum.Text = ds.Orders.Rows(0)("Equipment")
            txtPlannedCost.Text = String.Format("{0:0.00}", ds.Orders.Rows(0)("PlannedCost"))

            ddlWorkOrderStatus.SelectedValue = ds.Orders.Rows(0)("Status")
            ddlMaterialStatus.SelectedValue = ds.Orders.Rows(0)("MaterialStatus")
            ddlBreakins.SelectedValue = IIf(ds.Orders.Rows(0)("BreakIn"), "Yes", "No")
            ddlPriority.SelectedValue = ds.Orders.Rows(0)("Priority")
            ddlUnit.SelectedValue = ds.Orders.Rows(0)("Unit")
            ddlCallOut.SelectedValue = IIf(ds.Orders.Rows(0)("Callout"), "Yes", "No")
            ddlWorkOrderType.SelectedValue = ds.Orders.Rows(0)("WorkType")
            ddlMOCrequired.SelectedValue = IIf(ds.Orders.Rows(0)("MOCRequired"), "Yes", "No")

            txtWorkDesc.Text = ds.Orders.Rows(0)("Description")
            txtQuestionaire.Text = ds.Orders.Rows(0)("Questionaire")
            txtOperation.Text = ds.Orders.Rows(0)("Operation")

            Session("WorkOrder#") = txtSearchWorkOrder.Text
        Else
            Dim err As New CustomValidator
            err.IsValid = False
            err.ErrorMessage = "No data found for requested work order number."
            Page.Validators.Add(err)
            Exit Sub
        End If

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub FillMaterialData()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.MaterialListTableAdapter
        da.FillbyWorkOrderNumber(ds.MaterialList, txtSearchWorkOrder.Text)
        gvMaterial.DataSource = ds.MaterialList
        gvMaterial.DataBind()

        For a = 0 To gvMaterial.Rows.Count - 1
            'Flag inventory as deleted item
            If gvMaterial.Rows(a).Cells.Item(7).Text.ToUpper = "ZOB" Then
                gvMaterial.Rows(a).BackColor = Drawing.Color.LightPink
                gvMaterial.Rows.Item(a).ForeColor = Drawing.Color.Red
            End If

            'flag inventory as Replaceable item
            If gvMaterial.Rows(a).Cells.Item(7).Text.ToUpper = "ZRP" Then
                gvMaterial.Rows(a).BackColor = Drawing.Color.LightBlue
                gvMaterial.Rows.Item(a).ForeColor = Drawing.Color.Blue
            End If
        Next

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub FillStoreRmData()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.InventoryTableAdapter
        da.FillByMaterial(ds.Inventory, txtMaterial.Text)

        'Create column- Reserved, & Available
        ds.Inventory.Columns.Add(New DataColumn("Reserved", System.Type.GetType("System.Double")))
        ds.Inventory.Columns.Add(New DataColumn("Available", System.Type.GetType("System.Double")))

        'Assign [Available] default value from OnHand (UnrestrictedStock) column
        For r = 0 To ds.Inventory.Rows.Count - 1
            ds.Inventory.Rows(r)("Available") = ds.Inventory.Rows(r)("UnrestrictedStock")
        Next

        'query inventory calculation
        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim sQuery As String = "SELECT ml.Material, SUM(Quantity) AS [Reserved]
		            ,i.UnrestrictedStock AS [OnHand]
		            ,(i.UnrestrictedStock - SUM(ml.Quantity)) AS [Available]
	            FROM MaterialList ml
		            JOIN Inventory i ON i.Material=ml.Material 
	            WHERE [Status] = 'Waiting'
	            GROUP BY ml.Material, i.UnrestrictedStock;"

            Dim Adp As New SqlClient.SqlDataAdapter
            Dim Cmd As New SqlClient.SqlCommand(sQuery, Con)
            Adp.SelectCommand = Cmd

            Dim dt As New DataTable("Query")
            Adp.Fill(dt)
            Con.Close()
            Adp.Dispose()

            ' loop and copy matching Material values to master DataSet
            For r = 0 To ds.Inventory.Rows.Count - 1
                For q = 0 To dt.Rows.Count - 1
                    'IF found a matching Material, then copy [Reserved, & Available] columns
                    If ds.Inventory.Rows(r)("Material").ToString = dt.Rows(q)("Material").ToString Then
                        ds.Inventory.Rows(r)("Reserved") = dt.Rows(q)("Reserved")
                        ds.Inventory.Rows(r)("Available") = dt.Rows(q)("Available")
                        Exit For
                    End If
                Next
            Next

            Con.Close()
        End Using

        gvStoreroom.DataSource = ds.Inventory
        gvStoreroom.DataBind()

        For a = 0 To gvStoreroom.Rows.Count - 1
            'Flag inventory as deleted item
            If gvStoreroom.Rows(a).Cells.Item(4).Text.ToUpper = "ZOB" Then
                gvStoreroom.Rows(a).BackColor = Drawing.Color.LightPink
                gvStoreroom.Rows.Item(a).ForeColor = Drawing.Color.Red
            End If

            'flag inventory as Replaceable item
            If gvStoreroom.Rows(a).Cells.Item(4).Text.ToUpper = "ZRP" Then
                gvStoreroom.Rows(a).BackColor = Drawing.Color.LightBlue
                gvStoreroom.Rows.Item(a).ForeColor = Drawing.Color.Blue
            End If
        Next

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub FillOperationData()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.OperationsTableAdapter
        da.FillWorkOrderNumber(ds.Operations, Session("WorkOrder#").ToString)
        gvOperation.DataSource = ds.Operations
        gvOperation.DataBind()

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub FillBOM_Data()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.BOMTableAdapter
        da.FillByEquipment(ds.BOM, txtBOMEquipment.Text)

        gvBOM.DataSource = ds.BOM
        gvBOM.DataBind()

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub FillActualLabor()
        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.ActualLaborTableAdapter
        da.FillWorkOrderNumber(ds.ActualLabor, Session("WorkOrder#").ToString)

        gvActualLabor.DataSource = ds.ActualLabor
        gvActualLabor.DataBind()

        da.Dispose()
        ds.Dispose()
    End Sub

#End Region

#Region "StoreRoom tab and GridView"

    Private Sub gvStoreroom_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStoreroom.PageIndexChanging
        gvStoreroom.PageIndex = e.NewPageIndex
        FillStoreRmData()
    End Sub

    Private Sub gvStoreroom_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvStoreroom.RowCancelingEdit
        gvStoreroom.EditIndex = -1

        gvStoreroom.Columns(0).Visible = True 'Select
        gvStoreroom.Columns(2).Visible = True 'Description
        gvStoreroom.Columns(6).Visible = True 'Reserved
        gvStoreroom.Columns(7).Visible = True 'Available

        FillStoreRmData()
    End Sub

    Private Sub gvStoreroom_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvStoreroom.RowEditing
        gvStoreroom.EditIndex = e.NewEditIndex

        gvStoreroom.Columns(0).Visible = False 'Select
        gvStoreroom.Columns(2).Visible = False 'Description
        gvStoreroom.Columns(6).Visible = False 'Reserved
        gvStoreroom.Columns(7).Visible = False 'Available

        FillStoreRmData()
    End Sub

    Private Sub gvStoreroom_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvStoreroom.RowUpdating
        Dim id As Integer = Convert.ToInt32(gvStoreroom.DataKeys(e.RowIndex).Values(0))

        Dim txt1 As TextBox = gvStoreroom.Rows(e.RowIndex).Cells(3).Controls(0) 'StorageBin
        Dim txt2 As TextBox = gvStoreroom.Rows(e.RowIndex).Cells(4).Controls(0) 'MaterialGrp 
        Dim txt3 As TextBox = gvStoreroom.Rows(e.RowIndex).Cells(5).Controls(0) 'UnrestrictedStock

        'update inventory
        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim sQuery As String = "UPDATE [dbo].[Inventory]
                SET [MaterialGrp] = @MaterialGrp
                    ,[StorageBin] = @StorageBin
                    ,[UnrestrictedStock] = @UnrestrictedStock
                WHERE Id=@id"

            Dim cmd As SqlClient.SqlCommand = Con.CreateCommand()
            cmd.CommandText = sQuery
            cmd.Parameters.AddWithValue("@MaterialGrp", txt2.Text)
            cmd.Parameters.AddWithValue("@StorageBin", txt1.Text)
            cmd.Parameters.AddWithValue("@UnrestrictedStock", txt3.Text)
            cmd.Parameters.AddWithValue("@Id", id)

            cmd.ExecuteNonQuery()

            Con.Close()
        End Using

        gvStoreroom.Columns(0).Visible = True 'Select
        gvStoreroom.Columns(2).Visible = True 'Description
        gvStoreroom.Columns(6).Visible = True 'Reserved
        gvStoreroom.Columns(7).Visible = True 'Available

        gvStoreroom.EditIndex = -1
        FillStoreRmData()
    End Sub

#End Region

#Region "BOM tab and GridView"

    Private Sub gvBOM_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvBOM.PageIndexChanging
        gvBOM.PageIndex = e.NewPageIndex
        FillBOM_Data()
    End Sub

#End Region

#Region "Operation Tab and Gridview"

    Private Sub gvOperation_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvOperation.RowCancelingEdit
        gvOperation.EditIndex = -1
        gvOperation.Columns(4).Visible = True

        FillOperationData()
    End Sub

    Private Sub gvOperation_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvOperation.RowEditing
        gvOperation.EditIndex = e.NewEditIndex
        gvOperation.Columns(4).Visible = False

        FillOperationData()
    End Sub

    Private Sub gvOperation_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvOperation.RowUpdating
        Dim id As Integer = Convert.ToInt32(gvOperation.DataKeys(e.RowIndex).Values(0))

        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.OperationsTableAdapter

        Dim a As String = gvOperation.Rows(e.RowIndex).Cells(1).Text 'STEP
        Dim b As DropDownList = gvOperation.Rows(e.RowIndex).FindControl("ddlCraft")
        Dim d As DropDownList = gvOperation.Rows(e.RowIndex).FindControl("ddlOperationWorkStatus")

        Dim txt1 As TextBox = gvOperation.Rows(e.RowIndex).Cells(5).Controls(0) 'Work
        Dim txt2 As TextBox = gvOperation.Rows(e.RowIndex).Cells(6).Controls(0) 'PeopleUsed

        Dim duration As Double
        'Get total Duration
        If IsNumeric(txt1.Text) And IsNumeric(txt2.Text) Then
            duration = CDbl(txt1.Text) * CDbl(txt2.Text)
        End If

        'Get pricing per hours for craft to calculate PlannedCost
        Dim daCraft As New dsWorkOrderTableAdapters.CraftsTableAdapter
        daCraft.FillbyCraftCode(ds.Crafts, b.SelectedValue)
        Dim pc As Decimal = CDec(ds.Crafts.Rows(0)("Rate")) * CDec(duration)

        da.Update(Session("WorkOrder#").ToString,
                  a,
                  b.SelectedValue,
                  txt1.Text,
                  txt2.Text,
                  duration,
                  d.SelectedValue,
                  Now,
                  Session("UserName").ToString,
                  pc,
                  id)

        da.Dispose()
        ds.Dispose()

        gvOperation.EditIndex = -1
        gvOperation.Columns(4).Visible = True
        FillOperationData()

        CalculatePlannedCostTotal()
    End Sub

    Private Sub gvOperation_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvOperation.RowCommand
        If e.CommandName = "ViewLongText" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = Convert.ToInt32(gvOperation.DataKeys(index).Values(0))

            Dim sQuery As String = "SELECT [Id], [Desc] FROM [dbo].[Operations] WHERE [id]=" & id.ToString

            Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
                Con.Open()

                Dim Adp As New SqlClient.SqlDataAdapter
                Dim Cmd As New SqlClient.SqlCommand(sQuery, Con)
                Cmd.CommandText = sQuery
                Adp.SelectCommand = Cmd

                Dim dt As New DataTable("Query")
                Adp.Fill(dt)
                Con.Close()
                Adp.Dispose()

                Con.Close()

                HF1.Value = dt.Rows(0)("Id").ToString
                txtFullDesc.Text = dt.Rows(0)("Desc").ToString
                mpe.Show()
            End Using

        End If
    End Sub

#End Region

#Region "Material Tab and Gridview"

    Private Sub gvMaterial_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvMaterial.RowCancelingEdit
        gvMaterial.EditIndex = -1

        gvMaterial.Columns(3).Visible = True 'ShortText
        gvMaterial.Columns(4).Visible = True 'StorageBin
        gvMaterial.Columns(6).Visible = True 'UOM
        gvMaterial.Columns(7).Visible = True 'MatGrp
        gvMaterial.Columns(9).Visible = True 'SubTotal
        gvMaterial.Columns(10).Visible = True 'Status
        gvMaterial.Columns(11).Visible = True 'Comment

        FillMaterialData()
    End Sub

    Private Sub gvMaterial_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvMaterial.RowEditing
        gvMaterial.EditIndex = e.NewEditIndex
        'Hide columns if not StoreRoom Personnel
        If Session("IsStoreroom") = False Then
            gvMaterial.Columns(10).Visible = False 'Status
            gvMaterial.Columns(11).Visible = False 'Comment
        End If
        gvMaterial.Columns(3).Visible = False 'ShortText
        gvMaterial.Columns(4).Visible = False 'StorageBin
        gvMaterial.Columns(6).Visible = False 'UOM
        gvMaterial.Columns(7).Visible = False 'MatGrp
        gvMaterial.Columns(9).Visible = False 'SubTotal

        FillMaterialData()
    End Sub

    Private Sub gvMaterial_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvMaterial.RowUpdating
        Dim id As Integer = Convert.ToInt32(gvMaterial.DataKeys(e.RowIndex).Values(0))
        Dim sm As String = gvMaterial.Rows(e.RowIndex).Cells(1).Text.Replace("&nbsp;", "")
        Dim co As String = gvMaterial.Rows(e.RowIndex).Cells(2).Text.Replace("&nbsp;", "")
        Dim pr As String = gvMaterial.Rows(e.RowIndex).Cells(8).Text.Replace("&nbsp;", "").Replace("$", "") 'Price

        Dim txt1 As TextBox = gvMaterial.Rows(e.RowIndex).Cells(5).Controls(0) 'QTY
        Dim txt2 As TextBox = gvMaterial.Rows(e.RowIndex).Cells(11).Controls(0) 'Comment

        Dim ddl As DropDownList = gvMaterial.Rows(e.RowIndex).Cells(10).FindControl("ddlOperationWorkStatus") 'Status

        'TODO: if ddl <> 'Waiting' then remove inventory
        If Not ddl.SelectedValue = "Waiting" Then
            'query inventory Stock/OnHand level
            Dim ds As New dsWorkOrder
            Dim da As New dsWorkOrderTableAdapters.InventoryTableAdapter
            da.FillByMaterial(ds.Inventory, co.Trim)

            'OnHand cannot drop below (0) zero
            If (CDec(ds.Inventory.Rows(0)("UnrestrictedStock")) - CDec(txt1.Text)) > 0 Then

            End If
            'TODO: need to discuss with storeroom if ONHAND should drop below zero
            Dim temp As Double = CDbl(ds.Inventory.Rows(0)("UnrestrictedStock")) - CDbl(txt1.Text)

            da.Update(ds.Inventory.Rows(0)("Material"),
                      ds.Inventory.Rows(0)("ShortText"),
                      ds.Inventory.Rows(0)("UOM"),
                      ds.Inventory.Rows(0)("MaterialGrp"),
                      ds.Inventory.Rows(0)("OldMaterialNumber"),
                      ds.Inventory.Rows(0)("StorageBin"),
                      temp,
                      ds.Inventory.Rows(0)("TotalComsumption"),
                      ds.Inventory.Rows(0)("Price"),
                      ds.Inventory.Rows(0)("MRPType"),
                      ds.Inventory.Rows(0)("MaxStockLevel"),
                      ds.Inventory.Rows(0)("ReOrderPoint"),
                      ds.Inventory.Rows(0)("LongText"),
                      ds.Inventory.Rows(0)("Id"))
        End If

        'Calculate SubTotal before saving to DB
        Dim total = CDbl(txt1.Text) * CDbl(pr)

        'update DB
        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim sQuery As String = "UPDATE [dbo].[MaterialList]
                SET [Quantity] = @Quantity
                  ,[SubTotal] = @SubTotal
                  ,[EditDate] = @EditDate
                  ,[EditUser] = @EditUser
                  ,[Status] = @Status
                  ,[Comment] = @Comment
                WHERE Id=@Id"

            Dim cmd As SqlClient.SqlCommand = Con.CreateCommand()
            cmd.CommandText = sQuery
            cmd.Parameters.AddWithValue("@Quantity", CDbl(txt1.Text))
            cmd.Parameters.AddWithValue("@SubTotal", total)
            cmd.Parameters.AddWithValue("@EditDate", Now)
            cmd.Parameters.AddWithValue("@EditUser", Session("UserName").ToString)
            cmd.Parameters.AddWithValue("@Status", ddl.SelectedValue)
            cmd.Parameters.AddWithValue("@Comment", txt2.Text.Replace("'", "''"))
            cmd.Parameters.AddWithValue("@Id", id)

            cmd.ExecuteNonQuery()

            Con.Close()
        End Using

        gvMaterial.Columns(3).Visible = True 'ShortText
        gvMaterial.Columns(4).Visible = True 'StorageBin
        gvMaterial.Columns(6).Visible = True 'UOM
        gvMaterial.Columns(7).Visible = True 'MatGrp
        gvMaterial.Columns(9).Visible = True 'SubTotal

        gvMaterial.EditIndex = -1
        FillMaterialData()
        FillStoreRmData()

        lblStoreRmSuccess.Visible = False
        lblBOMSuccess.Visible = False
    End Sub

    Private Sub gvMaterial_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvMaterial.RowDeleting
        Dim id As Integer = Convert.ToInt32(gvMaterial.DataKeys(e.RowIndex).Values(0))

        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim sQuery As String = "DELETE [dbo].[MaterialList] WHERE Id=@Id"

            Dim cmd As SqlClient.SqlCommand = Con.CreateCommand()
            cmd.CommandText = sQuery
            cmd.Parameters.AddWithValue("@Id", id)

            cmd.ExecuteNonQuery()

            Con.Close()
        End Using

        FillMaterialData()
    End Sub

#End Region

#Region "Actual Labor Tab and Gridview"

    Private Sub gvActualLabor_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvActualLabor.RowCancelingEdit
        gvActualLabor.EditIndex = -1
        FillActualLabor()
    End Sub

    Private Sub gvActualLabor_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvActualLabor.RowEditing
        gvActualLabor.EditIndex = e.NewEditIndex
        FillActualLabor()
    End Sub

    Private Sub gvActualLabor_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvActualLabor.RowUpdating
        Dim id As Integer = Convert.ToInt32(gvActualLabor.DataKeys(e.RowIndex).Values(0))
        Dim wo As String = gvActualLabor.Rows(e.RowIndex).Cells(1).Text
        Dim txt1 As TextBox = gvActualLabor.Rows(e.RowIndex).FindControl("txtDate")

        Dim txt2 As TextBox = gvActualLabor.Rows(e.RowIndex).Cells(3).Controls(0)
        Dim b As DropDownList = gvActualLabor.Rows(e.RowIndex).FindControl("ddlCraft")

        Dim txt(2) As TextBox
        For i = 0 To 2
            txt(i) = gvActualLabor.Rows(e.RowIndex).Cells(i + 5).Controls(0)
        Next

        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.ActualLaborTableAdapter
        da.Update(wo, txt1.Text, txt2.Text, b.SelectedValue, CDbl(txt(0).Text), Now, Session("UserName").ToString,
                  CDbl(txt(1).Text), CDbl(txt(2).Text), id)

        da.Dispose()
        ds.Dispose()

        gvActualLabor.EditIndex = -1
        FillActualLabor()
    End Sub

    Private Sub gvActualLabor_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvActualLabor.RowDeleting
        Dim id As Integer = Convert.ToInt32(gvActualLabor.DataKeys(e.RowIndex).Values(0))

        Dim ds As New dsWorkOrder
        Dim da As New dsWorkOrderTableAdapters.ActualLaborTableAdapter
        da.Delete(id)

        da.Dispose()
        ds.Dispose()

        FillActualLabor()
    End Sub

#End Region

    Protected Sub DisplayDetails(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = CType(CType(sender, LinkButton).Parent.Parent, GridViewRow)

        Dim cs As ClientScriptManager = Page.ClientScript
        If Not cs.IsStartupScriptRegistered(Me.GetType, "pop") Then
            Dim s As String = "window.open('reservation.aspx?id=" & row.Cells(1).Text.Trim & "', 'popup_window', 'width=800,height=600,left=25,top=25,resizable=yes,scrollbars=yes');"

            'If using normal panel, then use this code to launch popup
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "pop", s, True)

            'if using a Update Panel, then use this code to launch popup
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "pop", s, True)
        End If
    End Sub

    Private Sub CalculatePlannedCostTotal()
        Dim t As Decimal
        For a = 0 To gvOperation.Rows.Count - 1
            t += CDec(gvOperation.Rows(a).Cells.Item(8).Text.Replace("$", ""))
        Next

        txtTotalPlannedCost.Text = String.Format("${0:#,###.00}", t.ToString)
    End Sub

    <System.Web.Services.WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Shared Function GetWorkOrder(ByVal prefixText As String, ByVal count As Integer) As System.String()
        Dim items As String()

        Using Con As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ConnectionString)
            Con.Open()

            Dim sQuery As String = "SELECT [WorkOrderNumber] FROM [dbo].[Orders] WHERE [WorkOrderNumber] LIKE @WorkOrderNumber ORDER BY [WorkOrderNumber] ASC;"

            Dim Adp As New SqlClient.SqlDataAdapter
            Dim Cmd As New SqlClient.SqlCommand(sQuery, Con)
            Cmd.Parameters.AddWithValue("@WorkOrderNumber", prefixText.Trim + "%")
            Cmd.CommandText = sQuery
            Adp.SelectCommand = Cmd

            Dim dt As New DataTable("Query")
            Adp.Fill(dt)

            ReDim items(dt.Rows.Count - 1)

            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows
                items(i) = dr(0).ToString()
                i = i + 1
            Next

            Con.Close()
            Adp.Dispose()
        End Using

        Return items
    End Function
End Class