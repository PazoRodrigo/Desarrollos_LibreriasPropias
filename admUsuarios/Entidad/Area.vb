Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports Newtonsoft.Json

Namespace Entidad
    Public Class Area
        Inherits LUM.DBE
        Private Shared _Todos As List(Of Area)
        Public Shared Property Todos() As List(Of Area)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Area.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Area.TraerTodos
            End Get
            Set(ByVal value As List(Of Area))
                _Todos = value
            End Set
        End Property
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
#Region " Lazy Load "
        'Private _ListaUsuarios As List(Of Usuario)
        Public ReadOnly Property ListaUsuarios() As List(Of Usuario)
            Get
                Return Usuario.TraerTodosXArea(IdEntidad)
                'If _ListaUsuarios Is Nothing Then
                '    _ListaUsuarios = Usuario.TraerTodosXArea(IdEntidad)
                'End If
                'Return _ListaUsuarios
            End Get
        End Property
        Public ReadOnly Property IdEstado() As Integer
            Get
                Dim result As Integer = 0
                If FechaBaja.HasValue Then
                    result = 1
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Area = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Area
            Dim result As Area = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Area)
            Return Todos
        End Function
        Friend Shared Function TraerTodosXUsuario(idEntidad As Integer) As List(Of Area)
            Return DAL_Area.TraerTodosXUsuario(idEntidad)
        End Function


        'Public Shared Function TraerUno(ByVal Id As Integer) As Area
        '    Dim result As Area= DAL_Area.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Area)
        '    Dim result As List(Of Area) = DAL_Area.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        Private Structure SqlJsonStr
            Property IdArea As Integer
            Property Nombre As String
        End Structure
        Public Function ToSQLJson() As String
            Dim Per As New SqlJsonStr With {
                .IdArea = IdEntidad,
                .Nombre = Nombre
            }
            Return JsonConvert.SerializeObject(Per)
        End Function
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Area.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Area.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Area.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Area
            Dim result As New DTO.DTO_Area With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Area.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        ' Validaciones
        Private Sub ValidarUsuario(ByVal idUsuario As Integer)
            If idUsuario = 0 Then
                Throw New Exception("Debe ingresar al sistema")
            End If
        End Sub
        Private Sub ValidarCampos()
            Dim sError As String = ""
            ValidarCaracteres()
            If Me.Nombre = "" Then
                sError &= "<b>Nombre</b> Debe ingresar la Nombre. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarCaracteres()
            Dim sError As String = ""
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Area.TraerTodosXDenominacionCant(Me.denominacion)
            'If Me.idEntidad = 0 Then
            '    ' Alta
            '    If cantidad > 0 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'Else
            '    ' Modifica
            '    If cantidad > 1 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'End If
        End Sub
#End Region
    End Class ' Area
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Area

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String = ""
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_Area
End Namespace ' DTO