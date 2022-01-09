Public Class FrmLeistung

    Private Sub FrmLeistung_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick


        ProgressBar1.Maximum = 100
        ProgressBar1.Value = My.Computer.Info.AvailableVirtualMemory / My.Computer.Info.TotalVirtualMemory * 100

        ProgressBar2.Maximum = 100
        ProgressBar2.Value = My.Computer.Info.AvailablePhysicalMemory / My.Computer.Info.TotalPhysicalMemory * 100

        Label3.Text = ProgressBar1.Value & " % (" & Form1.FormatBytes(My.Computer.Info.AvailableVirtualMemory) & " / " & Form1.FormatBytes(My.Computer.Info.TotalVirtualMemory) & ")"
        Label4.Text = ProgressBar2.Value & " % (" & Form1.FormatBytes(My.Computer.Info.AvailablePhysicalMemory) & " / " & Form1.FormatBytes(My.Computer.Info.TotalPhysicalMemory) & ")"

        Dim power As PowerStatus = SystemInformation.PowerStatus
        ProgressBar3.Value = power.BatteryLifePercent * 100
        Label5.Text = power.BatteryLifePercent * 100 & "%"
        If power.PowerLineStatus = PowerLineStatus.Online And (power.BatteryLifePercent * 100) < 100 Then
            Label6.Text = "Netzbetrieb, Akku wird aufgeladen..."
            PictureBox1.BackgroundImage = My.Resources.b_stedo
        ElseIf power.PowerLineStatus = PowerLineStatus.Online Then
            Label6.Text = "Netzbetrieb"
            PictureBox1.BackgroundImage = My.Resources.b_onlystedo
        ElseIf power.PowerLineStatus = PowerLineStatus.Offline Then
            Label6.Text = "Akkubetrieb"
            PictureBox1.BackgroundImage = My.Resources.b_battery
        End If

    End Sub




    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.TopMost = CheckBox1.Checked
    End Sub
End Class