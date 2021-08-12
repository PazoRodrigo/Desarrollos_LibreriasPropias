Option Explicit On
Option Strict On

Imports ClasesEmpresa.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Afiliado

#Region " Stored "
        Const storeAlta As String = "p_Afiliado_Alta"
        Const storeBaja As String = "p_Afiliado_Baja"
        Const storeModifica As String = "p_Afiliado_Modifica"
        Const storeTraerUnoXId As String = "p_Afiliado_TraerUnoXId"
        Const storeTraerTodos As String = "p_Afiliado_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Afiliado_TraerTodosActivos"
        Const storeTraerTodosXEmpresa As String = "p_Afiliado_TraerTodosXEmpresa"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Afiliado)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
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
            Using dt As DataTable = Connection.Connection.Traer(store, pa).Tables(0)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        'entidad.idEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Afiliado)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
            ' pa.add("@id", entidad.idEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Afiliado)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
            'pa.add("@id", entidad.idEntidad)
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
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal NroAfiliado As Long, Parent As Integer) As Afiliado
            Dim store As String = storeTraerUnoXId
            Dim result As New Afiliado
            Dim pa As New parametrosArray
            'pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.Traer(store, pa).Tables(0)
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
        Public Shared Function TraerTodos() As List(Of Afiliado)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Afiliado)
            Using dt As DataTable = Connection.Connection.Traer(store, pa).Tables(0)
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
        Public Shared Function TraerTodosXEmpresa(CodEmpresa As Long, Dependencia As Integer) As List(Of Afiliado)
            Dim store As String = storeTraerTodosXEmpresa
            Dim pa As New parametrosArray
            pa.add("@CodEmpresa", Right("00000000000" & CodEmpresa, 11))
            pa.add("@Dependencia", CInt(Dependencia))
            Dim listaResult As New List(Of Afiliado)
            Using ds As DataTable = Connection.Connection.Traer(store, pa, "StrLibreriasPropiasOSPE").Tables(0)
                If ds.Rows.Count > 0 Then
                    For Each dr As DataRow In ds.Rows
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Afiliado
            Dim entidad As New Afiliado
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
            If dr.Table.Columns.Contains("Id_MotivoBaja") Then
                If dr.Item("Id_MotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("Id_MotivoBaja"))
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
            If dr.Table.Columns.Contains("fec_baja") Then
                If dr.Item("fec_baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fec_baja"))
                End If
            End If
            If dr.Table.Columns.Contains("prov") Then
                If dr.Item("prov") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("prov")) Then
                    entidad.IdSeccional = CInt(dr.Item("prov"))
                End If
            End If
            If dr.Table.Columns.Contains("NRO_AFI") Then
                If dr.Item("NRO_AFI") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("NRO_AFI")) Then
                    entidad.NroAfiliado = CLng(dr.Item("NRO_AFI"))
                End If
            End If
            If dr.Table.Columns.Contains("PARENT") Then
                If dr.Item("PARENT") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("PARENT")) Then
                    entidad.Parent = CInt(dr.Item("PARENT"))
                End If
            End If
            If dr.Table.Columns.Contains("NRO_DOC") Then
                If dr.Item("NRO_DOC") IsNot DBNull.Value AndAlso IsNumeric(dr.Item("NRO_DOC")) Then
                    entidad.NroDocumento = CLng(dr.Item("NRO_DOC"))
                End If
            End If
            If dr.Table.Columns.Contains("TIP_DOC") Then
                If dr.Item("TIP_DOC") IsNot DBNull.Value Then
                    entidad.TipoDocumento = (dr.Item("TIP_DOC")).ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("APE_NOM") Then
                If dr.Item("APE_NOM") IsNot DBNull.Value Then
                    entidad.ApellidoNombre = dr.Item("APE_NOM").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("CUIL") Then
                If dr.Item("CUIL") IsNot DBNull.Value Then
                    entidad.CUIL = CLng(Replace(dr.Item("CUIL").ToString, "-", ""))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Afiliado
End Namespace ' DataAccessLibrary