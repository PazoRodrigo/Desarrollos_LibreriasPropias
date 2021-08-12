Imports admUsuarios.Entidad

Partial Class Usuarios_ABMUsuario
    Inherits System.Web.UI.Page

    Private Sub Usuarios_ABMUsuario_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenarUsuarios()
            LlenarPerfiles()
            ViewState("IdUsuario") = 0
        End If
    End Sub
    Private Sub LlenarUsuarios()
        Usuario.Refresh()
        GrillaUsuarios.DataSource = Usuario.TraerTodos
        GrillaUsuarios.DataBind()
    End Sub
    Private Sub LlenarPerfiles()
        Perfil.Refresh()
        GrillaPerfiles.DataSource = Perfil.TraerTodos
        GrillaPerfiles.DataBind()
    End Sub
    Protected Sub BtnAlta_Click(sender As Object, e As EventArgs) Handles BtnAlta.Click
        Dim nuevo As New Usuario
        nuevo.IdUsuarioAlta = 2
        nuevo.ListaPerfiles = AlmacenarPerfiles()
        nuevo.Nombre = TxtNombre.Text
        nuevo.Alta()
        LlenarUsuarios()
    End Sub
    Private Function AlmacenarPerfiles() As List(Of Perfil)
        Dim listaRes As New List(Of Perfil)
        Dim objPerfil As Perfil
        For Each dr As GridViewRow In GrillaPerfiles.Rows
            objPerfil = New Perfil
            Dim IdPerfil As Integer = CInt(GrillaPerfiles.DataKeys(dr.RowIndex)("IdEntidad"))
            Dim chk As CheckBox = DirectCast(dr.FindControl("CB_Perfil"), CheckBox)
            objPerfil.IdEntidad = IdPerfil
            'If chk.Checked Then
            '    objPerfil.incluye = True
            'End If
            listaRes.Add(objPerfil)
        Next
        Return listaRes
    End Function
    Protected Sub GrillaUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrillaUsuarios.SelectedIndexChanged
        Dim l As String = ""
        Dim IdUsusario As Integer = CInt(GrillaUsuarios.SelectedDataKey.Item("IdEntidad").ToString)
        ViewState("IdUsuario") = IdUsusario
        Dim us As New Usuario(ViewState("IdUsuario"))
        Dim lisRoles As List(Of Rol) = Nothing
        'Dim lisRoles As List(Of Rol) = us.ListaRoles
        If lisRoles IsNot Nothing And lisRoles.Count > 0 Then
            l &= "Lista Roles Posibles:"
            For Each item As Rol In lisRoles
                l &= " " & item.Nombre
            Next
        End If
        Dim lisFamilias As List(Of Familia) = us.ListaFamilias
        If lisFamilias IsNot Nothing And lisFamilias.Count > 0 Then
            l &= "<br /> <br /> Lista Familias:"
            For Each item As Familia In lisFamilias
                l &= " " & item.Nombre
            Next
        End If

        Literal1.Text = l

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim nombre As String = TxtNombre.Text
        Dim password As String = TxtPassword.Text
        Dim us As Usuario = Usuario.HacerLogIN(nombre, password)
        Dim l As String = ""
        'Dim lisRoles As List(Of Rol) = us.ListaRoles
        'l &= "Lista Roles Posibles"
        'If lisRoles IsNot Nothing And lisRoles.Count > 0 Then
        '    For Each item As Rol In lisRoles
        '        l &= " " & item.Nombre
        '    Next
        'End If
        Literal1.Text = l
        us.IdRol = 1
        Literal2.Text = us.ObjMenu
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nombre As String = TxtNombre.Text
        Dim password As String = TxtPassword.Text
        Dim us As Usuario = Usuario.HacerLogIN(nombre, password)
        Dim l As String = ""
        'Dim lisRoles As List(Of Rol) = us.ListaRoles
        'l &= "Lista Roles Posibles"
        'If lisRoles IsNot Nothing And lisRoles.Count > 0 Then
        '    For Each item As Rol In lisRoles
        '        l &= " " & item.Nombre
        '    Next
        'End If
        Literal1.Text = l
        us.IdRol = 2
        Literal2.Text = us.ObjMenu
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim nombre As String = TxtNombre.Text
        Dim password As String = TxtPassword.Text
        Dim us As Usuario = Usuario.HacerLogIN(nombre, password)
        Dim l As String = ""
        'Dim lisRoles As List(Of Rol) = us.ListaRoles
        'l &= "Lista Roles Posibles"
        'If lisRoles IsNot Nothing And lisRoles.Count > 0 Then
        '    For Each item As Rol In lisRoles
        '        l &= " " & item.Nombre
        '    Next
        'End If
        Literal1.Text = l
        us.IdRol = 3
        Literal2.Text = us.ObjMenu
    End Sub
    Protected Sub BtnLogIN_Click(sender As Object, e As EventArgs) Handles BtnLogIN.Click
        'Try
        '    Dim ws As wsTransferObj = CallApi.TraerGet("Users/login/" & txtusername.Text & "/" & TxtPassword.Text)
        '    If ws.TodoOk = True Then
        '        Dim jss As New JavaScriptSerializer()
        '        Dim usuario = ws.Data.Datos

        '        VariableSesion.Cookies.Id_Hotel = usuario("IdHotel")
        '        VariableSesion.Cookies.Id_User = usuario("Id")
        '        VariableSesion.Cookies.UserName = usuario("UserName")
        '        VariableSesion.Cookies.Permisos = Right("0000" & usuario("Permisos").ToString(), 4)
        '        lblError.Text = "Logueado Correctamente"
        '        BtnLogIN.Visible = False
        '        btnEntrar.Visible = True
        '    Else
        '        lblError.Text = ws.Mensaje
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try

        Dim nombre As String = "usrSistemas"
        Dim password As String = "123456"
        'Dim us As Usuario = Usuario.HacerLogIN(nombre, password)
        'Dim l As String = ""
        'Dim lisRoles As List(Of Rol) = us.ListaRoles
        'l &= "Lista Roles Posibles"
        'If lisRoles IsNot Nothing And lisRoles.Count > 0 Then
        '    For Each item As Rol In lisRoles
        '        l &= " " & item.Nombre
        '    Next
        'End If


        'Dim lisFamilias As List(Of Familia) = us.ListaFamilias
        'l &= "<br /> Lista Familias"
        'If lisFamilias IsNot Nothing And lisFamilias.Count > 0 Then
        '    For Each itemFa As Familia In lisFamilias
        '        l &= " " & itemFa.Nombre
        '    Next
        'End If
        'l &= "<br /> Lista Accesos"
        'If us.ListaAccesos IsNot Nothing And us.ListaAccesos.Count > 0 Then
        '    For Each itemLA As Acceso In us.ListaAccesos
        '        l &= " " & itemLA.ObjFormulario.Nombre
        '    Next
        'End If
        'Literal1.Text = l
        'Literal2.Text = us.ObjMenu
    End Sub
End Class
