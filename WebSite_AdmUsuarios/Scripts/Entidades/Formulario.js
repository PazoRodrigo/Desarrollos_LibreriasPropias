var _ListaFormularios;

class Formulario {
    constructor() {
        this.IdEntidad = 0;
        this.IdRama = 0;
        this.Orden = 0;
        this.Nombre = '';
        this.URL = '';
        this.AccionesPosibles = 0;
        this.IdEstado = 0;

        this._ObjRama;
        this._ListaAccesos;
        this._ListaPerfiles;
        this._ListaUsuarios;
    }

    async ObjRama() {
        try {
            if (this._ObjRama == undefined) {
                this._ObjRama = await Rama.TraerUno(this.IdRama);
            }
            return this._ObjRama;
        } catch (e) {
            return new Rama();
        }
    }
    async ListaAccesos() {
        try {
            if (this._ListaAccesos == undefined) {
                this._ListaAccesos = await Acceso.TraerTodasXFormulario(this.IdEntidad);
            }
            return this._ListaAccesos;
        } catch (e) {
            return new Rama();
        }
    }
    async ListaPerfiles() {
        try {
            let listaResult = [];
            let objPerfil;
            let ListaAccesos = await this.ListaAccesos();
            for (let objAc of ListaAccesos) {
                objPerfil = await Perfil.TraerUno(objAc.IdPerfil);
                if (ListaAccesos.length == 0) {
                    listaResult.push(objPerfil);
                } else {
                    let buscado = $.grep(listaResult, function (entidad, index) {
                        return entidad.IdEntidad == objPerfil.IdEntidad;
                    });
                    if (buscado.length == 0) {
                        listaResult.push(objPerfil);
                    }
                }
            }
            return listaResult;
        } catch (e) {
            return new Perfil();
        }
    }
    async ListaUsuarios() {
        try {
            let listaResult = [];
            let ListaPerfiles = await this.ListaPerfiles();
            let objUsuario;
            let ListaUsuariosXPerfil = [];
            for (let objPerfil of ListaPerfiles) {
                ListaUsuariosXPerfil = await objPerfil.ListaUsuarios();
                if (ListaUsuariosXPerfil.length > 0) {

                    console.log(ListaUsuariosXPerfil);
                    objUsuario = Usuario.TraerUno(objPerfil.IdUsuario);
                    //let buscado = $.grep(listaResult, function (entidad, index) {
                    //    return entidad.IdEntidad == objPerfil.IdEntidad;
                    //});
                    //if (buscado.length == 0) {
                    //    listaResult.push(objPerfil);
                    //}
                }
                console.log(ListaUsuariosXPerfil);
            }
            return listaResult;
        } catch (e) {
            return new Perfil();
        }
    }

    // ABM
    async Alta() {
        try {
            await this.ValidarCampos();
            this.Nombre = this.Nombre.toUpperCase();
            this.URL = this.URL.toUpperCase();
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsFormulario + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaFormularios.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica(IdRamaAnterior) {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        this.URL = this.URL.toUpperCase();
        try {
            console.log();
            if (IdRamaAnterior != undefined) {
                if (IdRamaAnterior != this.IdRama) {
                    this.Orden = await Formulario.NuevoOrden(this.IdRama);
                }
            }

            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsFormulario + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;

            if (IdRamaAnterior != undefined) {
                if (IdRamaAnterior != this.IdRama) {
                    await Rama.Reordenar(IdRamaAnterior);
                }
            }
            _ListaFormularios = await Formulario.TraerTodos();
            let buscado = $.grep(_ListaFormularios, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaFormularios = buscado;
            this.IdEstado = 0;
            _ListaFormularios.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsFormulario + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaFormularios = await Formulario.TraerTodos();
            let buscado = $.grep(_ListaFormularios, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaFormularios = buscado;
            this.IdEstado = 1;
            _ListaFormularios.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    // Validaciones
    async ValidarCampos() {
        if (this.IdRama == 0) {
            throw ('Debe indicar la Rama');
        }
        if (this.Orden == 0) {
            throw ('Debe indicar el Orden');
        }
        if (this.Nombre.length == 0) {
            throw ('Debe indicar el Nombre');
        }
        if (this.URL == 0) {
            throw ('Debe indicar la URL');
        }
        if (this.AccionesPosibles == 0) {
            throw ('Debe indicar las Acciones Posibles');
        }
    }
    // Traer
    static async TraerUno(IdEntidad) {
        _ListaFormularios = await Formulario.TraerTodos();
        let buscado = $.grep(_ListaFormularios, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async Todos() {
        if (_ListaFormularios == undefined) {
            _ListaFormularios = await Formulario.TraerTodas();
        }
        return _ListaFormularios;
    }
    static async TraerTodos() {
        return await Formulario.TraerTodas();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsFormulario + "/TraerTodos");
        _ListaFormularios = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadFormulario(value));
        });
        _ListaFormularios = result;
        return _ListaFormularios;
    }
    static async TraerTodosXRama(IdRama) {
        let data = {
            "IdRama": IdRama
        };
        let lista = await ejecutarAsync(urlWsFormulario + "/TraerTodosXRama", data);
        _ListaFormularios = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadFormulario(value));
        });
        _ListaFormularios = result;
        return _ListaFormularios;
        //_ListaFormularios = await Formulario.TraerTodos();
        //let buscado = $.grep(_ListaFormularios, function (entidad, index) {
        //    return entidad.IdRama == IdRama;
        //});
        //let Encontrados = buscado;
        //return Encontrados;
    }
    static async NuevoOrden(IdRama) {
        let lista = await Formulario.TraerTodosXRama(IdRama);
        let result = 1;
        if (lista.length > 0) {
            result = lista.length + 1;
        }
        return result;
    }
    async Ordena() {
        let $this = this;
        let lista = await Formulario.TraerTodosXRama($this.IdRama);
        if ($this.Ordenar == 1) {
            await $this.OrdenarSubir(lista);
        } else {
            if ($this.Ordenar == -1) {
                await $this.OrdenarBajar(lista);
            }
        }
    }
    async OrdenarSubir(lista) {
        let ordenBuscado = this.Orden - 1;
        let buscado = $.grep(lista, function (entidad, index) {
            return entidad.Orden == ordenBuscado;
        });
        let Encontrado = buscado[0];
        Encontrado.Orden = ordenBuscado + 1;
        await Encontrado.Modifica();
        this.Orden--;
        this.Modifica();
    }
    async OrdenarBajar(lista) {
        let ordenBuscado = this.Orden + 1;
        let buscado = $.grep(lista, function (entidad, index) {
            return entidad.Orden == ordenBuscado;
        });
        let Encontrado = buscado[0];
        Encontrado.Orden = ordenBuscado - 1;
        await Encontrado.Modifica();
        this.Orden++;
        this.Modifica();
    }
    // Herramientas
    static async ArmarGrilla(lista, evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        lista.sort(SortXNombre);
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <tbody>';
            let grafico = '';
            let color = '';
            for (let item of lista) {
                if (item.IdEstado == 0) {
                    grafico = 'search';
                    color = 'primary';
                } else {
                    grafico = 'trash';
                    color = 'danger';
                }
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionFormulario glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', btnSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarGrillaAcciones(lista, evento, div, estilo, encabezado) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            if (encabezado == true) {
                str += '<div style="width: 95%; padding-left: 15px;">';
                str += '	<div class="row">';
                str += '		<div class="col-lg-7 text-left"><span>Nombre</span></div>';
                str += '		<div class="col-lg-5">';
                str += '		    <div class="col-lg-3 text-center"><span>Vis.</span></div>';
                str += '		    <div class="col-lg-3 text-center"><span >Agr.</span></div>';
                str += '		    <div class="col-lg-3 text-center"><span >Mod.</span></div>';
                str += '		    <div class="col-lg-3 text-center"><span >Eli.</span></div>';
                str += '        </div>';
                str += '	</div>';
                str += '</div>';
            }
            str += '    <div style=" ' + estilo + '">';
            str += '	    <div style="width: 100%;">';
            for (let item of lista) {
                let tempAcciones = 0;
                if (item.AccionesPosibles > 0) {
                    tempAcciones = item.AccionesPosibles;
                } else {
                    if (item.IdAccion > 0) {
                        tempAcciones = item.IdAccion;
                    }
                }
                let cbxVisualizar = '';
                let cbxAgregar = '';
                let cbxModificar = '';
                let cbxEliminar = '';
                if (tempAcciones >= 8) {
                    cbxVisualizar = '<input type="checkbox" class="micbx-Acceso" name="Accion"  id="8_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="8_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 8;
                }
                if (tempAcciones >= 4) {
                    cbxEliminar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="4_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="4_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 4;
                }
                if (tempAcciones >= 2) {
                    cbxModificar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="2_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="2_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 2;
                }
                if (tempAcciones == 1) {
                    cbxAgregar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="1_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="1_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 1;
                }
                if (encabezado == true) {
                    str += String.format('<div class="row"><div class="col-lg-7 text-left">{0}</div><div class="col-lg-5"><div class="col-lg-3 text-center">{1}</div><div class="col-lg-3 text-center">{2}</div><div class="col-lg-3 text-center">{3}</div><div class="col-lg-3 text-center">{4}</div></div></div>', item.Nombre.substring(0, 30), cbxVisualizar, cbxAgregar, cbxModificar, cbxEliminar);
                } else {
                    str += String.format('</div><div class="col-lg-3 text-left">{0}</div><div class="col-lg-3 text-center">{1}</div><div class="col-lg-3 text-center">{2}</div><div class="col-lg-3 text-center">{3}</div>', cbxVisualizar, cbxAgregar, cbxModificar, cbxEliminar);
                }
            }
            str += '	</div>';
            str += '</div>';

        }



        //if (lista.length > 0) {
        //    str += '<div style="' + estilo + '">';
        //    str += '    <table class="table table-bordered" style="width: 70%;">';
        //    str += '        <thead>';
        //    str += '            <tr>';
        //    str += '                <th style="text-align: center;">Nombre</th>';
        //    str += '                <th style="text-align: center;">Vis.</th>';
        //    str += '                <th style="text-align: center;">Agr.</th>';
        //    str += '                <th style="text-align: center;">Mod.</th>';
        //    str += '                <th style="text-align: center;">Eli.</th>';
        //    str += '            </tr>';
        //    str += '        </thead>';
        //    str += '        <tbody>';
        //    for (let item of lista) {
        //        let tempAcciones = item.AccionesPosibles;
        //        let cbxVisualizar = '';
        //        let cbxAgregar = '';
        //        let cbxModificar = '';
        //        let cbxEliminar = '';
        //        if (tempAcciones >= 8) {
        //            cbxVisualizar = '<input type="checkbox" class="micbx-Acceso" name="Accion"  id="8_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="8_' + item.IdEntidad + '"></label>';
        //            tempAcciones -= 8;
        //        }
        //        if (tempAcciones >= 4) {
        //            cbxEliminar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="4_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="4_' + item.IdEntidad + '"></label>';
        //            tempAcciones -= 4;
        //        }
        //        if (tempAcciones >= 2) {
        //            cbxModificar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="2_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="2_' + item.IdEntidad + '"></label>';
        //            tempAcciones -= 2;
        //        }
        //        if (tempAcciones == 1) {
        //            cbxAgregar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="1_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="1_' + item.IdEntidad + '"></label>';
        //            tempAcciones -= 1;
        //        }
        //        str += String.format('<tr><td align="left" style="width: 60%;">{0}</td><td align="center" style="width: 5%;">{1}</td><td align="center" valign="middle" style="width: 5%;">{2}</td><td align="center" valign="middle" style="width: 5%;">{3}</td><td align="center" valign="middle" style="width: 5%;">{4}</td></tr>', item.Nombre, cbxVisualizar, cbxAgregar, cbxModificar, cbxEliminar);
        //    }
        //    str += '        </tbody>';
        //    str += '    </table>';
        //    str += '</div>';
        //}
        return $('#' + div + '').html(str);
    }
    static async ArmarGrillaConAccionesOrden(obj, div, evento, estilo) {
        let lista = await Formulario.TraerTodosXRama(obj.IdEntidad);
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += '<div class="row separadorRow50">';
            str += '    <div class="col-lg-7 text-center"><span class="spanContenidoCabecera">Formularios</span></div>';
            str += '</div >';
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th style="text-align: center;">Nombre</th>';
            str += '                <th style="text-align: center;">URL</th>';
            str += '                <th style="text-align: center;">Vis.</th>';
            str += '                <th style="text-align: center;">Agr.</th>';
            str += '                <th style="text-align: center;">Mod.</th>';
            str += '                <th style="text-align: center;">Eli.</th>';
            str += '                <th colspan="2" style="text-align: center;">Orden</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let tempAcciones = item.AccionesPosibles;
                let cbxVisualizar = '';
                let cbxAgregar = '';
                let cbxModificar = '';
                let cbxEliminar = '';
                if (tempAcciones >= 8) {
                    cbxVisualizar = '<input type="checkbox" class="micbx-Acceso" name="Accion"  id="8_' + item.IdEntidad + '" /><label for="8_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 8;
                }
                if (tempAcciones >= 4) {
                    cbxEliminar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="4_' + item.IdEntidad + '" /><label for="4_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 4;
                }
                if (tempAcciones >= 2) {
                    cbxModificar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="2_' + item.IdEntidad + '"  /><label for="2_' + item.IdEntidad + '"></label>';
                    tempAcciones -= 2;
                }
                if (tempAcciones == 1) {
                    cbxAgregar = '<input type="checkbox" class="micbx-Acceso" name="Accion" id="1_' + item.IdEntidad + '" /><label for="1_' + item.IdEntidad + '"></label>';
                }
                let btnOrdenSubir = '<a href="#" class="btn btn-success btn-xs mibtn-ordenFormulario glyphicon glyphicon-arrow-up" dataArrow="1" data-IdRama="' + item.IdRama + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                let btnORdenBajar = '<a href="#" class="btn btn-success btn-xs mibtn-ordenFormulario glyphicon glyphicon-arrow-down" dataArrow="0" data-IdRama="' + item.IdRama + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                let color = '';
                if (item.IdEstado == 1) {
                    color = 'background-color:red;';
                }
                str += String.format('<tr style="' + color + '"><td align="left" style="width: 40%;">{0}</td><td align="left" style="width: 40%;">{1}</td><td align="center" style="width: 5%;">{2}</td><td align="center" valign="middle" style="width: 5%;">{3}</td><td align="center" valign="middle" style="width: 5%;">{4}</td><td align="center" valign="middle" style="width: 5%;">{5}</td><td align="center" valign="middle" style="width: 5%;">{6}</td><td align="center" valign="middle" style="width: 5%;">{7}</td></tr>', item.Nombre, item.URL, cbxVisualizar, cbxAgregar, cbxModificar, cbxEliminar, btnOrdenSubir, btnORdenBajar);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarGrillaSinAccionesSinEvento(lista, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th style="text-align: center;">Nombre</th>';
            str += '                <th style="text-align: center;">URL</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {

                str += String.format('<tr><td align="left" style="width: 50%;">{0}</td><td align="left" style="width: 50%;">{1}</td></tr>', item.Nombre, item.URL);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCombo(lista, evento, div) {
        let cbo = "";
        cbo += '<div id="CboFormulario" class="dropdown">';
        cbo += '    <button id="selectorFormularioCombo" class="btn btn-primary dropdown-toggle btn-xs btn-block" type="button" data-toggle="dropdown">Formulario';
        cbo += '        <span class="caret"></span>';
        cbo += '    </button>';
        cbo += '    <ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionFormulario" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '" > ' + this.Nombre + '</a></li>';
        });
        cbo += '    </ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
    static async ArmarLista(lista, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th style="text-align: center;">Formularios</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                str += String.format('<tr><td align="left">{0}</td></tr>', item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarArbol(lista, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div style="' + estilo + '">';
        if (lista.length > 0) {
            str += '<ul><li class="LI_Seleccionable"><a href="#" class="btn btn-primary btn-sm mibtn-seleccionRamaArbol" data-Id="' + lista[0].IdRama + '">' + (await lista[0].ObjRama()).Nombre + '</a></li>';
            for (let item of lista) {
                str += '<ul><li class="LI_Seleccionable"><a href="#" class="btn btn-primary btn-sm mibtn-seleccionFormularioArbol" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a></li>';
            }
            str += '</ul>';
        }
        str += '</div>';
        console.log(str);
        //return $('#' + div + '').html(str);

    }
    static async Refresh() {
        _ListaFormularios = await Formulario.TraerTodas();
    }
}
function llenarEntidadFormulario(entidad) {
    let m = new Formulario;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdRama = entidad.IdRama;
    m.Orden = entidad.Orden;
    m.URL = entidad.URL;
    m.AccionesPosibles = entidad.AccionesPosibles;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionFormulario", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaFormularios, function (entidad, index) {
            return entidad.IdEntidad == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".micbx-Acceso", async function () {
    try {
        $this = $(this);
        let dato = $this.attr("id");
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: dato });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".mibtn-ordenFormulario", async function () {
    try {
        $this = $(this);
        let listaOriginal = await Formulario.TraerTodosXRama($this.attr("data-IdRama"));
        let buscado = $.grep(_ListaFormularios, function (entidad, index) {
            return entidad.IdEntidad == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let orden = $this.attr("dataArrow");
        if (orden == 1) {
            // Subir
            Seleccionado.Ordenar = 1;
            //await Formulario.OrdenarSubir(Seleccionado, listaOriginal);
        } else {
            // Bajar
            Seleccionado.Ordenar = -1;
            //await Formulario.OrdenarBaja(Seleccionado, listaOriginal);
        }
        //Formulario.Refresh();
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});