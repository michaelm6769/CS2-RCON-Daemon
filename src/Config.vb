Public Class Config

    '=============================
    ' RCON SERVER CONFIGURATION
    '=============================

    ' Your CS2 server IP
    Public Shared ReadOnly ServerIP As String = "SERVERIP"

    ' RCON port
    Public Shared ReadOnly ServerPort As Integer = 25755

    ' RCON password
    Public Shared ReadOnly RconPassword As String = "PASSWORD"



    '=============================
    ' DAEMON LOCAL CONFIGURATION
    '=============================

    ' Local daemon TCP listener port
    Public Shared ReadOnly ListenPort As Integer = 6060

End Class
