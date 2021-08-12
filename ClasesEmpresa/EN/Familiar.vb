Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Familiar
        Inherits Persona

        'Private Shared _Todos As List(Of Familiar)
        'Public Shared Property Todos() As List(Of Familiar)
        '    Get
        '        If _Todos Is Nothing Then
        '            _Todos = DAL_Familiar.TraerTodos
        '        End If
        '        Return _Todos
        '    End Get
        '    Set(ByVal value As List(Of Familiar))
        '        _Todos = value
        '    End Set
        'End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property IdAfiliado() As Integer
        Public Property CorreoElectronico() As String
        Public Property Telefono() As String
        Public Property Celular() As String
#End Region
#Region " Lazy Load "
        Public Property IdParentesco() As Integer
        Private _ObjParentesco As Parentesco
        Public ReadOnly Property ObjLazy() As Parentesco
            Get
                If _ObjParentesco Is Nothing Then
                    _ObjParentesco = Parentesco.TraerUno(IdParentesco)
                End If
                Return _ObjParentesco
            End Get
        End Property
        Public Property IdSeccional() As Integer
        Private _ObjSeccional As Comunes.Entidad.Seccional
        Public ReadOnly Property ObjSeccional() As Comunes.Entidad.Seccional
            Get
                If _ObjSeccional Is Nothing Then
                    _ObjSeccional = Comunes.Entidad.Seccional.TraerUno(IdSeccional)
                End If
                Return _ObjSeccional
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Familiar = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            fechaAlta = objImportar.fechaAlta
            fechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Familiar
        '    Dim result As Familiar = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Familiar)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Familiar
            Dim result As Familiar = DAL_Familiar.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXRepresentado(IdAfiliado As Integer) As List(Of Familiar)
            Dim result As List(Of Familiar) = DAL_Familiar.TraerTodosXRepresentado(IdAfiliado)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Familiar.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Familiar.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Familiar.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Familiar
            Dim result As New DTO.DTO_Familiar
            result.IdEntidad = IdEntidad
            Return result
        End Function
        'Public Shared Sub Refresh()
        '    _Todos = DAL_Familiar.TraerTodos
        'End Sub
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
            'Dim cantidad As Integer = DAL_Familiar.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Familiar
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Familiar

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
#End Region
    End Class ' DTO_Familiar
End Namespace ' DTO