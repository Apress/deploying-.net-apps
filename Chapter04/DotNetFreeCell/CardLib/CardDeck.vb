Public Class CardDeck
    Inherits CollectionBase


    Public Sub New()

        For CardNum As Integer = 1 To 13
            For SuitNum As Integer = 0 To 3
                Dim Suit As Suit
                Select Case SuitNum
                    Case 0
                        Suit = CardLib.Suit.Club
                    Case 1
                        Suit = CardLib.Suit.Diamond
                    Case 2
                        Suit = CardLib.Suit.Heart
                    Case 3
                        Suit = CardLib.Suit.Spade
                End Select
                Dim Card As New Card()
                Card.SetCard(CardNum, Suit)
                Me.InnerList.Add(Card)
            Next
        Next
    End Sub

    Default Public ReadOnly Property Item(ByVal index As Integer) As Card
        Get
            Return CType(Me.InnerList(index), Card)
        End Get
    End Property

    Default ReadOnly Property Item(ByVal cardNum As Integer, ByVal suit As Suit)
        Get
            For Each Card As Card In Me.InnerList
                If Card.Number = cardNum And Card.Suit = suit Then Return Card
            Next
            Return Nothing
        End Get
    End Property


    Private ReadOnly Property BaseArray() As Card()
        Get
            Dim Cards(Me.InnerList.Count - 1) As Card
            Dim Count As Integer = 0

            For i As Integer = 1 To 13
                Cards(Count) = Me(i, Suit.Spade)
                Count += 1
                Cards(Count) = Me(i, Suit.Diamond)
                Count += 1
                Cards(Count) = Me(i, Suit.Club)
                Count += 1
                Cards(Count) = Me(i, Suit.Heart)
                Count += 1

            Next
            Return Cards
        End Get
    End Property

    Public Sub Shuffle(ByVal seed As Integer)
        Dim Cards() As Card = Me.BaseArray

        Me.InnerList.Clear()
        For i As Integer = 0 To 133
            Cards = ShuffleArray(Cards, seed)
            If seed > 1 Then seed -= 1
        Next
        For Each Card As Card In Cards
            Me.InnerList.Add(Card)
        Next
        'For i As Integer = 0 To 10
        '    For Each Card As Card In Me.InnerList
        '        Card.GUID = System.Guid.NewGuid
        '    Next
        '    Me.InnerList.Sort(New CardSort)
        'Next
    End Sub


    Private Function CutDeck(ByVal values() As Card, ByVal rnd As System.Random) As Card()
        Dim Split As Integer = rnd.Next(23, 31)
        Dim LeftSide(Split - 1) As Card
        Dim RightSide(values.Length - Split - 1) As Card
        Dim ReturnArray(values.Length - 1) As Card
        Dim i As Integer = 0

        For i = 0 To Split - 1
            LeftSide(i) = values(i)
        Next
        For i = Split To values.Length - 1
            RightSide(i - LeftSide.Length) = values(i)
        Next

        Dim Count As Integer = 0
        For i = 0 To RightSide.Length - 1
            ReturnArray(Count) = RightSide(i)
            Count += 1
        Next
        For i = 0 To LeftSide.Length - 1
            ReturnArray(Count) = LeftSide(i)
            Count += 1
        Next
        Return ReturnArray
    End Function

    Private Function ShuffleArray(ByVal values() As Card, ByVal seed As Integer) As Card()
        Dim Split As Integer = CInt(values.Length / 2)

        Dim LeftSide(Split - 1) As Card
        Dim RightSide(values.Length - Split - 1) As Card
        Dim ReturnArray(values.Length - 1) As Card
        Dim i As Integer
        Dim LeftCount As Integer
        Dim RightCount As Integer

        Dim rnd As New System.Random(seed)

        values = CutDeck(values, rnd)

        For i = 0 To Split - 1
            LeftSide(i) = values(i)
        Next
        For i = Split To values.Length - 1
            RightSide(i - LeftSide.Length) = values(i)
        Next


        For i = 0 To values.Length - 1
            Dim Val As Integer = rnd.Next(0, 2)
            If Val = 0 Then
                If LeftCount < LeftSide.Length Then
                    ReturnArray(i) = LeftSide(LeftCount)
                    LeftCount += 1
                Else
                    If RightCount < RightSide.Length Then
                        ReturnArray(i) = RightSide(RightCount)
                        RightCount += 1
                    End If
                End If
            Else
                If RightCount < RightSide.Length Then
                    ReturnArray(i) = RightSide(RightCount)
                    RightCount += 1
                Else
                    If LeftCount < LeftSide.Length Then
                        ReturnArray(i) = LeftSide(LeftCount)
                        LeftCount += 1
                    End If
                End If
            End If
        Next

        Return ReturnArray
    End Function
End Class


Public Class CardSort
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Card1 As Card = CType(x, Card)
        Dim Card2 As Card = CType(y, Card)
        If Card1.GUID.ToString > Card2.GUID.ToString Then Return -1
        If Card1.GUID.ToString < Card2.GUID.ToString Then Return 1
        If Card1.GUID.ToString = Card2.GUID.ToString Then Return 0

    End Function
End Class