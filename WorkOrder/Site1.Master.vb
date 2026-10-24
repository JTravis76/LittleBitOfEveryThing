Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Page.User.Identity.IsAuthenticated Then
                lblLoginName.Text = HttpContext.Current.Session("UserName").ToString

                lblLoginName.Visible = True
                btnLogout.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        If Not IsNothing(Session) Then
            Session.Clear()
            Session.Abandon()
        End If

        If Page.User.Identity.IsAuthenticated Then
            FormsAuthentication.SignOut()
            FormsAuthentication.RedirectToLoginPage()
        End If
    End Sub

End Class