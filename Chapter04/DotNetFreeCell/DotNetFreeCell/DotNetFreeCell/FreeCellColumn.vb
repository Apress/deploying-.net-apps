Public Class FreeCellColumn
    Implements ICardHolder


    Dim _Cards As New Cards


    Private Sub FreeCellColumn_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles Me.ControlAdded
        _Cards.Add(CType(e.Control, CardLib.Card))
        e.Control.Top = (Me.Controls.Count - 1) * 18
        e.Control.BringToFront()
    End Sub

    Public ReadOnly Property Cards() As Cards
        Get
            Return _Cards
        End Get
    End Property

    'Public Sub AppendSeries(ByVal cards As Cards)
    '    If cards.Count = 0 Then Exit Sub

    '    If Me.Cards.Count = 0 Then

    '    Else
    '        For i As Integer = 0 To cards.Count - 1
    '            Dim LastCard As CardLib.Card = Me.Cards(Me.Cards.Count - 1)
    '            Dim FirstCard As CardLib.Card = cards(i)
    '            If FirstCard.IsAlternatingSuitAscMatch(LastCard) Then
    '                For p As Integer = i To cards.Count - 1
    '                    Me.Controls.Add(cards(p))
    '                    If cards(p).IsInverse = True Then cards(p).ToggleInverse()
    '                    Exit For
    '                Next
    '                'For Each Card As CardLib.Card In cards

    '                'Next
    '            End If
    '        Next i
    '    End If
    'End Sub

    Private Sub FreeCellColumn_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles Me.ControlRemoved
        _Cards.Remove(CType(e.Control, CardLib.Card))
    End Sub

    Public Function AcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.AcceptCard
        If card Is Nothing Then Return False
        Dim CardAccepted As Boolean = False
        Dim FreeCells As Integer = CType(Me.Parent, Form1).FreeCells
        If CType(Me.Parent, Form1).ControlKeyDown = True Then FreeCells = 100


        Dim Cards As Cards
        If TypeOf card.Parent Is FreeCellColumn Then
            Cards = CType(card.Parent, FreeCellColumn).Cards.CardSeries
        Else
            Cards = New Cards()
            Cards.Add(card)
        End If
        If Me.Cards.Count = 0 Then
            Dim Start As Integer = 0
            FreeCells = CType(Me.Parent, Form1).FreeCells(Me)

            If CType(Me.Parent, Form1).ControlKeyDown = True Then FreeCells = 100
            If FreeCells < Cards.Count Then Start = Cards.Count - FreeCells

            For i As Integer = Start To Cards.Count - 1
                Me.Controls.Add(Cards(i))
                If Cards(i).IsInverse = True Then Cards(i).ToggleInverse()
                CardAccepted = True
            Next
        Else
            For i As Integer = 0 To Cards.Count - 1
                Dim LastCard As CardLib.Card = Me.Cards(Me.Cards.Count - 1)
                Dim FirstCard As CardLib.Card = Cards(i)
                If FirstCard.IsAlternatingSuitAscMatch(LastCard) Then
                    Dim CellsNeeded As Integer = Cards.Count - i
                    If CellsNeeded <= FreeCells Then
                        For p As Integer = i To Cards.Count - 1
                            Me.Controls.Add(Cards(p))
                            If Cards(p).IsInverse = True Then Cards(p).ToggleInverse()
                            CardAccepted = True
                            Exit For
                        Next
                    End If
                End If
            Next i
        End If
        Return CardAccepted
    End Function

    Public ReadOnly Property SelectedCards() As Cards Implements ICardHolder.SelectedCards
        Get
            Return Me.Cards.CardSeries
        End Get
    End Property

    Public ReadOnly Property CanAcceptCard(ByVal card As CardLib.Card) As Boolean Implements ICardHolder.CanAcceptCard
        Get
            If card Is Nothing Then Return False
            Dim CardAccepted As Boolean = False
            Dim FreeCells As Integer = CType(Me.Parent, Form1).FreeCells

            Dim Cards As Cards
            If TypeOf card.Parent Is FreeCellColumn Then
                Cards = CType(card.Parent, FreeCellColumn).Cards.CardSeries
            Else
                Cards = New Cards()
                Cards.Add(card)
            End If
            If Me.Cards.Count = 0 Then
                Dim Start As Integer = 0
                FreeCells = CType(Me.Parent, Form1).FreeCells(Me)

                If FreeCells < Cards.Count Then Start = Cards.Count - FreeCells

                For i As Integer = Start To Cards.Count - 1
                    CardAccepted = True
                Next
            Else
                For i As Integer = 0 To Cards.Count - 1
                    Dim LastCard As CardLib.Card = Me.Cards(Me.Cards.Count - 1)
                    Dim FirstCard As CardLib.Card = Cards(i)
                    If FirstCard.IsAlternatingSuitAscMatch(LastCard) Then
                        Dim CellsNeeded As Integer = Cards.Count - i
                        If CellsNeeded <= FreeCells Then
                            For p As Integer = i To Cards.Count - 1
                                CardAccepted = True
                                Exit For
                            Next
                        End If
                    End If
                Next i
                'CardAccepted = False
            End If
            Return CardAccepted
        End Get
    End Property
End Class


Public Class Cards
    Inherits CollectionBase

    Default Public ReadOnly Property Item(ByVal index As Integer) As CardLib.Card
        Get
            Return CType(Me.InnerList(index), CardLib.Card)
        End Get
    End Property

    Public Sub Remove(ByVal card As CardLib.Card)
        Me.InnerList.Remove(card)

    End Sub

    Public Sub Reverse()
        Me.InnerList.Reverse()
    End Sub

    Public Sub Add(ByVal card As CardLib.Card)
        Me.InnerList.Add(card)
    End Sub

    Public ReadOnly Property IndexOf(ByVal card As CardLib.Card) As Integer
        Get
            Return Me.InnerList.IndexOf(card)
        End Get
    End Property

    Public ReadOnly Property IsLastCard(ByVal card As CardLib.Card) As Boolean
        Get
            If Me.IndexOf(card) = Me.InnerList.Count - 1 Then Return True Else Return False
        End Get
    End Property

    Public ReadOnly Property CardSeries() As Cards
        Get
            If Me.InnerList.Count = 0 Then Return Nothing
            Dim Cards As New Cards
            Cards.Add(CType(Me.Item(Me.Count - 1), CardLib.Card))

            For i As Integer = Me.InnerList.Count - 2 To 0 Step -1
                If Me(i).IsAlternatingSuitDescMatch(Me(i + 1)) Then
                    Cards.Add(Me(i))
                Else
                    Exit For
                End If
            Next
            Cards.Reverse()
            Return Cards
        End Get
    End Property
End Class

Public Interface ICardHolder
    Function AcceptCard(ByVal card As CardLib.Card) As Boolean
    ReadOnly Property SelectedCards() As Cards
    ReadOnly Property CanAcceptCard(ByVal card As CardLib.Card) As Boolean

End Interface