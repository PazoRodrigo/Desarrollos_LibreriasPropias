<%@ Page Title="Adm. Usuarios - Ramas" Language="VB" MasterPageFile="~/MP_AdmUsuarios.master" AutoEventWireup="false" CodeFile="FrmAdmUsuarios_Ramas.aspx.vb" Inherits="Forms_FrmAdmUsuarios_Ramas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="Server">
    <script src="../Scripts/Forms/FrmAdmUsuarios_Ramas.js?version=20190927"></script>
    <div id="DivFormulario">
        <div id="DivEncabezado"><span id='spanEncabezado'>Ramas</span></div>
        <div id="DivLateralIzquierdo">
            <div id="DivLateralEncabezado">
                <div class="row">
                   <div class="col-lg-12">
                        <input id="TxtBuscadorNombre" class="form-control DatoFormulario" placeholder="Buscador por Nombre" type="text" style="width: 100%;" />
                    </div>
                </div>
            </div>
            <div id="DivLateralContenido">
                <div class="row separadorRow50">
                    <div class="col-lg-12 text-center">
                        <span id="spanLateralContenido">Ramas</span>
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
            <div id="DivContenidoSuperior">
                <div id="DivContenidoSuperiorIzquierda">
                    <div class="row" style="visibility: hidden">
                        <div class="col-lg-4 text-right">
                            <span class="EtiquetaIngresoDatos">Id :</span>
                        </div>
                        <div class="col-lg-3">
                            <span id="IdEntidad" class="EtiquetaInformacionDatos DatoFormulario"></span>
                        </div>
                    </div>
                    <div class="row separador">
                        <div class="col-lg-4 text-right">
                            <span class="EtiquetaIngresoDatos">Nombre en Menu :</span>
                        </div>
                        <div class="col-lg-5">
                            <input id="TxtNombre" class="form-control DatoFormulario" placeholder="Nombre" type="text" style="width: 100%;" />
                        </div>
                    </div>
                </div>
                <div id="DivContenidoSuperiorMedio">
                    <div id="DivGrillaSuperiorDerecha"></div>
                </div>
                <div id="DivContenidoSuperiorDerecha">
                    <div id="DivBtnBotoneraSuperior"></div>
                </div>
            </div>
            <div id="DivContenidoMedio">
                <div id="DivGrillaContenido" class="DatoFormulario"></div>
            </div>
            <div id="DivContenidoInferior">
                <div class="col-lg-10"></div>
                <div class="col-lg-2">
                    <input id="BtnGuardar" type="button" value="Guardar" class="btn btn-block btn-md btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
