<%@ Page Title="Adm. Usuarios - Formularios" Language="VB" MasterPageFile="~/MP_AdmUsuarios.master" AutoEventWireup="false" CodeFile="FrmAdmUsuarios_Formularios.aspx.vb" Inherits="Forms_FrmAdmUsuarios_Formularios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="Server">
    <style>
        #DivGrillaContenido1Cabecera {
            position: absolute;
            width: 45%;
            text-align: center;
        }

        #DivGrillaContenido2Cabecera {
            position: absolute;
            left: 50%;
            width: 45%;
            text-align: center;
        }

        #DivGrillaContenido1 {
            position: absolute;
            top: 45px;
            width: 45%;
        }

        #DivGrillaContenido2 {
            position: absolute;
            top: 45px;
            left: 50%;
            width: 45%;
        }
    </style>
    <div id="DivFormulario">
        <script src="../Scripts/Forms/FrmAdmUsuarios_Formularios.js?version=20190927"></script>
        <div id="DivEncabezado"><span id='spanEncabezado'>Formularios</span></div>
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
                        <span id="spanLateralContenido">Formularios</span>
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
                        <div class="col-lg-4 text-right">
                            <span class="EtiquetaIngresoDatos">Nombre en Menu :</span>
                        </div>
                        <div class="col-lg-8">
                            <input id="TxtNombre" class="form-control DatoFormulario" placeholder="Nombre en Menu" type="text" style="width: 100%;" name="NoCopiable" />
                        </div>
                    </div>
                    <div class="row separador">
                        <div class="col-lg-4 text-right">
                            <span class="EtiquetaIngresoDatos">url :</span>
                        </div>
                        <div class="col-lg-8">
                            <input id="TxtURL" class="form-control DatoFormulario" placeholder="URL" type="text" style="width: 100%;" />
                        </div>
                    </div>
                </div>
                <div id="DivContenidoSuperiorMedio">
                    <div id="DivCheckBoxAcciones">
                        <div class="col-sm-12">
                            <input type="checkbox" name="Accion" id="cbx_Acciones_Visualizar" />
                            <label for="cbx_Acciones_Visualizar">Visualizar</label>
                        </div>
                        <div class="col-sm-12">
                            <input type="checkbox" name="Accion" id="cbx_Acciones_Agregar" />
                            <label for="cbx_Acciones_Agregar">Agregar</label>
                        </div>
                        <div class="col-sm-12">
                            <input type="checkbox" name="Accion" id="cbx_Acciones_Modificar" />
                            <label for="cbx_Acciones_Modificar">Modificar</label>
                        </div>
                        <div class="col-sm-12">
                            <input type="checkbox" name="Accion" id="cbx_Acciones_Eliminar" />
                            <label for="cbx_Acciones_Eliminar">Eliminar</label>
                        </div>
                    </div>
                </div>
                <div id="DivContenidoSuperiorDerecha">
                    <div id="DivBtnBotoneraSuperior"></div>
                </div>
            </div>
            <div id="DivContenidoMedio">
                <div id="DivGrillaContenido1Cabecera">
                    <span class="spanContenidoCabecera">Familias</span>
                </div>
                <div id="DivGrillaContenido2Cabecera">
                    <span class="spanContenidoCabecera">Ramas</span>
                </div>
                <div id="DivGrillaContenido1" class="DatoFormulario"></div>
                <div id="DivGrillaContenido2" class="DatoFormulario"></div>
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

