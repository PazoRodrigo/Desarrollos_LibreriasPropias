<%@ Page Title="3 - Perfil" Language="VB" MasterPageFile="~/Usuarios/MPUsuarios.master" AutoEventWireup="false" CodeFile="ABMPerfil.aspx.vb" Inherits="Usuarios_ABMPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content2" runat="Server">
    <table style="width: 90%;" border="1">
        <tr>
            <td style="width: 20%;">IdEntidad</td>
            <td style="width: 20%;">
                <asp:Label ID="LblIdentidad" runat="server"></asp:Label>
            </td>
            <td>
                <asp:GridView ID="GrillaFormularios" runat="server" AutoGenerateColumns="false" DataKeyNames="IdEntidad, IdFamilia">
                    <Columns>
                        <asp:BoundField DataField="IdFamilia" HeaderText="IdFamilia" />
                       <%-- <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ObjFamilia.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="IdEntidad" HeaderText="IdForm" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:TemplateField HeaderText="Visualizar">
                            <ItemTemplate>
                                <asp:CheckBox ID="CB_Visualizar" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agregar">
                            <ItemTemplate>
                                <asp:CheckBox ID="CB_Agregar" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>
                                <asp:CheckBox ID="CB_Modificar" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:CheckBox ID="CB_Eliminar" runat="server" />
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
            </td>
        </tr>
        <tr>
            <td>Rol</td>
            <td>
                <asp:CheckBoxList ID="CBL_Roles" runat="server"></asp:CheckBoxList>
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GrillaPerfiles" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IdEntidad" HeaderText="IdEntidad" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="idRol" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
                <asp:Literal ID="Lit_" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>

