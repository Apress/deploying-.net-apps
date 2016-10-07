Public Class frmChooseGame

    Dim _GameNumber As Integer

    Public Overloads Function ShowDialog() As DialogResult
        Dim Rnd As New System.Random(Environment.TickCount)
        txtGameNum.Text = Rnd.Next(1, 1000000).ToString

        Return MyBase.ShowDialog()
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsNumeric(txtGameNum.Text) = False Then
            txtGameNum.Text = "0"
            txtGameNum.Focus()
            Exit Sub
        End If
        Dim GameNum As Integer = CInt(txtGameNum.Text)
        If GameNum < 1 Or GameNum > 1000000 Then
            txtGameNum.Text = "0"
            txtGameNum.Focus()
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class