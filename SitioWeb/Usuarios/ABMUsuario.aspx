<%@ Page Title="4 - Usuario" Language="VB" MasterPageFile="~/Usuarios/MPUsuarios.master" AutoEventWireup="false" CodeFile="ABMUsuario.aspx.vb" Inherits="Usuarios_ABMUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content2" runat="Server">
    <script src="../Scripts/Forms/Frm_Usuario.js"></script>

    <table style="width: 90%;" border="1">
        <tr>
            <td style="width: 10%;">IdEntidad</td>
            <td style="width: 10%;">
                <asp:Label ID="LblIdentidad" runat="server"></asp:Label>
            </td>
            <td rowspan="2" style="width: 80%;">
                <asp:GridView ID="GrillaPerfiles" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:BoundField DataField="IdEntidad" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="IdRol" HeaderText="Rol" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CB_Perfil" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
                <asp:TextBox ID="TxtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="BtnAlta" runat="server" Text="Alta" />
                            <asp:Button ID="BtnBaja" runat="server" Text="Baja" />
                            <asp:Button ID="BtnModifica" runat="server" Text="Modifica" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Afiliado" />
                            <asp:Button ID="Button2" runat="server" Text="Empleado" />
                            <asp:Button ID="Button3" runat="server" Text="Empresa" />
                            <asp:Button ID="BtnLogIN" runat="server" Text="LogIN" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GrillaUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IdEntidad" HeaderText="IdEntidad" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>

            </td>
        </tr>
    </table>
</asp:Content>

