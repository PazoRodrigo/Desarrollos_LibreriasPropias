<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        //$(document).ready(function () {
        //    Inicio();
        //});
        //async function Inicio() {
        //    //let lista = await Representado.TraerTodosXApellidoNombre('Morelo');
        //    let listaConv = await Convenio.TraerTodos();
        //    console.log(lista);

        //    //let lista = await Empresa.TraerTodosXCodEmpresa(11);
        //    //console.log(lista);
        //}
    </script>

    <h2>Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

    </p>
    <p>
        <asp:GridView ID="Grilla" runat="server">
        </asp:GridView>

    </p>
    <div id="divPrueba"></div>
</asp:Content>
