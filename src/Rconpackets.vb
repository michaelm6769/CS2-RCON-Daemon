Imports System.Text

Public Class RconPackets

    ' Packet Types
    Private Const SERVERDATA_AUTH As Integer = 3
    Private Const SERVERDATA_AUTH_RESPONSE As Integer = 2
    Private Const SERVERDATA_EXECCOMMAND As Integer = 2
    Private Const SERVERDATA_RESPONSE_VALUE As Integer = 0

    ' Hardcoded request ID
    Private Const REQUEST_ID As Integer = 12345


    ''' <summary>
    ''' Builds an AUTH packet
    ''' </summary>
    Public Shared Function BuildAuthPacket(password As String) As Byte()
        Dim payloadBytes() As Byte = Encoding.UTF8.GetBytes(password)

        ' Length = ID(4) + Type(4) + Payload + 2 nulls
        Dim length As Integer = 4 + 4 + payloadBytes.Length + 2

        Dim packet As New List(Of Byte)

        ' Packet Size (4 bytes, LE)
        packet.AddRange(BitConverter.GetBytes(length))

        ' Request ID (4 bytes)
        packet.AddRange(BitConverter.GetBytes(REQUEST_ID))

        ' Type = SERVERDATA_AUTH (4 bytes)
        packet.AddRange(BitConverter.GetBytes(SERVERDATA_AUTH))

        ' Password payload
        packet.AddRange(payloadBytes)

        ' Null terminators
        packet.Add(0)
        packet.Add(0)

        Return packet.ToArray()
    End Function



    ''' <summary>
    ''' Build a command execution packet
    ''' </summary>
    Public Shared Function BuildCommandPacket(cmd As String) As Byte()
        Dim payloadBytes() As Byte = Encoding.UTF8.GetBytes(cmd)

        ' Length = ID(4) + Type(4) + Payload + 2 nulls
        Dim length As Integer = 4 + 4 + payloadBytes.Length + 2

        Dim packet As New List(Of Byte)

        ' Packet Size (4 bytes, LE)
        packet.AddRange(BitConverter.GetBytes(length))

        ' Request ID
        packet.AddRange(BitConverter.GetBytes(REQUEST_ID))

        ' Type = EXEC COMMAND
        packet.AddRange(BitConverter.GetBytes(SERVERDATA_EXECCOMMAND))

        ' Command payload
        packet.AddRange(payloadBytes)

        ' Two null terminators
        packet.Add(0)
        packet.Add(0)

        Return packet.ToArray()
    End Function

End Class
