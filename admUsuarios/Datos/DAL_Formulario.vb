Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Formulario

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Formularios_Alta"
        Const storeBaja As String = "Permisos.usp_Formularios_Baja"
        Const storeModifica As String = "Permisos.usp_Formularios_Modif"
        Const storeTraerUnoXId As String = "Permisos.usp_Formulario_TraerUnoXId"
        Const storeTraerTodos As String = "Permisos.usp_Formularios_Traer_Todos"
        Const storeAgregarEliminarAccion As String = "Permisos.usp_Formulario_AgregarEliminarAccion"
        Const storeTraerTodosXRama As String = "Permisos.usp_Formularios_TraerTodosXRama"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Formulario)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@Id_User_Alta", entidad.IdUsuarioAlta)
            pa.add("@Id_Rama", entidad.IdRama)
            pa.add("@Orden", entidad.Orden)
            pa.add("@Formulario", entidad.Nombre.ToString.ToUpper.Trim)
            pa.add("@URL", entidad.URL.ToString.ToUpper.Trim)
            pa.add("@Accion", entidad.AccionesPosibles)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Formulario)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@Id_Formularios", entidad.IdEntidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Formulario)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@Id_User", entidad.IdUsuarioModifica)
            pa.add("@ID_Formulario", entidad.IdEntidad)
            pa.add("@ID_rama", entidad.IdRama)
            pa.add("@Orden", entidad.Orden)
            pa.add("@Formulario", entidad.Nombre.ToString.ToUpper.Trim)
            pa.add("@URL", entidad.URL.ToString.ToUpper.Trim)
            pa.add("@Accion", entidad.AccionesPosibles)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Formulario
            Dim store As String = storeTraerUnoXId
            Dim result As New Formulario
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
        Public Shared Function TraerTodos() As List(Of Formulario)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Formulario)
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
        Public Shared Function TraerTodosXRama(IdRama As Integer) As List(Of Formulario)
            Dim store As String = storeTraerTodosXRama
            Dim pa As New parametrosArray
            pa.add("@Id_Rama", IdRama)
            Dim listaResult As New List(Of Formulario)
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
        Public Shared Sub AgregarEliminarAccion(entidad As Formulario, IdAccion As Integer, Existe As Boolean)
            Dim store As String = storeAgregarEliminarAccion
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@IdFormulario", entidad.IdEntidad)
            pa.add("@IdAccion", IdAccion)
            pa.add("@Existe", Existe)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Formulario
            Dim entidad As New Formulario
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
            If dr.Table.Columns.Contains("id_Formularios") Then
                If dr.Item("id_Formularios") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Formularios"))
                End If
            End If
            If dr.Table.Columns.Contains("id_Ramas") Then
                If dr.Item("id_Ramas") IsNot DBNull.Value Then
                    entidad.IdRama = CInt(dr.Item("id_Ramas"))
                End If
            End If
            If dr.Table.Columns.Contains("Formularios") Then
                If dr.Item("Formularios") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Formularios").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("URL") Then
                If dr.Item("URL") IsNot DBNull.Value Then
                    entidad.URL = dr.Item("URL").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Accion") Then
                If dr.Item("Accion") IsNot DBNull.Value Then
                    entidad.AccionesPosibles = CInt(dr.Item("Accion"))
                End If
            End If
            If dr.Table.Columns.Contains("Orden") Then
                If dr.Item("Orden") IsNot DBNull.Value Then
                    entidad.Orden = CInt(dr.Item("Orden"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Formulario
End Namespace ' DataAccessLibrary