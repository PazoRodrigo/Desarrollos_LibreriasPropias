Option Explicit On
Option Strict On

Imports Usuarios.DataAccessLibrary

Namespace Entidad
    Public Class Usuario
        Inherits LUM.DBE

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
        'Private Shared _UsuarioLogueado As Usuario
        'Public Shared Function UsuarioLogueado() As Usuario
        '    If (_UsuarioLogueado Is Nothing) Then
        '        _UsuarioLogueado = New Usuario()
        '    End If
        '    Return _UsuarioLogueado
        'End Function
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property UserLogin() As String = ""
        Public Property Password() As String = ""
        Public Property Nombre() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property Perfiles() As List(Of Perfil) = Nothing
        Public Property Roles As List(Of Rol) = Nothing
        Public Property Areas As List(Of Area) = Nothing
#End Region
#Region " Lazy Load "
        'Private _ListaAreas As List(Of Area)
        'Public ReadOnly Property ListaAreas() As List(Of Area)
        '    Get
        '        If _ListaAreas Is Nothing Then
        '            '_ListaAreas = Area.TraerTodasXUsuario(IdEntidad)
        '        End If
        '        Return _ListaAreas
        '    End Get
        'End Property
        'Private _ListaRoles As List(Of Rol)
        'Public ReadOnly Property ListaRoles() As List(Of Rol)
        '    Get
        '        If _ListaRoles Is Nothing Then
        '            '_ListaRoles = Rol.TraerTodasXUsuario(IdEntidad)
        '        End If
        '        Return _ListaRoles
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        'Sub New(UserLogin As String, Password As String)
        '    ' DBE  
        '    HaciendoLogIN(UserLogin, Password)
        '    IdUsuarioAlta = UsuarioLogueado.IdUsuarioAlta
        '    IdUsuarioBaja = UsuarioLogueado.IdUsuarioBaja
        '    IdMotivoBaja = UsuarioLogueado.IdMotivoBaja
        '    FechaAlta = UsuarioLogueado.FechaAlta
        '    FechaBaja = UsuarioLogueado.FechaBaja
        '    ' Entidad
        '    IdEntidad = UsuarioLogueado.IdEntidad
        '    UserLogin = UsuarioLogueado.UserLogin
        '    Nombre = UsuarioLogueado.Nombre
        '    CorreoElectronico = UsuarioLogueado.CorreoElectronico
        '    Perfiles = UsuarioLogueado.Perfiles
        '    Roles = UsuarioLogueado.Roles
        '    Areas = UsuarioLogueado.Areas
        'End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Usuario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            UserLogin = objImportar.UserLogin
            Nombre = objImportar.Nombre
            CorreoElectronico = objImportar.CorreoElectronico
            Perfiles = objImportar.Perfiles
            Roles = objImportar.Roles
            Areas = objImportar.Areas
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Usuario
            Dim result As Usuario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXArea(ByVal IdArea As Integer) As List(Of Usuario)
            Dim result As List(Of Usuario) = DAL_Usuario.TraerTodosXArea(IdArea)
            'If result Is Nothing Then
            '    Throw New Exception("No existen resultados para la búsqueda")
            'End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Usuario)
            Return Todos
        End Function
#End Region
#Region " Métodos Públicos "
        ' Otros
        Public Function ToDTO() As DTO.DTO_Usuario
            Dim result As New DTO.DTO_Usuario With {
                .IdEntidad = IdEntidad,
                .UserLogin = UserLogin,
                .Password = Password,
                .Nombre = Nombre,
                .CorreoElectronico = CorreoElectronico
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Usuario.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Casos de Uso "
        ' ABM
        ''' <summary>
        ''' CU.Usuario.01
        ''' </summary>
        Public Sub CreandoUsuario()
            'If UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Creación de Usuarios solo puede ser realizado por el Perfil Administrador")
            'End If
            Alta()
        End Sub
        ''' <summary>
        ''' CU.Usuario.02
        ''' </summary>
        Public Sub ModificandoDatosBasicos()
            'If UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Modificación de Datos Básicos solo puede ser realizado por el Perfil Administrador")
            'End If
            Dim usu As New Usuario(IdEntidad)
            usu.Nombre = Nombre
            usu.UserLogin = UserLogin
            usu.FechaAlta = FechaAlta
            usu.FechaBaja = FechaBaja
            usu.Modifica()
        End Sub
        ''' <summary>
        ''' CU.Usuario.03
        ''' </summary>
        Public Sub EliminandoUsuario()
            'If UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Eliminación de Usuarios solo puede ser realizado por el Perfil Administrador")
            'End If
            Baja()
        End Sub
        ''' <summary>
        ''' CU.Usuario.05
        ''' </summary>
        Public Sub ResetearPassword()
        End Sub


        'Private Shared Sub HaciendoLogIN(ByVal UserLogin As String, ByVal Password As String)
        '    'If UsuarioLogueado() IsNot Nothing Then
        '    '    Throw New Exception("Debe Salir del Sistema")
        '    'End If
        '    _UsuarioLogueado = DAL_Usuario.TraerUno(UserLogin, Password)
        'End Sub
        ''' <summary>
        ''' CU.Usuario.07
        ''' </summary>
        Public Sub ModificandoDatos()
            Dim usu As New Usuario(IdEntidad)
            usu.CorreoElectronico = CorreoElectronico
            usu.Modifica()
        End Sub
        ''' <summary>
        ''' CU.Usuario.08
        ''' </summary>
        Public Sub ModificandoPassword()
            Dim usu As New Usuario(IdEntidad)
            usu.Password = Password
            usu.Modifica()
        End Sub
        ''' <summary>
        ''' CU.Usuario.09
        ''' </summary>
        Public Sub VisualizandoMenu()

        End Sub
        ''' <summary>
        ''' CU.Usuario.10
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub HaciendoLogOUT()
            'If UsuarioLogueado() IsNot Nothing Then
            '    _UsuarioLogueado = Nothing
            'End If
        End Sub
        ''' <summary>
        ''' CU.Usuario.11
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub IdentificandoPerfilRolAreaEnUsuario(IdPerfil As Integer, IdRol As Integer, IdArea As Integer)
            'UsuarioLogueado.IdPerfil = Me.IdPerfil
            'UsuarioLogueado.IdRol = Me.IdRol
            'UsuarioLogueado.IdArea = Me.IdArea
        End Sub
        ''' <summary>
        ''' CU.Usuario.12
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub GenerandoPassword(IdPerfil As Integer, IdRol As Integer, IdArea As Integer)
            Dim usu As New Usuario(IdEntidad)
            usu.Password = Password
            usu.Modifica()
        End Sub
#End Region
#Region " Métodos Privados "
        Private Sub Alta()
            ValidarAlta()
            DAL_Usuario.Alta(Me)
        End Sub
        Private Sub Modifica()
            ValidarModifica()
            DAL_Usuario.Modifica(Me)
        End Sub
        Private Sub Baja()
            ValidarBaja()
            DAL_Usuario.Modifica(Me)
            'DAL_Usuario.Baja(Me)
        End Sub
        ' ABM
        Private Sub ValidarAlta()
            'ValidarUsuarioAdministrador(Me.IdUsuarioAlta)
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
            Me.Nombre = Me.Nombre.Trim
            Me.UserLogin = Me.UserLogin.Trim
            Me.Password = Me.Password.Trim
            If Me.Nombre = "" Then
                sError &= "<b>Nombre</b> Debe ingresar el Nombre. <br />"
            ElseIf Me.Nombre.Length < 10 Or Me.Nombre.Length > 50 Then
                If Me.Nombre.Length < 10 Then
                    sError &= "<b>Nombre</b> Debe tener como mínimo 10 caracteres. <br />"
                ElseIf Me.Nombre.Length > 50 Then
                    sError &= "<b>Nombre</b> Debe tener como máximo 50 caracteres. <br />"
                End If
            End If
            If Me.UserLogin = "" Then
                sError &= "<b>Nombre de Usuario</b> Debe ingresar el Nombre. <br />"
            ElseIf Me.UserLogin.Length < 6 Or Me.UserLogin.Length > 20 Then
                If Me.UserLogin.Length < 6 Then
                    sError &= "<b>Nombre de Usuario</b> Debe tener como mínimo 6 caracteres. <br />"
                ElseIf Me.UserLogin.Length > 20 Then
                    sError &= "<b>Nombre de Usuario</b> Debe tener como máximo 20 caracteres. <br />"
                End If
            End If
            If Me.Password = "" Then
                sError &= "<b>Contraseñao</b> Debe ingresar el Nombre. <br />"
            ElseIf Me.Password.Length < 5 Or Me.Password.Length > 10 Then
                If Me.Password.Length < 5 Then
                    sError &= "<b>Contraseña</b> Debe tener como mínimo 5 caracteres. <br />"
                ElseIf Me.Password.Length > 10 Then
                    sError &= "<b>Contraseña</b> Debe tener como máximo 10 caracteres. <br />"
                End If
            End If
            If Not Me.FechaAlta.HasValue Then
                sError &= "<b>Fecha Alta</b> Debe ingresar la Fecha de Alta. <br />"
            End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            Dim result As List(Of Usuario) = Todos.FindAll(Function(x) x.UserLogin = UserLogin And x.IdEntidad <> IdEntidad)
            If result.Count > 0 Then
                Throw New Exception("El Nombre de Usuario ya Existe, ingrese otro")
            End If
        End Sub
#End Region
    End Class ' Usuario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Usuario
#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property UserLogin() As String = ""
        Public Property Password() As String = ""
        Public Property Nombre() As String = ""
        Public Property CorreoElectronico() As String = ""
#End Region
    End Class ' DTO_Usuario
End Namespace ' DTO