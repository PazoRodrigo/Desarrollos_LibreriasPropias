Option Explicit On
Option Strict On

Imports ClasesEmpresa.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Empresa

#Region " Stored "
        Const storeAlta As String = "p_Empresa_Alta"
        Const storeBaja As String = "p_Empresa_Baja"
        Const storeModifica As String = "p_Empresa_Modifica"
        Const storeTraerUnoXId As String = "p_EmpresaAfib002_TraerUnoXId"
        Const storeTraerTodosXCodEmpresa As String = "p_EmpresaAfib002_TraerTodosXCodEmpresa"
        Const storeTraerTodosXDenominacion As String = "p_EmpresaAfib002_TraerTodosXDenominacion"
        Const storeTraerTodosXCUIT As String = "p_EmpresaAfib002_TraerTodosXCuit"
        Const storeTraerTodosXEmpleado As String = "p_EmpresaAfib002_TraerTodosXEmpleado"
        Const storeTraerTodosXSeccional As String = "p_EmpresaAfib002_TraerTodosXSeccional"
        Const storeTraerTodosXSeccionalConNomina As String = "p_EmpresaAfib002_TraerTodosXSeccional_Afiliados"
        'Const storeTraerUnoXId As String = "sp_traerAfib002AModificar"
        'Const storeTraerTodosActivos As String = "p_Empresa_TraerTodosActivos"
        'Const storeTraerTodosXCodEmpresa As String = "sp_traercod_empre"
        'Const storeTraerTodosXDenominacion As String = "sp_traerAfib002PorDenominacion"
        'Const storeTraerTodosXCUIT As String = "sp_traerAfib002AModificarcuit"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Entidad.Empresa)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
            ' Variable Numérica
            '	If entidad.codPostal <> 0 Then
            '		pa.add("@VariableNumero", entidad.VariableNumero)
            '	Else
            '		pa.add("@codPostal", "borrarEntero")
            '	End If
            ' VariableFecha
            '	If entidad.fechaNacimiento.HasValue Then
            '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
            '	Else
            '		pa.add("@fechaNacimiento", "borrarFecha")
            '	End If
            ' VariableString
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Entidad.Empresa)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "StrLibreriasPropiasUTE")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Entidad.Empresa)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            ' Variable Numérica
            '	If entidad.codPostal <> 0 Then
            '		pa.add("@VariableNumero", entidad.VariableNumero)
            '	Else
            '		pa.add("@codPostal", "borrarEntero")
            '	End If
            ' VariableFecha
            '	If entidad.fechaNacimiento.HasValue Then
            '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
            '	Else
            '		pa.add("@fechaNacimiento", "borrarFecha")
            '	End If
            ' VariableString
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "StrLibreriasPropiasUTE")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Long) As Entidad.Empresa
            Dim store As String = storeTraerUnoXId
            Dim result As New Entidad.Empresa
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    ElseIf dt.Rows.Count = 0 Then
                        result = Nothing
                    End If
                End If
            End Using
            'Dim lpa As New List(Of ParametrosObjetos)
            'Dim pao As ParametrosObjetos
            'pao = New ParametrosObjetos
            'Dim em As New Empresa
            'pao.Campo = "id"
            'pao.Propiedad = "IdEntidad"
            'lpa.Add(pao)
            'pao = New ParametrosObjetos
            'pao.Campo = "denomina"
            'pao.Propiedad = "Denominacion"
            'lpa.Add(pao)
            'Dim l As List(Of Empresa) = Connection.Connection.TraerListaDeObjetosGenerics(Of Empresa)(lpa, em, pa, store, "StrLibreriasPropiasUTE")
            'result = l(0)
            Return result
        End Function
        Public Shared Function TraerTodosXCodEmpresa(CodEmpresa As Long) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXCodEmpresa
            Dim pa As New parametrosArray
            pa.add("@CodEmpresa", Right("00000000000" & CodEmpresa.ToString, 11))
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXDenominacion(Denominacion As String) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXDenominacion
            Dim pa As New parametrosArray
            pa.add("@Denominacion", Denominacion)
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXCuit(CUIT As Long) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXCUIT
            Dim pa As New parametrosArray
            pa.add("@CUIT", CUIT)
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXEmpleado(idAfiliado As Integer) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXEmpleado
            Dim pa As New parametrosArray
            pa.add("@idAfiliado", idAfiliado)
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXSeccionalConNominaActiva(IdSeccional As Integer) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXSeccionalConNomina
            Dim pa As New parametrosArray
            pa.add("@IdSeccional", Right("00" & CStr(IdSeccional), 2))
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXSeccional(IdSeccional As Integer) As List(Of Entidad.Empresa)
            Dim store As String = storeTraerTodosXSeccional
            Dim pa As New parametrosArray
            pa.add("@IdSeccional", Right("00" & CStr(IdSeccional), 2))
            Dim listaResult As New List(Of Entidad.Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Entidad.Empresa
            Dim entidad As New Entidad.Empresa
            Try
                ' DBE
                If dr.Table.Columns.Contains("idUsuarioAlta") Then
                    If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                        entidad.idUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                    End If
                End If
                If dr.Table.Columns.Contains("idUsuarioBaja") Then
                    If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                        entidad.idUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                    End If
                End If
                If dr.Table.Columns.Contains("idUsuarioModifica") Then
                    If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                        entidad.idUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                    End If
                End If
                If dr.Table.Columns.Contains("Id_MotivoBaja") Then
                    If dr.Item("Id_MotivoBaja") IsNot DBNull.Value Then
                        entidad.IdMotivoBaja = CInt(dr.Item("Id_MotivoBaja"))
                    End If
                End If
                If dr.Table.Columns.Contains("fec_alta") Then
                    If dr.Item("fec_alta") IsNot DBNull.Value Then
                        entidad.fechaAlta = CDate(dr.Item("fec_alta"))
                    End If
                End If
                If dr.Table.Columns.Contains("fec_baja") Then
                    If dr.Item("fec_baja") IsNot DBNull.Value Then
                        entidad.fechaBaja = CDate(dr.Item("fec_baja"))
                    End If
                End If
                ' Entidad
                If dr.Table.Columns.Contains("id") Then
                    If dr.Item("id") IsNot DBNull.Value Then
                        entidad.IdEntidad = CInt(dr.Item("id"))
                    End If
                End If
                If dr.Table.Columns.Contains("cod_empre") Then
                    If dr.Item("cod_empre") IsNot DBNull.Value Then
                        entidad.CodEmpresa = CInt(dr.Item("cod_empre"))
                    End If
                ElseIf dr.Table.Columns.Contains("Codigo de Empresa") Then
                    If dr.Item("Codigo de Empresa") IsNot DBNull.Value Then
                        entidad.CodEmpresa = CInt(dr.Item("Codigo de Empresa"))
                    End If
                End If
                If dr.Table.Columns.Contains("denomina") Then
                    If dr.Item("denomina") IsNot DBNull.Value Then
                        entidad.Denominacion = dr.Item("denomina").ToString.Trim
                    End If
                ElseIf dr.Table.Columns.Contains("Denominacion") Then
                    If dr.Item("Denominacion") IsNot DBNull.Value Then
                        entidad.Denominacion = dr.Item("Denominacion").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("denominac") Then
                    If dr.Item("denominac") IsNot DBNull.Value Then
                        entidad.DenominacionCarnet = dr.Item("denominac").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("dep") Then
                    If dr.Item("dep") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("dep")) Then
                        entidad.Dependencia = CInt(dr.Item("dep"))
                    End If
                End If
                If dr.Table.Columns.Contains("cuit") Then
                    If dr.Item("cuit") IsNot DBNull.Value Then
                        Dim result As String = dr.Item("cuit").ToString.Trim.Replace("-", "")
                        If IsNumeric(result) Then
                            entidad.CUIT = CLng(result)
                        End If
                    End If
                End If
                If dr.Table.Columns.Contains("sec") Then
                    If dr.Item("sec") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("sec")) Then
                        entidad.IdSeccional = CInt(dr.Item("sec"))
                    End If
                End If
                If dr.Table.Columns.Contains("direccion") Then
                    If dr.Item("direccion") IsNot DBNull.Value Then
                        entidad.Direccion = dr.Item("direccion").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("prov") Then
                    If dr.Item("prov") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("prov")) Then
                        entidad.IdProvincia = CInt(dr.Item("prov"))
                    End If
                End If
                '    a.prov,
                'c.web,
                'a.motbaja,
                'If dr.Table.Columns.Contains("tipo1") Then
                '    If dr.Item("tipo1") IsNot DBNull.Value Then
                '        entidad.IdConvenio = CInt(dr.Item("tipo1"))
                '    End If
                'End If
                If dr.Table.Columns.Contains("tipoe") Then
                    If dr.Item("tipoe") IsNot DBNull.Value Then
                        entidad.TipoEntidad = dr.Item("tipoe").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("ddn") Then
                    If dr.Item("ddn") IsNot DBNull.Value Then
                        If dr.Item("ddn").ToString.Trim <> "" Then
                            entidad.Contacto_DDN = CInt(LUM.LUM.SoloNumeros(dr.Item("ddn").ToString))
                        End If
                    End If
                End If
                If dr.Table.Columns.Contains("te") Then
                    If dr.Item("te") IsNot DBNull.Value Then
                        If dr.Item("te").ToString.Trim <> "" Then
                            entidad.Contacto_Telefono = dr.Item("te").ToString.Trim
                        End If
                    End If
                End If
                If dr.Table.Columns.Contains("loc") Then
                    If dr.Item("loc") IsNot DBNull.Value Then
                        entidad.Localidad = dr.Item("loc").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("cod_postal") Then
                    If dr.Item("cod_postal") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("cod_postal")) Then
                        If dr.Item("cod_postal").ToString <> "" Then
                            entidad.CodPostal = CInt(LUM.LUM.SoloNumeros(dr.Item("cod_postal").ToString))
                        End If
                    End If
                End If
                If dr.Table.Columns.Contains("email") Then
                    If dr.Item("email") IsNot DBNull.Value Then
                        entidad.Email = dr.Item("email").ToString.Trim
                    End If
                End If

                If dr.Table.Columns.Contains("Procedencia") Then
                    If dr.Item("Procedencia") IsNot DBNull.Value Then
                        entidad.Procedencia = dr.Item("Procedencia").ToString.Trim
                    End If
                End If
                If dr.Table.Columns.Contains("DireccionAlternativa") Then
                    If dr.Item("DireccionAlternativa") IsNot DBNull.Value Then
                        entidad.DireccionAlternativa = dr.Item("DireccionAlternativa").ToString.Trim
                    End If
                End If

            Catch ex As Exception
                Dim e As String = ex.Message()
                Dim s As String = CStr(entidad.IdEntidad)
            End Try
            Return entidad
        End Function
#End Region
    End Class ' DAL_Empresa
End Namespace ' DataAccessLibrary