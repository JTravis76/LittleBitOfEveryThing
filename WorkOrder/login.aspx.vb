Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("action") = "SessionExpired" Then
            lblMessage.Visible = True
        Else
            lblMessage.Visible = False
        End If

        If Not IsPostBack Then
            If Debugger.IsAttached Then
                txtUsername.Text = "admin"
            End If
        End If

        txtUsername.Focus()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            If Authenticate(txtUsername.Text, txtPassword.Text) Then

                FormsAuthentication.SetAuthCookie(txtUsername.Text, False)
                'redirects to return URL
                Try
                    Response.Redirect(Request.QueryString("ReturnUrl").ToString)
                Catch ex As Exception
                Finally
                    Response.Redirect("/default.aspx")
                End Try

            Else
                lblFailureText.Visible = True
            End If
        End If
    End Sub

    Private Function Authenticate(ByVal UserName As String, ByVal Password As String) As Boolean
        Dim Valid As Boolean = False
        Dim isLDAP As Boolean = False

        'check for default administrator account
        If UserName.ToLower = "admin" And Password = "admin" Then
            Valid = True
        Else
            If ConfigurationManager.AppSettings("LDAPServer").Trim <> "" Then
                isLDAP = ValidateLDAPLogin(UserName, Password)
                If isLDAP Then Valid = True
            End If
        End If

        If Valid Then
            Dim ds As New dsWorkOrder
            Dim da As New dsWorkOrderTableAdapters.UsersTableAdapter
            da.FillbyUserName(ds.Users, UserName)

            If ds.Users.Rows.Count > 0 Then
                HttpContext.Current.Session("IsStoreroom") = CBool(ds.Users.Rows(0)("IsStoreroom"))
            Else
                HttpContext.Current.Session("IsStoreroom") = False
            End If

            HttpContext.Current.Session("UserName") = UserName.ToLower
        End If


        Return Valid
    End Function
End Class