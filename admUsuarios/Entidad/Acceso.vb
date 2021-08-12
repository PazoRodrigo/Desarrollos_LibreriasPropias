Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports Newtonsoft.Json

Namespace Entidad
    Public Class Acceso
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Acceso)
        Public Shared Property Todos() As List(Of Acceso)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Acceso.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Acceso))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdFormulario() As Integer = 0
        Public Property IdFamilia() As Integer = 0
        Public Property IdRama() As Integer = 0
        Public Property IdPerfil() As Integer = 0
        Public Property IdAccion() As Integer = 0
        Private _ListaAccionesPosibles As List(Of Accion)
        Public Property ListaAccionesPosibles() As List(Of Accion)
            Get
                If _ListaAccionesPosibles Is Nothing Then
                    _ListaAccionesPosibles = Accion.TraerTodosXFormulario(IdFormulario)
                End If
                Return _ListaAccionesPosibles
            End Get
            Set(ByVal value As List(Of Accion))
                _ListaAccionesPosibles = value
            End Set
        End Property
        Private _ListaAcciones As List(Of Accion)
        Public ReadOnly Property ListaAcciones() As List(Of Accion)
            Get
                If _ListaAcciones Is Nothing Then
                    _ListaAcciones = Accion.TraerTodosXAcceso(IdEntidad)
                End If
                Return _ListaAcciones
            End Get
        End Property
#End Region
#Region " Lazy Load "
        Private _ObjFormulario As Formulario
        Public ReadOnly Property ObjFormulario() As Formulario
            Get
                'If _ObjFormulario Is Nothing Then
                _ObjFormulario = Formulario.TraerUno(IdFormulario)
                'End If
                Return _ObjFormulario
            End Get
        End Property
        Private _ObjFamilia As Familia
        Public ReadOnly Property ObjFamilia() As Familia
            Get
                'If _ObjFamilia Is Nothing Then
                _ObjFamilia = Familia.TraerUno(IdFamilia)
                'End If
                Return _ObjFamilia
            End Get
        End Property
        Private _ObjPerfil As Perfil
        Public ReadOnly Property ObjPerfil() As Perfil
            Get
                'If _ObjFamilia Is Nothing Then
                _ObjPerfil = Perfil.TraerUno(IdPerfil)
                'End If
                Return _ObjPerfil
            End Get
        End Property
        'Private _ListaAcciones As List(Of Accion)
        'Public ReadOnly Property ListaAcciones() As List(Of Accion)
        '    Get
        '        If _ListaAcciones Is Nothing Then
        '            _ListaAcciones = Accion.TraerTodosXFormulario(IdFormulario)
        '        End If
        '        Return _ListaAcciones
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Acceso = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdFormulario = objImportar.IdFormulario
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Acceso
        '    Dim result As Acceso = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Acceso)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Acceso
            Dim result As Acceso = DAL_Acceso.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Acceso)
            Dim result As List(Of Acceso) = DAL_Acceso.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXPerfil(IdPerfil As Integer) As List(Of Acceso)
            Dim result As List(Of Acceso) = DAL_Acceso.TraerTodosXPerfil(IdPerfil)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        ' Nuevos
        Public Shared Function TraerTodosXFormulario(IdFormulario As Integer) As List(Of Acceso)
            Return DAL_Acceso.TraerTodosXFormulario(IdFormulario)
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Acceso.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Acceso.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Acceso.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Acceso
            Dim result As New DTO.DTO_Acceso With {
                .IdEntidad = IdEntidad,
                .IdFormulario = IdFormulario,
                .IdFamilia = IdFamilia,
                .IdRama = IdRama,
                .IdPerfil = IdPerfil,
                .IdAccion = IdAccion
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Acceso.TraerTodos
        End Sub
        ' Nuevos
        Friend Sub AgregarEliminarAccion(idAccion As Integer, existe As Boolean)
            DAL_Acceso.AgregarEliminarAccion(Me, idAccion, existe)
        End Sub

        Private Structure SqlJsonStr
            Property IdFormulario As Integer
            Property IdAccion As Integer
            Property IdFamilia As Integer
        End Structure
        Public Function ToSQLJson() As String
            Dim Per As New SqlJsonStr With {
                .IdFormulario = Me.IdFormulario,
                .IdAccion = Me.IdAccion,
                .IdFamilia = Me.ObjFormulario().ObjRama().ObjFamilia.IdEntidad
            }
            Return JsonConvert.SerializeObject(Per)
        End Function
        Private Sub ValidarAltaAccion()
        End Sub
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
            'Dim cantidad As Integer = DAL_Acceso.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Acceso
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Acceso

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdFormulario() As Integer = 0
        Public Property IdFamilia() As Integer = 0
        Public Property IdRama() As Integer = 0
        Public Property IdPerfil() As Integer = 0
        Public Property IdAccion() As Integer = 0
#End Region
    End Class ' DTO_Acceso
End Namespace ' DTO