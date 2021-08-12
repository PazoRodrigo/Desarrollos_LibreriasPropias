Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Rol

#Region " Stored "
        Const storeAlta As String = "permisos.usp_Roles_Alta"
        Const storeBaja As String = "permisos.usp_Roles_Baja"
        Const storeModifica As String = "permisos.usp_Roles_Modif"
        Const storeTraerTodos As String = "permisos.usp_Roles_Traer_Todas"
        Const storeTraerTodosXUsuario As String = "permisos.usp_Roles_Traer_TodosXUsuario"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Rol)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@id_Usuarios", entidad.IdUsuarioAlta)
            pa.add("@Rol", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
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
            pa.add("@id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@Id_Roles", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Rol)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@id_Usuarios", entidad.IdUsuarioModifica)
            pa.add("@Id_Roles", entidad.IdEntidad)
            pa.add("@Roles", entidad.Nombre.ToString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Rol)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Rol)
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
        Friend Shared Function TraerTodosXUsuario(IdUsuario As Integer) As List(Of Rol)
            Dim store As String = storeTraerTodosXUsuario
            Dim pa As New parametrosArray
            pa.add("@id_Usuarios", IdUsuario)
            Dim listaResult As New List(Of Rol)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Rol
            Dim entidad As New Rol
            ' DBE
            If dr.Table.Columns.Contains("id_User_Alta") Then
                If dr.Item("id_User_Alta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("id_User_Alta"))
                End If
            End If
            If dr.Table.Columns.Contains("id_User_Baja") Then
                If dr.Item("id_User_Baja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("id_User_Baja"))
                End If
            End If
            If dr.Table.Columns.Contains("id_User_Modif") Then
                If dr.Item("id_User_Modif") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("id_User_Modif"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_MotivoBaja") Then
                If dr.Item("Id_MotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("Id_MotivoBaja"))
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
            If dr.Table.Columns.Contains("id_Roles") Then
                If dr.Item("id_Roles") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Roles"))
                End If
            End If
            If dr.Table.Columns.Contains("Roles") Then
                If dr.Item("Roles") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Roles").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Rol
End Namespace ' DataAccessLibrary