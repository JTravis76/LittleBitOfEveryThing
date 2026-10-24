Public Class reservation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Load selected record into form
            If Not Request.QueryString("id") = Nothing Then

                Using Conn As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("db").ToString)
                    Conn.Open()

                    Dim sQuery As String = "SELECT WorkOrderNumber, Material, ShortText, Quantity, [Status] 
                        FROM MaterialList 
                        WHERE [Status]='Waiting' 
                        AND Material=" & Request.QueryString("id").ToString

                    Dim Adp As New SqlClient.SqlDataAdapter
                    Dim Cmd As New SqlClient.SqlCommand(sQuery, Conn)
                    Adp.SelectCommand = Cmd

                    Dim dt As New DataTable("Query")
                    Adp.Fill(dt)

                    gvReservation.DataSource = dt
                    gvReservation.DataBind()

                    Adp.Dispose()
                    Conn.Close()
                End Using

            End If
        End If
    End Sub

End Class