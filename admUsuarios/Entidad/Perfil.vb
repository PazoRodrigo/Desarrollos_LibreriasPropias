Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports admUsuarios.DTO
Imports Newtonsoft.Json
Imports LUM

Namespace Entidad
    Public Class Perfil
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Perfil)
        Public Shared Property Todos() As List(Of Perfil)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Perfil.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Perfil.TraerTodos
            End Get
            Set(ByVal value As List(Of Perfil))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property IdRol() As Integer = 0
        Private _ListaAccesos As List(Of Acceso)
        Public Property ListaAccesos() As List(Of Acceso)
            Get
                If _ListaAccesos Is Nothing Then
                    _ListaAccesos = Acceso.TraerTodosXPerfil(IdEntidad)
                End If
                Return _ListaAccesos
            End Get
            Set(ByVal value As List(Of Acceso))
                _ListaAccesos = value
            End Set
        End Property
        Friend Shared Function TraerTodosXUsuario(idUsuario As Integer) As List(Of Perfil)
            Return DAL_Perfil.TraerTodosXUsuario(idUsuario)
        End Function
        Private _ListaRoles As List(Of Rol)
        Public Property ListaRoles() As List(Of Rol)
            Get
                Return _ListaRoles
            End Get
            Set(ByVal value As List(Of Rol))
                _ListaRoles = value
            End Set
        End Property
#End Region
#Region " Lazy Load "
        Public Property _ObjRol() As Rol
        Public ReadOnly Property ObjRol() As Rol
            Get
                If _ObjRol Is Nothing Then
                    _ObjRol = Rol.TraerUno(IdRol)
                End If
                Return _ObjRol
            End Get
        End Property
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
            Dim objImportar As Perfil = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
            IdRol = objImportar.IdRol
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Perfil
            Dim result As Perfil = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Perfil)
            Return Todos
        End Function
        Public Shared Function TraerTodosXRol(IdRol As Integer) As List(Of Perfil)
            Return DAL_Perfil.TraerTodosXRol(IdRol)
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Perfil
        '    Dim result As Perfil= DAL_Perfil.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Perfil)
        '    Dim result As List(Of Perfil) = DAL_Perfil.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        Private Structure SqlJsonStr
            Property IdPerfil As Integer
            Property Nombre As String
            Property IdRol As Integer
        End Structure
        Public Function ToSQLJson() As String
            Dim ObjDev As New SqlJsonStr With {
                .IdPerfil = IdEntidad,
                .Nombre = Nombre,
                .IdRol = IdRol
            }
            Return JsonConvert.SerializeObject(ObjDev)
        End Function
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            Dim objGuardar As DTO.DTO_Perfil = ConvertoToDTO()
            DAL_Perfil.Alta(objGuardar)
        End Sub
        Public Sub Modifica()
            ValidarAlta()
            Dim objGuardar As DTO.DTO_Perfil = ConvertoToDTO()
            DAL_Perfil.Modifica(objGuardar)
        End Sub
        Private Function ConvertoToDTO() As DTO_Perfil
            Dim result As New DTO.DTO_Perfil
            result = Me.ToDTO
            Dim listaResultDTO As New List(Of DTO.DTO_Acceso)
            If Not Me.ListaAccesos Is Nothing AndAlso Me.ListaAccesos.Count > 0 Then
                For Each item As Acceso In Me.ListaAccesos
                    listaResultDTO.Add(item.ToDTO)
                Next
            End If
            result.ListaAccesos = listaResultDTO
            Return result
        End Function

        Public Sub AltaJson()
            ValidarAlta()
            Dim jssql As String = ""
            jssql += "{""Permisos"": {""Perfil"": "
            jssql += Me.ToSQLJson()
            jssql += ",""Accesos"": { ""items"" : ["
            For index = 0 To ListaAccesos.Count - 1
                Dim item As Acceso = ListaAccesos(index)
                jssql += item.ToSQLJson()
                If index < ListaAccesos.Count - 1 Then
                    jssql += ","
                End If
            Next
            jssql += "]}"
            jssql += "}}"
            DAL_Perfil.AltaJson(Me, jssql)
        End Sub
        'Public Sub ModificaJson()
        '    Dim jssql As String = ""
        '    jssql += "{""Permisos"": {""Perfil"": "
        '    jssql += Me.ToSQLJson()
        '    jssql += ",""Accesos"": { ""items"" : ["
        '    For index = 0 To ListaAccesos.Count - 1
        '        Dim item As Acceso = ListaAccesos(index)
        '        jssql += item.ToSQLJson()
        '        If index < ListaAccesos.Count - 1 Then
        '            jssql += ","
        '        End If
        '    Next
        '    jssql += "]}"
        '    jssql += "}}"
        '    DAL_Perfil.ModificaJson(Me, jssql)
        'End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Perfil.Baja(Me)
        End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Perfil.Modifica(Me)
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Perfil
            Dim result As New DTO.DTO_Perfil With {
                .IdUsuarioAlta = IdUsuarioAlta,
                .IdUsuarioBaja = IdUsuarioBaja,
                .IdUsuarioModifica = IdUsuarioModifica,
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .IdRol = IdRol,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Perfil.TraerTodos
        End Sub
        ' Nuevos
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
            Else
                If Me.IdRol = 0 Then
                    sError &= "<b>IdRol</b> Debe ingresar el Rol. <br />"
                End If
            End If
            If Me.ListaAccesos.Count = 0 Then
                sError &= "<b>Accesos</b> Debe ingresar un Acceso. <br />"
            End If
            ' Campo Integer/Decimal
            '	If Me.VariableNumero.toString = "" Then
            '   	sError &= "<b>VariableNumero</b> Debe ingresar VariableNumero. <br />"
            '	ElseIf Not isnumeric(Me.VariableNumero) Then
            '   	sError &= "<b>VariableNumero</b> Debe ser numérico. <br />"
            '	End If

            ' Campo String
            '	If Me.VariableString = "" Then
            '		sError &= "<b>VariableString</b> Debe ingresar VariableString. <br />"
            '	ElseIf Me.apellido.Length > 50 Then
            '		sError &= "<b>VariableString</b> El campo debe tener como máximo 50 caracteres. <br />"
            '	End If

            ' Campo Date
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
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
            'Dim cantidad As Integer = DAL_Perfil.TraerTodosXDenominacionCant(Me.denominacion)
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
#End Region
    End Class ' Perfil
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Perfil
        Inherits LUM.DTO.DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property IdRol() As Integer = 0
        Public Property IdEstado() As Integer = 0
        Public Property ListaAccesos() As List(Of DTO.DTO_Acceso)
#End Region
    End Class ' DTO_Perfil
End Namespace ' DTO