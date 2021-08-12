Imports admUsuarios.Entidad

Partial Class Usuarios_ABMFormulario
    Inherits System.Web.UI.Page

    Private Sub Usuarios_ABMFormulario_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CBL_Permisos.DataSource = Accion.Todos
            CBL_Permisos.DataTextField = "Nombre"
            CBL_Permisos.DataValueField = "IdEntidad"
            CBL_Permisos.DataBind()

            Familia.Refresh()
            CBL_Areas.DataSource = Familia.Todos
            CBL_Areas.DataTextField = "Nombre"
            CBL_Areas.DataValueField = "IdEntidad"
            CBL_Areas.DataBind()
            LlenarGrillaFormularios()
        End If
    End Sub
    Protected Sub BtnAlta_Click(sender As Object, e As EventArgs) Handles BtnAlta.Click
        Dim listaAcciones As List(Of Accion) = TraerAccionesFormulario()
        Dim listaFamilias As List(Of Familia) = TraerFamilias()
        Dim a As New Formulario
        a.IdUsuarioAlta = 2
        a.Nombre = TxtNombre.Text
        'a.ListaAccionesPosibles = listaAcciones
        'a.ListaFamilias = listaFamilias
        a.Alta()
        LlenarGrillaFormularios()
    End Sub
    Protected Sub BtnBaja_Click(sender As Object, e As EventArgs) Handles BtnBaja.Click
        Dim a As New Formulario(ViewState("IdEntidad"))
        a.IdUsuarioBaja = 2
        a.Nombre = TxtNombre.Text
        a.Baja()
        LlenarGrillaFormularios()
    End Sub
    Protected Sub BtnModifica_Click(sender As Object, e As EventArgs) Handles BtnModifica.Click
        Dim a As New Formulario(ViewState("IdEntidad"))
        a.IdUsuarioModifica = 2
        a.Nombre = TxtNombre.Text
        a.Modifica()
        LlenarGrillaFormularios()
    End Sub
    Private Sub LlenarGrillaFormularios()
        Formulario.Refresh()
        GrillaFormularios.DataSource = Formulario.TraerTodos
        GrillaFormularios.DataBind()
    End Sub
    Private Function TraerAccionesFormulario() As List(Of Accion)
        Dim lista As New List(Of Accion)
        For Each item As ListItem In CBL_Permisos.Items
            Dim acc As New Accion
            acc.IdEntidad = item.Value
            acc.Existe = item.Selected
            lista.Add(acc)
        Next
        Return lista
    End Function
    Private Function TraerFamilias() As List(Of Familia)
        Dim lista As New List(Of Familia)
        For Each item As ListItem In CBL_Areas.Items
            Dim ar As New Familia
            If item.Selected Then
                ar.IdEntidad = item.Value
                lista.Add(ar)
            End If
        Next
        Return lista
    End Function
    Protected Sub GrillaFormularios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrillaFormularios.SelectedIndexChanged
        Dim IdFormulario As Integer = CInt(GrillaFormularios.SelectedDataKey.Item("IdEntidad").ToString)
        Dim objForm As New Formulario(IdFormulario)
        LlenarFamilias(objForm)
        Dim l As String = ""
        If objForm.ListaAccionesPosibles IsNot Nothing Then
            For Each item As Accion In objForm.ListaAccionesPosibles
                If item.Existe Then
                    l &= item.Nombre & ","
                End If
            Next
        End If
        Lit_Permisos.Text = l
    End Sub
    Private Sub LlenarFamilias(objForm As Formulario)
        ' GrillaFamilias.DataSource = objForm.ListaFamilias
        GrillaFamilias.DataBind()
    End Sub
End Class
