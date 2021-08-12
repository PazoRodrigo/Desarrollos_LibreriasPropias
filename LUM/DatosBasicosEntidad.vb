Option Explicit On
Option Strict On

<Serializable()>
Public MustInherit Class DBE 'DatosBasicosEntidad
#Region " Atributos / Propiedades"
    Public Property FechaAlta() As Nullable(Of Date) = Nothing
    Public Property FechaBaja() As Nullable(Of Date) = Nothing
    Public Property FechaModifica() As Nullable(Of Date) = Nothing
    Public Property IdUsuarioAlta() As Integer = 0
    Public Property IdUsuarioBaja() As Integer = 0
    Public Property IdUsuarioModifica() As Integer = 0
    Public Property IdMotivoBaja() As Integer = 0
#End Region
#Region " Read Only "
    Public ReadOnly Property LngFechaAlta() As Long
        Get
            Dim result As Long = 0
            If FechaAlta.HasValue Then
                result = CLng(Year(FechaAlta.Value).ToString & Right("00" & Month(FechaAlta.Value).ToString, 2) & Right("00" & Day(FechaAlta.Value).ToString, 2))
            End If
            Return result
        End Get
    End Property
    Public ReadOnly Property LngFechaBaja() As Long
        Get
            Dim result As Long = 0
            If FechaBaja.HasValue Then
                result = CLng(Year(FechaBaja.Value).ToString & Right("00" & Month(FechaBaja.Value).ToString, 2) & Right("00" & Day(FechaBaja.Value).ToString, 2))
            End If
            Return result
        End Get
    End Property

#End Region
#Region " Lazy Load "
    'Public Property _ObjMotivoBaja() As Comunes.Entidad.MotivoBaja
    'Public ReadOnly Property ObjMotivoBaja() As Comunes.Entidad.MotivoBaja
    '    Get
    '        If _ObjMotivoBaja Is Nothing Then
    '            _ObjMotivoBaja = Comunes.Entidad.MotivoBaja.TraerUno(IdMotivoBaja)
    '        End If
    '        Return _ObjMotivoBaja
    '    End Get
    'End Property
#End Region
End Class


Namespace DTO
    Public MustInherit Class DTO_DBE

#Region " Atributos / Propiedades"
        Public Property FechaAlta() As Long = 0
        Public Property FechaBaja() As Long = 0
        Public Property FechaModifica() As Long = 0
        Public Property IdUsuarioAlta() As Integer = 0
        Public Property IdUsuarioBaja() As Integer = 0
        Public Property IdUsuarioModifica() As Integer = 0
        Public Property IdMotivoBaja() As Integer = 0
#End Region
    End Class ' DTO_TipoPago
End Namespace ' DTO
