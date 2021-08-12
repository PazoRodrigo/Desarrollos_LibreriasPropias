Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Acceso

#Region " Stored "
        Const storeAlta As String = "Permisos.usp_Accesos_Alta"
        Const storeBaja As String = "p_Acceso_Baja"
        Const storeModifica As String = "p_Acceso_Modifica"
        Const storeTraerUnoXId As String = "p_Acceso_TraerUnoXId"
        Const storeTraerTodos As String = "p_Acceso_TraerTodos"
        'Const storeTraerTodosXPerfil As String = "Permisos.usp_Accesos_TraerTodosXPerfil"
        Const storeTraerTodosXPerfil As String = "Permisos.usp_Roles_Perfil_Familia_Ramas_Formulario_Traer_TodosxId_Perfiles"
        Const storeTraerTodosXFormulario As String = "Permisos.usp_Roles_Perfil_Familia_Ramas_Formulario_Traer_TodosxId_Formularios"
        ' Acciones
        Const storeAgregarEliminarAccion As String = "Permisos.usp_Acceso_AgregarEliminarAccion"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Acceso)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@Id_Formularios", entidad.IdFormulario)
            pa.add("@Id_Roles", entidad.ObjPerfil.IdRol)
            pa.add("@Id_Perfiles", entidad.IdPerfil)
            pa.add("@Id_Familias", entidad.IdFamilia)
            pa.add("@Id_Ramas", entidad.IdRama)
            pa.add("@Accion", entidad.IdAccion)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "strConnUsuarios")
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Acceso)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@FechaBaja", entidad.FechaBaja)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Acceso)
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
        ' Otros
        Public Shared Sub AgregarEliminarAccion(entidad As Acceso, IdAccion As Integer, Existe As Boolean)
            Dim store As String = storeAgregarEliminarAccion
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", entidad.IdUsuarioAlta)
            pa.add("@IdAcceso", entidad.IdEntidad)
            pa.add("@IdAccion", IdAccion)
            pa.add("@Existe", Existe)
            Connection.Connection.Ejecutar(store, pa, "strConnUsuarios")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Acceso
            Dim store As String = storeTraerUnoXId
            Dim result As New Acceso
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
        Public Shared Function TraerTodos() As List(Of Acceso)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Acceso)
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
        Public Shared Function TraerTodosXPerfil(IdPerfil As Integer) As List(Of Acceso)
            Dim store As String = storeTraerTodosXPerfil
            Dim pa As New parametrosArray
            pa.add("@Id_Perfiles", IdPerfil)
            Dim listaResult As New List(Of Acceso)
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
        Friend Shared Function TraerTodosXFormulario(IdFormulario As Integer) As List(Of Acceso)
            Dim store As String = storeTraerTodosXFormulario
            Dim pa As New parametrosArray
            pa.add("@Id_Formularios", IdFormulario)
            Dim listaResult As New List(Of Acceso)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Acceso
            Dim entidad As New Acceso
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
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("Id_Template") Then
                If dr.Item("Id_Template") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Template"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_Formularios") Then
                If dr.Item("Id_Formularios") IsNot DBNull.Value Then
                    entidad.IdFormulario = CInt(dr.Item("Id_Formularios"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_Familias") Then
                If dr.Item("Id_Familias") IsNot DBNull.Value Then
                    entidad.IdFamilia = CInt(dr.Item("Id_Familias"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_Ramas") Then
                If dr.Item("Id_Ramas") IsNot DBNull.Value Then
                    entidad.IdRama = CInt(dr.Item("Id_Ramas"))
                End If
            End If
            If dr.Table.Columns.Contains("Id_Perfiles") Then
                If dr.Item("Id_Perfiles") IsNot DBNull.Value Then
                    entidad.IdPerfil = CInt(dr.Item("Id_Perfiles"))
                End If
            End If
            If dr.Table.Columns.Contains("Accion") Then
                If dr.Item("Accion") IsNot DBNull.Value Then
                    entidad.IdAccion = CInt(dr.Item("Accion"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Acceso
End Namespace ' DataAccessLibrary