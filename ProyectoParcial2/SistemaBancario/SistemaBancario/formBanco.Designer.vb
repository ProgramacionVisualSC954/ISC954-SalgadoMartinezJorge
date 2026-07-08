<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SisBancario
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        section1 = New GroupBox()
        btnRegistrarCliente = New Button()
        txtEdad = New TextBox()
        lblEdad = New Label()
        lblEstadoCliente = New Label()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        Label1 = New Label()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        seccion2 = New GroupBox()
        listMovimientos = New ListBox()
        btnRetirar = New Button()
        txtMontoRetiro = New TextBox()
        btnAbrirCuenta = New Button()
        txtSaldoInicial = New TextBox()
        Label4 = New Label()
        lblSaldoActual = New Label()
        Label2 = New Label()
        GroupBox1 = New GroupBox()
        txtMeses = New TextBox()
        Label7 = New Label()
        btnCalcularPago = New Button()
        txtMontoPrestamo = New TextBox()
        lblPagoMensual = New Label()
        Label6 = New Label()
        section1.SuspendLayout()
        seccion2.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' section1
        ' 
        section1.Controls.Add(btnRegistrarCliente)
        section1.Controls.Add(txtEdad)
        section1.Controls.Add(lblEdad)
        section1.Controls.Add(lblEstadoCliente)
        section1.Location = New Point(34, 42)
        section1.Name = "section1"
        section1.Size = New Size(200, 159)
        section1.TabIndex = 0
        section1.TabStop = False
        section1.Text = "Registrar"
        ' 
        ' btnRegistrarCliente
        ' 
        btnRegistrarCliente.Location = New Point(47, 115)
        btnRegistrarCliente.Name = "btnRegistrarCliente"
        btnRegistrarCliente.Size = New Size(106, 23)
        btnRegistrarCliente.TabIndex = 4
        btnRegistrarCliente.Text = "Registrar Cliente"
        btnRegistrarCliente.UseVisualStyleBackColor = True
        ' 
        ' txtEdad
        ' 
        txtEdad.Location = New Point(83, 56)
        txtEdad.Name = "txtEdad"
        txtEdad.Size = New Size(100, 23)
        txtEdad.TabIndex = 3
        ' 
        ' lblEdad
        ' 
        lblEdad.AutoSize = True
        lblEdad.Location = New Point(6, 59)
        lblEdad.Name = "lblEdad"
        lblEdad.Size = New Size(71, 15)
        lblEdad.TabIndex = 2
        lblEdad.Text = "Edad cliente"
        ' 
        ' lblEstadoCliente
        ' 
        lblEstadoCliente.AutoSize = True
        lblEstadoCliente.Location = New Point(32, 90)
        lblEstadoCliente.Name = "lblEstadoCliente"
        lblEstadoCliente.Size = New Size(0, 15)
        lblEstadoCliente.TabIndex = 1
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(61, 4)
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(94, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 15)
        Label1.TabIndex = 2
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(61, 4)
        ' 
        ' seccion2
        ' 
        seccion2.Controls.Add(listMovimientos)
        seccion2.Controls.Add(btnRetirar)
        seccion2.Controls.Add(txtMontoRetiro)
        seccion2.Controls.Add(btnAbrirCuenta)
        seccion2.Controls.Add(txtSaldoInicial)
        seccion2.Controls.Add(Label4)
        seccion2.Controls.Add(lblSaldoActual)
        seccion2.Controls.Add(Label2)
        seccion2.Location = New Point(275, 51)
        seccion2.Name = "seccion2"
        seccion2.Size = New Size(200, 339)
        seccion2.TabIndex = 4
        seccion2.TabStop = False
        seccion2.Text = "Info Cliente"
        ' 
        ' listMovimientos
        ' 
        listMovimientos.FormattingEnabled = True
        listMovimientos.ItemHeight = 15
        listMovimientos.Location = New Point(6, 221)
        listMovimientos.Name = "listMovimientos"
        listMovimientos.Size = New Size(188, 109)
        listMovimientos.TabIndex = 7
        ' 
        ' btnRetirar
        ' 
        btnRetirar.Location = New Point(37, 192)
        btnRetirar.Name = "btnRetirar"
        btnRetirar.Size = New Size(117, 23)
        btnRetirar.TabIndex = 6
        btnRetirar.Text = "Retirar"
        btnRetirar.UseVisualStyleBackColor = True
        ' 
        ' txtMontoRetiro
        ' 
        txtMontoRetiro.Location = New Point(94, 163)
        txtMontoRetiro.Name = "txtMontoRetiro"
        txtMontoRetiro.Size = New Size(100, 23)
        txtMontoRetiro.TabIndex = 5
        ' 
        ' btnAbrirCuenta
        ' 
        btnAbrirCuenta.Location = New Point(37, 73)
        btnAbrirCuenta.Name = "btnAbrirCuenta"
        btnAbrirCuenta.Size = New Size(117, 23)
        btnAbrirCuenta.TabIndex = 4
        btnAbrirCuenta.Text = "Abrir Cuenta"
        btnAbrirCuenta.UseVisualStyleBackColor = True
        ' 
        ' txtSaldoInicial
        ' 
        txtSaldoInicial.Location = New Point(82, 28)
        txtSaldoInicial.Name = "txtSaldoInicial"
        txtSaldoInicial.Size = New Size(100, 23)
        txtSaldoInicial.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(6, 166)
        Label4.Name = "Label4"
        Label4.Size = New Size(92, 15)
        Label4.TabIndex = 2
        Label4.Text = "Monto a Retirar:"
        ' 
        ' lblSaldoActual
        ' 
        lblSaldoActual.AutoSize = True
        lblSaldoActual.Location = New Point(48, 126)
        lblSaldoActual.Name = "lblSaldoActual"
        lblSaldoActual.Size = New Size(106, 15)
        lblSaldoActual.TabIndex = 1
        lblSaldoActual.Text = "Saldo Actual $ 0.00"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(6, 31)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 15)
        Label2.TabIndex = 0
        Label2.Text = "Saldo Inicial"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtMeses)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(btnCalcularPago)
        GroupBox1.Controls.Add(txtMontoPrestamo)
        GroupBox1.Controls.Add(lblPagoMensual)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Location = New Point(521, 60)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(267, 339)
        GroupBox1.TabIndex = 8
        GroupBox1.TabStop = False
        GroupBox1.Text = "Simulador de Prestamo"
        ' 
        ' txtMeses
        ' 
        txtMeses.Location = New Point(125, 69)
        txtMeses.Name = "txtMeses"
        txtMeses.Size = New Size(100, 23)
        txtMeses.TabIndex = 9
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(7, 72)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 15)
        Label7.TabIndex = 8
        Label7.Text = "Numero de meses"
        ' 
        ' btnCalcularPago
        ' 
        btnCalcularPago.Location = New Point(48, 97)
        btnCalcularPago.Name = "btnCalcularPago"
        btnCalcularPago.Size = New Size(117, 23)
        btnCalcularPago.TabIndex = 4
        btnCalcularPago.Text = "Calcular Pago Mensual"
        btnCalcularPago.UseVisualStyleBackColor = True
        ' 
        ' txtMontoPrestamo
        ' 
        txtMontoPrestamo.Location = New Point(125, 33)
        txtMontoPrestamo.Name = "txtMontoPrestamo"
        txtMontoPrestamo.Size = New Size(100, 23)
        txtMontoPrestamo.TabIndex = 3
        ' 
        ' lblPagoMensual
        ' 
        lblPagoMensual.AutoSize = True
        lblPagoMensual.Location = New Point(57, 154)
        lblPagoMensual.Name = "lblPagoMensual"
        lblPagoMensual.Size = New Size(0, 15)
        lblPagoMensual.TabIndex = 2
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(7, 36)
        Label6.Name = "Label6"
        Label6.Size = New Size(112, 15)
        Label6.TabIndex = 0
        Label6.Text = "Monto de Prestamo"
        ' 
        ' SisBancario
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(GroupBox1)
        Controls.Add(seccion2)
        Controls.Add(Label1)
        Controls.Add(section1)
        Name = "SisBancario"
        Text = "  "
        section1.ResumeLayout(False)
        section1.PerformLayout()
        seccion2.ResumeLayout(False)
        seccion2.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents section1 As GroupBox
    Friend WithEvents txtEdad As TextBox
    Friend WithEvents lblEdad As Label
    Friend WithEvents lblEstadoCliente As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents btnRegistrarCliente As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents seccion2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtMontoRetiro As TextBox
    Friend WithEvents btnAbrirCuenta As Button
    Friend WithEvents txtSaldoInicial As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblSaldoActual As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnCalcularPago As Button
    Friend WithEvents txtMontoPrestamo As TextBox
    Friend WithEvents lblPagoMensual As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents listMovimientos As ListBox
    Friend WithEvents btnRetirar As Button
    Friend WithEvents txtMeses As TextBox
    Friend WithEvents Label7 As Label

End Class
