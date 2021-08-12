Option Explicit On
Option Strict On

Imports Comunes.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Seccional
        Inherits DBE

        Private Shared _Todos As List(Of Seccional)
        Public Shared Property Todos() As List(Of Seccional)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Seccional.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Seccional))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
#Region " Lazy Load / Read Only"
        Public ReadOnly Property Codigo() As String
            Get
                Dim result As String = CStr(IdEntidad)
                If IdEntidad < 10 Then
                    result = Right("00" & IdEntidad, 2)
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
            IdEntidad = 0
            Nombre = ""
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Seccional = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            fechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Seccional
            Dim result As Seccional = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen Seccionales para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Seccional)
            Return Todos.FindAll(Function(x) x.IdEntidad <= 69)
        End Function
        Public Shared Function TraerTodosXNombre(ByVal Nombre As String) As List(Of Seccional)
            Dim result As List(Of Seccional) = Todos.FindAll(Function(x) x.Nombre.Contains(Nombre.Trim.ToUpper))
            If result Is Nothing Then
                Throw New Exception("No existen Seccionales para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Seccional.TraerTodos
        End Sub
        Public Function ToDTO() As DTO.DTO_Seccional
            Dim result As New DTO.DTO_Seccional With {
                .IdUsuarioAlta = IdUsuarioAlta,
                .IdUsuarioBaja = IdUsuarioBaja,
                .IdUsuarioModifica = IdUsuarioModifica,
                .FechaAlta = FechaAlta,
                .FechaBaja = FechaBaja,
                .IdMotivoBaja = IdMotivoBaja,
                .IdEntidad = IdEntidad,
                .Codigo = Codigo,
                .Nombre = Nombre
            }
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Seccional.Alta(Me)
        'End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Seccional.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Seccional.Modifica(Me)
        'End Sub
        ' Otros
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.idUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.idUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.idUsuarioModifica)
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
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Seccional.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Seccional
End Namespace ' Entidad
Namespace DTO
    Public Class DTO_Seccional

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
        Public Property Codigo() As String
        'DBE
        Public Property FechaAlta() As Nullable(Of Date) = Nothing
        Public Property FechaBaja() As Nullable(Of Date) = Nothing
        Public Property IdUsuarioAlta() As Integer = 0
        Public Property IdUsuarioBaja() As Integer = 0
        Public Property IdUsuarioModifica() As Integer = 0
        Public Property IdMotivoBaja() As Integer = 0
#End Region
    End Class ' DTO_Seccional
End Namespace ' DTO