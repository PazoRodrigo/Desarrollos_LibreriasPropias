Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports Newtonsoft.Json

Namespace Entidad
    Public Class Usuario
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Usuario)
        Public Shared Property Todos() As List(Of Usuario)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Usuario.TraerTodos
                End If
                Return _Todos
                'Return DAL_Usuario.TraerTodos
            End Get
            Set(ByVal value As List(Of Usuario))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdRol() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Documento_CUIT() As Long = 0
        Public Property Login() As String = ""
        Public Property Telefono() As String = ""
        Public Property CorreoElectronico() As String = ""
        'Private _ListaAreas As List(Of Area)
        'Public Property ListaAreas() As List(Of Area)
        '    Get
        '        If _ListaAreas Is Nothing Then
        '            _ListaAreas = Area.TraerTodosXUsuario(IdEntidad)
        '        End If
        '        Return _ListaAreas
        '    End Get
        '    Set(ByVal value As List(Of Area))
        '        _ListaAreas = value
        '    End Set
        'End Property
        'Private _ListaPerfiles As List(Of Perfil)
        'Public Property ListaPerfiles() As List(Of Perfil)
        '    Get
        '        If _ListaPerfiles Is Nothing Then
        '            _ListaPerfiles = Perfil.TraerTodosXUsuario(IdEntidad)
        '        End If
        '        Return _ListaPerfiles
        '    End Get
        '    Set(ByVal value As List(Of Perfil))
        '        _ListaPerfiles = value
        '    End Set
        'End Property
        'Private _ListaAtajos As List(Of Acceso)
        'Public Property ListaAtajos() As List(Of Acceso)
        '    Get
        '        If _ListaAtajos Is Nothing Then
        '            '_ListaAtajos = Acceso.TraerTodosAtajosXUsuario(IdEntidad)
        '        End If
        '        Return _ListaAtajos
        '    End Get
        '    Set(ByVal value As List(Of Acceso))
        '        _ListaAtajos = value
        '    End Set
        'End Property
        'Private _ListaSeccionales As List(Of Comunes.Entidad.Seccional)
        'Public Property ListaSeccionales() As List(Of Comunes.Entidad.Seccional)
        '    Get
        '        If _ListaSeccionales Is Nothing Then
        '            '_ListaSeccionales = Area.TraerTodosXUsuario(IdEntidad)
        '        End If
        '        Return _ListaSeccionales
        '    End Get
        '    Set(ByVal value As List(Of Comunes.Entidad.Seccional))
        '        _ListaSeccionales = value
        '    End Set
        'End Property
#End Region
#Region " Lazy Load / Read Only"
        'Public ReadOnly Property ListaRolesInicio() As List(Of Rol)
        '    Get
        '        Return DAL_Rol.TraerTodosXUsuario(IdEntidad)
        '    End Get
        'End Property
        'Private _ListaRolesPerfiles As List(Of Rol)
        'Public ReadOnly Property ListaRolesPerfiles() As List(Of Rol)
        '    Get
        '        Dim result As New List(Of Rol)
        '        If _ListaRolesPerfiles Is Nothing Then
        '            If ListaPerfiles IsNot Nothing Then
        '                Dim objRol As Rol
        '                For Each item As Perfil In ListaPerfiles
        '                    objRol = New Rol(item.IdRol)
        '                    If Not result.Contains(objRol) Then
        '                        result.Add(objRol)
        '                    End If
        '                Next
        '            End If
        '            _ListaRolesPerfiles = result
        '        End If
        '        Return _ListaRolesPerfiles
        '    End Get
        'End Property
        'Private _ListaFamilias As List(Of Familia)
        'Public ReadOnly Property ListaFamilias() As List(Of Familia)
        '    Get
        '        Dim result As New List(Of Familia)
        '        If _ListaFamilias Is Nothing Then
        '            If ListaPerfiles IsNot Nothing Then
        '                For Each itemPerfil As Perfil In ListaPerfiles
        '                    Dim ListaAccesos As List(Of Acceso) = itemPerfil.ListaAccesos
        '                    If ListaAccesos IsNot Nothing Then
        '                        For Each itemAcceso As Acceso In ListaAccesos
        '                            Dim ListaAcciones As List(Of Accion) = itemAcceso.ListaAcciones
        '                            If ListaAcciones IsNot Nothing And ListaAcciones.Count > 0 Then
        '                                For Each itemAccion As Accion In ListaAcciones
        '                                    Dim objFamilia As New Familia
        '                                    If itemAccion.Existe Then
        '                                        If Not result.Contains(itemAcceso.ObjFamilia) Then
        '                                            result.Add(itemAcceso.ObjFamilia)
        '                                        End If
        '                                    End If
        '                                Next
        '                            End If
        '                        Next
        '                    End If
        '                Next
        '            End If
        '            _ListaFamilias = result
        '        End If
        '        Return _ListaFamilias
        '    End Get
        'End Property
        'Private _ListaAccesos As List(Of Acceso)
        'Public ReadOnly Property ListaAccesos() As List(Of Acceso)
        '    Get
        '        Dim result As New List(Of Acceso)
        '        If _ListaAccesos Is Nothing Then
        '            If ListaPerfiles IsNot Nothing Then
        '                For Each itemPerfil As Perfil In ListaPerfiles
        '                    Dim TempListaAccesos As List(Of Acceso) = itemPerfil.ListaAccesos
        '                    If TempListaAccesos IsNot Nothing Then
        '                        For Each itemAcceso As Acceso In TempListaAccesos
        '                            Dim ListaAcciones As List(Of Accion) = itemAcceso.ListaAcciones
        '                            If ListaAcciones IsNot Nothing And ListaAcciones.Count > 0 Then
        '                                For Each itemAccion As Accion In ListaAcciones
        '                                    Dim e As Boolean = False
        '                                    If itemAccion.Existe Then
        '                                        If Not result.Contains(itemAcceso) Then
        '                                            result.Add(itemAcceso)
        '                                        End If
        '                                    End If
        '                                Next
        '                            End If
        '                        Next
        '                    End If
        '                Next
        '            End If
        '            _ListaAccesos = result
        '        End If
        '        Return _ListaAccesos
        '    End Get
        'End Property
        'Private _ListaAccesosRol As List(Of Acceso)
        'Public ReadOnly Property ListaAccesosRol() As List(Of Acceso)
        '    Get
        '        Dim result As New List(Of Acceso)
        '        If _ListaAccesosRol Is Nothing Then
        '            If ListaPerfiles IsNot Nothing Then
        '                For Each itemPerfil As Perfil In ListaPerfiles
        '                    If itemPerfil.IdRol = IdRol Then
        '                        Dim TempListaAccesos As List(Of Acceso) = itemPerfil.ListaAccesos
        '                        If TempListaAccesos IsNot Nothing Then
        '                            For Each itemAcceso As Acceso In TempListaAccesos
        '                                Dim ListaAcciones As List(Of Accion) = itemAcceso.ListaAcciones
        '                                If ListaAcciones IsNot Nothing And ListaAcciones.Count > 0 Then
        '                                    For Each itemAccion As Accion In ListaAcciones
        '                                        Dim e As Boolean = False
        '                                        If itemAccion.Existe Then
        '                                            If Not result.Contains(itemAcceso) Then
        '                                                result.Add(itemAcceso)
        '                                            End If
        '                                        End If
        '                                    Next
        '                                End If
        '                            Next
        '                        End If
        '                    End If
        '                Next
        '            End If
        '            _ListaAccesosRol = result
        '        End If
        '        Return _ListaAccesosRol
        '    End Get
        'End Property
        'Private _ObjMenu As String
        'Public ReadOnly Property ObjMenu() As String
        '    Get
        '        If _ObjMenu Is Nothing Then
        '            _ObjMenu = Menu.ArmarMenu(ListaAccesosRol)
        '        End If
        '        Return _ObjMenu
        '    End Get
        'End Property
        Public ReadOnly Property IdEstado() As Integer
            Get
                Dim result As Integer = 0
                If FechaBaja.HasValue Then
                    result = 1
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Usuario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
        Sub New(ByVal login As String, pass As String)
            Dim objImportar As Usuario = HacerLogIN(login, pass)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Usuario
            Dim result As Usuario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Usuario)
            Return Todos
        End Function
        Public Shared Function TraerTodosXArea(IdArea As Integer) As List(Of Usuario)
            Return DAL_Usuario.TraerTodosXArea(IdArea)
        End Function
        Public Shared Function TraerTodosXPerfil(idPerfil As Integer) As List(Of Usuario)
            Return DAL_Usuario.TraerTodosXPerfil(idPerfil)
        End Function
        Public Shared Function TraerTodosXRol(IdRol As Integer) As List(Of Usuario)
            Return DAL_Usuario.TraerTodosXRol(IdRol)
        End Function
        Public Shared Function TraerTodosXRolSinPerfil(IdRol As Integer) As List(Of Usuario)
            Return Nothing
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Usuario
        '    Dim result As Usuario= DAL_Usuario.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodosXArea() As List(Of Usuario)
        '    Dim result As List(Of Usuario) = DAL_Usuario.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        Private Shared Function HacerLogIN(ByVal Login As String, ByVal Password As String) As Usuario
            Dim Result As Usuario = DAL_Usuario.TraerUno(Login.Trim, Password.Trim)
            If Result Is Nothing Then
                Throw New Exception("Buscar Texto")
            End If
            Return Result


            ''Return DAL_Usuario.TraerUno(Nombre, Password)
            'Dim result As Usuario = Todos.Find(Function(x) x.Login.ToUpper.Trim = Login.ToUpper.Trim)
            ''Dim listaRol As List(Of Rol) = result.ListaRolesPerfiles
            'Return result

            ''If result Is Nothing Then
            ''    Throw New Exception("No existen resultados para la búsqueda")
            ''End If
            ''Return result
        End Function
        Public Shared Sub HacerLogOUT(ByVal Id As Integer)

        End Sub
#End Region
#Region " Métodos Públicos"
        Private Structure SqlJsonStrSecc
            Property IdSecc As String
        End Structure
        Public Function ToSQLJsonSecc(IdSeccional As Integer) As String
            Dim temp As String = Right("00" & IdSeccional.ToString, 2)
            Dim objDev As New SqlJsonStrSecc With {
                .IdSecc = temp
            }
            Return JsonConvert.SerializeObject(objDev)
        End Function
        Private Structure SqlJsonStr
            Property ApeNom_RSocial As String
            Property IdRol As Integer
            Property Doc_Cuit As Long
            Property UserLogin As String
            Property Telefono As String
            Property Correo As String
        End Structure
        Public Function ToSQLJson() As String
            Dim Per As New SqlJsonStr With {
                .ApeNom_RSocial = Nombre,
                .IdRol = IdRol,
                .Doc_Cuit = Documento_CUIT,
                .UserLogin = Login,
                .telefono = Telefono,
                .Correo = CorreoElectronico
            }
            Return JsonConvert.SerializeObject(Per)
        End Function
        ' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Usuario.Alta(Me)
        '    If ListaPerfiles IsNot Nothing AndAlso ListaPerfiles.Count > 0 Then
        '        For Each itemPerfil As Perfil In ListaPerfiles
        '            'AgregarEliminarPerfil(itemPerfil.IdEntidad)
        '        Next
        '    End If
        'End Sub
        'Public Sub AltaJson()
        '    ValidarAlta()
        '    Dim jssql As String = ""
        '    jssql += "{""Datos"": {""Basicos"": "
        '    jssql += Me.ToSQLJson()
        '    jssql += "},""Area"": ["
        '    For index = 0 To ListaAreas.Count - 1
        '        Dim item As Area = ListaAreas(index)
        '        jssql += item.ToSQLJson()
        '        If index < ListaAreas.Count - 1 Then
        '            jssql += ","
        '        End If
        '    Next
        '    jssql += "],""Seccional"": ["
        '    For index = 0 To ListaSeccionales.Count - 1
        '        Dim item As Comunes.Entidad.Seccional = ListaSeccionales(index)
        '        jssql += ToSQLJsonSecc(item.IdEntidad)
        '        If index < ListaSeccionales.Count - 1 Then
        '            jssql += ","
        '        End If
        '    Next
        '    jssql += "],""Perfil"": ["
        '    For index = 0 To ListaPerfiles.Count - 1
        '        Dim item As Entidad.Perfil = ListaPerfiles(index)
        '        jssql += item.ToSQLJson()
        '        If index < ListaPerfiles.Count - 1 Then
        '            jssql += ","
        '        End If
        '    Next
        '    jssql += "]}"
        '    DAL_Usuario.AltaJson(Me, jssql)
        'End Sub
        'Private Sub AgregarEliminarPerfil(IdPerfil As Integer, incluye As Boolean)
        '    DAL_Usuario.AgregarEliminarPerfil(Me, IdPerfil, incluye)
        'End Sub

        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Usuario.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Usuario.Modifica(Me)
        'End Sub
        '' Otros
        'Public Function ToDTO() As DTO.DTO_Usuario
        '    Dim result As New DTO.DTO_Usuario With {
        '        .IdEntidad = IdEntidad
        '    }
        '    Return result
        'End Function
        'Public Shared Sub Refresh()
        '    _Todos = DAL_Usuario.TraerTodos
        'End Sub
        ' Nuevos
        ' Otros
        Public Function ToDTO() As DTO.DTO_Usuario
            Dim result As New DTO.DTO_Usuario With {
                .IdEntidad = IdEntidad,
                .IdRol = IdRol,
                .Nombre = Nombre,
                .Documento_CUIT = Documento_CUIT,
                .Login = Login,
                .Telefono = Telefono,
                .CorreoElectronico = CorreoElectronico,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        'Public Shared Sub Refresh()
        '    _Todos = DAL_Usuario.TraerTodos
        'End Sub
#End Region
#Region " Métodos Privados "
        ' ABM
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
            If Me.Nombre = "" Then
                sError &= "<b>Nombre</b> Debe ingresar el Nombre. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.IdRol.ToString = "" Then
                sError &= "<b>Rol</b> Debe ingresar el Rol. <br />"
            ElseIf Not IsNumeric(Me.IdRol) Then
                sError &= "<b>Rol</b> Debe ser numérico. <br />"
            End If
            If Me.Documento_CUIT.ToString = "" Then
                sError &= "<b>Documento / CUIT</b> Debe ingresar el Documento / CUIT. <br />"
            ElseIf Not IsNumeric(Me.Documento_CUIT) Then
                sError &= "<b>Documento / CUIT</b> Debe ser numérico. <br />"
            End If
            If Me.Login = "" Then
                sError &= "<b>Login</b> Debe ingresar el Login. <br />"
            ElseIf Me.Login.Length > 50 Then
                sError &= "<b>Login</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.Telefono.ToString = "" Then
                sError &= "<b>Teléfono</b> Debe ingresar el Teléfono. <br />"
            ElseIf Not IsNumeric(Me.Documento_CUIT) Then
                sError &= "<b>Telefono</b> Debe ser numérico. <br />"
            End If
            If Me.CorreoElectronico.ToString = "" Then
                sError &= "<b>Correo Electrónico</b> Debe ingresar el Correo Electrónico . <br />"
            ElseIf Not IsNumeric(Me.Documento_CUIT) Then
                sError &= "<b>Correo Electrónico</b> Debe ser numérico. <br />"
            End If
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
            'Dim cantidad As Integer = DAL_Usuario.TraerTodosXDenominacionCant(Me.denominacion)
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
        'Private Sub ValidarLista()
        '    Dim sError As String = ""
        '    If ListaAreas Is Nothing Or ListaAreas.Count = 0 Then
        '        sError = "<b>No se han ingresado Áreas</b> <br /> <br />" & sError
        '    End If
        '    If ListaPerfiles Is Nothing Or ListaPerfiles.Count = 0 Then
        '        sError = "<b>No se han ingresado Perfiles</b> <br /> <br />" & sError
        '    End If
        '    If ListaSeccionales Is Nothing Or ListaSeccionales.Count = 0 Then
        '        sError = "<b>No se han ingresado Seccionales. </b> <br /> <br />" & sError
        '    End If
        '    If sError <> "" Then
        '        sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
        '        Throw New Exception(sError)
        '    End If
        'End Sub
#End Region
    End Class ' Usuario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Usuario

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdRol() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Documento_CUIT() As Long = 0
        Public Property Login() As String = ""
        Public Property Telefono() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property ListaRoles As List(Of DTO.DTO_Rol)
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_Usuario
End Namespace ' DTO
