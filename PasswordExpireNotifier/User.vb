Public Class User
    Private m_DisplayName As String
    Private m_PasswordLastSet As DateTime
    Private m_MailAddress As String
    Private m_PasswordExpiryDuration As Double
    Private m_LastLogon As Int64

    Public Property DisplayName() As String
        Get
            Return m_DisplayName
        End Get
        Set(value As String)
            m_DisplayName = value
        End Set
    End Property

    Public Property PasswordLastSet() As DateTime
        Get
            Return m_PasswordLastSet
        End Get
        Set(value As DateTime)
            m_PasswordLastSet = Value
        End Set
    End Property

    Public Property MailAddress() As String
        Get
            Return m_MailAddress
        End Get
        Set(value As String)
            m_MailAddress = Value
        End Set
    End Property

    Public Property PasswordExpiryDuration() As Double
        Get
            Return m_PasswordExpiryDuration
        End Get
        Set(value As Double)
            m_PasswordExpiryDuration = Value
        End Set
    End Property

    Public ReadOnly Property PasswordExpiresInDays() As Double
        Get
            Dim time As TimeSpan = DateTime.Now - PasswordLastSet
            Return PasswordExpiryDuration - time.TotalDays
        End Get
    End Property

    Public Property UserLastLogon() As Long
        Get
            Return m_LastLogon
        End Get
        Set(value As Long)
            m_LastLogon = value
        End Set
    End Property
End Class

