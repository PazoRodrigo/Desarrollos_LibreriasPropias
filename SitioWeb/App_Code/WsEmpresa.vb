Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports ClasesEmpresa
'Imports Newtonsoft.Json
Imports System.Web.Script.Services


Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsEmpresa
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerUno(IdEntidad As Integer) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As ClasesEmpresa.DTO.DTO_Empresa = Entidad.Empresa.TraerUno(IdEntidad).ToDTO
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerUnoXCUIT(CUIT As Long) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As ClasesEmpresa.DTO.DTO_Empresa = Entidad.Empresa.TraerUnoXCUIT(CUIT).ToDTO
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXCodEmpresa(CodEmpresa As Long) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXCodEmpresa(CodEmpresa)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXDenominacion(Denominacion As String) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXDenominacion(Denominacion)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXCuit(CUIT As Long) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXCuit(CUIT)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXEmpleado(IdAfiliado As Integer) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXEmpleado(IdAfiliado)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXSeccional(IdSeccional As Integer) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXSeccional(IdSeccional)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXSeccionalConNominaActiva(IdSeccional As Integer) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of ClasesEmpresa.Entidad.Empresa) = Entidad.Empresa.TraerTodosXSeccionalConNominaActiva(IdSeccional)
            For Each item As Entidad.Empresa In lista
                result.Add(item.ToDTO)
            Next
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
End Class