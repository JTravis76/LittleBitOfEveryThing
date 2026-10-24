Imports System.Collections
Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

<RunInstaller(True)> _
Partial Public Class ProcessInstaller
    Inherits Installer
    Private ReadOnly _serviceInstaller As ServiceInstaller
    Private ReadOnly _serviceProcessInstaller As ServiceProcessInstaller

    Private Const InternalServiceName As String = "NotifySvc"

    Public Sub New()
        _serviceInstaller = New ServiceInstaller() With { _
          .StartType = ServiceStartMode.Automatic, _
          .ServiceName = InternalServiceName, _
          .DisplayName = "Password Notify Service", _
          .Description = "Sends a mail to notify users when their password is due to expire" _
        }
        Installers.Add(_serviceInstaller)

        _serviceProcessInstaller = New ServiceProcessInstaller() With { _
          .Account = ServiceAccount.User _
        }
        Installers.Add(_serviceProcessInstaller)

        AddHandler Committed, AddressOf ProcessInstaller_Committed
    End Sub

    Private Sub ProcessInstaller_Committed(sender As Object, e As InstallEventArgs)
        Dim controller = New ServiceController() With { _
          .ServiceName = InternalServiceName _
        }
        controller.Start()
    End Sub

    Public Function GetContextParameter(key As String) As String
        Dim sValue As String
        Try
            sValue = Context.Parameters(key)
        Catch
            sValue = ""
        End Try
        Return sValue
    End Function

    Protected Overrides Sub OnBeforeInstall(savedState As IDictionary)
        MyBase.OnBeforeInstall(savedState)

        Dim username = GetContextParameter("user").Trim()
        Dim password = GetContextParameter("password").Trim()

        If Not String.IsNullOrEmpty(username) Then
            _serviceProcessInstaller.Username = username
        End If
        If Not String.IsNullOrEmpty(password) Then
            _serviceProcessInstaller.Password = password
        End If
    End Sub
End Class