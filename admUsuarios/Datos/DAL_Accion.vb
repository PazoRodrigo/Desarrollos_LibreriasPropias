Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Accion

#Region " Stored "
        Const storeTraerTodos As String = "Permisos.usp_Acciones_Traer_Todas"
        Const storeTraerTodosXFormulario As String = "Permisos.usp_Acciones_TraerTodosXFormulario"
        Const storeTraerTodosXAcceso As String = "Permisos.usp_Acciones_TraerTodosXAcceso"
#End Region
#Region " Métodos Públicos "
        '' ABM
        'Public Shared Sub Alta(ByVal entidad As Accion)
        '    Dim store As String = storeAlta
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
        '    pa.add("@FechaAlta", entidad.FechaAlta)
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
        '    '	pa.add("@VariableString", entidad.VariableString.ToString.ToUpper.Trim)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                entidad.IdEntidad = CInt(dt.Rows(0)(0))
        '            End If
        '        End If
        '    End Using
        'End Sub
        'Public Shared Sub Baja(ByVal entidad As Accion)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
        '    pa.add("@id", entidad.IdEntidad)
        '    pa.add("@FechaBaja", entidad.FechaBaja)
        '    pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
        '    Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        'End Sub
        'Public Shared Sub Modifica(ByVal entidad As Accion)
        '    Dim store As String = storeModifica
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
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
        '    '	pa.add("@VariableString", entidad.VariableString.ToString.ToUpper.Trim)
        '    Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        'End Sub
        ' Traer
        'Public Shared Function TraerUno(ByVal id As Integer) As Accion
        '    Dim store As String = storeTraerUnoXId
        '    Dim result As New Accion
        '    Dim pa As New parametrosArray
        '    pa.add("@id", id)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
        Public Shared Function TraerTodos() As List(Of Accion)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Accion)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
        Public Shared Function TraerTodosXFormulario(IdFormulario As Integer) As List(Of Accion)
            Dim store As String = storeTraerTodosXFormulario
            Dim pa As New parametrosArray
            pa.add("@IdFormulario", IdFormulario)
            Dim listaResult As New List(Of Accion)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
        Public Shared Function TraerTodosXAcceso(IdAcceso As Integer) As List(Of Accion)
            Dim store As String = storeTraerTodosXAcceso
            Dim pa As New parametrosArray
            pa.add("@IdAcceso", IdAcceso)
            Dim listaResult As New List(Of Accion)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Accion
            Dim entidad As New Accion
            ' Entidad
            If dr.Table.Columns.Contains("id_Acciones") Then
                If dr.Item("id_Acciones") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Acciones"))
                End If
            End If
            If dr.Table.Columns.Contains("Acciones") Then
                If dr.Item("Acciones") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Acciones").ToString.ToUpper.Trim
                End If
            End If
            'If dr.Table.Columns.Contains("Existe") Then
            '    If dr.Item("Existe") IsNot DBNull.Value Then
            '        entidad.Existe = CBool(dr.Item("Existe"))
            '    End If
            'End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Accion
End Namespace ' DataAccessLibrary