Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM

Namespace Entidad
    <Serializable()>
    Public Class Representado
        Inherits Persona

#Region " Atributos / Propiedades "
        Public Property FechaIngreso() As Date?
        Public Property NroSindical() As Long
        Public Property IdAfiliado() As Integer
        Public Property Observaciones() As String
        Public Property IdSeccional() As Integer
        Public Property IdTipoAfiliado() As Integer
#End Region
#Region " Lazy Load / ReadOnly "
        Private _ObjSedccional As Comunes.Entidad.Seccional
        Public ReadOnly Property ObjSedccional() As Comunes.Entidad.Seccional
            Get
                If _ObjSedccional Is Nothing Then
                    _ObjSedccional = Comunes.Entidad.Seccional.TraerUno(IdSeccional)
                End If
                Return _ObjSedccional
            End Get
        End Property
        Private _ListaEmpresa As List(Of Empresa)
        Public ReadOnly Property ListaEmpresa() As List(Of Empresa)
            Get
                If _ListaEmpresa Is Nothing Then
                    _ListaEmpresa = Empresa.TraerTodosXEmpleado(IdAfiliado)
                End If
                Return _ListaEmpresa
            End Get
        End Property
        Private _ListaFamiliares As List(Of Familiar)
        Public ReadOnly Property ListaFamiliares() As List(Of Familiar)
            Get
                If _ListaFamiliares Is Nothing Then
                    _ListaFamiliares = Familiar.TraerTodosXRepresentado(IdAfiliado)
                End If
                Return _ListaFamiliares
            End Get
        End Property
        Public ReadOnly Property StrTipoAfiliado() As String
            Get
                Dim result As String = ""
                Select Case IdTipoAfiliado
                    Case 1
                        result = "Afil. Permanente"
                    Case 2
                        result = "Afil. Temporario"
                    Case 3
                        result = "Rama x Reunion"
                    Case 4
                        result = "Jubilado"
                    Case 5
                        result = "Aportante"
                    Case 6
                        result = "Aport. Temporario"
                    Case 7
                        result = "Aport. Rama x Reunion"
                    Case 8
                        result = "Afiliado Jub. Simple"
                    Case 9
                        'result = "Afiliado Jub. Cotizante"
                        result = "Jubilado"
                    Case Else

                End Select
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaIngreso() As Long
            Get
                Dim result As Long = 0
                If FechaIngreso.HasValue Then
                    result = CLng(FechaIngreso.Value.Year.ToString & Right("00" & FechaIngreso.Value.Month.ToString, 2) & Right("00" & FechaIngreso.Value.Day.ToString, 2))
                End If
                Return result
            End Get
        End Property
        'Public ReadOnly Property LngFechaBaja() As Long
        '    Get
        '        Dim result As Long = 0
        '        If FechaBaja.HasValue Then
        '            result = CLng(FechaBaja.Value.Year.ToString & Right("00" & FechaBaja.Value.Month.ToString, 2) & Right("00" & FechaBaja.Value.Day.ToString, 2))
        '        End If
        '        Return result
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()
            ' Persona
            'NroDocumento = 0
            'TipoDocumento = ""
            'ApellidoNombre = ""
            'FechaNacimiento
            'CUIL = 0
            ' UTE
            NroSindical = 0
            IdAfiliado = 0
            Observaciones = ""
            IdSeccional = 0
            IdTipoAfiliado = 0
        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Representado = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            NroDocumento = objImportar.NroDocumento
            TipoDocumento = objImportar.TipoDocumento
            ApellidoNombre = objImportar.ApellidoNombre
            FechaNacimiento = objImportar.FechaNacimiento
            CUIL = objImportar.CUIL
            ' Entidad UTE
            NroSindical = objImportar.NroSindical
            IdAfiliado = objImportar.IdAfiliado
            Observaciones = objImportar.Observaciones
            IdSeccional = objImportar.IdSeccional
            IdTipoAfiliado = objImportar.IdTipoAfiliado
            Nacionalidad = objImportar.Nacionalidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Representado
            Dim result As Representado = DAL_Representado.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Representados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXNroSindical(NroSindical As Long) As Representado
            Dim result As Representado = DAL_Representado.TraerUnoXNroSindical(NroSindical)
            If result Is Nothing Then
                Throw New Exception("No existen Representados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXNroDocumento(NroDocumento As Long) As Representado
            Dim result As Representado = DAL_Representado.TraerUnoXNroDocumento(NroDocumento)
            If result Is Nothing Then
                Throw New Exception("No existen Representados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXCUIL(CUIL As Long) As Representado
            Dim result As Representado = DAL_Representado.TraerUnoXCUIL(CUIL)
            If result Is Nothing Then
                Throw New Exception("No existen Representados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXApellidoNombre(ApellidoNombre As String) As List(Of Representado)
            Dim result As List(Of Representado) = DAL_Representado.TraerTodosXApellidoNombre(ApellidoNombre)
            If result Is Nothing Then
                Throw New Exception("No existen Representados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXEmpresa(CodEmpresa As Long, Dependencia As Integer) As List(Of Representado)
            Dim result As New List(Of Representado)
            Try
                result = DAL_Representado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
            Catch ex As Exception
                Return result
            End Try
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            'DAL_Representado.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            'DAL_Representado.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            'DAL_Representado.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Representado
            Dim result As New DTO.DTO_Representado With {
                .NroDocumento = NroDocumento,
                .TipoDocumento = TipoDocumento,
                .ApellidoNombre = ApellidoNombre,
                .CUIL = CUIL,
                .IdSexo = IdSexo,
                .NroSindical = NroSindical,
                .IdAfiliado = IdAfiliado,
                .Observaciones = Observaciones,
                .IdSeccional = IdSeccional,
                .IdTipoAfiliado = IdTipoAfiliado,
                .FechaNacimiento = LngFechaNacimiento,
                .StrCuilConGuiones = StrCuilConGuiones,
                .Edad = Edad,
                .Sexo = Sexo,
                .FechaIngreso = LngFechaIngreso,
                .FechaBaja = LngFechaBaja,
                .Nacionalidad = Nacionalidad,
                .StrNacionalidad = StrNacionalidad
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
            'Dim cantidad As Integer = DAL_Representado.TraerTodosXDenominacionCant(Me.denominacion)
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
    Public Class DTO_Representado
#Region " Atributos / Propiedades"
        ' Persona
        Public Property NroDocumento() As Long
        Public Property TipoDocumento() As String
        Public Property ApellidoNombre() As String
        Public Property CUIL() As Long
        Public Property IdSexo() As Integer
        ' Representado
        Public Property FechaIngreso() As Long
        Public Property NroSindical() As Long
        Public Property IdAfiliado() As Integer
        Public Property Observaciones() As String
        Public Property IdSeccional() As Integer
        Public Property IdTipoAfiliado() As Integer
        ' ReadOnly Persona
        Public Property FechaNacimiento() As Long
        Public Property StrCuilConGuiones() As String
        Public Property Edad() As Integer
        Public Property Sexo() As String
        Public Property Nacionalidad() As String
        Public Property StrNacionalidad() As String
        ' LUM
        Public Property FechaBaja() As Long
#End Region
    End Class ' DTO_Empleado
End Namespace ' DTO