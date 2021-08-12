Option Explicit On
Option Strict On

<Serializable()>
Public MustInherit Class Persona
    Inherits DBE
#Region " Atributos/Propiedades "
    Public Property NroDocumento() As Long = 0
    Public Property TipoDocumento() As String = ""
    Public Property IdTipoDocumento() As Integer = 0
    Public Property ApellidoNombre() As String = ""
    Public Property FechaNacimiento() As Date? = Nothing
    Public Property CUIL() As Long = 0
    Public Property IdSexo() As Integer = 0
    Public Property Nacionalidad() As Char = CChar("")
    Public Property IdEstadoCivil() As Integer = 0
#End Region
#Region " Lazy Load / ReadOnly "
    Public ReadOnly Property LngFechaNacimiento() As Long
        Get
            Dim result As Long = 0
            If FechaNacimiento.HasValue Then
                result = CLng(FechaNacimiento.Value.Year.ToString & Right("00" & FechaNacimiento.Value.Month.ToString, 2) & Right("00" & FechaNacimiento.Value.Day.ToString, 2))
            End If
            Return result
        End Get
    End Property
    Public ReadOnly Property StrCuilConGuiones() As String
        Get
            Dim result As String = ""
            If CUIL.ToString.Length = 11 Then
                result = CUIL.ToString
                result = result.Insert(2, "-")
                result = result.Insert(11, "-")
            End If
            Return result
        End Get
    End Property
    Public ReadOnly Property Edad() As Integer
        Get
            Dim result As Integer = 0
            If FechaNacimiento.HasValue Then
                result = CInt(DateDiff(DateInterval.Year, FechaNacimiento.Value, CDate(Today())))
                Edad = result
            End If
            Return Edad
        End Get
    End Property
    Public ReadOnly Property Sexo() As String
        Get
            Dim result As String = ""
            If IdSexo <> 0 Then
                Select Case IdSexo
                    Case Enums.Sexo.Masculino
                        result = "Masculino"
                    Case Enums.Sexo.Femenino
                        result = "Femenino"
                    Case Else

                End Select
            End If
            Return result
        End Get
    End Property
    Public ReadOnly Property StrNacionalidad() As String
        Get
            Dim result As String = ""
            If Nacionalidad <> "" Then
                Select Case Nacionalidad
                    Case CChar("A")
                        result = "Argentina"
                    Case CChar("E")
                        result = "Extranjero"
                    Case Else

                End Select
            End If
            Return result
        End Get
    End Property
#End Region
End Class ' PErsona
