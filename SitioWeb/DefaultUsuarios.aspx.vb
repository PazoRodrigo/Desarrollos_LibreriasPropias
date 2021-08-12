Option Explicit On
Option Strict On

'Imports Usuarios.Entidad

Partial Class DefaultUsuarios
    Inherits System.Web.UI.Page

    Protected Sub BtnCreandoUsuario_Click(sender As Object, e As EventArgs) Handles BtnCreandoUsuario.Click
        Try
            'Dim us As New Usuario()
            'us.Nombre = TxtNombre.Text '"Rodrigo Pazo"
            'us.UserLogin = txtLogIN.Text '"pazo.rodrigo"
            'us.Password = txtPass.Text '"Rodrigo1976"
            'us.FechaAlta = Today()
            'us.FechaBaja = CType("2020-01-01", Date?)
            'us.IdUsuarioAlta = 1
            'us.CreandoUsuario()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub DefaultUsuarios_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
        End If
    End Sub
End Class
