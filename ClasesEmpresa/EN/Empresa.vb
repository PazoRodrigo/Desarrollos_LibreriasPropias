Option Explicit On
Option Strict On

Imports ClasesEmpresa.DataAccessLibrary
Imports LUM


Namespace Entidad
    <Serializable()>
    Public Class Empresa
        Inherits DBE

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Long
        Public Property CodEmpresa() As Long
        Public Property Denominacion() As String
        Public Property DenominacionCarnet() As String
        Public Property Dependencia() As Integer
        Public Property CUIT() As Long
        Public Property IdSeccional() As Integer
        Public Property Direccion() As String
        Public Property IdProvincia() As Integer
        Public Property TipoEntidad() As String
        Public Property Contacto_DDN() As Integer
        Public Property Contacto_Telefono() As String
        Public Property Localidad() As String
        Public Property CodPostal() As Integer
        Public Property Email() As String
        Public Property Procedencia() As String
        Public Property DireccionAlternativa() As String

        ' Enlatado
        Public Property IdTipoEmpresa() As Integer
        Public Property IdSubTipoEmpresa() As Integer
#End Region
#Region " Lazy Load / Read Only"
        Private _ListaConvenios As List(Of Convenio)
        Public ReadOnly Property ListaConvenios() As List(Of Convenio)
            ' Trae los Convenios desde la Empresa
            Get
                If _ListaConvenios Is Nothing Then
                    _ListaConvenios = Convenio.TraerTodosXEmpresa(CInt(IdEntidad))
                End If
                Return _ListaConvenios
            End Get
        End Property
        Private _ListaConveniosDesdeEmpleados As List(Of Convenio)
        Public ReadOnly Property ListaConveniosDesdeEmpleados() As List(Of Convenio)
            ' Trae los Convenios desde los Empleados
            Get
                If _ListaConveniosDesdeEmpleados Is Nothing Then
                    _ListaConveniosDesdeEmpleados = Convenio.TraerTodosXEmpresaDesdeEmpleados(CInt(IdEntidad))
                End If
                Return _ListaConveniosDesdeEmpleados
            End Get
        End Property
        Private _ObjSeccional As Comunes.Entidad.Seccional
        Public ReadOnly Property ObjSeccional() As Comunes.Entidad.Seccional
            Get
                If _ObjSeccional Is Nothing Then
                    _ObjSeccional = Comunes.Entidad.Seccional.TraerUno(IdSeccional)
                End If
                Return _ObjSeccional
            End Get
        End Property
        Private _ObjProvincia As Comunes.Entidad.Provincia
        Public ReadOnly Property Objprovincia() As Comunes.Entidad.Provincia
            Get
                If _ObjProvincia Is Nothing Then
                    _ObjProvincia = Comunes.Entidad.Provincia.TraerUno(IdProvincia)
                End If
                Return _ObjProvincia
            End Get
        End Property
        Private _ListaDependencias As List(Of Empresa)
        Public ReadOnly Property ListaDependencias() As List(Of Empresa)
            Get
                If _ListaDependencias Is Nothing Then
                    Dim listaTemporal As List(Of Empresa) = TraerTodosXCodEmpresa(CodEmpresa)
                    _ListaDependencias = listaTemporal.FindAll(Function(x) x.Dependencia > 0)
                End If
                Return _ListaDependencias
            End Get
        End Property
        ' UTEDyC
        Private _ListaEmpleadoUTEDyC As List(Of Representado)
        Public ReadOnly Property ListaEmpleadoUTEDyC() As List(Of Representado)
            Get
                If _ListaEmpleadoUTEDyC Is Nothing Then
                    _ListaEmpleadoUTEDyC = Representado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                End If
                Return _ListaEmpleadoUTEDyC
            End Get
        End Property
        Private _ListaAfiliadosUTEDyC As List(Of Representado)
        Public ReadOnly Property ListaAfiliadosUTEDyC() As List(Of Representado)
            Get
                If _ListaAfiliadosUTEDyC Is Nothing Then
                    If _ListaEmpleadoUTEDyC Is Nothing Then
                        _ListaEmpleadoUTEDyC = Representado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                    End If
                    Dim result As New List(Of Representado)
                    If _ListaEmpleadoUTEDyC IsNot Nothing Then
                        result = _ListaEmpleadoUTEDyC.FindAll(Function(x) x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AfiliadoPermanente Or x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AfiliadoTemporario Or x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AfiliadoRamaXReunion)
                    End If
                    'If result Is Nothing Then
                    '    Throw New Exception("No existen Afiliados para la Empresa")
                    'End If
                    _ListaAfiliadosUTEDyC = result
                End If
                Return _ListaAfiliadosUTEDyC
            End Get
        End Property
        Private _ListaAportantesUTEDyC As List(Of Representado)
        Public ReadOnly Property ListaAportantesUTEDyC() As List(Of Representado)
            Get
                If _ListaAportantesUTEDyC Is Nothing Then
                    If _ListaEmpleadoUTEDyC Is Nothing Then
                        _ListaEmpleadoUTEDyC = Representado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                    End If
                    Dim result As New List(Of Representado)
                    If _ListaEmpleadoUTEDyC IsNot Nothing Then
                        result = _ListaEmpleadoUTEDyC.FindAll(Function(x) x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AportantePermanente Or x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AportanteTemporario)
                    End If
                    'If result Is Nothing Then
                    '    Throw New Exception("No existen Aportantes para la Empresa")
                    'End If
                    _ListaAportantesUTEDyC = result
                End If
                Return _ListaAportantesUTEDyC
            End Get
        End Property
        Private _ListaRamasXReunion As List(Of Representado)
        Public ReadOnly Property ListaRamasXReunion() As List(Of Representado)
            Get
                If _ListaRamasXReunion Is Nothing Then
                    If _ListaEmpleadoUTEDyC Is Nothing Then
                        _ListaEmpleadoUTEDyC = Representado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                    End If
                    Dim result As New List(Of Representado)
                    If _ListaEmpleadoUTEDyC IsNot Nothing Then
                        result = _ListaEmpleadoUTEDyC.FindAll(Function(x) x.IdTipoAfiliado = Enumeradores.TipoAfiliado.AportanteRamaXReunion)
                    End If
                    _ListaRamasXReunion = result
                End If
                Return _ListaRamasXReunion
            End Get
        End Property

        Public ReadOnly Property CantidadTotalUTEDyC() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaEmpleadoUTEDyC Is Nothing Then
                    result = ListaEmpleadoUTEDyC.Count
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property CantidadAfiliadosUTEDyC() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaAfiliadosUTEDyC Is Nothing Then
                    result = ListaAfiliadosUTEDyC.Count
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property CantidadRamasXReunion() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaRamasXReunion Is Nothing Then
                    result = ListaRamasXReunion.Count
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property CantidadAportantesUTEDyC() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaAportantesUTEDyC Is Nothing Then
                    result = ListaAportantesUTEDyC.Count
                End If
                Return result
            End Get
        End Property
        ' OSPEDyC
        Private _ListaEmpleadoOSPEDyC As List(Of Afiliado)
        Public ReadOnly Property ListaEmpleadoOSPEDyC() As List(Of Afiliado)
            Get
                If _ListaEmpleadoOSPEDyC Is Nothing Then
                    _ListaEmpleadoOSPEDyC = Afiliado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                End If
                Return _ListaEmpleadoOSPEDyC
            End Get
        End Property
        Private _ListaEmpleadoOSPEDyC_Titulares As List(Of Afiliado)
        Public ReadOnly Property ListaEmpleadoOSPEDyC_Titulares() As List(Of Afiliado)
            Get
                If _ListaEmpleadoOSPEDyC_Titulares Is Nothing Then
                    If _ListaEmpleadoOSPEDyC Is Nothing Then
                        _ListaEmpleadoOSPEDyC = Afiliado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                    End If
                    Dim result As New List(Of Afiliado)
                    If _ListaEmpleadoOSPEDyC IsNot Nothing Then
                        result = _ListaEmpleadoOSPEDyC.FindAll(Function(x) x.Parent = 0)
                    End If
                    _ListaEmpleadoOSPEDyC_Titulares = result
                End If
                Return _ListaEmpleadoOSPEDyC_Titulares
            End Get
        End Property
        Private _ListaEmpleadoOSPEDyC_Familiares As List(Of Afiliado)
        Public ReadOnly Property ListaEmpleadoOSPEDyC_Familiares() As List(Of Afiliado)
            Get
                If _ListaEmpleadoOSPEDyC_Familiares Is Nothing Then
                    If _ListaEmpleadoOSPEDyC Is Nothing Then
                        _ListaEmpleadoOSPEDyC = Afiliado.TraerTodosXEmpresa(CodEmpresa, Dependencia)
                    End If
                    Dim result As New List(Of Afiliado)
                    If _ListaEmpleadoOSPEDyC IsNot Nothing Then
                        result = _ListaEmpleadoOSPEDyC.FindAll(Function(x) x.Parent > 0)
                    End If
                    _ListaEmpleadoOSPEDyC_Familiares = result
                End If
                Return _ListaEmpleadoOSPEDyC_Familiares
            End Get
        End Property

        Public ReadOnly Property CantidadEmpleadosOSPEDyC() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaEmpleadoOSPEDyC Is Nothing Then
                    result = ListaEmpleadoOSPEDyC.Count
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property CantidadEmpleadosOSPEDyC_Titulares() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaEmpleadoOSPEDyC_Titulares Is Nothing Then
                    result = ListaEmpleadoOSPEDyC_Titulares.Count
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property CantidadEmpleadosOSPEDyC_Familiares() As Integer
            Get
                Dim result As Integer = 0
                If Not ListaEmpleadoOSPEDyC_Familiares Is Nothing Then
                    result = ListaEmpleadoOSPEDyC_Familiares.Count
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Lazy Load / Read Only"
        Public ReadOnly Property StrCodEmpresa11() As String
            Get
                Dim result As String = ""
                If CodEmpresa > 0 Then
                    result = Right("00000000000" & CodEmpresa.ToString, 11)
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property StrCodEmpresa5() As String
            Get
                Dim result As String = ""
                If CodEmpresa > 0 Then
                    result = Right("00000" & CodEmpresa.ToString, 5)
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property StrDependencia() As String
            Get
                Return Right("00" & Dependencia.ToString, 2)
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()
            IdEntidad = 0
            CodEmpresa = 0
            Denominacion = ""
            DenominacionCarnet = ""
            Dependencia = 0
            CUIT = 0
            IdSeccional = 0
            Direccion = ""
            IdProvincia = 0
            TipoEntidad = ""
            Contacto_DDN = 0
            Contacto_Telefono = ""
            Localidad = ""
            CodPostal = 0
            Email = ""
            Procedencia = ""
            DireccionAlternativa = ""
        End Sub
        Sub New(ByVal idEmpresa As Integer)
            Dim objImportar As Empresa = TraerUno(idEmpresa)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            CodEmpresa = objImportar.CodEmpresa
            Denominacion = objImportar.Denominacion
            DenominacionCarnet = objImportar.DenominacionCarnet
            Dependencia = objImportar.Dependencia
            CUIT = objImportar.CUIT
            IdSeccional = objImportar.IdSeccional
            Direccion = objImportar.Direccion
            IdProvincia = objImportar.IdProvincia
            TipoEntidad = objImportar.TipoEntidad
            Contacto_DDN = objImportar.Contacto_DDN
            Contacto_Telefono = LUM.LUM.DejarNumeros(objImportar.Contacto_Telefono)
            Localidad = objImportar.Localidad
            CodPostal = objImportar.CodPostal
            Email = objImportar.Email
            Procedencia = objImportar.Procedencia
            DireccionAlternativa = objImportar.DireccionAlternativa
            IdTipoEmpresa = objImportar.IdTipoEmpresa
            IdSubTipoEmpresa = objImportar.IdSubTipoEmpresa
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal IdEntidad As Long) As Empresa
            Dim result As Empresa = DAL_Empresa.TraerUno(IdEntidad)
            'If result Is Nothing Then
            '    Throw New Exception("No existen Empresas para la búsqueda")
            'End If
            Return result
        End Function
        Public Shared Function TraerUnoXCUIT(ByVal CUIT As Long) As Empresa
            Dim listaTemp As List(Of Empresa) = TraerTodosXCuit(CUIT)
            Dim result As Empresa = listaTemp.Find(Function(x) x.Dependencia = 0)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCodEmpresa(CodEmpresa As Long) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXCodEmpresa(CodEmpresa)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXDenominacion(Denominacion As String) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXDenominacion(Denominacion)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCuit(CUIT As Long) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXCuit(CUIT)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXEmpleado(IdAfiliado As Integer) As List(Of Entidad.Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXEmpleado(IdAfiliado)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para el Afiliado")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXSeccional(IdSeccional As Integer) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXSeccional(IdSeccional)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la Seccional")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXSeccionalConNominaActiva(IdSeccional As Integer) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXSeccionalConNominaActiva(IdSeccional)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        ' Otros
        Public Function ToDTO() As DTO.DTO_Empresa
            Dim result As New DTO.DTO_Empresa With {
                .IdEntidad = IdEntidad,
                .CodEmpresa = CodEmpresa,
                .Denominacion = Denominacion,
                .DenominacionCarnet = DenominacionCarnet,
                .Dependencia = Dependencia,
                .CUIT = CUIT,
                .IdSeccional = IdSeccional,
                .Direccion = Direccion,
                .IdProvincia = IdProvincia,
                .TipoEntidad = TipoEntidad,
                .Contacto_DDN = Contacto_DDN,
                .Contacto_Telefono = Contacto_Telefono,
                .CodPostal = CodPostal,
                .Email = Email,
                .Procedencia = Procedencia,
                .DireccionAlternativa = DireccionAlternativa,
                .IdTipoEmpresa = IdTipoEmpresa,
                .IdSubTipoEmpresa = IdSubTipoEmpresa,
                .StrDependencia = StrDependencia,
                .StrCodEmpresa11 = StrCodEmpresa11,
                .StrCodEmpresa5 = StrCodEmpresa5,
                .CantidadTotalUTEDyC = CantidadTotalUTEDyC,
                .CantidadAfiliadosUTEDyC = CantidadAfiliadosUTEDyC,
                .CantidadRamasXReunion = CantidadRamasXReunion,
                .CantidadAportantesUTEDyC = CantidadAportantesUTEDyC,
                .CantidadEmpleadosOSPEDyC = CantidadEmpleadosOSPEDyC,
                .CantidadEmpleadosOSPEDyC_Titulares = CantidadEmpleadosOSPEDyC_Titulares,
                .CantidadEmpleadosOSPEDyC_Familiares = CantidadEmpleadosOSPEDyC_Familiares
            }
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        'Public Sub Alta()
        '    ValidarAlta()
        '    DAL_Empresa.Alta(Me)
        'End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Empresa.Baja(Me)
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Empresa.Modifica(Me)
        'End Sub
        ' Otros
        'Public Function ToDTO() As DTO.DTO_Empresa
        '    Dim result As New DTO.DTO_Empresa
        '    result.IdEntidad = IdEntidad
        '    Return result
        'End Function
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
            'Dim cantidad As Integer = DAL_Empresa.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Empresa
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Empresa
        Public Property IdEntidad() As Long
        Public Property CodEmpresa() As Long
        Public Property Denominacion() As String
        Public Property DenominacionCarnet() As String
        Public Property Dependencia() As Integer
        Public Property CUIT() As Long
        Public Property IdSeccional() As Integer
        Public Property Direccion() As String
        Public Property IdProvincia() As Integer
        Public Property TipoEntidad() As String
        Public Property Contacto_DDN() As Integer
        Public Property Contacto_Telefono() As String
        Public Property Localidad() As String
        Public Property CodPostal() As Integer
        Public Property Email() As String
        Public Property Procedencia() As String
        Public Property DireccionAlternativa() As String

        ' Cantidades
        Public Property CantidadTotalUTEDyC() As Integer
        Public Property CantidadAfiliadosUTEDyC() As Integer
        Public Property CantidadRamasXReunion() As Integer
        Public Property CantidadAportantesUTEDyC() As Integer
        Public Property CantidadEmpleadosOSPEDyC() As Integer
        Public Property CantidadEmpleadosOSPEDyC_Titulares() As Integer
        Public Property CantidadEmpleadosOSPEDyC_Familiares() As Integer

        ' Enlatado
        Public Property IdTipoEmpresa() As Integer
        Public Property IdSubTipoEmpresa() As Integer



        Public Property StrDependencia() As String
        Public Property StrCodEmpresa11() As String
        Public Property StrCodEmpresa5() As String

    End Class ' DTO_Empresa
End Namespace ' DTO