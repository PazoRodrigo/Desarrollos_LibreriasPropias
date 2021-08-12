Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports LUM

Partial Class admUsuarios_ABMUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LimpiarEntidad()
            LimpiarGrilla()
            ViewState("idEntidad") = 0
        End If
    End Sub

#Region " Entidad "
    Private Sub LimpiarEntidad()
        Throw New NotImplementedException
    End Sub
    Private Sub LlenarEntidad()
        Throw New NotImplementedException
    End Sub
#End Region
#Region " Grilla "
    Private Sub LimpiarGrilla()
        Throw New NotImplementedException
    End Sub
    Private Sub LlenarGrilla()
        Throw New NotImplementedException
    End Sub
    Protected Sub grillaEntidades_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grillaEntidades.SelectedIndexChanged

    End Sub
#End Region
#Region " Botones "
    Protected Sub btnAlta_Click(sender As Object, e As System.EventArgs) Handles btnAlta.Click

    End Sub
    Protected Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

    End Sub
    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click

    End Sub
#End Region
End Class
