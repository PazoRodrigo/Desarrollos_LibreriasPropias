Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports ClasesEmpresa

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsRepresentado
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodosXApellidoNombre(ApellidoNombre As String) As wsTransfer
        Dim ws As New wsTransfer
        Try
            Dim result As New List(Of DTO.DTO_Representado)
            Dim lista As List(Of ClasesEmpresa.Entidad.Representado) = Entidad.Representado.TraerTodosXApellidoNombre(ApellidoNombre)
            For Each item As Entidad.Representado In lista
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