Imports Comunes.Entidad
Imports ClasesEmpresa.Entidad

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Repr As New Representado(400094)
        Grilla.DataSource = Repr.ListaEmpresa()
        'Empleado.Refresh()
        'Grilla.DataSource = Empleado.Todos
        Grilla.DataBind()
        Dim a As Empleado = Empleado.TraerUnoXLegajo(103871)
        '    'Dim lista As List(Of SubTipoEmpresa) = SubTipoEmpresa.TraerTodos()
        '    'Dim lista2 As List(Of SubTipoEmpresa) = obj.ListaSubTipoEmpresas
        '    'Dim a As List(Of Convenio) = Convenio.TraerTodos()
        '    'Dim a As Long = LUM.LUM.ObtenerNumeroAleatorio(100)
        Label1.Text = a.ToString
        '    Dim a As List(Of ClasesEmpresa.Entidad.Empresa) = ClasesEmpresa.Entidad.Empresa.TraerTodosXSeccional(10)

        '    Dim b As List(Of Comunes.Entidad.Empleado) = Comunes.Entidad.Empleado.TraerTodos()
        '    'Dim l As ClasesEmpresa.Entidad.Empresa = Empresa.TraerUno(11318)
        '    'Dim l As List(Of ClasesEmpresa.Entidad.Representado) = ClasesEmpresa.Entidad.Representado.TraerTodosXApellidoNombre("SAUCEDO MARIA BENITA          ")
        '    'Label1.Text = l.Count.ToString
        '    'Dim l As List(Of ActividadAFIP) = ActividadAFIP.TraerTodosXNombre("bil")
        '    'Dim empre1 As List(Of Empresa) = Empresa.TraerTodosXCodEmpresa(11)
        '    'Dim obj As Empresa = empre1(0)
        '    'Dim empre2 As List(Of Empresa) = Empresa.TraerTodosXDenominacion("Denom")
        '    'Dim lista As List(Of Convenio) = Convenio.TraerTodosXEmpresa(11318)
        '    'Dim s As New StringBuilder
        '    's.Append("Ospe" + empre.CantidadEmpleadosOSPEDyC.ToString & vbCrLf)
        '    's.Append("Ute" + empre.CantidadTotalUTEDyC.ToString & vbCrLf)
        '    's.Append("Sind" + empre.CantidadAfiliadosUTEDyC.ToString & vbCrLf)
        '    's.Append("Aport" + empre.CantidadAportantesUTEDyC.ToString & vbCrLf)
        '    's.Append("Ramas" + empre.CantidadRamasXReunion.ToString & vbCrLf)
        '    's.Append("Prov " & empre.Objprovincia.Nombre)
        '    'Label1.Text = s.ToString
    End Sub
End Class
