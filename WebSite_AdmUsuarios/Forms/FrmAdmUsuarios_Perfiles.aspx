<%@ Page Title="Adm. Usuarios - Perfiles" Language="VB" MasterPageFile="~/MP_AdmUsuarios.master" AutoEventWireup="false" CodeFile="FrmAdmUsuarios_Perfiles.aspx.vb" Inherits="Forms_FrmAdmUsuarios_Perfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="Server">
    <style>
        #DivGrillaContenido1 {
            position: absolute;
            width: 20%;
        }

        #DivGrillaContenido2 {
            position: absolute;
            left: 21%;
            width: 20%;
        }

        #DivGrillaContenido3 {
            position: absolute;
            left: 42%;
            width: 57%;
        }
    </style>
    <div id="DivFormulario">
        <script src="../Scripts/Forms/FrmAdmUsuarios_Perfiles.js?version=20190927"></script>
        <div id="DivEncabezado"><span id='spanEncabezado'>Perfiles</span></div>
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
                        <span id="spanLateralContenido">Perfiles</span>
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
                        <div class="col-lg-3 text-right">
                            <span class="EtiquetaIngresoDatos">Id :</span>
                        </div>
                        <div class="col-lg-1 text-center">
                            <span id="IdEntidad" class="EtiquetaInformacionDatos DatoFormulario"></span>
                        </div>
                    </div>
                    <div class="row separador">
                        <div class="col-lg-3 text-right">
                            <span class="EtiquetaIngresoDatos">Nombre :</span>
                        </div>
                        <div class="col-lg-8">
                            <input id="TxtNombre" class="form-control DatoFormulario" placeholder="Nombre" type="text" style="width: 100%;" />
                        </div>
                    </div>
                </div>
                <div id="DivContenidoSuperiorMedio">
                    <div id="DivCheckBoxAcciones">
                        <div class="form-group row">
                            <div id="DivRadioButtons"></div>
                        </div>
                    </div>
                </div>
                <div id="DivContenidoSuperiorDerecha">
                    <div id="DivBtnBotoneraSuperior"></div>
                </div>
            </div>
            <div id="DivContenidoMedio">
                <div id="DivGrillaContenido" class="DatoFormulario"></div>
                <div id="DivGrillaContenido1" class="DatoFormulario"></div>
                <div id="DivGrillaContenido2" class="DatoFormulario"></div>
                <div id="DivGrillaContenido3" class="DatoFormulario"></div>
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
