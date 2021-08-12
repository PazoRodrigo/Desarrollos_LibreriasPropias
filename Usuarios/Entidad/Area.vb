'Public Class Area
'    Public Property Id_Area() As Integer
'    Public Property Descripcion() As String
'End Class

Option Explicit On
Option Strict On

Imports Usuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Area
        Inherits DBE

        Private Shared _Todos As List(Of Area)
        Public Shared Property Todos() As List(Of Area)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Area.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Area))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Descripcion() As String = ""
        Public Property Alcance() As Integer = 0

#End Region
#Region " Lazy Load "
        'Public Property IdLazy() As Integer
        'Private _ObjLazy As Lazy
        'Public ReadOnly Property ObjLazy() As Lazy
        '    Get
        '        If _ObjLazy Is Nothing Then
        '            _ObjLazy = Lazy.TraerUno(IdLazy)
        '        End If
        '        Return _ObjLazy
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Area = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Descripcion = objImportar.Descripcion
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
        Public Shared Function TraerTodosXAlcance(ByVal Alcance As Integer) As List(Of Area)
            Dim result As List(Of Area) = Todos.FindAll(Function(x) x.Alcance = Alcance)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXIdUsuario(ByVal Id As Integer) As List(Of Area)
            Dim result As List(Of Area) = DAL_Area.TraerTodosXIdUsuario(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerAreaOrigenXIdTicket(ByVal IdTicket As Integer) As Area
            Dim result As Area = DAL_Area.TraerAreaOrigenXIdTicket(IdTicket)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerAreaDestinoXIdTicket(ByVal IdTicket As Integer) As Area
            Dim result As Area = DAL_Area.TraerAreaDestinoXIdTicket(IdTicket)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
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
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Area.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Area.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Area.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Area
            Dim result As New DTO.DTO_Area With {
                .IdEntidad = IdEntidad,
                .Descripcion = Descripcion,
                .Alcance = Alcance
            }
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Area.TraerTodos()
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
            ' Campo Integer/Decimal
            '	If Me.VariableNumero.toString = "" Then
            '   	sError &= "<b>VariableNumero</b> Debe ingresar VariableNumero. <br />"
            '	ElseIf Not isnumeric(Me.VariableNumero) Then
            '   	sError &= "<b>VariableNumero</b> Debe ser numérico. <br />"
            '	End If

            ' Campo String
            '	If Me.VariableString = "" Then
            '		sError &= "<b>VariableString</b> Debe ingresar VariableString. <br />"
            '	ElseIf Me.apellido.Length > 50 Then
            '		sError &= "<b>VariableString</b> El campo debe tener como máximo 50 caracteres. <br />"
            '	End If

            ' Campo Date
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Perfil.TraerTodosXDenominacionCant(Me.denominacion)
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
        Public Property IdEntidad() As Integer = 0
        Public Property Descripcion() As String = ""
        Public Property Alcance() As Integer = 0
#End Region
    End Class ' DTO_Area
End Namespace ' DTO