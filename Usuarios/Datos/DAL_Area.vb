'Public Class DAL_Area

'End Class

Option Explicit On
Option Strict On

Imports Usuarios.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Area

#Region " Stored "
        Const storeAlta As String = "p_Area_Alta"
        Const storeBaja As String = "p_Area_Baja"
        Const storeModifica As String = "p_Area_Modifica"
        Const storeTraerUnoXId As String = "p_Area_TraerUnoXId"
        Const storeTraerTodos As String = "Permisos.usp_Traer_Todas_Familias" '"p_Area_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Area_TraerTodosActivos"
        Const storeTraerTodosXIdUsuario As String = "Permisos.usp_Traer_FamiliaxIdUsuario"
        Const storeTraerAreaOrigenXIdTicket As String = "Tickets.usp_TraerAreaOrigenxIdTicket"
        Const storeTraerAreaDestinoXIdTicket As String = "Tickets.usp_TraerAreaDestinoxIdTicket"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Area)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
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
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa, "StrUtedyc2.0")
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Area)
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
            '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa, "StrUtedyc2.0")
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Area
            Dim store As String = storeTraerUnoXId
            Dim result As New Area
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
        Public Shared Function TraerTodos() As List(Of Area)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Area)
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
        Public Shared Function TraerTodosXIdUsuario(ByVal id As Integer) As List(Of Area)
            Dim store As String = storeTraerTodosXIdUsuario
            Dim pa As New parametrosArray
            pa.add("@IdUsuario", id)
            Dim listaResult As New List(Of Area)
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
        Public Shared Function TraerAreaOrigenXIdTicket(ByVal IdTicket As Integer) As Area
            Dim store As String = storeTraerAreaOrigenXIdTicket
            Dim result As New Area
            Dim pa As New parametrosArray
            pa.add("@IdTicket", IdTicket)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
        Public Shared Function TraerAreaDestinoXIdTicket(ByVal IdTicket As Integer) As Area
            Dim store As String = storeTraerAreaDestinoXIdTicket
            Dim result As New Area
            Dim pa As New parametrosArray
            pa.add("@IdTicket", IdTicket)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrUtedyc2.0")
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
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Area
            Dim entidad As New Area
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
            If dr.Table.Columns.Contains("Id_Familias") Then
                If dr.Item("Id_Familias") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Familias"))
                End If
            End If
            If dr.Table.Columns.Contains("Familias") Then
                If dr.Item("Familias") IsNot DBNull.Value Then
                    entidad.Descripcion = CStr(dr.Item("Familias")).Trim
                End If
            End If
            If dr.Table.Columns.Contains("Alcance") Then
                If dr.Item("Alcance") IsNot DBNull.Value Then
                    entidad.Alcance = CInt(dr.Item("Alcance"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Area
End Namespace ' DataAccessLibrary
