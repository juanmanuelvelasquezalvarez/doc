'If the year has or not 1 week more
Function days(y As Long) As Integer
    r = Round(Round(y * 1.242189 / 7) * 7 / 1.242189)
    If y = r Then
        days = 371
    Else
        days = 364
    End If
End Function
'Date as number in Excel and version
Function ideal(f As Long, v As Integer) As String
    Dim n, d, a, m As Integer
    Dim y As Long
    'Days since the glorious change of millennium
    '36526 days from 1900 to 2000 in Excel
    a = f - 36526
    'Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
    d = 6 + a
    y = 2000 'Year initiating in 2000
    If a > 0 Then
        n = days(y)
        While d >= n
            d = d - n
            y = y + 1
            n = days(y)
        Wend
    Else
        While d < 0
            y = y - 1
            d = d + days(y)
        Wend
    End If
    ideal = ""
    'Day of the year, day (1 to 28) and month or day (1 to 7) and week and the year
    If v = 1 Then
        m = 28
    ElseIf v = 2 Then
        m = 7
    End If
    If v = 1 Or v = 2 Then
        ideal = ((d Mod m) + 1) & " " & (Round(d / m) + 1) & " " & y
    ElseIf v = 3 Then
        ideal = d & " " & y
    End If
End Function
'Year and day
Function gregorian(d As Integer, y As Long) As Long
    'The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
    gregorian = 36519 + d
    Dim i As Long
    i = 2000
    If y > 2000 Then
        While i < y
            gregorian = gregorian + days(i)
            i = i + 1
        Wend
    End If
    If y < 2000 Then
        While i > y
            i = i - 1
            gregorian = gregorian - days(i)
        Wend
    End If
End Function