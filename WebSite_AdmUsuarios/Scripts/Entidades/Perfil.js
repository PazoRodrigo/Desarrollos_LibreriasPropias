var _ListaPerfiles;

class Perfil {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
        this.IdRol = 0;
        this.IdEstado = 0;

        this._ListaAccesos;
        this._ListaUsuarios;
        this._ObjRol;
    }
    async ListaAccesos() {
        try {
            //if (this._ListaAccesos == undefined) {
            this._ListaAccesos = await Acceso.TraerTodasXPerfil(this.IdEntidad);
            //}
            return this._ListaAccesos;
        } catch (e) {
            throw new Acceso();
        }
    }
    async ListaUsuarios() {
        try {
            this._ListaUsuarios = await Usuario.TraerTodosXPerfil(this.IdEntidad);
            return this._ListaUsuarios;
        } catch (e) {
            throw new Usuario();
        }
    }

    async ObjRol() {
        try {
            if (this._ObjRol == undefined) {
                this._ObjRol = await Rol.TraerUno(this.IdRol);
            }
            return this._ObjRol;
        } catch (e) {
            return new Rol();
        }
    }
    // ABM
    async Alta() {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        console.log(this);
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsPerfil + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaPerfiles.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        this.Nombre = this.Nombre.toUpperCase();
        console.log(this);
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsPerfil + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaPerfiles = await Perfil.TraerTodos();
            let buscado = $.grep(_ListaPerfiles, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaPerfiles = buscado;
            this.IdEstado = 0;
            _ListaPerfiles.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        //try {
        //    let data = {
        //        'entidad': this
        //    };
        //    let id = await ejecutarAsync(urlWsPerfil + "/Baja", data);
        //    if (id !== undefined)
        //        this.IdEntidad = id;

        //    _ListaPerfiles = await Perfil.TraerTodos();
        //    let buscado = $.grep(_ListaPerfiles, function (entidad, index) {
        //        return entidad.IdEntidad != id;
        //    });
        //    _ListaPerfiles = buscado;
        //    this.IdEstado = 1;
        //    _ListaPerfiles.push(this);
        //    return;
        //} catch (e) {
        //    throw e;
        //}
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsPerfil + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaPerfiles = await Perfil.TraerTodos();
            let buscado = $.grep(_ListaPerfiles, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaPerfiles = buscado;
            this.IdEstado = 1;
            _ListaPerfiles.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Clonar() {
        this.Nombre = this.Nombre.toUpperCase();
        console.log(this);
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsPerfil + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaPerfiles.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    // Traer
    static async TraerUno(IdEntidad) {
        _ListaPerfiles = await Perfil.TraerTodos();
        let buscado = $.grep(_ListaPerfiles, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async Todos() {
        if (_ListaPerfiles == undefined) {
            _ListaPerfiles = await Perfil.TraerTodas();
        }
        return _ListaPerfiles;
    }
    static async TraerTodos() {
        return await Perfil.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsPerfil + "/TraerTodos");
        _ListaPerfiles = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadPerfil(value));
        });
        _ListaPerfiles = result;
        return result;
    }
    static async TraerTodosXRol(IdRol) {
        console.log(IdRol);
        let data = {
            "IdRol": IdRol
        }
        let lista = await ejecutarAsync(urlWsPerfil + "/TraerTodosXRol", data);
        _ListaPerfiles = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadPerfil(value));
        });
        _ListaPerfiles = result;
        return result;
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
                console.log(item);
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionPerfil glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', btnSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCheckBoxs(lista, evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-3"><input type="checkbox" class="mibtn-seleccionPerfil" value="' + item.IdEntidad + '"   name="CkbList_Perfiles" data-Id="' + item.IdEntidad + '"  id="chk_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarLista(lista, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div class="row separadorRow50">';
        str += '<div class="col-lg-12 text-center"> <span class="spanContenidoCabecera">Perfiles</span> </div>';
        str += '</div >';
        str += '<div style="' + estilo + '">';
        if (lista.length > 0) {
            for (let item of lista) {
                str += String.format('<div class="col-lg-2">{0}</div>', item.Nombre);
            }
        }
        str += '</div >';
        return $('#' + div + '').html(str);
    }
    async ArmarGrillaContenido(div, estilo, seleccionable) {
        $('#' + div + '').html('');
        let str = '';
        let ListaAccesos = await this.ListaAccesos();
        str += '<div class="row separadorRow30">';
        str += '    <div class="col-lg-2"><span class="spanContenidoCabecera">Familias</span></div>';
        str += '    <div class="col-lg-3"><span class="spanContenidoCabecera">Rama</span></div>';
        str += '    <div class="col-lg-2"><span class="spanContenidoCabecera">Formulario</span></div>';
        str += '    <div class="col-lg-1"></div>';
        str += '    <div class="col-lg-2"><input id="BtnAgregarAcceso" type="button" value="Agregar Acceso" class="btn btn-block btn-md btn-info"  /></div>';
        str += '    <div class="col-lg-2"><input id="BtnClonarPerfil" type="button" value="Clonar Perfil" class="btn btn-block btn-md btn-warning" style="margin-left:1px" /></div>';
        str += '</div>';
        str += '<div style=' + estilo + '>';
        for (let linea of ListaAccesos) {
            str += '<div class="row">';
            str += '    <div class="col-lg-2">' + (await linea.ObjFamilia()).Nombre + '</div>';
            str += '    <div class="col-lg-2">' + (await linea.ObjRama()).Nombre + '</div>';
            if (seleccionable == true) {
                str += '<a href="#" class="btn-secondary milink-ModificarAcceso" data-Evento="eventoModificarAcceso" data-Id="' + linea.IdEntidad + '" data-IdPerfil="' + linea.IdPerfil + '"  data-IdFamilia="' + linea.IdFamilia + '"  data-IdRama="' + linea.IdRama + '" data-IdFormulario="' + linea.IdFormulario + '" data-IdAccion="' + linea.IdAccion + '">';
                str += '    <div class="col-lg-4">' + (await linea.ObjFormulario()).Nombre + '</div>';
                str += '</a>';
            } else {
                str += '    <div class="col-lg-4">' + (await linea.ObjFormulario()).Nombre + '</div>';
            }
            str += '    <div class="col-lg-4">' + await Acceso.ArmarStrAcciones(linea.IdAccion) + '</div>';
            str += '</div>';
        }
        str += '    </div>';
        str += '</div>';
        return $('#' + div + '').html(str);
    }
    async ArmarPopUpClonar() {
        let MenuIdFamilia = 0;
        let MenuIdRama = 0;
        let MenuListaAccesos = [];
        let i = 0;
        let strMenu = '';
        MenuListaAccesos = await this.ListaAccesos();
        while (i <= MenuListaAccesos.length - 1) {
            MenuIdFamilia = MenuListaAccesos[i].IdFamilia;
            strMenu += '<div class="row">';
            strMenu += '    <div class="col-lg-7">';
            strMenu += '        <ul><li class="UL_Seleccionable"><a href="#" class="btn btn-success btn-xs mibtn-seleccionMenuFamilia" data-IdFam="' + MenuListaAccesos[i].IdFamilia + '">' + (await MenuListaAccesos[i].ObjFamilia()).Nombre + '</a></li>';
            strMenu += '    </div>';
            strMenu += '</div>';
            while ((i <= MenuListaAccesos.length - 1) && (MenuListaAccesos[i].IdFamilia = MenuIdFamilia)) {
                MenuIdRama = MenuListaAccesos[i].IdRama;
                strMenu += '<div class="row">';
                strMenu += '    <div class="col-lg-1"></div>';
                strMenu += '    <div class="col-lg-7">';
                strMenu += '        <ul><li class="UL_Seleccionable"><a href="#" class="btn btn-info btn-xs mibtn-seleccionMenuRama" data-IdRam="' + MenuListaAccesos[i].idRama + '">' + (await MenuListaAccesos[i].ObjRama()).Nombre + '</a></li>';
                strMenu += '    </div>';
                strMenu += '</div>';
                while ((i <= MenuListaAccesos.length - 1) && (MenuListaAccesos[i].IdFamilia = MenuIdFamilia) && (MenuListaAccesos[i].IdRama = MenuIdRama)) {
                    strMenu += '<div class="row">';
                    strMenu += '    <div class="col-lg-2"></div>';
                    strMenu += '    <div class="col-lg-6">';
                    strMenu += '        <li class="LI_Seleccionable"><a href="#" class="btn btn-primary btn-xs mibtn-seleccionMenuFormulario" data-IdForm="' + MenuListaAccesos[i].IdFormulario + '"><span class="textoMenu">' + (await MenuListaAccesos[i].ObjFormulario()).Nombre + '</span></a></li>';
                    strMenu += '    </div>';
                    strMenu += '    <div>';
                    strMenu += await Acceso.ArmarStrAcciones(MenuListaAccesos[i].IdAccion);
                    strMenu += '    </div>';
                    strMenu += '</div>';
                    i++;
                }
            }
        }
        let strBoton = '';
        strBoton += '<div class="row separadorRow50">';
        strBoton += '    <div class="col-lg-4 text-left"><h3 class="modal-title">Nombre Nuevo Perfil :</h3></div>';
        strBoton += '    <div class="col-lg-5 text-left"><input id="TxtNombreClonado" class="form-control" placeholder="Nombre Nuevo Perfil" type="text" style="width: 100%;" /></div>';
        strBoton += '    <div class="col-lg-2 text-right"><button id="BtnClonar" type="button" class="btn btn-block btn-success">Clonar</button></div>';
        strBoton += '</div>';
        strBoton += '<div class="row separadorRow30">';
        strBoton += '    <div class="col-lg-12 text-center"><h4 class="modal-title">Accesos</h4></div>';
        strBoton += '</div>';
        let control = '';
        if ($("#Modal-PopUpClonar").length == 0) {
            control += '<div id="Modal-PopUpClonar" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header HeaderPopUp">';
            control += '                <div class="row">';
            control += '                    <div class="col-lg-8 text-center"><h2 class="modal-title spanPopUpCabecera">Clonar Perfil ' + this.Nombre + '</h2></div>';
            control += '                </div>';
            control += '            </div>';
            control += '            <div class="modal-body" style="height:450px;">';
            control += strBoton;
            control += strMenu;
            control += '            </div>';
            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        $('#Modal-PopUpClonar').modal({ show: true });
    }
    static async Refresh() {
        _ListaPerfiles = await Perfil.TraerTodas();
    }
}
function llenarEntidadPerfil(entidad) {
    let m = new Perfil;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdRol = entidad.IdRol;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionPerfil", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaPerfiles, function (entidad, index) {
            return entidad.IdEntidad == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let marca = false;
        Seleccionado.marcado = ($this).prop("checked");
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".milink-ModificarAcceso", async function () {
    try {
        $this = $(this);
        let objAcceso = new Acceso();
        objAcceso.IdEntidad = $this.attr("data-Id");
        objAcceso.IdPerfil = $this.attr("data-IdPerfil");
        objAcceso.IdFamilia = $this.attr("data-IdFamilia");
        objAcceso.IdRama = $this.attr("data-IdRama");
        objAcceso.IdFormulario = $this.attr("data-IdFormulario");
        objAcceso.IdAccion = $this.attr("data-IdAccion");
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: objAcceso });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});