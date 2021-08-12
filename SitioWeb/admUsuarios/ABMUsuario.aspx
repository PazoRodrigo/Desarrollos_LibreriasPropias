<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeFile="ABMUsuario.aspx.vb" Inherits="admUsuarios_ABMUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
                            <asp:TextBox ID="txt" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            nivelAcceso
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            nombre
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            logIn
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            password
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            nroDocumento
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            mail
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            idSeccional
                        </td>
                        <td align="left" style="width: 70%">
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
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
                            <asp:GridView ID="grillaEntidades" runat="server">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
