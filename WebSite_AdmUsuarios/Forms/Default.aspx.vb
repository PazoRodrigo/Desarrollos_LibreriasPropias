
Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim lacce As New List(Of admUsuarios.Entidad.Acceso)
            admUsuarios.Entidad.Accion.refresh()
            admUsuarios.Entidad.Familia.Refresh()
            admUsuarios.Entidad.Area.Refresh()
            admUsuarios.Entidad.Rol.Refresh()
            admUsuarios.Entidad.Rama.Refresh()
            admUsuarios.Entidad.Formulario.Refresh()
            admUsuarios.Entidad.Perfil.Refresh()
            admUsuarios.Entidad.Usuario.Refresh()

            'Dim Usuario As New admUsuarios.Entidad.Usuario
            'Usuario = admUsuarios.Entidad.Usuario.HacerLogIN("usrTurismo01", "s")
            'Dim lP As List(Of admUsuarios.Entidad.Perfil) = Usuario.ListaPerfiles
            'Dim lR As List(Of admUsuarios.Entidad.Rol) = Usuario.ListaRolesInicio
            'Dim b As New admUsuarios.Entidad.Usuario
        End If
    End Sub
End Class
