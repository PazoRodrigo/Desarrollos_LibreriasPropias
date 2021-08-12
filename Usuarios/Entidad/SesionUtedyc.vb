Option Explicit On
Option Strict On

Imports Usuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class SesionUtedyc
        Inherits DBE

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Usuario As Usuarios.Entidad.Usuario
        Public Token As String
        Public IdRol As Integer = 0
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(UserLogin As String, Password As String)
            ' DBE  
            HaciendoLogIN(UserLogin, Password)
            IdUsuarioAlta = UsuarioLogueado.IdUsuarioAlta
            IdUsuarioBaja = UsuarioLogueado.IdUsuarioBaja
            IdMotivoBaja = UsuarioLogueado.IdMotivoBaja
            FechaAlta = UsuarioLogueado.FechaAlta
            FechaBaja = UsuarioLogueado.FechaBaja
            ' Entidad
            'IdEntidad = UsuarioLogueado.IdEntidad
            'UserLogin = UsuarioLogueado.UserLogin
            'IdEntidad = objImportar.IdEntidad
            Usuario = _UsuarioLogueado
        End Sub
#End Region
#Region " Métodos Estáticos"
        Private Shared _UsuarioLogueado As Usuario
        Public Shared Function UsuarioLogueado() As Usuario
            If (_UsuarioLogueado Is Nothing) Then
                _UsuarioLogueado = New Usuario()
            End If
            Return _UsuarioLogueado
        End Function
        Private Shared Sub HaciendoLogIN(ByVal UserLogin As String, ByVal Password As String)
            'If UsuarioLogueado() IsNot Nothing Then
            '    Throw New Exception("Debe Salir del Sistema")
            'End If

            _UsuarioLogueado = DAL_Usuario.TraerUno(UserLogin, Password)
        End Sub
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As SesionUtedyc
        '    Dim result As SesionUtedyc = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of SesionUtedyc)
        '    Return Todos
        'End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As SesionUtedyc
        '    Dim result As SesionUtedyc= DAL_SesionUtedyc.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of SesionUtedyc)
        '    Dim result As List(Of SesionUtedyc) = DAL_SesionUtedyc.TraerTodos()
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
            DAL_SesionUtedyc.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_SesionUtedyc.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_SesionUtedyc.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_SesionUtedyc
            Dim result As New DTO.DTO_SesionUtedyc
            result.IdEntidad = IdEntidad
            Return result
        End Function
        'Public Shared Sub refresh()
        '    _Todos = DAL_SesionUtedyc.TraerTodos
        'End Sub
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
            'Dim cantidad As Integer = DAL_SesionUtedyc.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' SesionUtedyc
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_SesionUtedyc

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
#End Region
    End Class ' DTO_SesionUtedyc
End Namespace ' DTO