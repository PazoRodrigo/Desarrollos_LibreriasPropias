Option Explicit On
Option Strict On

'Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
'Imports System.Reflection
'Imports System.Web.Mail
Imports System.Net.Mail
Imports System.Configuration
Imports System.Text

Public Class LUM
    ' LUM
    Public Shared Function getRandomText() As String
        Dim s As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 62)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
    ' Fechas
    Public Shared ReadOnly Property Fecha_DateToLng(Fecha As Nullable(Of Date)) As Long
        Get
            Dim result As Long = 0
            If Fecha.HasValue Then
                result = CLng(Year(Fecha.Value).ToString & Right("00" & Month(Fecha.Value).ToString, 2) & Right("00" & Day(Fecha.Value).ToString, 2))
            End If
            Return result
        End Get
    End Property
    Public Shared ReadOnly Property Fecha_LngToDate(LngFecha As Long) As Nullable(Of Date)
        Get
            Dim result As Nullable(Of Date) = Nothing
            If LngFecha.ToString.Length = 8 Then
                If IsNumeric(LngFecha) Then
                    If LngFecha > 0 Then
                        result = CDate(Right(LngFecha.ToString, 2) & "/" & Right(Left(LngFecha.ToString, 4), 2) & "/" & Left(LngFecha.ToString, 4))
                    End If
                End If
            End If
            Return result
        End Get
    End Property
    Public Shared Function SoloNumeros(ByVal value As String) As Integer
        Dim returnVal As Integer = 0
        Try
            Dim collection As MatchCollection = Regex.Matches(value, "\d+")
            For Each m As Match In collection
                returnVal += CInt(m.ToString())
            Next
            Return returnVal
        Catch ex As Exception
            Return returnVal
        End Try
    End Function
    Public Shared Sub LlenarCombo(ByRef DdL As DropDownList, ByVal listaObjetos As List(Of Object), ByVal datatext As String, ByVal datavalue As String,
                                   ByVal tieneSeleccionar As Boolean, textoSeleccionar As String)
        Try
            DdL.Items.Clear()
            DdL.DataSource = listaObjetos
            DdL.DataTextField = datatext
            DdL.DataValueField = datavalue
            DdL.DataBind()
            If tieneSeleccionar Then
                Dim Seleccionar As New ListItem("(" & textoSeleccionar & ")", CStr(0))
                DdL.Items.Insert(0, Seleccionar)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub setProperty(ByRef prop As Object, ByVal dr As Data.DataRow, ByVal fieldNAme As String)
        Try
            If dr.Table.Columns.Contains(fieldNAme) Then
                If dr(fieldNAme) IsNot DBNull.Value Then
                    prop = dr(fieldNAme)
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error al asignar el valor", ex)
        End Try
    End Sub
    Public Shared Function DejarNumerosPuntos(cadenaTexto As String) As String
        Const listaNumeros = "0123456789."
        Dim result As String = ""
        Dim i As Integer
        cadenaTexto = Trim$(cadenaTexto)
        If Len(cadenaTexto) > 0 Then
            For i = 1 To Len(cadenaTexto)
                If CBool(InStr(listaNumeros, Mid$(cadenaTexto, i, 1))) Then
                    result = result + Mid$(cadenaTexto, i, 1)
                End If
            Next
        End If
        Return result
    End Function
    Public Shared Function DejarNumeros(cadenaTexto As String) As String
        Const listaNumeros = "0123456789"
        Dim result As String = ""
        Dim i As Integer
        cadenaTexto = Trim$(cadenaTexto)
        If Len(cadenaTexto) > 0 Then
            For i = 1 To Len(cadenaTexto)
                If CBool(InStr(listaNumeros, Mid$(cadenaTexto, i, 1))) Then
                    result = result + Mid$(cadenaTexto, i, 1)
                End If
            Next
        End If
        Return result
    End Function
    Public Shared Function ObtenerNumeroAleatorio(maximo As Integer) As Long
        Randomize()
        'Dim result As Long = CLng(Rnd() * 100000000)
        'Return result
        Return CLng(Math.Ceiling(Rnd() * maximo)) + 1
    End Function
    ' Validadores
    Public Shared Function validarPeriodo(varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        If varTempo <> "" Then
            If varTempo.ToString.Length() <> 7 Then
                sError &= "Registro :" & regLinea & " - Existen inconsistencias. Período (7) caracteres. <br />"
            Else
                If Left(Right(CStr(varTempo), 5), 1) <> "/" Then
                    sError &= "Registro :" & regLinea & " - Existen inconsistencias. Período (mm/yyyy). <br />"
                Else
                    If (CInt(Left(CStr(varTempo), 2)) < 1 Or CInt(Left(CStr(varTempo), 2)) > 12) Then
                        sError &= "Registro :" & regLinea & " - Existen inconsistencias. Período Mes 01 a 12. <br />"
                    ElseIf CInt(Right(CStr(varTempo), 4)) > Today.Year Then
                        sError &= "Registro :" & regLinea & " - Existen inconsistencias. Año no mayor que el actual. <br />"
                    Else
                        Dim Actual As String = CStr(Today.Year) & CStr(Right(0 & Today.Month, 2))
                        Dim periodoIngresado As String = CStr(Right(CStr(varTempo), 4)) & CStr(Left(CStr(varTempo), 2))
                        If CInt(Actual) < CInt(periodoIngresado) Then
                            sError &= "Registro :" & regLinea & " - Existen inconsistencias. Período no mayor que el actual. <br />"
                        End If
                    End If
                End If
            End If
        End If
        Return sError
    End Function
    Public Shared Function validarFecha(varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        If varTempo.Length = 0 Then
            sError &= "Registro :" & regLinea & " - Existen datos vacíos. Fecha. <br />"
        ElseIf varTempo.Length <> 10 Then
            sError &= "Registro :" & regLinea & " - Existen inconsistencias. Fecha. <br />"
        End If
        Return sError
    End Function
    Public Shared Function validarImporte(varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        If Not IsNumeric(varTempo) Then
            sError &= "Registro :" & regLinea & " - Existen inconsistencias. El Importe debe ser de tipo numérico. <br />"
        Else
            If CDec(varTempo) < 0 Then
                sError &= "Registro :" & regLinea & " - Existen inconsistencias. El Importe debe ser mayor o igual que 0. <br />"
            End If
        End If
        Return sError
    End Function
    Public Shared Function validarImporteMayor0(varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        If Not IsNumeric(varTempo) Then
            sError &= "Registro :" & regLinea & " - Existen inconsistencias. El Importe debe ser de tipo numérico. <br />"
        Else
            If CDec(varTempo) <= 0 Then
                sError &= "Registro :" & regLinea & " - Existen inconsistencias. El Importe debe ser mayor que 0. <br />"
            End If
        End If
        Return sError
    End Function
    Public Shared Function validarCuit(varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        Dim temp As String = Replace(varTempo, "-", "")
        If Not IsNumeric(temp) Then
            sError &= "Registro :" & regLinea & " - Existen inconsistencias. El CUIT debe ser de tipo numérico. <br />"
        Else
            If temp.Length <> 11 Then
                sError &= "Registro :" & regLinea & " - Existen inconsistencias. La cantidad de dígitos es incorrecta. <br />"
            End If
        End If
        Return sError
    End Function
    Public Shared Function validarSeccional(listaSeccionales As List(Of Integer), varTempo As String, regLinea As Integer) As String
        Dim sError As String = ""
        Dim encontroSec As Boolean = False
        For Each sec As Integer In listaSeccionales
            If CInt(varTempo) = sec Then
                encontroSec = True
            End If
        Next
        If Not encontroSec Then
            sError &= "Registro :" & regLinea & " - Existen inconsistencias. El código de la seccional no Existe. <br />"
        End If
        Return sError
    End Function
    Public Shared Function validateEmail(emailAddress As String) As Boolean
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(emailAddress) Then
            Return True
        Else
            Return False
        End If
    End Function
    ' Validar CBU
    Public Shared Function validarCBU(cbu As String) As Boolean
        If validarLongitudCBU(cbu) Then
            Return validarCodigoBanco(cbu.Substring(0, 8)) And validarCuenta(cbu.Substring(8, 14))
        End If
        Return False
    End Function
    Private Shared Function validarLongitudCBU(cbu As String) As Boolean
        If cbu.Length <> 22 Then Return False
        Return True
    End Function
    Private Shared Function validarCodigoBanco(codigo As String) As Boolean
        If codigo.Length <> 8 Then Return False

        Dim strbanco = codigo.Substring(0, 3)
        Dim banco As List(Of Integer) = toIntLista(strbanco)

        Dim digitoVerificador1 As Integer = CInt(codigo.Substring(3, 1))
        Dim strsucursal As String = codigo.Substring(4, 3)
        Dim sucursal As List(Of Integer) = toIntLista(strsucursal)

        Dim digitoVerificador2 As Integer = CInt(codigo.Substring(7, 1))

        Dim suma As Integer = banco(0) * 7 + banco(1) * 1 + banco(2) * 3 + digitoVerificador1 * 9 + sucursal(0) * 7 + sucursal(1) * 1 + sucursal(2) * 3

        Dim diferencia As Integer = (10 - (suma Mod 10)) Mod 10
        Return diferencia = digitoVerificador2
    End Function
    Private Shared Function validarCuenta(strcuenta As String) As Boolean
        Dim cuenta As List(Of Integer) = toIntLista(strcuenta)
        If cuenta.Count <> 14 Then Return False
        Dim digitoVerificador As Integer = cuenta(13)
        Dim suma As Integer = cuenta(0) * 3 + cuenta(1) * 9 + cuenta(2) * 7 + cuenta(3) * 1 + cuenta(4) * 3 + cuenta(5) * 9 + cuenta(6) * 7 + cuenta(7) * 1 + cuenta(8) * 3 + cuenta(9) * 9 + cuenta(10) * 7 + cuenta(11) * 1 + cuenta(12) * 3
        Dim diferencia As Integer = (10 - (suma Mod 10)) Mod 10
        Return diferencia = digitoVerificador
    End Function
    Private Shared Function toIntLista(str As String) As List(Of Integer)
        Dim li As New List(Of Integer)
        For Each item As Char In str.ToCharArray
            If IsNumeric(item) Then
                li.Add(CInt(item.ToString))
            End If
        Next
        Return li
    End Function
    ' Archivo
    Public Shared Function VarlidarArchivoExcel(pf As HttpPostedFile) As Boolean
        Dim result As Boolean = True

        Return result
    End Function
    Public Shared Sub mensaje(Mensaje__1 As String)
        Dim animacionesEntrada As String() = New String(68) {"bounceIn", "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp", "slideInDown",
            "slideInLeft", "slideInRight", "slideInUp", "fadeIn", "fadeInDown", "fadeInDownBig",
            "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp", "fadeInUpBig",
            "flip", "flipInX", "flipInY", "lightSpeedIn", "rotateIn", "rotateInDownLeft",
            "rotateInDownRight", "rotateInUpLeft", "rotateInUpRight", "bounce", "flash", "pulse",
            "rubberBand", "shake", "swing", "tada", "wobble", "rollIn",
            "zoomIn", "zoomInDown", "zoomInLeft", "zoomInRight", "zoomInUp", "bounceIn",
            "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp", "fadeIn", "fadeInDown",
            "fadeInDownBig", "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp",
            "fadeInUpBig", "flipInX", "flipInY", "lightSpeedIn", "rotateIn", "rotateInDownLeft",
            "rotateInDownRight", "rotateInUpLeft", "rotateInUpRight", "rollIn", "zoomIn", "zoomInDown",
            "zoomInLeft", "zoomInRight", "zoomInUp"}
        Dim animacionesSalida As String() = New String(61) {"bounceOut", "bounceOutDown", "bounceOutLeft", "bounceOutRight", "bounceOutUp", "slideOutDown",
            "slideOutLeft", "slideOutRight", "slideOutUp", "fadeOut", "fadeOutDown", "fadeOutDownBig",
            "fadeOutLeft", "fadeOutLeftBig", "fadeOutRight", "fadeOutRightBig", "fadeOutUp", "fadeOutUpBig",
            "flip", "flipOutX", "flipOutY", "lightSpeedOut", "rotateOut", "rotateOutDownLeft",
            "rotateOutDownRight", "rotateOutUpLeft", "rotateOutUpRight", "hinge", "rollOut", "zoomOut",
            "zoomOutDown", "zoomOutLeft", "zoomOutRight", "zoomOutUp", "bounceOut", "bounceOutDown",
            "bounceOutLeft", "bounceOutRight", "bounceOutUp", "fadeOut", "fadeOutDown", "fadeOutDownBig",
            "fadeOutLeft", "fadeOutLeftBig", "fadeOutRight", "fadeOutRightBig", "fadeOutUp", "fadeOutUpBig",
            "flipOutX", "flipOutY", "lightSpeedOut", "rotateOut", "rotateOutDownLeft", "rotateOutDownRight",
            "rotateOutUpLeft", "rotateOutUpRight", "rollOut", "zoomOut", "zoomOutDown", "zoomOutLeft",
            "zoomOutRight", "zoomOutUp"}
        Dim rnd As New Random()
        Dim iAnimShow As Integer = rnd.[Next](0, 69)
        Dim ianimHide As Integer = rnd.[Next](0, 62)
        Dim mens As String = Mensaje__1.Trim.Replace("""", "").Replace("'", "")
        Dim script As String = (Convert.ToString("$.iGrowl({" + " type: 'error'," + " icon: 'vicons-envelope'," + " message: '") & mens) + "'," + " animShow: '" + animacionesEntrada(iAnimShow) + "'," + " animHide: '" + animacionesSalida(ianimHide) + "'," + " delay: 12000," + " offset : {  y: " & vbTab & "50 } ," + " placement: {  x: " & vbTab & "'center', y: 'top' } " + "});"
        If TypeOf HttpContext.Current.CurrentHandler Is Page Then
            Dim p As Page = DirectCast(HttpContext.Current.CurrentHandler, Page)
            If ScriptManager.GetCurrent(p) IsNot Nothing Then
                ScriptManager.RegisterStartupScript(p, GetType(Page), "Message", script, True)
            Else
                p.ClientScript.RegisterStartupScript(GetType(Page), "Message", script, True)
            End If
        End If
    End Sub
    ' Mail
    Private Shared Sub MandarMailAsync(ByVal para As String, ByVal Concopia As String, ByVal desde As String, ByVal subject As String,
                           ByVal smtpHost As String, ByVal smtpUsername As String, ByVal smtpPassword As String, ByVal Body As String)
        Using mm As New System.Net.Mail.MailMessage(desde, para)
            mm.Subject = subject
            mm.Body = Body
            mm.IsBodyHtml = False
            Dim smtp As New SmtpClient()
            smtp.Host = smtpHost
            smtp.EnableSsl = True
            Dim NetworkCred As New System.Net.NetworkCredential(smtpUsername, smtpPassword)
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = 587
            smtp.Send(mm)
        End Using
    End Sub
    Public Shared Sub ArmarMail_A_AudTraumatologicaAsync(idSolic As String, nombrePaciente As String, nroAfiliado As String, diagnostico As String)
        Try
            Dim Para As String = ConfigurationManager.AppSettings("Mail_DestinatarioAuditoriaCompras")
            Dim ConCopia As String = ConfigurationManager.AppSettings("Mail_DestinatarioPrueba")
            Dim Asunto As String = idSolic & " - " & nroAfiliado & " - " & nombrePaciente & " - " & diagnostico
            Dim smtpHost As String = ConfigurationManager.AppSettings("smtpHost")
            Dim smtpUsername As String = ConfigurationManager.AppSettings("smtpUsername")
            Dim smtpPassword As String = ConfigurationManager.AppSettings("smtpPassword")
            Dim Desde As String = ConfigurationManager.AppSettings("smtpFrom")
            Dim cuerpoMail As String = ""
            Dim email As New Threading.Thread(Sub() MandarMailAsync(Para, ConCopia, Desde, Asunto, smtpHost, smtpUsername, smtpPassword, cuerpoMail))
            email.IsBackground = True
            email.Start()
        Catch ex As SmtpException
            Throw New Exception("Error 0015. \n Comuníquese con el área de Sistemas. Disculpe las molestias.")
        End Try
    End Sub
    ' Mensaje
    Public Shared Sub MensajeSweet(Titulo As String, Mensaje As String, TipoMensaje As String)
        Dim script As String = "swal(""" + Titulo + """,""" + Mensaje.Replace("'", "").Replace("""", "") + """,""" + TipoMensaje + """);"
        If TypeOf HttpContext.Current.CurrentHandler Is Page Then
            Dim p As Page = DirectCast(HttpContext.Current.CurrentHandler, Page)
            If ScriptManager.GetCurrent(p) IsNot Nothing Then
                ScriptManager.RegisterStartupScript(p, GetType(Page), "ms", script, True)
            Else
                p.ClientScript.RegisterStartupScript(GetType(Page), "ms", script, True)
            End If
        End If
    End Sub
End Class ' LUM


