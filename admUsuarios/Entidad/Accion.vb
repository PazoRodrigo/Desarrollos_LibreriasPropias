Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Accion
        Inherits DBE

        Private Shared _Todos As List(Of Accion)
        Public Shared Property Todos() As List(Of Accion)
            Get
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Accion.TraerTodos
                'End If
                'Return _Todos
                Return DAL_Accion.TraerTodos

            End Get
            Set(ByVal value As List(Of Accion))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
        Public Property Existe() As Boolean = False
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Accion = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
            Existe = objImportar.Existe
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Accion
            Dim result As Accion = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Accion)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Accion
        '    Dim result As Accion= DAL_Accion.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Accion)
        '    Dim result As List(Of Accion) = DAL_Accion.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        Friend Shared Function TraerTodosXFormulario(IdFormulario As Integer) As List(Of Accion)
            Return DAL_Accion.TraerTodosXFormulario(IdFormulario)
        End Function
        Friend Shared Function TraerTodosXAcceso(IdAcceso As Integer) As List(Of Accion)
            Return DAL_Accion.TraerTodosXAcceso(IdAcceso)
        End Function

#End Region
#Region " Métodos Públicos"
        '' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Accion.Alta(Me)
        'End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Accion.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Accion.Modifica(Me)
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Accion
            Dim result As New DTO.DTO_Accion With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .Existe = Existe
            }
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Accion.TraerTodos
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
            'Dim cantidad As Integer = DAL_Accion.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Accion
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Accion

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
        Public Property Existe() As Boolean = False
#End Region
    End Class ' DTO_Accion
End Namespace ' DTO