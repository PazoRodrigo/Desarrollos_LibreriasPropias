Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    <Serializable()>
    Public Class Convenio
        Inherits DBE

        Private Shared _Todos As List(Of Convenio)
        Public Shared Property Todos() As List(Of Convenio)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Convenio.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Convenio))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property Descripcion() As String
        Public Property Numero() As String
        Public Property Sigla() As String
#End Region
#Region " Lazy Load "
        Public Property IdTipoCalculo() As Integer
        Private _ObjTipoCalculo As TipoCalculo
        Public ReadOnly Property ObjTipoCalculo() As TipoCalculo
            Get
                'If _ObjTipoCalculo Is Nothing Then
                '    _ObjTipoCalculo = TipoCalculo.TraerUno(IdTipoCalculo)
                'End If
                Return _ObjTipoCalculo
            End Get
        End Property
        Public Property IdTipoConvenio() As Integer
        Private _ObjTipoConvenio As TipoConvenio
        Public ReadOnly Property ObjTipoConvenio() As TipoConvenio
            Get
                'If _ObjTipoConvenio Is Nothing Then
                '    _ObjTipoConvenio = TipoConvenio.TraerUno(IdTipoConvenio)
                'End If
                Return _ObjTipoConvenio
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
            IdEntidad = 0
            Descripcion = ""
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Convenio = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            fechaAlta = objImportar.fechaAlta
            fechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal IdEntidad As Integer) As Convenio
            Return Todos.Find(Function(x) x.IdEntidad = IdEntidad)
        End Function
        Public Shared Function TraerTodos() As List(Of Convenio)
            Return Todos
        End Function
        Public Shared Function TraerTodosXEmpresaDesdeEmpleados(IdEmpresa As Integer) As List(Of Convenio)
            Return DAL_Convenio.TraerTodosXEmpresaDesdeEmpleados(IdEmpresa)
        End Function
        Public Shared Function TraerTodosXEmpresa(IdEmpresa As Integer) As List(Of Convenio)
            Return DAL_Convenio.TraerTodosXEmpresa(IdEmpresa)
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Convenio.Alta(Me)
        'End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Convenio.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Convenio.Modifica(Me)
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Convenio
            Dim result As New DTO.DTO_Convenio With {
                .IdEntidad = IdEntidad,
                .Descripcion = Descripcion,
                .Numero = Numero,
                .Sigla = Sigla
            }
            Return result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_Convenio.TraerTodos
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
            'Dim cantidad As Integer = DAL_Convenio.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Convenio
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Convenio

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property Descripcion() As String
        Public Property Numero() As String
        Public Property Sigla() As String
#End Region
    End Class ' DTO_Convenio
End Namespace ' DTO
