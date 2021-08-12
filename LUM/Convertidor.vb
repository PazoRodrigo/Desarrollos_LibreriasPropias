Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Convertidor

    Public Shared Function aDecimal(ByVal valor As String) As Decimal
        If Not IsNumeric(valor) Then
            Throw New Exception("Revise la conversión de valores")
        End If
        Return Decimal.Parse(valor.Replace(".", ","))
    End Function
    Public Shared Function ConvertToDataTable(Of T)(ByVal list As List(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function
    Public Shared Function ConvertirExcelADatatable(ByVal a_sFilepath As String) As DataSet
        Dim ds As New DataSet()
        Dim stConexion As String = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (a_sFilepath & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=2"";")))
        Dim cn As New OleDbConnection(stConexion)
        Try
            cn.Open()
        Catch ex As OleDbException
            Throw New Exception(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Dim dt As New System.Data.DataTable()
        dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            For sheet_count As Integer = 0 To dt.Rows.Count - 1
                Try
                    Dim sheetname As String = dt.Rows(sheet_count)("table_name").ToString()
                    Dim da As New OleDbDataAdapter("SELECT * FROM [" & sheetname & "]", cn)
                    da.Fill(ds, sheetname)
                Catch ex As DataException
                    Throw New Exception(ex.Message)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next
        End If
        cn.Close()
        Return ds
    End Function
    Public Shared Function DateToDB(ByVal calFecha As Object) As String
        If IsNothing(calFecha) OrElse calFecha.ToString.Trim = "" Then
            DateToDB = ""
        Else
            DateToDB = CDate(calFecha).ToString("yyyy-MM-dd")
        End If
    End Function
    Public Shared Function DateFromDB(ByVal calFecha As Object) As String
        If IsDBNull(calFecha) OrElse IsNothing(calFecha) OrElse calFecha.ToString.Trim = "" Then
            DateFromDB = ""
        Else
            DateFromDB = CDate(calFecha).ToString("dd/MM/yyyy")
        End If
    End Function
End Class
