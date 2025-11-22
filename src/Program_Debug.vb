Module Program_Debug
    Sub Main()
        Console.Title = "CS2Daemon (Debug Mode)"
        Console.WriteLine("Starting CS2Daemon in DEBUG mode...")
        Console.WriteLine("Logs will still be written to C:\RconDaemon\CS2Daemon.log")
        Console.WriteLine()

        Dim d As New Daemon()
        d.Run(debugMode:=True)
    End Sub
End Module
