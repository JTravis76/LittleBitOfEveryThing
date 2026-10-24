Imports System.Configuration
Imports System.ServiceProcess
Imports System.Threading
Imports System.Timers
Imports Timer = System.Timers.Timer

Imports System.IO
Imports System.Text
Imports System.Net.Mail

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser

Public Class Service1
    Inherits ServiceBase

    Private _timer As New Timer

    Public Sub New()
        InitializeComponent()
    End Sub

#Region "Service Controls"

    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim eventLogMessage As String = String.Format("Service is starting :{0}", DateTime.Now)
        EventLogging.LogInformation(eventLogMessage)

        Dim interval As Double

        Try
            interval = Convert.ToDouble(ConfigurationManager.AppSettings("intervalInSeconds"))
            EventLogging.LogInformation(String.Format("Loaded configuration: Interval duration is {0} minutes", (interval / 60)))
        Catch exception As Exception
            interval = 300

            eventLogMessage = String.Format("Loading configuration failed: Interval duration is {0} minutes", (interval / 60))
            eventLogMessage += String.Format(vbLf & "Message was: {0}", exception.Message)

            EventLogging.LogWarning(eventLogMessage)
        End Try

        interval = interval * 1000

        _timer.Interval = interval
        AddHandler _timer.Elapsed, AddressOf TimerTick
        _timer.Start()

        'Run through once upon startup
        Dim workerThread = New System.Threading.Thread(AddressOf ProcessPDFs) With { _
          .IsBackground = True _
        }
        workerThread.Start()
    End Sub

    Protected Overrides Sub OnStop()
        MyBase.OnStop()
        Dim eventLogMessage As String = "Service has stopped: " + DateTime.Now
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

#End Region

    Private Sub TimerTick(sender As Object, e As ElapsedEventArgs)
        Dim workerThread = New System.Threading.Thread(AddressOf ProcessPDFs) With { _
         .IsBackground = True _
        }
        workerThread.Start()
    End Sub

    Private Sub ProcessPDFs()
        Try
            'Gets list of files from desired directory
            Dim dirList() As String = Directory.GetFiles(ConfigurationManager.AppSettings("SourceDir").ToString)

            EventLogging.LogInformation("Processing PDFs reports: " & dirList.Count)

            Dim sData() As String
            Dim sec As Integer = 1

            'loops through each file in array and check if PDF ext
            For Each f In dirList
                sData = Split(f.ToString, ".")
                If sData(UBound(sData)).ToUpper = "PDF" Then
                    'Console.WriteLine(f.ToString)

                    Dim text As New StringBuilder

                    If File.Exists(f) Then
                        'Read and extract text from PDF
                        Dim pdfReader As New PdfReader(f)

                        For page = 1 To pdfReader.NumberOfPages
                            Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy
                            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)))
                            text.Append(currentText)
                        Next

                        pdfReader.Close()

                        If text.ToString = "<End of Report>" Then
                            EventLogging.LogInformation("Deleted empty file. " & f)
                            File.Delete(f)
                        Else
                            'Parse text and rename PDF file
                            Dim s As String = ""

                            'get Report Title
                            s = Mid(text.ToString, 9, InStr(text.ToString, "Page ") - 9).Trim.Replace(" ", "_").Replace(",", "")
                            s += "-"

                            'Get Job Name and Product Name
                            Dim sJob, sProduct As String
                            sData = Split(s, "_")
                            sJob = sData(0).ToString

                            Select Case sJob
                                Case "D802"
                                    sProduct = Left(sData(2).ToString, Len(sData(2).ToString) - 1)
                                Case Else
                                    sProduct = sData(1).ToString
                            End Select



                            'Gets Date/Time with lot was created.
                            Dim rDate As Date
                            Select Case sJob
                                Case "3507FA", "D802"
                                    rDate = CDate(Mid(text.ToString, InStr(text.ToString, "#1") + 3, 16))
                                Case Else
                                    rDate = CDate(Mid(text.ToString, InStr(text.ToString, "Date/Time:") + 11, 16))
                            End Select

                            s += String.Format("{0:yyyyMMdd_HHmmss}", rDate.AddSeconds(sec))
                            sec += 1

                            'Set Dest then add sub-directories as needed
                            Dim sDest As String = ConfigurationManager.AppSettings("DestDir").ToString

                            If Not Directory.Exists(sDest & "\" & sProduct & "\" & sJob) Then
                                Directory.CreateDirectory(sDest & "\" & sProduct & "\" & sJob)
                            End If

                            'assign new directory path
                            sDest = sDest & "\" & sProduct & "\" & sJob

                            FileCopy(f, sDest & "\" & s & ".pdf")
                            File.Delete(f)
                            EmailFiles(s, sDest & "\" & s & ".pdf")
                        End If

                        text.Clear()
                    End If
                End If
            Next
        Catch ex As Exception
            EventLogging.LogError(ex.Message)
        End Try

    End Sub

    Private Sub EmailFiles(ByVal subject As String, ByVal FileLink As String)
        Dim EmailAddress As String = ConfigurationManager.AppSettings("EmailAddress")
        Dim serviceMailAddress As String = ConfigurationManager.AppSettings("ServiceMailAddress")

        Dim message = New MailMessage() With { _
          .Body = MsgBody(FileLink), _
          .From = New MailAddress(serviceMailAddress, "Labsoft PDF Report"), _
          .Subject = subject _
        }

        'loop through email list and add address to message
        Dim sData() As String = Split(EmailAddress, ";")
        For a = 0 To UBound(sData)
            message.To.Add(sData(a).ToString)
        Next

        Dim data As New Attachment(FileLink)
        message.Attachments.Add(data)

        Dim client = New SmtpClient() With { _
          .Credentials = System.Net.CredentialCache.DefaultNetworkCredentials _
        }

        AddHandler client.SendCompleted, AddressOf ClientOnSendCompleted
        client.SendAsync(message, EmailAddress)
    End Sub

    Private Function MsgBody(FileLink As String) As String
        Dim sb As New StringBuilder
        sb.AppendLine("Labsoft report ready to be viewed. Please see attachment.").AppendLine()
        sb.AppendLine(FileLink.Replace(" ", "%20"))

        Return sb.ToString
    End Function

    Private Sub ClientOnSendCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
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
End Class
