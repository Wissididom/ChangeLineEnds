Module Module1

    Sub Main(ByVal args As String())
        Dim currentArgType As ArgTypes = ArgTypes.None
        Dim files As New List(Of String)
        Dim lineEnding As LineEndings = LineEndings.None
        For Each arg As String In args
            arg = arg.Replace("/", "-")
            If arg.ToLower() = "-file" Then
                currentArgType = ArgTypes.File
            ElseIf arg.ToLower() = "-lineending" OrElse arg.ToLower() = "-ending" Then
                currentArgType = ArgTypes.LineEnding
            Else
                Select Case currentArgType
                    Case ArgTypes.None
                        Console.WriteLine("You did't give any File")
                        Console.WriteLine("Press any key to continue")
                        Console.ReadKey()
                        Return
                    Case ArgTypes.File
                        files.Add(arg)
                    Case ArgTypes.LineEnding
                        If arg.ToUpper() = "CRLF" Then
                            lineEnding = LineEndings.CRLF
                        ElseIf arg.ToUpper() = "CR" Then
                            lineEnding = LineEndings.CR
                        ElseIf arg.ToUpper() = "LF" Then
                            lineEnding = LineEndings.LF
                        Else
                            Console.WriteLine("You have given an invalid, an empty or no line ending")
                            Console.WriteLine("Press any key to continue")
                            Console.ReadKey()
                            Return
                        End If
                End Select
            End If
        Next
        If currentArgType = ArgTypes.None AndAlso files.Count < 1 AndAlso lineEnding = LineEndings.None Then
            Console.WriteLine("You did't give any Commandlineparameters")
            Console.WriteLine("Press any key to continue")
            Console.ReadKey()
            Return
        End If
        For Each file As String In files.ToArray()
            Dim content As String = My.Computer.FileSystem.ReadAllText(file)
            content = content.Replace(vbCrLf, "<NewLine>")
            content = content.Replace(vbCr, "<NewLine>")
            content = content.Replace(vbLf, "<NewLine>")
            content = content.Replace("<NewLine>", LineEndingsToString(lineEnding))
            My.Computer.FileSystem.WriteAllText(file, content, False)
        Next
    End Sub

    Private Function LineEndingsToString(ByVal lineEnding As LineEndings) As String
        Select Case lineEnding
            Case LineEndings.None
                Return ""
            Case LineEndings.CRLF
                Return vbCrLf
            Case LineEndings.CR
                Return vbCr
            Case LineEndings.LF
                Return vbLf
            Case Else
                Return ""
        End Select
    End Function

    Private Enum ArgTypes
        None
        File
        LineEnding
    End Enum

    Private Enum LineEndings
        None
        CRLF
        CR
        LF
    End Enum
End Module
