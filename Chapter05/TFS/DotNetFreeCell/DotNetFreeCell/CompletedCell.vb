Public Class CompletedCell
    Implements ICardHolder

    Event CellClicked(ByVal sender As Object, ByVal e As EventArgs)
    Event CardAdded(ByVal sender As Object, ByVal e As EventArgs)


    Public Function AcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.AcceptCard
        If Me.Controls.Count = 0 Then
            If card.Number = 1 Then
                AddCard(card)
                Return True
            End If
        Else
            Dim LastCard As CardLib.Card = CType(Me.Controls(0), CardLib.Card)
            If card.IsMatchingSuitDescMatch(LastCard) Then
                AddCard(card)
                Return True
            End If

        End If
        Return False
    End Function

    Public ReadOnly Property CanAcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.CanAcceptCard
        Get
            If Me.Controls.Count = 0 Then
                If card.Number = 1 Then
                    Return True
                End If
            Else
                Dim LastCard As CardLib.Card = CType(Me.Controls(0), CardLib.Card)
                If card.IsMatchingSuitDescMatch(LastCard) Then
                    Return True
                End If

            End If
            Return False
        End Get
    End Property



    Public Function MoveOKComplete(ByVal cards As Cards, ByVal deck As CardLib.CardDeck) As Boolean
        Dim Accepted As Boolean = False
        Dim RetVal As Boolean = False

        For i As Integer = cards.Count - 1 To 0 Step -1

            Dim Card As CardLib.Card = cards(i)
            If Card.Number > 2 Then
                Dim LastCard1 As CardLib.Card
                Dim LastCard2 As CardLib.Card
                Accepted = False

                If Card.Suit = CardLib.Suit.Club Or Card.Suit = CardLib.Suit.Spade Then
                    LastCard1 = deck(Card.Number - 1, CardLib.Suit.Diamond)
                    LastCard2 = deck(Card.Number - 1, CardLib.Suit.Heart)
                Else
                    LastCard1 = deck(Card.Number - 1, CardLib.Suit.Club)
                    LastCard2 = deck(Card.Number - 1, CardLib.Suit.Spade)
                End If
                If LastCard1.Tag = "Completed" And LastCard2.Tag = "Completed" Then
                    If Me.AcceptCard(Card) = True Then
                        RetVal = True
                        Accepted = True
                    End If
                End If
                If Accepted = False Then Exit For
            Else
                If Me.AcceptCard(Card) = True Then
                    RetVal = True
                Else
                    Return False
                End If
            End If
        Next
        Return RetVal
    End Function



    Private Sub AddCard(ByVal card As CardLib.Card)
        card.Controls.Remove(card)
        Me.Controls.Add(card)
        card.Location = New Point(0, 0)
        card.BringToFront()
        card.Tag = "Completed"
        If card.IsInverse = True Then card.ToggleInverse()
        RaiseEvent CardAdded(Me, New EventArgs)
    End Sub

    Public ReadOnly Property SelectedCards() As Cards Implements ICardHolder.SelectedCards
        Get
            Return Nothing
        End Get
    End Property

    Private Sub CompletedCell_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        RaiseEvent CellClicked(Me, New EventArgs)
    End Sub

    Public Sub RaiseClick()
        RaiseEvent CellClicked(Me, New EventArgs)
    End Sub



End Class
