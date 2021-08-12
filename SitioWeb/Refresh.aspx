<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Refresh.aspx.vb" Inherits="Refresh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table>
        <tr>
            <td>Seccionales</td>
            <td>
                <asp:Button ID="BtnSeccionales" runat="server" Text="Ref. Seccionales" /></td>
            <td>
                <asp:Label ID="LblSeccionales" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

