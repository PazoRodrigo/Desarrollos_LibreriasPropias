<%@ Page Title="Adm. Usuarios - Usuarios" Language="VB" MasterPageFile="~/MP_AdmUsuarios.master" AutoEventWireup="false" CodeFile="FrmAdmUsuarios_Usuarios.aspx.vb" Inherits="Forms_FrmAdmUsuarios_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="Server">
    <style>
        #DivContenidoMedioUsuario {
            position: relative;
            top: 2%;
            border-radius: 10px;
            padding-top: 5px;
            padding-left: 5px;
            padding-right: 5px;
            margin-right: auto;
            margin-left: auto;
            width: 98%;
            height: 88%;
            background-color: rgba(92, 79, 60, 0.2);
        }

        #DivContenidoInferiorUsuario {
            position: relative;
            top: 2%;
            width: 95%;
            height: 5%;
            margin: 5px auto;
        }

        .ContenidoTab {
            padding-top: 5px;
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
    <script src="../Scripts/Forms/FrmAdmUsuarios_Usuarios.js?version=20190927"></script>
    <div id="DivFormulario">
        <div id="DivIndicadores">
            <%--<div id="Globo1">
                <a href="#" id="SelectorGloboEmpleadoTipo1" class="selectorGlobo">
                    <div id="GloboEmpleadoTipo1" class="Globo Tipo1">150</div>
                </a>
                <a href="#" id="SelectorGloboEmpleadoTipo2" class="selectorGlobo">
                    <div id="GloboEmpleadoTipo2" class="Globo Tipo2">12</div>
                </a>
            </div>
            <div id="Globo2">
                <a href="#" id="SelectorGloboAfiliadoTipo1" class="selectorGlobo">
                    <div id="GlobooAfiliadoTipo1" class="Globo Tipo1">1500</div>
                </a>
                <a href="#" id="SelectorGlobooAfiliadoTipo2" class="selectorGlobo">
                    <div id="GlobooAfiliadoTipo2" class="Globo Tipo2">120</div>
                </a>
            </div>
            <div id="Globo3">
                <a href="#" id="SelectorGloboEntidadTipo1" class="selectorGlobo">
                    <div id="GlobooEntidadTipo1" class="Globo Tipo1">1500</div>
                </a>
                <a href="#" id="SelectorGlobooEntidadTipo2" class="selectorGlobo">
                    <div id="GlobooEntidadTipo2" class="Globo Tipo2">120</div>
                </a>
            </div>--%>
        </div>
        <div id="DivEncabezado"><span id='spanEncabezado'>Usuarios</span></div>
        <div id="DivLateralIzquierdo">
            <div id="DivLateralEncabezado">
                <div class="row">
                    <div class="col-lg-12">
                        <input id="TxtBuscadorNombre" class="form-control DatoFormulario" placeholder="Nombre / Login" type="text" style="width: 100%;" />
                    </div>
                </div>
            </div>
            <div id="DivLateralContenido">
                <div class="row separadorRow30">
                    <div class="col-lg-12 text-center">
                        <span id="spanLateralContenido">Usuarios</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div id="DivGrillaLateralContenido"></div>
                    </div>
                </div>
            </div>
            <div id="DivLateralPie">
                <div class="row">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <input id="BtnNuevo" type="button" value="Nuevo" class="btn btn-block btn-md btn-primary" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivContenido">
            <div id="DivContenidoMedioUsuario">
                <div id="PnlTabs" class="tabs" style="height: 400px;">
                    <ul>
                        <li><a href="#tabs-DatosBasicos"><span class="spanTab">Datos Básicos</span></a></li>
                        <li><a href="#tabs-Perfiles"><span class="spanTab">Perfiles</span></a></li>
                        <li><a href="#tabs-Accesos"><span class="spanTab">Accesos</span></a></li>
                        <li><a href="#tabs-Areas"><span class="spanTab">Areas</span></a></li>
                        <li><a href="#tabs-Seccionales"><span class="spanTab">Seccionales</span></a></li>
                    </ul>
                    <div id="tabs-DatosBasicos">
                        <div id="Tab_DatosBasicos" class="ContenidoTab">
                            <div class="row separadorRow35">
                                <div class="col-lg-4" style="text-align: right;">
                                    <span class="EtiquetaIngresoDatosUsuario">Nro. Documento / CUIT :</span>
                                </div>
                                <div class="col-lg-3 text-left">
                                    <input id="DB_NroDocumento_CUIT" type="text" class="DatoFormulario" maxlength="11" onkeypress="return jsSoloNumerosSinPuntos(event);" disabled />
                                </div>
                              <%--  <div class="col-lg-2 text-left">
                                    <div class="row">
                                        <div class="col-lg-12 text-left">
                                            <input id="BtnValidarUsuario" type="text" class="btn btn-block btn-success" value="Validar" />
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row separadorRow35">
                                <div class="col-lg-4" style="text-align: right;">
                                    <span class="EtiquetaIngresoDatosUsuario">Apellido y Nombre / razón Social :</span>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <input id="DB_NombreApellido_RazonSocial" type="text" class="DatoFormulario" disabled style="width:100%;" />
                                </div>
                            </div>
                            <div class="row separadorRow35">
                                <div class="col-lg-4" style="text-align: right;">
                                    <span class="EtiquetaIngresoDatosUsuario ">User Login :</span>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <input id="DB_UserLogin" type="text" class="DatoFormulario" />
                                </div>
                            </div>
                            <div class="row separadorRow35">
                                <div class="col-lg-4" style="text-align: right;">
                                    <span class="EtiquetaIngresoDatosUsuario">Teléfono :</span>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <input id="DB_Telefono" type="text" class="DatoFormulario" onkeypress="return jsSoloNumerosSinPuntos(event);" />
                                </div>
                            </div>
                            <div class="row separadorRow50">
                                <div class="col-lg-4" style="text-align: right;">
                                    <span class="EtiquetaIngresoDatosUsuario">Correo Electrónico :</span>
                                </div>
                                <div class="col-lg-6 text-left">
                                    <input id="DB_CorreoElectronico" type="text" class="DatoFormulario" style="width: 70%;" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <input id="Btn_GenerarNotificacion" type="text" class="btn btn-info btn-block" value="Generar Notificación" />
                                    <%--<input id="Btn_ResetearPassword" type="text" class="btn btn-block btn-danger" value="Resetear Contraseña" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-Perfiles">
                        <div id="Tab_Perfiles" class="ContenidoTab">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <span class="spanContenidoCabecera">Perfiles</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 text-left">
                                    <div id="Tab_Perfiles_Perfiles_Contenido" class="ContenidoTab"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-Accesos">
                        <div id="Tab_Accesos" class="ContenidoTab DatoFormulario" style="height: 300px;"></div>

                        <div class="row">
                            <div class="col-lg-7"></div>
                            <div class="col-lg-2">
                                <input id="BtnMenu" type="button" value="Ver Menú" class="btn btn-block btn-md btn-info" />
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">
                                <input id="BtnAtajos" type="button" value="Ver Atajos" class="btn btn-block btn-md btn-info" />
                            </div>
                        </div>
                    </div>
                    <div id="tabs-Areas">
                        <div id="Tab_Areas" class="ContenidoTab DatoFormulario"></div>
                    </div>
                    <div id="tabs-Seccionales">
                        <div id="Tab_Seccionales" class="ContenidoTab DatoFormulario"></div>
                    </div>
                </div>
            </div>
            <div id="DivContenidoInferiorUsuario">
                <div class="col-lg-10"></div>
                <div class="col-lg-2">
                    <input id="BtnGuardar" type="button" value="Guardar" class="btn btn-block btn-md btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
