Module Program_Release
    Sub Main()
        ' Release mode runs silently with no console window.
        Dim d As New Daemon()
        d.Run(debugMode:=False)
    End Sub
End Module
