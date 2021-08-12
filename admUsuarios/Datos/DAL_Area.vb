Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Area

#Region " Stored "
        Const storeAlta As String = "permisos.usp_Areas_Alta"
        Const storeBaja As String = "permisos.usp_Areas_Baja"
        Const storeModifica As String = "permisos.usp_Areas_Modif"
        Const storeTraerTodos As String = "permisos.usp_Areas_Traer_Todas"
        Const storeTraerTodosXUsuario As String = "permisos.usp_Areas_TraerTodosXUsuario"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Area)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioAlta)
            pa.add("@Area", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Area)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@Id_Area", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Area)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioModifica)
            pa.add("@id_Area", entidad.IdEntidad)
            pa.add("@Area", entidad.Nombre.ToString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Area)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Area)
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
        Friend Shared Function TraerTodosXUsuario(IdUsuario As Integer) As List(Of Area)
            Dim store As String = storeTraerTodosXUsuario
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", IdUsuario)
            Dim listaResult As New List(Of Area)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Area
            Dim entidad As New Area
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
            If dr.Table.Columns.Contains("id_Areas") Then
                If dr.Item("id_Areas") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Areas"))
                End If
            End If
            If dr.Table.Columns.Contains("Areas") Then
                If dr.Item("Areas") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Areas").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Area
End Namespace ' DataAccessLibrary