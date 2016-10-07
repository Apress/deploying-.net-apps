<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Card
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.picCard = New System.Windows.Forms.PictureBox
        CType(Me.picCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picCard
        '
        Me.picCard.AutoSize = True
        Me.picCard.Location = New System.Drawing.Point(0, 0)
        Me.picCard.Name = "picCard"
        Me.picCard.Size = New System.Drawing.Size(48, 55)
        Me.picCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picCard.TabIndex = 0
        Me.picCard.TabStop = False
        '
        'CardUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picCard)
        Me.Name = "CardUI"
        CType(Me.picCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picCard As System.Windows.Forms.PictureBox

End Class
