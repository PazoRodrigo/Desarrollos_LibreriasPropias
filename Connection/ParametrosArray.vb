Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data
Imports System.ComponentModel

Public Class parametrosArray
    Implements IDisposable
    Private _Parametros As New List(Of SqlParameter)

    Public Property Parametros() As List(Of SqlParameter)
        Get
            Return _Parametros
        End Get
        Set(ByVal value As List(Of SqlParameter))
            _Parametros = value
        End Set
    End Property
    Public Sub clear()
        Parametros.Clear()
    End Sub
    Public Sub add(ByVal name As String, ByVal valor As Object)
        Dim pa As New SqlParameter()
        If valor Is Nothing Then
            pa = New SqlParameter(name, DBNull.Value)
        Else
            If CType(valor, String) = "borrar" Then
                pa = New SqlParameter(name, SqlDbType.VarChar)
                pa.Value = "Null"
            Else
                If CType(valor, String) = "borrarFecha" Then
                    pa = New SqlParameter(name, SqlDbType.DateTime)
                    pa.Value = DBNull.Value
                Else
                    If CType(valor, String) = "borrarEntero" Then
                        pa = New SqlParameter(name, SqlDbType.Int)
                        pa.Value = DBNull.Value
                    Else
                        If TypeOf (valor) Is Byte Then
                            pa = New SqlParameter(name, SqlDbType.Image)
                            pa.Value = valor
                        Else
                            pa = New SqlParameter(name, valor)
                        End If
                    End If
                End If
            End If
        End If
        Parametros.Add(pa)
    End Sub
    'Public Sub add(ByVal name As String, ByVal valor As Object)
    '    Dim pa As New SqlParameter()
    '    If valor Is Nothing Then
    '        pa = New SqlParameter(name, DBNull.Value)
    '    Else
    '        If CType(valor, String) = "borrar" Then
    '            pa = New SqlParameter(name, SqlDbType.NVarChar)
    '            pa.Value = "Null"
    '        Else
    '            If CType(valor, String) = "borrarFecha" Then
    '                pa = New SqlParameter(name, SqlDbType.DateTime)
    '                pa.Value = DBNull.Value
    '            Else
    '                If CType(valor, String) = "borrarEntero" Then
    '                    pa = New SqlParameter(name, SqlDbType.Int)
    '                    pa.Value = DBNull.Value
    '                Else
    '                    If CType(valor, String) = "borrarEntero" Then
    '                        pa = New SqlParameter(name, SqlDbType.Int)
    '                        pa.Value = DBNull.Value
    '                    Else
    '                        pa = New SqlParameter(name, valor)
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    '    Parametros.Add(pa)
    'End Sub
    Public Sub addbyte(ByVal name As String, ByVal valor As Byte())
        Dim pa As New SqlParameter()
        pa = New SqlParameter(name, valor)
        Parametros.Add(pa)
    End Sub
    Public Sub addGuid(ByVal name As String, ByVal valor As Guid)
        Dim pa As New SqlParameter()
        pa = New SqlParameter(name, valor)
        Parametros.Add(pa)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

