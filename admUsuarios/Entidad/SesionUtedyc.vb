
Namespace Entidad
    Public Class SesionUtedyc
#Region "Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Usuario As Entidad.Usuario = Nothing
        Public Property Token As String = ""
        Public Property IdRolSeleccionado As Integer = 0
#End Region
        'Sub New(login As LoginRequest)
        '    Dim ObjResult As New Usuario(login.UserLogin, login.Password)
        '    If ObjResult Is Nothing Then
        '        Throw New Exception()
        '    End If
        '    Usuario = ObjResult
        'End Sub
        Public Function ToDTO() As DTO.DTO_SesionUtedyc
            Dim objDTO As New DTO.DTO_SesionUtedyc
            objDTO.IdEntidad = Usuario.IdEntidad
            objDTO.Usuario = Usuario.ToDTO
            objDTO.Token = Token
            objDTO.IdRolSeleccionado = IdRolSeleccionado
            Return objDTO
        End Function
    End Class

End Namespace
Namespace DTO
    Public Class DTO_SesionUtedyc
#Region "Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Usuario As DTO.DTO_Usuario = Nothing
        Public Property Token As String = ""
        Public Property IdRolSeleccionado As Integer = 0
#End Region

    End Class
End Namespace

