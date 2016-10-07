<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.GameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RestartGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.StatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.OptionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SearchForHelpOnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HowtoUseHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutNETFreeCellToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.picKing = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.picKing2 = New System.Windows.Forms.PictureBox
        Me.CompleteCell1 = New DotNetFreeCell.CompletedCell
        Me.CompleteCell2 = New DotNetFreeCell.CompletedCell
        Me.CompleteCell3 = New DotNetFreeCell.CompletedCell
        Me.CompleteCell4 = New DotNetFreeCell.CompletedCell
        Me.FreeCell4 = New DotNetFreeCell.EmptyCell
        Me.FreeCell3 = New DotNetFreeCell.EmptyCell
        Me.FreeCell2 = New DotNetFreeCell.EmptyCell
        Me.FreeCell1 = New DotNetFreeCell.EmptyCell
        Me.lblStatus = New System.Windows.Forms.Label
        Me.picShowCard = New System.Windows.Forms.PictureBox
        Me.Column8 = New DotNetFreeCell.FreeCellColumn
        Me.Column7 = New DotNetFreeCell.FreeCellColumn
        Me.Column6 = New DotNetFreeCell.FreeCellColumn
        Me.Column5 = New DotNetFreeCell.FreeCellColumn
        Me.Column4 = New DotNetFreeCell.FreeCellColumn
        Me.Column3 = New DotNetFreeCell.FreeCellColumn
        Me.Column2 = New DotNetFreeCell.FreeCellColumn
        Me.Column1 = New DotNetFreeCell.FreeCellColumn
        Me.MenuStrip1.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.picKing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picKing2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GameToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(632, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GameToolStripMenuItem
        '
        Me.GameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewGameToolStripMenuItem, Me.SelectGameToolStripMenuItem, Me.RestartGameToolStripMenuItem, Me.ToolStripSeparator1, Me.StatisticsToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.ToolStripSeparator2, Me.OptionsToolStripMenuItem1, Me.ToolStripSeparator3, Me.ExitToolStripMenuItem})
        Me.GameToolStripMenuItem.Name = "GameToolStripMenuItem"
        Me.GameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.GameToolStripMenuItem.Text = "&Game"
        '
        'NewGameToolStripMenuItem
        '
        Me.NewGameToolStripMenuItem.Name = "NewGameToolStripMenuItem"
        Me.NewGameToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.NewGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.NewGameToolStripMenuItem.Text = "New Game"
        '
        'SelectGameToolStripMenuItem
        '
        Me.SelectGameToolStripMenuItem.Name = "SelectGameToolStripMenuItem"
        Me.SelectGameToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SelectGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.SelectGameToolStripMenuItem.Text = "Select Game"
        '
        'RestartGameToolStripMenuItem
        '
        Me.RestartGameToolStripMenuItem.Enabled = False
        Me.RestartGameToolStripMenuItem.Name = "RestartGameToolStripMenuItem"
        Me.RestartGameToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.RestartGameToolStripMenuItem.Text = "Restart Game"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        '
        'StatisticsToolStripMenuItem
        '
        Me.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem"
        Me.StatisticsToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.StatisticsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.StatisticsToolStripMenuItem.Text = "Statistics"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.OptionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        '
        'OptionsToolStripMenuItem1
        '
        Me.OptionsToolStripMenuItem1.Name = "OptionsToolStripMenuItem1"
        Me.OptionsToolStripMenuItem1.Padding = New System.Windows.Forms.Padding(0)
        Me.OptionsToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F10
        Me.OptionsToolStripMenuItem1.Text = "Undo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.SearchForHelpOnToolStripMenuItem, Me.HowtoUseHelpToolStripMenuItem, Me.ToolStripSeparator4, Me.AboutNETFreeCellToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.ContentsToolStripMenuItem.Text = "Contents"
        '
        'SearchForHelpOnToolStripMenuItem
        '
        Me.SearchForHelpOnToolStripMenuItem.Name = "SearchForHelpOnToolStripMenuItem"
        Me.SearchForHelpOnToolStripMenuItem.Text = "Search for Help on..."
        '
        'HowtoUseHelpToolStripMenuItem
        '
        Me.HowtoUseHelpToolStripMenuItem.Name = "HowtoUseHelpToolStripMenuItem"
        Me.HowtoUseHelpToolStripMenuItem.Text = "How to Use Help"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        '
        'AboutNETFreeCellToolStripMenuItem
        '
        Me.AboutNETFreeCellToolStripMenuItem.Name = "AboutNETFreeCellToolStripMenuItem"
        Me.AboutNETFreeCellToolStripMenuItem.Text = "About .NET Free Cell"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.picKing)
        Me.Panel9.Controls.Add(Me.PictureBox1)
        Me.Panel9.Controls.Add(Me.picKing2)
        Me.Panel9.Controls.Add(Me.CompleteCell1)
        Me.Panel9.Controls.Add(Me.CompleteCell2)
        Me.Panel9.Controls.Add(Me.CompleteCell3)
        Me.Panel9.Controls.Add(Me.CompleteCell4)
        Me.Panel9.Controls.Add(Me.FreeCell4)
        Me.Panel9.Controls.Add(Me.FreeCell3)
        Me.Panel9.Controls.Add(Me.FreeCell2)
        Me.Panel9.Controls.Add(Me.FreeCell1)
        Me.Panel9.Location = New System.Drawing.Point(0, 24)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.Panel9.Size = New System.Drawing.Size(636, 96)
        Me.Panel9.TabIndex = 112
        '
        'picKing
        '
        Me.picKing.Image = CType(resources.GetObject("picKing.Image"), System.Drawing.Image)
        Me.picKing.Location = New System.Drawing.Point(290, 0)
        Me.picKing.Name = "picKing"
        Me.picKing.Size = New System.Drawing.Size(37, 38)
        Me.picKing.TabIndex = 114
        Me.picKing.TabStop = False
        Me.picKing.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(300, 28)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(38, 38)
        Me.PictureBox1.TabIndex = 108
        Me.PictureBox1.TabStop = False
        '
        'picKing2
        '
        Me.picKing2.Image = CType(resources.GetObject("picKing2.Image"), System.Drawing.Image)
        Me.picKing2.Location = New System.Drawing.Point(290, 3)
        Me.picKing2.Name = "picKing2"
        Me.picKing2.Size = New System.Drawing.Size(37, 38)
        Me.picKing2.TabIndex = 113
        Me.picKing2.TabStop = False
        Me.picKing2.Visible = False
        '
        'CompleteCell1
        '
        Me.CompleteCell1.Dock = System.Windows.Forms.DockStyle.Right
        Me.CompleteCell1.Location = New System.Drawing.Point(347, 0)
        Me.CompleteCell1.Name = "CompleteCell1"
        Me.CompleteCell1.Size = New System.Drawing.Size(71, 96)
        Me.CompleteCell1.TabIndex = 107
        '
        'CompleteCell2
        '
        Me.CompleteCell2.Dock = System.Windows.Forms.DockStyle.Right
        Me.CompleteCell2.Location = New System.Drawing.Point(418, 0)
        Me.CompleteCell2.Name = "CompleteCell2"
        Me.CompleteCell2.Size = New System.Drawing.Size(71, 96)
        Me.CompleteCell2.TabIndex = 106
        '
        'CompleteCell3
        '
        Me.CompleteCell3.Dock = System.Windows.Forms.DockStyle.Right
        Me.CompleteCell3.Location = New System.Drawing.Point(489, 0)
        Me.CompleteCell3.Name = "CompleteCell3"
        Me.CompleteCell3.Size = New System.Drawing.Size(71, 96)
        Me.CompleteCell3.TabIndex = 105
        '
        'CompleteCell4
        '
        Me.CompleteCell4.Dock = System.Windows.Forms.DockStyle.Right
        Me.CompleteCell4.Location = New System.Drawing.Point(560, 0)
        Me.CompleteCell4.Name = "CompleteCell4"
        Me.CompleteCell4.Size = New System.Drawing.Size(71, 96)
        Me.CompleteCell4.TabIndex = 104
        '
        'FreeCell4
        '
        Me.FreeCell4.Dock = System.Windows.Forms.DockStyle.Left
        Me.FreeCell4.Location = New System.Drawing.Point(213, 0)
        Me.FreeCell4.Name = "FreeCell4"
        Me.FreeCell4.Size = New System.Drawing.Size(71, 96)
        Me.FreeCell4.TabIndex = 103
        '
        'FreeCell3
        '
        Me.FreeCell3.Dock = System.Windows.Forms.DockStyle.Left
        Me.FreeCell3.Location = New System.Drawing.Point(142, 0)
        Me.FreeCell3.Name = "FreeCell3"
        Me.FreeCell3.Size = New System.Drawing.Size(71, 96)
        Me.FreeCell3.TabIndex = 102
        '
        'FreeCell2
        '
        Me.FreeCell2.Dock = System.Windows.Forms.DockStyle.Left
        Me.FreeCell2.Location = New System.Drawing.Point(71, 0)
        Me.FreeCell2.Name = "FreeCell2"
        Me.FreeCell2.Size = New System.Drawing.Size(71, 96)
        Me.FreeCell2.TabIndex = 101
        '
        'FreeCell1
        '
        Me.FreeCell1.Dock = System.Windows.Forms.DockStyle.Left
        Me.FreeCell1.Location = New System.Drawing.Point(0, 0)
        Me.FreeCell1.Name = "FreeCell1"
        Me.FreeCell1.Size = New System.Drawing.Size(71, 96)
        Me.FreeCell1.TabIndex = 100
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Location = New System.Drawing.Point(550, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(79, 24)
        Me.lblStatus.TabIndex = 114
        Me.lblStatus.Text = "Cards Left: 52"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picShowCard
        '
        Me.picShowCard.Location = New System.Drawing.Point(226, 127)
        Me.picShowCard.Name = "picShowCard"
        Me.picShowCard.Size = New System.Drawing.Size(71, 96)
        Me.picShowCard.TabIndex = 115
        Me.picShowCard.TabStop = False
        Me.picShowCard.Visible = False
        '
        'Column8
        '
        Me.Column8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column8.Location = New System.Drawing.Point(551, 127)
        Me.Column8.Name = "Column8"
        Me.Column8.Size = New System.Drawing.Size(71, 317)
        Me.Column8.TabIndex = 111
        '
        'Column7
        '
        Me.Column7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column7.Location = New System.Drawing.Point(474, 127)
        Me.Column7.Name = "Column7"
        Me.Column7.Size = New System.Drawing.Size(71, 317)
        Me.Column7.TabIndex = 110
        '
        'Column6
        '
        Me.Column6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column6.Location = New System.Drawing.Point(397, 127)
        Me.Column6.Name = "Column6"
        Me.Column6.Size = New System.Drawing.Size(71, 317)
        Me.Column6.TabIndex = 109
        '
        'Column5
        '
        Me.Column5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column5.Location = New System.Drawing.Point(320, 127)
        Me.Column5.Name = "Column5"
        Me.Column5.Size = New System.Drawing.Size(71, 317)
        Me.Column5.TabIndex = 108
        '
        'Column4
        '
        Me.Column4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column4.Location = New System.Drawing.Point(243, 127)
        Me.Column4.Name = "Column4"
        Me.Column4.Size = New System.Drawing.Size(71, 317)
        Me.Column4.TabIndex = 107
        '
        'Column3
        '
        Me.Column3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column3.Location = New System.Drawing.Point(166, 127)
        Me.Column3.Name = "Column3"
        Me.Column3.Size = New System.Drawing.Size(71, 317)
        Me.Column3.TabIndex = 106
        '
        'Column2
        '
        Me.Column2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column2.Location = New System.Drawing.Point(89, 127)
        Me.Column2.Name = "Column2"
        Me.Column2.Size = New System.Drawing.Size(71, 317)
        Me.Column2.TabIndex = 105
        '
        'Column1
        '
        Me.Column1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Column1.Location = New System.Drawing.Point(12, 127)
        Me.Column1.Name = "Column1"
        Me.Column1.Size = New System.Drawing.Size(71, 317)
        Me.Column1.TabIndex = 104
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(632, 446)
        Me.Controls.Add(Me.picShowCard)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Column8)
        Me.Controls.Add(Me.Column7)
        Me.Controls.Add(Me.Column6)
        Me.Controls.Add(Me.Column5)
        Me.Controls.Add(Me.Column4)
        Me.Controls.Add(Me.Column3)
        Me.Controls.Add(Me.Column2)
        Me.Controls.Add(Me.Column1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximumSize = New System.Drawing.Size(640, 5000)
        Me.Name = "Form1"
        Me.Text = ".NET FreeCell"
        Me.MenuStrip1.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        CType(Me.picKing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picKing2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents GameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutNETFreeCellToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewGameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents picKing2 As System.Windows.Forms.PictureBox
    Friend WithEvents SelectGameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartGameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OptionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchForHelpOnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HowtoUseHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents picKing As System.Windows.Forms.PictureBox
    Friend WithEvents picShowCard As System.Windows.Forms.PictureBox
    Friend WithEvents CompleteCell1 As DotNetFreeCell.CompletedCell
    Friend WithEvents CompleteCell2 As DotNetFreeCell.CompletedCell
    Friend WithEvents CompleteCell3 As DotNetFreeCell.CompletedCell
    Friend WithEvents CompleteCell4 As DotNetFreeCell.CompletedCell
    Friend WithEvents FreeCell4 As DotNetFreeCell.EmptyCell
    Friend WithEvents FreeCell3 As DotNetFreeCell.EmptyCell
    Friend WithEvents FreeCell2 As DotNetFreeCell.EmptyCell
    Friend WithEvents FreeCell1 As DotNetFreeCell.EmptyCell
    Friend WithEvents Column8 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column7 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column6 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column5 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column4 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column3 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column2 As DotNetFreeCell.FreeCellColumn
    Friend WithEvents Column1 As DotNetFreeCell.FreeCellColumn

End Class
