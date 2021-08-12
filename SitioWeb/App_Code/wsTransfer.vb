Imports Microsoft.VisualBasic


Public Class wsTransfer
#Region " Atributos "
    Private _todoOk As Boolean
    Private _data As Object
    Private _mensaje As String
#End Region
#Region " Propiedades "
    Public Property todoOk() As Boolean
        Get
            Return _todoOk
        End Get
        Set(ByVal value As Boolean)
            _todoOk = value
        End Set
    End Property
    Public Property data() As Object
        Get
            Return _data
        End Get
        Set(ByVal value As Object)
            _data = value
        End Set
    End Property
    Public Property mensaje() As String
        Get
            Return _mensaje
        End Get
        Set(ByVal value As String)
            _mensaje = value
        End Set
    End Property
#End Region
End Class
