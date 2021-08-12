Option Explicit On
Option Strict On

Imports admUsuarios.DataAccessLibrary

Namespace Entidad
    Public Class Menu

#Region " Atributos / Propiedades "
        Public Property Menu() As String = ""

        Public Shared ReadOnly Property ArmarMenu(Accesos As List(Of Acceso)) As String
            Get
                Dim resultStr As String = ""
                Dim listaFamilias As New List(Of Familia)
                If Accesos IsNot Nothing And Accesos.Count > 0 Then
                    Dim ObjFa As New Familia(Accesos(0).IdFamilia)
                    For Each itemLA As Acceso In Accesos
                        If Not listaFamilias.Contains(itemLA.ObjFamilia) Then
                            listaFamilias.Add(itemLA.ObjFamilia)
                            resultStr &= "<br /> " & itemLA.ObjFamilia.Nombre & "<br />"
                        End If
                        resultStr &= " " & itemLA.ObjFormulario.Nombre & "<br />"
                    Next
                End If
                Return resultStr
            End Get
        End Property
        Public Shared ReadOnly Property ArmarMenu(Accesos As List(Of Perfil)) As String
            Get
                Dim resultStr As String = ""

                Return resultStr
            End Get
        End Property

#End Region
#Region " Lazy Load "
#End Region
#Region " Constructores "
        Sub New(Accesos As List(Of Acceso))
            Menu = ArmarMenu(Accesos)
        End Sub
#End Region
#Region " Métodos Estáticos"
#End Region
#Region " Métodos Públicos"
#End Region
#Region " Métodos Privados "

#End Region
    End Class ' Menu
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Menu
#Region " Atributos / Propiedades"
#End Region
    End Class ' DTO_Menu
End Namespace ' DTO