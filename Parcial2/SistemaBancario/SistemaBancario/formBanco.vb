Public Class SisBancario

    Private cuenta As CuentaBancaria = Nothing
    Private cliente As Persona = Nothing

    Private Sub SisBancario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listMovimientos.Items.Clear()
        lblSaldoActual.Text = "Saldo actual: --"
        btnRetirar.Enabled = False
    End Sub

    Private Sub section1_Enter(sender As Object, e As EventArgs) Handles section1.Enter
    End Sub

    Private Sub txtSaldoInicial_TextChanged(sender As Object, e As EventArgs) Handles txtSaldoInicial.TextChanged
    End Sub

    Private Sub seccion2_Enter(sender As Object, e As EventArgs) Handles seccion2.Enter
    End Sub

    Private Sub lblEdad_Click(sender As Object, e As EventArgs) Handles lblEdad.Click
    End Sub

    Private Sub txtEdad_TextChanged(sender As Object, e As EventArgs) Handles txtEdad.TextChanged
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub lblSaldoActual_Click(sender As Object, e As EventArgs) Handles lblSaldoActual.Click
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
    End Sub

    Private Sub txtMontoRetiro_TextChanged(sender As Object, e As EventArgs) Handles txtMontoRetiro.TextChanged
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
    End Sub

    Private Sub txtMontoPrestamo_TextChanged(sender As Object, e As EventArgs) Handles txtMontoPrestamo.TextChanged
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
    End Sub

    Private Sub txtMeses_TextChanged(sender As Object, e As EventArgs) Handles txtMeses.TextChanged
    End Sub

    Private Sub listMovimientos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listMovimientos.SelectedIndexChanged
    End Sub

    Private Sub btnRegistrarCliente_Click(sender As Object, e As EventArgs) Handles btnRegistrarCliente.Click
        Dim edadIngresada As Integer

        If Not Integer.TryParse(txtEdad.Text, edadIngresada) Then
            MessageBox.Show("Ingresa una edad válida (número entero).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            cliente = New Persona()
            cliente.Edad = edadIngresada
            MessageBox.Show($"Cliente registrado con edad {cliente.Edad} años.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As ArgumentOutOfRangeException
            MessageBox.Show(ex.Message, "Edad fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cliente = Nothing
        End Try
    End Sub

    Private Sub btnAbrirCuenta_Click(sender As Object, e As EventArgs) Handles btnAbrirCuenta.Click
        Dim saldoInicial As Decimal

        If cliente Is Nothing Then
            MessageBox.Show("Primero registra al cliente.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(txtSaldoInicial.Text, saldoInicial) OrElse saldoInicial < 0 Then
            MessageBox.Show("Ingresa un saldo inicial válido (número, no negativo).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        cuenta = New CuentaBancaria(saldoInicial)
        lblSaldoActual.Text = $"Saldo actual: {cuenta.Saldo:C}"
        btnRetirar.Enabled = True

        listMovimientos.Items.Add($"Cuenta abierta con saldo inicial {saldoInicial:C}")
    End Sub

    Private Sub btnRetirar_Click(sender As Object, e As EventArgs) Handles btnRetirar.Click
        Dim monto As Decimal

        If cuenta Is Nothing Then
            MessageBox.Show("Primero abre una cuenta.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(txtMontoRetiro.Text, monto) OrElse monto <= 0 Then
            MessageBox.Show("Ingresa un monto de retiro válido (mayor a 0).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            cuenta.Retirar(monto)
            lblSaldoActual.Text = $"Saldo actual: {cuenta.Saldo:C}"
            listMovimientos.Items.Add($"Retiro de {monto:C} — Nuevo saldo: {cuenta.Saldo:C}")
            txtMontoRetiro.Clear()
        Catch ex As SaldoInsuficienteException
            MessageBox.Show(ex.Message, "Saldo insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCalcularPago_Click(sender As Object, e As EventArgs) Handles btnCalcularPago.Click
        Dim montoPrestamo As Decimal
        Dim meses As Integer

        If Not Decimal.TryParse(txtMontoPrestamo.Text, montoPrestamo) OrElse montoPrestamo <= 0 Then
            MessageBox.Show("Ingresa un monto de préstamo válido (mayor a 0).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Integer.TryParse(txtMeses.Text, meses) Then
            MessageBox.Show("Ingresa un número de meses válido (entero).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim pagoMensual As Decimal = Operaciones.CalcularPagoMensual(montoPrestamo, meses)
            MessageBox.Show($"Pago mensual: {pagoMensual:C}", "Cálculo de préstamo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As DivideByZeroException
            MessageBox.Show(ex.Message, "Error de cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class