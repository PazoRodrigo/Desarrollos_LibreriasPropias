Imports Microsoft.VisualBasic


Public Class wsTransferObj
    Public Property TodoOk() As Boolean
    Public Property Data As Pagina
    Public Property Mensaje() As String

    Public Sub New()
        TodoOk = True
        Data = Nothing
        Mensaje = ""
    End Sub
End Class
Public Class Pagina
    Public Property Datos As Object
    Public Property Cantidad As Integer
    Public Sub New(_Datos As Object, _Cantidad As Integer)
        Datos = _Datos
        Cantidad = _Cantidad
    End Sub
    Public Sub New(_Datos As Object)
        Datos = _Datos
        Cantidad = -1
    End Sub
End Class
