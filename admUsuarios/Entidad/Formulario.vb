Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Formulario
        Inherits DBE

        Private Shared _Todos As List(Of Formulario)
        Public Shared Property Todos() As List(Of Formulario)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Formulario.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Formulario.TraerTodos
            End Get
            Set(ByVal value As List(Of Formulario))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdRama() As Integer = 0
        Public Property Orden() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property URL() As String = ""
        Public Property AccionesPosibles() As Integer = 0
        Private _IdEstado As Integer
        Public Property IdEstado() As Integer
            Get
                Dim result As Integer = 0
                If FechaBaja.HasValue Then
                    result = 1
                End If
                Return result
            End Get
            Set(ByVal value As Integer)
                _IdEstado = value
            End Set
        End Property
        Public ReadOnly Property ObjRama() As Rama
            Get
                Dim _objRama As New Rama()
                If IdRama > 0 Then
                    _objRama = Rama.TraerUno(IdRama)
                End If
                Return _objRama
            End Get
        End Property
        Private ReadOnly _ListaAccionesPosibles As List(Of Accion)
        Public ReadOnly Property ListaAccionesPosibles() As List(Of Accion)
            Get
                'If _ListaAccionesPosibles Is Nothing Then
                '    _ListaAccionesPosibles = Accion.TraerTodosXFormulario(IdEntidad)
                'End If
                'Return _ListaAccionesPosibles
                Return Nothing
            End Get
        End Property
        'Public Property ListaAccionesPosibles() As List(Of Accion)
        '    Get
        '        If _ListaAccionesPosibles Is Nothing Then
        '            _ListaAccionesPosibles = Accion.TraerTodosXFormulario(IdEntidad)
        '        End If
        '        Return _ListaAccionesPosibles
        '    End Get
        '    Set(ByVal value As List(Of Accion))
        '        _ListaAccionesPosibles = value
        '    End Set
        'End Property
        'Private _ListaFamilias As List(Of Familia)
        'Public Property ListaFamilias() As List(Of Familia)
        '    Get
        '        If _ListaFamilias Is Nothing Then
        '            _ListaFamilias = Familia.TraerTodosXFormulario(IdEntidad)
        '        End If
        '        Return _ListaFamilias
        '    End Get
        '    Set(ByVal value As List(Of Familia))
        '        _ListaFamilias = value
        '    End Set
        'End Property
#End Region
#Region " Lazy Load "
        Private ReadOnly _ListaAccesos As List(Of Acceso)
        Public ReadOnly Property ListaAccesos() As List(Of Acceso)
            Get
                Return Acceso.TraerTodosXFormulario(IdEntidad)
                'If _ListaAccionesPosibles Is Nothing Then
                '    _ListaAccionesPosibles = Accion.TraerTodosXFormulario(IdEntidad)
                'End If
                'Return _ListaAccionesPosibles
                Return Nothing
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Formulario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdRama = objImportar.IdRama
            Nombre = objImportar.Nombre
            URL = objImportar.URL
            AccionesPosibles = objImportar.AccionesPosibles
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Formulario
            Dim result As Formulario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Formulario)
            Return Todos
        End Function
        Friend Shared Function TraerTodosXRama(IdRama As Integer) As List(Of Formulario)
            Return DAL_Formulario.TraerTodosXRama(IdRama)
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Formulario
        '    Dim result As Formulario= DAL_Formulario.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Formulario)
        '    Dim result As List(Of Formulario) = DAL_Formulario.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Formulario.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Formulario.Baja(Me)
            'Refresh()
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Formulario.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Formulario
            Dim result As New DTO.DTO_Formulario With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .IdRama = IdRama,
                .Orden = Orden,
                .URL = URL,
                .AccionesPosibles = AccionesPosibles,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Formulario.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
            'ValidarFamilias()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
            'ValidarAccesos()
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
            Dim formAnterior = TraerUno(IdEntidad)
            ValidarCampos()
            ValidarNoDuplicados()
            ValidarAccesos(formAnterior)
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
                sError &= "<b>Nombre en Menú</b> Debe ingresar el Nombre en Menú. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre en Menú</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.Nombre = "" Then
                sError &= "<b>URL</b> Debe ingresar la URL. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>URL</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            'If Me.IdRama.ToString = "" Then
            '    sError &= "<b>Rama</b> Debe ingresar la Rama. <br />"
            'ElseIf Not IsNumeric(Me.IdRama) Then
            '    sError &= "<b>Rama</b> Debe ser numérico. <br />"
            'Else
            '    If Me.IdRama = 0 Then
            '        sError &= "<b>Rama</b> Debe ingresar la Rama. <br />"
            '    End If
            'End If
            If Me.AccionesPosibles.ToString = "" Then
                sError &= "<b>Acciones Posibles</b> Debe ingresar Acciones Posibles. <br />"
            ElseIf Not IsNumeric(Me.AccionesPosibles) Then
                sError &= "<b>Acciones Posibles</b> Debe ser numérico. <br />"
            Else
                If Me.AccionesPosibles = 0 Then
                    sError &= "<b>Acciones Posibles</b> Debe ingresar Acciones Posibles. <br />"
                End If
            End If
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
            'Dim cantidad As Integer = DAL_Formulario.TraerTodosXDenominacionCant(Me.denominacion)
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
        'Private Sub ValidarFamilias()
        '    If ListaFamilias Is Nothing Or ListaFamilias.Count = 0 Then
        '        Throw New Exception("Debe seleccionar una Familia para el Formulario")
        '    End If
        'End Sub
        'Otros
        Private Sub AgregarEliminarAccion(IdAccion As Integer, Existe As Boolean)
            DAL_Formulario.AgregarEliminarAccion(Me, IdAccion, Existe)
        End Sub
        Private Sub ValidarAccesos(formAnterior As Formulario)
            Dim encontrado As Boolean
            If formAnterior.ListaAccesos IsNot Nothing AndAlso formAnterior.ListaAccesos.Count > 0 Then
                If ListaAccesos IsNot Nothing AndAlso ListaAccesos.Count > 0 Then
                    For Each item1 As Acceso In ListaAccesos
                        encontrado = False
                        For Each item2 As Acceso In formAnterior.ListaAccesos
                            If item1.IdAccion = item2.IdAccion Then
                                encontrado = True
                            End If
                        Next
                        If Not encontrado Then
                            Throw New Exception("El Formulario no puede modificarse ya que tiene Accesos generados")
                        End If
                    Next
                End If
            End If
        End Sub
        ' Otros
        'Private Sub EstablecerOrden()
        '    Dim lista As New List(Of Formulario)
        '    lista = DAL_Formulario.TraerTodosXRama(IdRama)
        '    Dim TempOrden As Integer = 1
        '    If lista IsNot Nothing AndAlso lista.Count > 0 Then
        '        TempOrden = lista.Count + 1
        '    End If
        '    Me.Orden = TempOrden
        'End Sub
#End Region
    End Class ' Formulario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Formulario

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdRama() As Integer = 0
        Public Property Orden() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property URL() As String = ""
        Public Property AccionesPosibles() As Integer = 0
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_Formulario
End Namespace ' DTO