<%@ Page Title="Usuarios" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="DefaultUsuarios.aspx.vb" Inherits="DefaultUsuarios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        // TODO: Replace with the URL of your WebService app
        //var ApiURL = 'http://www.ospedycdirecto.org.ar/webapiutedyc/api';
        var ApiURL = 'http://localhost:54382/api';
        var Ctrol = '/login';
        //var Acc = ;
        function GetAPI(API, C, Acc) {
            $.ajax({
                type: 'GET',
                url: API + C + Acc
            }).done(function (data) {
                $('#txtLogIN').text(data);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#txtPass').text(jqXHR.responseText || textStatus);
            });
        }
        function PostAPI(API, C, Acc) {
            let userName = $('#txtLogIN').val();
            let password = $('#txtPass').val();
            let mess = '';
            let ret = '';
            //console.log(JSON.stringify('LoginRequest {"UserLogin": "' + userName + '", "Password" : "' + password + '"}'));
            $.ajax({
                //headers: {
                //    'Accept': 'application/json',
                //    'Content-Type': 'application/json'
                //},
                type: "POST",
                url: API + C + Acc,
                data: '{ "UserLogin": "' + userName + '", "Password" : "' + password + '"}',
                crossDomain: true,
                dataType: 'json',
                contentType: "application/json",

                success: function (data) {



                    console.log(JSON.stringify(data));
                    alert("Ud. se logueo como " + JSON.stringify(data.usuario.Nombre));
                    //console.log("data:", JSON.stringify(result.Usuario[0], null, 2));
                    //console.log(response.d);
                },
                error: function (xhr, textStatus, error) {
                    console.log(xhr);
                    console.log(textStatus);
                    console.log(error);

                    switch (xhr.status) {
                        case 401:
                            alert('Acceso No Autorizado!');
                            break;
                        case 403:
                            alert('Acceso Restringido!');
                            break;
                        default:
                            alert(xhr.status);
                            break;
                    }
                }
            });
           
        }
        //function OnSuccess() {
        //    console.log("aca");
        //    console.log(response.d);
        //}
    </script>
    <table width="70%">
        <%-- <tr>
            <td>Nombre y Apellido</td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td>Usuario</td>
            <td>
                <input type="text" id="txtLogIN" />
                <%--<asp:TextBox ID="txtLogIN" runat="server"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td>Password</td>
            <td>
                <input type="text" id="txtPass" />
                <%--<asp:TextBox ID="txtPass" runat="server"></asp:TextBox>--%>

            </td>
        </tr>
        <tr>
            <td align="right">
                <a href="#" onclick="PostAPI(ApiURL, Ctrol, '/authenticate');">Autenticarse</a>
            </td>
            <td align="right">
                <asp:Button ID="BtnCreandoUsuario" runat="server" Text="Creando Usuario" />
            </td>
        </tr>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:Label ID="LblNombre" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Id</td>
            <td>
                <asp:Label ID="LblId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GrillaUsuarios" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IdEntidad" HeaderText="IdEntidad" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="UserLogin" HeaderText="UserLogin" />
                        <asp:BoundField DataField="FechaAlta" HeaderText="FechaAlta" />
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>

</asp:Content>

