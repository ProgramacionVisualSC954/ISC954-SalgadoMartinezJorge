Option Strict On
Option Explicit On

Public Class Form1

    Private expression As String = ""
    Private justCalculated As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Calculadora"
        Me.Size = New Size(320, 480)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.White
        Me.StartPosition = FormStartPosition.CenterScreen

        BuildUI()
    End Sub

    Private txtExpr As TextBox
    Private txtResult As TextBox

    Private Shared ReadOnly AzulMarino As Color = Color.FromArgb(13, 32, 77)
    Private Shared ReadOnly AzulClaro As Color = Color.FromArgb(230, 235, 245)
    Private Shared ReadOnly Blanco As Color = Color.White

    Private Sub BuildUI()
        txtExpr = New TextBox()
        txtExpr.ReadOnly = True
        txtExpr.TextAlign = HorizontalAlignment.Right
        txtExpr.Font = New Font("Segoe UI", 10)
        txtExpr.ForeColor = Color.Gray
        txtExpr.BackColor = Blanco
        txtExpr.BorderStyle = BorderStyle.None
        txtExpr.Size = New Size(272, 22)
        txtExpr.Location = New Point(12, 10)
        Me.Controls.Add(txtExpr)

        txtResult = New TextBox()
        txtResult.ReadOnly = True
        txtResult.TextAlign = HorizontalAlignment.Right
        txtResult.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        txtResult.ForeColor = AzulMarino
        txtResult.BackColor = Blanco
        txtResult.BorderStyle = BorderStyle.None
        txtResult.Size = New Size(272, 45)
        txtResult.Location = New Point(12, 35)
        txtResult.Text = "0"
        Me.Controls.Add(txtResult)

        Dim sep As New Panel()
        sep.BackColor = AzulMarino
        sep.Size = New Size(276, 1)
        sep.Location = New Point(12, 85)
        Me.Controls.Add(sep)

        Dim buttons As String(,) = {
            {"AC", "0", "0"}, {"(", "1", "0"}, {")", "2", "0"}, {"⌫", "3", "0"},
            {"7", "0", "1"}, {"8", "1", "1"}, {"9", "2", "1"}, {"÷", "3", "1"},
            {"4", "0", "2"}, {"5", "1", "2"}, {"6", "2", "2"}, {"×", "3", "2"},
            {"1", "0", "3"}, {"2", "1", "3"}, {"3", "2", "3"}, {"−", "3", "3"},
            {"0", "0", "4"}, {".", "1", "4"}, {"=", "2", "4"}, {"+", "3", "4"}
        }

        Dim btnW As Integer = 62
        Dim btnH As Integer = 58
        Dim startX As Integer = 12
        Dim startY As Integer = 95
        Dim gap As Integer = 8

        For i As Integer = 0 To buttons.GetLength(0) - 1
            Dim lbl As String = buttons(i, 0)
            Dim col As Integer = CInt(buttons(i, 1))
            Dim row As Integer = CInt(buttons(i, 2))

            Dim btn As New Button()
            btn.Text = lbl
            btn.Size = New Size(btnW, btnH)
            btn.Location = New Point(startX + col * (btnW + gap), startY + row * (btnH + gap))
            btn.Font = New Font("Segoe UI", 14, FontStyle.Regular)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 1
            btn.Cursor = Cursors.Hand
            btn.Tag = lbl

            Select Case lbl
                Case "="
                    ' Único botón con fondo azul marino
                    btn.BackColor = AzulMarino
                    btn.ForeColor = Blanco
                    btn.FlatAppearance.BorderColor = AzulMarino
                    btn.Font = New Font("Segoe UI", 18, FontStyle.Bold)
                Case "AC", "⌫", "(", ")", "÷", "×", "−", "+"
                    ' Operadores y especiales: fondo blanco, texto y borde azul marino
                    btn.BackColor = Blanco
                    btn.ForeColor = AzulMarino
                    btn.FlatAppearance.BorderColor = AzulMarino
                Case Else
                    ' Números: todo blanco con borde gris suave
                    btn.BackColor = Blanco
                    btn.ForeColor = AzulMarino
                    btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200)
            End Select

            AddHandler btn.Click, AddressOf ButtonClick
            Me.Controls.Add(btn)
        Next
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs)
        Dim lbl As String = CStr(CType(sender, Button).Tag)
        ProcessInput(lbl)
    End Sub

    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0"c To "9"c : ProcessInput(e.KeyChar.ToString())
            Case "."c : ProcessInput(".")
            Case "+"c : ProcessInput("+")
            Case "-"c : ProcessInput("−")
            Case "*"c : ProcessInput("×")
            Case "/"c : ProcessInput("÷")
            Case "("c : ProcessInput("(")
            Case ")"c : ProcessInput(")")
            Case Chr(13) : ProcessInput("=")
            Case Chr(8) : ProcessInput("⌫")
            Case Chr(27) : ProcessInput("AC")
        End Select
        e.Handled = True
        MyBase.OnKeyPress(e)
    End Sub

    Private Sub ProcessInput(input As String)
        Select Case input
            Case "AC"
                expression = ""
                txtExpr.Text = ""
                txtResult.Text = "0"
                justCalculated = False

            Case "⌫"
                justCalculated = False
                If expression.Length > 0 Then
                    expression = expression.Substring(0, expression.Length - 1)
                End If
                UpdateDisplay()

            Case "="
                Calculate()

            Case "+", "−", "×", "÷"
                justCalculated = False
                If expression = "" AndAlso input = "−" Then
                    expression = "−"
                ElseIf expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    If "+-×÷".IndexOf(last) >= 0 Then
                        expression = expression.Substring(0, expression.Length - 1) & input
                    ElseIf last <> "("c Then
                        expression &= input
                    End If
                End If
                UpdateDisplay()

            Case "("
                justCalculated = False
                If expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    If Char.IsDigit(last) OrElse last = ")"c Then
                        expression &= "×"
                    End If
                End If
                expression &= "("
                UpdateDisplay()

            Case ")"
                justCalculated = False
                Dim openCount As Integer = 0
                Dim closeCount As Integer = 0
                For Each c As Char In expression
                    If c = "("c Then openCount += 1
                    If c = ")"c Then closeCount += 1
                Next
                If openCount > closeCount AndAlso expression.Length > 0 Then
                    Dim last As Char = expression(expression.Length - 1)
                    If Char.IsDigit(last) OrElse last = ")"c Then
                        expression &= ")"
                    End If
                End If
                UpdateDisplay()

            Case "."
                justCalculated = False
                If expression = "" Then
                    expression = "0."
                Else
                    Dim last As Char = expression(expression.Length - 1)
                    If "+-×÷(".IndexOf(last) >= 0 Then
                        expression &= "0."
                    ElseIf last <> "."c Then
                        Dim numStart As Integer = expression.Length - 1
                        While numStart > 0 AndAlso (Char.IsDigit(expression(numStart - 1)))
                            numStart -= 1
                        End While
                        Dim currentNum As String = expression.Substring(numStart)
                        If Not currentNum.Contains(".") Then
                            expression &= "."
                        End If
                    End If
                End If
                UpdateDisplay()

            Case Else
                If justCalculated Then
                    expression = ""
                    justCalculated = False
                End If
                expression &= input
                UpdateDisplay()
        End Select
    End Sub

    Private Sub UpdateDisplay()
        txtExpr.Text = expression
        If expression = "" Then
            txtResult.Text = "0"
            Return
        End If
        Try
            Dim result As Double = EvaluateExpression(expression)
            If Double.IsInfinity(result) OrElse Double.IsNaN(result) Then Return
            txtResult.Text = FormatResult(result)
        Catch
        End Try
    End Sub

    Private Sub Calculate()
        If expression = "" Then Return
        Try
            Dim openCount As Integer = 0
            Dim closeCount As Integer = 0
            For Each c As Char In expression
                If c = "("c Then openCount += 1
                If c = ")"c Then closeCount += 1
            Next
            Dim fullExpr As String = expression & New String(")"c, openCount - closeCount)

            Dim result As Double = EvaluateExpression(fullExpr)
            If Double.IsInfinity(result) Then
                txtResult.Text = "Error: ÷0"
                Return
            End If
            txtExpr.Text = expression & " ="
            txtResult.Text = FormatResult(result)
            expression = FormatResult(result)
            justCalculated = True
        Catch ex As Exception
            txtResult.Text = "Error"
        End Try
    End Sub

    Private Function FormatResult(value As Double) As String
        If value = Math.Floor(value) AndAlso Math.Abs(value) < 1.0E+15 Then
            Return CLng(value).ToString()
        Else
            Return Math.Round(value, 10).ToString("G10")
        End If
    End Function

    Private evalPos As Integer
    Private evalExpr As String

    Private Function EvaluateExpression(expr As String) As Double
        evalExpr = expr.Replace("×", "*").Replace("÷", "/").Replace("−", "-")
        evalPos = 0
        Return ParseExpr()
    End Function

    Private Function ParseExpr() As Double
        Dim result As Double = ParseTerm()
        While evalPos < evalExpr.Length
            Dim c As Char = evalExpr(evalPos)
            If c = "+"c Then
                evalPos += 1
                result += ParseTerm()
            ElseIf c = "-"c Then
                evalPos += 1
                result -= ParseTerm()
            Else
                Exit While
            End If
        End While
        Return result
    End Function

    Private Function ParseTerm() As Double
        Dim result As Double = ParseFactor()
        While evalPos < evalExpr.Length
            Dim c As Char = evalExpr(evalPos)
            If c = "*"c Then
                evalPos += 1
                result *= ParseFactor()
            ElseIf c = "/"c Then
                evalPos += 1
                Dim divisor As Double = ParseFactor()
                result /= divisor
            Else
                Exit While
            End If
        End While
        Return result
    End Function

    Private Function ParseFactor() As Double
        If evalPos >= evalExpr.Length Then Throw New Exception("Expresión inválida")

        Dim c As Char = evalExpr(evalPos)

        If c = "("c Then
            evalPos += 1
            Dim result As Double = ParseExpr()
            If evalPos < evalExpr.Length AndAlso evalExpr(evalPos) = ")"c Then
                evalPos += 1
            End If
            Return result
        End If

        Dim sign As Double = 1
        If c = "-"c Then
            sign = -1
            evalPos += 1
        ElseIf c = "+"c Then
            evalPos += 1
        End If

        Dim numStr As String = ""
        While evalPos < evalExpr.Length AndAlso
              (Char.IsDigit(evalExpr(evalPos)) OrElse evalExpr(evalPos) = "."c)
            numStr &= evalExpr(evalPos)
            evalPos += 1
        End While

        If numStr = "" Then Throw New Exception("Se esperaba un número")
        Return sign * Double.Parse(numStr, System.Globalization.CultureInfo.InvariantCulture)
    End Function

End Class