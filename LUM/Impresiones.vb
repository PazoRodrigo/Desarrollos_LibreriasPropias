Imports System.IO

    Public Class Impresiones
    Public Shared Sub ImprimirAPDF(ByVal Reporte As DevExpress.XtraReports.UI.XtraReport, ByVal nombreImpresion As String)
        Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        Using Stream As New MemoryStream
            Dim opts As New DevExpress.XtraPrinting.PdfExportOptions()
            opts.ShowPrintDialogOnOpen = False
            Reporte.ExportToPdf(Stream, opts)
            Stream.Seek(0, SeekOrigin.Begin)
            Response.Clear()
            Response.AddHeader("Content-Type", "application/pdf")
            Response.AddHeader("Cache-Control", "no-cache")
            Response.AddHeader("Accept-Ranges", "none")
            Response.AddHeader("Content-Disposition", "attachment; filename=" & nombreImpresion & ".pdf")
            Stream.WriteTo(Response.OutputStream)
            Stream.Close()
            Response.Flush()
            Response.End()
        End Using
    End Sub
End Class
