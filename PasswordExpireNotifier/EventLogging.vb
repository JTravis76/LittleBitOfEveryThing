Imports System.Diagnostics


Public Class EventLogging
    Private Const Source As String = "NotifyService"
    Private Const LogName As String = "Password Notify Service"

    Public Shared Sub LogInformation(message As String)
        LogNotifyServiceEvent(message, EventLogEntryType.Information)
    End Sub

    Public Shared Sub LogWarning(message As String)
        LogNotifyServiceEvent(message, EventLogEntryType.Warning)
    End Sub

    Public Shared Sub LogError(message As String)
        LogNotifyServiceEvent(message, EventLogEntryType.[Error])
    End Sub

    Private Shared Sub LogNotifyServiceEvent(message As String, type As EventLogEntryType)
        If Not EventLog.SourceExists(Source) Then
            EventLog.CreateEventSource(Source, LogName)
        End If

        Dim eLog = New EventLog() With { _
            .Source = Source _
        }

        eLog.WriteEntry(message, type)
    End Sub
End Class


