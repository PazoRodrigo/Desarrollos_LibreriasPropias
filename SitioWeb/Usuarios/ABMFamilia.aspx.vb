Imports admUsuarios.Entidad

Partial Class Usuarios_ABMFamilia
    Inherits System.Web.UI.Page

    Private Sub Usuarios_ABMArea_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
        End If
        LlenarGrillaFamilias()
    End Sub
    Protected Sub BtnAlta_Click(sender As Object, e As EventArgs) Handles BtnAlta.Click
        Dim a As New Familia
        a.IdUsuarioAlta = 2
        a.Nombre = TxtNombre.Text
        a.Alta()
        LlenarGrillaFamilias()
    End Sub
    Protected Sub BtnBaja_Click(sender As Object, e As EventArgs) Handles BtnBaja.Click
        Dim a As New Familia(ViewState("IdEntidad"))
        a.IdUsuarioBaja = 2
        a.Nombre = TxtNombre.Text
        a.Baja()
        LlenarGrillaFamilias()
    End Sub
    Protected Sub BtnModifica_Click(sender As Object, e As EventArgs) Handles BtnModifica.Click
        Dim a As New Familia(ViewState("IdEntidad"))
        a.IdUsuarioModifica = 2
        a.Nombre = TxtNombre.Text
        a.Modifica()
        LlenarGrillaFamilias()
    End Sub
    Protected Sub GrillaFamilias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrillaFamilias.SelectedIndexChanged
        Dim IdFamilia As Integer = CInt(GrillaFamilias.SelectedDataKey.Item("IdEntidad").ToString)
        Dim objFami As New Familia(IdFamilia)
        LlenarGrillaFormularios(objFami)
    End Sub
    Private Sub LlenarGrillaFamilias()
        Familia.Refresh()
        GrillaFamilias.DataSource = Familia.TraerTodos
        GrillaFamilias.DataBind()
    End Sub
    Private Sub LlenarGrillaFormularios(ObjFamilia As Familia)
        'GrillaFormularios.DataSource = ObjFamilia.ListaFormularios
        GrillaFormularios.DataBind()
    End Sub
End Class
