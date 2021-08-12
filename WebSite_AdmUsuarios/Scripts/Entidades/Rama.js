var _ListaRamas;

class Rama {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = "";
        this.IdFamilia = 0;
        this.Orden = 0;
        this.IdEstado = 0;

        this._ObjFamilia;
        this._ListaFormularios;
        this._ListaUsuarios;
    }

    async ObjFamilia() {
        try {
            if (this._ObjFamilia == undefined) {
                this._ObjFamilia = await Familia.TraerUno(this.IdFamilia);
            }
            return this._ObjFamilia;
        } catch (e) {
            return new Familia();
        }
    }
    async ListaFormularios() {
        try {
            if (this._ListaFormularios == undefined) {
                this._ListaFormularios = await Formulario.TraerTodosXRama(this.IdEntidad);
            }
            return this._ListaFormularios;
            //return await Formulario.TraerTodosXRama(this.IdEntidad);
        } catch (e) {
            return new Formulario();
        }
    }
    async ListaUsuarios() {
        try {
            if (this._ListaUsuarios == undefined) {
                //this._ListaUsuarios = await Usuario.TraerTodosXRama(this.IdEntidad);
            }
            return this._ListaUsuarios;
        } catch (e) {
            return new Formulario();
        }
    }
    // ABM
    async Alta() {
        //await this.ValidarCampos();
        this.Nombre = this.Nombre.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRama + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaRamas.push(this);
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
            let id = await ejecutarAsync(urlWsRama + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaRamas = await Rama.TraerTodos();
            let buscado = $.grep(_ListaRamas, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaRamas = buscado;
            this.IdEstado = 0;
            _ListaRamas.push(this);
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
            let id = await ejecutarAsync(urlWsRama + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaRamas = await Rama.TraerTodos();
            let buscado = $.grep(_ListaRamas, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaRamas = buscado;
            this.IdEstado = 1;
            _ListaRamas.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    // Traer
    static async TraerUno(IdEntidad) {
        _ListaRamas = await Formulario.TraerTodos();
        let buscado = $.grep(_ListaRamas, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async Todos() {
        if (_ListaRamas == undefined) {
            _ListaRamas = await Rama.TraerTodas();
        }
        return _ListaRamas;
    }
    static async TraerUno(IdEntidad) {
        _ListaRamas = await Rama.TraerTodos();
        let buscado = $.grep(_ListaRamas, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Rama.TraerTodas();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsRama + "/TraerTodos");
        _ListaRamas = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRama(value));
        });
        _ListaRamas = result;
        return _ListaRamas;
    }
    static async TraerTodosXFamilia(IdFamilia) {
        _ListaRamas = await Rama.TraerTodos();
        let buscado = $.grep(_ListaRamas, function (entidad, index) {
            return entidad.IdFamilia == IdFamilia;
        });
        let Encontrado = buscado;
        return Encontrado;
    }
    static async Reordenar(IdRama) {
        let data = {
            "IdRama": IdRama
        };
        let lista = await ejecutarAsync(urlWsRama + "/Reordenar", data);
        _ListaRamas = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRama(value));
        });
        _ListaRamas = result;
        return _ListaRamas;
    }
    // Herramientas
    static async ArmarGrilla(lista, evento, div, estilo, conFamilia) {
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
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionRama glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
                if (conFamilia == true) {
                    str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td><td align="left">{2}</td></tr>', btnSeleccion, item.Nombre, (await item.ObjFamilia()).Nombre);
                } else {
                    str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', btnSeleccion, item.Nombre);
                }
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarRadios(lista, evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        //Rama.Refresh();
        //let lista;
        //if (IdFamilia == 0) {
        //    lista = await Rama.TraerTodas();
        //} else {
        //    lista = await Rama.TraerTodosXFamilia(IdFamilia);
        //}
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            //str += '        <thead>';
            //str += '            <tr>';
            //str += '                <th colspan="2" style="text-align: center;">Ramas</th>';
            //str += '            </tr>';
            //str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionRama"  name="rblRama" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);

    }
    static async ArmarCombo(lista, evento, div) {
        let cbo = "";
        cbo += '<div id="CboRama" class="dropdown">';
        cbo += '    <button id="selectorRamaCombo" class="btn btn-primary dropdown-toggle btn-xs btn-block" type="button" data-toggle="dropdown">Rama';
        cbo += '        <span class="caret"></span>';
        cbo += '    </button>';
        cbo += '    <ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionRama" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '" > ' + this.Nombre + '</a></li>';
        });
        cbo += '    </ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
    static async Refresh() {
        _ListaRamas = await Rama.TraerTodas();
    }
}
function llenarEntidadRama(entidad) {
    let m = new Rama;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdFamilia = entidad.IdFamilia;
    m.Orden = entidad.Orden;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionRama", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaRamas, function (entidad, index) {
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
