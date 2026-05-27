Option Strict On
Option Explicit On

Public Class Form1

    Private expression As String = ""
    Private justCalculated As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Calculadora"
        Me.Size = New Size(320, 540)
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

        ' Fila 0: AC  ( )  ⌫  xʸ
        ' Fila 1:  7   8   9   ÷
        ' Fila 2:  4   5   6   ×
        ' Fila 3:  1   2   3   −
        ' Fila 4:  0   .   √   +
        ' Fila 5:  =  (span 4)
        Dim buttons As String(,) = {
            {"AC", "0", "0"}, {"( )", "1", "0"}, {"⌫", "2", "0"}, {"xʸ", "3", "0"},
            {"7", "0", "1"}, {"8", "1", "1"}, {"9", "2", "1"}, {"÷", "3", "1"},
            {"4", "0", "2"}, {"5", "1", "2"}, {"6", "2", "2"}, {"×", "3", "2"},
            {"1", "0", "3"}, {"2", "1", "3"}, {"3", "2", "3"}, {"−", "3", "3"},
            {"0", "0", "4"}, {".", "1", "4"}, {"√", "2", "4"}, {"+", "3", "4"}
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
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 1
            btn.Cursor = Cursors.Hand
            btn.Tag = lbl

            Select Case lbl
                Case "AC", "⌫", "( )", "÷", "×", "−", "+"
                    btn.Font = New Font("Segoe UI", 14, FontStyle.Regular)
                    btn.BackColor = Blanco
                    btn.ForeColor = AzulMarino
                    btn.FlatAppearance.BorderColor = AzulMarino
                Case "xʸ", "√"
                    btn.Font = New Font("Segoe UI", 13, FontStyle.Regular)
                    btn.BackColor = AzulClaro
                    btn.ForeColor = AzulMarino
                    btn.FlatAppearance.BorderColor = AzulMarino
                Case Else
                    btn.Font = New Font("Segoe UI", 14, FontStyle.Regular)
                    btn.BackColor = Blanco
                    btn.ForeColor = AzulMarino
                    btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200)
            End Select

            AddHandler btn.Click, AddressOf ButtonClick
            Me.Controls.Add(btn)
        Next

        ' Botón = ocupa todo el ancho
        Dim btnEq As New Button()
        btnEq.Text = "="
        btnEq.Size = New Size(btnW * 4 + gap * 3, btnH)
        btnEq.Location = New Point(startX, startY + 5 * (btnH + gap))
        btnEq.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        btnEq.FlatStyle = FlatStyle.Flat
        btnEq.FlatAppearance.BorderSize = 1
        btnEq.BackColor = AzulMarino
        btnEq.ForeColor = Blanco
        btnEq.FlatAppearance.BorderColor = AzulMarino
        btnEq.Cursor = Cursors.Hand
        btnEq.Tag = "="
        AddHandler btnEq.Click, AddressOf ButtonClick
        Me.Controls.Add(btnEq)
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
            Case "^"c : ProcessInput("^")
            Case "("c : ProcessInput("(")
            Case ")"c : ProcessInput(")")
            Case Chr(13) : ProcessInput("=")
            Case Chr(8) : ProcessInput("⌫")
            Case Chr(27) : ProcessInput("AC")
        End Select
        e.Handled = True
        MyBase.OnKeyPress(e)
    End Sub

    ' ---------------------------------------------------------------
    '  Helpers
    ' ---------------------------------------------------------------
    Private Function IsOperatorChar(c As Char) As Boolean
        Return "+-×÷^".IndexOf(c) >= 0
    End Function

    Private Function IsDigitOrClose(c As Char) As Boolean
        Return Char.IsDigit(c) OrElse c = ")"c
    End Function

    ' ---------------------------------------------------------------
    '  Procesamiento de entrada
    ' ---------------------------------------------------------------
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

            ' ----- Potencia -----
            Case "^"
                justCalculated = False
                If expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    If IsDigitOrClose(last) Then
                        expression &= "^"
                    End If
                End If
                UpdateDisplay()

            ' ----- Raíz cuadrada -----
            Case "√"
                justCalculated = False
                If expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    ' Paréntesis implícito: 3√( → 3×√(
                    If IsDigitOrClose(last) Then
                        expression &= "×"
                    End If
                End If
                expression &= "√("
                UpdateDisplay()

            ' ----- Botón ( ) inteligente -----
            Case "( )"
                justCalculated = False
                Dim openCount As Integer = 0
                Dim closeCount As Integer = 0
                For Each c As Char In expression
                    If c = "("c Then openCount += 1
                    If c = ")"c Then closeCount += 1
                Next

                If openCount > closeCount AndAlso expression.Length > 0 Then
                    Dim last As Char = expression(expression.Length - 1)
                    If IsDigitOrClose(last) Then
                        ' Cerrar paréntesis pendiente
                        expression &= ")"
                    Else
                        ' Después de operador → abrir nuevo
                        expression &= "("
                    End If
                Else
                    ' No hay pendientes → abrir, con × implícito si hace falta
                    If expression <> "" Then
                        Dim last As Char = expression(expression.Length - 1)
                        If IsDigitOrClose(last) Then expression &= "×"
                    End If
                    expression &= "("
                End If
                UpdateDisplay()

            ' ----- Paréntesis individuales (teclado) -----
            Case "("
                justCalculated = False
                If expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    If IsDigitOrClose(last) Then expression &= "×"
                End If
                expression &= "("
                UpdateDisplay()

            Case ")"
                justCalculated = False
                Dim openC As Integer = 0
                Dim closeC As Integer = 0
                For Each c As Char In expression
                    If c = "("c Then openC += 1
                    If c = ")"c Then closeC += 1
                Next
                If openC > closeC AndAlso expression.Length > 0 Then
                    Dim last As Char = expression(expression.Length - 1)
                    If IsDigitOrClose(last) Then expression &= ")"
                End If
                UpdateDisplay()

            ' ----- Operadores +  −  ×  ÷ -----
            Case "+", "−", "×", "÷"
                justCalculated = False
                If expression = "" AndAlso input = "−" Then
                    expression = "−"
                ElseIf expression <> "" Then
                    Dim last As Char = expression(expression.Length - 1)
                    If IsOperatorChar(last) Then
                        ' Reemplazar operador anterior (excepto −  negativo)
                        expression = expression.Substring(0, expression.Length - 1) & input
                    ElseIf last <> "("c Then
                        expression &= input
                    End If
                End If
                UpdateDisplay()

            ' ----- Punto decimal -----
            Case "."
                justCalculated = False
                If expression = "" Then
                    expression = "0."
                Else
                    Dim last As Char = expression(expression.Length - 1)
                    If IsOperatorChar(last) OrElse last = "("c Then
                        expression &= "0."
                    ElseIf last <> "."c Then
                        ' Verificar que el número actual no tenga ya punto
                        Dim numStart As Integer = expression.Length - 1
                        While numStart > 0 AndAlso Char.IsDigit(expression(numStart - 1))
                            numStart -= 1
                        End While
                        Dim currentNum As String = expression.Substring(numStart)
                        If Not currentNum.Contains(".") Then expression &= "."
                    End If
                End If
                UpdateDisplay()

                ' ----- Dígitos -----
            Case Else
                If justCalculated Then
                    expression = ""
                    justCalculated = False
                End If
                expression &= input
                UpdateDisplay()

        End Select
    End Sub

    ' ---------------------------------------------------------------
    '  Actualizar pantalla en tiempo real
    ' ---------------------------------------------------------------
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

    ' ---------------------------------------------------------------
    '  Calcular al pulsar =
    ' ---------------------------------------------------------------
    Private Sub Calculate()
        If expression = "" Then Return
        Try
            ' Cerrar paréntesis pendientes
            Dim openCount As Integer = 0
            Dim closeCount As Integer = 0
            For Each c As Char In expression
                If c = "("c Then openCount += 1
                If c = ")"c Then closeCount += 1
            Next
            Dim fullExpr As String = expression & New String(")"c, Math.Max(0, openCount - closeCount))

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

    ' ===============================================================
    '  PARSER  (recursive-descent)
    '  Precedencia: + −  <  × ÷  <  ^ (derecha)  <  unario  <  ()  √
    ' ===============================================================
    Private evalPos As Integer
    Private evalExpr As String

    Private Function EvaluateExpression(expr As String) As Double
        ' Normalizar operadores a ASCII
        evalExpr = expr _
            .Replace("×", "*") _
            .Replace("÷", "/") _
            .Replace("−", "-")
        evalPos = 0
        Return ParseAddSub()
    End Function

    ' Nivel 1: suma y resta
    Private Function ParseAddSub() As Double
        Dim result As Double = ParseMulDiv()
        While evalPos < evalExpr.Length
            Dim c As Char = evalExpr(evalPos)
            If c = "+"c Then
                evalPos += 1
                result += ParseMulDiv()
            ElseIf c = "-"c Then
                evalPos += 1
                result -= ParseMulDiv()
            Else
                Exit While
            End If
        End While
        Return result
    End Function

    ' Nivel 2: multiplicación y división
    Private Function ParseMulDiv() As Double
        Dim result As Double = ParsePow()
        While evalPos < evalExpr.Length
            Dim c As Char = evalExpr(evalPos)
            If c = "*"c Then
                evalPos += 1
                result *= ParsePow()
            ElseIf c = "/"c Then
                evalPos += 1
                Dim divisor As Double = ParsePow()
                result /= divisor
            Else
                Exit While
            End If
        End While
        Return result
    End Function

    ' Nivel 3: potencia (asociativa por la derecha: 2^3^2 = 2^(3^2))
    Private Function ParsePow() As Double
        Dim base As Double = ParseUnary()
        If evalPos < evalExpr.Length AndAlso evalExpr(evalPos) = "^"c Then
            evalPos += 1
            Dim exp As Double = ParsePow()   ' recursión derecha
            Return Math.Pow(base, exp)
        End If
        Return base
    End Function

    ' Nivel 4: signo unario
    Private Function ParseUnary() As Double
        If evalPos < evalExpr.Length Then
            Dim c As Char = evalExpr(evalPos)
            If c = "-"c Then
                evalPos += 1
                Return -ParsePrimary()
            ElseIf c = "+"c Then
                evalPos += 1
            End If
        End If
        Return ParsePrimary()
    End Function

    ' Nivel 5: número, paréntesis, √(
    Private Function ParsePrimary() As Double
        If evalPos >= evalExpr.Length Then Throw New Exception("Expresión inválida")

        Dim c As Char = evalExpr(evalPos)

        ' Paréntesis
        If c = "("c Then
            evalPos += 1
            Dim result As Double = ParseAddSub()
            If evalPos < evalExpr.Length AndAlso evalExpr(evalPos) = ")"c Then
                evalPos += 1
            End If
            Return result
        End If

        ' Raíz cuadrada: √(expr)
        If evalExpr.Length - evalPos >= 2 AndAlso
           evalExpr(evalPos) = "√"c AndAlso evalExpr(evalPos + 1) = "("c Then
            evalPos += 2          ' saltar √(
            Dim inner As Double = ParseAddSub()
            If evalPos < evalExpr.Length AndAlso evalExpr(evalPos) = ")"c Then
                evalPos += 1
            End If
            Return Math.Sqrt(inner)
        End If

        ' Número
        Dim numStr As String = ""
        While evalPos < evalExpr.Length AndAlso
              (Char.IsDigit(evalExpr(evalPos)) OrElse evalExpr(evalPos) = "."c)
            numStr &= evalExpr(evalPos)
            evalPos += 1
        End While

        If numStr = "" Then Throw New Exception("Se esperaba un número")
        Return Double.Parse(numStr, System.Globalization.CultureInfo.InvariantCulture)
    End Function

End Class