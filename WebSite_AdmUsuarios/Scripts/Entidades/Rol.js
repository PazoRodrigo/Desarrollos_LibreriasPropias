var _ListaRoles;

class Rol {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
        this.IdEstado = 0;
    }

    // ABM
    async Alta() {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRol + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaRoles.push(this);
            console.log(_ListaRoles.length);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRol + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaRoles = await Rol.TraerTodos();
            let buscado = $.grep(_ListaRoles, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _ListaRoles = buscado;
            this.IdEstado = 0;
            _ListaRoles.push(this);
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
            console.log(this);
            let id = await ejecutarAsync(urlWsRol + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaRoles = await Rol.TraerTodos();
            let buscado = $.grep(_ListaRoles, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaRoles = buscado;
            console.log(this);

            this.IdEstado = 1;
            console.log(this);

            _ListaRoles.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _ListaRoles = await Rol.TraerTodos();
        let buscado = $.grep(_ListaRoles, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async Todos() {
        if (_ListaRoles == undefined) {
            _ListaRoles = await Rol.TraerTodas();
        }
        _ListaRoles.sort(SortXNombre);
        return _ListaRoles;
    }
    static async TraerTodos() {
        return await Rol.Todos();
    }
    static async TraerTodosXUsuario(IdUsuario) {
        let data = {
            "IdUsuario": IdUsuario
        }
        let lista = await ejecutarAsync(urlWsRol + "/TraerTodosXUsuario", data);
        _ListaRoles = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRol(value));
        });
        _ListaRoles = result;
        _ListaRoles.sort(SortXNombre);
        return result;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsRol + "/TraerTodos");
        _ListaRoles = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRol(value));
        });
        _ListaRoles = result;
        _ListaRoles.sort(SortXNombre);
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
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionRol glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', btnSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarRadios(evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        await Rol.Refresh();
        let lista = await Rol.TraerTodas();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionRol"  name="rblRol" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<div class="col-sm-12">{0}{1}</div>', radioSeleccion, item.Nombre);
            }
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCheckBoxs(evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        await Rol.Refresh();
        let lista = await Rol.Todos();
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-12"><input type="checkbox" class="micbx-Area" name="CkbList_Roles" ' + estilo + ' value="' + item.IdEntidad + '" id="chk_' + item.IdEntidad + '" data-Evento="' + evento + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCombo(lista, evento, div) {
        let cbo = "";
        cbo += '<div id="CboRol" class="dropdown">';
        cbo += '    <button id="selectorRolCombo" class="btn btn-primary dropdown-toggle btn-md btn-block" type="button" data-toggle="dropdown">Rol';
        cbo += '        <span class="caret"></span>';
        cbo += '    </button>';
        cbo += '    <ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionRol" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '" > ' + this.Nombre + '</a></li>';
        });
        cbo += '    </ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
    static async Refresh() {
        _ListaRoles = await Rol.TraerTodas();
    }
}
function llenarEntidadRol(entidad) {
    let m = new Rol;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionRol", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaRoles, function (entidad, index) {
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