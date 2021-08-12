Option Explicit On
Option Strict On

Imports Usuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Rol

#Region " Stored "
        Const storeAlta As String = "p_Rol_Alta"
        Const storeBaja As String = "p_Rol_Baja"
        Const storeModifica As String = "p_Rol_Modifica"
        Const storeTraerUnoXId As String = "p_Rol_TraerUnoXId"
        Const storeTraerTodos As String = "p_Rol_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Rol)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@FechaAlta", entidad.FechaAlta)
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
            '	pa.add("@VariableString", entidad.VariableString.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasEnlatadoSindical")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Rol)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@FechaBaja", entidad.FechaBaja)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Connection.Connection.Ejecutar(store, pa, "StrLibreriasEnlatadoSindical")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Rol)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
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
            '	pa.add("@VariableString", entidad.VariableString.ToString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "StrLibreriasEnlatadoSindical")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Rol
            Dim store As String = storeTraerUnoXId
            Dim result As New Rol
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasEnlatadoSindical")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    ElseIf dt.Rows.Count = 0 Then
                        result = Nothing
                    End If
                End If
            End Using
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Rol)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Rol)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasEnlatadoSindical")
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Rol
            Dim entidad As New Rol
            ' DBE
            If dr.Table.Columns.Contains("Id_User_Alta") Then
                If dr.Item("Id_User_Alta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("Id_User_Alta"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_User_Baja") Then
                If dr.Item("Id_User_Baja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("Id_User_Baja"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_User_Modif") Then
                If dr.Item("Id_User_Modif") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("Id_User_Modif"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_MotivoBaja") Then
                If dr.Item("Id_MotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("Id_MotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("Fecha_Alta") Then
                If dr.Item("Fecha_Alta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("Fecha_Alta"))
                End If
            End If
            If dr.Table.Columns.Contains("Fecha_Baja") Then
                If dr.Item("Fecha_Baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("Fecha_Baja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            ' VariableString
            '	If dr.Table.Columns.Contains("VariableString") Then
            '   	If dr.Item("VariableString") IsNot DBNull.Value Then
            '   		entidad.VariableString = dr.Item("VariableString").ToString.ToUpper.Trim
            '    	End If
            '	End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Rol
End Namespace ' DataAccessLibrary