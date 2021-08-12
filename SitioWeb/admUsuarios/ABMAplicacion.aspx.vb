Option Explicit On
Option Strict On

Imports admUsuarios.Entidad
Imports LUM

Partial Class admUsuarios_ABMAplicacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LimpiarEntidad()
            LlenarGrilla()
            ViewState("idEntidad") = 0
        End If
    End Sub

#Region " Entidad "
    Private Sub LimpiarEntidad()
        txtId.Text = ""
        txtNivelAcceso.Text = ""
        txtNombre.Text = ""
        txtBaseDeDatos.Text = ""
        txtConexion.Text = ""
    End Sub
    Private Sub LlenarEntidad()
        'Dim objetoImp As New Aplicacion(CInt(ViewState("idEntidad")))
        'txtId.Text = CStr(objetoImp.idEntidad)
        'txtNivelAcceso.Text = CStr(objetoImp.nivelAcceso)
        'txtNombre.Text = objetoImp.nombre
        'txtBaseDeDatos.Text = objetoImp.baseDeDatos
        'txtConexion.Text = objetoImp.conexion
    End Sub
#End Region
#Region " Grilla "
    Private Sub LimpiarGrilla()
        grillaEntidades.DataSource = Nothing
        grillaEntidades.DataBind()
    End Sub
    Private Sub LlenarGrilla()
        'grillaEntidades.DataSource = Aplicacion.TraerTodos
        'grillaEntidades.DataBind()
    End Sub
    Protected Sub grillaEntidades_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grillaEntidades.SelectedIndexChanged
        ViewState("idEntidad") = grillaEntidades.SelectedDataKey("idEntidad")
        LlenarEntidad()
    End Sub
#End Region
#Region " Botones "
    Protected Sub btnAlta_Click(sender As Object, e As System.EventArgs) Handles btnAlta.Click
        'Dim obj As New Aplicacion
        'obj.nivelAcceso = CInt(txtNivelAcceso.Text)
        'obj.nombre = txtNombre.Text
        'obj.baseDeDatos = txtBaseDeDatos.Text
        'obj.conexion = txtConexion.Text
        'obj.Alta()
    End Sub
    Protected Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click
        'Dim obj As New Aplicacion(CInt(ViewState("idEntidad")))
        'obj.Baja()
    End Sub
    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        'Dim obj As New Aplicacion(CInt(ViewState("idEntidad")))
        'obj.nivelAcceso = CInt(txtNivelAcceso.Text)
        'obj.nombre = txtNombre.Text
        'obj.baseDeDatos = txtBaseDeDatos.Text
        'obj.conexion = txtConexion.Text
        'obj.Modifica()
    End Sub
#End Region
End Class

