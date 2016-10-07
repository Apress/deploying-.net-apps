<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmStatistics
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSessionPct = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.lblTotalPct = New System.Windows.Forms.Label
        Me.lblSessionWon = New System.Windows.Forms.Label
        Me.lblSessionLost = New System.Windows.Forms.Label
        Me.lblTotalWon = New System.Windows.Forms.Label
        Me.lblTotalLost = New System.Windows.Forms.Label
        Me.lblMaxWinStreak = New System.Windows.Forms.Label
        Me.lblMaxLoseStreak = New System.Windows.Forms.Label
        Me.lblCurrentStreak = New System.Windows.Forms.Label
        Me.lblStreakType = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This session"
        '
        'lblSessionPct
        '
        Me.lblSessionPct.Location = New System.Drawing.Point(135, 18)
        Me.lblSessionPct.Name = "lblSessionPct"
        Me.lblSessionPct.Size = New System.Drawing.Size(61, 13)
        Me.lblSessionPct.TabIndex = 1
        Me.lblSessionPct.Text = "0%"
        Me.lblSessionPct.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "won:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "lost:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "lost:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(33, 90)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "won:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Total"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(33, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "loses:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(33, 143)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "wins:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Streaks"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(33, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "current:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(34, 190)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 11
        Me.btnOK.Text = "OK"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(120, 190)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(64, 23)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "&Clear"
        '
        'lblTotalPct
        '
        Me.lblTotalPct.Location = New System.Drawing.Point(135, 73)
        Me.lblTotalPct.Name = "lblTotalPct"
        Me.lblTotalPct.Size = New System.Drawing.Size(61, 13)
        Me.lblTotalPct.TabIndex = 13
        Me.lblTotalPct.Text = "0%"
        Me.lblTotalPct.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSessionWon
        '
        Me.lblSessionWon.Location = New System.Drawing.Point(96, 35)
        Me.lblSessionWon.Name = "lblSessionWon"
        Me.lblSessionWon.Size = New System.Drawing.Size(61, 13)
        Me.lblSessionWon.TabIndex = 14
        Me.lblSessionWon.Text = "0"
        Me.lblSessionWon.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSessionLost
        '
        Me.lblSessionLost.Location = New System.Drawing.Point(96, 48)
        Me.lblSessionLost.Name = "lblSessionLost"
        Me.lblSessionLost.Size = New System.Drawing.Size(61, 13)
        Me.lblSessionLost.TabIndex = 15
        Me.lblSessionLost.Text = "0"
        Me.lblSessionLost.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalWon
        '
        Me.lblTotalWon.Location = New System.Drawing.Point(96, 90)
        Me.lblTotalWon.Name = "lblTotalWon"
        Me.lblTotalWon.Size = New System.Drawing.Size(61, 13)
        Me.lblTotalWon.TabIndex = 16
        Me.lblTotalWon.Text = "0"
        Me.lblTotalWon.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalLost
        '
        Me.lblTotalLost.Location = New System.Drawing.Point(96, 103)
        Me.lblTotalLost.Name = "lblTotalLost"
        Me.lblTotalLost.Size = New System.Drawing.Size(61, 13)
        Me.lblTotalLost.TabIndex = 17
        Me.lblTotalLost.Text = "0"
        Me.lblTotalLost.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaxWinStreak
        '
        Me.lblMaxWinStreak.Location = New System.Drawing.Point(96, 143)
        Me.lblMaxWinStreak.Name = "lblMaxWinStreak"
        Me.lblMaxWinStreak.Size = New System.Drawing.Size(61, 13)
        Me.lblMaxWinStreak.TabIndex = 18
        Me.lblMaxWinStreak.Text = "0"
        Me.lblMaxWinStreak.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaxLoseStreak
        '
        Me.lblMaxLoseStreak.Location = New System.Drawing.Point(96, 156)
        Me.lblMaxLoseStreak.Name = "lblMaxLoseStreak"
        Me.lblMaxLoseStreak.Size = New System.Drawing.Size(61, 13)
        Me.lblMaxLoseStreak.TabIndex = 19
        Me.lblMaxLoseStreak.Text = "0"
        Me.lblMaxLoseStreak.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCurrentStreak
        '
        Me.lblCurrentStreak.Location = New System.Drawing.Point(96, 169)
        Me.lblCurrentStreak.Name = "lblCurrentStreak"
        Me.lblCurrentStreak.Size = New System.Drawing.Size(61, 13)
        Me.lblCurrentStreak.TabIndex = 20
        Me.lblCurrentStreak.Text = "0"
        Me.lblCurrentStreak.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStreakType
        '
        Me.lblStreakType.Location = New System.Drawing.Point(163, 169)
        Me.lblStreakType.Name = "lblStreakType"
        Me.lblStreakType.Size = New System.Drawing.Size(61, 13)
        Me.lblStreakType.TabIndex = 21
        '
        'frmStatistics
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(225, 219)
        Me.Controls.Add(Me.lblStreakType)
        Me.Controls.Add(Me.lblCurrentStreak)
        Me.Controls.Add(Me.lblMaxLoseStreak)
        Me.Controls.Add(Me.lblMaxWinStreak)
        Me.Controls.Add(Me.lblTotalLost)
        Me.Controls.Add(Me.lblTotalWon)
        Me.Controls.Add(Me.lblSessionLost)
        Me.Controls.Add(Me.lblSessionWon)
        Me.Controls.Add(Me.lblTotalPct)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSessionPct)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStatistics"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FreeCell Statistics"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSessionPct As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblTotalPct As System.Windows.Forms.Label
    Friend WithEvents lblSessionWon As System.Windows.Forms.Label
    Friend WithEvents lblSessionLost As System.Windows.Forms.Label
    Friend WithEvents lblTotalWon As System.Windows.Forms.Label
    Friend WithEvents lblTotalLost As System.Windows.Forms.Label
    Friend WithEvents lblMaxWinStreak As System.Windows.Forms.Label
    Friend WithEvents lblMaxLoseStreak As System.Windows.Forms.Label
    Friend WithEvents lblCurrentStreak As System.Windows.Forms.Label
    Friend WithEvents lblStreakType As System.Windows.Forms.Label
End Class
