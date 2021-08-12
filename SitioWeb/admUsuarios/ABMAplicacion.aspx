<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ABMAplicacion.aspx.vb" Inherits="admUsuarios_ABMAplicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td align="center" valign="top" style="width: 60%">
                <table width="100%">
                    <tr>
                        <td align="right" style="width: 30%">
                            Id:
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            nivelAcceso:&nbsp;
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="txtNivelAcceso" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            nombre
                            :
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            Base de Datos :
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="txtBaseDeDatos" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            Conexión :
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="txtConexion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td align="center" style="width: 33%">
                                        <asp:Button ID="btnAlta" runat="server" Text="Alta" Width="80%" />
                                    </td>
                                    <td align="center" style="width: 33%">
                                        <asp:Button ID="btnBaja" runat="server" Text="Baja" Width="80%" />
                                    </td>
                                    <td align="center" style="width: 33%">
                                        <asp:Button ID="btnModifica" runat="server" Text="Modifica" Width="80%" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" style="width: 40%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="grillaEntidades" runat="server" DataKeyNames="idEntidad">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

