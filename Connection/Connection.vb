Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.ComponentModel

Public Class Connection
#Region " Variable "
    Private Shared ReadOnly TimeOutStored As Integer = CInt(ConfigurationManager.AppSettings("TimeOut"))
#End Region

    Public Structure StTransaccion
        Dim cabecera As Boolean
        Dim stored As String
        Dim parametros As parametrosArray
    End Structure

#Region " Ejecutar "
    ' Ejecutar sin DB
    Public Shared Sub Ejecutar(ByVal storedProcedure As String, ByVal parameters As parametrosArray)
        Ejecutar(storedProcedure, parameters, "strConn")
    End Sub
    ' Ejecutar con DB
    Public Shared Sub Ejecutar(ByVal storedProcedure As String, ByVal parameters As parametrosArray, ByVal conexion As String)
        Try
            Using conn As New SqlConnection(TraerConexion(conexion))
                Using Comando As New SqlCommand
                    Dim pa As New parametrosArray
                    pa = parameters
                    With Comando
                        .Parameters.Clear()
                        .CommandType = CommandType.StoredProcedure
                        .Connection = conn
                        .CommandText = storedProcedure
                        .CommandTimeout = 200
                        For Each p As SqlParameter In pa.Parametros
                            .Parameters.Add(p)
                        Next
                    End With
                    Try
                        conn.Open()
                        Comando.ExecuteNonQuery()
                    Catch ex As Exception
                        Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & ex.Message)
                    Finally
                        pa.clear()
                        Comando.Dispose()
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Ejecutar con Manejo de Errores sin DB
    Public Shared Sub EjecutarConManejoDeError(ByVal storedProcedure As String, ByVal parameters As parametrosArray)
        EjecutarConManejoDeError(storedProcedure, parameters, "strConn")
    End Sub
    ' Ejecutar con Manejo de Errores con DB
    Public Shared Sub EjecutarConManejoDeError(ByVal storedProcedure As String, ByVal parameters As parametrosArray, ByVal conexion As String)
        Try
            Dim conn As New SqlConnection(TraerConexion(conexion))
            Dim Comando As New SqlCommand
            Dim stored As String = storedProcedure
            Dim pa As New parametrosArray
            pa = parameters
            With Comando
                .Parameters.Clear()
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .CommandText = stored
                .CommandTimeout = 200
                For Each p As SqlParameter In pa.Parametros
                    .Parameters.Add(p)
                Next
            End With
            Try
                Using da As New SqlDataAdapter
                    da.SelectCommand = Comando
                    Using ds As New DataSet
                        ds.Clear()
                        da.Fill(ds)
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(0).Columns.Contains("Error") Then
                                    Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & (ds.Tables(0).Rows(0)("Error").ToString & " - " & ds.Tables(0).Rows(0)("Mensaje").ToString))
                                End If
                            End If
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & ex.Message)
            Finally
                pa.clear()
                Comando.Dispose()
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Ejecutar con TimeOut sin DB
    Public Shared Sub EjecutarTimeOut(ByVal storedProcedure As String, ByVal parameters As parametrosArray)
        EjecutarTimeOut(storedProcedure, parameters, "strConn")
    End Sub
    ' Ejecutar con TimeOut con DB
    Public Shared Sub EjecutarTimeOut(ByVal storedProcedure As String, ByVal parameters As parametrosArray, conexion As String)
        Try
            Dim conn As New SqlConnection(TraerConexion(conexion))
            Dim Comando As New SqlCommand
            Dim stored As String = storedProcedure
            Dim pa As New parametrosArray
            pa = parameters
            With Comando
                .Parameters.Clear()
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .CommandText = stored
                .CommandTimeout = TimeOutStored
                For Each p As SqlParameter In pa.Parametros
                    .Parameters.Add(p)
                Next
            End With
            Try
                conn.Open()
                Comando.ExecuteNonQuery()
            Catch ex As Exception
                Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & ex.Message)
            Finally
                pa.clear()
                Comando.Dispose()
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Ejecutar utilizando la Transacion de SQL desde VB.NET sin DB
    Public Shared Sub EjecutarTransacion(lista As List(Of StTransaccion))
        EjecutarTransacion(lista, "strConn")
    End Sub
    ' Ejecutar utilizando la Transacion de SQL desde VB.NET con DB
    Public Shared Sub EjecutarTransacion(lista As List(Of StTransaccion), conexion As String)
        Dim strException As String = ""
        Dim conn As New SqlConnection(TraerConexion(conexion))
        Dim trans As SqlTransaction = Nothing
        conn.Open()
        trans = conn.BeginTransaction()
        Dim Comando As New SqlCommand
        Dim i As Integer = 0
        While strException = "" And i < lista.Count - 1
            Dim item As StTransaccion = lista(i)
            Dim stored As String = item.stored
            Dim pa As New parametrosArray
            pa = item.parametros
            With Comando
                .Parameters.Clear()
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .CommandText = stored
                .CommandTimeout = 200
                For Each p As SqlParameter In pa.Parametros
                    .Parameters.Add(p)
                Next
            End With
            Try
                Comando.ExecuteNonQuery()
            Catch ex As Exception
                strException = "Se produjo un error SP: " & item.stored & " - " & ex.Message
            Finally
                pa.clear()
            End Try
            i += 1
        End While
        Comando.Dispose()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Dispose()
        If strException = "" Then
            'Next
            trans.Commit()
        Else
            trans.Rollback()
            Throw New Exception(strException)
        End If
    End Sub
    ' Ejecutar  utilizando la Transacion de SQL desde VB.NET sin DB
    Public Shared Function EjecutarTransacionCabeceraLinea(lista As List(Of StTransaccion), parametroIdCabecera As String) As Integer
        Return EjecutarTransacionCabeceraLinea(lista, parametroIdCabecera, "strConn")
    End Function
    ' Ejecutar  utilizando la Transacion de SQL desde VB.NET con DB
    Public Shared Function EjecutarTransacionCabeceraLinea(lista As List(Of StTransaccion), parametroIdCabecera As String, conexion As String) As Integer
        Dim strException As String = ""
        Dim ConnString As String = ConfigurationManager.ConnectionStrings(conexion).ToString
        Dim conn As New SqlConnection(ConnString)
        Dim trans As SqlTransaction = Nothing
        conn.Open()
        trans = conn.BeginTransaction()
        Dim Comando As New SqlCommand
        Comando.Transaction = trans
        Dim i As Integer = 0
        Dim id As Integer = 0
        While strException = "" And i < lista.Count
            Dim item As StTransaccion = lista(i)
            Dim stored As String = item.stored
            Dim pa As New parametrosArray
            pa = item.parametros
            If item.cabecera Then
                With Comando
                    .Parameters.Clear()
                    .CommandType = CommandType.StoredProcedure
                    .Connection = conn
                    .CommandText = stored
                    .CommandTimeout = 200
                    For Each p As SqlParameter In pa.Parametros
                        .Parameters.Add(p)
                    Next
                End With
                Try
                    Using da As New SqlDataAdapter
                        da.SelectCommand = Comando
                        Using ds As New DataSet
                            ds.Clear()
                            da.Fill(ds)
                            If ds.Tables(0).Rows.Count > 0 Then
                                If Not ds.Tables(0).Columns.Contains("Error") Then
                                    id = CInt(ds.Tables(0)(0)(0))
                                Else
                                    strException = "Se produjo un error SP: " & item.stored & " - " & (ds.Tables(0).Rows(0)("Error").ToString & " - " & ds.Tables(0).Rows(0)("Mensaje").ToString)
                                End If
                            Else
                                strException = "Se produjo un error SP: " & item.stored & ". No se pudo insertar la cabecera."
                            End If
                        End Using
                    End Using
                Catch ex As Exception
                    strException = "Se produjo un error SP: " & item.stored & " - " & ex.Message
                End Try
            Else
                With Comando
                    .Parameters.Clear()
                    .CommandType = CommandType.StoredProcedure
                    .Connection = conn
                    .CommandText = stored
                    .CommandTimeout = 200
                    For Each p As SqlParameter In pa.Parametros
                        .Parameters.Add(p)
                    Next
                    .Parameters.AddWithValue(parametroIdCabecera, id)
                End With
                Try
                    Using da As New SqlDataAdapter
                        da.SelectCommand = Comando
                        Using ds As New DataSet
                            ds.Clear()
                            da.Fill(ds)
                            If ds.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(0).Columns.Contains("Error") Then
                                    strException = "Se produjo un error SP: " & item.stored & " - " & (ds.Tables(0).Rows(0)("Error").ToString & " - " & ds.Tables(0).Rows(0)("Mensaje").ToString)
                                End If
                            Else
                                strException = "Se produjo un error SP: " & item.stored & ". No se pudo insertar la linea."
                            End If
                        End Using
                    End Using
                Catch ex As Exception
                    strException = "Se produjo un error SP: " & item.stored & " - " & ex.Message
                Finally
                    pa.clear()
                End Try
            End If
            i += 1
        End While
        Try
            If strException = "" Then
                trans.Commit()
                Return id
            Else
                trans.Rollback()
                Throw New Exception(strException)
            End If
        Catch ex As Exception
            Comando.Dispose()
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            Throw
        End Try
    End Function
#End Region
#Region " Traer DataSet "
    ' Traer DataSet sin parámetros, sin DB
    Public Shared Function Traer(ByVal storedProcedure As String) As DataSet
        Return Traer(storedProcedure, "strConn")
    End Function
    ' Traer DataSet sin parámetros, con DB
    Public Shared Function Traer(ByVal storedProcedure As String, ByVal conexion As String) As DataSet
        Try
            Using conn As New SqlConnection(TraerConexion(conexion))
                Using Comando As New SqlCommand
                    With Comando
                        .CommandType = CommandType.StoredProcedure
                        .Connection = conn
                        .CommandText = storedProcedure
                        .CommandTimeout = 200
                    End With
                    Try
                        Using da As New SqlDataAdapter
                            da.SelectCommand = Comando
                            Using ds As New DataSet
                                ds.Clear()
                                da.Fill(ds)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    If ds.Tables(0).Columns.Contains("Error") Then
                                        Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & (ds.Tables(0).Rows(0)("Error").ToString & " - " & ds.Tables(0).Rows(0)("Mensaje").ToString))
                                    End If
                                End If
                                Return ds
                            End Using
                        End Using
                    Catch ex As Exception
                        Throw ex
                    Finally
                        Comando.Dispose()
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' Traer DataSet con parámetros, sin DB
    Public Shared Function Traer(ByVal storedProcedure As String, ByVal parameters As parametrosArray) As DataSet
        Return Traer(storedProcedure, parameters, "strConn")
    End Function
    ' Traer DataSet con parámetros, con DB
    Public Shared Function Traer(ByVal storedProcedure As String, ByVal parameters As parametrosArray, ByVal conexion As String) As DataSet
        Try
            Using conn As New SqlConnection(TraerConexion(conexion))
                Using Comando As New SqlCommand
                    Using pa As parametrosArray = parameters
                        With Comando
                            .CommandType = CommandType.StoredProcedure
                            .Connection = conn
                            .CommandText = storedProcedure
                            .CommandTimeout = 200
                            For Each p As SqlParameter In pa.Parametros
                                .Parameters.Add(p)
                            Next
                        End With
                        Try
                            Using da As New SqlDataAdapter
                                da.SelectCommand = Comando
                                Using ds As New DataSet
                                    ds.Clear()
                                    da.Fill(ds)
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        If ds.Tables(0).Columns.Contains("Error") Then
                                            Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & (ds.Tables(0).Rows(0)("Error").ToString & " - " & ds.Tables(0).Rows(0)("Mensaje").ToString))
                                        End If
                                    End If
                                    Return ds
                                End Using
                            End Using
                        Catch ex As Exception
                            Throw ex
                        Finally
                            pa.clear()
                            Comando.Dispose()
                            If conn.State = ConnectionState.Open Then
                                conn.Close()
                            End If
                        End Try
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region " Traer DataTable "
    ' Traer DataTable sin parámetros, sin DB
    Public Shared Function TraerDt(ByVal storedProcedure As String) As DataTable
        Return TraerDt(storedProcedure, "strConn")
    End Function
    ' Traer DataTable sin parámetros, con DB
    Public Shared Function TraerDt(ByVal storedProcedure As String, ByVal conexion As String) As DataTable
        Try
            Using conn As New SqlConnection(TraerConexion(conexion))
                Using Comando As New SqlCommand
                    With Comando
                        .CommandType = CommandType.StoredProcedure
                        .Connection = conn
                        .CommandText = storedProcedure
                        .CommandTimeout = 200
                    End With
                    Try
                        Using da As New SqlDataAdapter
                            da.SelectCommand = Comando
                            Using dt As New DataTable
                                dt.Clear()
                                da.Fill(dt)
                                If dt.Rows.Count > 0 Then
                                    If dt.Columns.Contains("Error") Then
                                        Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & (dt.Rows(0)("Error").ToString & " - " & dt.Rows(0)("Mensaje").ToString))
                                    End If
                                End If
                                Return dt
                            End Using
                        End Using
                    Catch ex As Exception
                        Throw ex
                    Finally
                        Comando.Dispose()
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' Traer DataTable con parámetros, sin DB
    Public Shared Function TraerDt(ByVal storedProcedure As String, ByVal parameters As parametrosArray) As DataTable
        Return TraerDt(storedProcedure, parameters, "strConn")
    End Function
    ' Traer DataTable con parámetros, con DB
    Public Shared Function TraerDt(ByVal storedProcedure As String, ByVal parameters As parametrosArray, ByVal conexion As String) As DataTable
        Try
            Using conn As New SqlConnection(TraerConexion(conexion))
                Using Comando As New SqlCommand
                    Using pa As parametrosArray = parameters
                        With Comando
                            .CommandType = CommandType.StoredProcedure
                            .Connection = conn
                            .CommandText = storedProcedure
                            .CommandTimeout = 200
                            For Each p As SqlParameter In pa.Parametros
                                .Parameters.Add(p)
                            Next
                        End With
                        Try
                            Using da As New SqlDataAdapter
                                da.SelectCommand = Comando
                                Using dt As New DataTable
                                    dt.Clear()
                                    da.Fill(dt)
                                    If dt.Rows.Count > 0 Then
                                        If dt.Columns.Contains("Error") Then
                                            Throw New Exception("Se produjo un error SP: " & storedProcedure & " - " & (dt.Rows(0)("Error").ToString & " - " & dt.Rows(0)("Mensaje").ToString))
                                        End If
                                    End If
                                    Return dt
                                End Using
                            End Using
                        Catch ex As Exception
                            Throw ex
                        Finally
                            pa.clear()
                            Comando.Dispose()
                            If conn.State = ConnectionState.Open Then
                                conn.Close()
                            End If
                        End Try
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region " Métodos Privados. LUM"
    Private Shared Function TraerConexion(ByVal Conexion As String) As String
        Try
            Return ConfigurationManager.ConnectionStrings(Conexion).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
