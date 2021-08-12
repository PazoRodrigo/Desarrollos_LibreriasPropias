Option Explicit On
Option Strict On

Imports Comunes.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Empleado

#Region " Stored "
        'Const storeAlta As String = "p_Empleado_Alta"
        'Const storeBaja As String = "p_Empleado_Baja"
        'Const storeModifica As String = "p_Empleado_Modifica"
        'Const storeTraerUnoXId As String = "p_Empleado_TraerUnoXId"
        Const storeTraerTodos As String = "p_Empleado_TraerTodos"
        'Const storeTraerTodosActivos As String = "p_Empleado_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        'Public Shared Sub Alta(ByVal entidad As Empleado)
        '    Dim store As String = storeAlta
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
        '    ' Variable Numérica
        '    '	If entidad.codPostal <> 0 Then
        '    '		pa.add("@VariableNumero", entidad.VariableNumero)
        '    '	Else
        '    '		pa.add("@codPostal", "borrarEntero")
        '    '	End If
        '    ' VariableFecha
        '    '	If entidad.fechaNacimiento.HasValue Then
        '    '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
        '    '	Else
        '    '		pa.add("@fechaNacimiento", "borrarFecha")
        '    '	End If
        '    ' VariableString
        '    '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                entidad.IdEntidad = CInt(dt.Rows(0)(0))
        '            End If
        '        End If
        '    End Using
        'End Sub
        'Public Shared Sub Baja(ByVal entidad As Empleado)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
        '    pa.add("@id", entidad.IdEntidad)
        '    Connection.Connection.Ejecutar(store, pa)
        'End Sub
        'Public Shared Sub Modifica(ByVal entidad As Empleado)
        '    Dim store As String = storeModifica
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
        '    pa.add("@id", entidad.IdEntidad)
        '    ' Variable Numérica
        '    '	If entidad.codPostal <> 0 Then
        '    '		pa.add("@VariableNumero", entidad.VariableNumero)
        '    '	Else
        '    '		pa.add("@codPostal", "borrarEntero")
        '    '	End If
        '    ' VariableFecha
        '    '	If entidad.fechaNacimiento.HasValue Then
        '    '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
        '    '	Else
        '    '		pa.add("@fechaNacimiento", "borrarFecha")
        '    '	End If
        '    ' VariableString
        '    '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
        '    Connection.Connection.Ejecutar(store, pa)
        'End Sub
        ' Traer
        'Public Shared Function TraerUno(ByVal id As Integer) As Empleado
        '    Dim store As String = storeTraerUnoXId
        '    Dim result As New Empleado
        '    Dim pa As New parametrosArray
        '    pa.add("@id", id)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                result = LlenarEntidad(dt.Rows(0))
        '            ElseIf dt.Rows.Count = 0 Then
        '                result = Nothing
        '            End If
        '        End If
        '    End Using
        '    Return result
        'End Function
        Public Shared Function TraerTodos() As List(Of Empleado)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Empleado)
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
        ' Búsqueda
        'Public Shared Function TraerTodosActivos() As List(Of Empleado)
        '    Dim store As String = storeTraerTodosActivos
        '    Dim pa As New parametrosArray
        '    Dim listaResult As New List(Of Empleado)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If dt.Rows.Count > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                listaResult.Add(LlenarEntidad(dr))
        '            Next
        '        Else
        '            listaResult = Nothing
        '        End If
        '    End Using
        '    Return listaResult
        'End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Empleado
            Dim entidad As New Empleado
            ' Persona
            If dr.Table.Columns.Contains("Nombre completo") Then
                If dr.Item("Nombre completo") IsNot DBNull.Value Then
                    entidad.ApellidoNombre = dr.Item("Nombre completo").ToString.Trim
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("Legajo") Then
                If dr.Item("Legajo") IsNot DBNull.Value Then
                    entidad.Legajo = CInt(dr.Item("Legajo"))
                End If
            End If
            If dr.Table.Columns.Contains("nro_doc") Then
                If dr.Item("nro_doc") IsNot DBNull.Value Then
                    entidad.NroDocumento = CInt(dr.Item("nro_doc"))
                End If
            End If
            If dr.Table.Columns.Contains("Lugar Pago") Then
                If dr.Item("Lugar Pago") IsNot DBNull.Value Then
                    entidad.LugarPago = dr.Item("Lugar Pago").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Fecha Ingreso") Then
                If dr.Item("Fecha Ingreso") IsNot DBNull.Value Then
                    entidad.FechaIngreso = CDate(dr.Item("Fecha Ingreso"))
                End If
            End If
            If dr.Table.Columns.Contains("Empresa") Then
                If dr.Item("Empresa") IsNot DBNull.Value Then
                    entidad.Empresa = dr.Item("Empresa").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("TipoDocumento") Then
                If dr.Item("TipoDocumento") IsNot DBNull.Value Then
                    entidad.TipoDocumento = dr.Item("TipoDocumento").ToString.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Empleado
End Namespace ' DataAccessLibrary