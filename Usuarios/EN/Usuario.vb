Option Explicit On
Option Strict On

Imports Usuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Usuario
        Inherits DBE
        Private Shared _Todos As List(Of Usuario)
        Public Shared Property Todos() As List(Of Usuario)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Usuario.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Usuario))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property UserLogin() As String
        Public Property UserName() As String
        Public Property PassWord() As String
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
        Sub New(userlogin As String, password As String)
            Dim objImportar As Usuario = TraerUnoPorUserLoginyPassword(userlogin, password)
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            UserName = objImportar.UserName
            userlogin = userlogin
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Usuario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            UserName = objImportar.UserName
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Private Shared Function TraerUno(ByVal Id As Integer) As Usuario
            Dim result As Usuario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Private Shared Function TraerTodos() As List(Of Usuario)
            Return Todos
        End Function
        Public Shared Function TraerUnoPorUserLoginyPassword(ByVal UserLogin As String, Password As String) As Usuario
            Dim result As Usuario = DAL_Usuario.TraerUnoPorUserLoginyPassword(UserLogin, Password)
            If result Is Nothing Then
                Throw New Exception("Usuario Inexistente")
            End If
            Return result
        End Function
        'Public Shared Function TraerTodos() As List(Of Usuario)
        '    Dim result As List(Of Usuario) = DAL_Usuario.TraerTodos()
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
            DAL_Usuario.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Usuario.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Usuario.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Usuario
            Dim result As New DTO.DTO_Usuario
            result.IdEntidad = IdEntidad
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Usuario.TraerTodos
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
            'Dim cantidad As Integer = DAL_Usuario.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Usuario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Usuario

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
#End Region
    End Class ' DTO_Usuario
End Namespace ' DTO

