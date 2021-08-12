Option Explicit On
Option Strict On

Imports ClasesEmpresa.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Representado

#Region " Stored "
        'Const storeAlta As String = "p_Representado_Alta"
        'Const storeBaja As String = "p_Representado_Baja"
        'Const storeModifica As String = "p_Representado_Modifica"
        Const storeTraerUnoXId As String = "p_Representado_TraerUnoXId"
        Const storeTraerUnoXNroSindical As String = "p_Representado_TraerUnoXNroSindical"
        Const storeTraerUnoXNroDocumento As String = "p_Representado_TraerUnoXNroDocumento"
        Const storeTraerUnoXCUIL As String = "p_Representado_TraerUnoXCUIL"
        Const storeTraerTodosXApellidoNombre As String = "p_Representado_TraerTodosXApellidoNombre"
        Const storeTraerTodosXEmpresa As String = "p_Representado_TraerTodosXEmpresa"
#End Region
#Region " Métodos Públicos "
        ' ABM
        'Public Shared Sub Alta(ByVal entidad As Representado)
        '    Dim store As String = storeAlta
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
        '    ' Variable Numérica
        '    '	If entidad.codPostal <> 0 Then
        '    '		pa.add("@VariableNumero", entidad.VariableNumero)
        '    '	Else
        '    '		pa.add("@codPostal", "borrarEntero")
        '    '	End If
        '    ' VariableFecha
        '    '	If entidad.fechaNacimiento.HasValue Then
        '    '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
        '    '	Else
        '    '		pa.add("@fechaNacimiento", "borrarFecha")
        '    '	End If
        '    ' VariableString
        '    '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
        '    'Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '    '    If Not dt Is Nothing Then
        '    '        If dt.Rows.Count = 1 Then
        '    '            entidad.IdEntidad = CInt(dt.Rows(0)(0))
        '    '        End If
        '    '    End If
        '    'End Using
        'End Sub
        'Public Shared Sub Baja(ByVal entidad As Representado)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
        '    'pa.add("@id", entidad.IdEntidad)
        '    Connection.Connection.Ejecutar(store, pa, "StrLibreriasPropiasUTE")
        'End Sub
        'Public Shared Sub Modifica(ByVal entidad As Representado)
        '    Dim store As String = storeModifica
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
        '    'pa.add("@id", entidad.IdEntidad)
        '    ' Variable Numérica
        '    '	If entidad.codPostal <> 0 Then
        '    '		pa.add("@VariableNumero", entidad.VariableNumero)
        '    '	Else
        '    '		pa.add("@codPostal", "borrarEntero")
        '    '	End If
        '    ' VariableFecha
        '    '	If entidad.fechaNacimiento.HasValue Then
        '    '		pa.add("@fechaNacimiento", entidad.fechaNacimiento)
        '    '	Else
        '    '		pa.add("@fechaNacimiento", "borrarFecha")
        '    '	End If
        '    ' VariableString
        '    '	pa.add("@VariableString", entidad.VariableString.ToUpper.Trim)
        '    Connection.Connection.Ejecutar(store, pa, "StrLibreriasPropiasUTE")
        'End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Representado
            Dim store As String = storeTraerUnoXId
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Dim result As New Representado
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
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
        Public Shared Function TraerUnoXNroSindical(NroSindical As Long) As Representado
            Dim store As String = storeTraerUnoXNroSindical
            Dim pa As New parametrosArray
            pa.add("@NroSindical", NroSindical)
            Dim result As New Representado
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
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
        Public Shared Function TraerUnoXNroDocumento(NroDocumento As Long) As Representado
            Dim store As String = storeTraerUnoXNroDocumento
            Dim pa As New parametrosArray
            pa.add("@NroDocumento", NroDocumento)
            Dim result As New Representado
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
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
        'Public Shared Function traerxgenericsprueba() As List(Of Representado)
        '    Dim pa As New parametrosArray
        '    pa.add("@ApellidoNombre", "pereira")
        '    Return CType(Connection.Connection.TraerListaDeObjetosGenerics(Of Representado)("p_Representado_TraerTodosXApellidoNombre", pa, "StrLibreriasPropiasUTE"), List(Of Representado))
        'End Function
        Public Shared Function TraerTodosXApellidoNombre(ApellidoNombre As String) As List(Of Representado)
            Dim store As String = storeTraerTodosXApellidoNombre
            Dim pa As New parametrosArray
            pa.add("@ApellidoNombre", ApellidoNombre.Trim)
            Dim listaResult As New List(Of Representado)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
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
        Public Shared Function TraerTodosXEmpresa(CodEmpresa As Long, Dependencia As Integer) As List(Of Representado)
            Dim store As String = storeTraerTodosXEmpresa
            Dim cadena As String = ""
            'If base = LUM.Enums.Base.Utedyc Then
            '    cadena = "StrLibreriasPropiasUTE"
            'ElseIf base = LUM.Enums.Base.Ospedyc Then
            '    cadena = "StrLibreriasPropiasOSPE"
            'End If
            Dim pa As New parametrosArray
            pa.add("@CodEmpresa", Right("00000000000" & CodEmpresa, 11))
            pa.add("@Dependencia", CInt(Dependencia))
            Dim listaResult As New List(Of Representado)
            Using ds As DataTable = Connection.Connection.Traer(store, pa, "StrLibreriasPropiasUTE").Tables(0)
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
        Public Shared Function TraerUnoXCUIL(CUIL As Long) As Representado
            Dim store As String = storeTraerUnoXCUIL
            Dim pa As New parametrosArray
            pa.add("@CUIL", CUIL)
            Dim result As New Representado
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasPropiasUTE")
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Representado
            Dim entidad As New Representado
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
            'Entidad UTEDYC
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
            If dr.Table.Columns.Contains("NRO_SIND") Then
                If dr.Item("NRO_SIND") IsNot DBNull.Value Then
                    If IsNumeric(dr.Item("NRO_SIND")) Then
                        entidad.NroSindical = CLng(dr.Item("NRO_SIND"))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("id_afiliado") Then
                If dr.Item("id_afiliado") IsNot DBNull.Value Then
                    entidad.IdAfiliado = CInt(dr.Item("id_afiliado"))
                End If
            End If
            If dr.Table.Columns.Contains("OBSERVAC") Then
                If dr.Item("OBSERVAC") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("OBSERVAC").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("sec") Then
                If dr.Item("sec") IsNot DBNull.Value Then
                    entidad.IdSeccional = CInt(dr.Item("sec"))
                End If
            End If
            If dr.Table.Columns.Contains("afutedyc") Then
                If dr.Item("afutedyc") IsNot DBNull.Value Then
                    entidad.IdTipoAfiliado = CInt(dr.Item("afutedyc"))
                End If
            End If
            If dr.Table.Columns.Contains("sex") Then
                If dr.Item("sex") IsNot DBNull.Value Then
                    entidad.IdSexo = CInt(dr.Item("sex"))
                End If
            End If
            If dr.Table.Columns.Contains("fec_ing") Then
                If dr.Item("fec_ing") IsNot DBNull.Value Then
                    entidad.FechaIngreso = CDate(dr.Item("fec_ing"))
                End If
            End If
            If dr.Table.Columns.Contains("fec_nac") Then
                If dr.Item("fec_nac") IsNot DBNull.Value Then
                    entidad.FechaNacimiento = CDate(dr.Item("fec_nac"))
                End If
            End If
            If dr.Table.Columns.Contains("NAC") Then
                If dr.Item("NAC") IsNot DBNull.Value Then
                    entidad.Nacionalidad = CChar(dr.Item("NAC"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Empleado
End Namespace ' DataAccessLibrary