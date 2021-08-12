Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Familia

#Region " Stored "
        Const storeAlta As String = "permisos.usp_Familias_Alta"
        Const storeBaja As String = "permisos.usp_Familias_Baja"
        Const storeModifica As String = "permisos.usp_Familias_Modif"
        Const storeTraerTodos As String = "permisos.usp_Familias_Traer_Todas"
        'Const storeTraerTodos As String = "Permisos.usp_Traer_Todas_Familias"
        Const storeAgregarFormulario As String = "Permisos.usp_Familia_FormularioAgregar"
        Const storeEliminarFormulario As String = "Permisos.usp_Familia_FormularioEliminar"
        Const storeTraerFormularios As String = "Permisos.usp_Familia_TraerTodosXFormulario"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Familia)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioAlta)
            pa.add("@Familia", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Familia)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioBaja)
            pa.add("@id_Familias", entidad.IdEntidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Familia)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@Id_Usuarios", entidad.IdUsuarioModifica)
            pa.add("@id_Familia", entidad.IdEntidad)
            pa.add("@Familia", entidad.Nombre.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Familia)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Familia)
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
        Public Shared Sub Formulario_Agregar(entidad As Familia, IdFormulario As Integer)
            Dim store As String = storeAgregarFormulario
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@IdFormulario", IdFormulario)
            pa.add("@IdFamilia", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Sub Formulario_Eliminar(entidad As Familia, IdArea As Integer)
            Dim store As String = storeEliminarFormulario
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@IdFormulario", entidad.IdEntidad)
            pa.add("@IdFamilia", IdArea)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Function TraerTodasXFormulario(IdFormulario As Integer) As List(Of Familia)
            Dim store As String = storeTraerFormularios
            Dim pa As New parametrosArray
            pa.add("@IdFormulario", IdFormulario)
            Dim listaResult As New List(Of Familia)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Familia
            Dim entidad As New Familia
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
            If dr.Table.Columns.Contains("Id_Familias") Then
                If dr.Item("Id_Familias") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Familias"))
                End If
            End If
            If dr.Table.Columns.Contains("Familias") Then
                If dr.Item("Familias") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Familias").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Familia
End Namespace ' DataAccessLibrary



