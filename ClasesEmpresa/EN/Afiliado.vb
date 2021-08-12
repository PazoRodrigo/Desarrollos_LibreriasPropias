Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Afiliado
        Inherits Persona

        Private Shared _Todos As List(Of Afiliado)
        Public Shared Property Todos() As List(Of Afiliado)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Afiliado.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Afiliado))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property NroAfiliado As Long
        Public Property Parent As Integer
        Public Property IdSeccional As Integer
#End Region
#Region " Lazy Load "
        Private _ObjSedccional As Comunes.Entidad.Seccional
        Public ReadOnly Property ObjSedccional() As Comunes.Entidad.Seccional
            Get
                If _ObjSedccional Is Nothing Then
                    _ObjSedccional = Comunes.Entidad.Seccional.TraerUno(IdSeccional)
                End If
                Return _ObjSedccional
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
            NroAfiliado = 0
            Parent = 0
        End Sub
        Sub New(ByVal NroAfiliado As Long, Parent As Integer)
            Dim objImportar As Afiliado = TraerUno(NroAfiliado, Parent)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            NroAfiliado = objImportar.NroAfiliado
            Parent = objImportar.Parent
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal NroAfiliado As Long, Parent As Integer) As Afiliado
            Dim result As Afiliado = DAL_Afiliado.TraerUno(NroAfiliado, Parent)
            If result Is Nothing Then
                Throw New Exception("No existen Afiliados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Afiliado)
            Return Todos
        End Function
        Friend Shared Function TraerTodosXEmpresa(codEmpresa As Long, dependencia As Integer) As List(Of Afiliado)
            Dim result As New List(Of Afiliado)
            Try
                result = DAL_Afiliado.TraerTodosXEmpresa(codEmpresa, dependencia)
                Return result
            Catch ex As Exception
                Return result
            End Try
            Return result
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Afiliado.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Afiliado.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Afiliado.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Afiliado
            Dim result As New DTO.DTO_Afiliado With {
                .NroAfiliado = NroAfiliado,
                .Parent = Parent,
                .NombreApellido = ApellidoNombre,
                .TipoDocumento = TipoDocumento,
                .NroDocumento = NroDocumento,
                .Edad = Edad
            }
            Return result
        End Function
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
            'Dim cantidad As Integer = DAL_Afiliado.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Afiliado
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Afiliado

#Region " Atributos / Propiedades"
        Public Property NroAfiliado As Long
        Public Property Parent As Integer
        Public Property IdSeccional As Integer
        Public Property NombreApellido As String
        Public Property TipoDocumento As String
        Public Property NroDocumento As Long
        Public Property Edad As Integer
#End Region
    End Class ' DTO_Afiliado
End Namespace ' DTO