Public Class EmptyCell
    Implements ICardHolder


    Event CellClicked(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub EmptyCell_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If Me.Controls.Count = 0 Then RaiseEvent CellClicked(Me, New EventArgs)
    End Sub

    Public Function AddCard(ByVal cards As Cards) As Boolean

    End Function

    Public Function AcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.AcceptCard
        If card Is Nothing Then Return False

        If Me.Controls.Count > 0 Then Return False
        card.Parent.Controls.Remove(card)
        Me.Controls.Add(card)
        If card.IsInverse = True Then card.ToggleInverse()
        card.Top = 0
        card.Left = 0
        Return True
    End Function

    Public ReadOnly Property SelectedCards() As Cards Implements ICardHolder.SelectedCards
        Get
            If Me.Controls.Count = 0 Then
                Return Nothing
            Else
                Dim Cards As New Cards
                Cards.Add(CType(Me.Controls(0), CardLib.Card))
                Return Cards
            End If
        End Get
    End Property

    Public ReadOnly Property CanAcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.CanAcceptCard
        Get
            If card Is Nothing Then Return False
            If Me.Controls.Count > 0 Then Return False
            Return True
        End Get
    End Property
End Class
