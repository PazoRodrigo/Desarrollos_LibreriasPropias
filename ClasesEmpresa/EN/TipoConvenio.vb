Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class TipoConvenio
        Inherits DBE

        Private Shared _Todos As List(Of TipoConvenio)
        Public Shared Property Todos() As List(Of TipoConvenio)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_TipoConvenio.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of TipoConvenio))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
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
            Dim objImportar As TipoConvenio = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            fechaAlta = objImportar.fechaAlta
            fechaBaja = objImportar.FechaBaja
            'fechaModifica = objImportar.fechaModifica
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As TipoConvenio
            Dim result As TipoConvenio = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of TipoConvenio)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As TipoConvenio
        '    Dim result As TipoConvenio= DAL_TipoConvenio.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of TipoConvenio)
        '    Dim result As List(Of TipoConvenio) = DAL_TipoConvenio.TraerTodos()
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
            DAL_TipoConvenio.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_TipoConvenio.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_TipoConvenio.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_TipoConvenio
            Dim result As New DTO.DTO_TipoConvenio
            result.IdEntidad = IdEntidad
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_TipoConvenio.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.idUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.idUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.idUsuarioModifica)
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
            'Dim cantidad As Integer = DAL_TipoConvenio.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' TipoConvenio
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_TipoConvenio

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
#End Region
    End Class ' DTO_TipoConvenio
End Namespace ' DTO

