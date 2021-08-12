var _ListaFamilias;

class Familia {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = "";
        this.IdEstado = 0;

        this._ListaRamas;
    }

    async ListaRamas() {
        try {
            if (this._ListaRamas == undefined) {
                this._ListaRamas = await Rama.TraerTodosXFamilia(this.IdEntidad);
            }
            return this._ListaRamas;
        } catch (e) {
            throw new Rama();
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
            let id = await ejecutarAsync(urlWsFamilia + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _ListaFamilias.push(this);
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
            let id = await ejecutarAsync(urlWsFamilia + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaFamilias = await Familia.TraerTodos();
            let buscado = $.grep(_ListaFamilias, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaFamilias = buscado;
            this.IdEstado = 0;
            _ListaFamilias.push(this);
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
            let id = await ejecutarAsync(urlWsFamilia + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;

            _ListaFamilias = await Familia.TraerTodos();
            let buscado = $.grep(_ListaFamilias, function (entidad, index) {
                return entidad.IdEntidad != id;
            });
            _ListaFamilias = buscado;
            this.IdEstado = 1;
            _ListaFamilias.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    // Traer
    static async Todos() {
        if (_ListaFamilias == undefined) {
            _ListaFamilias = await Familia.TraerTodas();
        }
        return _ListaFamilias;
    }
    static async TraerUno(IdEntidad) {
        _ListaFamilias = await Familia.TraerTodos();
        let buscado = $.grep(_ListaFamilias, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Familia.TraerTodas();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsFamilia + "/TraerTodos");
        _ListaFamilias = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadFamilia(value));
        });
        _ListaFamilias = result;
        return _ListaFamilias;
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
                let btnSeleccion = '<a href="#"  class="btn btn-' + color + ' btn-xs mibtn-seleccionFamilia glyphicon glyphicon-' + grafico + '" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '"></a></td>';
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
        await Familia.Refresh();
        let lista = await Familia.Todos();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            //str += '        <thead>';
            //str += '            <tr>';
            //str += '                <th colspan="2" style="text-align: center;">Familias</th>';
            //str += '            </tr>';
            //str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionFamilia"  name="rblFamilia" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
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
        cbo += '<div id="CboFamilia" class="dropdown">';
        cbo += '    <button id="selectorFamiliaCombo" class="btn btn-primary dropdown-toggle btn-xs btn-block" type="button" data-toggle="dropdown">Familia';
        cbo += '        <span class="caret"></span>';
        cbo += '    </button>';
        cbo += '    <ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionFamilia" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '" > ' + this.Nombre + '</a></li>';
        });
        cbo += '    </ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
    async ArmarGrillaContenido(div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        let ListaRamas = await this.ListaRamas();
        ListaRamas.sort(SortXNombre);
        if (ListaRamas.length > 0) {
            str += '<div id="DivGrillaContenido1" style="width: 100%;">';
            str += '	<div class="row separadorRow30">';
            str += '		<div class="col-lg-3 text-center"><span class="spanContenidoCabecera">Rama</span></div>';
            str += '		<div class="col-lg-5 text-center"><span class="spanContenidoCabecera">Formulario</span></div>';
            str += '		<div class="col-lg-4 text-center"><span class="spanContenidoCabecera">Accesos</span></div>';
            str += '	</div>';
            str += '</div>';
            str += '<div id="DivGrillaContenido2" style="width: 100%; ' + estilo + '">';
            str += '	<div id="DivGrillaContenido3" style="width: 95%;">';
            for (let rama of ListaRamas) {
                let ListaFormularios = await rama.ListaFormularios();
                for (let form of ListaFormularios) {
                    str += '<div class="row">';
                    str += '    <div class="col-lg-3">' + rama.Nombre + '</div>';
                    str += '    <div class="col-lg-5">' + form.Nombre + '</div>';
                    str += '    <div class="col-lg-4">' + await Acceso.ArmarStrAcciones(form.AccionesPosibles) + '</div>';
                    str += '</div>';
                }
            }
            str += '	</div>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async Refresh() {
        _ListaFamilias = await Familia.TraerTodas();
    }
}
function llenarEntidadFamilia(entidad) {
    let m = new Familia;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    m.IdEstado = entidad.IdEstado;
    return m;
}
$('body').on('click', ".mibtn-seleccionFamilia", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaFamilias, function (entidad, index) {
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
