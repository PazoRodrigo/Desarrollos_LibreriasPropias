Option Explicit On
Option Strict On

Imports Comunes.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Empleado
        Inherits Persona

        Private Shared _Todos As List(Of Empleado)
        Public Shared Property Todos() As List(Of Empleado)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Empleado.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Empleado))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property Legajo() As Integer = 0
        Public Property Empresa() As String = ""
        Public Property LugarPago() As String = ""
        Public Property FechaIngreso() As Date? = Nothing
#End Region
#Region " Lazy Load "
        Public ReadOnly Property Antiguedad() As Integer
            Get
                Dim result As Integer = 0
                If FechaIngreso.HasValue Then
                    result = CInt(DateDiff(DateInterval.Year, FechaIngreso.Value, CDate(Today())))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
           
        End Sub
        Sub New(ByVal _Legajo As Integer)
            Dim objImportar As Empleado = TraerUnoXLegajo(_Legajo)
            ' Persona
            ApellidoNombre = objImportar.ApellidoNombre
            ' Entidad
            Legajo = objImportar.Legajo
            LugarPago = objImportar.LugarPago
            FechaIngreso = objImportar.FechaIngreso
            NroDocumento = objImportar.NroDocumento
            Empresa = objImportar.Empresa
            TipoDocumento = objImportar.TipoDocumento
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUnoXLegajo(ByVal Legajo As Integer) As Empleado
            Dim result As Empleado = Todos.Find(Function(x) x.Legajo = Legajo)
            If result Is Nothing Then
                Throw New Exception("No existen Empleados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Empleado)
            Return Todos
        End Function
        Public Shared Function TraerTodosXNombre(ByVal Nombre As String) As List(Of Empleado)
            Dim result As List(Of Empleado) = Todos.FindAll(Function(x) x.ApellidoNombre.ToUpper.Contains(Nombre.ToUpper))
            If result.Count = 0 Then
                Throw New Exception("No existen Empleados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXNroDocumento(ByVal NroDocumento As Long) As Empleado
            Dim result As Empleado = Todos.Find(Function(x) x.NroDocumento = NroDocumento)
            If result Is Nothing Then
                Throw New Exception("No existen Empleados para la búsqueda")
            End If
            Return result
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Empleado.Alta(Me)
        'End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Empleado.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Empleado.Modifica(Me)
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Empleado
            Dim result As New DTO.DTO_Empleado With {
                .Legajo = Legajo,
                .ApellidoNombre = ApellidoNombre,
                .LugarPago = LugarPago,
                .FechaIngreso = FechaIngreso,
                .FechaBaja = FechaBaja,
                .NroDocumento = NroDocumento,
                .Empresa = Empresa,
                .TipoDocumento = TipoDocumento
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Empleado.TraerTodos
        End Sub
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
            'Dim cantidad As Integer = DAL_Empleado.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Empleado
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Empleado

#Region " Atributos / Propiedades"
        ' Persona
        Public Property ApellidoNombre() As String
        Public Property NroDocumento() As Long
        Public Property TipoDocumento() As String
        ' Entidad
        Public Property Legajo() As Integer
        Public Property LugarPago() As String
        Public Property Empresa() As String
        Public Property FechaIngreso() As Date?
        Public Property FechaBaja() As Date?
#End Region
    End Class ' DTO_Empleado
End Namespace ' DTO