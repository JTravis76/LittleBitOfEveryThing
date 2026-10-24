
Module Main
    Private g_DataSource As String = "" 'Path to ACCESS database
    Private g_SQLFile As String = ""    'Path to SQL script file
    Private g_FilePath As String = ""   'Path/File name to export data
    Private g_Vebose As Boolean = False

    Sub Main(ByVal args() As String)
        Try
            If args.Length = 0 Then
                ShowUsage()

                If Debugger.IsAttached Then Console.ReadLine()
                Exit Sub
            End If
            '---------------------------------------
            'Process Arguments, assign to variables
            '---------------------------------------
            For a = 0 To UBound(args)

                Select Case args(0)
                    Case "/h", "?", "help", "-h"
                        ShowUsage()
                        Exit Sub
                End Select

                If args(a).Length > 0 Then
                    'Check for SQL script file
                    If InStr(args(a).ToLower, "/s") Then
                        If Right(args(a + 1).ToString, 3).ToLower <> "sql" Then
                            Console.WriteLine("Not a vaild SQL file.")
                            ShowUsage()
                            Exit Sub
                        Else
                            g_SQLFile = args(a + 1).ToString
                        End If
                    End If
                    'check for Access database file
                    If InStr(args(a).ToLower, "/db") Then
                        Dim fileParts As String() = args(a + 1).Split(".")
                        Dim ext = fileParts(fileParts.Length - 1)

                        If ext.ToLower <> "accdb" And ext.ToLower <> "mdb" Then
                            Console.WriteLine("Not a vaild MS-Access database.")
                            Console.Write(Environment.NewLine)
                            ShowUsage()
                            Exit Sub
                        Else
                            g_DataSource = args(a + 1).ToString
                        End If
                    End If
                    'check for export path and format
                    If InStr(args(a).ToLower, "/e") Then
                        g_FilePath = args(a + 1).ToString
                    End If

                    If InStr(args(a).ToLower, "-v") Then
                        g_Vebose = True
                        Console.WriteLine("DataSource=" + g_DataSource)
                        Console.WriteLine("ExportPath=" + g_FilePath)
                    End If

                End If
            Next

            If Not String.IsNullOrEmpty(g_DataSource) Then
                Dim conStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & g_DataSource & ";Persist Security Info=False;"

                'Dim sCurPath As String = System.AppDomain.CurrentDomain.BaseDirectory & "\"
                Dim sQuery As String = OpenTextFile(g_SQLFile)
                Dim sData() As String = Split(sQuery, ";")
                Dim MAX As Integer = UBound(sData)
                Dim DB As New OleDb.OleDbConnection(conStr)

                'check if right verion of MS ACCESS driver installed.
                Try
                    DB.Open()
                Catch ex As Exception
                    Console.WriteLine("")
                    Console.WriteLine(ex.Message)

                    If InStr(ex.Message, "Unrecognized database format") Then End
                End Try


                Console.WriteLine("Running SQL scripts...")

                For a = 0 To MAX
                    If sData(a).Trim.Length > 0 Then
                        Console.Write(String.Format("Progress: {0:##}%" & vbCr, (a / MAX) * 100))
                        ' skip SQL comments
                        If Not Left(sData(a), 2) = "--" Then

                            Try
                                If InStr(sData(a).ToUpper, "SELECT") Then
                                    'export several files if more than one SELECT statement
                                    Dim sFile As String = GetFileName(g_FilePath)
                                    Dim NewFilePath = g_FilePath.Replace(GetFileName(g_FilePath), sFile + " (" + a.ToString + ")")

                                    Dim sTableName As String = GetTableNameFromQuery(sData(a))

                                    Dim dt As New DataTable(sTableName)
                                    Dim conn As OleDb.OleDbConnection = New OleDb.OleDbConnection(conStr)
                                    Dim adp As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter With {
                                        .SelectCommand = New OleDb.OleDbCommand(sData(a), conn)
                                    }
                                    adp.Fill(dt)

                                    conn.Close()
                                    adp.Dispose()

                                    '-----------------------------------------
                                    ' Export data based on format
                                    '  to either working or desired diectory
                                    ' XML, SQL, CSV
                                    '-----------------------------------------
                                    If InStr(g_FilePath.ToLower, ".xml") Then
                                        dt.WriteXml(NewFilePath)
                                    ElseIf InStr(g_FilePath.ToLower, ".sql") Then
                                        Call Helper.CreateInsertStatement(dt, sTableName, NewFilePath)
                                    ElseIf InStr(g_FilePath.ToLower, ".csv") Then
                                        Call Helper.Export2CSV(dt, NewFilePath)
                                    End If


                                    '---------------------
                                    ' Cleanup data table
                                    '---------------------
                                    dt.Dispose()
                                Else
                                    'Set the OleDbCommand object, assign the SQL statement and execute query
                                    Dim DC As New OleDb.OleDbCommand
                                    DC = New OleDb.OleDbCommand(sData(a), DB)
                                    DC.ExecuteNonQuery()
                                End If


                            Catch ex As Exception
                                Console.WriteLine("")
                                Console.WriteLine(ex.Message)
                                Console.WriteLine("")
                                DB.Close()

                                If Debugger.IsAttached Then Console.ReadLine()

                                End
                            End Try

                        End If
                    End If
                Next

                '---------------------
                ' Close DB connection
                '---------------------
                DB.Close()

                Console.WriteLine("Commands executed successfully.")
                Console.WriteLine("")
            End If

        Catch ex As Exception
            Console.WriteLine("")
            Console.WriteLine(ex.Message)
            Console.WriteLine("")
            'ShowUsage()
        End Try

        If Debugger.IsAttached Then Console.ReadLine()
    End Sub

    Private Function OpenTextFile(ByVal strFileName As String) As String
        Dim strReturn As String = String.Empty

        Dim objStreamReader As System.IO.StreamReader
        objStreamReader = System.IO.File.OpenText(strFileName)
        strReturn = objStreamReader.ReadToEnd
        objStreamReader.Close()

        Return strReturn
    End Function

    Private Function GetFileName(s As String) As String
        If InStr(s, "\") Then
            Dim sData() As String = Split(s, "\")
            Dim sFile() = Split(sData(UBound(sData)), ".")
            Return sFile(0).Trim
        Else
            Dim sFile() = Split(s, ".")
            Return sFile(0).Trim
        End If
        Return s
    End Function

    Private Function GetTableNameFromQuery(s As String) As String
        Dim sTableName As String = ""
        Dim sData() = Split(s.ToUpper, " FROM ")
        sData = Split(sData(1), "] ")
        sTableName = sData(0).Replace("[", "").Replace("]", "")
        Return sTableName
    End Function

    Private Sub ShowUsage()
        Console.WriteLine("")
        Console.WriteLine("Usage: sql2access.exe [/s myScript.sql] [/db myDatabase.[mdb][accdb]] [/e path-to-export.xml]")
        Console.WriteLine("")
        Console.WriteLine("Options:")
        Console.WriteLine("    /s    Path to file that contains sql statements.")
        Console.WriteLine("          SELECT, INSERT, UPDATE, DROP are all supported.")
        Console.WriteLine("          NOTE: All statements must end with a semi-colon (;)")
        Console.WriteLine("")
        Console.WriteLine("    /db   Path to MS Access database file (mdb)(accdb).")
        Console.WriteLine("")
        Console.WriteLine("    /e    Path and file name to export files. ONLY use with ")
        Console.WriteLine("          SELECT statement. File names are index if more than ")
        Console.WriteLine("          one SELECT statement is performed.")
        Console.WriteLine("")
        Console.WriteLine("          Export format is based on file extension.")
        Console.WriteLine("          XML - will export file in xml format")
        Console.WriteLine("          SQL - will export query as INSERT statements")
        Console.WriteLine("          CSV - will export query into comma delimited file")
        Console.WriteLine("")
        Console.WriteLine("This tool is use to execute a list of sql queries against a Microsoft Access database (accdb).")
        Console.WriteLine("")
    End Sub
End Module
