Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports admUsuarios

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsUsuario
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Usuario)
            Dim lista As List(Of Entidad.Usuario) = Entidad.Usuario.TraerTodos
            If Not lista Is Nothing Then
                For Each item As Entidad.Usuario In lista
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
    Public Function TraerTodosXArea(IdArea As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Usuario)
            Dim obj As New Entidad.Area(IdArea)
            Dim lista As List(Of Entidad.Usuario) = obj.ListaUsuarios
            If Not lista Is Nothing Then
                For Each item As Entidad.Usuario In lista
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
    Public Function TraerTodosXPerfil(IdPerfil As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Usuario)
            Dim lista As List(Of Entidad.Usuario) = admUsuarios.Entidad.Usuario.TraerTodosXPerfil(IdPerfil)
            If Not lista Is Nothing Then
                For Each item As Entidad.Usuario In lista
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
    Public Function Alta(entidad As Entidad.Usuario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioAlta = 1
            entidad.AltaJson()
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
    Public Function Modifica(entidad As Entidad.Usuario) As Transfer
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
End Class