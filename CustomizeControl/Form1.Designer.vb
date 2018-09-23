<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CtrlTextbox1 = New CustomizeControl.CtrlTextbox()
        Me.SuspendLayout()
        '
        'CtrlTextbox1
        '
        Me.CtrlTextbox1.aaAcceptNextControl = True
        Me.CtrlTextbox1.aaBorderLine = False
        Me.CtrlTextbox1.aaCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CtrlTextbox1.aaCustomFormat = "##,##0.########"
        Me.CtrlTextbox1.aaFontText = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.CtrlTextbox1.aaForeColor = System.Drawing.SystemColors.WindowText
        Me.CtrlTextbox1.aaForeColorLabel = System.Drawing.Color.Black
        Me.CtrlTextbox1.aaLabel = "Label"
        Me.CtrlTextbox1.aaLabelSize = 9
        Me.CtrlTextbox1.aaLabelWidth = 100
        Me.CtrlTextbox1.aaMultiline = False
        Me.CtrlTextbox1.aaOnlyNumber = True
        Me.CtrlTextbox1.aaReadOnly = False
        Me.CtrlTextbox1.aaSelectAllFocus = True
        Me.CtrlTextbox1.aaText = "0"
        Me.CtrlTextbox1.aaTextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.CtrlTextbox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlTextbox1.Location = New System.Drawing.Point(40, 47)
        Me.CtrlTextbox1.Name = "CtrlTextbox1"
        Me.CtrlTextbox1.Size = New System.Drawing.Size(350, 24)
        Me.CtrlTextbox1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 261)
        Me.Controls.Add(Me.CtrlTextbox1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrlTextbox1 As CtrlTextbox
End Class
