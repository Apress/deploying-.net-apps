Public Class frmStatistics


    Public Overloads Function ShowDialog(ByRef sessionWon As Integer, ByRef sessionLost As Integer) As DialogResult
        lblSessionWon.Text = sessionWon.ToString
        lblSessionLost.Text = sessionLost.ToString
        Dim Pct As Integer = 0
        Dim Count As Integer = sessionWon + sessionLost
        If Count > 0 Then Pct = CInt(sessionWon / Count * 100)
        lblSessionPct.Text = Pct.ToString & "%"

        Dim StreakType As String = GetSetting(Application.ProductName, "Score", "StreakType")
        Dim StreakCount As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Streak"))
        Dim TotalWon As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Won"))
        Dim TotalLost As Integer = cInteger(GetSetting(Application.ProductName, "Score", "Lost"))
        Dim MaxWinStreak As Integer = cInteger(GetSetting(Application.ProductName, "Score", "MaxWinStreak"))
        Dim MaxLoseStreak As Integer = cInteger(GetSetting(Application.ProductName, "Score", "MaxLoseStreak"))

        lblTotalWon.Text = TotalWon.ToString
        lblTotalLost.Text = TotalLost.ToString
        Dim TPct As Integer = 0
        Dim TCount As Integer = TotalWon + TotalLost
        If TCount > 0 Then TPct = CInt(TotalWon / TCount * 100)
        lblTotalPct.Text = TPct.ToString & "%"
        lblCurrentStreak.Text = StreakCount.ToString
        lblStreakType.Text = StreakType
        lblMaxWinStreak.Text = MaxWinStreak.ToString
        lblMaxLoseStreak.Text = MaxLoseStreak.ToString

        Return MyBase.ShowDialog
    End Function

    Private Function cInteger(ByVal value As Object) As Integer
        If IsNumeric(value) Then Return CInt(value) Else Return 0
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub LoadTotals()

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If MessageBox.Show("Are you sure you want to delete all statistics?", "FreeCell", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            SaveSetting(Application.ProductName, "Score", "StreakType", "")
            SaveSetting(Application.ProductName, "Score", "Streak", "0")
            SaveSetting(Application.ProductName, "Score", "Won", "0")
            SaveSetting(Application.ProductName, "Score", "Lost", "0")
            SaveSetting(Application.ProductName, "Score", "MaxWinStreak", "0")
            SaveSetting(Application.ProductName, "Score", "MaxLoseStreak", "0")
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()
        End If
    End Sub
End Class