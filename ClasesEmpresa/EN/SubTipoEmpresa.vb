Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class SubTipoEmpresa
        Inherits DBE

        Private Shared _Todos As List(Of SubTipoEmpresa)
        Public Shared Property Todos() As List(Of SubTipoEmpresa)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_SubTipoEmpresa.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of SubTipoEmpresa))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property IdTipoEmpresa() As Integer
        Public Property Nombre() As String
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
            Dim objImportar As SubTipoEmpresa = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdTipoEmpresa = objImportar.IdTipoEmpresa
            Nombre = objImportar.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As SubTipoEmpresa
            Dim result As SubTipoEmpresa = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen Sub Tipo Empresa para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of SubTipoEmpresa)
            Return Todos
        End Function
        Public Shared Function TraerTodosXTipo(IdTipo As Integer) As List(Of SubTipoEmpresa)
            Dim result As List(Of SubTipoEmpresa) = Todos.FindAll(Function(x) x.IdTipoEmpresa = IdTipo)
            If result Is Nothing Then
                Throw New Exception("No existen Sub Tipo Empresa para ese Tipo")
            End If
            Return result
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As SubTipoEmpresa
        '    Dim result As SubTipoEmpresa= DAL_SubTipoEmpresa.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of SubTipoEmpresa)
        '    Dim result As List(Of SubTipoEmpresa) = DAL_SubTipoEmpresa.TraerTodos()
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
            DAL_SubTipoEmpresa.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_SubTipoEmpresa.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_SubTipoEmpresa.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_SubTipoEmpresa
            Dim result As New DTO.DTO_SubTipoEmpresa With {
                .IdEntidad = IdEntidad,
                .IdTipoEmpresa = IdTipoEmpresa,
                .Nombre = Nombre
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_SubTipoEmpresa.TraerTodos
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
        Private Sub ValidarCaracteres()
            Dim sError As String = ""
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_SubTipoEmpresa.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' SubTipoEmpresa
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_SubTipoEmpresa

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property IdTipoEmpresa() As Integer
        Public Property Nombre() As String
#End Region
    End Class ' DTO_SubTipoEmpresa
End Namespace ' DTO