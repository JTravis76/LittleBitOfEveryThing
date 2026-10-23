<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtRefreshSec = New System.Windows.Forms.NumericUpDown()
        Me.cbRefreshStartup = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnBackERT = New System.Windows.Forms.Button()
        Me.btnForeERT = New System.Windows.Forms.Button()
        Me.btnBackMuster = New System.Windows.Forms.Button()
        Me.btnForeMuster = New System.Windows.Forms.Button()
        Me.btnBackSelect = New System.Windows.Forms.Button()
        Me.btnForeSelect = New System.Windows.Forms.Button()
        Me.btnBackContr = New System.Windows.Forms.Button()
        Me.btnForeContr = New System.Windows.Forms.Button()
        Me.btnBackEmp = New System.Windows.Forms.Button()
        Me.btnForeEmp = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.lblMustered = New System.Windows.Forms.Label()
        Me.lblERT = New System.Windows.Forms.Label()
        Me.lblContractors = New System.Windows.Forms.Label()
        Me.lblEmployees = New System.Windows.Forms.Label()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.cbShowLegend = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtRefreshSec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(432, 224)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRefreshSec)
        Me.GroupBox1.Controls.Add(Me.cbRefreshStartup)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(205, 102)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dashboard Refresh"
        '
        'txtRefreshSec
        '
        Me.txtRefreshSec.Location = New System.Drawing.Point(114, 27)
        Me.txtRefreshSec.Name = "txtRefreshSec"
        Me.txtRefreshSec.Size = New System.Drawing.Size(61, 20)
        Me.txtRefreshSec.TabIndex = 3
        '
        'cbRefreshStartup
        '
        Me.cbRefreshStartup.AutoSize = True
        Me.cbRefreshStartup.Location = New System.Drawing.Point(113, 57)
        Me.cbRefreshStartup.Name = "cbRefreshStartup"
        Me.cbRefreshStartup.Size = New System.Drawing.Size(29, 17)
        Me.cbRefreshStartup.TabIndex = 2
        Me.cbRefreshStartup.Text = " "
        Me.cbRefreshStartup.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Refresh on startup:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Refresh in seconds:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBackERT)
        Me.GroupBox2.Controls.Add(Me.btnForeERT)
        Me.GroupBox2.Controls.Add(Me.btnBackMuster)
        Me.GroupBox2.Controls.Add(Me.btnForeMuster)
        Me.GroupBox2.Controls.Add(Me.btnBackSelect)
        Me.GroupBox2.Controls.Add(Me.btnForeSelect)
        Me.GroupBox2.Controls.Add(Me.btnBackContr)
        Me.GroupBox2.Controls.Add(Me.btnForeContr)
        Me.GroupBox2.Controls.Add(Me.btnBackEmp)
        Me.GroupBox2.Controls.Add(Me.btnForeEmp)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblSelected)
        Me.GroupBox2.Controls.Add(Me.lblMustered)
        Me.GroupBox2.Controls.Add(Me.lblERT)
        Me.GroupBox2.Controls.Add(Me.lblContractors)
        Me.GroupBox2.Controls.Add(Me.lblEmployees)
        Me.GroupBox2.Location = New System.Drawing.Point(223, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(358, 198)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Color Settings"
        '
        'btnBackERT
        '
        Me.btnBackERT.Location = New System.Drawing.Point(315, 101)
        Me.btnBackERT.Name = "btnBackERT"
        Me.btnBackERT.Size = New System.Drawing.Size(25, 23)
        Me.btnBackERT.TabIndex = 16
        Me.btnBackERT.Text = "..."
        Me.btnBackERT.UseVisualStyleBackColor = True
        '
        'btnForeERT
        '
        Me.btnForeERT.Location = New System.Drawing.Point(268, 101)
        Me.btnForeERT.Name = "btnForeERT"
        Me.btnForeERT.Size = New System.Drawing.Size(25, 23)
        Me.btnForeERT.TabIndex = 15
        Me.btnForeERT.Text = "..."
        Me.btnForeERT.UseVisualStyleBackColor = True
        '
        'btnBackMuster
        '
        Me.btnBackMuster.Location = New System.Drawing.Point(315, 129)
        Me.btnBackMuster.Name = "btnBackMuster"
        Me.btnBackMuster.Size = New System.Drawing.Size(25, 23)
        Me.btnBackMuster.TabIndex = 14
        Me.btnBackMuster.Text = "..."
        Me.btnBackMuster.UseVisualStyleBackColor = True
        '
        'btnForeMuster
        '
        Me.btnForeMuster.Location = New System.Drawing.Point(268, 129)
        Me.btnForeMuster.Name = "btnForeMuster"
        Me.btnForeMuster.Size = New System.Drawing.Size(25, 23)
        Me.btnForeMuster.TabIndex = 13
        Me.btnForeMuster.Text = "..."
        Me.btnForeMuster.UseVisualStyleBackColor = True
        '
        'btnBackSelect
        '
        Me.btnBackSelect.Location = New System.Drawing.Point(315, 157)
        Me.btnBackSelect.Name = "btnBackSelect"
        Me.btnBackSelect.Size = New System.Drawing.Size(25, 23)
        Me.btnBackSelect.TabIndex = 12
        Me.btnBackSelect.Text = "..."
        Me.btnBackSelect.UseVisualStyleBackColor = True
        '
        'btnForeSelect
        '
        Me.btnForeSelect.Location = New System.Drawing.Point(268, 157)
        Me.btnForeSelect.Name = "btnForeSelect"
        Me.btnForeSelect.Size = New System.Drawing.Size(25, 23)
        Me.btnForeSelect.TabIndex = 11
        Me.btnForeSelect.Text = "..."
        Me.btnForeSelect.UseVisualStyleBackColor = True
        '
        'btnBackContr
        '
        Me.btnBackContr.Location = New System.Drawing.Point(315, 73)
        Me.btnBackContr.Name = "btnBackContr"
        Me.btnBackContr.Size = New System.Drawing.Size(25, 23)
        Me.btnBackContr.TabIndex = 10
        Me.btnBackContr.Text = "..."
        Me.btnBackContr.UseVisualStyleBackColor = True
        '
        'btnForeContr
        '
        Me.btnForeContr.Location = New System.Drawing.Point(268, 73)
        Me.btnForeContr.Name = "btnForeContr"
        Me.btnForeContr.Size = New System.Drawing.Size(25, 23)
        Me.btnForeContr.TabIndex = 9
        Me.btnForeContr.Text = "..."
        Me.btnForeContr.UseVisualStyleBackColor = True
        '
        'btnBackEmp
        '
        Me.btnBackEmp.Location = New System.Drawing.Point(315, 41)
        Me.btnBackEmp.Name = "btnBackEmp"
        Me.btnBackEmp.Size = New System.Drawing.Size(25, 23)
        Me.btnBackEmp.TabIndex = 8
        Me.btnBackEmp.Text = "..."
        Me.btnBackEmp.UseVisualStyleBackColor = True
        '
        'btnForeEmp
        '
        Me.btnForeEmp.Location = New System.Drawing.Point(268, 41)
        Me.btnForeEmp.Name = "btnForeEmp"
        Me.btnForeEmp.Size = New System.Drawing.Size(25, 23)
        Me.btnForeEmp.TabIndex = 7
        Me.btnForeEmp.Text = "..."
        Me.btnForeEmp.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(312, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Back"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(267, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Fore"
        '
        'lblSelected
        '
        Me.lblSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelected.Location = New System.Drawing.Point(9, 153)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(243, 28)
        Me.lblSelected.TabIndex = 4
        Me.lblSelected.Text = "Selected"
        '
        'lblMustered
        '
        Me.lblMustered.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMustered.Location = New System.Drawing.Point(9, 125)
        Me.lblMustered.Name = "lblMustered"
        Me.lblMustered.Size = New System.Drawing.Size(243, 28)
        Me.lblMustered.TabIndex = 3
        Me.lblMustered.Text = "Mustered In"
        '
        'lblERT
        '
        Me.lblERT.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblERT.Location = New System.Drawing.Point(9, 97)
        Me.lblERT.Name = "lblERT"
        Me.lblERT.Size = New System.Drawing.Size(243, 28)
        Me.lblERT.TabIndex = 2
        Me.lblERT.Text = "ERT Members"
        '
        'lblContractors
        '
        Me.lblContractors.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContractors.Location = New System.Drawing.Point(9, 69)
        Me.lblContractors.Name = "lblContractors"
        Me.lblContractors.Size = New System.Drawing.Size(243, 28)
        Me.lblContractors.TabIndex = 1
        Me.lblContractors.Text = "Contractors"
        '
        'lblEmployees
        '
        Me.lblEmployees.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEmployees.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployees.Location = New System.Drawing.Point(9, 41)
        Me.lblEmployees.Name = "lblEmployees"
        Me.lblEmployees.Size = New System.Drawing.Size(243, 28)
        Me.lblEmployees.TabIndex = 0
        Me.lblEmployees.Text = "Plant Employees"
        '
        'cbShowLegend
        '
        Me.cbShowLegend.AutoSize = True
        Me.cbShowLegend.Location = New System.Drawing.Point(24, 137)
        Me.cbShowLegend.Name = "cbShowLegend"
        Me.cbShowLegend.Size = New System.Drawing.Size(138, 17)
        Me.cbShowLegend.TabIndex = 3
        Me.cbShowLegend.Text = "Show legend on startup"
        Me.cbShowLegend.UseVisualStyleBackColor = True
        '
        'dlgOptions
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(590, 265)
        Me.Controls.Add(Me.cbShowLegend)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtRefreshSec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRefreshSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbRefreshStartup As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblEmployees As System.Windows.Forms.Label
    Friend WithEvents lblERT As System.Windows.Forms.Label
    Friend WithEvents lblContractors As System.Windows.Forms.Label
    Friend WithEvents lblSelected As System.Windows.Forms.Label
    Friend WithEvents lblMustered As System.Windows.Forms.Label
    Friend WithEvents btnForeEmp As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents btnBackEmp As System.Windows.Forms.Button
    Friend WithEvents btnBackERT As System.Windows.Forms.Button
    Friend WithEvents btnForeERT As System.Windows.Forms.Button
    Friend WithEvents btnBackMuster As System.Windows.Forms.Button
    Friend WithEvents btnForeMuster As System.Windows.Forms.Button
    Friend WithEvents btnBackSelect As System.Windows.Forms.Button
    Friend WithEvents btnForeSelect As System.Windows.Forms.Button
    Friend WithEvents btnBackContr As System.Windows.Forms.Button
    Friend WithEvents btnForeContr As System.Windows.Forms.Button
    Friend WithEvents cbShowLegend As System.Windows.Forms.CheckBox

End Class
