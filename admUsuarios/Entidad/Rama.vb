Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary

Namespace Entidad
    Public Class Rama
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Rama)
        Public Shared Property Todos() As List(Of Rama)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Rama.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Rama.TraerTodos
            End Get
            Set(ByVal value As List(Of Rama))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property IdFamilia() As Integer = 0
        Public Property Orden() As Integer = 0
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
#End Region
#Region " Lazy Load "
        Private _ListaFormularios As List(Of Formulario)
        Public Property ListaFormularios() As List(Of Formulario)
            Get
                Return Formulario.TraerTodosXRama(IdEntidad)
            End Get
            Set(ByVal value As List(Of Formulario))
                _ListaFormularios = value
            End Set
        End Property
        Private _ObjFamilia As Familia
        Public ReadOnly Property ObjFamilia() As Familia
            Get
                Return Familia.TraerUno(IdFamilia)
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Rama = TraerUno(id)
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
        Public Shared Function TraerUno(ByVal Id As Integer) As Rama
            Dim result As Rama = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Rama)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Rama
        '    Dim result As Rama= DAL_Rama.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Rama)
        '    Dim result As List(Of Rama) = DAL_Rama.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        Friend Shared Function TraerTodosXFamilia(idFamilia As Integer) As List(Of Rama)
            Dim result As New List(Of Rama)
            If Todos IsNot Nothing AndAlso Todos.Count > 0 Then
                result = Todos.FindAll(Function(x) x.IdFamilia = idFamilia)
            End If
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function Reordenar(IdRama As Integer) As List(Of Rama)
            Return DAL_Rama.Reordenar(IdRama)
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Rama.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Rama.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Rama.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Rama
            Dim result As New DTO.DTO_Rama With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .IdFamilia = IdFamilia,
                .Orden = Orden,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Rama.TraerTodos
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
            ValidarFormularioActiva()
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
            If Me.Nombre = "" Then
                sError &= "<b>Nombre en Menú</b> Debe ingresar el Nombre en Menú. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre en Menú</b> El campo debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.IdFamilia.ToString = "" Then
                sError &= "<b>Familia</b> Debe ingresar la Familia. <br />"
            ElseIf Not IsNumeric(Me.IdFamilia) Then
                sError &= "<b>Familia</b> Debe ser numérico. <br />"
            Else
                If Me.IdFamilia = 0 Then
                    sError &= "<b>Familia</b> Debe ingresar la Familia. <br />"
                End If
            End If

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
            'Dim cantidad As Integer = DAL_Rama.TraerTodosXDenominacionCant(Me.denominacion)
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
        Private Sub ValidarFormularioActiva()
            Dim listaControl As List(Of Formulario) = ListaFormularios
            Dim activa As Boolean = False
            If listaControl IsNot Nothing AndAlso listaControl.Count > 0 Then
                For Each item As Formulario In listaControl
                    If Not item.FechaBaja.HasValue Then
                        activa = True
                    End If
                Next
            End If
            If activa Then
                Throw New Exception("Existen Formularios Activos")
            End If
        End Sub
#End Region
    End Class ' Rama
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Rama

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property IdFamilia() As Integer = 0
        Public Property Orden() As Integer = 0
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_Rama
End Namespace ' DTO