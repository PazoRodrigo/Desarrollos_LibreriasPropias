Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Familia
        Inherits DBE

        Private Shared _Todos As List(Of Familia)
        Public Shared Property Todos() As List(Of Familia)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Familia.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Familia.TraerTodos
            End Get
            Set(ByVal value As List(Of Familia))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
#End Region
#Region " Lazy Load "
        Private _ListaRamas As List(Of Rama)
        Public ReadOnly Property ListaRamas() As List(Of Rama)
            Get
                If _ListaRamas Is Nothing Then
                    _ListaRamas = Rama.TraerTodosXFamilia(IdEntidad)
                End If
                Return _ListaRamas
                'Return Rama.TraerTodosXFamilia(IdEntidad)
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
            Dim objImportar As Familia = TraerUno(id)
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
        Public Shared Function TraerUno(ByVal Id As Integer) As Familia
            Dim result As Familia = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Familia)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Area
        '    Dim result As Area= DAL_Familia.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Area)
        '    Dim result As List(Of Area) = DAL_Familia.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        ' Otros
        Friend Shared Function TraerTodosXFormulario(IdFormulario As Integer) As List(Of Familia)
            Return DAL_Familia.TraerTodasXFormulario(IdFormulario)
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Familia.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Familia.Baja(Me)
            'Refresh()
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Familia.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Familia
            Dim result As New DTO.DTO_Familia With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Familia.TraerTodos
        End Sub
        ' Nuevos
        Friend Sub AgregarFormulario(IdFormulario As Integer)
            DAL_Familia.Formulario_Agregar(Me, IdFormulario)
        End Sub
        Friend Sub EliminarFormulario(IdFormulario As Integer)
            DAL_Familia.Formulario_Eliminar(Me, IdFormulario)
        End Sub
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
            ValidarRamaActiva()
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
            'Dim cantidad As Integer = DAL_Familia.TraerTodosXDenominacionCant(Me.denominacion)
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
        Private Sub ValidarRamaActiva()
            Dim activo As Boolean = False
            If ListaRamas IsNot Nothing And ListaRamas.Count > 0 Then
                For Each item As Rama In ListaRamas
                    If Not item.FechaBaja.HasValue Then
                        activo = True
                    End If
                Next
            End If
            If activo Then
                Throw New Exception("Existen Ramas Activas")
            End If
        End Sub
#End Region
    End Class ' Area
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Familia

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_Area
End Namespace ' DTO