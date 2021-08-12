Option Explicit On
Option Strict On

Imports Comunes.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Parentesco
        Inherits DBE

        Private Shared _Todos As List(Of Parentesco)
        Public Shared Property Todos() As List(Of Parentesco)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Parentesco.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Parentesco))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
#Region " Lazy Load "
        Private _ObjMotivoBaja As MotivoBaja
        Public ReadOnly Property ObjMotivoBaja() As MotivoBaja
            Get
                If _ObjMotivoBaja Is Nothing Then
                    ' _ObjMotivoBaja = MotivoBaja.TraerUno(IdMotivoBaja)
                End If
                Return _ObjMotivoBaja
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
            IdEntidad = 0
            Nombre = ""
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Parentesco = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Parentesco
            Dim result As Parentesco = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Parentesco)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Parentesco
        '    Dim result As Parentesco= DAL_Parentesco.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Parentesco)
        '    Dim result As List(Of Parentesco) = DAL_Parentesco.TraerTodos()
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
            DAL_Parentesco.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Parentesco.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Parentesco.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Parentesco
            Dim result As New DTO.DTO_Parentesco
            result.IdEntidad = IdEntidad
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Parentesco.TraerTodos
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
            'Dim cantidad As Integer = DAL_Parentesco.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Parentesco
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Parentesco

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property Nombre() As String
#End Region
    End Class ' DTO_Parentesco
End Namespace ' DTO