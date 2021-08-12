<%@ Page Title="1 - Familia" Language="VB" MasterPageFile="~/Usuarios/MPUsuarios.master" AutoEventWireup="false" CodeFile="ABMFamilia.aspx.vb" Inherits="Usuarios_ABMFamilia    " %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content2" runat="Server">
    <table style="width: 50%;" border="1">
        <tr>
            <td>IdEntidad</td>
            <td>
                <asp:Label ID="LblIdentidad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
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
            <td colspan="4">
                <asp:GridView ID="GrillaFamilias" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEntidad">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IdEntidad" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GrillaFormularios" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IdEntidad" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <%--<asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>
            
        </tr>
    </table>
</asp:Content>

