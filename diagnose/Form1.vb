Imports Microsoft.Win32
Imports System.Drawing.Printing

Public Class Form1

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Label1.Text = "Lese Systeminformationen..."
        'PictureBox1.Image = System.Systeminc.ToBitmap



        Main.lvSysComputer.Columns.Add("Name", 250)
        Main.lvSysComputer.Columns.Add("Wert", 360)

        SetData("Betriebssystem", My.Computer.Info.OSFullName, Main.lvSysComputer)
        SetData("Kernel Version", My.Computer.Info.OSVersion, Main.lvSysComputer)
        SetData("Platform", My.Computer.Info.OSPlatform, Main.lvSysComputer)
        SetData("Systemverzeichnis", Environment.SystemDirectory, Main.lvSysComputer)
        SetData("NetBIOS Name", Environment.MachineName, Main.lvSysComputer)

        SetData("Produktschlüssel", WindowsCDKey.ToString, Main.lvSysComputer)

        If Environment.HasShutdownStarted = True Then
            SetData("Herunterfahren gestartet?", "Ja", Main.lvSysComputer)
        Else
            SetData("Herunterfahren gestartet?", "Nein", Main.lvSysComputer)
        End If



        SetData("Verfügbarer virtueller Speicher", FormatBytes(My.Computer.Info.TotalVirtualMemory), Main.lvSysComputer)
        SetData("Genutzter virtueller Speicher", FormatBytes(My.Computer.Info.AvailableVirtualMemory), Main.lvSysComputer)
        SetData("Verfügbarer physischer Speicher", FormatBytes(My.Computer.Info.TotalPhysicalMemory), Main.lvSysComputer)
        SetData("Genutzter physischer Speicher", FormatBytes(My.Computer.Info.AvailablePhysicalMemory), Main.lvSysComputer)


        Label1.Text = "Ermittle installierte Programme..."

        Main.lvProgramme.Columns.Add("Name", 250)
        Main.lvProgramme.Columns.Add("Herausgeber", 250)
        Main.lvProgramme.Columns.Add("Ort", 250)

        Dim Software As String = Nothing
        'The registry key will be held in a string SoftwareKey.
        Dim SoftwareKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products"
        Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(SoftwareKey)
            For Each skName In rk.GetSubKeyNames
                'Get sub keys
                Dim name = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("DisplayName")
                'Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("DisplayName")
                Dim installocation = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("InstallLocation")
                'InstallProperties
                Dim publisher = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("Publisher")
                Dim uninstallString = Registry.LocalMachine.OpenSubKey(SoftwareKey).OpenSubKey(skName).OpenSubKey("InstallProperties").GetValue("UninstallString")
                'Add the Software information to lstPrograms



                If name.ToString <> "" Then

                    Dim LVI As New ListViewItem
                    LVI.Text = name.ToString
                    LVI.SubItems.Add(publisher.ToString)
                    LVI.SubItems.Add(installocation.ToString)

                    Main.lvProgramme.Items.Add(LVI)



                End If
                'next
            Next
        End Using

        Label1.Text = "Ermittle Drucker..."

        Main.lvDrucker.Columns.Add("Drucker", 250)

        If PrinterSettings.InstalledPrinters.Count = 0 Then
            Dim LVI As New ListViewItem
            LVI.Text = "(Keine Drucker installiert)"

            Main.lvDrucker.Items.Add(LVI)
        End If

        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In PrinterSettings.InstalledPrinters

            Dim LVI As New ListViewItem
            LVI.Text = pkInstalledPrinters

            Main.lvDrucker.Items.Add(LVI)


        Next pkInstalledPrinters


        'IP-Konfiguration ermitteln
        Label1.Text = "Ermittle IP-Konfiguration..."

        Dim p1 As New Process
        p1.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\ipconfig.exe"
        p1.StartInfo.UseShellExecute = False
        p1.StartInfo.RedirectStandardOutput = True
        p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p1.Start()
        p1.WaitForExit()
        Main.txtIPconfig.Text = p1.StandardOutput.ReadToEnd

        'Benutzer auslesen
        Label1.Text = "Lese Benutzer aus..."
        Main.lvBenutzer.Columns.Add("Name", 250)
        Main.lvBenutzer.Columns.Add("Pfad", 250)

        Dim userskey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList")
        For Each keyname As String In userskey.GetSubKeyNames()
            Using key As RegistryKey = userskey.OpenSubKey(keyname)
                Dim userpath As String = DirectCast(key.GetValue("ProfileImagePath"), String)
                Dim username As String = System.IO.Path.GetFileNameWithoutExtension(userpath)

                Dim LVI2 As New ListViewItem
                LVI2.Text = username.ToString
                LVI2.SubItems.Add(userpath.ToString)


                Main.lvBenutzer.Items.Add(LVI2)

            End Using
        Next

        'Ungebungsvariablen auslesen
        Label1.Text = "Lese Envrionment Variablen aus..."
        Main.lvEnvrionment.Columns.Add("Schlüssel", 250)
        Main.lvEnvrionment.Columns.Add("Wert", 250)


        Dim dictEntry As System.Collections.DictionaryEntry
        For Each dictEntry In Environment.GetEnvironmentVariables()
            'ListBox1.Items.Add("Key: " & (dictEntry.Key.ToString()) & "  Value: " & (dictEntry.Value.ToString()))





            Dim LVI2 As New ListViewItem
            LVI2.Text = dictEntry.Key.ToString()
            LVI2.SubItems.Add(dictEntry.Value.ToString())



            Main.lvEnvrionment.Items.Add(LVI2)


        Next

        'Schriftarten auslesen
        Label1.Text = "Lese Schriftarten aus..."
        Main.lvFonts.Columns.Add("Name", 250)

        Dim installedFonts As New System.Drawing.Text.InstalledFontCollection
        Dim fontFamilies = installedFonts.Families()

        For Each fontFamily In fontFamilies


            Dim LVI2 As New ListViewItem
            LVI2.Text = fontFamily.Name
            Main.lvFonts.Items.Add(LVI2)
        Next

        Me.Hide()
        Main.Show()
    End Sub

    Dim DoubleBytes As Double
    Default Public Property FormatBytes(ByVal BytesCaller As ULong) As String
        Get
            Try
                Select Case BytesCaller
                    Case Is >= 1099511627776
                        DoubleBytes = CDbl(BytesCaller / 1099511627776) 'TB
                        Return FormatNumber(DoubleBytes, 2) & " TB"
                    Case 1073741824 To 1099511627775
                        DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                        Return FormatNumber(DoubleBytes, 2) & " GB"
                    Case 1048576 To 1073741823
                        DoubleBytes = CDbl(BytesCaller / 1048576) 'MB
                        Return FormatNumber(DoubleBytes, 2) & " MB"
                    Case 1024 To 1048575
                        DoubleBytes = CDbl(BytesCaller / 1024) 'KB
                        Return FormatNumber(DoubleBytes, 2) & " KB"
                    Case 0 To 1023
                        DoubleBytes = BytesCaller ' bytes
                        Return FormatNumber(DoubleBytes, 2) & " bytes"
                    Case Else
                        Return ""
                End Select
            Catch
                Return ""
            End Try
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Sub SetData(ByVal Name As String, ByVal Wert As String, ByVal lv As ListView)
        Dim Item As New ListViewItem(Name)
        Item.SubItems.Add(Wert)
        lv.Items.Add(Item)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Public Shared ReadOnly Property WindowsCDKey() As String
        Get
            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion")
            Dim rpk As Byte() = rKey.GetValue("DigitalProductId", 0)
            Dim strKey As String = ""
            Const iRPKOffset As Integer = 52
            Const strPossibleChars As String = "BCDFGHJKMPQRTVWXY2346789"
            Dim i As Integer = 28
            Do
                Dim lAccu As Long = 0
                Dim j As Integer = 14
                Do
                    lAccu *= 256
                    lAccu += Convert.ToInt64(rpk(iRPKOffset + j))
                    rpk(iRPKOffset + j) = Convert.ToByte(Convert.ToInt64(Math.Floor(CSng(lAccu) / 24.0F)) And Convert.ToInt64(255))
                    lAccu = lAccu Mod 24
                    j -= 1
                Loop While j >= 0
                i -= 1
                strKey = strPossibleChars(CInt(lAccu)).ToString() + strKey
                If (0 = ((29 - i) Mod 6)) AndAlso (-1 <> i) Then
                    i -= 1
                    strKey = "-" + strKey
                End If
            Loop While i >= 0
            Return strKey
        End Get
    End Property

    Public Shared ReadOnly Property WindowsCDKeyParts() As String()
        Get
            Dim strKeyParts As String() = WindowsCDKey.Split("-"c)
            Return strKeyParts
        End Get
    End Property
End Class
