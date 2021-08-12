Option Explicit On
Option Strict On

Imports Comunes.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Seccional

#Region " Stored "
        'Const storeAlta As String = "p_Seccional_Alta"
        'Const storeBaja As String = "p_Seccional_Baja"
        'Const storeModifica As String = "p_Seccional_Modifica"
        'Const storeTraerUnoXId As String = "p_Seccional_TraerUnoXId"
        Const storeTraerTodos As String = "p_Seccional_TraerTodos"
        'Const storeTraerTodosActivos As String = "p_Seccional_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        '' ABM
        'Public Shared Sub Alta(ByVal entidad As Seccional)
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
        'Public Shared Sub Baja(ByVal entidad As Seccional)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
        '    pa.add("@id", entidad.IdEntidad)
        '    Connection.Connection.Ejecutar(store, pa)
        'End Sub
        'Public Shared Sub Modifica(ByVal entidad As Seccional)
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
        '' Traer
        'Public Shared Function TraerUno(ByVal id As Integer) As Seccional
        '    Dim store As String = storeTraerUnoXId
        '    Dim result As New Seccional
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
        Public Shared Function TraerTodos() As List(Of Seccional)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Seccional)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Seccional
            Dim entidad As New Seccional
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
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.fechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.fechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("cod_prov") Then
                If dr.Item("cod_prov") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("cod_prov"))
                End If
            End If
            If dr.Table.Columns.Contains("desc_prov") Then
                If dr.Item("desc_prov") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("desc_prov").ToString.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Seccional
End Namespace ' DataAccessLibrary