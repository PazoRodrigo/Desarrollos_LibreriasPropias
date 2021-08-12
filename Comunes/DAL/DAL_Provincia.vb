Option Explicit On
Option Strict On

Imports Comunes.Entidad
Imports Connection

Namespace DataAccessLibrary
    Public Class DAL_Provincia

#Region " Stored "
        Const storeTraerTodos As String = "p_Provincia_TraerTodos"
#End Region
#Region " Métodos Públicos "
        Public Shared Function TraerTodos() As List(Of Provincia)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Provincia)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Provincia
            Dim entidad As New Provincia
            ' Entidad
            If dr.Table.Columns.Contains("cod_prov") Then
                If dr.Item("cod_prov") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("cod_prov"))
                End If
            End If
            If dr.Table.Columns.Contains("desc_prov") Then
                If dr.Item("desc_prov") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("desc_prov").ToString.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Provincia
End Namespace ' DataAccessLibrary