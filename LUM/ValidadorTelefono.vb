'Option Explicit On
'Option Strict On

'Imports Connection
'Imports LUM.Entidad

'Public Class ValidadorTelefono
'#Region " Públicos "
'#Region " DDN "
'    Public Shared Sub ValidarDDN(numero As String)
'        Dim tipoNumero As String = "DDN"
'        LimpiarNumero(numero, tipoNumero)
'        ExisteDDN(CInt(numero))
'    End Sub
'#End Region
'#Region " Celulares "
'    Public Shared Sub ValidarCelular(numero As String)
'        Dim tipoNumero As String = "celular"
'        LimpiarNumero(numero, tipoNumero)
'    End Sub
'#End Region
'#Region " Fijos "
'    Public Shared Sub ValidarTelefono(numero As String)
'        Dim tipoNumero As String = "teléfono"
'        LimpiarNumero(numero, tipoNumero)
'    End Sub
'#End Region
'#End Region
'#Region " Privados "
'    Private Shared Sub ExisteDDN(numeroDDN As Integer)
'        Dim listaDDN As List(Of DDN) = DDN.TraerTodos
'        Dim existe As Boolean = False
'        For Each item As DDN In listaDDN
'            If item.interurbano = CInt(numeroDDN) Then
'                existe = True
'            End If
'        Next
'        If Not existe Then
'            Throw New Exception("El DDN " & numeroDDN & " no existe.")
'        End If
'    End Sub
'    Private Shared Sub LimpiarNumero(numero As String, tipoNumero As String)
'        Dim numeroOrig As String = numero
'        numero = numero.Trim
'        numero = Replace(numero, "([^0-9]|[^a-zA-Z]|-)", "")
'        If Not IsNumeric(numero) Then
'            Throw New Exception("El número de " & tipoNumero & ": " & numeroOrig & " debe ser sólo numérico")
'        End If
'    End Sub
'#End Region
'End Class
