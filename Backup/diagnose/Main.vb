Imports Microsoft.Win32

Public Class Main

    Private Sub Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()

    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub ListView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSysComputer.MouseDown

        If e.Button = MouseButtons.Right Then
            ' ContextMenuStrip1.Show(e.Location)
        End If
    End Sub


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        TabControl1.SelectedIndex = 2
    End Sub

    Private Sub AktionenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AktionenToolStripMenuItem.Click

    End Sub

    Private Sub BeendenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeendenToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        TabControl1.SelectedIndex = 3
    End Sub

    Private Sub GeheZuSymbolleisteAnzeigenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeheZuSymbolleisteAnzeigenToolStripMenuItem.Click
        If GeheZuSymbolleisteAnzeigenToolStripMenuItem.Checked = False Then
            GeheZuSymbolleisteAnzeigenToolStripMenuItem.Checked = True
            ToolStrip1.Visible = True
        Else
            GeheZuSymbolleisteAnzeigenToolStripMenuItem.Checked = False
            ToolStrip1.Visible = False
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        TabControl1.SelectedIndex = 4
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        TabControl1.SelectedIndex = 5
    End Sub


  

    Private Sub SpeichermonitorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeichermonitorToolStripMenuItem.Click
        FrmLeistung.Show()

    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        TabControl1.SelectedIndex = 6
    End Sub

    Private Sub ÜberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÜberToolStripMenuItem.Click
        About.ShowDialog()

    End Sub

    Private Sub BerichtErstellenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BerichtErstellenToolStripMenuItem.Click
        MsgBox("Funktion noch nicht implementiert")
    End Sub
End Class