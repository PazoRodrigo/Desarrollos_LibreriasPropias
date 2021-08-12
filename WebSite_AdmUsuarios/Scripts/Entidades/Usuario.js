var _ListaUsuarios;

class Usuario {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = "";
        this.IdRol = 0;
        this.Documento_CUIT = 0;
        this.Login = "";
        this.Telefono = "";
        this.CorreoElectronico = "";
        this.IdEstado = 0;

        this._ListaRoles;
        this._ListaAreas;
        this._ListaPerfiles;
        this._ListaSeccionales;
    }


    async ListaRoles() {
        try {
            if (this._ListaRoles == undefined) {
                this._ListaRoles = await Rol.TraerTodosXUsuario(this.IdEntidad);
            }
            return this._ListaRoles;
        } catch (e) {
            return new Rol();
        }
    }
    async ListaPerfiles() {
        try {
            if (this._ListaPerfiles == undefined) {
                this._ListaPerfiles = await Perfil.TraerTodosXRol(this.IdRol);
            }
            return this._ListaPerfiles;
        } catch (e) {
            return new Perfil();
        }
    }
    // ABM
    async Alta() {
        await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        this.Login = this.Login.toUpperCase();
        this.CorreoElectronico = this.CorreoElectronico.toUpperCase();
        console.log(this);
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsUsuario + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaUsuarios.push(this);
            console.log(_ListaUsuarios.length);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        this.Login = this.Login.toUpperCase();
        this.CorreoElectronico = this.CorreoElectronico.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsUsuario + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
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
            let id = await ejecutarAsync(urlWsUsuario + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            return;
        } catch (e) {
            throw e;
        }
    }
    // Validaciones
    async ValidarCampos() {
        let sError = '';
        if (this.Nombre.length == 0) {
            sError += 'Debe ingresar el Nombre \n';
        }
        if (this.Login.length == 0) {
            sError += 'Debe ingresar el Login \n';
        }
        if (this.CorreoElectronico.length == 0) {
            sError += 'Debe ingresar el Correo Electrónico \n';
        } else {
            if (!await validarEmail(this.CorreoElectronico)) {
                sError += 'El Correo Electrónico ingresado es inválido \n';
            }
        }
        if (sError.length > 0) {
            throw (sError);
        }
    }

    // Traer
    static async Todos() {
        if (_ListaUsuarios == undefined) {
            _ListaUsuarios = await Usuario.TraerTodas();
        }
        _ListaUsuarios.sort(SortXNombre);
        return _ListaUsuarios;
    }
    static async TraerTodos() {
        return await Usuario.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodos");
        _ListaUsuarios = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadUsuario(value));
        });
        _ListaUsuarios = result;
        _ListaUsuarios.sort(SortXNombre);
        return _ListaUsuarios;
    }
    static async TraerTodosXArea(IdArea) {
        let data = {
            "IdArea": IdArea
        };
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodosXArea", data);
        _ListaUsuarios = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadUsuario(value));
        });
        _ListaUsuarios = result;
        _ListaUsuarios.sort(SortXNombre);
        return _ListaUsuarios;
    }
    static async TraerTodosXPerfil(IdPerfil) {
        let data = {
            "IdPerfil": IdPerfil
        };
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodosXPerfil", data);
        _ListaUsuarios = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadUsuario(value));
        });
        _ListaUsuarios = result;
        _ListaUsuarios.sort(SortXNombre);
        return _ListaUsuarios;
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
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionUsuario glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
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
        await Usuario.Refresh();
        let lista = await Usuario.TraerTodas();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionUsuario"  name="rblUsuario" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<div class="col-sm-12">{0}{1}</div>', radioSeleccion, item.Nombre);
            }
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarLista(lista, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div class="row separadorRow50">';
        str += '<div class="col-lg-12 text-center"> <span class="spanContenidoCabecera">Usuarios</span> </div>';
        str += '</div >';
        if (lista.length > 0) {
            for (let item of lista) {
                str += String.format('<div class="col-lg-3">{0}</div>', item.Nombre);
            }
        }
        return $('#' + div + '').html(str);
    }
    static async Refresh() {
        _ListaUsuarios = await Usuario.TraerTodas();
    }
}
function llenarEntidadUsuario(entidad) {
    let m = new Usuario;
    m.IdEntidad = entidad.IdEntidad;
    m.IdRol = entidad.IdRol;
    m.Nombre = entidad.Nombre;
    m.Documento_CUIT = entidad.Documento_CUIT;
    m.Login = entidad.Login;
    m.Telefono = entidad.Telefono;
    m.CorreoElectronico = entidad.CorreoElectronico;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionUsuario", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaUsuarios, function (entidad, index) {
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
