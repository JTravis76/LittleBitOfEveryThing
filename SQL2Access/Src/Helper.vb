

Module Helper
    ''' <summary>
    ''' Generate SQL INSERT statement and output to file.
    ''' </summary>
    ''' <param name="dt">Collection of data</param>
    ''' <param name="tablename">Table name for the INSERT statement</param>
    ''' <param name="FilePath">Location path to export data</param>
    ''' <remarks></remarks>
    Public Sub CreateInsertStatement(dt As DataTable, tablename As String, Optional FilePath As String = "")

        Dim sInsert As String = "INSERT INTO [" & tablename.Trim & "] ("
        Dim s As String = ""

        For c = 0 To dt.Columns.Count - 1
            sInsert += "[" & dt.Columns(c).ColumnName & "]"

            If c <> dt.Columns.Count - 1 Then sInsert += ", "
        Next

        sInsert += ") VALUES ("


        Dim FILE_NAME As String = "" 'Application.StartupPath & "\" & tablename.Trim & ".sql"
        If FilePath.Length > 0 Then
            FILE_NAME = FilePath
        End If
        If System.IO.File.Exists(FILE_NAME) Then IO.File.Delete(FILE_NAME)
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME)

        If FilePath.Length > 0 Then
            'write DELETE statement
            objWriter.Write("DELETE FROM [" & tablename & "];" & vbCrLf)
        End If

        If dt.Rows.Count > 1 Then
            For r = 0 To dt.Rows.Count - 1
                s = ""
                For c = 0 To dt.Columns.Count - 1

                    If IsDBNull(dt.Rows(r)(c)) Then
                        s += "NULL"
                    Else
                        Select Case dt.Columns(c).DataType.ToString
                            Case "System.Int32", "System.Boolean"
                                s += dt.Rows(r)(c).ToString
                            Case "System.String"
                                s += "'" & dt.Rows(r)(c).ToString.Replace("'", "''").Replace("""", " ").Replace(";", ":") & "'"
                            Case "System.DateTime"
                                s += "#" & dt.Rows(r)(c).ToString & "#"
                            Case Else
                                s += dt.Rows(r)(c).ToString
                        End Select
                    End If

                    If c <> dt.Columns.Count - 1 Then s += ","
                Next
                objWriter.Write(sInsert & s & ");" & vbCrLf)
            Next


        End If

        objWriter.Close()
        dt.Dispose()
    End Sub

    Public Sub Export2CSV(dt As DataTable, FilePath As String)
        Dim headers = (From header As DataColumn In dt.Columns.Cast(Of DataColumn)() Select header.ColumnName).ToArray

        Dim rows = From row As DataRow In dt.Rows.Cast(Of DataRow)() Select row.ItemArray.ToArray
        'Select Array.ConvertAll(row.Cell.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))

        Using sw As New IO.StreamWriter(FilePath)
            sw.WriteLine(String.Join(",", headers))
            For Each r In rows
                sw.WriteLine(String.Join(",", r))
            Next
        End Using
    End Sub
End Module
