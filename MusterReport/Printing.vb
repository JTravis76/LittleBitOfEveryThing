
'# How To Use:
'
'
'
'


Module Printing

    ' for PrintDialog, PrintPreviewDialog and PrintDocument:
    Public prnDialog As System.Windows.Forms.PrintDialog
    Public prnPreview As System.Windows.Forms.PrintPreviewDialog
    Public prnDocument As System.Drawing.Printing.PrintDocument

    ' for Invoice Head:
    Private InvTitle As String
    Private InvSubTitle1 As String
    Private InvSubTitle2 As String
    Private InvSubTitle3 As String
    Private InvImage As String

    ' for Report:
    Private CurrentY As Integer
    Private CurrentX As Integer
    Private leftMargin As Integer
    Private rightMargin As Integer
    Private topMargin As Integer
    Private bottomMargin As Integer
    Private InvoiceWidth As Integer
    Private InvoiceHeight As Integer
    Private CustomerName As String
    Private CustomerCity As String
    Private SellerName As String
    Private SaleID As String
    Private SaleDate As String
    Private SaleFreight As Decimal
    Private SubTotal As Decimal
    Private InvoiceTotal As Decimal
    Private ReadInvoice As Boolean
    Private AmountPosition As Integer

    ' Font and Color:------------------
    ' Title Font
    Private InvTitleFont As Font = New Font("Arial", 24, FontStyle.Regular)
    ' Title Font height
    Private InvTitleHeight As Integer
    ' SubTitle Font
    Private InvSubTitleFont As Font = New Font("Arial", 14, FontStyle.Regular)
    ' SubTitle Font height
    Private InvSubTitleHeight As Integer
    ' Invoice Font
    Private InvoiceFont As Font = New Font("Arial", 12, FontStyle.Regular)
    ' Invoice Font height
    Private InvoiceFontHeight As Integer
    ' Blue Color
    Private BlueBrush As SolidBrush = New SolidBrush(Color.Blue)
    ' Red Color
    Private RedBrush As SolidBrush = New SolidBrush(Color.Red)
    ' Black Color
    Private BlackBrush As SolidBrush = New SolidBrush(Color.Black)

    ' Result of the Event 'PrintPage'
    Public Sub prnDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        leftMargin = Convert.ToInt32(e.MarginBounds.Left)
        rightMargin = Convert.ToInt32(e.MarginBounds.Right)
        topMargin = Convert.ToInt32(e.MarginBounds.Top)
        bottomMargin = Convert.ToInt32(e.MarginBounds.Bottom)
        InvoiceWidth = Convert.ToInt32(e.MarginBounds.Width)
        InvoiceHeight = Convert.ToInt32(e.MarginBounds.Height)

        SetReportHead(e.Graphics) ' Draw Invoice Head
        'SetOrderData(e.Graphics) ' Draw Order Data
        'SetInvoiceData(e.Graphics, e) ' Draw Invoice Data

    End Sub

    Private Sub ReadInvoiceHead()
        'Titles and Image of invoice:
        InvTitle = "International Food Company"
        InvSubTitle1 = "23 Abo Naga street"
        InvSubTitle2 = "Cairo, Egypt"
        InvSubTitle3 = "Phone 2233445566"
        InvImage = Application.StartupPath & "\Images\logo600.png"
    End Sub

    Private Sub SetReportHead(ByVal g As Graphics)
        'ReadInvoiceHead()
        'Titles and Image of invoice:
        InvTitle = "International Food Company"
        InvSubTitle1 = "23 Abo Naga street"
        InvSubTitle2 = "Cairo, Egypt"
        InvSubTitle3 = "Phone 2233445566"
        InvImage = Application.StartupPath & "\Images\logo600.png"

        CurrentY = topMargin
        CurrentX = leftMargin
        Dim ImageHeight As Integer = 0

        ' Draw Invoice image:
        If (System.IO.File.Exists(InvImage)) Then
            Dim oInvImage As Bitmap = New Bitmap(InvImage)
            ' Set Image Left to center Image:
            Dim xImage As Integer = CurrentX + (InvoiceWidth - oInvImage.Width) / 2
            ImageHeight = oInvImage.Height ' Get Image Height
            g.DrawImage(oInvImage, xImage, CurrentY)
        End If

        InvTitleHeight = Convert.ToInt32(InvTitleFont.GetHeight(g))
        InvSubTitleHeight = Convert.ToInt32(InvSubTitleFont.GetHeight(g))
        ' Get Titles Length:
        Dim lenInvTitle As Integer = Convert.ToInt32(g.MeasureString(InvTitle, InvTitleFont).Width)
        Dim lenInvSubTitle1 As Integer = Convert.ToInt32(g.MeasureString(InvSubTitle1, InvSubTitleFont).Width)
        Dim lenInvSubTitle2 As Integer = Convert.ToInt32(g.MeasureString(InvSubTitle2, InvSubTitleFont).Width)
        Dim lenInvSubTitle3 As Integer = Convert.ToInt32(g.MeasureString(InvSubTitle3, InvSubTitleFont).Width)
        ' Set Titles Left:
        Dim xInvTitle As Integer = CurrentX + (InvoiceWidth - lenInvTitle) / 2
        Dim xInvSubTitle1 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2
        Dim xInvSubTitle2 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2
        Dim xInvSubTitle3 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2

        ' Draw Invoice Head:
        If (InvTitle <> "") Then
            CurrentY = CurrentY + ImageHeight
            g.DrawString(InvTitle, InvTitleFont, BlueBrush, xInvTitle, CurrentY)
        End If
        If (InvSubTitle1 <> "") Then
            CurrentY = CurrentY + InvTitleHeight
            g.DrawString(InvSubTitle1, InvSubTitleFont, BlueBrush, xInvSubTitle1, CurrentY)
        End If
        If (InvSubTitle2 <> "") Then
            CurrentY = CurrentY + InvSubTitleHeight
            g.DrawString(InvSubTitle2, InvSubTitleFont, BlueBrush, xInvSubTitle2, CurrentY)
        End If
        If (InvSubTitle3 <> "") Then
            CurrentY = CurrentY + InvSubTitleHeight
            g.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, xInvSubTitle3, CurrentY)
        End If

        ' Draw line:
        CurrentY = CurrentY + InvSubTitleHeight + 8
        g.DrawLine(New Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin, CurrentY)
    End Sub

    Private Sub SetOrderData(ByVal g As Graphics)
        ' Set Company Name, City, Salesperson, Order ID and Order Date
        Dim FieldValue As String = ""
        InvoiceFontHeight = Convert.ToInt32(InvoiceFont.GetHeight(g))

        ' Set Company Name:
        CurrentX = leftMargin
        CurrentY = CurrentY + 8
        FieldValue = "Company Name: " & CustomerName
        g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)
        ' Set City:
        CurrentX = CurrentX + Convert.ToInt32(g.MeasureString(FieldValue, InvoiceFont).Width) + 16
        FieldValue = "City: " & CustomerCity
        g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)
        ' Set Salesperson:
        CurrentX = leftMargin
        CurrentY = CurrentY + InvoiceFontHeight
        FieldValue = "Salesperson: " & SellerName
        g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)
        ' Set Order ID:
        CurrentX = leftMargin
        CurrentY = CurrentY + InvoiceFontHeight
        FieldValue = "Order ID: " & SaleID
        g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)
        ' Set Order Date:
        CurrentX = CurrentX + Convert.ToInt32(g.MeasureString(FieldValue, InvoiceFont).Width) + 16
        FieldValue = "Order Date: " & SaleDate
        g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)

        ' Draw line:
        CurrentY = CurrentY + InvoiceFontHeight + 8
        g.DrawLine(New Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY)
    End Sub
End Module
