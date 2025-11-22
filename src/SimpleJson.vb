Imports System.Text.RegularExpressions

Public Class SimpleJson

    Public Shared Function GetValue(json As String, key As String) As String
        If String.IsNullOrWhiteSpace(json) Then
            Return ""
        End If

        Try
            ' Regex: "key" : "value"
            Dim pattern As String = """" & key & """" & "\s*:\s*""([^""]*)"""
            Dim m As Match = Regex.Match(json, pattern)

            If m.Success Then
                Return m.Groups(1).Value
            End If

        Catch ex As Exception
            ' ignore all parsing issues
        End Try

        Return ""
    End Function

End Class
