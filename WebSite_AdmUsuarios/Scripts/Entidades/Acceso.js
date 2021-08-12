var _ListaAccesos;

class Acceso {
    constructor() {
        this.IdEntidad = 0;
        this.IdFormulario = 0;
        this.IdFamilia = 0;
        this.IdRama = 0;
        this.IdPerfil = 0;
        this.IdAccion = 0;
    }

    // ABM
    async Alta() {
        //await this.ValidarCampos();
        console.log(this);
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsAcceso + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaAccesos.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ObjFormulario() {
        try {
            return await Formulario.TraerUno(this.IdFormulario);
        } catch (e) {
            return new Formulario();
        }
    }
    async ObjRama() {
        try {
            return await Rama.TraerUno(this.IdRama);
        } catch (e) {
            return new Rama();
        }
    }
    async ObjFamilia() {
        try {
            return await Familia.TraerUno(this.IdFamilia);
        } catch (e) {
            return new Familia();
        }
    }
    // Traer
    static async TraerTodasXPerfil(IdPerfil) {
        let data = {
            "IdPerfil": IdPerfil
        };
        let lista = await ejecutarAsync(urlWsAcceso + "/TraerTodosXPerfil", data);
        _ListaAccesos = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadAcceso(value));
        });
        _ListaAccesos = result;
        return result;
    }
    static async TraerTodasXFormulario(IdFormulario) {
        let data = {
            "IdFormulario": IdFormulario
        };
        let lista = await ejecutarAsync(urlWsAcceso + "/TraerTodasXFormulario", data);
        _ListaAccesos = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadAcceso(value));
        });
        _ListaAccesos = result;
        return result;
    }
    // Herramientas
    static async ArmarLista(div, lista, estilo) {
        $('#' + div + '').html('');
        let lResul = [];
        if (lista.length > 0) {
            for (let itemPerfil of lista) {
                let listaFAccesos = await Acceso.TraerTodasXPerfil(itemPerfil.IdEntidad);
                let objAcceso = new Acceso();
                for (let itemAcceso of listaFAccesos) {
                    objAcceso = new Acceso();
                    objAcceso.IdRama = itemAcceso.IdRama;
                    objAcceso.IdFormulario = itemAcceso.IdFormulario;
                    objAcceso.IdAccion = itemAcceso.IdAccion;
                    lResul.push(objAcceso);
                }
            }
        }
        let str = '';
        if (lResul.length > 0) {
            str += '<div style="' + estilo + '">';
            let strAcciones = '';
            for (let item of lResul) {
                let objFormulario = await item.ObjFormulario();
                let objRama = await item.ObjRama();
                strAcciones = await Acceso.ArmarStrAcciones(item.IdAccion);
                str += '<div class="row separadorRow30">';
                str += String.format('<div class="col-lg-4">{0}</div><div class="col-lg-4">{1}</div><div class="col-lg-4">{2}</div>', objRama.Nombre, objFormulario.Nombre, strAcciones);
                str += '</div>';
            }
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    //static async ArmarAcciones(IntAcciones) {
    //    let Result = '';
    //    let anterior = false;
    //    if (IntAcciones >= 8) {
    //        Result += 'Visualizar';
    //        anterior = true;
    //        IntAcciones -= 8;
    //    }
    //    if (IntAcciones >= 7) {
    //        if (anterior == true) {
    //            Result += ' - ';
    //        }
    //        Result += 'Eliminar';
    //        anterior = true;
    //        IntAcciones -= 4;
    //    }
    //    if (IntAcciones >= 2) {
    //        if (anterior == true) {
    //            Result += ' - ';
    //        }
    //        Result += 'Modificar';
    //        anterior = true;
    //        IntAcciones -= 2;
    //    }
    //    if (IntAcciones == 1) {
    //        if (anterior == true) {
    //            Result += ' - ';
    //        }
    //        Result += 'Agregar';
    //        IntAcciones -= 1;
    //    }
    //    return Result;
    //}
    static async ArmarStrAcciones(IntAcciones) {
        //let str = '';
        //let anterior = false;
        //if (IntAcciones >= 8) {
        //    IntAcciones -= 8;
        //    str = 'Visualizar';
        //    anterior = true;
        //}
        //if (IntAcciones >= 4) {
        //    IntAcciones -= 4;
        //    if (anterior == true) {
        //        str += ', ';
        //    }
        //    str += 'Eliminar';
        //    anterior = true;
        //}
        //if (IntAcciones >= 2) {
        //    IntAcciones -= 2;
        //    if (anterior == true) {
        //        str += ', ';
        //    }
        //    str += 'Modificar';
        //    anterior = true;
        //}
        //if (IntAcciones == 1) {
        //    IntAcciones -= 1;
        //    if (anterior == true) {
        //        str += ', ';
        //    }
        //    str += 'Agregar';
        //}

        //return str;
        let Agregar = 0;
        let Modificar = 0;
        let Eliminar = 0;
        let Visualizar = 0;

        if (IntAcciones >= 8) {
            IntAcciones -= 8;
            Visualizar = 1;
        }
        if (IntAcciones >= 4) {
            IntAcciones -= 4;
            Eliminar = 1;
        }
        if (IntAcciones >= 2) {
            IntAcciones -= 2;
            Modificar = 1;
        }
        if (IntAcciones == 1) {
            IntAcciones -= 1;
            Agregar = 1;
        }
        let str = '';
        let anterior = false;
        if (Agregar == 1) {
            str += 'Agregar';
            anterior = true;
        }
        if (Modificar == 1) {
            if (anterior == true) {
                str += ', ';
            } else {
                anterior = true;
            }
            str += 'Modificar';
        }
        if (Eliminar == 1) {
            if (anterior == true) {
                str += ', ';
            } else {
                anterior = true;
            }
            str += 'Eliminar';
        }
        if (Visualizar == 1) {
            if (anterior == true) {
                str += ', ';
            } else {
                anterior = true;
            }
            str += 'Visualizar';
        }
        return str;
    }
    async MarcarAcciones() {
        let evento = '';
        let IntAcciones = this.IdAccion;
        let item = await this.ObjFormulario();
        if (IntAcciones >= 8) {
            let ckb = $("#8_" + item.IdEntidad + "");
            ckb.prop('checked', true);
            IntAcciones -= 8;
        }
        if (IntAcciones >= 7) {
            let ckb = $("#4_" + item.IdEntidad + "");
            ckb.prop('checked', true);
            IntAcciones -= 4;
        }
        if (IntAcciones >= 2) {
            let ckb = $("#2_" + item.IdEntidad + "");
            ckb.prop('checked', true);
            IntAcciones -= 2;
        }
        if (IntAcciones == 1) {
            let ckb = $("#1_" + item.IdEntidad + "");
            ckb.prop('checked', true);
            IntAcciones -= 1;
        }
    }
    static async ArmarPopUp(objPerfil, objAcceso) {
        let titulo = '';
        let str = '';
        str += '<div class="row">';
        str += '    <div class="col-lg-2">Familia</div>';
        str += '    <div class="col-lg-2">Rama</div>';
        str += '    <div class="col-lg-4">Formulario</div>';
        str += '	<div class="col-lg-4">';
        str += '	    <div class="row">';
        str += '		    <div class="col-lg-3 text-center"><span>Vis.</span></div>';
        str += '		    <div class="col-lg-3 text-center"><span >Agr.</span></div>';
        str += '		    <div class="col-lg-3 text-center"><span >Mod.</span></div>';
        str += '		    <div class="col-lg-3 text-center"><span >Eli.</span></div>';
        str += '	    </div>';
        str += '    </div>';
        str += '</div>';
        if (objAcceso == undefined) {
            titulo = 'Perfil ' + objPerfil.Nombre + ':     Nuevo Acceso';
            str += '<div class="row separadorRow50">';
            str += '    <div class="col-lg-2"><div id="divNuevoAccesoFamilia"></div></div>';
            str += '    <div class="col-lg-2"><div id="divNuevoAccesoRama"></div></div>';
            str += '    <div class="col-lg-4"><div id="divNuevoAccesoFormulario"></div></div>';
            str += '    <div class="col-lg-4"><div id="divNuevoAccesoAccesos"></div></div>';
            str += '</div>';
        } else {
            titulo = 'Perfil ' + objPerfil.Nombre + ':     Modificación de Acceso';
            str += '<div class="row separadorRow50">';
            str += '    <div class="col-lg-2">' + (await objAcceso.ObjFamilia()).Nombre + '</div>';
            str += '    <div class="col-lg-2">' + (await objAcceso.ObjRama()).Nombre + '</div>';
            str += '    <div class="col-lg-4">' + (await objAcceso.ObjFormulario()).Nombre + '</div>';
            str += '    <div class="col-lg-4"><div id="divAccesoAccesos"></div></div>';
            str += '</div>';
        }
        let control = '';
        if ($("#Modal-PopUpAcceso").length == 0) {
            control += '<div id="Modal-PopUpAcceso" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header HeaderPopUp">';
            control += '                <div class="row">';
            control += '                    <div class="col-lg-8 text-center"><h2 class="modal-title spanPopUpCabecera">' + titulo + '</h2></div>';
            control += '                </div>';
            control += '            </div>';
            control += '            <div class="modal-body" style="height:150px;">';
            control += str;
            control += '                <div class="row separadorRow50">';
            control += '                    <div class="col-lg-10 text-right"><button id="BtnGuardarAcceso" data-Id="1" type="button" class="btn btn-success">Guardar</button></div>';
            control += '                </div>';
            control += '            </div>';

            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        $('#Modal-PopUpAcceso').modal({ show: true });
    }
}
function llenarEntidadAcceso(entidad) {
    let m = new Acceso;
    m.IdEntidad = entidad.IdEntidad;
    m.IdFormulario = entidad.IdFormulario;
    m.IdFamilia = entidad.IdFamilia;
    m.IdRama = entidad.IdRama;
    m.IdPerfil = entidad.IdPerfil;
    m.IdAccion = entidad.IdAccion;
    return m;
}