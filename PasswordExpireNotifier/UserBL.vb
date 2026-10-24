Imports System.ComponentModel
Imports System.Configuration
Imports System.DirectoryServices
Imports System.Globalization
Imports System.Net
Imports System.Net.Mail
Imports System.Text


Public Class UserBL
    Public Function GetAllUsersWhosePasswordsWillExpire() As List(Of User)
        Dim ldapDomain As String, eventMessage As String

        Try
            ldapDomain = ConfigurationManager.AppSettings("DomainString")

            eventMessage = "LDAP string being used is " & ldapDomain
            EventLogging.LogInformation(eventMessage)
        Catch generatedExceptionName As Exception
            eventMessage = "Unable to retrieve LDAP string: Using default domain version"
            EventLogging.LogError(eventMessage)

            Using rootDSE = New DirectoryEntry("LDAP://RootDSE")
                ldapDomain = rootDSE.Properties("defaultNamingContext").Value.ToString()
            End Using
        End Try

        EventLogging.LogInformation("Current naming context is " & ldapDomain)

        Dim limitInDays As Double

        Try
            limitInDays = Convert.ToDouble(ConfigurationManager.AppSettings("PasswordLimitInDays"))
            eventMessage = "Password limit in days is " & limitInDays
            EventLogging.LogInformation(eventMessage)
        Catch generatedExceptionName As Exception
            eventMessage = "Unable to set the limit in days. Setting to 14.0 by default"
            EventLogging.LogWarning(eventMessage)
            limitInDays = 14.0
        End Try


        Dim domain = New DirectoryEntry(ldapDomain, ConfigurationManager.AppSettings("DomainAdmin"), ConfigurationManager.AppSettings("DomainPwd"))
        Dim maxPasswordAge As Double = GetMaxPasswordAge(domain)

        '(homeMDB=*) 'removed 7/14/2014 due to environment NOt having on-premise Exchange server
        ' added (mail=*) to get all accounts with mailboxes only
        '66064 Enabled - DONT_EXPIRE_PASSWORD - LOCKOUT
        '65536 - DONT_EXPIRE_PASSWORD
        Dim deSearch = New DirectorySearcher(domain) With { _
          .SearchRoot = domain, _
          .Filter = "(&(mail=*)(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=66064))(!(userAccountControl:1.2.840.113556.1.4.803:=65536)))", _
          .SearchScope = SearchScope.Subtree, _
          .PageSize = 1000 _
        }

        deSearch.PropertiesToLoad.Add("displayName")
        deSearch.PropertiesToLoad.Add("mail")
        deSearch.PropertiesToLoad.Add("pwdLastSet")
        deSearch.PropertiesToLoad.Add("lastLogon")

        Dim results As SearchResultCollection = deSearch.FindAll()

        Dim users As List(Of User) = (From searchResult In results _
                                      Select New User With { _
             .DisplayName = searchResult.Properties("displayName")(0).ToString(), _
             .PasswordExpiryDuration = maxPasswordAge, _
             .MailAddress = searchResult.Properties("mail")(0).ToString(), _
             .PasswordLastSet = GetPasswordLastSetAsDate(searchResult.Properties("pwdLastSet")(0)), _
             .UserLastLogon = searchResult.Properties("lastLogon")(0) _
           }).ToList()


        Dim usersThatWillGetEmails As List(Of User) = users.Where(Function(user) user.PasswordExpiresInDays <= limitInDays AndAlso user.PasswordExpiresInDays > 0).ToList()

        usersThatWillGetEmails.Sort(Function(a, b) [String].CompareOrdinal(a.DisplayName, b.DisplayName))

        Return usersThatWillGetEmails
    End Function

    Public Function GetPasswordLastSetAsDate(o As Object) As DateTime
        Dim ticks As Long = GetAbsoluteValue(o)
        Return If(ticks > 0, DateTime.FromFileTime(ticks), DateTime.Now)
    End Function

    Public Function GetMaxPasswordAge(root As DirectoryEntry) As Double
        Dim ds = New DirectorySearcher() With { _
          .SearchRoot = root, _
          .Filter = "(objectClass=domainDNS)", _
          .SearchScope = SearchScope.Base _
        }

        Dim search As SearchResult = ds.FindOne()

        Dim ticks As Long = GetAbsoluteValue(search.Properties("maxPwdAge")(0))
        Return If(ticks > 0, TimeSpan.FromTicks(ticks).TotalDays, TimeSpan.MaxValue.TotalDays)
    End Function

    Private Shared Function GetAbsoluteValue(longInt As Object) As Long
        Dim val = CLng(longInt)
        Return If(val = Long.MinValue, Long.MaxValue, Math.Abs(CLng(longInt)))
    End Function

    Public Sub MailUser(user As User)
        Dim isDebug As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("isDebug"))
        Dim debugEmailUser As String = ConfigurationManager.AppSettings("DebugEmailUser")
        Dim serviceMailAddress As String = ConfigurationManager.AppSettings("ServiceMailAddress")

        Dim message = New MailMessage() With { _
          .Body = MailMessageBody(user), _
          .From = New MailAddress(serviceMailAddress, "Password notification service"), _
          .Subject = "Network Password Expiration Notice" _
        }

        message.To.Add(IIf(isDebug, debugEmailUser, user.MailAddress)) '
        'message.[To].Add(If(isDebug, debugEmailUser, user.MailAddress))

        Dim client = New SmtpClient() With { _
          .Credentials = CredentialCache.DefaultNetworkCredentials _
        }

        AddHandler client.SendCompleted, AddressOf ClientOnSendCompleted
        client.SendAsync(message, user.MailAddress) '
    End Sub

    Private Sub ClientOnSendCompleted(sender As Object, e As AsyncCompletedEventArgs)
        Dim mailSentTo = DirectCast(e.UserState, String)

        Dim eventMessage As String

        If e.Cancelled Then
            eventMessage = "Mail sending was cancelled while sending to " & mailSentTo
            EventLogging.LogWarning(eventMessage)
        End If
        If e.[Error] IsNot Nothing Then
            eventMessage = "Error sending mail to " & mailSentTo
            eventMessage += vbLf & " Error " & Convert.ToString(e.[Error].InnerException)
            EventLogging.LogError(eventMessage)
        Else
            eventMessage = "Message sent to " & mailSentTo
            EventLogging.LogInformation(eventMessage)
        End If
    End Sub

    Private Shared Function MailMessageBody(user As User) As String
        Dim numberOfDays As Integer = Convert.ToInt16(user.PasswordExpiresInDays)

        Dim dayValue As String = numberOfDays.ToString(CultureInfo.InvariantCulture) & " days from today"

        If numberOfDays = 0 Then
            dayValue = "Today"
        End If
        If numberOfDays = 1 Then
            dayValue = "Tomorrow"
        End If


        Dim sb As New StringBuilder
        sb.Append(IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath & "\templates\EmailTemplate.txt"))
        sb.Replace("<%DisplayName%>", user.DisplayName.ToUpper)
        sb.Replace("<%PasswordExpiresInDays%>", DateTime.Now.AddDays(user.PasswordExpiresInDays).ToString("D", CultureInfo.CreateSpecificCulture("en-US")))

        Return sb.ToString
    End Function
End Class


