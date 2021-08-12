Imports Comunes.Entidad
Partial Class Refresh
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblSeccionales.Text = Provincia.Todos.Count.ToString
        End If
    End Sub


End Class
