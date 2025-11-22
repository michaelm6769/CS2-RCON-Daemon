Imports System.Net.Sockets
Imports System.Text

Public Class RconClient

    Private client As TcpClient
    Private stream As NetworkStream
    Private connected As Boolean = False

    Public Function Connect() As Boolean
        Console.WriteLine("Attempting RCON connection...")

        Try
            client = New TcpClient()
            client.ReceiveTimeout = 3000
            client.SendTimeout = 3000

            Console.WriteLine("Connecting to " & Config.ServerIP & ":" & Config.ServerPort)
            client.Connect(Config.ServerIP, Config.ServerPort)

            Console.WriteLine("Connected! Sending auth...")
            stream = client.GetStream()

            Dim authPacket As Byte() = RconPackets.BuildAuthPacket(Config.RconPassword)
            stream.Write(authPacket, 0, authPacket.Length)

            Dim buffer(4096) As Byte
            Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)

            Console.WriteLine("Auth response received: " & bytesRead & " bytes")

            Dim id As Integer = BitConverter.ToInt32(buffer, 4)
            Dim type As Integer = BitConverter.ToInt32(buffer, 8)

            Console.WriteLine("Auth ID=" & id & " Type=" & type)

            If id = 12345 AndAlso type = 2 Then
                Console.WriteLine("RCON authenticated successfully.")
                connected = True
                Return True
            End If

            Console.WriteLine("Authentication failed.")

        Catch ex As Exception
            Console.WriteLine("RCON connect exception: " & ex.Message)
        End Try

        connected = False
        Return False
    End Function



    Public Function SendCommand(cmd As String) As String
        If Not connected Then
            Logger.Log("RCON disconnected — trying reconnect...")
            If Not Connect() Then
                Return "ERROR: Could not reconnect to RCON."
            End If
        End If

        Try
            ' Build command packet
            Dim cmdPacket As Byte() = RconPackets.BuildCommandPacket(cmd)
            stream.Write(cmdPacket, 0, cmdPacket.Length)

            ' Read response
            Dim buffer(4096) As Byte
            Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)

            If bytesRead <= 0 Then
                Logger.Log("RCON empty response — forcing reconnect.")
                connected = False
                Return "ERROR: Empty response from server."
            End If

            Dim size As Integer = BitConverter.ToInt32(buffer, 0)
            Dim payload As String = Encoding.UTF8.GetString(buffer, 12, size - 10)

            Return payload

        Catch ex As Exception
            Logger.Log("RCON CMD error: " & ex.Message)
            connected = False
            Return "ERROR: " & ex.Message
        End Try
    End Function

End Class
