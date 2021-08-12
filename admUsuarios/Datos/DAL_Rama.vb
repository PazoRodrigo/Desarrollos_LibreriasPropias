Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Rama

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Ramas_Alta"
        Const storeBaja As String = "Permisos.usp_Ramas_Baja"
        Const storeModifica As String = "Permisos.usp_Ramas_Modif"
        Const storeTraerTodos As String = "Permisos.usp_Ramas_Traer_Todos"
        Const storeReordenar As String = "Permisos.usp_Reordenamiento_Formularios"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Rama)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@id_User_Alta", entidad.IdUsuarioAlta)
            pa.add("@Id_Familias", entidad.IdFamilia)
            pa.add("@Orden", entidad.Orden)
            pa.add("@Desc_Menu", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Rama)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@id_Rama", entidad.IdEntidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Rama)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@id_User_Modif", entidad.IdUsuarioModifica)
            pa.add("@id_Rama", entidad.IdEntidad)
            pa.add("@Id_Familias", entidad.IdFamilia)
            pa.add("@Orden", entidad.Orden)
            pa.add("@Ramas", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Rama)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Rama)
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
        Public Shared Function Reordenar(ByVal IdRama As Integer) As List(Of Rama)
            Dim store As String = storeReordenar
            Dim pa As New parametrosArray
            pa.add("@Id_Ramas", IdRama)
            Dim listaResult As New List(Of Rama)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Rama
            Dim entidad As New Rama
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
            If dr.Table.Columns.Contains("id_Ramas") Then
                If dr.Item("id_Ramas") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_Ramas"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_Familias") Then
                If dr.Item("Id_Familias") IsNot DBNull.Value Then
                    entidad.IdFamilia = CInt(dr.Item("Id_Familias"))
                End If
            End If
            If dr.Table.Columns.Contains("Ramas") Then
                If dr.Item("Ramas") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Ramas").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Rama
End Namespace ' DataAccessLibrary