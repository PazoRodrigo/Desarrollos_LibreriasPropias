Option Explicit On
Option Strict On

Imports Usuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Usuario

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Alta_Usuarios"
        Const storeBaja As String = "p_Usuario_Baja"
        Const storeModifica As String = "Permisos.usp_Modif_Usuarios"
        Const storeTraerUnoXId As String = "p_Usuario_TraerUnoXId"
        Const storeTraerTodos As String = "Permisos.usp_TraerTodos_Usuarios" '"Permisos.usp_TraerDatosUsuario_Todos" 
        Const storeTraerTodosXArea As String = "Permisos.usp_Traer_UsuariosxFamilia"
        Const storeTraerUnoXUserLoginyPassword As String = "Permisos.usp_Traer_UsuariosxPass_UserLogin"
#End Region
#Region " Métodos Públicos "
        Public Shared Function TraerUno(ByVal UserLogin As String, Password As String) As Usuario
            Dim store As String = storeTraerUnoXUserLoginyPassword
            Dim result As New Usuario
            Dim pa As New parametrosArray
            pa.add("@User_login", UserLogin)
            pa.add("@Password", Password)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
                If Not dt Is Nothing Then
                    'If dt.Rows.Count = 1 Then
                    '    result = LlenarEntidad(dt.Rows(0))
                    'ElseIf dt.Rows.Count > 1 Then
                    result = LlenarEntidadMasDeUnRol(dt)
                    'ElseIf dt.Rows.Count = 0 Then
                    '    result = Nothing
                    'End If
                End If
            End Using
            Return result
        End Function
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Usuario)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@id_User_Alta", entidad.IdUsuarioAlta)
            pa.add("@Fecha_Alta", entidad.FechaAlta)
            pa.add("@Fec_Proc_Alta", entidad.FechaAlta)
            pa.add("@Fecha_Baja", entidad.FechaBaja)
            pa.add("@User_name", entidad.Nombre)
            pa.add("@User_login", entidad.UserLogin)
            pa.add("@Password", entidad.Password)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        'Public Shared Sub Baja(ByVal entidad As Usuario)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
        '    pa.add("@id", entidad.IdEntidad)
        '    pa.add("@FechaBaja", entidad.FechaBaja)
        '    pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
        '    Connection.Connection.Ejecutar(store, pa, "StrUtedyc2.0")
        'End Sub
        Public Shared Sub Modifica(ByVal entidad As Usuario)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@id_User_Modif", entidad.IdUsuarioModifica)
            pa.add("@id_User", entidad.IdEntidad)
            pa.add("@Fecha_Alta", entidad.FechaAlta)
            'pa.add("@Fec_Proc_Alta", entidad.FechaAlta)
            pa.add("@Fecha_Baja", entidad.FechaBaja)
            pa.add("@User_name", entidad.Nombre)
            pa.add("@User_login", entidad.UserLogin)
            pa.add("@Password", entidad.Password)
            Connection.Connection.Ejecutar(store, pa, "StrUtedyc2.0")
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Usuario)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Usuario)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
        Public Shared Function TraerTodosXArea(ByVal IdArea As Integer) As List(Of Usuario)
            Dim store As String = storeTraerTodosXArea
            Dim pa As New parametrosArray
            'Permisos.usp_Traer_UsuariosxFamilia
            'Id_Familias
            pa.add("@Id_familia", IdArea)
            Dim listaResult As New List(Of Usuario)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
        Private Shared Function LlenarEntidadMasDeUnRol(ByVal dt As DataTable) As Usuario
            Dim entidad As New Usuario
            Try

                ' DBE
                If dt.Rows(0).Table.Columns.Contains("idUsuarioAlta") Then
                    If dt.Rows(0).Item("idUsuarioAlta") IsNot DBNull.Value Then
                        entidad.IdUsuarioAlta = CInt(dt.Rows(0).Item("idUsuarioAlta"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("idUsuarioBaja") Then
                    If dt.Rows(0).Item("idUsuarioBaja") IsNot DBNull.Value Then
                        entidad.IdUsuarioBaja = CInt(dt.Rows(0).Item("idUsuarioBaja"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("idUsuarioModifica") Then
                    If dt.Rows(0).Item("idUsuarioModifica") IsNot DBNull.Value Then
                        entidad.IdUsuarioModifica = CInt(dt.Rows(0).Item("idUsuarioModifica"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("IdMotivoBaja") Then
                    If dt.Rows(0).Item("IdMotivoBaja") IsNot DBNull.Value Then
                        entidad.IdMotivoBaja = CInt(dt.Rows(0).Item("IdMotivoBaja"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("fecha_Alta") Then
                    If dt.Rows(0).Item("fecha_Alta") IsNot DBNull.Value Then
                        entidad.FechaAlta = CDate(dt.Rows(0).Item("fecha_Alta"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("fecha_Baja") Then
                    If dt.Rows(0).Item("fecha_Baja") IsNot DBNull.Value Then
                        entidad.FechaBaja = CDate(dt.Rows(0).Item("fecha_Baja"))
                    End If
                End If
                ' Entidad
                If dt.Rows(0).Table.Columns.Contains("id_user") Then
                    If dt.Rows(0).Item("id_user") IsNot DBNull.Value Then
                        entidad.IdEntidad = CInt(dt.Rows(0).Item("id_user"))
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("user_name") Then
                    If dt.Rows(0).Item("user_name") IsNot DBNull.Value Then
                        entidad.Nombre = dt.Rows(0).Item("user_name").ToString.ToUpper.Trim
                    End If
                End If
                If dt.Rows(0).Table.Columns.Contains("user_login") Then
                    If dt.Rows(0).Item("user_login") IsNot DBNull.Value Then
                        entidad.UserLogin = dt.Rows(0).Item("user_login").ToString.Trim
                    End If
                End If
                Dim lroles As New List(Of Rol)
                For Each dr As DataRow In dt.Rows
                    Dim r As New Rol With {
                        .Descripcion = dr("roles").ToString,
                        .IdEntidad = CInt(dr("id_rol").ToString)
                    }
                    If Not lroles.Contains(r) Then
                        lroles.Add(r)
                    End If
                Next
                entidad.Roles = lroles
                Dim lareas As New List(Of Area)
                For Each dr As DataRow In dt.Rows
                    Dim a As New Area With {
                        .Descripcion = dr("familias").ToString,
                        .IdEntidad = CInt(dr("id_familia").ToString)
                    }
                    If Not lareas.Contains(a) Then
                        lareas.Add(a)
                    End If
                Next
                entidad.Areas = lareas
            Catch ex As Exception
                entidad = Nothing
            End Try
            Return entidad
        End Function
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Usuario
            Dim entidad As New Usuario
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
            If dr.Table.Columns.Contains("fecha_Alta") Then
                If dr.Item("fecha_Alta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fecha_Alta"))
                End If
            End If
            If dr.Table.Columns.Contains("fecha_Baja") Then
                If dr.Item("fecha_Baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fecha_Baja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id_user") Then
                If dr.Item("id_user") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_user"))
                End If
            End If
            If dr.Table.Columns.Contains("user_name") Then
                If dr.Item("user_name") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("user_name").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("user_login") Then
                If dr.Item("user_login") IsNot DBNull.Value Then
                    entidad.UserLogin = dr.Item("user_login").ToString.Trim
                End If
            End If
            'Dim lroles As New List(Of Rol)
            'Dim r As New Rol With {
            '        .Descripcion = dr("roles").ToString,
            '        .IdRol = CInt(dr("id_rol").ToString)
            '    }
            'lroles.Add(r)
            'entidad.Roles = lroles
            Return entidad
        End Function
#End Region
    End Class ' DAL_Usuario
End Namespace ' DataAccessLibrary