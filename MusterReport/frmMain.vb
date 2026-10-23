Imports System.Data.Odbc
Imports JT2014.Library

Public Class frmMain

    ' Global Var
    Public g_dtPerson As New DataTable("Person")
    Private g_dtJourn As New DataTable("Journ")
    Private g_dtDoor As New DataTable("Door")
    Private g_dtJNL As New DataTable("JNL_Directory")
    Private g_dtCard As New DataTable("Card")
    Private g_dsConfig As New DataSet("Config")

    ' for moving legend box
    Private MouseIsDown As Boolean = False
    Private MouseIsDownLoc As Point = Nothing

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                RefreshToolStripMenuItem_Click(sender, e)
            Case Keys.L
                LegendToolStripMenuItem_Click(sender, e)
            Case Keys.P
                ReportViewerToolStripMenuItem_Click(sender, e)
        End Select
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        g_dtPerson.Columns.Add("Status", GetType(String), "")
        g_dtPerson.Columns.Add("Door", GetType(String), "")

        g_dtJNL.Columns.Add("Port", GetType(String), "")
        g_dtJNL.Columns.Add("DB", GetType(String), "")

        pbAllClear.Left = Me.Width - 46
        pbAllClear.Size = New Point(30, Me.Height - 92)

        'Get ConfigDB
        Try
            g_dsConfig.ReadXml(Application.StartupPath & "\db.xml")

            If g_dsConfig.Tables("Config").Rows.Count > 0 Then
                'Set Legend to display selected color
                Label1.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_BackColor").ToString.Trim)
                Label1.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_ForeColor").ToString.Trim)

                Label2.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_BackColor").ToString.Trim)
                Label2.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_ForeColor").ToString.Trim)

                Label3.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor").ToString.Trim)
                Label3.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor").ToString.Trim)

                Label4.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_BackColor").ToString.Trim)
                Label4.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_ForeColor").ToString.Trim)
            End If
        Catch ex As Exception
            MsgBox("Could not load configuration file!", MsgBoxStyle.Critical, "Application Error")
            End
        End Try


        GetJNL_Directory()

        If g_dtJNL.Rows.Count > 0 Then
            g_dtJNL.Rows(0)("DB") = "JL_" & String.Format("{0:00000}", g_dtJNL.Rows(0)("Volume"))
            ToolStripStatusLabel3.Text = "Current Journal: " & g_dtJNL.Rows(0)("DB").ToString

            If g_dtJNL.Rows(0)("Volume") Mod 2 <> 0 Then
                g_dtJNL.Rows(0)("Port") = "JNSRV1"
            Else
                g_dtJNL.Rows(0)("Port") = "JNSRV2"
            End If
        End If


        GetDoors()
        GetActivePerson()
        GetCards()

        Timer1.Enabled = True
        ToolStripStatusLabel2.ProgressBar.Maximum = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec")
        ToolStripStatusLabel2.ProgressBar.Value = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec")
        ToolStripStatusLabel2.Size = New Size(Me.Width - 400, 17)

        Panel1.Location = New Point(Me.Width - 280, Me.Height - 280)
        If g_dsConfig.Tables("Config").Rows(0)("LegendOnStartup") Then
            LegendToolStripMenuItem.Checked = True
            Panel1.Visible = True
        End If

        AllClearToolStripMenuItem.Checked = False

        If g_dsConfig.Tables("Config").Rows(0)("RefreshOnStartup") Then RefreshToolStripMenuItem_Click(sender, e)

        'Init Printer var
        prnDialog = New System.Windows.Forms.PrintDialog
        prnPreview = New System.Windows.Forms.PrintPreviewDialog
        prnDocument = New System.Drawing.Printing.PrintDocument
        ' The Event of 'PrintPage'
        AddHandler prnDocument.PrintPage, AddressOf prnDocument_PrintPage

        prnPreview.ClientSize = New Size(800, 600)
    End Sub


    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Timer1.Enabled = False

        ToolStripStatusLabel2.Size = New Size(Me.Width - 400, 17)
        Panel1.Location = New Point(Me.Width - 280, Me.Height - 280)
        pbAllClear.Left = Me.Width - 46
        pbAllClear.Size = New Point(30, Me.Height - 92)

        Select Case Me.WindowState
            Case FormWindowState.Maximized
                Timer1.Enabled = True
            Case FormWindowState.Minimized
                Timer1.Enabled = False
        End Select
    End Sub

    Private Sub GetJNL_Directory()
        Dim strSQL As String = "SELECT Volume FROM Pub.JNL_Directory WHERE Current_Volume = '1'"

        Dim strConn As String = "DRIVER={MERANT 3.60 32-BIT Progress SQL92 v9.1D};HOST=HVSV-CCURE01;PORT=CFSRV;DB=CF;UID=sysprogress;PWD=Letmein123;DIL=READ UNCOMMITTED"
        '"DSN=CFSRV;UID=sysprogress;PWD=Letmein123;"
        Dim Conn As System.Data.Odbc.OdbcConnection = New OdbcConnection(strConn)
        Dim adp As OdbcDataAdapter = New OdbcDataAdapter
        adp.SelectCommand = New OdbcCommand(strSQL, Conn)
        adp.Fill(g_dtJNL)

        Conn.Open()
        Conn.Close()
    End Sub

    Private Sub GetActivePerson()
        g_dtPerson.Clear()
        'Text1 & Logical2 are custom fields within CCURE
        'Text1 = Department, Logical2 = ERT Member
        Dim strSQL As String = "SELECT Person_ID, Last_Name, First_Name, Middle_Name, Card_num, Person_Type, Text1, Logical3 FROM Pub.Person" & _
            " WHERE Deleted = '0' AND Inactive = '0' AND Expired = '0' AND" & _
            " (Person_ID <> '1623') AND (Person_ID <> '1720') AND (Last_Name NOT LIKE '$default%')" & _
            " ORDER BY First_Name"
        '" AND (Person_ID='1755641')" & _


        Dim strConn As String = "DRIVER={MERANT 3.60 32-BIT Progress SQL92 v9.1D};HOST=HVSV-CCURE01;PORT=CFSRV;DB=CF;UID=sysprogress;PWD=Letmein123;DIL=READ UNCOMMITTED"
        '"DSN=CFSRV;UID=sysprogress;PWD=Letmein123;"
        Dim Conn As System.Data.Odbc.OdbcConnection = New OdbcConnection(strConn)
        Dim adp As OdbcDataAdapter = New OdbcDataAdapter
        adp.SelectCommand = New OdbcCommand(strSQL, Conn)
        adp.Fill(g_dtPerson)

        Conn.Open()
        Conn.Close()
    End Sub

    Private Sub GetCards()
        g_dtCard.Clear()
        Dim strSQL As String = "SELECT Person_ID, Card_num FROM Pub.Card" & _
            " WHERE (Deleted = '0') AND (Disabled_Card = '0') AND (Expired = '0') AND" & _
            " (Person_ID <> '1623') AND (Person_ID <> '1720') AND (Lost ='0') ANd (Stolen = '0')" & _
            " ORDER BY Person_ID"

        Dim strConn As String = "DRIVER={MERANT 3.60 32-BIT Progress SQL92 v9.1D};HOST=HVSV-CCURE01;PORT=CFSRV;DB=CF;UID=sysprogress;PWD=Letmein123;DIL=READ UNCOMMITTED"
        '"DSN=CFSRV;UID=sysprogress;PWD=Letmein123;"
        Dim Conn As System.Data.Odbc.OdbcConnection = New OdbcConnection(strConn)
        Dim adp As OdbcDataAdapter = New OdbcDataAdapter
        adp.SelectCommand = New OdbcCommand(strSQL, Conn)
        adp.Fill(g_dtCard)

        Conn.Open()
        Conn.Close()
    End Sub

    Private Sub GetDoors()
        g_dtDoor.Clear()

        Dim strSQL As String = "SELECT Door_ID, Door_Name FROM Pub.Door"

        Dim strConn As String = "DRIVER={MERANT 3.60 32-BIT Progress SQL92 v9.1D};HOST=HVSV-CCURE01;PORT=CFSRV;DB=CF;UID=sysprogress;PWD=Letmein123;DIL=READ UNCOMMITTED"
        '"DSN=CFSRV;UID=sysprogress;PWD=Letmein123;"
        Dim Conn As System.Data.Odbc.OdbcConnection = New OdbcConnection(strConn)
        Dim adp As OdbcDataAdapter = New OdbcDataAdapter
        adp.SelectCommand = New OdbcCommand(strSQL, Conn)
        adp.Fill(g_dtDoor)

        Conn.Open()
        Conn.Close()
    End Sub

    Private Sub GetActivityReport()
        g_dtJourn.Clear()
        Dim v As System.DateTime = Now.AddHours(-23)

        'System.Diagnostics.Debug.Print(CDateTimeLng(v).ToString)

        'Int_Data2 (64 = IN , 128 = OUT, 65 = IN [UNUSED])
        'User_PID = '1755641 = Jeremy Travis' 1755405 = Jeff huffman, 1677201 = Tom Rickman, 1755397 = Shawn Darnell, Aaron Fry = 1755566
        'Msg_Code (1= login/off, 2= admitted, 3= rejected)
        Dim strSQL As String = "SELECT User_PID, Int_Data1, Host_DT, Int_Data2, Jnl_ID FROM Pub.Journ" & _
                " WHERE Msg_Code = 2 AND HOST_DT > '" & CDateTimeLng(v) & "' ORDER BY Jnl_ID DESC"

        Dim strConn As String = "DRIVER={MERANT 3.60 32-BIT Progress SQL92 v9.1D};HOST=HVSV-CCURE01;PORT=" & g_dtJNL.Rows(0)("Port").ToString & ";DB=" & g_dtJNL.Rows(0)("DB").ToString & ";UID=sysprogress;PWD=Letmein123;DIL=READ UNCOMMITTED"
        '"DSN=CFSRV;UID=sysprogress;PWD=Letmein123;"
        Dim Conn As System.Data.Odbc.OdbcConnection = New OdbcConnection(strConn)
        Dim adp As OdbcDataAdapter = New OdbcDataAdapter
        adp.SelectCommand = New OdbcCommand(strSQL, Conn)
        adp.Fill(g_dtJourn)

        Conn.Open()
        Conn.Close()
    End Sub

    Private Sub BuildTable()
        '1st loop through Person table to fetch Person_ID
        'then loop through Journ table and check if in plant
        'if IN, set status to "IN"
        'then check if swiped at muster station, set status is needed

        For a = 0 To g_dtPerson.Rows.Count - 1

            'All Clear unchecked, set status to default "OUT"
            If Not AllClearToolStripMenuItem.Checked Then
                g_dtPerson.Rows(a)("Status") = "OUT"
            End If


            For b = 0 To g_dtJourn.Rows.Count - 1

                If g_dtPerson.Rows(a)("Person_ID") = g_dtJourn.Rows(b)("User_PID") Then

                    Select Case g_dtJourn.Rows(b)("Int_Data2")
                        Case "64", "65" 'find a [IN] access point

                            'skip if already MUSTER
                            'when all clear has been triggered
                            If g_dtPerson.Rows(a)("Status") = "MUSTER" Then Exit For

                            g_dtPerson.Rows(a)("Status") = "IN"

                            'Is Muster??
                            Select Case g_dtJourn.Rows(b)("Int_Data1")
                                Case "25453", "25454", "25455", "25456", "25459", "25460", "25463", "25471", "25472", "25473", "25474", "25475", "25476", "25477", "25478", "25501"
                                    g_dtPerson.Rows(a)("Status") = "MUSTER"
                            End Select

                            'Find door friendly name
                            For z = 0 To g_dtDoor.Rows.Count - 1
                                If g_dtJourn.Rows(b)("Int_Data1") = g_dtDoor.Rows(z)("Door_ID") Then
                                    g_dtPerson.Rows(a)("Door") = g_dtDoor.Rows(z)("Door_Name")
                                    Exit For
                                End If
                            Next
                            Exit For

                        Case "128" '[Out]
                            g_dtPerson.Rows(a)("Status") = "OUT"
                            Exit For

                    End Select

                End If

            Next b
        Next a

    End Sub

    Private Sub DrawTable()
        'Remove ALL Label Controls
        For i As Integer = Me.Controls.Count - 1 To 0 Step -1
            If TypeOf Me.Controls(i) Is Label Then
                Me.Controls.RemoveAt(i)
                'Me.Controls.Remove(controlName)
            End If
        Next

        'Calulate rows based on window height and label height
        Dim lblHeight As Integer = 20
        Dim rows As Integer = Math.Round(((Me.Height - 150) / lblHeight))

        Dim x, c, r, rc As Integer
        r = 1

        Dim lbl(g_dtPerson.Rows.Count - 1) As Label

        For y = 0 To g_dtPerson.Rows.Count - 1

            If g_dtPerson.Rows(y)("Status") = "IN" Then
                lbl(x) = New Label
                lbl(x).Name = "lbl" & y.ToString
                lbl(x).Text = g_dtPerson.Rows(y)("First_Name").ToString.ToUpper & " " & g_dtPerson.Rows(y)("Last_Name").ToString.ToUpper  ' & " - " & g_dtPerson.Rows(y)("Door").ToString
                lbl(x).Size = New Size(200, lblHeight)
                lbl(x).AutoSize = False
                lbl(x).Location = New Point(c * 202, (r * (lblHeight + 2)) + 4)
                lbl(x).Font = New Drawing.Font("Arial", 12, FontStyle.Bold)
                lbl(x).Tag = g_dtPerson.Rows(y)("Door").ToString.ToUpper & "|" & g_dtPerson.Rows(y)("Middle_Name").ToString.ToUpper & "|" & g_dtPerson.Rows(y)("Text1").ToString.ToUpper & _
                     "|" & g_dtPerson.Rows(y)("Person_ID").ToString.ToUpper

                'assign backcolor if Employee/Contractor
                If g_dtPerson.Rows(y)("Person_Type") = 5 Then
                    lbl(x).BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_BackColor").ToString.Trim)
                    lbl(x).ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_ForeColor").ToString.Trim)
                Else
                    lbl(x).BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_BackColor").ToString.Trim)
                    lbl(x).ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_ForeColor").ToString.Trim)
                End If

                'assign ERT color
                If g_dtPerson.Rows(y)("Logical3") Then
                    lbl(x).BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor").ToString.Trim)
                    lbl(x).ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor").ToString.Trim)
                End If


                AddHandler lbl(x).Click, AddressOf OnCustomClick

                x += 1
                r += 1
                'add 1 to c when column is filled
                If r = rows Then c += 1 : r = 1

                rc += 1

            ElseIf g_dtPerson.Rows(y)("Status") = "MUSTER" Then
                lbl(x) = New Label
                lbl(x).Name = "lbl" & y.ToString
                lbl(x).Text = g_dtPerson.Rows(y)("First_Name").ToString.ToUpper & " " & g_dtPerson.Rows(y)("Last_Name").ToString.ToUpper  ' & " - " & g_dtPerson.Rows(y)("Door").ToString
                lbl(x).Size = New Size(200, lblHeight)
                lbl(x).AutoSize = False
                lbl(x).Location = New Point(c * 202, (r * (lblHeight + 2)) + 4)
                lbl(x).BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_BackColor").ToString.Trim)
                lbl(x).ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_ForeColor").ToString.Trim)

                lbl(x).Font = New Drawing.Font("Arial", 12, FontStyle.Bold)
                lbl(x).Tag = g_dtPerson.Rows(y)("Door").ToString.ToUpper & "|" & g_dtPerson.Rows(y)("Middle_Name").ToString.ToUpper & "|" & g_dtPerson.Rows(y)("Text1").ToString.ToUpper & _
                     "|" & g_dtPerson.Rows(y)("Person_ID").ToString.ToUpper

                'assign ERT color
                If g_dtPerson.Rows(y)("Logical3") Then
                    lbl(x).BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor").ToString.Trim)
                    lbl(x).ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor").ToString.Trim)
                End If

                AddHandler lbl(x).Click, AddressOf OnCustomClick

                x += 1
                r += 1
                'add 1 to c when column is filled
                If r = rows Then c += 1 : r = 1

                rc += 1
            End If
        Next

        ToolStripStatusLabel1.Text = "Personnel Count: " & rc.ToString
        Me.Controls.AddRange(lbl)
    End Sub

    Private Sub OnCustomClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = DirectCast(sender, Label)
        Dim curBackColor As Color = lbl.BackColor
        Dim curForeColor As Color = lbl.ForeColor

        lbl.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Selected_BackColor").ToString.Trim)
        lbl.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Selected_ForeColor").ToString.Trim)

        Timer1.Enabled = False

        Dim sData() As String = Split(lbl.Tag.ToString, "|")

        For r = 0 To g_dtCard.Rows.Count - 1
            If sData(3).ToString = g_dtCard.Rows(r)("Person_ID").ToString Then

                dlgProperties.lblCardNum.Text = g_dtCard.Rows(r)("Card_num").ToString
                dlgProperties.lblDepartment.Text = sData(2).ToString
                dlgProperties.lblDoor.Text = sData(0).ToString
                dlgProperties.lblMiddleName.Text = sData(1).ToString

                If dlgProperties.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    lbl.BackColor = curBackColor
                    lbl.ForeColor = curForeColor
                    Timer1.Enabled = True
                End If
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ToolStripStatusLabel2.ProgressBar.Value > 0 Then
            ToolStripStatusLabel2.ProgressBar.Value = ToolStripStatusLabel2.ProgressBar.Value - 1
        Else
            RefreshToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub btnCloseLegend_Click(sender As Object, e As EventArgs) Handles btnCloseLegend.Click
        Panel1.Visible = False
        LegendToolStripMenuItem.Checked = False
    End Sub

    Private Sub pbAllClear_Click(sender As Object, e As EventArgs) Handles pbAllClear.Click
        If pbAllClear.BackColor = Color.DarkRed Then
            AllClearToolStripMenuItem.Checked = True
            pbAllClear.BackColor = Color.Green
        Else
            AllClearToolStripMenuItem.Checked = False
            pbAllClear.BackColor = Color.DarkRed
        End If
    End Sub

#Region "Mouse Events"

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        MouseIsDown = True
        MouseIsDownLoc = New Point(e.X, e.Y)
        Me.Cursor = Cursors.SizeAll
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        MouseIsDown = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = MouseButtons.Left Then
            If MouseIsDown Then
                Panel1.Location = New Point(Panel1.Location.X + e.X - MouseIsDownLoc.X, Panel1.Location.Y + e.Y - MouseIsDownLoc.Y)
            End If
        End If
    End Sub

#End Region

#Region "Menu Items"

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs)
        RefreshToolStripMenuItem.Enabled = False

        GetActivityReport()
        BuildTable()
        DrawTable()

        ToolStripStatusLabel2.ProgressBar.Value = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec")

        RefreshToolStripMenuItem.Enabled = True
    End Sub

    Private Sub AllClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllClearToolStripMenuItem.Click
        If AllClearToolStripMenuItem.Checked Then
            AllClearToolStripMenuItem.Checked = False
        Else
            AllClearToolStripMenuItem.Checked = True
        End If

        pbAllClear_Click(sender, e)
    End Sub

    Private Sub ViewHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHelpToolStripMenuItem.Click
        Try
            Dim Path As String = Application.StartupPath & "\Help\index.html"
            System.Diagnostics.Process.Start(Path)
        Catch ex As Exception
            MsgBox(ex.Message, , "Muster Dashboard")
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Timer1.Enabled = False

        Dim frm As New AboutBox1
        If frm.ShowDialog = DialogResult.Cancel Then Timer1.Enabled = True
    End Sub

    Private Sub LegendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LegendToolStripMenuItem.Click
        If Panel1.Visible Then
            Panel1.Visible = False
            LegendToolStripMenuItem.Checked = False
        Else
            Panel1.Visible = True
            LegendToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Timer1.Enabled = False

        dlgOptions.txtRefreshSec.Value = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec").ToString.Trim
        dlgOptions.cbRefreshStartup.Checked = g_dsConfig.Tables("Config").Rows(0)("RefreshOnStartup")

        dlgOptions.cbShowLegend.Checked = g_dsConfig.Tables("Config").Rows(0)("LegendOnStartup")

        dlgOptions.lblEmployees.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_ForeColor").ToString.Trim)
        dlgOptions.lblEmployees.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_BackColor").ToString.Trim)

        dlgOptions.lblContractors.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_ForeColor").ToString.Trim)
        dlgOptions.lblContractors.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_BackColor").ToString.Trim)

        dlgOptions.lblERT.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor").ToString.Trim)
        dlgOptions.lblERT.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor").ToString.Trim)

        dlgOptions.lblMustered.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_ForeColor").ToString.Trim)
        dlgOptions.lblMustered.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_BackColor").ToString.Trim)

        dlgOptions.lblSelected.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Selected_ForeColor").ToString.Trim)
        dlgOptions.lblSelected.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Selected_BackColor").ToString.Trim)

        If dlgOptions.ShowDialog = Windows.Forms.DialogResult.OK Then
            'save data to db.xml
            g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec") = dlgOptions.txtRefreshSec.Value
            g_dsConfig.Tables("Config").Rows(0)("RefreshOnStartup") = dlgOptions.cbRefreshStartup.Checked

            g_dsConfig.Tables("Config").Rows(0)("LegendOnStartup") = dlgOptions.cbRefreshStartup.Checked

            g_dsConfig.Tables("Config").Rows(0)("Employee_ForeColor") = ColorTranslator.ToHtml(dlgOptions.lblEmployees.ForeColor)
            g_dsConfig.Tables("Config").Rows(0)("Employee_BackColor") = ColorTranslator.ToHtml(dlgOptions.lblEmployees.BackColor)

            g_dsConfig.Tables("Config").Rows(0)("Contractor_ForeColor") = ColorTranslator.ToHtml(dlgOptions.lblContractors.ForeColor)
            g_dsConfig.Tables("Config").Rows(0)("Contractor_BackColor") = ColorTranslator.ToHtml(dlgOptions.lblContractors.BackColor)

            g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor") = ColorTranslator.ToHtml(dlgOptions.lblERT.ForeColor)
            g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor") = ColorTranslator.ToHtml(dlgOptions.lblERT.BackColor)

            g_dsConfig.Tables("Config").Rows(0)("Muster_ForeColor") = ColorTranslator.ToHtml(dlgOptions.lblMustered.ForeColor)
            g_dsConfig.Tables("Config").Rows(0)("Muster_BackColor") = ColorTranslator.ToHtml(dlgOptions.lblMustered.BackColor)

            g_dsConfig.Tables("Config").Rows(0)("Selected_ForeColor") = ColorTranslator.ToHtml(dlgOptions.lblSelected.ForeColor)
            g_dsConfig.Tables("Config").Rows(0)("Selected_BackColor") = ColorTranslator.ToHtml(dlgOptions.lblSelected.BackColor)


            g_dsConfig.WriteXml(Application.StartupPath & "\db.xml")


            'Set Legend to display selected color
            Label1.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_BackColor").ToString.Trim)
            Label1.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Employee_ForeColor").ToString.Trim)

            Label2.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_BackColor").ToString.Trim)
            Label2.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Contractor_ForeColor").ToString.Trim)

            Label3.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_BackColor").ToString.Trim)
            Label3.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("ERT_ForeColor").ToString.Trim)

            Label4.BackColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_BackColor").ToString.Trim)
            Label4.ForeColor = ColorTranslator.FromHtml(g_dsConfig.Tables("Config").Rows(0)("Muster_ForeColor").ToString.Trim)

            'adjust timer settings
            ToolStripStatusLabel2.ProgressBar.Maximum = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec")
            ToolStripStatusLabel2.ProgressBar.Value = g_dsConfig.Tables("Config").Rows(0)("Refresh_Sec")
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        Timer1.Enabled = False

        Try
            prnDialog.Document = prnDocument
            ' If user Click 'OK', Print Invoice
            If (prnDialog.ShowDialog = DialogResult.OK) Then
                prnDocument.Print()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        Timer1.Enabled = True
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Timer1.Enabled = False

        prnPreview.Document = prnDocument
        Try
            prnPreview.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        Timer1.Enabled = True
    End Sub

    Private Sub ReportViewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportViewerToolStripMenuItem.Click
        Timer1.Enabled = False
        dlgReportViewer.ShowDialog()
        Timer1.Enabled = True
    End Sub

#End Region

End Class
