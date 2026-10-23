Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms

Public Class dlgReportViewer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' Hidden the special SSRS rendering format in ReportViewer control
    ''' </summary>
    ''' <param name="ReportViewerID">The ID of the relevant ReportViewer control</param>
    ''' <param name="strFormatName">The ID of the relevant ReportViewer control</param>
    ''' <remarks></remarks>
    Private Sub DisableUnwantedExportFormat(ReportViewerID As ReportViewer, strFormatName As String)
        Dim info As System.Reflection.FieldInfo
        For Each ext As RenderingExtension In ReportViewerID.LocalReport.ListRenderingExtensions
            If ext.Name.Trim.ToUpper = strFormatName.ToUpper.ToUpper Then
                info = ext.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance + Reflection.BindingFlags.NonPublic)
                info.SetValue(ext, False)
            End If
        Next

    End Sub

    Private Sub dlgReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.BindingSource1.DataSource = frmMain.g_dtPerson

        Dim dv = New DataView(frmMain.g_dtPerson)
        dv.RowFilter = "Status IN ('IN','MUSTER')"
        dv.Sort = "First_name ASC"
        Me.BindingSource1.DataSource = dv


        Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)

        'Set margins to 0.5 on all sides
        Dim newPageSettings As New System.Drawing.Printing.PageSettings
        newPageSettings.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)

        ReportViewer1.SetPageSettings(newPageSettings)

        Me.ReportViewer1.LocalReport.ReportPath = Application.StartupPath & "\Reports\Report1.rdlc"
        Me.ReportViewer1.LocalReport.DataSources.Clear()
        Me.ReportViewer1.LocalReport.DataSources.Add(rds)


        Me.ReportViewer1.ProcessingMode = ProcessingMode.Local
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_RenderingBegin(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.RenderingBegin
        DisableUnwantedExportFormat(ReportViewer1, "PDF")
    End Sub

    Private Sub dlgReportViewer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ReportViewer1.Width = Me.Width - 40
        ReportViewer1.Height = Me.Height - 98
        Me.Refresh()
    End Sub
End Class
