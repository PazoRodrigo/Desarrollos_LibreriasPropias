var _ListaAreas;

class Area {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = "";
        this.IdEstado = 0;

        this._ListaUsuarios;
    }

    async ListaUsuarios() {
        try {
            if (this._ListaUsuarios == undefined) {
                this._ListaUsuarios = await Usuario.TraerTodosXArea(this.IdEntidad);
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
            let id = await ejecutarAsync(urlWsArea + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaAreas.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        this.Nombre = this.Nombre.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsArea + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaAreas = await Area.TraerTodos();
            let buscado = $.grep(_ListaAreas, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaAreas = buscado;
            this.IdEstado = 0;
            _ListaAreas.push(this);
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
            let id = await ejecutarAsync(urlWsArea + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaAreas = await Area.TraerTodos();
            let buscado = $.grep(_ListaAreas, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaAreas = buscado;
            this.IdEstado = 1;
            _ListaAreas.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    // Traer
    static async Todos() {
        if (_ListaAreas == undefined) {
            _ListaAreas = await Area.TraerTodas();
        }
        return _ListaAreas;
    }
    static async TraerTodos() {
        return await Area.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsArea + "/TraerTodos");
        _ListaAreas = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadArea(value));
        });
        _ListaAreas = result;
        return _ListaAreas;
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
                let btnSeleccion = '<a href="#" class="btn btn-' + color + ' btn-xs mibtn-seleccionArea glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
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
        await Area.Refresh();
        let lista = await Area.Todos();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 70%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th colspan="2" style="text-align: center;">Areas</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionArea"  name="rblArea" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCheckBoxs(evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div style="' + estilo + '">';
        await Area.Refresh();
        let lista = await Area.Todos();
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-4"><input type="checkbox" class="micbx-Area" name="CkbList_Areas" value="' + item.IdEntidad + '"    id="chk_' + item.IdEntidad + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        str += '</div>';
        return $('#' + div + '').html(str);
    }
    static async Refresh() {
        _ListaAreas = await Area.TraerTodas();
    }
}
function llenarEntidadArea(entidad) {
    let m = new Area;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionArea", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaAreas, function (entidad, index) {
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