Imports System.Collections.Generic
Imports System.Configuration
Imports System.Diagnostics
Imports System.ServiceProcess
Imports System.Threading
Imports System.Timers
Imports Timer = System.Timers.Timer

'to run service as a console app. See Sub Main is NotifyService.Designer.vb
'http:///makit.net/post/running-a-windows-service-as-a-console-application
Public Class NotifyService
    Inherits ServiceBase

    Private _timer As New Timer

    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Dim eventLogMessage As String = String.Format("Notify Service is starting :{0}", DateTime.Now)
        EventLogging.LogInformation(eventLogMessage)

        Dim interval As Double

        Try
            interval = Convert.ToDouble(ConfigurationManager.AppSettings("intervalInSeconds"))
            EventLogging.LogInformation(String.Format("Loaded configuration: Interval duration is {0} minutes", (interval / 60)))
        Catch exception As Exception
            interval = 3600

            eventLogMessage = String.Format("Loading configuration failed: Interval duration is {0} minutes", (interval / 60))
            eventLogMessage += String.Format(vbLf & "Message was: {0}", exception.Message)

            EventLogging.LogWarning(eventLogMessage)
        End Try

        interval = interval * 1000

        _timer.Interval = interval
        AddHandler _timer.Elapsed, AddressOf TimerTick
        _timer.Start()

        'Run through once upon startup
        Dim workerThread = New System.Threading.Thread(AddressOf NotifyUsers) With { _
          .IsBackground = True _
        }
        workerThread.Start()
    End Sub

    Protected Overrides Sub OnStop()
        MyBase.OnStop()
        Dim eventLogMessage As String = "Password notify service has stopped: " + DateTime.Now
        EventLogging.LogInformation(eventLogMessage)
    End Sub

    Protected Overrides Sub OnPause()
        MyBase.OnPause()
        _timer.[Stop]()
        EventLogging.LogWarning("Paused")
    End Sub

    Protected Overrides Sub OnContinue()
        MyBase.OnContinue()
        _timer.Start()
        EventLogging.LogInformation("Resumed")
    End Sub

    Private Sub TimerTick(sender As Object, e As ElapsedEventArgs)
        Dim workerThread = New System.Threading.Thread(AddressOf NotifyUsers) With { _
         .IsBackground = True _
        }
        workerThread.Start()
    End Sub

    Private Sub NotifyUsers()
        Dim userBL = New UserBL()
        Dim usersToBeMailed As List(Of User) = userBL.GetAllUsersWhosePasswordsWillExpire

        Dim eventLogMessage As String = String.Format("Number of users to be mailed is {0}", usersToBeMailed.Count)
        EventLogging.LogInformation(eventLogMessage)


        Dim sb As New System.Text.StringBuilder
        'sb.Append(IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath & "\ReportTemplate.txt"))
        sb.Append("<html><head><title></title></head><body><center><table style='border:1px solid #C2D4DA;'>").AppendLine()
        sb.Append("<thead><tr style='background-color:#C0DDE0;'><th>Display Name</th><th>Expired Date</th><th>Last Logon</th></tr></thead><tbody>").AppendLine()

        For Each user As User In usersToBeMailed
            userBL.MailUser(user)
            sb.AppendLine("<tr><td>" & user.DisplayName & "</td><td>" & DateTime.Now.AddDays(user.PasswordExpiresInDays).ToString("D", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")))
            If user.UserLastLogon = 0 Then
                sb.AppendLine("</td><td>NEVER</td></tr>")
            Else
                sb.AppendLine("</td><td>" & DateTime.FromFileTime(user.UserLastLogon).ToString & "</td></tr>")
                '.ToString("dd/MM/yy") = 01/10/08
            End If
        Next

        sb.AppendLine("</tbody></table></center></body></html>")

        'create and email report to Debug user
        Dim debugEmailUser As String = ConfigurationManager.AppSettings("DebugEmailUser")
        Dim serviceMailAddress As String = ConfigurationManager.AppSettings("ServiceMailAddress")

        Dim message = New System.Net.Mail.MailMessage() With { _
          .Body = sb.ToString(), _
          .From = New System.Net.Mail.MailAddress(serviceMailAddress, "Password notification service"), _
          .Subject = "Network Password Expiration Report" _
        }

        message.IsBodyHtml = True
        message.[To].Add(debugEmailUser)

        Dim client = New System.Net.Mail.SmtpClient() With { _
          .Credentials = System.Net.CredentialCache.DefaultNetworkCredentials _
        }

        'client.SendCompleted += ClientOnSendCompleted;
        client.SendAsync(message, debugEmailUser)
    End Sub

End Class



