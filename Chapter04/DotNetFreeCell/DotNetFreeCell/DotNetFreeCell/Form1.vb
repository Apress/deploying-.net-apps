Imports CardLib

Public Class Form1

    Private Const TITLE_BAR As String = "FreeCell"

    Dim _Columns() As FreeCellColumn
    Dim _CompletedCells() As CompletedCell
    Dim _EmptyCells() As EmptyCell

    Dim _Deck As New CardLib.CardDeck
    Dim _GameNumber As Integer

    Dim _SelectedCardHolder As ICardHolder
    Dim _DownArrow As Cursor
    Dim _SessionWon As Integer = 0
    Dim _SessionLost As Integer = 0
    Dim _CtrlKeyDown As Boolean = False

    Friend ReadOnly Property ControlKeyDown() As Boolean
        Get
            Return _CtrlKeyDown
        End Get
    End Property

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Function ExtractCursor(ByVal imageName As String) As Cursor
        Dim thisExe As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()


        Dim File As System.IO.Stream = thisExe.GetManifestResourceStream("DotNetFreeCell." & imageName)
        Dim Cur As New Cursor(File)
        Return Cur
    End Function

    Private Sub Form1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

        For Each Card As Card In _Deck
            If Card.IsInverse = True Then

                Card.ToggleInverse()
                _SelectedCardHolder = Nothing
            End If
        Next
        _SelectedCardHolder = Nothing
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If NewGameConfirm() = False Then e.Cancel = True
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then _CtrlKeyDown = True

    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then _CtrlKeyDown = False
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = TITLE_BAR
        _DownArrow = ExtractCursor("NORMAL05.CUR")

        _Columns = New FreeCellColumn() {Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8}
        _CompletedCells = New CompletedCell() {CompleteCell1, CompleteCell2, CompleteCell3, CompleteCell4}
        _EmptyCells = New EmptyCell() {FreeCell1, FreeCell2, FreeCell3, FreeCell4}

        For Each Panel As Panel In _Columns
            AddHandler Panel.Click, AddressOf Form1_Click
            '    AddHandler Panel.MouseEnter, AddressOf DoMouseOver
            AddHandler Panel.MouseEnter, AddressOf DoMouseOver
            AddHandler Panel.MouseLeave, AddressOf DoMouseLeave
        Next
        For Each EmptyCell As EmptyCell In _EmptyCells
            AddHandler EmptyCell.MouseEnter, AddressOf DoMouseOver
            AddHandler EmptyCell.MouseLeave, AddressOf DoMouseLeave
        Next
        For Each CompletedCell As CompletedCell In _CompletedCells
            AddHandler CompletedCell.MouseEnter, AddressOf DoMouseOver
            AddHandler CompletedCell.MouseLeave, AddressOf DoMouseLeave
        Next
        For Each Card As Card In _Deck
            AddHandler Card.CardClick, AddressOf CardClick
            AddHandler Card.CardDoubleClick, AddressOf CardDoubleClick
            AddHandler Card.CardMouseDown, AddressOf CardMouseDown
            AddHandler Card.CardMouseUp, AddressOf CardMouseUp
            AddHandler Card.CardMouseEnter, AddressOf DoMouseOver
            AddHandler Card.CardMouseLeave, AddressOf DoMouseLeave
        Next
    End Sub

    Private Sub CompletedCellCardAdded(ByVal sender As Object, ByVal e As EventArgs) Handles CompleteCell1.CardAdded, CompleteCell2.CardAdded, CompleteCell3.CardAdded, CompleteCell4.CardAdded
        Dim CardsCompleted As Integer = 0
        For Each CompletedCell As CompletedCell In _CompletedCells
            CardsCompleted += CompletedCell.Controls.Count
        Next
        lblStatus.Text = "Cards Left: " & (52 - CardsCompleted)
    End Sub

    Private Sub NewGameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewGameToolStripMenuItem.Click
        If NewGameConfirm() = True Then StartRandomGame()
    End Sub

    Private Sub StartRandomGame()
        Dim Rnd As New System.Random(Environment.TickCount)
        _GameNumber = Rnd.Next(1, 100000)
        StartGame()
    End Sub

    Private Sub RecordResults()

    End Sub

    Private Sub StartGame()
        _Deck.Shuffle(_GameNumber)
        _SelectedCardHolder = Nothing
        lblStatus.Text = "Cards Left: 52"

        Dim Column As Integer = 0
        Dim Row As Integer = 0

        For Each Panel As Panel In _Columns
            Panel.Controls.Clear()

        Next
        For Each Card As Card In _Deck
            _Columns(Column).Controls.Add(Card)
            Card.BringToFront()
            Column += 1
            If Column = 8 Then
                Column = 0
                Row += 1
            End If
            Card.Tag = ""
        Next
        Me.Text = TITLE_BAR & " Game #" & _GameNumber.ToString
        RestartGameToolStripMenuItem.Enabled = True
    End Sub

    Private Sub CardMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim Card As Card = CType(sender, Card)
            picShowCard.Location = GetGlobalPos(Card)
            picShowCard.BringToFront()
            picShowCard.Image = Card.Image
            picShowCard.Visible = True
        End If
    End Sub

    Private Sub CardMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        picShowCard.Visible = False
    End Sub


    Private Function GetGlobalPos(ByVal ctrl As Control) As Point
        Dim X As Integer = ctrl.Left
        Dim Y As Integer = ctrl.Top
        Dim Parent As Control = ctrl.Parent
        While Not Parent Is Me
            X += Parent.Left
            Y += Parent.Top
            Parent = Parent.Parent
        End While
        Return New Point(X, Y)

    End Function

    Private Sub CardDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Card As Card = CType(sender, Card)
        If TypeOf Card.Parent Is FreeCellColumn Then
            Card = CType(Card.Parent, FreeCellColumn).Cards.CardSeries(CType(Card.Parent, FreeCellColumn).Cards.CardSeries.Count - 1)

            For Each EmptyCell As EmptyCell In _EmptyCells
                If EmptyCell.AcceptCard(Card) = True Then
                    CheckForOKMove()
                    _SelectedCardHolder = Nothing
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub CardClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Card As Card = CType(sender, Card)

        Dim CurrentCardHolder As ICardHolder = CType(Card.Parent, ICardHolder)

        If Not _SelectedCardHolder Is Nothing Then
            Dim SelectedCards As Cards = _SelectedCardHolder.SelectedCards

            If CurrentCardHolder.AcceptCard(SelectedCards(SelectedCards.Count - 1)) = True Then
                _SelectedCardHolder = Nothing
                CheckForOKMove()
            Else
                If _SelectedCardHolder Is CurrentCardHolder Then

                    If _SelectedCardHolder.SelectedCards(_SelectedCardHolder.SelectedCards.Count - 1).IsInverse Then _SelectedCardHolder.SelectedCards(_SelectedCardHolder.SelectedCards.Count - 1).ToggleInverse()
                    _SelectedCardHolder = Nothing
                End If
            End If
        Else
            If Not TypeOf Card.Parent Is CompletedCell Then
                If Card.IsInverse = False Then
                    _SelectedCardHolder = CType(Card.Parent, ICardHolder)
                    _SelectedCardHolder.SelectedCards(_SelectedCardHolder.SelectedCards.Count - 1).ToggleInverse()
                Else
                    _SelectedCardHolder = Nothing
                End If
            End If
        End If
    End Sub

    Public ReadOnly Property FreeCells() As Integer
        Get
            Dim Count As Integer = 1
            Dim EmptyCellCount As Integer = 0
            Dim EmptyColumnCount As Integer = 1
            For Each EmptyCell As EmptyCell In _EmptyCells
                If EmptyCell.Controls.Count = 0 Then EmptyCellCount += 1
            Next
            For Each FreeCellColumn As FreeCellColumn In _Columns
                If FreeCellColumn.Controls.Count = 0 Then EmptyColumnCount += 1
            Next
            Count += (EmptyColumnCount * EmptyCellCount)

            If EmptyCellCount = 0 Then Count = EmptyColumnCount
            Return Count
        End Get
    End Property

    Public ReadOnly Property FreeCells(ByVal exludeColumn As FreeCellColumn)
        Get
            Dim Count As Integer = 1
            Dim EmptyCellCount As Integer = 0
            Dim EmptyColumnCount As Integer = 1
            For Each EmptyCell As EmptyCell In _EmptyCells
                If EmptyCell.Controls.Count = 0 Then EmptyCellCount += 1
            Next
            For Each FreeCellColumn As FreeCellColumn In _Columns
                If FreeCellColumn.Controls.Count = 0 And Not FreeCellColumn Is exludeColumn Then EmptyColumnCount += 1
            Next
            Count += (EmptyColumnCount * EmptyCellCount)

            Return Count
        End Get
    End Property

    Private Sub CheckForOKMove()
        Dim CardsMoved As Boolean = True

        Do While CardsMoved = True
            CardsMoved = False
            For Each CompletedCell As CompletedCell In _CompletedCells
                For Each FreeCellColumn As FreeCellColumn In _Columns
                    If CompletedCell.MoveOKComplete(FreeCellColumn.Cards, _Deck) = True Then CardsMoved = True

                Next
                For Each EmptyCell As EmptyCell In _EmptyCells
                    If Not EmptyCell.SelectedCards Is Nothing Then
                        If CompletedCell.MoveOKComplete(EmptyCell.SelectedCards, _Deck) = True Then CardsMoved = True
                    End If
                Next
            Next
        Loop
        If Me.IsGameStarted = False Then RecordOutCome(True)

    End Sub

    Private Sub CompleteCell1_CellClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles CompleteCell1.CellClicked, CompleteCell2.Click, CompleteCell3.CellClicked, CompleteCell4.CellClicked
        Dim CompletedCell As CompletedCell = CType(sender, CompletedCell)

        If Not _SelectedCardHolder Is Nothing Then
            Dim SelectedCards As Cards = _SelectedCardHolder.SelectedCards
            If CompletedCell.AcceptCard(SelectedCards(SelectedCards.Count - 1)) = True Then
                _SelectedCardHolder = Nothing
            End If
        End If
    End Sub

    Private Sub CompleteCell1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles FreeCell1.Paint, FreeCell2.Paint, FreeCell3.Paint, FreeCell4.Paint, CompleteCell1.Paint, CompleteCell2.Paint, CompleteCell3.Paint, CompleteCell4.Paint
        Dim Panel As Panel = CType(sender, Panel)
        e.Graphics.DrawLine(New Pen(Color.FromArgb(0, 255, 0), 0.5), Panel.Width - 1, 0, Panel.Width - 1, Panel.Height)
        e.Graphics.DrawLine(New Pen(Color.FromArgb(0, 255, 0), 0.5), 0, Panel.Height - 1, Panel.Width, Panel.Height - 1)
        e.Graphics.DrawLine(New Pen(Color.Black, 0.5), 0, 0, 0, Panel.Height)
        e.Graphics.DrawLine(New Pen(Color.Black, 0.5), 0, 0, Panel.Width, 0)

    End Sub

    Private Sub FreeCell_CellClicked(ByVal sender As Object, ByVal e As EventArgs) Handles FreeCell1.CellClicked, FreeCell2.CellClicked, FreeCell3.CellClicked, FreeCell4.CellClicked
        Dim EmptyCell As EmptyCell = CType(sender, EmptyCell)
        'If EmptyCell.AddCard(_SelectedColumn.Cards.CardSeries) = True Then _SelectedCardHolder = Nothing
        If Not _SelectedCardHolder Is Nothing Then
            Dim SelectedCards As Cards = _SelectedCardHolder.SelectedCards
            If EmptyCell.AcceptCard(SelectedCards(SelectedCards.Count - 1)) = True Then
                _SelectedCardHolder = Nothing
                CheckForOKMove()
            End If
        End If
    End Sub

    Private Sub Column1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Column1.Click, Column2.Click, Column3.Click, Column4.Click, Column5.Click, Column6.Click, Column7.Click, Column8.Click
        Dim FreeCellColumn As FreeCellColumn = CType(sender, FreeCellColumn)

        If Not _SelectedCardHolder Is Nothing Then
            Dim Cards As Cards = _SelectedCardHolder.SelectedCards

            If FreeCellColumn.AcceptCard(Cards(Cards.Count - 1)) = True Then
                _SelectedCardHolder = Nothing
                CheckForOKMove()
            End If
        End If
    End Sub

    Private Sub FreeCell1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub SelectGameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectGameToolStripMenuItem.Click
        Dim f As New frmChooseGame

        If NewGameConfirm() = True Then
            If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                _GameNumber = CInt(f.txtGameNum.Text)
                StartGame()

            End If
        End If
    End Sub

    Private Sub RestartGameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartGameToolStripMenuItem.Click
        If _GameNumber > 0 Then
            If NewGameConfirm() = True Then StartGame()
        Else

        End If
    End Sub

    Private ReadOnly Property IsGameStarted() As Boolean
        Get
            For Each FreeCellColumn As FreeCellColumn In _columns
                If FreeCellColumn.Controls.Count > 0 Then Return True
            Next
            Return False
        End Get
    End Property

    Private Function NewGameConfirm() As Boolean
        If IsGameStarted = False Then Return True
        If MessageBox.Show("Do you want to resign this game?", "FreeCell", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            RecordOutCome(False)
            Return True

        Else
            Return False
        End If
    End Function

    Private Sub RecordOutCome(ByVal didWin As Boolean)
        Dim StreakType As String = GetSetting(Application.ProductName, "Score", "StreakType")
        Dim StreakCount As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Streak"))
        Dim MaxWinStreak As Integer = cInteger(GetSetting(Application.ProductName, "Score", "MaxWinStreak"))
        Dim MaxLoseStreak As Integer = cInteger(GetSetting(Application.ProductName, "Score", "MaxLoseStreak"))


        If didWin = True Then
            _SessionWon += 1
            Dim TotalWon As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Won"))
            SaveSetting(Application.ProductName, "Score", "Won", (TotalWon + 1).ToString)
            If StreakType = "loss" Then StreakCount = 0
            StreakCount += 1
            SaveSetting(Application.ProductName, "Score", "StreakType", "won")
            SaveSetting(Application.ProductName, "Score", "Streak", StreakCount.ToString)
            If StreakCount > MaxWinStreak Then SaveSetting(Application.ProductName, "Score", "MaxWinStreak", StreakCount.ToString)

        Else
            _SessionLost += 1
            Dim TotalLost As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Lost"))
            SaveSetting(Application.ProductName, "Score", "Lost", (TotalLost + 1).ToString)
            If StreakType = "won" Then StreakCount = 0
            StreakCount += 1
            SaveSetting(Application.ProductName, "Score", "StreakType", "loss")
            SaveSetting(Application.ProductName, "Score", "Streak", StreakCount.ToString)
            If StreakCount > MaxLoseStreak Then SaveSetting(Application.ProductName, "Score", "MaxLoseStreak", StreakCount.ToString)

        End If
    End Sub

    Private Function cInteger(ByVal value As Object) As Integer
        If IsNumeric(value) Then Return CInt(value) Else Return 0
    End Function

    Private Sub DoMouseOver(ByVal sender As Object, ByVal e As EventArgs)
        If Not _SelectedCardHolder Is Nothing Then
            Dim NewCardHolder As ICardHolder
            If TypeOf sender Is Card Then NewCardHolder = CType(CType(sender, Card).Parent, ICardHolder) Else NewCardHolder = CType(sender, ICardHolder)

            Dim Cards As Cards = _SelectedCardHolder.SelectedCards
            If Cards.Count > 0 Then
                If NewCardHolder.CanAcceptCard(Cards(Cards.Count - 1)) = True Then
                    Me.Cursor = _DownArrow
                Else
                    Me.Cursor = Cursors.Default
                End If
            End If

        End If
    End Sub

    Private Sub DoMouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub StatisticsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatisticsToolStripMenuItem.Click
        Dim f As New frmStatistics
        If f.ShowDialog(_SessionWon, _SessionLost) = Windows.Forms.DialogResult.Abort Then
            _SessionWon = 0
            _SessionLost = 0
        End If

    End Sub
End Class
