Imports System.IO
Imports System.Text

Public Class Logger

    Private Shared logPath As String = "C:\RconDaemon\CS2Daemon.log"
    Private Shared lockObj As New Object()

    Public Shared Sub Log(msg As String)
        Try
            Dim line As String = "[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "] " & msg

            SyncLock lockObj
                Dim dir As String = Path.GetDirectoryName(logPath)
                If Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If

                File.AppendAllText(logPath, line & Environment.NewLine, Encoding.UTF8)
            End SyncLock

        Catch
        End Try
    End Sub

End Class
