Imports System.Windows.Forms

Public Class dlgOptions

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnForeEmp_Click(sender As Object, e As EventArgs) Handles btnForeEmp.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblEmployees.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnBackEmp_Click(sender As Object, e As EventArgs) Handles btnBackEmp.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblEmployees.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnForeContr_Click(sender As Object, e As EventArgs) Handles btnForeContr.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblContractors.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnBackContr_Click(sender As Object, e As EventArgs) Handles btnBackContr.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblContractors.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnForeERT_Click(sender As Object, e As EventArgs) Handles btnForeERT.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblERT.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnBackERT_Click(sender As Object, e As EventArgs) Handles btnBackERT.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblERT.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnForeMuster_Click(sender As Object, e As EventArgs) Handles btnForeMuster.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblMustered.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnBackMuster_Click(sender As Object, e As EventArgs) Handles btnBackMuster.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblMustered.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnForeSelect_Click(sender As Object, e As EventArgs) Handles btnForeSelect.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblSelected.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnBackSelect_Click(sender As Object, e As EventArgs) Handles btnBackSelect.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblSelected.BackColor = ColorDialog1.Color
        End If
    End Sub
End Class
