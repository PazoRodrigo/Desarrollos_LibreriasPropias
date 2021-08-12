Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Comunes

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsSeccional
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Seccional)
            Dim lista As List(Of Entidad.Seccional) = Entidad.Seccional.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Seccional In lista
                    If item.Nombre.Length > 0 Then
                        result.Add(item.ToDTO)
                    End If
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