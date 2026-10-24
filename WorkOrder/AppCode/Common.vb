Imports System.Data.SqlClient
Imports System.DirectoryServices

Module Common
    Public _MemberOf() As String

    ''' <summary>
    ''' use to authenticated against Domain via LDAP
    ''' </summary>
    ''' <param name="userName">user to authenticate</param>
    ''' <param name="userPassword">user's passwordd</param>
    ''' <param name="groupName">optional: active group membership</param>
    ''' <returns>True, if successful. Otherwise, False</returns>
    ''' <remarks></remarks>
    Public Function ValidateLDAPLogin(ByVal userName As String, ByVal userPassword As String, Optional groupName As String = "", Optional TestMembership As Boolean = False) As Boolean
        Dim isValidated As Boolean = False
        Dim sData() As String = Nothing
        Dim domain As String = ConfigurationManager.AppSettings("Domain")
        Dim dcDNS As String = ConfigurationManager.AppSettings("LDAPServer")

        If groupName.Length > 0 Then sData = Split(groupName, "|")

        Try
            Dim ldapPath As String = "LDAP://" & dcDNS
            Dim dirEntry As New DirectoryEntry(ldapPath, userName, userPassword, AuthenticationTypes.Secure)
            Dim dirSearcher As New DirectorySearcher(dirEntry)


            'sAMAccountNames are like eQuest\Wokawidget
            'userPrincipalNames are like Wokawidget@eQuest.com

            'dirSearcher.Filter = "(sAMAccountNames=" & domain & "\" & userName & ")"
            dirSearcher.Filter = "(userPrincipalName=" & userName & "@" & domain & ")"
            dirSearcher.PropertiesToLoad.Add("MemberOf")

            Dim result As SearchResult = dirSearcher.FindOne()

            If Not result Is Nothing Then
                If groupName.Length = 0 Then
                    isValidated = True
                Else
                    Dim groupCount As Integer = result.Properties("MemberOf").Count
                    Dim isInGroup As Boolean = False

                    For index As Integer = 0 To groupCount - 1
                        Dim groupDN As String = result.Properties("MemberOf").Item(index)

                        Dim equalsIndex As Integer = groupDN.IndexOf("=")
                        Dim commaIndex As Integer = groupDN.IndexOf(",")

                        Dim group As String = groupDN.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1).ToLower

                        ReDim Preserve _MemberOf(index)
                        _MemberOf(index) = group
                        'System.Diagnostics.Debug.Print(group)

                        If TestMembership Then
                            For a As Int16 = 0 To UBound(sData)
                                If group.Equals(sData(a).ToLower) Then
                                    isInGroup = True
                                    Exit For
                                End If
                            Next
                        Else
                            isInGroup = True
                        End If

                    Next index

                    isValidated = isInGroup
                End If
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try

        Return isValidated
    End Function

    Public Sub FindUserId()
        Dim dt As New DataTable("Query")

        'find Userid if already exist
        Using Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("db").ToString)
            Conn.Open()

            Dim Adp As New SqlDataAdapter
            Dim Cmd As New SqlCommand("SELECT TOP 1 [id],[isAdmin] FROM [Users] WHERE [name]=@username", Conn)
            Cmd.Parameters.AddWithValue("@username", HttpContext.Current.Session("UserName").ToString)
            Adp.SelectCommand = Cmd

            Adp.Fill(dt)
            Conn.Close()
            Adp.Dispose()
        End Using

        If dt.Rows.Count > 0 Then
            HttpContext.Current.Session("Userid") = dt.Rows(0)("id")
            HttpContext.Current.Session("IsAdmin") = dt.Rows(0)("isAdmin")
        Else
            HttpContext.Current.Session("Userid") = String.Empty
            HttpContext.Current.Session("IsAdmin") = False
        End If

        dt.Dispose()
    End Sub

    Public Sub UserAutoCreated()
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("db").ToString)
            con.Open()
            'Insert new user into DB
            Dim cmd As SqlCommand = con.CreateCommand()
            cmd.CommandText = "INSERT INTO [dbo].[Users]([name],[email],[phone])VALUES(@username,@email,@phone)"
            cmd.Parameters.AddWithValue("@username", HttpContext.Current.Session("UserName").ToString)
            cmd.Parameters.AddWithValue("@email", DBNull.Value)
            cmd.Parameters.AddWithValue("@phone", DBNull.Value)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

            con.Close()
        End Using
    End Sub

    Public Sub InsertHistory(ByVal histType As Integer, ByVal deviceId As Integer, ByVal strValue As String)
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("db").ToString)
            con.Open()
            'Insert history for audit trail
            Dim cmd As SqlCommand = con.CreateCommand()
            cmd.CommandText = "INSERT INTO [dbo].[History] ([hist_type],[device_id],[user_id],[timestamp],[description])VALUES(@hist,@device,@userid,@logon,@descr)"

            Select Case (histType)
                Case 1
                    cmd.CommandText = "INSERT INTO [dbo].[History] ([hist_type],[device_id],[user_id],[timestamp],[description])VALUES(@hist,@device,@userid,@logon,@descr)"
                    cmd.Parameters.AddWithValue("@hist", 1)
                    cmd.Parameters.AddWithValue("@device", 0)
                    cmd.Parameters.AddWithValue("@userid", 0)
                    cmd.Parameters.AddWithValue("@logon", Now.ToString)
                    cmd.Parameters.AddWithValue("@descr", strValue.ToLower & ": Logged onto system")
                Case 2
                    cmd.Parameters.AddWithValue("@hist", 2)
                    cmd.Parameters.AddWithValue("@device", 0)
                    cmd.Parameters.AddWithValue("@userid", 0)
                    cmd.Parameters.AddWithValue("@logon", Now.ToString)
                    cmd.Parameters.AddWithValue("@descr", strValue.ToLower & ": Auto account creation.")
                Case 3
                    cmd.Parameters.AddWithValue("@hist", 3)
                    cmd.Parameters.AddWithValue("@device", deviceId)
                    cmd.Parameters.AddWithValue("@userid", HttpContext.Current.Session("Userid").ToString)
                    cmd.Parameters.AddWithValue("@logon", Now.ToString)
                    cmd.Parameters.AddWithValue("@descr", strValue & ": User made changes.")
                Case 4
                    cmd.Parameters.AddWithValue("@hist", 4)
                    cmd.Parameters.AddWithValue("@device", deviceId)
                    cmd.Parameters.AddWithValue("@userid", HttpContext.Current.Session("Userid").ToString)
                    cmd.Parameters.AddWithValue("@logon", Now.ToString)
                    cmd.Parameters.AddWithValue("@descr", strValue & ": New record was added.")
                Case 5
                    cmd.Parameters.AddWithValue("@hist", 5)
                    cmd.Parameters.AddWithValue("@device", deviceId)
                    cmd.Parameters.AddWithValue("@userid", HttpContext.Current.Session("Userid").ToString)
                    cmd.Parameters.AddWithValue("@logon", Now.ToString)
                    cmd.Parameters.AddWithValue("@descr", strValue & ": Record was edited.")
            End Select

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            con.Close()
        End Using
    End Sub

    Public Sub HandleError(ByVal ServerError As Exception)
        Try
            Dim MyMailMessage As New System.Net.Mail.MailMessage
            With MyMailMessage
                .From = New System.Net.Mail.MailAddress("syserror@altivia.com", "Haverhill Work Order")
                .To.Add(New System.Net.Mail.MailAddress("jtravis@altivia.com", "Jeremy Travis"))
                .Subject = "Haverhill Work Order error notification!"
                .IsBodyHtml = False

                If Not ServerError Is Nothing Then
                    .Body = "Page : " & HttpContext.Current.Request.Url.AbsoluteUri & vbCrLf & vbCrLf &
                            "Exception : " & ServerError.ToString() & vbCrLf & vbCrLf &
                            "User Agent : " & HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT") & vbCrLf & vbCrLf &
                            "IP Address : " & HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
                End If
            End With

            SendMyMessage(MyMailMessage)
        Catch
        End Try
    End Sub

    Public Sub SendMyMessage(ByVal MyMailMessage As System.Net.Mail.MailMessage)
        'SMTP setting are in Web.config
        Dim SMTP As New System.Net.Mail.SmtpClient()
        With SMTP
            .Timeout = 10000
            .Send(MyMailMessage)
        End With
    End Sub

End Module
