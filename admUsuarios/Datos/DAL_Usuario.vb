Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Usuario

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Usuarios_Areas_Seccional_Alta"
        Const storeBaja As String = "p_Usuario_Baja"
        Const storeModifica As String = "p_Usuario_Modifica"
        Const storeTraerUno As String = "[Permisos].[usp_Traer_UsuariosxPass_UserLogin]"
        Const storeTraerUnoXId As String = "p_Usuario_TraerUnoXId"
        Const storeTraerTodos As String = "Permisos.usp_Usuarios_TraerTodos"
        Const storeTraerTodosXArea As String = "Permisos.usp_Usuarios_TraerTodosXArea"
        Const storeTraerTodosXPerfil As String = "Permisos.usp_Usuarios_Traer_TodosxId_perfiles"
        Const storeTraerTodosXRol As String = "Permisos.usp_Usuarios_Traer_TodosxId_Roles"
        Const storeAgregarEliminarPerfil As String = "Permisos.usp_Usuario_AgregarEliminarPerfil"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Usuario)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Nombre", entidad.Nombre)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub AltaJson(entidad As Usuario, jSon As String)
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
        Public Shared Sub Baja(ByVal entidad As Usuario)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@FechaBaja", entidad.FechaBaja)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Usuario)
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
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Usuario
            Dim store As String = storeTraerUnoXId
            Dim result As New Usuario
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
        Public Shared Function TraerUno(ByVal Nombre As String, ByVal Password As String) As Usuario
            Dim store As String = storeTraerUno
            Dim result As New Usuario
            Dim pa As New parametrosArray
            pa.add("@user_login", Nombre)
            pa.add("@Password", Password)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    End If
                End If
            End Using
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Usuario)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Usuario)
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
        Public Shared Function TraerTodosXArea(IdArea As Integer) As List(Of Usuario)
            Dim store As String = storeTraerTodosXArea
            Dim pa As New parametrosArray
            pa.add("@Id_Area", IdArea)
            Dim listaResult As New List(Of Usuario)
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
        Friend Shared Function TraerTodosXPerfil(IdPerfil As Integer) As List(Of Usuario)
            Dim store As String = storeTraerTodosXPerfil
            Dim pa As New parametrosArray
            pa.add("@Id_Perfiles", IdPerfil)
            Dim listaResult As New List(Of Usuario)
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
        Friend Shared Function TraerTodosXRol(IdRol As Integer) As List(Of Usuario)
            Dim store As String = storeTraerTodosXRol
            Dim pa As New parametrosArray
            pa.add("@Id_Roles", IdRol)
            Dim listaResult As New List(Of Usuario)
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
        Friend Shared Sub AgregarEliminarPerfil(entidad As Usuario, IdPerfil As Integer, Incluye As Boolean)
            Dim store As String = storeAgregarEliminarPerfil
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@IdUser", entidad.IdEntidad)
            pa.add("@IdPerfil", IdPerfil)
            pa.add("@Incluye", Incluye)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Usuario
            Dim entidad As New Usuario
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
            If dr.Table.Columns.Contains("Id_Usuarios") Then
                If dr.Item("Id_Usuarios") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Usuarios"))
                End If
            End If
            If dr.Table.Columns.Contains("User_name") Then
                If dr.Item("User_name") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("User_name").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("User_login") Then
                If dr.Item("User_login") IsNot DBNull.Value Then
                    entidad.Login = dr.Item("User_login").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("cuil") Then
                If dr.Item("cuil") IsNot DBNull.Value Then
                    entidad.Documento_CUIT = CLng(dr.Item("cuil").ToString)
                End If
            End If
            If dr.Table.Columns.Contains("email") Then
                If dr.Item("email") IsNot DBNull.Value Then
                    entidad.CorreoElectronico = dr.Item("email").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Telefono") Then
                If dr.Item("Telefono") IsNot DBNull.Value Then
                    entidad.Telefono = dr.Item("Telefono").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Usuario
End Namespace ' DataAccessLibrary