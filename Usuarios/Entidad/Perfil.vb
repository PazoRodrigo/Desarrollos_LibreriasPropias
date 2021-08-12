Option Explicit On
Option Strict On

Imports Usuarios.DataAccessLibrary

Namespace Entidad
    Public Class Perfil
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Perfil)
        Public Shared Property Todos() As List(Of Perfil)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Perfil.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Perfil))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
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
            Dim objImportar As Perfil = TraerUno(id)
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
        Public Shared Function TraerUno(ByVal Id As Integer) As Perfil
            Dim result As Perfil = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Perfil)
            Return Todos
        End Function
#End Region
#Region " Métodos Públicos"
        Public Function ToDTO() As DTO.DTO_Perfil
            Dim result As New DTO.DTO_Perfil With {
                .IdEntidad = IdEntidad
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Perfil.TraerTodos
        End Sub
#End Region
#Region " Casos de Uso "
        ''' <summary>
        ''' CU.Perfil.01
        ''' </summary>
        Public Sub CreandoPerfil()
            'If Usuario.UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Creación de Perfiles solo puede ser realizado por el Perfil Administrador")
            'End If

            'Alta()
        End Sub
        ''' <summary>
        ''' CU.Perfil.02
        ''' </summary>
        Public Sub ModificandoPerfil()
            'If Usuario.UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Modificación Perfiles solo puede ser realizado por el Perfil Administrador")
            'End If
            'Dim usu As New Perfil(IdEntidad) With {
            '    .Nombre = Nombre,
            '    .FechaAlta = FechaAlta,
            '    .FechaBaja = FechaBaja
            '}
            'usu.Modifica()
        End Sub
        ''' <summary>
        ''' CU.Perfil.03
        ''' </summary>
        Public Sub EliminandoPerfil()
            'If Usuarios.Entidad.Usuario.UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("La Eliminación de Perfiles solo puede ser realizado por el Perfil Administrador")
            'End If
            'Baja()
        End Sub
        ''' <summary>
        ''' CU.v.05
        ''' </summary>
        Public Shared Function BuscandoPerfilesPorUsuario(IdUsuario As Integer) As List(Of Perfil)
            'DAL_Perfil.TraerTodosXUsuario(IdUsuario)
            Return Nothing
        End Function
        ''' <summary>
        ''' CU.Perfil.06
        ''' </summary>
        Private Shared Sub AsignandoPerfilAUsuario(ByVal IdUsuario As Integer)
            'If Usuarios.Entidad.Usuario.UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("Asignar Perfiles a Usuarios solo puede ser realizado por el Perfil Administrador")
            'End If
            'If UsuarioLogueado() IsNot Nothing Then
            '    Throw New Exception("Debe Salir del Sistema")
            'End If
            '_UsuarioLogueado = DAL_Usuario.TraerUno(UserLogin, Password)
        End Sub
        ''' <summary>
        ''' CU.Perfil.07
        ''' </summary>
        Public Sub QuitandoPerfilAUsuario(ByVal IdUsuario As Integer)
            'If Usuarios.Entidad.Usuario.UsuarioLogueado.IdPerfil <> 1 Then
            '    Throw New Exception("Quitar Perfiles a Usuarios solo puede ser realizado por el Perfil Administrador")
            'End If
            'usu.Modifica()
        End Sub
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub Alta()
            ValidarAlta()
            DAL_Perfil.Alta(Me)
        End Sub
        Private Sub Baja()
            ValidarBaja()
            DAL_Perfil.Baja(Me)
        End Sub
        Private Sub Modifica()
            ValidarModifica()
            DAL_Perfil.Modifica(Me)
        End Sub

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
    End Class ' Perfil
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Perfil

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
#End Region
    End Class ' DTO_Perfil
End Namespace ' DTO