Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.IO
Imports System.Threading

Public Class Daemon

    Private listener As TcpListener
    Private running As Boolean = True

    Public Sub Run(debugMode As Boolean)

        Logger.Log("Daemon starting...")

        ' Ensure log directory exists
        Try
            If Not Directory.Exists("C:\RconDaemon") Then
                Directory.CreateDirectory("C:\RconDaemon")
            End If
        Catch ex As Exception
            ' ignore
        End Try

        ' Create persistent RCON instance
        Dim rcon As New RconClient()
        rcon.Connect()

        ' Create TCP listener
        Try
            Console.WriteLine("Creating listener on port " & Config.ListenPort & "...")
            listener = New TcpListener(IPAddress.Any, Config.ListenPort)

            listener.Start()
            Console.WriteLine(">>> Listener actually started!")
        Catch ex As Exception
            Console.WriteLine(">>> Listener FAILED: " & ex.Message)
        End Try



        Logger.Log("TCP listener active on 127.0.0.1:" & Config.ListenPort)

        If debugMode Then
            Console.WriteLine("Daemon listening on 127.0.0.1:" & Config.ListenPort)
        End If

        ' Main loop
        While running
            Try
                If Not listener.Pending() Then
                    Thread.Sleep(20)
                    Continue While
                End If

                Dim client As TcpClient = listener.AcceptTcpClient()

                ' Handle each client on separate thread
                Dim t As New Thread(Sub() HandleClient(client, rcon, debugMode))
                t.IsBackground = True
                t.Start()

            Catch ex As Exception
                Logger.Log("Listener error: " & ex.Message)
            End Try
        End While

    End Sub


    Private Sub HandleClient(client As TcpClient, rcon As RconClient, debugMode As Boolean)
        Using client
            Try
                Dim stream = client.GetStream()
                Dim reader As New StreamReader(stream, New UTF8Encoding(False))
                Dim writer As New StreamWriter(stream, New UTF8Encoding(False)) With {.AutoFlush = True}

                Dim incoming As String = reader.ReadLine()
                If incoming Is Nothing Then Exit Sub

                If debugMode Then
                    Console.WriteLine("Received: " & incoming)
                End If

                Dim command As String = SimpleJson.GetValue(incoming, "cmd")

                If debugMode Then
                    Console.WriteLine("CMD => " & command)
                End If

                Dim response As String = rcon.SendCommand(command)
                Dim b64 As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(response))

                Dim jsonResponse As String =
                    "{""ok"":true,""output_b64"":""" & b64 & """}"

                writer.WriteLine(jsonResponse)

                Logger.Log("Executed command: " & command)

            Catch ex As Exception
                Logger.Log("Client handler error: " & ex.Message)
            End Try
        End Using
    End Sub

End Class
