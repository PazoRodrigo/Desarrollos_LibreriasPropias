Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports admUsuarios

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsFormulario
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Formulario)
            Dim lista As List(Of Entidad.Formulario) = Entidad.Formulario.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Formulario In lista
                    result.Add(item.ToDTO)
                Next
            End If
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
    Public Function TraerTodosXRama(IdRama As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Formulario)
            Dim ram As New Entidad.Rama(IdRama)
            Dim lista As List(Of Entidad.Formulario) = ram.ListaFormularios
            If Not lista Is Nothing Then
                For Each item As Entidad.Formulario In lista
                    result.Add(item.ToDTO)
                Next
            End If
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
    Public Function Alta(entidad As Entidad.Formulario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioAlta = 1
            entidad.Alta()
            ws.data = entidad.IdEntidad
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
    Public Function Modifica(entidad As Entidad.Formulario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioModifica = 1
            entidad.Modifica()
            ws.data = entidad.IdEntidad
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
    Public Function Baja(entidad As Entidad.Formulario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioBaja = 1
            entidad.Baja()
            ws.data = entidad.IdEntidad
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    '<WebMethod()>
    'Public Function Baja(IdEntidad As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim entidad As New Entidad.Formulario
    '        entidad.IdUsuarioBaja = 1
    '        Entidad.Baja()
    '        ws.data = Entidad.IdEntidad
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
End Class