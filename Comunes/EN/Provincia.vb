Option Explicit On
Option Strict On

Imports Comunes.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Provincia

        Private Shared _Todos As List(Of Provincia)
        Public Shared Property Todos() As List(Of Provincia)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Provincia.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Provincia))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()
            IdEntidad = 0
            Nombre = ""
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Provincia = TraerUno(id)
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Provincia
            Dim result As Provincia = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen Provincias para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Provincia)
            Return Todos
        End Function
        Public Shared Function TraerTodosXNombre(ByVal Nombre As String) As List(Of Provincia)
            Dim result As List(Of Provincia) = Todos.FindAll(Function(x) x.Nombre.Contains(Nombre.Trim.ToUpper))
            If result Is Nothing Then
                Throw New Exception("No existen Provincias para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Provincia.TraerTodos
        End Sub
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_Provincia
            Dim result As New DTO.DTO_Provincia With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre
            }
            Return result
        End Function
#End Region
    End Class ' Provincia
End Namespace ' Entidad
Namespace DTO
    Public Class DTO_Provincia

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
    End Class ' DTO_Provincia
End Namespace ' DTO