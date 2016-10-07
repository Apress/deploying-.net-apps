Imports System.Drawing

Public Class Card

    Dim _Number As Integer
    Dim _Suit As Suit
    Dim _Image As System.Drawing.Bitmap
    Dim _InverseImage As Bitmap
    Event CardClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CardDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CardMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Event CardMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Event CardMouseEnter(ByVal sender As Object, ByVal e As EventArgs)
    Event CardMouseLeave(ByVal sender As Object, ByVal e As EventArgs)



    Dim _GUID As System.Guid = System.Guid.NewGuid
    Dim _IsInverse As Boolean = False


    Public ReadOnly Property IsInverse() As Boolean
        Get
            Return _IsInverse
        End Get
    End Property

    Friend Sub SetCard(ByVal number As Integer, ByVal suit As Suit)
        _Number = number
        _Suit = suit

        Dim FileName As String = number.ToString
        If FileName.Length = 1 Then FileName = "0" & FileName
        Select Case suit
            Case CardLib.Suit.Club
                FileName &= "C"
            Case CardLib.Suit.Diamond
                FileName &= "D"
            Case CardLib.Suit.Heart
                FileName &= "H"
            Case CardLib.Suit.Spade
                FileName &= "S"
        End Select
        FileName &= ".png"
        _Image = ExtractBitmap(FileName)
        _InverseImage = New Bitmap(_Image)
        CreateInverseImage()
        picCard.Image = _Image
        Me.Width = picCard.Width
        Me.Height = picCard.Height
    End Sub

    Private Sub CreateInverseImage()
        For y As Integer = 0 To _Image.Height - 1
            For x As Integer = 0 To _Image.Width - 1
                Dim Pix As Color = _Image.GetPixel(x, y)
                Dim NewPix As Color = Color.FromArgb(255 - Pix.R, 255 - Pix.G, 255 - Pix.B)
                If Pix = Color.Black And Me.Number < 11 And (Me.Suit = CardLib.Suit.Spade Or Me.Suit = CardLib.Suit.Club) Then
                    NewPix = Color.FromArgb(0, 255, 255)
                End If
                _InverseImage.SetPixel(x, y, NewPix)
            Next
        Next
    End Sub

    Public ReadOnly Property IsAlternatingSuitDescMatch(ByVal card As Card) As Boolean
        Get
            If card.Number = Me.Number - 1 Then
                If card.Suit = CardLib.Suit.Club Or card.Suit = CardLib.Suit.Spade Then
                    If Me.Suit = CardLib.Suit.Diamond Or Me.Suit = CardLib.Suit.Heart Then Return True Else Return False
                Else
                    If Me.Suit = CardLib.Suit.Club Or Me.Suit = CardLib.Suit.Spade Then Return True Else Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsAlternatingSuitAscMatch(ByVal card As Card) As Boolean
        Get
            If card.Number = Me.Number + 1 Then
                If card.Suit = CardLib.Suit.Club Or card.Suit = CardLib.Suit.Spade Then
                    If Me.Suit = CardLib.Suit.Diamond Or Me.Suit = CardLib.Suit.Heart Then Return True Else Return False
                Else
                    If Me.Suit = CardLib.Suit.Club Or Me.Suit = CardLib.Suit.Spade Then Return True Else Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsMatchingSuitAscMatch(ByVal card As Card) As Boolean
        Get
            If card.Number = Me.Number + 1 And card.Suit = Me.Suit Then Return True Else Return False
        End Get
    End Property

    Public ReadOnly Property IsMatchingSuitDescMatch(ByVal card As Card) As Boolean
        Get
            If card.Number = Me.Number - 1 And card.Suit = Me.Suit Then Return True Else Return False
        End Get
    End Property

    Public Sub ToggleInverse()
        If _IsInverse = False Then
            picCard.Image = _InverseImage
            _IsInverse = True
        Else
            picCard.Image = _Image
            _IsInverse = False
        End If
    End Sub

    Friend Property GUID() As System.Guid
        Get
            Return _GUID
        End Get
        Set(ByVal value As System.Guid)
            _GUID = value
        End Set
    End Property

    Public ReadOnly Property Number() As Integer
        Get
            Return _Number
        End Get
    End Property

    Public ReadOnly Property Suit() As Suit
        Get
            Return _Suit
        End Get
    End Property

    Public ReadOnly Property Image() As System.Drawing.Bitmap
        Get
            Return _Image
        End Get
    End Property

    Private Function ExtractBitmap(ByVal imageName As String) As Bitmap
        Dim thisExe As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()


        Dim File As System.IO.Stream = thisExe.GetManifestResourceStream("CardLib." & imageName)
        Dim Bmp As New Bitmap(File)
        Return Bmp
    End Function

    Private Sub picCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCard.Click
        RaiseEvent CardClick(Me, New EventArgs)

    End Sub

    Private Sub picCard_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCard.DoubleClick
        RaiseEvent CardDoubleClick(Me, New EventArgs)
    End Sub

    Private Sub Card_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        RaiseEvent CardDoubleClick(Me, New EventArgs)
    End Sub

    Private Sub picCard_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCard.MouseDown
        RaiseEvent CardMouseDown(Me, e)
    End Sub

    Private Sub picCard_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCard.MouseEnter
        RaiseEvent CardMouseEnter(Me, e)
    End Sub

    Private Sub picCard_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCard.MouseLeave
        RaiseEvent CardMouseLeave(Me, e)
    End Sub

    Private Sub picCard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCard.MouseUp
        RaiseEvent CardMouseUp(Me, e)
    End Sub
End Class


Public Enum Suit
    Club
    Diamond
    Heart
    Spade
End Enum