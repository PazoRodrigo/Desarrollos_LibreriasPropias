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
    Public Function HaciendoLogIN(a As String, b As String) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New DTO.DTO_Usuario
            Dim ListaRoles As New List(Of DTO.DTO_Rol)
            'Dim result As New List(Of DTO.DTO_Usuario)
            'Dim lista As List(Of Entidad.Usuario) = Entidad.Usuario.TraerTodos
            'If Not lista Is Nothing Then
            '    For Each item As Entidad.Usuario In lista
            '        result.Add(item.ToDTO)
            '    Next
            'End If
            Dim ObjUs As Entidad.Usuario = Entidad.Usuario.HacerLogIN(a, b)
            result = ObjUs.ToDTO
            For Each item As Entidad.Rol In ObjUs.ListaRolesInicio
                ListaRoles.Add(item.ToDTO)
            Next
            result.ListaRoles = ListaRoles
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
    Public Function HaciendoLogIN2(Nombre As String, Password As String) As wsTransfer
        Dim ws As New wsTransfer
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

        'Dim ws As New wsTransfer
        'Try
        'Dim objResult As New admUsuarios.DTO.DTO_Usuario
        'Dim objUsuario As admUsuarios.Entidad.Usuario = admUsuarios.Entidad.Usuario.HacerLogIN(Nombre, Password)
        'If objUsuario IsNot Nothing Then
        '    objResult = objUsuario.ToDTO
        '    '    For Each item As admUsuarios.Entidad.Rol In objUsuario.ListaRoles
        '    '        objResult.ListaRoles.Add(item.ToDTO)
        '    '    Next
        'End If
        'ws.data = objResult
        '    ws.todoOk = True
        '    ws.mensaje = ""
        'Catch ex As Exception
        '    ws.todoOk = False
        '    ws.mensaje = ex.Message
        '    ws.data = Nothing
        'End Try
        'Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodos() As wsTransfer
        Dim ws As New wsTransfer
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
End Class