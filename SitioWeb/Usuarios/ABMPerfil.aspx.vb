Imports admUsuarios.Entidad

Partial Class Usuarios_ABMPerfil
    Inherits System.Web.UI.Page

    Private Sub Usuarios_ABMPerfil_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenarForms()
            LlenarRoles()
            LlenarPerfiles()
        End If
    End Sub
    Private Sub LlenarForms()
        Familia.Refresh()
        Formulario.Refresh()
        'Acceso.Refresh()
        Dim listaFam As List(Of Familia) = Familia.TraerTodos
        Dim listaFrms As New List(Of Formulario)
        Dim listaResult As New List(Of Formulario)
        'If listaFam IsNot Nothing AndAlso listaFam.Count > 0 Then
        '    For Each itemFam As Familia In listaFam
        '        listaFrms = itemFam.ListaFormularios
        '        If listaFrms IsNot Nothing AndAlso listaFrms.Count > 0 Then
        '            For Each itemFrm As Formulario In listaFrms
        '                itemFrm.IdFamilia = itemFam.IdEntidad
        '                listaResult.Add(itemFrm)
        '            Next
        '        End If
        '    Next
        'End If
        GrillaFormularios.DataSource = listaResult
        GrillaFormularios.DataBind()
    End Sub
    Private Sub LlenarPerfiles()
        Perfil.Refresh()
        GrillaPerfiles.DataSource = Perfil.TraerTodos
        GrillaPerfiles.DataBind()
    End Sub
    Private Sub LlenarFormularios(objFam As Familia)
        'GrillaFormularios.DataSource = objFam.ListaFormularios
        GrillaFormularios.DataBind()
    End Sub
    Private Sub GrillaFormularios_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrillaFormularios.RowDataBound
        Dim form As Formulario
        Dim lista As List(Of Accion)
        If e.Row.RowType = DataControlRowType.DataRow Then
            form = New Formulario(CInt(e.Row.Cells(1).Text))
            lista = form.ListaAccionesPosibles
            For Each item As Accion In lista
                Select Case item.IdEntidad
                    Case admUsuarios.Enumeradores.Accion.Visualizar
                        TryCast(e.Row.FindControl("CB_Visualizar"), CheckBox).Visible = item.Existe
                    Case admUsuarios.Enumeradores.Accion.Agregar
                        TryCast(e.Row.FindControl("CB_Agregar"), CheckBox).Visible = item.Existe
                    Case admUsuarios.Enumeradores.Accion.Modificar
                        TryCast(e.Row.FindControl("CB_Modificar"), CheckBox).Visible = item.Existe
                    Case admUsuarios.Enumeradores.Accion.Eliminar
                        TryCast(e.Row.FindControl("CB_Eliminar"), CheckBox).Visible = item.Existe
                    Case Else
                End Select
            Next
        End If
    End Sub
    Private Sub LlenarRoles()
        Rol.Refresh()
        CBL_Roles.DataSource = Rol.Todos
        CBL_Roles.DataTextField = "Nombre"
        CBL_Roles.DataValueField = "IdEntidad"
        CBL_Roles.DataBind()
    End Sub
    Protected Sub BtnAlta_Click(sender As Object, e As EventArgs) Handles BtnAlta.Click
        Dim listaAcc As List(Of Acceso) = AlmacenarLinea()
        Dim listaRol As List(Of Rol) = AlmacenarRoles()
        Dim a As New Perfil
        a.IdUsuarioAlta = 2
        a.ListaAccesos = listaAcc
        a.ListaRoles = listaRol
        a.Nombre = TxtNombre.Text
        'a.Alta()
    End Sub
    Private Function AlmacenarRoles() As List(Of Rol)
        Dim listaRes As New List(Of Rol)
        Dim objRol As Rol
        For Each item As ListItem In CBL_Roles.Items
            If item.Selected Then
                objRol = New Rol
                objRol.IdEntidad = item.Value
                listaRes.Add(objRol)
            End If
        Next
        Return listaRes
    End Function
    Private Function AlmacenarLinea() As List(Of Acceso)
        Dim listaResult As New List(Of Acceso)
        Dim listaAcc As List(Of Accion)
        Dim objAcceso As Acceso
        Dim acc As Accion
        For Each dr As GridViewRow In GrillaFormularios.Rows
            objAcceso = New Acceso
            Dim IdForm As Integer = CInt(GrillaFormularios.DataKeys(dr.RowIndex)("IdEntidad"))
            objAcceso.IdFormulario = IdForm
            objAcceso.IdFamilia = CInt(GrillaFormularios.DataKeys(dr.RowIndex)("IdFamilia"))
            listaAcc = New List(Of Accion)
            Dim chkV As CheckBox = DirectCast(dr.FindControl("CB_Visualizar"), CheckBox)
            Dim chkA As CheckBox = DirectCast(dr.FindControl("CB_Agregar"), CheckBox)
            Dim chkE As CheckBox = DirectCast(dr.FindControl("CB_Eliminar"), CheckBox)
            Dim chkM As CheckBox = DirectCast(dr.FindControl("CB_Modificar"), CheckBox)
            acc = New Accion
            acc.IdEntidad = 1
            If chkV.Checked Then
                acc.Existe = True
            Else
                acc.Existe = False
            End If
            listaAcc.Add(acc)
            acc = New Accion
            acc.IdEntidad = 2
            If chkA.Checked Then
                acc.Existe = True
            Else
                acc.Existe = False
            End If
            listaAcc.Add(acc)
            acc = New Accion
            acc.IdEntidad = 3
            If chkM.Checked Then
                acc.Existe = True
            Else
                acc.Existe = False
            End If
            listaAcc.Add(acc)
            acc = New Accion
            acc.IdEntidad = 4
            If chkE.Checked Then
                acc.Existe = True
            Else
                acc.Existe = False
            End If
            listaAcc.Add(acc)
            objAcceso.ListaAccionesPosibles = listaAcc
            listaResult.Add(objAcceso)
        Next
        Return listaResult
    End Function
    Protected Sub GrillaPerfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrillaPerfiles.SelectedIndexChanged
        'Dim IdPerfil As Integer = CInt(GrillaFormularios.SelectedDataKey.Item("IdEntidad").ToString)
        'Dim objPErf As New Perfil(IdPerfil)
        'Dim l As String = ""
        'If objPErf.ListaAccesos IsNot Nothing Then
        '    For Each item As Acceso In objPErf.ListaAccesos
        '        l &= item. & "  --  "
        '        If item.Existe Then
        '            l &= item.Nombre & ","
        '        End If
        '    Next
        'End If
        'Lit_Permisos.Text = l
    End Sub
End Class
