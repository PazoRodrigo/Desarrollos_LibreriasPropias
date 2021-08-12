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

Public Class ExcelManager
    'Prueba de Modificacion
    Public Function GetDataFromExcel(ByVal a_sFilepath As String) As DataSet
        Dim ds As New DataSet()
        Dim stConexion As String = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (a_sFilepath & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=2"";")))
        'Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & a_sFilepath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties= Excel 8.0")
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
            'If dt IsNot Nothing Then
            'If dt.Rows.Count - 1 <> 1 Then
            '   Throw New Exception("La cantidad de hojas en el archivo es incorrecta. Valide que sea sólo una.")
            'Else
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
            'End If
        End If
        cn.Close()
        Return ds
    End Function
    Public Function GetDataFromExcelVersion2(ByVal a_sFilepath As String) As DataSet
        Dim ds As New DataSet()
        Dim stConexion As String = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (a_sFilepath & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1"";")))
        'Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & a_sFilepath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
        'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties= Excel 8.0")
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
            'If dt IsNot Nothing Then
            'If dt.Rows.Count - 1 <> 1 Then
            '   Throw New Exception("La cantidad de hojas en el archivo es incorrecta. Valide que sea sólo una.")
            'Else
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
            'End If
        End If
        cn.Close()
        Return ds
    End Function

    'Public Function ExportToExcel(ByVal a_sFilename As String, ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String) As Boolean
    '    a_sErrorMessage = String.Empty
    '    Dim bRetVal As Boolean = False
    '    Dim dsDataSet As DataSet = Nothing
    '    Try
    '        dsDataSet = a_sData
    '        Dim xlObject As Microsoft.Office.Tools.Excel.Application = Nothing
    '        Dim xlWB As Excel.Workbook = Nothing
    '        Dim xlSh As Excel.Worksheet = Nothing
    '        Dim rg As Excel.Range = Nothing
    '        Try
    '            xlObject = New Excel.Application()
    '            xlObject.AlertBeforeOverwriting = False
    '            xlObject.DisplayAlerts = False
    '            xlWB = xlObject.Workbooks.Add(Type.Missing)
    '            xlWB.SaveAs(a_sFilename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
    '            Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value)
    '            xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)
    '            Dim sUpperRange As String = "A1"
    '            Dim sLastCol As String = "E"
    '            Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()
    '            rg = xlSh.Range(sUpperRange, sLowerRange)
    '            rg.Value2 = GetData(dsDataSet.Tables(0))
    '            ' formating '
    '            xlSh.Range("A1", sLastCol & "1").Font.Bold = True
    '            xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
    '            xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()
    '            If String.IsNullOrEmpty(a_sFileTitle) Then
    '                xlObject.Caption = "untitled"
    '            Else
    '                xlObject.Caption = a_sFileTitle
    '            End If
    '            xlWB.Save()
    '            bRetVal = True
    '        Catch ex As System.Runtime.InteropServices.COMException
    '            If ex.ErrorCode = -2147221164 Then
    '                a_sErrorMessage = "Error en importacion: Por favor instale Microsoft Office (Excel) para poder utilizar este MIGRADOR."
    '            ElseIf ex.ErrorCode = -2146827284 Then
    '                a_sErrorMessage = "Error en importacion: Maximo de filas superada."
    '            Else
    '                a_sErrorMessage = (("Error en importacion: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
    '            End If
    '        Catch ex As Exception
    '            a_sErrorMessage = "Error en importacion: " & ex.Message
    '        Finally
    '            Try
    '                If xlWB IsNot Nothing Then
    '                    xlWB.Close(Nothing, Nothing, Nothing)
    '                End If
    '                xlObject.Workbooks.Close()
    '                xlObject.Quit()
    '                If rg IsNot Nothing Then
    '                    Marshal.ReleaseComObject(rg)
    '                End If
    '                If xlSh IsNot Nothing Then
    '                    Marshal.ReleaseComObject(xlSh)
    '                End If
    '                If xlWB IsNot Nothing Then
    '                    Marshal.ReleaseComObject(xlWB)
    '                End If
    '                If xlObject IsNot Nothing Then
    '                    Marshal.ReleaseComObject(xlObject)
    '                End If
    '            Catch
    '            End Try
    '            xlSh = Nothing
    '            xlWB = Nothing
    '            xlObject = Nothing
    '            GC.Collect()
    '            GC.WaitForPendingFinalizers()
    '        End Try
    '    Catch ex As Exception
    '        a_sErrorMessage = "Error de importacion: " & ex.Message
    '    End Try
    '    Return bRetVal
    'End Function
    '' returns data as two dimentional object array. '
    'Private Function GetData(ByVal a_dtData As System.Data.DataTable) As Object(,)
    '    Dim obj As Object(,) = New Object((a_dtData.Rows.Count + 1) - 1, a_dtData.Columns.Count - 1) {}
    '    Try
    '        For j As Integer = 0 To a_dtData.Columns.Count - 1
    '            obj(0, j) = a_dtData.Columns(j).Caption
    '        Next
    '        Dim dt As New DateTime()
    '        Dim sTmpStr As String = String.Empty
    '        For i As Integer = 1 To a_dtData.Rows.Count
    '            For j As Integer = 0 To a_dtData.Columns.Count - 1
    '                If a_dtData.Columns(j).DataType Is dt.[GetType]() Then
    '                    If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
    '                        DateTime.TryParse(a_dtData.Rows(i - 1)(j).ToString(), dt)
    '                        obj(i, j) = dt.ToString("MM/dd/yy hh:mm tt")
    '                    Else
    '                        obj(i, j) = a_dtData.Rows(i - 1)(j)
    '                    End If
    '                ElseIf a_dtData.Columns(j).DataType Is sTmpStr.[GetType]() Then
    '                    If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
    '                        sTmpStr = a_dtData.Rows(i - 1)(j).ToString().Replace(vbCr, "")
    '                        obj(i, j) = sTmpStr
    '                    Else
    '                        obj(i, j) = a_dtData.Rows(i - 1)(j)
    '                    End If
    '                Else
    '                    obj(i, j) = a_dtData.Rows(i - 1)(j)
    '                End If
    '            Next
    '        Next
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return obj
    'End Function
End Class
