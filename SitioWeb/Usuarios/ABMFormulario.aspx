<%@ Page Title="2 - Formulario" Language="VB" MasterPageFile="~/Usuarios/MPUsuarios.master" AutoEventWireup="false" CodeFile="ABMFormulario.aspx.vb" Inherits="Usuarios_ABMFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content2" runat="Server">
    <table style="width: 70%;" border="1">
        <tr>
            <td>IdEntidad</td>
            <td>
                <asp:Label ID="LblIdentidad" runat="server"></asp:Label>
            </td>
            <td rowspan="3" style="vertical-align:top;">
                <asp:CheckBoxList ID="CBL_Permisos" runat="server"></asp:CheckBoxList>
                <asp:Literal ID="Lit_Permisos" runat="server"></asp:Literal>
            </td>
            <td rowspan="3" style="vertical-align:top;">
                <asp:CheckBoxList ID="CBL_Areas" runat="server"></asp:CheckBoxList>
                <asp:Literal ID="Lit_Familias" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>URL</td>
            <td>
                <asp:TextBox ID="TxtURL" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="BtnAlta" runat="server" Text="Alta" />
                            <asp:Button ID="BtnBaja" runat="server" Text="Baja" />
                            <asp:Button ID="BtnModifica" runat="server" Text="Modifica" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GrillaFormularios" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IdEntidad" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
            </td>
                <td colspan="2">
                <asp:GridView ID="GrillaFamilias" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:BoundField DataField="IdEntidad" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
