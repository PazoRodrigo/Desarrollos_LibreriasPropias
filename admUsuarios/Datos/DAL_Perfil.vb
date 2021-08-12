Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection
Imports Newtonsoft.Json

Namespace DataAccessLibrary
    Public Class DAL_Perfil

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Perfiles_Alta"
        Const storeBaja As String = "Permisos.usp_Perfiles_Baja"
        Const storeModifica As String = "Permisos.usp_Perfiles_Modif"
        Const storeAltaJson As String = "Permisos.usp_Roles_Perfil_Familia_Ramas_Formulario_Alta"
        Const storeModificaJson As String = "Permisos.usp_Roles_Perfil_Familia_Ramas_Formulario_Modifica"
        Const storeTraerUnoXId As String = "Permisos.usp_Perfiles_TraerUnoXId"
        Const storeTraerTodos As String = "Permisos.usp_Perfiles_Traer_Todos"
        Const storeTraerTodosXUsuario As String = "Permisos.usp_Perfiles_TraerTodosXUsuario"
        Const storeTraerTodosXRol As String = "Permisos.usp_Perfiles_Traer_TodosXidRol"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(entidad As DTO.DTO_Perfil)
            Dim store As String = storeAltaJson
            Dim EntidadAJson As String = JsonConvert.SerializeObject(entidad)
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@JSON", EntidadAJson)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(entidad As DTO.DTO_Perfil)
            Dim store As String = storeModificaJson
            Dim EntidadAJson As String = JsonConvert.SerializeObject(entidad)
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@JSON", EntidadAJson)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Perfil)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@Id_Perfiles", entidad.IdEntidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        'Public Shared Sub Modifica(ByVal entidad As Perfil)
        '    Dim store As String = storeModifica
        '    Dim pa As New parametrosArray
        '    pa.add("@Id_Perfiles", entidad.IdEntidad)
        '    pa.add("@Perfiles", entidad.Nombre)
        '    pa.add("@Id_Roles", entidad.IdRol)
        '    pa.add("@Id_Usuarios", entidad.IdUsuarioAlta)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                entidad.IdEntidad = CInt(dt.Rows(0)(0))
        '            End If
        '        End If
        '    End Using
        'End Sub
        ' ABM Json
        Public Shared Sub AltaJson(entidad As Perfil, jSon As String)
            Dim store As String = storeAltaJson
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@JSON", jSon)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub ModificaJson(entidad As Perfil, jSon As String)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@JSON", jSon)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Perfil
            Dim store As String = storeTraerUnoXId
            Dim result As New Perfil
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
        Public Shared Function TraerTodos() As List(Of Perfil)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Perfil)
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
        ' Otros
        Friend Shared Function TraerTodosXUsuario(idUsuario As Integer) As List(Of Perfil)
            Dim store As String = storeTraerTodosXUsuario
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", idUsuario)
            Dim listaResult As New List(Of Perfil)
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
        Friend Shared Function TraerTodosXRol(IdRol As Integer) As List(Of Perfil)
            Dim store As String = storeTraerTodosXRol
            Dim pa As New parametrosArray
            pa.add("@IdRol", IdRol)
            Dim listaResult As New List(Of Perfil)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Perfil
            Dim entidad As New Perfil
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("Fec_Proc_Alta") Then
                If dr.Item("Fec_Proc_Alta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("Fec_Proc_Alta"))
                End If
            End If
            If dr.Table.Columns.Contains("Fec_Proc_Baja") Then
                If dr.Item("Fec_Proc_Baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("Fec_Proc_Baja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id_Perfiles") Then
                If dr.Item("id_Perfiles") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Perfiles"))
                End If
            End If
            If dr.Table.Columns.Contains("Perfiles") Then
                If dr.Item("Perfiles") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Perfiles").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Id_Roles") Then
                If dr.Item("Id_Roles") IsNot DBNull.Value Then
                    entidad.IdRol = CInt(dr.Item("Id_Roles"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Perfil
End Namespace ' DataAccessLibrary